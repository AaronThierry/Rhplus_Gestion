using System;
using System.Security.Cryptography;
using System.Text;

namespace RH_GRH.Auth
{
    /// <summary>
    /// Service de hachage et vérification des mots de passe
    /// Utilise BCrypt pour un hachage sécurisé
    /// </summary>
    public static class PasswordHasher
    {
        private const int WORK_FACTOR = 11; // Facteur de travail BCrypt (2^11 iterations)

        /// <summary>
        /// Hache un mot de passe en utilisant BCrypt
        /// </summary>
        /// <param name="password">Mot de passe en clair</param>
        /// <returns>Hash BCrypt du mot de passe</returns>
        public static string HashPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Le mot de passe ne peut pas être vide", nameof(password));
            }

            return BCrypt.Net.BCrypt.HashPassword(password, WORK_FACTOR);
        }

        /// <summary>
        /// Vérifie si un mot de passe correspond à un hash
        /// </summary>
        /// <param name="password">Mot de passe en clair à vérifier</param>
        /// <param name="hash">Hash BCrypt stocké</param>
        /// <returns>True si le mot de passe correspond, False sinon</returns>
        public static bool VerifyPassword(string password, string hash)
        {
            if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(hash))
            {
                return false;
            }

            try
            {
                return BCrypt.Net.BCrypt.Verify(password, hash);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Génère un mot de passe aléatoire sécurisé
        /// </summary>
        /// <param name="length">Longueur du mot de passe (minimum 8)</param>
        /// <returns>Mot de passe aléatoire</returns>
        public static string GenerateRandomPassword(int length = 12)
        {
            if (length < 8)
            {
                throw new ArgumentException("La longueur minimale du mot de passe est 8 caractères", nameof(length));
            }

            const string uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string lowercase = "abcdefghijklmnopqrstuvwxyz";
            const string digits = "0123456789";
            const string special = "@#$%&*+-=!";
            const string allChars = uppercase + lowercase + digits + special;

            using (var rng = new RNGCryptoServiceProvider())
            {
                var data = new byte[length];
                rng.GetBytes(data);

                var result = new StringBuilder(length);

                // Assurer au moins un caractère de chaque catégorie
                result.Append(uppercase[data[0] % uppercase.Length]);
                result.Append(lowercase[data[1] % lowercase.Length]);
                result.Append(digits[data[2] % digits.Length]);
                result.Append(special[data[3] % special.Length]);

                // Remplir le reste avec des caractères aléatoires
                for (int i = 4; i < length; i++)
                {
                    result.Append(allChars[data[i] % allChars.Length]);
                }

                // Mélanger les caractères
                return ShuffleString(result.ToString(), data);
            }
        }

        /// <summary>
        /// Valide la force d'un mot de passe
        /// </summary>
        /// <param name="password">Mot de passe à valider</param>
        /// <returns>Tuple (isValid, errorMessage)</returns>
        public static (bool IsValid, string ErrorMessage) ValidatePasswordStrength(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                return (false, "Le mot de passe ne peut pas être vide");
            }

            if (password.Length < 8)
            {
                return (false, "Le mot de passe doit contenir au moins 8 caractères");
            }

            if (password.Length > 128)
            {
                return (false, "Le mot de passe ne peut pas dépasser 128 caractères");
            }

            bool hasUpper = false;
            bool hasLower = false;
            bool hasDigit = false;
            bool hasSpecial = false;

            foreach (char c in password)
            {
                if (char.IsUpper(c)) hasUpper = true;
                else if (char.IsLower(c)) hasLower = true;
                else if (char.IsDigit(c)) hasDigit = true;
                else if (!char.IsLetterOrDigit(c)) hasSpecial = true;
            }

            if (!hasUpper)
            {
                return (false, "Le mot de passe doit contenir au moins une lettre majuscule");
            }

            if (!hasLower)
            {
                return (false, "Le mot de passe doit contenir au moins une lettre minuscule");
            }

            if (!hasDigit)
            {
                return (false, "Le mot de passe doit contenir au moins un chiffre");
            }

            if (!hasSpecial)
            {
                return (false, "Le mot de passe doit contenir au moins un caractère spécial (@#$%&*+-=!)");
            }

            return (true, string.Empty);
        }

        /// <summary>
        /// Mélange une chaîne de caractères de manière cryptographiquement sécurisée
        /// </summary>
        private static string ShuffleString(string input, byte[] randomData)
        {
            char[] array = input.ToCharArray();
            int n = array.Length;

            for (int i = n - 1; i > 0; i--)
            {
                int j = randomData[i % randomData.Length] % (i + 1);
                // Swap
                char temp = array[i];
                array[i] = array[j];
                array[j] = temp;
            }

            return new string(array);
        }
    }
}

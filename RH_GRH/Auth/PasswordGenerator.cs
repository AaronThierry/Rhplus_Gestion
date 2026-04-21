using System;
using System.Text;

namespace RH_GRH.Auth
{
    /// <summary>
    /// Générateur de mots de passe par défaut pour les nouveaux utilisateurs
    /// </summary>
    public static class PasswordGenerator
    {
        /// <summary>
        /// Mot de passe par défaut fixe pour tous les nouveaux utilisateurs
        /// </summary>
        public const string DEFAULT_PASSWORD = "RHPlus2026!";

        /// <summary>
        /// Génère un mot de passe par défaut simple et mémorable
        /// Format: RHPlus2026!
        /// </summary>
        /// <returns>Mot de passe par défaut</returns>
        public static string GenerateDefaultPassword()
        {
            return DEFAULT_PASSWORD;
        }

        /// <summary>
        /// Génère un mot de passe aléatoire sécurisé (optionnel pour usage futur)
        /// </summary>
        /// <param name="length">Longueur du mot de passe (8-20 caractères)</param>
        /// <returns>Mot de passe aléatoire</returns>
        public static string GenerateRandomPassword(int length = 12)
        {
            if (length < 8) length = 8;
            if (length > 20) length = 20;

            const string uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string lowercase = "abcdefghijklmnopqrstuvwxyz";
            const string digits = "0123456789";
            const string special = "!@#$%&*";

            Random random = new Random();
            StringBuilder password = new StringBuilder();

            // Assurer au moins un caractère de chaque type
            password.Append(uppercase[random.Next(uppercase.Length)]);
            password.Append(lowercase[random.Next(lowercase.Length)]);
            password.Append(digits[random.Next(digits.Length)]);
            password.Append(special[random.Next(special.Length)]);

            // Remplir le reste
            string allChars = uppercase + lowercase + digits + special;
            for (int i = 4; i < length; i++)
            {
                password.Append(allChars[random.Next(allChars.Length)]);
            }

            // Mélanger les caractères
            return ShuffleString(password.ToString());
        }

        /// <summary>
        /// Mélange les caractères d'une chaîne
        /// </summary>
        private static string ShuffleString(string input)
        {
            char[] array = input.ToCharArray();
            Random random = new Random();

            for (int i = array.Length - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                char temp = array[i];
                array[i] = array[j];
                array[j] = temp;
            }

            return new string(array);
        }

        /// <summary>
        /// Valide la force d'un mot de passe
        /// </summary>
        /// <param name="password">Mot de passe à valider</param>
        /// <param name="errorMessage">Message d'erreur si invalide</param>
        /// <returns>True si valide</returns>
        public static bool ValidatePasswordStrength(string password, out string errorMessage)
        {
            errorMessage = "";

            if (string.IsNullOrWhiteSpace(password))
            {
                errorMessage = "Le mot de passe ne peut pas être vide";
                return false;
            }

            if (password.Length < 8)
            {
                errorMessage = "Le mot de passe doit contenir au moins 8 caractères";
                return false;
            }

            if (password.Length > 50)
            {
                errorMessage = "Le mot de passe ne peut pas dépasser 50 caractères";
                return false;
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
                else if ("!@#$%^&*()_+-=[]{}|;:,.<>?".Contains(c.ToString())) hasSpecial = true;
            }

            if (!hasUpper)
            {
                errorMessage = "Le mot de passe doit contenir au moins une majuscule";
                return false;
            }

            if (!hasLower)
            {
                errorMessage = "Le mot de passe doit contenir au moins une minuscule";
                return false;
            }

            if (!hasDigit)
            {
                errorMessage = "Le mot de passe doit contenir au moins un chiffre";
                return false;
            }

            if (!hasSpecial)
            {
                errorMessage = "Le mot de passe doit contenir au moins un caractère spécial (!@#$%^&*...)";
                return false;
            }

            return true;
        }

        /// <summary>
        /// Vérifie si un mot de passe est le mot de passe par défaut
        /// </summary>
        public static bool IsDefaultPassword(string password)
        {
            return password == DEFAULT_PASSWORD;
        }
    }
}

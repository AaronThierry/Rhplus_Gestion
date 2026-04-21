using MySql.Data.MySqlClient;
using System;

namespace RH_GRH
{
    /// <summary>
    /// Classe pour générer automatiquement les numéros de police uniques
    /// Format: XXXAXXX (ex: 123A456, 789B012, etc.)
    /// 3 chiffres + 1 lettre majuscule + 3 chiffres
    /// </summary>
    public static class PoliceGenerator
    {
        private static readonly Random random = new Random();
        private static readonly object lockObject = new object();

        /// <summary>
        /// Génère un nouveau numéro de police unique de 7 caractères
        /// Format: 3 chiffres + 1 lettre + 3 chiffres (exemple: 123A456)
        /// </summary>
        /// <returns>Numéro de police aléatoire unique</returns>
        public static string GenererNumeroPolice()
        {
            const int maxTentatives = 100;
            int tentatives = 0;

            while (tentatives < maxTentatives)
            {
                string numeroPolice = GenererNumeroAleatoire();

                // Vérifier que le numéro n'existe pas déjà
                if (!NumeroPoliceExiste(numeroPolice))
                {
                    return numeroPolice;
                }

                tentatives++;
            }

            throw new Exception($"Impossible de générer un numéro de police unique après {maxTentatives} tentatives. Veuillez réessayer.");
        }

        /// <summary>
        /// Génère un numéro de police aléatoire au format XXXAXXX
        /// </summary>
        /// <returns>Numéro de 7 caractères: 3 chiffres + 1 lettre + 3 chiffres</returns>
        private static string GenererNumeroAleatoire()
        {
            lock (lockObject)
            {
                // Générer 3 premiers chiffres (000-999)
                int partie1 = random.Next(0, 1000);

                // Générer 1 lettre majuscule (A-Z)
                char lettre = (char)random.Next('A', 'Z' + 1);

                // Générer 3 derniers chiffres (000-999)
                int partie2 = random.Next(0, 1000);

                // Formater: 123A456
                return $"{partie1:D3}{lettre}{partie2:D3}";
            }
        }

        /// <summary>
        /// Vérifie si un numéro de police existe déjà dans la base de données
        /// </summary>
        /// <param name="numeroPolice">Numéro de police à vérifier</param>
        /// <returns>True si le numéro existe déjà, False sinon</returns>
        public static bool NumeroPoliceExiste(string numeroPolice)
        {
            try
            {
                var connect = new Dbconnect();
                using (var connection = connect.getconnection)
                {
                    connection.Open();

                    string query = @"
                        SELECT COUNT(*)
                        FROM personnel
                        WHERE police = @police";

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@police", numeroPolice);

                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erreur lors de la vérification du numéro de police: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Compte le nombre total de numéros de police générés
        /// </summary>
        /// <returns>Nombre d'employés ayant un numéro de police</returns>
        public static int GetNombreNumerosPolice()
        {
            try
            {
                var connect = new Dbconnect();
                using (var connection = connect.getconnection)
                {
                    connection.Open();

                    string query = @"
                        SELECT COUNT(*)
                        FROM personnel
                        WHERE police IS NOT NULL";

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        return Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erreur lors du comptage des numéros de police: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Valide le format d'un numéro de police (XXXAXXX)
        /// </summary>
        /// <param name="numeroPolice">Numéro à valider</param>
        /// <returns>True si le format est valide, False sinon</returns>
        public static bool ValiderFormatNumeroPolice(string numeroPolice)
        {
            if (string.IsNullOrWhiteSpace(numeroPolice) || numeroPolice.Length != 7)
                return false;

            // Vérifier format: 3 chiffres + 1 lettre + 3 chiffres
            for (int i = 0; i < 7; i++)
            {
                if (i < 3 || i > 3) // Positions 0,1,2 et 4,5,6 doivent être des chiffres
                {
                    if (!char.IsDigit(numeroPolice[i]))
                        return false;
                }
                else if (i == 3) // Position 3 doit être une lettre majuscule
                {
                    if (!char.IsUpper(numeroPolice[i]))
                        return false;
                }
            }

            return true;
        }
    }
}

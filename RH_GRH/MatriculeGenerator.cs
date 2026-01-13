using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RH_GRH
{
    public static class MatriculeGenerator
    {
        public static string GetInitialesEntreprise(string nomEntreprise)
        {
            if (string.IsNullOrWhiteSpace(nomEntreprise)) return "XX";
            var mots = nomEntreprise.Trim()
                .Replace("-", " ").Replace("_", " ")
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            return (mots.Length == 1)
                ? mots[0].Substring(0, Math.Min(2, mots[0].Length)).ToUpper()
                : string.Concat(mots[0][0], mots[mots.Length - 1][0]).ToUpper();
        }

        // ➜ Surcharge "standalone" (garde si tu veux l'utiliser ailleurs, hors service)
        public static async Task<string> NextAsync(MySqlConnection cn, string nomEntreprise, CancellationToken ct = default)
        {
            if (cn.State != System.Data.ConnectionState.Open)
                await cn.OpenAsync(ct);

            using (var tx = await cn.BeginTransactionAsync(ct))
            {
                try
                {
                    var m = await NextAsync(cn, (MySqlTransaction)tx, nomEntreprise, ct); // <-- utilise la transaction
                    await ((MySqlTransaction)tx).CommitAsync(ct);
                    return m;
                }
                catch
                {
                    await ((MySqlTransaction)tx).RollbackAsync(ct);
                    throw;
                }
            }
        }

        // ➜ NOUVELLE surcharge : utilise une transaction existante (AUCUN Begin/Commit ici)
        public static async Task<string> NextAsync(
            MySqlConnection cn,
            MySqlTransaction tx,
            string nomEntreprise,
            CancellationToken ct = default)
        {
            // 1) Séquence atomique dans la transaction fournie
            using (var insertCmd = new MySqlCommand("INSERT INTO seq_matricule() VALUES ();", cn, tx))
                await insertCmd.ExecuteNonQueryAsync(ct);

            long n;
            using (var selectCmd = new MySqlCommand("SELECT LAST_INSERT_ID();", cn, tx))
                n = Convert.ToInt64(await selectCmd.ExecuteScalarAsync(ct));

            // 2) Numéro & lettre
            int num = (int)((n - 1) % 999) + 1;
            int letterIndex = (int)((n - 1) / 999);
            char lettre = (char)('A' + (letterIndex % 26));

            // 3) Préfixe initiales
            string prefix = GetInitialesEntreprise(nomEntreprise);

            return $"{prefix}{num:000}{lettre}";
        }

        // Méthode synchrone pour générer un matricule (version simplifiée)
        public static string GenererMatricule(int idEntreprise)
        {
            string logPath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "rh_debug_log.txt");
            try
            {
                System.IO.File.AppendAllText(logPath, $"{DateTime.Now}: [MAT] Début GenererMatricule, idEntreprise={idEntreprise}\n");
                var dbconnect = new Dbconnect();
                System.IO.File.AppendAllText(logPath, $"{DateTime.Now}: [MAT] Dbconnect créé\n");

                using (MySqlConnection cn = dbconnect.getconnection)
                {
                    System.IO.File.AppendAllText(logPath, $"{DateTime.Now}: [MAT] Connection obtenue, State={cn.State}\n");
                    if (cn.State != System.Data.ConnectionState.Open)
                    {
                        System.IO.File.AppendAllText(logPath, $"{DateTime.Now}: [MAT] Ouverture connexion...\n");
                        cn.Open();
                        System.IO.File.AppendAllText(logPath, $"{DateTime.Now}: [MAT] Connexion ouverte\n");
                    }

                    // Récupérer le nom de l'entreprise
                    string nomEntreprise = "";
                    System.IO.File.AppendAllText(logPath, $"{DateTime.Now}: [MAT] Avant SELECT nomEntreprise\n");
                    using (var cmd = new MySqlCommand("SELECT nomEntreprise FROM entreprise WHERE id_entreprise = @id", cn))
                    {
                        cmd.Parameters.AddWithValue("@id", idEntreprise);
                        System.IO.File.AppendAllText(logPath, $"{DateTime.Now}: [MAT] Avant ExecuteScalar\n");
                        var result = cmd.ExecuteScalar();
                        System.IO.File.AppendAllText(logPath, $"{DateTime.Now}: [MAT] Après ExecuteScalar, result={result}\n");
                        nomEntreprise = result?.ToString() ?? "XX";
                        System.IO.File.AppendAllText(logPath, $"{DateTime.Now}: [MAT] nomEntreprise={nomEntreprise}\n");
                    }

                    // Utiliser la méthode asynchrone de manière synchrone avec Task.Run pour éviter deadlock
                    System.IO.File.AppendAllText(logPath, $"{DateTime.Now}: [MAT] Avant Task.Run NextAsync\n");
                    var matricule = System.Threading.Tasks.Task.Run(async () => await NextAsync(cn, nomEntreprise)).Result;
                    System.IO.File.AppendAllText(logPath, $"{DateTime.Now}: [MAT] Après NextAsync, matricule={matricule}\n");
                    return matricule;
                }
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText(logPath, $"{DateTime.Now}: [MAT] EXCEPTION: {ex.ToString()}\n");
                // En cas d'erreur, retourner un matricule temporaire
                return $"TEMP{DateTime.Now.Ticks % 10000:0000}";
            }
        }
    }

}

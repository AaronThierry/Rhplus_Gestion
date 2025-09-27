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
    }

}

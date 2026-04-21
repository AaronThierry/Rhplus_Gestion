// Test rapide pour BCrypt - À exécuter dans une console C#
// Ce code génère le hash pour "Admin@123" et le vérifie

using System;

class Program
{
    static void Main()
    {
        string password = "Admin@123";

        // Hash qui devrait être dans la base de données
        string storedHash = "$2a$11$7rLSvRVyTfIapPwVu.bEy.KqV5pDfKCrRfKpG/QNVQbW7VqkN6qZG";

        // Générer un nouveau hash
        string newHash = BCrypt.Net.BCrypt.HashPassword(password, 11);

        Console.WriteLine("Mot de passe: " + password);
        Console.WriteLine("Hash stocké:  " + storedHash);
        Console.WriteLine("Longueur:     " + storedHash.Length + " caractères");
        Console.WriteLine();
        Console.WriteLine("Nouveau hash: " + newHash);
        Console.WriteLine("Longueur:     " + newHash.Length + " caractères");
        Console.WriteLine();

        // Vérifier le hash stocké
        bool isValid1 = BCrypt.Net.BCrypt.Verify(password, storedHash);
        Console.WriteLine("Vérification du hash stocké: " + (isValid1 ? "✓ VALIDE" : "✗ INVALIDE"));

        // Vérifier le nouveau hash
        bool isValid2 = BCrypt.Net.BCrypt.Verify(password, newHash);
        Console.WriteLine("Vérification du nouveau hash: " + (isValid2 ? "✓ VALIDE" : "✗ INVALIDE"));
    }
}

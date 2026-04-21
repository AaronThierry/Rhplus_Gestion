// FICHIER DE TEST - Peut être supprimé après vérification
// Ce fichier teste si Visual Studio peut voir PasswordGenerator et ChangerMotDePasseObligatoireForm

using System;
using RH_GRH.Auth;

namespace RH_GRH
{
    /// <summary>
    /// Classe de test pour vérifier que Visual Studio voit les nouvelles classes
    /// </summary>
    internal class TestPasswordGenerator
    {
        public static void Test()
        {
            // Test 1: PasswordGenerator doit être visible
            string motDePasse = PasswordGenerator.GenerateDefaultPassword();
            Console.WriteLine($"Mot de passe par défaut: {motDePasse}");

            // Test 2: Validation du mot de passe
            bool isValid = PasswordGenerator.ValidatePasswordStrength("TestMotDePasse123!", out string error);
            Console.WriteLine($"Validation: {isValid}, Erreur: {error}");

            // Test 3: ChangerMotDePasseObligatoireForm doit être visible
            // (Ne pas instancier, juste vérifier que le type existe)
            Type formType = typeof(ChangerMotDePasseObligatoireForm);
            Console.WriteLine($"Type du formulaire: {formType.Name}");

            Console.WriteLine("\n✅ TOUS LES TESTS REUSSIS - Visual Studio voit les nouvelles classes !");
        }
    }
}

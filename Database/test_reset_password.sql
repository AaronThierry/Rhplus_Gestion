-- ================================================================================
-- SCRIPT DE TEST - SYSTÈME DE RÉINITIALISATION DE MOT DE PASSE
-- Date: 2026-03-16
-- Version: 1.1.5
-- ================================================================================

USE gmp_rh_gestion;

-- ================================================================================
-- 1. CRÉER UN UTILISATEUR DE TEST
-- ================================================================================

SELECT '=== CRÉATION D''UN UTILISATEUR DE TEST ===' AS '';

-- Supprimer l'utilisateur de test s'il existe déjà
DELETE FROM utilisateur_roles WHERE utilisateur_id IN (SELECT id FROM utilisateurs WHERE nom_utilisateur = 'test_reset');
DELETE FROM utilisateurs WHERE nom_utilisateur = 'test_reset';

-- Créer l'utilisateur de test avec le mot de passe par défaut
-- Hash BCrypt pour "RHPlus2026!"
INSERT INTO utilisateurs (
    nom_utilisateur,
    mot_de_passe_hash,
    nom_complet,
    email,
    telephone,
    actif,
    premier_connexion,
    mot_de_passe_par_defaut,
    tentatives_echec,
    compte_verrouille
) VALUES (
    'test_reset',
    '$2a$11$7rLSvRVyTfIapPwVu.bEy.KqV5pDfKCrRfKpG/QNVQbW7VqkN6qZG', -- Hash de "RHPlus2026!"
    'Utilisateur Test Réinitialisation',
    'test@gmp-rh.local',
    '0123456789',
    1,
    TRUE,
    'RHPlus2026!',
    0,
    0
);

-- Assigner le rôle Assistant RH
INSERT INTO utilisateur_roles (utilisateur_id, role_id)
SELECT u.id, r.id
FROM utilisateurs u
CROSS JOIN roles r
WHERE u.nom_utilisateur = 'test_reset'
AND r.nom_role = 'Assistant RH';

SELECT 'Utilisateur de test créé avec succès !' AS resultat;
SELECT '  Nom d''utilisateur: test_reset' AS '';
SELECT '  Mot de passe: RHPlus2026!' AS '';
SELECT '  Première connexion: OUI (changement obligatoire)' AS '';

-- ================================================================================
-- 2. VÉRIFIER L'UTILISATEUR DE TEST
-- ================================================================================

SELECT '=== VÉRIFICATION DE L''UTILISATEUR DE TEST ===' AS '';

SELECT
    u.id,
    u.nom_utilisateur,
    u.nom_complet,
    u.email,
    u.actif AS 'Actif',
    u.premier_connexion AS 'Première connexion',
    u.mot_de_passe_par_defaut AS 'Mot de passe par défaut',
    u.tentatives_echec AS 'Tentatives échouées',
    u.compte_verrouille AS 'Verrouillé',
    GROUP_CONCAT(r.nom_role SEPARATOR ', ') AS 'Rôles'
FROM utilisateurs u
LEFT JOIN utilisateur_roles ur ON u.id = ur.utilisateur_id
LEFT JOIN roles r ON ur.role_id = r.id
WHERE u.nom_utilisateur = 'test_reset'
GROUP BY u.id;

-- ================================================================================
-- 3. SIMULER UN VERROUILLAGE DE COMPTE (5 tentatives échouées)
-- ================================================================================

SELECT '=== SIMULATION D''UN VERROUILLAGE DE COMPTE ===' AS '';

UPDATE utilisateurs
SET tentatives_echec = 5,
    compte_verrouille = 1
WHERE nom_utilisateur = 'test_reset';

SELECT
    nom_utilisateur,
    tentatives_echec AS 'Tentatives',
    compte_verrouille AS 'Verrouillé'
FROM utilisateurs
WHERE nom_utilisateur = 'test_reset';

SELECT 'Compte verrouillé simulé !' AS resultat;

-- ================================================================================
-- 4. SIMULER UNE RÉINITIALISATION PAR L'ADMIN
-- ================================================================================

SELECT '=== SIMULATION D''UNE RÉINITIALISATION ===' AS '';

-- C'est exactement ce que fait l'interface admin
UPDATE utilisateurs
SET mot_de_passe_hash = '$2a$11$7rLSvRVyTfIapPwVu.bEy.KqV5pDfKCrRfKpG/QNVQbW7VqkN6qZG',
    tentatives_echec = 0,
    compte_verrouille = 0,
    premier_connexion = TRUE,
    mot_de_passe_par_defaut = 'RHPlus2026!',
    date_modification = NOW()
WHERE nom_utilisateur = 'test_reset';

-- Logger l'événement
INSERT INTO logs_activite (
    utilisateur_id,
    nom_utilisateur,
    action,
    module,
    details,
    resultat
)
SELECT
    u.id,
    'admin',
    'RESET_PASSWORD_TEST',
    'Système',
    CONCAT('Test de réinitialisation pour utilisateur: test_reset (ID: ', u.id, ')'),
    'SUCCESS'
FROM utilisateurs u
WHERE u.nom_utilisateur = 'test_reset';

SELECT
    nom_utilisateur,
    tentatives_echec AS 'Tentatives',
    compte_verrouille AS 'Verrouillé',
    premier_connexion AS 'Première connexion',
    mot_de_passe_par_defaut AS 'Mot de passe'
FROM utilisateurs
WHERE nom_utilisateur = 'test_reset';

SELECT 'Réinitialisation simulée avec succès !' AS resultat;

-- ================================================================================
-- 5. SIMULER LE CHANGEMENT DE MOT DE PASSE PAR L'UTILISATEUR
-- ================================================================================

SELECT '=== SIMULATION DU CHANGEMENT DE MOT DE PASSE ===' AS '';

-- Hash BCrypt pour "NouveauMotDePasse2026!"
UPDATE utilisateurs
SET mot_de_passe_hash = '$2a$11$V8KqZvLN6pY3xH5mJxWlLu9hGqT2yRfVwXnZ4kPmQsEaB7CdFgHiJ',
    premier_connexion = FALSE,
    mot_de_passe_par_defaut = NULL,
    date_modification = NOW()
WHERE nom_utilisateur = 'test_reset';

-- Logger l'événement
INSERT INTO logs_activite (
    utilisateur_id,
    nom_utilisateur,
    action,
    module,
    details,
    resultat
)
SELECT
    id,
    nom_utilisateur,
    'PASSWORD_CHANGE_FIRST_LOGIN_TEST',
    'Sécurité',
    'Test de changement de mot de passe obligatoire effectué',
    'SUCCESS'
FROM utilisateurs
WHERE nom_utilisateur = 'test_reset';

SELECT
    nom_utilisateur,
    premier_connexion AS 'Première connexion',
    mot_de_passe_par_defaut AS 'Mot de passe par défaut'
FROM utilisateurs
WHERE nom_utilisateur = 'test_reset';

SELECT 'Changement de mot de passe simulé avec succès !' AS resultat;
SELECT '  L''utilisateur peut maintenant se connecter normalement' AS '';
SELECT '  Le flag première connexion est désactivé' AS '';

-- ================================================================================
-- 6. VÉRIFIER LES LOGS D'AUDIT
-- ================================================================================

SELECT '=== LOGS D''AUDIT DES TESTS ===' AS '';

SELECT
    id,
    nom_utilisateur,
    action,
    module,
    details,
    date_action,
    resultat
FROM logs_activite
WHERE action IN ('RESET_PASSWORD_TEST', 'PASSWORD_CHANGE_FIRST_LOGIN_TEST')
ORDER BY date_action DESC
LIMIT 10;

-- ================================================================================
-- 7. STATISTIQUES GLOBALES
-- ================================================================================

SELECT '=== STATISTIQUES GLOBALES ===' AS '';

SELECT
    COUNT(*) AS 'Total utilisateurs',
    SUM(CASE WHEN actif = 1 THEN 1 ELSE 0 END) AS 'Actifs',
    SUM(CASE WHEN compte_verrouille = 1 THEN 1 ELSE 0 END) AS 'Verrouillés',
    SUM(CASE WHEN premier_connexion = TRUE THEN 1 ELSE 0 END) AS 'Première connexion',
    SUM(CASE WHEN tentatives_echec > 0 THEN 1 ELSE 0 END) AS 'Avec tentatives échouées'
FROM utilisateurs;

-- ================================================================================
-- 8. NETTOYAGE (Optionnel)
-- ================================================================================

SELECT '=== NETTOYAGE (décommentez pour supprimer l''utilisateur de test) ===' AS '';

/*
-- Supprimer l'utilisateur de test
DELETE FROM utilisateur_roles WHERE utilisateur_id IN (SELECT id FROM utilisateurs WHERE nom_utilisateur = 'test_reset');
DELETE FROM utilisateurs WHERE nom_utilisateur = 'test_reset';

-- Supprimer les logs de test
DELETE FROM logs_activite WHERE action IN ('RESET_PASSWORD_TEST', 'PASSWORD_CHANGE_FIRST_LOGIN_TEST');

SELECT 'Utilisateur de test supprimé !' AS resultat;
*/

-- ================================================================================
-- RÉSUMÉ DES TESTS
-- ================================================================================

SELECT '=== RÉSUMÉ DES TESTS ===' AS '';
SELECT '✓ Création utilisateur de test' AS '';
SELECT '✓ Simulation verrouillage de compte' AS '';
SELECT '✓ Simulation réinitialisation par admin' AS '';
SELECT '✓ Simulation changement mot de passe utilisateur' AS '';
SELECT '✓ Vérification des logs d''audit' AS '';
SELECT '' AS '';
SELECT 'TOUS LES TESTS SONT TERMINÉS !' AS '';
SELECT '' AS '';
SELECT 'Pour tester manuellement dans l''application :' AS '';
SELECT '1. Lancez l''application' AS '';
SELECT '2. Connectez-vous avec admin / Admin@123' AS '';
SELECT '3. Allez dans Système > Gestion des utilisateurs' AS '';
SELECT '4. Trouvez l''utilisateur "test_reset"' AS '';
SELECT '5. Testez les boutons Réinitialiser / Déverrouiller' AS '';
SELECT '6. Déconnectez-vous et testez la connexion avec test_reset / RHPlus2026!' AS '';
SELECT '7. Vérifiez que le formulaire de changement obligatoire s''affiche' AS '';

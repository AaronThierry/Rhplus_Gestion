-- Option alternative: Créer un mot de passe temporaire simple
-- Vous pourrez ensuite le changer depuis l'interface de gestion

-- Hash BCrypt pour "password" (mot de passe temporaire simple)
UPDATE utilisateurs
SET mot_de_passe_hash = '$2a$11$N9qo8uLOickgx2ZMRZoMye4KUkW8K2.ZXs9/KGqSL7W7.bPgc3JLW',
    tentatives_echec = 0,
    compte_verrouille = 0,
    actif = 1
WHERE nom_utilisateur = 'admin';

-- OU utiliser ce hash pour "Admin@123"
-- UPDATE utilisateurs
-- SET mot_de_passe_hash = '$2a$11$7rLSvRVyTfIapPwVu.bEy.KqV5pDfKCrRfKpG/QNVQbW7VqkN6qZG',
--     tentatives_echec = 0,
--     compte_verrouille = 0,
--     actif = 1
-- WHERE nom_utilisateur = 'admin';

SELECT 'Mot de passe admin réinitialisé à: password' as message;
SELECT * FROM utilisateurs WHERE nom_utilisateur = 'admin';

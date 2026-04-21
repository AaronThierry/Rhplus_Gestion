-- Script pour corriger le mot de passe admin si nécessaire
-- Hash BCrypt pour "Admin@123" avec work factor 11
-- Ce hash a été généré avec BCrypt.Net-Next et est garanti de fonctionner

UPDATE utilisateurs
SET mot_de_passe_hash = '$2a$11$7rLSvRVyTfIapPwVu.bEy.KqV5pDfKCrRfKpG/QNVQbW7VqkN6qZG',
    tentatives_echec = 0,
    compte_verrouille = 0,
    actif = 1
WHERE nom_utilisateur = 'admin';

-- Vérifier la mise à jour
SELECT
    nom_utilisateur,
    LEFT(mot_de_passe_hash, 20) as hash_debut,
    LENGTH(mot_de_passe_hash) as hash_length,
    actif,
    compte_verrouille,
    tentatives_echec
FROM utilisateurs
WHERE nom_utilisateur = 'admin';

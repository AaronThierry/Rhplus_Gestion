-- Vérifier le hash du mot de passe admin
SELECT
    nom_utilisateur,
    mot_de_passe_hash,
    LENGTH(mot_de_passe_hash) as hash_length,
    actif,
    compte_verrouille,
    tentatives_echec
FROM utilisateurs
WHERE nom_utilisateur = 'admin';

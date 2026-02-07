-- Script pour rendre le sursalaire unique par employé
-- Un employé ne peut avoir qu'un seul sursalaire

-- Supprimer d'abord les doublons éventuels (garde le plus récent par employé)
DELETE s1 FROM sursalaire s1
INNER JOIN sursalaire s2
WHERE s1.id_personnel = s2.id_personnel
  AND s1.id_sursalaire < s2.id_sursalaire;

-- Ajouter la contrainte UNIQUE sur id_personnel
ALTER TABLE `sursalaire`
ADD UNIQUE INDEX `idx_unique_personnel` (`id_personnel`);

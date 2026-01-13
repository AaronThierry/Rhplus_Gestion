# 🔒 Documentation Système QR Code Sécurisé - Bulletins de Paie

## Vue d'ensemble

Le système de QR code implémenté dans les bulletins de paie RH_GMP offre une solution de sécurisation et de vérification de pointe, combinant haute qualité visuelle et sécurité cryptographique robuste.

---

## 📊 Caractéristiques Techniques

### 1. Qualité Visuelle Premium

| Paramètre | Valeur | Description |
|-----------|--------|-------------|
| **Résolution** | 20 pixels/module | Haute définition pour impression et scan |
| **Correction d'erreur** | Niveau H (30%) | QR code lisible même avec 30% de dommages |
| **Quiet Zone** | 4 modules | Zone de silence obligatoire pour détection |
| **Contraste** | Noir pur / Blanc pur | RGBA: [0,0,0,255] / [255,255,255,255] |
| **Bordure** | Double (3px + 1.5px) | Effet de profondeur premium |
| **Taille affichage** | 85x85 points | Taille optimale pour visibilité et scan |

### 2. Structure du Payload JSON (v2.0)

```json
{
  "v": "2.0",                    // Version du format QR
  "id": "ABC123DEF456...",       // GUID unique (32 caractères)
  "type": "BULLETIN_PAIE",       // Type de document
  "app": "RH_GMP_v1.0",         // Version de l'application
  "emis": "2026-01-11T14:30:00Z", // Timestamp UTC ISO-8601

  "data": {
    "entreprise": "NOM_ENTREPRISE",
    "employe": "NOM_PRENOM",
    "matricule": "MAT001",
    "poste": "DEVELOPPEUR",
    "periode": "JANVIER 2026"
  },

  "finance": {
    "brut": 500000.00,           // Salaire brut (XOF)
    "net": 450000.00,            // Salaire net
    "dette": 0.00,               // Remboursement dette
    "netFinal": 450000.00,       // Net à payer final
    "devise": "XOF"              // Franc CFA
  },

  "security": {
    "signature": "A1B2C3D4E5F6...", // HMAC-SHA256 (64 caractères hex)
    "checksum": "12345678",         // CRC32 (8 caractères hex)
    "algo": "HMAC-SHA256"           // Algorithme de signature
  }
}
```

---

## 🔐 Sécurité Multi-Niveaux

### Niveau 1 : Identifiant Unique (Anti-Rejeu)
- **Technologie**: GUID (Globally Unique Identifier)
- **Format**: 32 caractères hexadécimaux (sans tirets)
- **Objectif**: Empêcher la duplication et le rejeu de bulletins
- **Affichage**: Les 10 premiers caractères sont affichés sur le bulletin

### Niveau 2 : Checksum CRC32 (Vérification Rapide)
- **Technologie**: Cyclic Redundancy Check 32 bits
- **Format**: 8 caractères hexadécimaux
- **Objectif**: Détection rapide de corruption ou modification
- **Performance**: Calcul instantané, idéal pour vérification mobile

### Niveau 3 : Signature HMAC-SHA256 (Authentification)
- **Technologie**: Hash-based Message Authentication Code avec SHA-256
- **Format**: 64 caractères hexadécimaux
- **Clé secrète**: 256 bits (à changer en production)
- **Objectif**: Garantir l'authenticité et l'intégrité du bulletin
- **Avantage**: Impossible à forger sans la clé secrète

---

## ✅ Processus de Vérification

### Vérification Manuelle (Offline)

#### Étape 1 : Extraction des données
```
1. Scanner le QR code avec une application QR moderne
2. Extraire le JSON du payload
3. Parser le JSON pour obtenir les champs
```

#### Étape 2 : Vérification CRC32 (Rapide)
```csharp
// Reconstruire la chaîne canonique
string canonical = $"{id}|{type}|{version}|{entreprise}|{employe}|{matricule}|" +
                   $"{poste}|{periode}|{brut}|{net}|{dette}|{netFinal}|" +
                   $"{devise}|{timestamp}|{appVersion}";

// Calculer le CRC32
string calculatedChecksum = ComputeCRC32(canonical);

// Comparer
if (calculatedChecksum == checksum)
    Console.WriteLine("✓ Checksum valide");
```

#### Étape 3 : Vérification HMAC-SHA256 (Sécurisée)
```csharp
// Utiliser la même clé secrète que lors de la génération
const string SECRET_KEY = "RH_GMP_BULLETIN_SECRET_KEY_2026_CHANGE_IN_PROD";

// Calculer la signature
string calculatedSignature = ComputeHMAC_SHA256(canonical, SECRET_KEY);

// Comparer
if (calculatedSignature.ToUpper() == signature.ToUpper())
    Console.WriteLine("✓ Bulletin authentique et non modifié");
else
    Console.WriteLine("✗ ATTENTION: Bulletin falsifié ou corrompu!");
```

---

## 📱 Application Mobile de Vérification (Recommandation)

### Fonctionnalités suggérées

1. **Scanner le QR code**
   - Utiliser la caméra du smartphone
   - Détecter et lire le QR code automatiquement

2. **Vérification en temps réel**
   - Vérifier le checksum CRC32 (offline)
   - Afficher les données du bulletin
   - Vérifier la signature HMAC (nécessite la clé secrète)

3. **Affichage des résultats**
   ```
   ✓ Checksum: VALIDE
   ✓ Signature: AUTHENTIQUE
   ✓ Bulletin: CONFORME

   Entreprise: ABC Corp
   Employé: Jean DUPONT
   Matricule: EMP001
   Période: Janvier 2026
   Net à payer: 450,000 XOF

   ID: ABC123DEF4
   Émis le: 11/01/2026 14:30 UTC
   ```

4. **Historique et logs**
   - Conserver l'historique des vérifications
   - Logger date/heure/résultat pour audit

---

## 🎨 Design Visuel

### Composition du QR code sur le bulletin

```
┌──────────────────────────────────┐
│  ╔════════════════════════════╗  │ ← Bordure externe bleue (3px)
│  ║ ┌────────────────────────┐ ║  │
│  ║ │  ▀▀▀▀▀▀  ▀▀  ▀▀▀▀▀▀   │ ║  │ ← QR Code haute résolution
│  ║ │  ▀  ▀▀  ▀▀▀▀  ▀▀  ▀   │ ║  │   20 pixels/module
│  ║ │  ▀▀▀▀▀▀  ▀▀  ▀▀▀▀▀▀   │ ║  │
│  ║ └────────────────────────┘ ║  │ ← Bordure interne grise (1.5px)
│  ╚════════════════════════════╝  │
│                                  │
│     ┌──────────────────┐         │
│     │  🔒 SÉCURISÉ    │         │ ← Badge bleu
│     └──────────────────┘         │
│                                  │
│     ID: ABC123DEF4               │ ← ID unique (10 chars)
│     CRC: 12345678                │ ← Checksum CRC32
│     v2.0                         │ ← Version
└──────────────────────────────────┘
```

### Éléments visuels
- **Badge "🔒 SÉCURISÉ"**: Fond bleu foncé, texte blanc, gras
- **ID unique**: Police Courier New, bleu foncé
- **Checksum**: Police Courier New, gris foncé
- **Version**: Police Montserrat, gris, italique

---

## ⚠️ Recommandations de Sécurité

### 1. Gestion de la clé secrète
```csharp
// ❌ MAUVAIS: Clé en dur dans le code
const string SECRET_KEY = "ma_cle_secrete";

// ✅ BON: Clé stockée dans variable d'environnement
string secretKey = Environment.GetEnvironmentVariable("BULLETIN_SECRET_KEY");

// ✅ MEILLEUR: Clé stockée dans Azure Key Vault / AWS Secrets Manager
string secretKey = await GetSecretFromVault("bulletin-hmac-key");
```

### 2. Rotation des clés
- Changer la clé secrète tous les 6-12 mois
- Conserver l'ancienne clé pour vérifier les anciens bulletins
- Implémenter un système de versioning des clés

### 3. Audit et logging
```csharp
// Logger toutes les vérifications
Logger.Info($"QR verification: Bulletin {bulletinId} - Result: {isValid}");
Logger.Info($"Verified by: {userName} - IP: {ipAddress} - Time: {timestamp}");
```

### 4. Protection contre les attaques

| Type d'attaque | Protection | Implémentation |
|----------------|------------|----------------|
| **Falsification** | HMAC-SHA256 | Signature cryptographique |
| **Rejeu** | GUID unique | Chaque bulletin a un ID différent |
| **Corruption** | CRC32 + ECC Level H | Détection et récupération |
| **Force brute** | Clé 256 bits | 2^256 possibilités |

---

## 📈 Optimisations

### Compression du JSON
Le système compresse automatiquement le JSON en supprimant:
- Les retours à la ligne (`\n`, `\r`)
- Les espaces multiples
- Les espaces inutiles

**Avant compression** (255 caractères):
```json
{
  "v": "2.0",
  "id": "ABC123",
  "type": "BULLETIN_PAIE"
}
```

**Après compression** (52 caractères):
```json
{"v":"2.0","id":"ABC123","type":"BULLETIN_PAIE"}
```

**Réduction**: ~80% pour un QR code plus petit et plus rapide à scanner.

---

## 🔧 Maintenance

### Tests recommandés

1. **Test de scan**
   - Scanner avec 5+ applications différentes
   - Tester sur iOS et Android
   - Vérifier la vitesse de détection

2. **Test de résistance**
   - Imprimer et plier légèrement
   - Tester avec 10-20-30% de dégradation
   - Vérifier la récupération grâce au niveau H

3. **Test de sécurité**
   - Modifier manuellement un caractère dans le JSON
   - Vérifier que la signature échoue
   - Tester avec une fausse clé secrète

### Monitoring

```csharp
// Métriques à surveiller
- Taux de scan réussi
- Temps moyen de vérification
- Nombre de tentatives de falsification détectées
- Distribution des versions de QR code
```

---

## 📝 Changelog

### Version 2.0 (Actuelle)
- ✅ Résolution augmentée à 20 pixels/module
- ✅ Ajout checksum CRC32
- ✅ Signature HMAC-SHA256
- ✅ Payload JSON structuré
- ✅ Métadonnées enrichies (poste, dette, version)
- ✅ Design premium avec bordure double
- ✅ Badge de sécurité visible
- ✅ Affichage ID et CRC sur le bulletin

### Version 1.0 (Précédente)
- QR code basique avec SHA256
- Résolution standard (8 pixels/module)
- Payload texte simple (pipe-delimited)

---

## 🤝 Support

Pour toute question ou amélioration:
1. Consulter cette documentation
2. Vérifier les logs d'erreur
3. Contacter l'équipe de développement RH_GMP

---

**Dernière mise à jour**: 11 janvier 2026
**Version du système**: 2.0
**Auteur**: Équipe RH_GMP

---

## 📚 Références

- [RFC 2104 - HMAC](https://tools.ietf.org/html/rfc2104)
- [QR Code Error Correction](https://www.qrcode.com/en/about/error_correction.html)
- [ISO/IEC 18004:2015 - QR Code](https://www.iso.org/standard/62021.html)
- [CRC32 Algorithm](https://en.wikipedia.org/wiki/Cyclic_redundancy_check)

# 📱 Guide de Vérification QR Code - Bulletin de Paie

## 🎯 Guide Rapide pour Employés

### Étape 1 : Scanner le QR Code

```
┌─────────────────────────────────────┐
│  📱 Ouvrez l'application caméra     │
│  ou une application QR code         │
│                                     │
│  Applications recommandées:         │
│  • iOS: Caméra native               │
│  • Android: Google Lens             │
│  • Universel: QR Code Reader        │
└─────────────────────────────────────┘
```

### Étape 2 : Lire les Informations

Après le scan, vous verrez un JSON similaire à :

```json
{
  "v": "2.0",
  "id": "A1B2C3D4E5F6...",
  "type": "BULLETIN_PAIE",
  "app": "RH_GMP_v1.0",
  "emis": "2026-01-11T14:30:00Z",

  "data": {
    "entreprise": "VOTRE_ENTREPRISE",
    "employe": "VOTRE_NOM",
    "matricule": "VOTRE_MATRICULE",
    "poste": "VOTRE_POSTE",
    "periode": "JANVIER 2026"
  },

  "finance": {
    "brut": 500000.00,
    "net": 450000.00,
    "dette": 0.00,
    "netFinal": 450000.00,
    "devise": "XOF"
  },

  "security": {
    "signature": "A1B2C3D4E5F6...",
    "checksum": "12345678",
    "algo": "HMAC-SHA256"
  }
}
```

### Étape 3 : Vérifier les Informations Clés

✅ **Vérifiez que les données correspondent** :

| Champ | Vérification |
|-------|--------------|
| **employe** | Votre nom complet |
| **matricule** | Votre matricule |
| **periode** | Le mois concerné |
| **brut** | Salaire brut affiché sur le bulletin |
| **net** | Salaire net affiché sur le bulletin |
| **netFinal** | Net à payer (après déduction dette) |

### Étape 4 : Vérifier les Indicateurs de Sécurité

Sur le bulletin, vous verrez :

```
┌──────────────────────────────┐
│  ╔════════════════════════╗  │
│  ║  [QR CODE IMAGE]       ║  │
│  ╚════════════════════════╝  │
│                              │
│     ┌────────────────┐       │
│     │  🔒 SÉCURISÉ   │       │ ← Badge de sécurité
│     └────────────────┘       │
│                              │
│     ID: A1B2C3D4E5            │ ← Identifiant unique
│     CRC: 12345678             │ ← Checksum de vérification
│     v2.0                      │ ← Version du QR
└──────────────────────────────┘
```

**Indicateurs de confiance** :
- ✅ Badge "🔒 SÉCURISÉ" présent
- ✅ ID unique affiché (10 caractères)
- ✅ CRC checksum visible (8 caractères)
- ✅ Version "v2.0" indiquée

---

## 🔐 Guide de Vérification Technique (RH/IT)

### Méthode 1 : Vérification CRC32 (Rapide - Offline)

**Temps**: ~1 seconde | **Sécurité**: Moyenne | **Complexité**: Faible

```csharp
// 1. Extraire le JSON du QR code
string json = ScanQRCode();
var data = JsonConvert.DeserializeObject<BulletinQR>(json);

// 2. Reconstruire la chaîne canonique
string canonical = $"{data.id}|{data.type}|{data.v}|" +
                   $"{data.data.entreprise}|{data.data.employe}|{data.data.matricule}|" +
                   $"{data.data.poste}|{data.data.periode}|" +
                   $"{data.finance.brut}|{data.finance.net}|" +
                   $"{data.finance.dette}|{data.finance.netFinal}|" +
                   $"{data.finance.devise}|{data.emis}|{data.app}";

// 3. Calculer le CRC32
string calculatedCRC = ComputeCRC32(canonical);

// 4. Comparer
if (calculatedCRC == data.security.checksum)
{
    Console.WriteLine("✅ CRC32 VALIDE - Données non corrompues");
}
else
{
    Console.WriteLine("❌ CRC32 INVALIDE - Données corrompues ou modifiées!");
}
```

**Utilisation** :
- Vérification rapide en mobilité
- Première étape de validation
- Détection de corruption accidentelle

---

### Méthode 2 : Vérification HMAC-SHA256 (Complète - Sécurisée)

**Temps**: ~2 secondes | **Sécurité**: Maximum | **Complexité**: Moyenne

```csharp
// 1. Extraire et parser le JSON (même que méthode 1)
string json = ScanQRCode();
var data = JsonConvert.DeserializeObject<BulletinQR>(json);

// 2. Reconstruire la chaîne canonique (même que méthode 1)
string canonical = $"{data.id}|{data.type}|{data.v}|" +
                   $"{data.data.entreprise}|{data.data.employe}|{data.data.matricule}|" +
                   $"{data.data.poste}|{data.data.periode}|" +
                   $"{data.finance.brut}|{data.finance.net}|" +
                   $"{data.finance.dette}|{data.finance.netFinal}|" +
                   $"{data.finance.devise}|{data.emis}|{data.app}";

// 3. Récupérer la clé secrète (SÉCURISÉ!)
string secretKey = Environment.GetEnvironmentVariable("BULLETIN_SECRET_KEY");
// ⚠️ NE JAMAIS mettre la clé en dur dans le code!

// 4. Calculer HMAC-SHA256
string calculatedHMAC;
using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secretKey)))
{
    var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(canonical));
    var sb = new StringBuilder(hash.Length * 2);
    foreach (var b in hash) sb.Append(b.ToString("x2"));
    calculatedHMAC = sb.ToString().ToUpperInvariant();
}

// 5. Comparer avec la signature du QR
if (calculatedHMAC == data.security.signature.ToUpperInvariant())
{
    Console.WriteLine("✅ SIGNATURE VALIDE - Bulletin authentique!");
    Console.WriteLine($"   ID: {data.id}");
    Console.WriteLine($"   Employé: {data.data.employe}");
    Console.WriteLine($"   Net à payer: {data.finance.netFinal} {data.finance.devise}");
}
else
{
    Console.WriteLine("❌ SIGNATURE INVALIDE - BULLETIN FALSIFIÉ!");
    Console.WriteLine("   ⚠️ NE PAS ACCEPTER CE BULLETIN");
    Console.WriteLine("   ⚠️ SIGNALER À LA SÉCURITÉ");
}
```

**Utilisation** :
- Vérification officielle et définitive
- Preuve juridique d'authenticité
- Audit et conformité

---

## 📊 Matrice de Décision

### Quand utiliser quelle méthode ?

| Scénario | Méthode CRC32 | Méthode HMAC | Les Deux |
|----------|---------------|--------------|----------|
| **Vérification employé (quotidien)** | ✅ | ❌ | ❌ |
| **Audit interne (RH)** | ❌ | ✅ | ✅ |
| **Contrôle fiscal** | ❌ | ✅ | ✅ |
| **Litige juridique** | ❌ | ✅ | ✅ |
| **Vérification mobile rapide** | ✅ | ❌ | ❌ |
| **Système automatisé** | ❌ | ❌ | ✅ |

### Workflow Recommandé

```
┌─────────────────────┐
│  Scanner QR Code    │
└──────────┬──────────┘
           │
           ▼
┌─────────────────────┐
│  Vérifier CRC32     │◄──── Rapide (1 sec)
└──────────┬──────────┘
           │
      ✅ Valide?
           │
    ┌──────┴──────┐
    │             │
   OUI           NON
    │             │
    ▼             ▼
┌─────────────────────┐     ┌─────────────────────┐
│  Vérifier HMAC-256  │     │  ❌ REJET IMMÉDIAT  │
└──────────┬──────────┘     │  Bulletin corrompu  │
           │                 └─────────────────────┘
      ✅ Valide?
           │
    ┌──────┴──────┐
    │             │
   OUI           NON
    │             │
    ▼             ▼
┌─────────────────────┐     ┌─────────────────────┐
│  ✅ ACCEPTÉ         │     │  ❌ REJET TOTAL     │
│  Bulletin valide    │     │  Bulletin falsifié! │
│  Données conformes  │     │  ⚠️ ALERTE SÉCURITÉ │
└─────────────────────┘     └─────────────────────┘
```

---

## 🚨 Détection de Fraude

### Signes d'Alerte

#### ❌ CRC32 Invalide
**Signification** : Les données ont été modifiées ou corrompues

**Causes possibles** :
1. Modification manuelle du JSON
2. Corruption lors du scan
3. QR code endommagé physiquement

**Action** :
- Rescanner le QR code
- Si persiste → Vérifier l'état physique du bulletin
- Si bulletin intact → **FRAUDE PROBABLE**

#### ❌ HMAC Invalide (mais CRC valide)
**Signification** : Tentative de falsification sophistiquée

**Causes possibles** :
1. **FRAUDE AVÉRÉE** : Quelqu'un a modifié les données et recalculé le CRC
2. Clé secrète incorrecte (erreur de configuration)

**Action** :
- ⚠️ **ALERTE MAXIMALE**
- Isoler le bulletin
- Contacter la sécurité immédiatement
- Enquête sur la source du bulletin

#### ❌ ID déjà utilisé
**Signification** : Duplication de bulletin (rejeu)

**Causes possibles** :
1. **FRAUDE** : Réutilisation d'un bulletin valide
2. Erreur système (rare)

**Action** :
- Vérifier la base de données
- Comparer les deux bulletins
- Déterminer l'original vs le duplicata

---

## 📱 Application Mobile Recommandée

### Fonctionnalités Essentielles

```
┌────────────────────────────────────┐
│  RH GMP - Vérificateur Bulletin   │
│                                    │
│  ┌──────────────────────────────┐ │
│  │                              │ │
│  │    [CAMÉRA QR CODE]          │ │
│  │                              │ │
│  │    Pointez vers le QR        │ │
│  │                              │ │
│  └──────────────────────────────┘ │
│                                    │
│  Résultat:                         │
│  ┌──────────────────────────────┐ │
│  │ ✅ Bulletin AUTHENTIQUE      │ │
│  │                              │ │
│  │ Employé: Jean DUPONT         │ │
│  │ Matricule: EMP001            │ │
│  │ Période: Janvier 2026        │ │
│  │ Net à payer: 450,000 XOF     │ │
│  │                              │ │
│  │ ✅ CRC32: Valide             │ │
│  │ ✅ HMAC: Authentique         │ │
│  │                              │ │
│  │ ID: A1B2C3D4E5               │ │
│  │ Émis: 11/01/2026 14:30 UTC   │ │
│  └──────────────────────────────┘ │
│                                    │
│  [Historique] [Paramètres]        │
└────────────────────────────────────┘
```

### Captures d'écran Types

#### Scan Réussi ✅
```
╔════════════════════════════════════╗
║  ✅ BULLETIN AUTHENTIQUE           ║
╚════════════════════════════════════╝

Entreprise: ABC Corporation
Employé: Marie KOUASSI
Matricule: EMP042
Poste: Comptable Senior
Période: Janvier 2026

Salaire Brut:     500,000 XOF
Salaire Net:      450,000 XOF
Remb. Dette:            0 XOF
Net à Payer:      450,000 XOF

─────────────────────────────────────
Sécurité:
✅ Checksum CRC32:    VALIDE
✅ Signature HMAC:    AUTHENTIQUE
✅ ID unique:         Nouveau

ID Bulletin: A1B2C3D4E5F6
Émis le: 11/01/2026 à 14:30 UTC
Version QR: v2.0

[Enregistrer] [Partager] [Fermer]
```

#### Scan Échoué ❌
```
╔════════════════════════════════════╗
║  ❌ BULLETIN INVALIDE              ║
╚════════════════════════════════════╝

⚠️ ATTENTION: Ce bulletin n'est pas
   authentique ou a été modifié!

Détails de l'erreur:
─────────────────────────────────────
❌ Checksum CRC32:    INVALIDE
   Calculé:  12345678
   Attendu:  87654321

❌ Signature HMAC:    INVALIDE
   La signature ne correspond pas
   aux données du bulletin.

⚠️ NE PAS ACCEPTER CE BULLETIN
⚠️ SIGNALER À VOTRE RESPONSABLE RH

[Signaler] [Rescanner] [Fermer]
```

---

## 🎓 FAQ - Questions Fréquentes

### Q1 : Pourquoi mon QR code ne scanne pas ?

**Réponses possibles** :
1. **Éclairage insuffisant** : Utilisez un meilleur éclairage
2. **QR code endommagé** : Le système peut récupérer jusqu'à 30% de dégâts
3. **Application incompatible** : Utilisez une app QR moderne
4. **Caméra floue** : Nettoyez la lentille de votre smartphone

### Q2 : Le CRC est valide mais HMAC invalide, pourquoi ?

**Réponse** : C'est le signe d'une **tentative de fraude sophistiquée**. Quelqu'un a modifié les données ET recalculé le CRC, mais sans la clé secrète, impossible de recalculer le HMAC correct.

**Action** : **ALERTE SÉCURITÉ** - Ne pas accepter le bulletin.

### Q3 : Peut-on vérifier sans connexion Internet ?

**Réponse** : **OUI** ! Les deux vérifications (CRC32 et HMAC) peuvent se faire complètement offline si vous avez :
- La clé secrète (pour HMAC)
- Le code de vérification (disponible dans le projet)

### Q4 : Combien de temps un bulletin reste-t-il valide ?

**Réponse** : **Indéfiniment** du point de vue cryptographique. Cependant, votre politique RH peut définir une durée de validité (ex: 5 ans pour archivage).

### Q5 : Que faire si je détecte un bulletin frauduleux ?

**Procédure** :
1. ⚠️ **Ne pas confronter** la personne immédiatement
2. 📸 Photographier le bulletin (avec le QR visible)
3. 📝 Noter les circonstances (date, heure, personne)
4. 🔒 Conserver les preuves en sécurité
5. 📞 Contacter immédiatement :
   - Votre responsable RH
   - Le service de sécurité
   - Si nécessaire, les autorités

### Q6 : Le système peut-il détecter les photocopies ?

**Réponse** : **OUI et NON**
- ✅ Le QR code fonctionnera sur une photocopie de qualité
- ❌ Mais l'ID unique permettra de détecter que c'est une copie si l'original existe déjà dans la base

**Conseil** : Toujours vérifier l'ID dans la base de données pour détecter les duplicatas.

---

## 📞 Support et Contact

### En cas de problème technique

**Email** : support.rh@votre-entreprise.com
**Téléphone** : +226 XX XX XX XX
**Heures** : Lundi-Vendredi, 8h-17h

### En cas de fraude détectée

**URGENT** : security@votre-entreprise.com
**Téléphone** : +226 XX XX XX XX (24/7)

---

**Document créé le**: 11 janvier 2026
**Version**: 1.0
**Auteur**: Équipe RH_GMP

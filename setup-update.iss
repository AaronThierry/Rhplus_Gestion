; Script d'installation Inno Setup pour Gestion Moderne RH v1.0.5 - UPDATE
; Mise à jour CRITIQUE pour corriger le calcul IUTS des employés cadres

#define MyAppName "Gestion Moderne RH"
#define MyAppVersion "1.0.5"
#define MyAppPublisher "GMP - Gestion Moderne de Paie"
#define MyAppURL "https://github.com/AaronThierry/Rhplus_Gestion"
#define MyAppExeName "RH_GRH.exe"
#define MyAppAssocName MyAppName + " File"
#define MyAppAssocExt ".rhplus"
#define MyAppAssocKey StringChange(MyAppAssocName, " ", "") + MyAppAssocExt

[Setup]
; Identifiants de l'application
AppId={{B3E8C9D1-5A2F-4E3B-9C7D-1F8E6A4B2C5D}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}/issues
AppUpdatesURL={#MyAppURL}/releases
DefaultDirName={autopf}\{#MyAppName}
DisableProgramGroupPage=yes
LicenseFile=LICENSE.txt
InfoBeforeFile=UPDATE_NOTES_v1.0.5.txt
OutputDir=Setup\Output
OutputBaseFilename=GestionModerneRH_v{#MyAppVersion}_Update
SetupIconFile=RH_GRH\logo-rh-modified.ico
Compression=lzma2/ultra64
SolidCompression=yes
WizardStyle=modern
; Application compilée en x86 32-bit, compatible avec Windows 32-bit et 64-bit
ArchitecturesAllowed=x86 x64compatible
PrivilegesRequired=admin
UninstallDisplayIcon={app}\{#MyAppExeName}
UninstallDisplayName={#MyAppName}
VersionInfoVersion={#MyAppVersion}
VersionInfoCompany={#MyAppPublisher}
VersionInfoDescription=Système de Gestion des Ressources Humaines et Paie - Mise à jour v1.0.5
VersionInfoCopyright=Copyright (C) 2025 {#MyAppPublisher}
VersionInfoProductName={#MyAppName}
VersionInfoProductVersion={#MyAppVersion}

[Languages]
Name: "french"; MessagesFile: "compiler:Languages\French.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"
Name: "quicklaunchicon"; Description: "{cm:CreateQuickLaunchIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked; OnlyBelowVersion: 6.1; Check: not IsAdminInstallMode

[Files]
; Application principale
Source: "RH_GRH\bin\Release\{#MyAppExeName}"; DestDir: "{app}"; Flags: ignoreversion
Source: "RH_GRH\bin\Release\*.dll"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs
Source: "RH_GRH\bin\Release\*.config"; DestDir: "{app}"; Flags: ignoreversion

; Dépendances natives - copier dans les sous-dossiers ET à la racine pour compatibilité
Source: "RH_GRH\bin\Release\runtimes\*"; DestDir: "{app}\runtimes"; Flags: ignoreversion recursesubdirs createallsubdirs

; Copier les DLLs natives x86 à la racine pour éviter les erreurs de chargement
Source: "RH_GRH\bin\Release\runtimes\win-x86\native\*.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "RH_GRH\bin\Release\x86\*.dll"; DestDir: "{app}"; Flags: ignoreversion

; Note: Application compilée en x86 32-bit, compatible avec systèmes 32 et 64 bits

; Documentation
Source: "README.md"; DestDir: "{app}"; Flags: ignoreversion
Source: "UPDATE_NOTES_v1.0.5.txt"; DestDir: "{app}"; Flags: ignoreversion

; Scripts SQL (optionnels)
Source: "verify_column.sql"; DestDir: "{app}\Database"; Flags: ignoreversion; Check: FileExists(ExpandConstant('{#SourcePath}\verify_column.sql'))

[Icons]
Name: "{autoprograms}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon
Name: "{userappdata}\Microsoft\Internet Explorer\Quick Launch\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: quicklaunchicon

[Run]
; Lancer l'application
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: postinstall nowait skipifsilent

[Registry]
Root: HKA; Subkey: "Software\Classes\{#MyAppAssocExt}\OpenWithProgids"; ValueType: string; ValueName: "{#MyAppAssocKey}"; ValueData: ""; Flags: uninsdeletevalue
Root: HKA; Subkey: "Software\Classes\{#MyAppAssocKey}"; ValueType: string; ValueName: ""; ValueData: "{#MyAppAssocName}"; Flags: uninsdeletekey
Root: HKA; Subkey: "Software\Classes\{#MyAppAssocKey}\DefaultIcon"; ValueType: string; ValueName: ""; ValueData: "{app}\{#MyAppExeName},0"
Root: HKA; Subkey: "Software\Classes\{#MyAppAssocKey}\shell\open\command"; ValueType: string; ValueName: ""; ValueData: """{app}\{#MyAppExeName}"" ""%1"""
Root: HKA; Subkey: "Software\Classes\Applications\{#MyAppExeName}\SupportedTypes"; ValueType: string; ValueName: ".myp"; ValueData: ""

[Code]
var
  DotNetInstallNeeded: Boolean;

const
  DOTNET_URL = 'https://dotnet.microsoft.com/download/dotnet-framework/thank-you/net472-web-installer';

// Vérifier si .NET Framework 4.7.2 est installé
function IsDotNet472Installed: Boolean;
var
  Release: Cardinal;
begin
  Result := False;
  if RegQueryDWordValue(HKLM, 'SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full', 'Release', Release) then
  begin
    // .NET Framework 4.7.2 = Release >= 461808
    Result := Release >= 461808;
  end;
end;

function InitializeSetup(): Boolean;
var
  ErrorCode: Integer;
  ResultMsg: Integer;
begin
  Result := True;
  DotNetInstallNeeded := False;

  // Vérifier .NET Framework 4.7.2
  if not IsDotNet472Installed then
  begin
    ResultMsg := MsgBox('Cette application nécessite Microsoft .NET Framework 4.7.2 ou supérieur.' + #13#10 + #13#10 +
                       'Voulez-vous télécharger et installer .NET Framework maintenant?',
                       mbConfirmation, MB_YESNO);
    if ResultMsg = IDYES then
    begin
      ShellExec('open', DOTNET_URL, '', '', SW_SHOWNORMAL, ewNoWait, ErrorCode);
      Result := False;
      MsgBox('Veuillez installer .NET Framework 4.7.2 et relancer ce programme d''installation.',
             mbInformation, MB_OK);
    end
    else
    begin
      Result := False;
      MsgBox('L''installation ne peut pas continuer sans .NET Framework 4.7.2.',
             mbError, MB_OK);
    end;
  end;
end;

procedure InitializeWizard();
begin
  // Wizard initialization
end;

function ShouldSkipPage(PageID: Integer): Boolean;
begin
  Result := False;
end;

procedure CurStepChanged(CurStep: TSetupStep);
var
  ConfigFile: string;
  FileContent: TStringList;
begin
  if CurStep = ssPostInstall then
  begin
    // Créer un fichier de configuration par défaut
    ConfigFile := ExpandConstant('{app}\database_config.txt');
    FileContent := TStringList.Create;
    try
      FileContent.Add('Configuration de la base de données');
      FileContent.Add('=====================================');
      FileContent.Add('');
      FileContent.Add('Veuillez configurer votre connexion MySQL dans l''application.');
      FileContent.Add('');
      FileContent.Add('Informations requises:');
      FileContent.Add('- Serveur: localhost ou adresse IP du serveur MySQL');
      FileContent.Add('- Port: 3306 (par défaut)');
      FileContent.Add('- Base de données: nom de votre base RH');
      FileContent.Add('- Utilisateur: utilisateur MySQL avec les droits appropriés');
      FileContent.Add('- Mot de passe: mot de passe de l''utilisateur');
      FileContent.Add('');
      FileContent.Add('Scripts SQL disponibles dans le dossier Database/');
      FileContent.SaveToFile(ConfigFile);
    finally
      FileContent.Free;
    end;
  end;
end;

function UpdateReadyMemo(Space, NewLine, MemoUserInfoInfo, MemoDirInfo, MemoTypeInfo, MemoComponentsInfo, MemoGroupInfo, MemoTasksInfo: String): String;
var
  S: String;
begin
  S := '';
  S := S + 'MISE À JOUR v1.0.5 - CORRECTION CRITIQUE' + NewLine + NewLine;
  S := S + 'CORRECTION MAJEURE :' + NewLine;
  S := S + Space + '- FIX CRITIQUE : Calcul IUTS pour les CADRES' + NewLine;
  S := S + Space + '- Tous les cadres étaient traités comme non-cadres' + NewLine;
  S := S + Space + '- Abattement corrigé : 20% pour cadres (au lieu de 25%)' + NewLine;
  S := S + Space + '- Impact : Salaire net des CADRES augmenté' + NewLine + NewLine;
  S := S + 'INCLUS AUSSI (v1.0.4) :' + NewLine;
  S := S + Space + '- Affichage du Net à Payer sur les bulletins' + NewLine;
  S := S + Space + '- Récupération des employés CDD par lot' + NewLine;
  S := S + Space + '- Arrondi standardisé 1 FCFA (tous calculs)' + NewLine + NewLine;

  S := S + MemoDirInfo + NewLine + NewLine;

  if MemoTasksInfo <> '' then
    S := S + MemoTasksInfo + NewLine + NewLine;

  S := S + 'Configuration requise:' + NewLine;
  S := S + Space + '- .NET Framework 4.7.2 ou supérieur' + NewLine;
  S := S + Space + '- MySQL Server 5.7 ou supérieur' + NewLine;
  S := S + Space + '- Windows 7 SP1 / Windows Server 2008 R2 SP1 ou supérieur' + NewLine;

  Result := S;
end;

procedure CurUninstallStepChanged(CurUninstallStep: TUninstallStep);
var
  ResultMsg: Integer;
begin
  if CurUninstallStep = usUninstall then
  begin
    ResultMsg := MsgBox('Voulez-vous conserver les fichiers de configuration et les données?',
                       mbConfirmation, MB_YESNO);
    if ResultMsg = IDNO then
    begin
      // Supprimer les fichiers de configuration
      DeleteFile(ExpandConstant('{app}\database_config.txt'));
    end;
  end;
end;

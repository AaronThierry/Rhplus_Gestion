; Script d'installation Inno Setup pour RH Plus Gestion v1.0
; Installation professionnelle avec vérification des prérequis

#define MyAppName "RH Plus Gestion"
#define MyAppVersion "1.0.0"
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
InfoBeforeFile=INSTALLATION_INFO.txt
InfoAfterFile=POST_INSTALL_INFO.txt
OutputDir=Setup\Output
OutputBaseFilename=RHPlusGestion_v{#MyAppVersion}_Setup
SetupIconFile=RH_GRH\logo-rh-modified.ico
Compression=lzma2/ultra64
SolidCompression=yes
WizardStyle=modern
ArchitecturesInstallIn64BitMode=x64
PrivilegesRequired=admin
UninstallDisplayIcon={app}\{#MyAppExeName}
UninstallDisplayName={#MyAppName}
VersionInfoVersion={#MyAppVersion}
VersionInfoCompany={#MyAppPublisher}
VersionInfoDescription=Système de Gestion des Ressources Humaines et Paie
VersionInfoCopyright=Copyright (C) 2025 {#MyAppPublisher}
VersionInfoProductName={#MyAppName}
VersionInfoProductVersion={#MyAppVersion}

; Styles visuels modernes
WizardImageFile=Setup\Assets\WizardImage.bmp
WizardSmallImageFile=Setup\Assets\WizardSmallImage.bmp

[Languages]
Name: "french"; MessagesFile: "compiler:Languages\French.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked
Name: "quicklaunchicon"; Description: "{cm:CreateQuickLaunchIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked; OnlyBelowVersion: 6.1; Check: not IsAdminInstallMode

[Files]
; Application principale
Source: "RH_GRH\bin\Release\{#MyAppExeName}"; DestDir: "{app}"; Flags: ignoreversion
Source: "RH_GRH\bin\Release\*.dll"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs
Source: "RH_GRH\bin\Release\*.config"; DestDir: "{app}"; Flags: ignoreversion

; Ressources et dépendances
Source: "RH_GRH\bin\Release\Resources\*"; DestDir: "{app}\Resources"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "RH_GRH\bin\Release\Reports\*"; DestDir: "{app}\Reports"; Flags: ignoreversion recursesubdirs createallsubdirs

; Dépendances natives SkiaSharp
Source: "RH_GRH\bin\Release\runtimes\*"; DestDir: "{app}\runtimes"; Flags: ignoreversion recursesubdirs createallsubdirs

; Documentation
Source: "README.md"; DestDir: "{app}"; Flags: ignoreversion isreadme
Source: "MANUEL_UTILISATEUR.pdf"; DestDir: "{app}"; Flags: ignoreversion; Check: FileExists(ExpandConstant('{#SourcePath}\MANUEL_UTILISATEUR.pdf'))
Source: "GUIDE_INSTALLATION.pdf"; DestDir: "{app}"; Flags: ignoreversion; Check: FileExists(ExpandConstant('{#SourcePath}\GUIDE_INSTALLATION.pdf'))

; Scripts SQL
Source: "Database\*.sql"; DestDir: "{app}\Database"; Flags: ignoreversion recursesubdirs createallsubdirs; Check: DirExists(ExpandConstant('{#SourcePath}\Database'))
Source: "verify_column.sql"; DestDir: "{app}\Database"; Flags: ignoreversion

[Icons]
Name: "{autoprograms}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon
Name: "{userappdata}\Microsoft\Internet Explorer\Quick Launch\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: quicklaunchicon

[Run]
; Ouvrir le guide après installation
Filename: "{app}\GUIDE_INSTALLATION.pdf"; Description: "Ouvrir le guide d'installation"; Flags: postinstall shellexec skipifsilent nowait; Check: FileExists(ExpandConstant('{app}\GUIDE_INSTALLATION.pdf'))
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
  MySQLConfigPage: TInputFileWizardPage;

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

// Vérifier si MySQL Connector est disponible
function IsMySQLConnectorAvailable: Boolean;
begin
  Result := FileExists(ExpandConstant('{app}\MySql.Data.dll')) or
            FileExists(ExpandConstant('{app}\MySqlConnector.dll'));
end;

// Page de configuration de la connexion MySQL
procedure CreateMySQLConfigPage;
begin
  MySQLConfigPage := CreateInputFilePage(wpSelectDir,
    'Configuration de la base de données',
    'Veuillez fournir les informations de connexion MySQL',
    'Ces informations seront utilisées pour configurer la connexion à la base de données.');

  MySQLConfigPage.Add('Serveur MySQL:', 'Serveur MySQL|*.txt', '.txt');
  MySQLConfigPage.Add('Port:', 'Port|*.txt', '.txt');
  MySQLConfigPage.Add('Nom de la base:', 'Base de données|*.txt', '.txt');
  MySQLConfigPage.Add('Utilisateur:', 'Utilisateur|*.txt', '.txt');
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
  // Créer la page de configuration MySQL
  CreateMySQLConfigPage;
end;

function ShouldSkipPage(PageID: Integer): Boolean;
begin
  Result := False;
end;

procedure CurStepChanged(CurStep: TSetupStep);
var
  ConfigFile: string;
  FileContent: TStringList;
  ServerInfo, Port, Database, Username: string;
begin
  if CurStep = ssPostInstall then
  begin
    // Note: La configuration MySQL devrait être faite via l'application
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
  S := S + MemoDirInfo + NewLine + NewLine;

  if MemoTasksInfo <> '' then
    S := S + MemoTasksInfo + NewLine + NewLine;

  S := S + 'Configuration requise:' + NewLine;
  S := S + Space + '- .NET Framework 4.7.2 ou supérieur' + NewLine;
  S := S + Space + '- MySQL Server 5.7 ou supérieur' + NewLine;
  S := S + Space + '- Windows 7 SP1 / Windows Server 2008 R2 SP1 ou supérieur' + NewLine;
  S := S + Space + '- 2 GB RAM minimum (4 GB recommandé)' + NewLine;
  S := S + Space + '- 500 MB d''espace disque disponible' + NewLine;

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

$ErrorActionPreference = "Stop"

[reflection.assembly]::LoadWithPartialName("Microsoft.SqlServer.Smo")
Import-Module SitecoreInstallFramework
Import-Module Carbon

function Get-SitecoreDatabase(
    [parameter(Mandatory=$true)]$SqlServer, 
    [parameter(Mandatory=$true)]$SqlAdminUser, 
    [parameter(Mandatory=$true)]$SqlAdminPassword
) {
    $databaseServer = New-Object Microsoft.SqlServer.Management.Smo.Server($SqlServer)
    $databaseServer.ConnectionContext.LoginSecure = $false
    $databaseServer.ConnectionContext.Login = $SqlAdminUser
    $databaseServer.ConnectionContext.Password = $SqlAdminPassword
    $databases = $databaseServer.Databases
    return $databaseServer
}


function Remove-SitecoreDatabase(
    [parameter(Mandatory=$true)] $Name,
    [parameter(Mandatory=$true)] $Server
) {
    if ($Server.Databases[$Name]) {
        $Server.KillDatabase($Name)
        Write-Host "Database ($Name) is dropped" -ForegroundColor Green
    }
    else {
        Write-Host "Could not find database ($Name)" -ForegroundColor Yellow
    }
}

function Remove-SitecoreSolrCore(
    [parameter(Mandatory=$true)]$coreName,
    [parameter(Mandatory=$true)]$root) {

    $coreRootPath = Join-Path $root "server\solr"
    $corePath = Join-Path $coreRootPath $coreName
    if (Test-Path $corePath) {
        Remove-Item $corePath -Force -Recurse
        Write-Host "Solr Core $coreName, ($corePath) is removed" -ForegroundColor Green
    }
    else {
        Write-Host "Could not find Solr Core $coreName, ($corePath)" -ForegroundColor Yellow
    }
}

function Remove-SitecoreCertificate($certificateName) {

    $cert = Get-ChildItem -Path "cert:\LocalMachine\My" | Where-Object { $_.subject -like "CN=$certificateName" }
    if ($cert -and $cert.Thumbprint) {
        $certPath = "cert:\LocalMachine\My\" + $cert.Thumbprint
        Remove-Item $certPath
        Write-Host "Removing certificate $certificateName ($certPath)" -ForegroundColor Green
    }
    else {
        Write-Host "Could not find certificate $certificateName under cert:\LocalMachine\My" -ForegroundColor Yellow
    }
}

function Remove-SitecoreWindowsService($name) {
    if (Get-Service $name -ErrorAction SilentlyContinue) {
        Uninstall-Service $name
        Write-Host "Windows service $name is uninstalled" -ForegroundColor Green
    }
    else {
        Write-Host "Could not find windows service $name" -ForegroundColor Yellow
    }
}


function Remove-SitecoreIisSite($name) {
    # Delete site
    if (Get-IisWebsite $name) {
        Uninstall-IisWebsite $name
        Write-Host "IIS site $name is uninstalled" -ForegroundColor Green
    }
    else {
        Write-Host "Could not find IIS site $name" -ForegroundColor Yellow
    }

    # Delete app pool
    if (Get-IisAppPool $name) {
        Uninstall-IisAppPool $name
        Write-Host "IIS App Pool $name is uninstalled" -ForegroundColor Green
    }
    else {
        Write-Host "Could not find IIS App Pool $name" -ForegroundColor Yellow
    }
}

function Remove-SitecoreFiles($path) {
    # Delete site
    if (Test-Path($path)) {
        Remove-Item $path -Recurse -Force
        Write-Host "Removing files $path" -ForegroundColor Green
    }
    else {
        Write-Host "Could not find files $path" -ForegroundColor Yellow
    }
}

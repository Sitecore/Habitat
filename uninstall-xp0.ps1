#####################################################
# 
#  Uninstall Sitecore
# 
#####################################################
$ErrorActionPreference = "Stop"
. $PSScriptRoot\settings.ps1

Write-Host "*******************************************************" -ForegroundColor Green
Write-Host " Uninstalling Sitecore $SitecoreVersion" -ForegroundColor Green
Write-Host " Sitecore: $SitecoreSiteName" -ForegroundColor Green
Write-Host " xConnect: $XConnectSiteName" -ForegroundColor Green
Write-Host "*******************************************************" -ForegroundColor Green

Push-Location $AssetsRoot
$uninstallParams = @{
    Path = $SingleDeveloperConfiguration
    SqlServer = $SqlServer
    SqlAdminUser = $SqlAdminUser
    SqlAdminPassword = $SqlAdminPassword
    SolrUrl = $SolrUrl
    SolrRoot = $SolrRoot
    SolrService = $SolrService
    Prefix = $SolutionPrefix
    XConnectCertificateName = $XConnectSiteName
    IdentityServerCertificateName = $IdentityServerSiteName
    IdentityServerSiteName = $IdentityServerSiteName
    LicenseFile = $LicenseFile
    XConnectPackage = $XConnectPackage
    SitecorePackage = $SitecorePackage
    IdentityServerPackage = $IdentityServerPackage
    XConnectSiteName = $XConnectSiteName
    SitecoreSitename = $SitecoreSiteName
    PasswordRecoveryUrl = $SitecoreSiteUrl
    SitecoreIdentityAuthority = $IdentityServerUrl
    XConnectCollectionService = $XConnectSiteUrl
    ClientSecret = $IdentityClientSecret
    AllowedCorsOrigins = $IdentityAllowedCorsOrigins
    SitecoreAdminPassword = $SitecoreAdminPassword
}
try {
    Uninstall-SitecoreConfiguration @uninstallParams *>&1 | Tee-Object XP0-SingleDeveloper-Uninstall.log
}
catch
{
    write-host "Sitecore XP0 Single Developer Uninstall Failed" -ForegroundColor Red
    throw
}
finally {
    Pop-Location
}

# Remove App Pool membership 
try 
{
    Remove-LocalGroupMember "Performance Log Users" "IIS AppPool\$SitecoreSiteName"
    Write-Host "Removed IIS AppPool\$SitecoreSiteName from Performance Log Users" -ForegroundColor Green
}
catch 
{
    Write-Host "Could not find IIS AppPool\$SitecoreSiteName in Performance Log Users" -ForegroundColor Yellow
}
try 
{
    Remove-LocalGroupMember "Performance Monitor Users" "IIS AppPool\$SitecoreSiteName"
    Write-Host "Removed IIS AppPool\$SitecoreSiteName from Performance Monitor Users" -ForegroundColor Green
}
catch 
{
    Write-Host "Could not find IIS AppPool\$SitecoreSiteName to Performance Monitor Users" -ForegroundColor Yellow
}
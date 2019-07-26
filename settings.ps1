# Solution parameters
$SolutionPrefix = "habitat"
$SitePostFix = "dev.local"
$webroot = "C:\inetpub\wwwroot"

$SitecoreVersion = "9.2.0 rev. 002893"
$IdentityServerVersion = "3.0.0 rev. 00211"
$InstallerVersion = "2.1.0"

# Assets and prerequisites
$AssetsRoot = "$PSScriptRoot\build\assets"
$AssetsPSRepository = "https://sitecore.myget.org/F/sc-powershell/api/v2/"
$AssetsPSRepositoryName = "SitecoreGallery"

$LicenseFile = "$AssetsRoot\license.xml"

# Certificates
$CertPath = Join-Path "$AssetsRoot" "Certificates"

# SQL Parameters
$SqlServer = "."
$SqlAdminUser = "sa"
$SqlAdminPassword = "12345"
# Prerequisities Check
$PrerequisitiesConfiguration = "$AssetsRoot\Prerequisites.json"

# XP0 Single Developer Parameters
$SingleDeveloperConfiguration = "$AssetsRoot\XP0-SingleDeveloper.json"

# Sitecore Parameters
$SitecorePackage = "$AssetsRoot\Sitecore $SitecoreVersion (OnPrem)_single.scwdp.zip"
$SitecoreSiteName = "$SolutionPrefix.$SitePostFix"
$SitecoreSiteUrl = "http://$SitecoreSiteName"
$SitecoreSiteRoot = Join-Path $webroot -ChildPath $SitecoreSiteName
$SitecoreAdminPassword = "b"

# XConnect Parameters
$XConnectPackage = "$AssetsRoot\Sitecore $SitecoreVersion (OnPrem)_xp0xconnect.scwdp.zip"
$XConnectSiteName = "${SolutionPrefix}_xconnect.$SitePostFix"
$XConnectSiteUrl = "https://$XConnectSiteName"
$XConnectSiteRoot = Join-Path $webroot -ChildPath $XConnectSiteName

# Identity Server Parameters
$IdentityServerSiteName = "${SolutionPrefix}_IdentityServer.$SitePostFix"
$IdentityServerUrl = "https://$IdentityServerName"
$IdentityServerPackage = "$AssetsRoot\Sitecore.IdentityServer $IdentityServerVersion (OnPrem)_identityserver.scwdp.zip"
$IdentityClientSecret = "SPDHZpF6g8EXq5F7C5EhPQdsC1UbvTU3"
$IdentityAllowedCorsOrigins = $SitecoreSiteUrl
$IdentityServerSiteRoot = Join-Path $webroot -ChildPath $IdentityServerSiteName

# Solr Parameters
$SolrUrl = "https://solr750:8750/solr"
$SolrRoot = "C:\\solr\\solr-7.5.0"
$SolrService = "Solr-7.5.0"

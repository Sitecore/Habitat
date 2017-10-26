# Solution parameters
$SolutionPrefix = "habitat"
$SitePostFix = "dev.local"
$webroot = "C:\inetpub\wwwroot"

# Assets and prerequisites
$AssetsRoot = "$PSScriptRoot\build\assets"
$AssetsNugetFeed = "http://nuget1dk1:8181/nuget/9.0.0_master/"
$AssetsPSRepository = "http://nuget1dk1:8181/nuget/Sitecore_Gallery/"
$AssetsPSRepositoryName = "SitecoreGallery"

$SitecoreVersion = "9.0.0 rev. 171002"
$ConfigurationsVersion = "1.1.0-r00119"
$InstallerVersion = "1.0.2"
$ConfigurationRoot = "$AssetsRoot\Sitecore.WDP.Resources.$ConfigurationsVersion\content\Deployment\OnPrem\"

$LicenseFile = "$AssetsRoot\license.xml"

# Certificates
$CertPath = Join-Path "$AssetsRoot" "Certificates"

# SQL Parameters
$SqlServer = "."
$SqlAdminUser = "sa"
$SqlAdminPassword = "12345"

# XConnect Parameters
$XConnectPackage = "$AssetsRoot\Sitecore $SitecoreVersion (OnPrem)_xp0xconnect.scwdp.zip"
$XConnectSiteName = "${SolutionPrefix}_xconnect.$SitePostFix"
$XConnectCert = "$SolutionPrefix.$SitePostFix.xConnect.Client"
$XConnectSiteRoot = Join-Path $webroot -ChildPath $XConnectSiteName
$XConnectSqlCollectionUser = "collectionuser"
$XConnectSqlCollectionPassword = "Test12345"

# Sitecore Parameters
$SitecorePackage = "$AssetsRoot\Sitecore $SitecoreVersion (OnPrem)_single.scwdp.zip"
$SitecoreSiteName = "$SolutionPrefix.$SitePostFix"
$SitecoreSiteRoot = Join-Path $webroot -ChildPath $SitecoreSiteName

# Solr Parameters
$SolrUrl = "https://localhost:8983/solr"
$SolrRoot = "c:\\solr"
$SolrService = "Solr"

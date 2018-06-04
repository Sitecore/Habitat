#####################################################
# 
#  Uninstall Sitecore
# 
#####################################################
$ErrorActionPreference = "Stop"
. $PSScriptRoot\settings.ps1

Write-Host "*******************************************************" -ForegroundColor Green
Write-Host " UN Installing Sitecore $SitecoreVersion" -ForegroundColor Green
Write-Host " Sitecore: $SitecoreSiteName" -ForegroundColor Green
Write-Host " xConnect: $XConnectSiteName" -ForegroundColor Green
Write-Host "*******************************************************" -ForegroundColor Green

Import-Module "$PSScriptRoot\build\uninstall\uninstall.psm1" -Force

$carbon = Get-Module Carbon
if (-not $carbon) {
    $carbon = Get-InstalledModule Carbon -ErrorAction SilentlyContinue
    if (-not $carbon) {
        write-host "Installing Carbon..." -ForegroundColor Green
        Install-Module -Name 'Carbon' -AllowClobber -Scope CurrentUser -Repository PSGallery
    }
    Import-Module Carbon
}

$database = Get-SitecoreDatabase -SqlServer $SqlServer -SqlAdminUser $SqlAdminUser -SqlAdminPassword $SqlAdminPassword

# Unregister xconnect services
Remove-SitecoreWindowsService "$XConnectSiteName-MarketingAutomationService"
Remove-SitecoreWindowsService "$XConnectSiteName-IndexWorker"

# Delete xconnect site
Remove-SitecoreIisSite $XConnectSiteName

# Drop xconnect databases
Remove-SitecoreDatabase -Name "${SolutionPrefix}_Xdb.Collection.Shard0" -Server $database
Remove-SitecoreDatabase -Name "${SolutionPrefix}_Xdb.Collection.Shard1" -Server $database
Remove-SitecoreDatabase -Name "${SolutionPrefix}_Xdb.Collection.ShardMapManager" -Server $database
Remove-SitecoreDatabase -Name "${SolutionPrefix}_MarketingAutomation" -Server $database
Remove-SitecoreDatabase -Name "${SolutionPrefix}_Processing.Pools" -Server $database
Remove-SitecoreDatabase -Name "${SolutionPrefix}_Processing.Tasks" -Server $database
Remove-SitecoreDatabase -Name "${SolutionPrefix}_ReferenceData" -Server $database
Remove-SitecoreDatabase -Name "${SolutionPrefix}_Reporting" -Server $database

# Delete xconnect files
Remove-SitecoreFiles $XConnectSiteRoot

# Delete xconnect cores
Stop-Service $SolrService
Remove-SitecoreSolrCore "${SolutionPrefix}_xdb" -Root $SolrRoot
Remove-SitecoreSolrCore "${SolutionPrefix}_xdb_rebuild" -Root $SolrRoot
Start-Service $SolrService

# Delete xconnect server certificate
Remove-SitecoreCertificate $XConnectSiteName
# Delete xconnect client certificate
Remove-SitecoreCertificate $XConnectCert

# Delete sitecore site
Remove-SitecoreIisSite $SitecoreSiteName

# Drop sitecore databases
Remove-SitecoreDatabase -Name "${SolutionPrefix}_Core" -Server $database
Remove-SitecoreDatabase -Name "${SolutionPrefix}_ExperienceForms" -Server $database
Remove-SitecoreDatabase -Name "${SolutionPrefix}_EXM.Master" -Server $database
Remove-SitecoreDatabase -Name "${SolutionPrefix}_Master" -Server $database
Remove-SitecoreDatabase -Name "${SolutionPrefix}_Web" -Server $database
Remove-SitecoreDatabase -Name "${SolutionPrefix}_Messaging" -Server $database

# Delete sitecore files
Remove-SitecoreFiles $SitecoreSiteRoot

# Delete sitecore cores
Stop-Service $SolrService
Remove-SitecoreSolrCore "${SolutionPrefix}_core_index" -Root $SolrRoot
Remove-SitecoreSolrCore "${SolutionPrefix}_master_index" -Root $SolrRoot
Remove-SitecoreSolrCore "${SolutionPrefix}_web_index" -Root $SolrRoot
Remove-SitecoreSolrCore "${SolutionPrefix}_marketingdefinitions_master" -Root $SolrRoot
Remove-SitecoreSolrCore "${SolutionPrefix}_marketingdefinitions_web" -Root $SolrRoot
Remove-SitecoreSolrCore "${SolutionPrefix}_marketing_asset_index_master" -Root $SolrRoot
Remove-SitecoreSolrCore "${SolutionPrefix}_marketing_asset_index_web" -Root $SolrRoot
Remove-SitecoreSolrCore "${SolutionPrefix}_testing_index" -Root $SolrRoot
Remove-SitecoreSolrCore "${SolutionPrefix}_suggested_test_index" -Root $SolrRoot
Remove-SitecoreSolrCore "${SolutionPrefix}_fxm_master_index" -Root $SolrRoot
Remove-SitecoreSolrCore "${SolutionPrefix}_fxm_web_index" -Root $SolrRoot
Start-Service $SolrService

# Delete sitecore certificate
Remove-SitecoreCertificate $SitecoreSiteName
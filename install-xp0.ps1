#####################################################
# 
#  Install Sitecore
# 
#####################################################
$ErrorActionPreference = 'Stop'

. $PSScriptRoot\settings.ps1

Write-Host "*******************************************************" -ForegroundColor Green
Write-Host " Installing Sitecore $SitecoreVersion" -ForegroundColor Green
Write-Host " Sitecore: $SitecoreSiteName" -ForegroundColor Green
Write-Host " xConnect: $XConnectSiteName" -ForegroundColor Green
Write-Host "*******************************************************" -ForegroundColor Green

function Install-Prerequisites {
    #Verify SQL version
    $SqlRequiredVersion = "13.0.4001"
    [reflection.assembly]::LoadWithPartialName("Microsoft.SqlServer.Smo") | out-null
    $srv = New-Object "Microsoft.SqlServer.Management.Smo.Server" $SqlServer
    if (-not $srv -or -not $srv.Version) {
        throw "Could not find SQL Server '$SqlServer', check settings.ps1"
    }
    $minVersion = New-Object System.Version($RequiredSqlVersion)
    if ($srv.Version.CompareTo($minVersion) -lt 0) {
        throw "Invalid SQL version. Expected SQL 2016 SP1 (13.0.4001.0) or over."
    }

    #Verify Java version
    $JRERequiredVersion = "1.8"
    $minVersion = New-Object System.Version($JRERequiredVersion)
    $foundVersion = $FALSE
    $jrePath = "HKLM:\SOFTWARE\JavaSoft\Java Runtime Environment"
	$jdkPath = "HKLM:\SOFTWARE\JavaSoft\Java Development Kit"
	$jrePath9 = "HKLM:\SOFTWARE\JavaSoft\JRE"
    $jdkPath9 = "HKLM:\SOFTWARE\JavaSoft\JDK"
	if (Test-Path $jrePath) {
		$path = $jrePath
	}
	elseif (Test-Path $jdkPath) {
		$path = $jdkPath
	}
    elseif (Test-Path $jrePath9) {
		$path = $jrePath9
	}
    elseif (Test-Path $jdkPath9) {
		$path = $jdkPath9
	}
	else {
        throw "Cannot find Java Runtime Environment or Java Development Kit on this machine."
	}
	
	$javaVersionStrings = Get-ChildItem $path | ForEach-Object { $parts = $_.Name.Split("\"); $parts[$parts.Count-1] } 
    foreach ($versionString in $javaVersionStrings) {
        try {
            $version = New-Object System.Version($versionString)
        } catch {
            continue
        }

        if ($version.CompareTo($minVersion) -ge 0) {
            $foundVersion = $TRUE
        }
    }
    if (-not $foundVersion) {
        throw "Invalid Java version. Expected $minVersion or over."
    }

    # Verify Web Deploy
    $webDeployPath = ([IO.Path]::Combine($env:ProgramFiles, 'iis', 'Microsoft Web Deploy V3', 'msdeploy.exe'))
    if (!(Test-Path $webDeployPath)) {
        throw "Could not find WebDeploy in $webDeployPath"
    }   

    # Verify DAC Fx
    # Verify Microsoft.SqlServer.TransactSql.ScriptDom.dll
    try {
        $assembly = [reflection.assembly]::LoadWithPartialName("Microsoft.SqlServer.TransactSql.ScriptDom")
        if (-not $assembly) {
            throw "error"
        }
    } catch {
        throw "Could load the Microsoft.SqlServer.TransactSql.ScriptDom assembly. Please make sure it is installed and registered in the GAC"
    }
    
    #Enable Contained Databases
    Write-Host "Enable contained databases" -ForegroundColor Green
    try
    {
        Invoke-Sqlcmd -ServerInstance $SqlServer `
                      -Username $SqlAdminUser `
                      -Password $SqlAdminPassword `
                      -InputFile "$PSScriptRoot\build\database\containedauthentication.sql"
    }
    catch
    {
        write-host "Set Enable contained databases failed" -ForegroundColor Red
        throw
    }

    # Verify Solr
    Write-Host "Verifying Solr connection" -ForegroundColor Green
    if (-not $SolrUrl.ToLower().StartsWith("https")) {
        throw "Solr URL ($SolrUrl) must be secured with https"
    }
	$SolrRequest = [System.Net.WebRequest]::Create($SolrUrl)
	$SolrResponse = $SolrRequest.GetResponse()
	try {
		If ($SolrResponse.StatusCode -ne 200) {
			throw "Could not contact Solr on '$SolrUrl'. Response status was '$SolrResponse.StatusCode'"
		}
	}
	finally {
		$SolrResponse.Close()
    }
    
    Write-Host "Verifying Solr directory" -ForegroundColor Green
    if(-not (Test-Path "$SolrRoot\server")) {
        throw "The Solr root path '$SolrRoot' appears invalid. A 'server' folder should be present in this path to be a valid Solr distributive."
    }

    Write-Host "Verifying Solr service" -ForegroundColor Green
    try {
        $null = Get-Service $SolrService
    } catch {
        throw "The Solr service '$SolrService' does not exist. Perhaps it's incorrect in settings.ps1?"
    }

	#Verify .NET framework
	$requiredDotNetFrameworkVersionValue = 394802
	$requiredDotNetFrameworkVersion = "4.6.2"
	$versionExists = Get-ChildItem "hklm:SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full\" | Get-ItemPropertyValue -Name Release | % { $_ -ge $requiredDotNetFrameworkVersionValue }
	if (-not $versionExists) {
		throw "Please install .NET Framework $requiredDotNetFrameworkVersion or later"
	}
}

function Install-Assets {
    #Register Assets PowerShell Repository
    if ((Get-PSRepository | Where-Object {$_.Name -eq $AssetsPSRepositoryName}).count -eq 0) {
        Register-PSRepository -Name $AssetsPSRepositoryName -SourceLocation $AssetsPSRepository -InstallationPolicy Trusted
    }

    #Sitecore Install Framework dependencies
    Import-Module WebAdministration

    #Install SIF
    $module = Get-Module -FullyQualifiedName @{ModuleName="SitecoreInstallFramework";ModuleVersion=$InstallerVersion}
    if (-not $module) {
        write-host "Installing the Sitecore Install Framework, version $InstallerVersion" -ForegroundColor Green
        Install-Module SitecoreInstallFramework -RequiredVersion $InstallerVersion -Scope CurrentUser -Repository $AssetsPSRepositoryName
        Import-Module SitecoreInstallFramework -RequiredVersion $InstallerVersion
    }

    #Verify that manual assets are present
    if (!(Test-Path $AssetsRoot)) {
        throw "$AssetsRoot not found"
    }

    #Verify license file
    if (!(Test-Path $LicenseFile)) {
        throw "License file $LicenseFile not found"
    }
    
    #Verify Sitecore package
    if (!(Test-Path $SitecorePackage)) {
        throw "Sitecore package $SitecorePackage not found"
    }
    
    #Verify xConnect package
    if (!(Test-Path $XConnectPackage)) {
        throw "XConnect package $XConnectPackage not found"
    }
}

function Install-XConnect {
    #Install xConnect Solr
    try
    {
        Install-SitecoreConfiguration $XConnectSolrConfiguration `
                                      -SolrUrl $SolrUrl `
                                      -SolrRoot $SolrRoot `
                                      -SolrService $SolrService `
                                      -CorePrefix $SolutionPrefix
    }
    catch
    {
        write-host "XConnect SOLR Failed" -ForegroundColor Red
        throw
    }

    #Generate xConnect client certificate
    try
    {
        Install-SitecoreConfiguration $XConnectCertificateConfiguration `
                                      -CertificateName $XConnectCert `
                                      -CertPath $CertPath
    }
    catch
    {
        write-host "XConnect Certificate Creation Failed" -ForegroundColor Red
        throw
    }

    #Install xConnect
    try
    {
        Install-SitecoreConfiguration $XConnectConfiguration `
                                      -Package $XConnectPackage `
                                      -LicenseFile $LicenseFile `
                                      -SiteName $XConnectSiteName `
                                      -XConnectCert $XConnectCert `
                                      -SqlDbPrefix $SolutionPrefix `
                                      -SolrCorePrefix $SolutionPrefix `
                                      -SqlAdminUser $SqlAdminUser `
                                      -SqlAdminPassword $SqlAdminPassword `
                                      -SqlServer $SqlServer `
                                      -SqlCollectionUser $XConnectSqlCollectionUser `
                                      -SqlCollectionPassword $XConnectSqlCollectionPassword `
                                      -SolrUrl $SolrUrl `
                                      -SqlProcessingPoolsPassword $SqlStrongPassword `
                                      -SqlReferenceDataPassword $SqlStrongPassword `
                                      -SqlMarketingAutomationPassword $SqlStrongPassword `
                                      -SqlMessagingPassword $SqlStrongPassword `

    }
    catch
    {
        write-host "XConnect Setup Failed" -ForegroundColor Red
        throw
    }
                             

    #Set rights on the xDB connection database
    Write-Host "Setting Collection User rights" -ForegroundColor Green
    try
    {
        $sqlVariables = "DatabasePrefix = $SolutionPrefix", "UserName = $XConnectSqlCollectionUser", "Password = $XConnectSqlCollectionPassword"
        Invoke-Sqlcmd -ServerInstance $SqlServer `
                      -Username $SqlAdminUser `
                      -Password $SqlAdminPassword `
                      -InputFile "$PSScriptRoot\build\database\collectionusergrant.sql" `
                      -Variable $sqlVariables
    }
    catch
    {
        write-host "Set Collection User rights failed" -ForegroundColor Red
        throw
    }
}

function Install-Sitecore {

    try
    {
        #Install Sitecore Solr
        Install-SitecoreConfiguration $SitecoreSolrConfiguration `
                                      -SolrUrl $SolrUrl `
                                      -SolrRoot $SolrRoot `
                                      -SolrService $SolrService `
                                      -CorePrefix $SolutionPrefix
    }
    catch
    {
        write-host "Sitecore SOLR Failed" -ForegroundColor Red
        throw
    }

    try
    {
        #Install Sitecore
        Install-SitecoreConfiguration $SitecoreConfiguration `
                                      -Package $SitecorePackage `
                                      -LicenseFile $LicenseFile `
                                      -SiteName $SitecoreSiteName `
                                      -XConnectCert $XConnectCert `
                                      -SqlDbPrefix $SolutionPrefix `
                                      -SolrCorePrefix $SolutionPrefix `
                                      -SqlAdminUser $SqlAdminUser `
                                      -SqlAdminPassword $SqlAdminPassword `
                                      -SqlServer $SqlServer `
                                      -SolrUrl $SolrUrl `
                                      -XConnectCollectionService "https://$XConnectSiteName" `
                                      -XConnectReferenceDataService "https://$XConnectSiteName" `
                                      -MarketingAutomationOperationsService "https://$XConnectSiteName" `
                                      -MarketingAutomationReportingService "https://$XConnectSiteName" `
                                      -SqlCorePassword $SqlStrongPassword `
                                      -SqlMasterPassword $SqlStrongPassword `
                                      -SqlWebPassword $SqlStrongPassword `
                                      -SqlReportingPassword $SqlStrongPassword `
                                      -SqlProcessingPoolsPassword $SqlStrongPassword `
                                      -SqlProcessingTasksPassword $SqlStrongPassword `
                                      -SqlReferenceDataPassword $SqlStrongPassword `
                                      -SqlMarketingAutomationPassword $SqlStrongPassword `
                                      -SqlFormsPassword $SqlStrongPassword `
                                      -SqlExmMasterPassword $SqlStrongPassword `
                                      -SqlMessagingPassword $SqlStrongPassword `

    }
    catch
    {
        write-host "Sitecore Setup Failed" -ForegroundColor Red
        throw
    }

    try
    {
        #Set web certificate on Sitecore site
        Install-SitecoreConfiguration $SitecoreSSLConfiguration `
                                      -SiteName $SitecoreSiteName
    }
    catch
    {
        write-host "Sitecore SSL Binding Failed" -ForegroundColor Red
        throw
    }
}

function Add-AppPool-Membership {

    #Add ApplicationPoolIdentity to performance log users to avoid Sitecore log errors (https://kb.sitecore.net/articles/404548)
    
    try 
    {
        Add-LocalGroupMember "Performance Log Users" "IIS AppPool\$SitecoreSiteName"
        Write-Host "Added IIS AppPool\$SitecoreSiteName to Performance Log Users" -ForegroundColor Green
    }
    catch 
    {
        Write-Host "Warning: Couldn't add IIS AppPool\$SitecoreSiteName to Performance Log Users -- user may already exist" -ForegroundColor Yellow
    }
    try 
    {
        Add-LocalGroupMember "Performance Monitor Users" "IIS AppPool\$SitecoreSiteName"
        Write-Host "Added IIS AppPool\$SitecoreSiteName to Performance Monitor Users" -ForegroundColor Green
    }
    catch 
    {
        Write-Host "Warning: Couldn't add IIS AppPool\$SitecoreSiteName to Performance Monitor Users -- user may already exist" -ForegroundColor Yellow
    }
}

Install-Prerequisites
Install-Assets
Install-XConnect
Install-Sitecore
Add-AppPool-Membership


# TODO: 
# Run optimization scripts
# Deploy-Habitat
# Deploy marketing definitions
# Rebuild indexes
# Rebuild links database
# Test-Setup

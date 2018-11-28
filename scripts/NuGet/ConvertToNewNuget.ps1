## Nuget v2 URL for the target platform, and the target marketing version
$SitecoreNuget = "https://sitecore.myget.org/F/sc-platform-9-1/api/v2"
$TargetVersion = "9.1.0"

## Nuget URL to retrieve package metadata, including dependencies
$MetaPackage = "$SitecoreNuget/Packages(Id='Sitecore.Experience.Platform',Version='$TargetVersion')"

## Retrieve the package metadata from nuget (no nice way of doing this via CLI)
$PackageMetadata = [xml](Invoke-WebRequest $MetaPackage).Content

## Parse the package dependencies so we know the appropriate package versions for this release
$versions = $PackageMetadata.entry.properties.Dependencies.Split('|') | % {
    $dependencySplit = $_.Split(':')
    New-Object PSObject -Property @{
        Name = $dependencySplit[0]
        Version = $dependencySplit[1]
    }
}

## Find all Visual Studio projects and parse their XML
Get-ChildItem *.csproj -recurse | % {
    New-Object psobject -Property @{
        File = $_
        ProjectXml = [xml](gc $_ -Raw)
    }
} | % {
    $fileName = $_.File.Name

    ## Find all package references
    $_.ProjectXml.GetElementsByTagName("PackageReference") | ? {
        ## Exclude non-Sitecore packages, and FakeDb
        $_.Include.StartsWith("Sitecore") -and -not $_.Include.Contains("FakeDb")
    } | % {
        ## Remove NoReferences extension on the package if present
        if ($_.Include.EndsWith(".NoReferences")) {
            $_.SetAttribute("Include", $_.Include.Replace(".NoReferences", ""))
        }
        ## Look up the appropriate version and replace
        $packageName = $_.Include
        $newPackage = $versions | ? { $_.Name -eq $packageName } | select -first 1
        if (-not $newPackage) {
            Write-Host "WARNING: Could not find new version for package $packageName in $fileName" -ForegroundColor yellow
        }
        $_.SetAttribute("Version", $newPackage.Version)
    }

    ## Save the project file
    $_.ProjectXml.Save($_.File)
}
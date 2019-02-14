$TargetVersion = "9.1.0"

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
        $_.SetAttribute("Version", $TargetVersion)
    }

    ## Save the project file
    $_.ProjectXml.Save($_.File)
}
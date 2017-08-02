# Script to convert all sitecore assembly references to Sitecore Public NuGet feed
# Run from root solution folder (where your packages folder is)
# execute this script with powershell
Param
(
    $sitecoreVersion = '8.2.160729', # NuGet package version to convert to. Format is major.minor.releasedate.
    $frameworkVersion = 'net452' # for 8.2: net452. For 7.0-8.1: net45
)

$ScriptPath = $MyInvocation.MyCommand.Path
$ParentPath = Split-Path -Parent $ScriptPath
$SolutionDir  = Join-Path -path $ParentPath -childpath ".."
$MsbNSString = 'http://schemas.microsoft.com/developer/msbuild/2003'
$MsbNS = @{msb = $MsbNSString}
$PackagesConfigFileName = 'packages.config'

#Create project.json from packages.config
Write-Host 'Scanning for projects to update...' -ForegroundColor Green
Get-ChildItem -path '..' -Recurse -Include $PackagesConfigFileName |
    ForEach {
        $PackageFilePath = $_.FullName
        $PackageFileDir = $_.Directory
        Write-Host "Processing $PackageFilePath" -ForegroundColor Green

        # Find existing csproj to match direct references
        $csproj = Resolve-Path "$($_.Directory)\*.csproj"
        $proj = [xml] (Get-Content $csproj)

        # Find existing Sitecore references and NuGet-ify them
        Write-Host "Checking for non-NuGet Sitecore references in $csproj"
        $xpath = "//msb:Reference/msb:HintPath[not(contains(.,'packages\'))]"

        $changedProj = $false
        $sitecoreNuGetPackages = @(Select-Xml -xpath $xpath $proj -Namespace $MsbNS | foreach {
            $node = $_.Node.ParentNode

            $referenceName = $node.Attributes['Include'].Value.Split(',')[0]

              # Filter non-NuGet references to transform into NuGet packages
            if($referenceName.StartsWith("Sitecore") `
                -or $referenceName.StartsWith('sitecore.nexus') `
                -and -not $referenceName.StartsWith('Sitecore.PrintStudio') `
                -and -not $referenceName.StartsWith('Sitecore.Foundation') `
                -and -not $referenceName.StartsWith('Sitecore.Feature') `
                -and -not ($referenceName.StartsWith('Sitecore') -and $referenceName.EndsWith('Website'))) {

                $changedProj = $true

                Write-Host "NuGet-ifying assembly reference $referenceName"

                # set hintPath to package path
                Push-Location -Path $PackageFileDir
                $hintPathRoot = Resolve-Path "$SolutionDir\packages" -Relative
                Pop-Location

                $hintPath = "$hintPathRoot\$referenceName.NoReferences.$sitecoreVersion\lib\$frameworkVersion\$referenceName.dll"

                $existingHintPath = $node['HintPath', $MsbNSString]
                if($existingHintPath -eq $null) {
                    $hint = $proj.CreateElement("HintPath", $MsbNSString)
                    $hint.InnerXml = $hintPath
                    $foo = $node.AppendChild($hint)
                } else {
                    $existingHintPath.InnerXml = $hintPath
                }

                "$referenceName.NoReferences"
            }
        })

        if($changedProj) {
            Write-Host "Saving NuGet-ified references to csproj" -ForegroundColor Yellow
            $proj.Save($csproj)
        } else {
            Write-Host "Found no references to change."
        }

        # Add packages to packages.config
        $packageXml = [xml] (Get-Content $PackageFilePath)

        $sitecoreNuGetPackages | % {
            $packageNode = $packageXml.CreateElement('package');
            $packageNode.SetAttribute('id', $_)
            $packageNode.SetAttribute('version', $sitecoreVersion)
            $packageNode.SetAttribute('targetFramework', $frameworkVersion)

            $foo = $packageXml.DocumentElement.AppendChild($packageNode)
        }

        Write-Host "Updating packages.config with new packages" -ForegroundColor Yellow
        $packageXml.Save($PackageFilePath)
    }
$ns = @{x = 'http://schemas.microsoft.com/developer/msbuild/2003'}
$noWarn = "NU1603"

function Create-XMLElement([System.Xml.XmlNode] $Node, $Name)
{
  $Node.OwnerDocument.CreateElement($Name, $ns.x)
}

## Find all Visual Studio projects and parse their XML
Get-ChildItem *.csproj -recurse | % {
    New-Object psobject -Property @{
        File = $_
        ProjectXml = [xml](gc $_ -Raw)
    }
} | % {
    $propertyGroup = $_.ProjectXml.Project.PropertyGroup[0]
    if (-not $propertyGroup) {
        return
    }
    $noWarnElement = Create-XMLElement -Node $propertyGroup -Name NoWarn
    $noWarnElement.InnerText = $noWarn
    $propertyGroup.AppendChild($noWarnElement) | out-null
    $_.ProjectXml.Save($_.File)
}
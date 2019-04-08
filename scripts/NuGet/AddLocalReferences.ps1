$ns = @{x = 'http://schemas.microsoft.com/developer/msbuild/2003'}

function Add-XMLAttribute([System.Xml.XmlNode] $Node, $Name, $Value)
{
  $attrib = $Node.OwnerDocument.CreateAttribute($Name)
  $attrib.Value = $Value
  $node.Attributes.Append($attrib)
}

function Create-XMLElement([System.Xml.XmlNode] $Node, $Name)
{
  $Node.OwnerDocument.CreateElement($Name, $ns.x)
}

Get-ChildItem *.csproj -recurse | % {
    New-Object psobject -Property @{
        File = $_
        ProjectXml = [xml](gc $_ -Raw)
    }
} | % {
    $_.ProjectXml.Project.ItemGroup | % {
        $itemGroup = $_
        $projectNode = $itemGroup.ParentNode
        $packageReferences = @()

        # collect the package references
        if ($itemGroup.PackageReference.Count -lt 1) {
            return
        }
        $itemGroup.PackageReference | ? {
            $_.Include.StartsWith("Sitecore") -and -not $_.Include.Contains("FakeDb")
        } | % {
            $packageReferences += $_
        }
        if ($packageReferences.Count -lt 1) {
            return
        }
        
        # remove package references from existing item group
        $packageReferences | % {
            $itemGroup.RemoveChild($_)
        }

        # add the build config import
        $import = Create-XMLElement -Node $projectNode -Name Import
        Add-XMLAttribute -Node $import -Name Project -Value '$(SolutionDir)\BuildConfiguration.csproj' | out-null
        Add-XMLAttribute -Node $import -Name Condition -Value 'Exists(''$(SolutionDir)\BuildConfiguration.csproj'')' | out-null
        $projectNode.PrependChild($import)

        # create the choose which will replace existing item group, and the conditional
        $choose = Create-XMLElement -Node $projectNode -Name Choose
        $when = Create-XMLElement -Node $projectNode -Name When
        Add-XMLAttribute -Node $when -Name Condition -Value '''$(LocalReferences)'' == ''true''' | out-null
        $choose.AppendChild($when)

        # create the new item group to go in the conditional
        $localReferenceGroup = Create-XMLElement -Node $_ -Name ItemGroup
        $packageReferences | % {
            $assembly = $_.Include.Replace('.NoReferences', '')
            $reference = Create-XMLElement -Node $_ -Name Reference
            Add-XMLAttribute -Node $reference -Name Include -Value $assembly | out-null
            $hintPath = Create-XMLElement -Node $_ -Name HintPath
            $hintPath.InnerText = "`$(SitecorePath)\bin\$assembly.dll"
            $reference.AppendChild($hintPath)
            $private = Create-XMLElement -Node $_ -Name Private
            $private.InnerText = "False"
            $reference.AppendChild($private)
            $localReferenceGroup.AppendChild($reference)
        }
        $when.AppendChild($localReferenceGroup)

        # add the package reference item group as default
        $otherwise = Create-XMLElement -Node $projectNode -Name Otherwise
        $choose.AppendChild($otherwise)      
        $packageReferenceGroup = Create-XMLElement -Node $_ -Name ItemGroup
        $otherwise.AppendChild($packageReferenceGroup)
        $packageReferences | % {
            $packageReferenceGroup.AppendChild($_)
        }

        # add the choose
        $projectNode.InsertAfter($choose, $itemGroup)

        # remove the original item group if empty
        if (-not $itemGroup.HasChildNodes) {
            $projectNode.RemoveChild($itemGroup)
        }
    }
    $_.ProjectXml.Save($_.File)
}
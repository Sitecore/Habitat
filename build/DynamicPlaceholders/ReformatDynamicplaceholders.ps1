# This script will transform the layout definitions on all items from the custom dynamicplaceholder format to the new standard v9 format
# Run this script using the Sitecore Powershell extensions

function Replace($renderings, $item) {
    [regex]$placeholderRegEx = "([\w-_\.]*)_([{(]?[0-9A-Fa-f]{8}[-]?([0-9A-Fa-f]{4}[-]?){3}[0-9A-Fa-f]{12}[)}]?)"
    $matches = $placeholderRegEx.Matches($renderings)
    if ($matches.Count -gt 0) 
    {
        Write-Host $item.Paths.FullPath
    }
    
    while ($matches.Count -gt 0) {
        $match = $matches[0]

        $placeholder = $match.Groups[1].Value
        $id = $match.Groups[2].Value.ToUpper()
        $newPlaceholderReference = "$placeholder-{$id}-0"
        $renderings = $renderings.Remove($match.Index, $match.Length).Insert($match.Index, $newPlaceholderReference);
        $matches = $placeholderRegEx.Matches($renderings)
    }
    return $renderings
}

Set-Location -Path master:\content\Habitat
$items = Get-ChildItem -Recurse
foreach ($item in $items) {
    foreach ($itemVersion in $item.Versions.GetVersions()) {
        $finalRenderings = $itemVersion["{04BF00DB-F5FB-41F7-8AB7-22408372A981}"]
        $renderings = $itemVersion["{F1A1FE9E-A60C-4DDB-A3A0-BB5B29FE732E}"]
        
        $newFinalRenderings = Replace $finalRenderings $itemVersion
        $newRenderings = Replace $renderings $itemVersion
        
        if ($newFinalRenderings -ne $finalRenderings -or $newRenderings -ne $renderings) {
            $item.Editing.BeginEdit()
            $item["{04BF00DB-F5FB-41F7-8AB7-22408372A981}"] = $newFinalRenderings
            $item["{F1A1FE9E-A60C-4DDB-A3A0-BB5B29FE732E}"] = $newRenderings
            $item.Editing.EndEdit()    
        }
    }
}

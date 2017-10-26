# Package name filter
$Filter = "Sitecore.FakeDb"
# Specify version to upgrade from
$FromVersion = "1.6.1"
# Specify version to upgrade to
$ToVersion = "1.7.0"
# Define Sitecore nuget package source
# HOSTED: https://sitecore.myget.org/F/sc-packages/api/v3/index.json
# LOCAL (as per CreateLocalNugetFeed.ps1): join-path ([Environment]::GetFolderPath("MyDocuments")) "NuGetLocal"
# $PackageSource = "https://sitecore.myget.org/F/sc-packages/api/v3/index.json"


$projects = Get-Project -All
$projectIndex = 1
foreach ($project in $projects) {
    Write-Host "Updating $($project.Name) ($projectIndex/$($projects.Count))" -foregroundcolor "yellow"
    $packages = Get-Package -ProjectName $project.Name -Filter $Filter
    $packageIndex = 1
    foreach ($package in $packages) {
        if ($package.Versions -eq $FromVersion) {
            Write-Host "Upgrade $($package.Id) ($packageIndex/$($packages.Count))" -foregroundcolor "magenta"
            Update-Package $package.Id -ProjectName $project.Name -Version $ToVersion -IgnoreDependencies # -WhatIf -Source $PackageSource
            $packageIndex++
        }
    }
    $projectIndex++
}

Write-Host "Updated $count packages"


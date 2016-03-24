param([string]$url, [string]$secret, [string]$configurations)
$ErrorActionPreference = 'Stop'

# This is an example PowerShell script that will remotely execute a Unicorn sync using the new CHAP authentication system.

Import-Module .\Unicorn.psm1
$configs = $configurations.Split('^')

Sync-Unicorn -ControlPanelUrl $url -SharedSecret $secret -Configurations $configs

# Note: you may pass -Verb 'Reserialize' for remote reserialize. Usually not needed though.
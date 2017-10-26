# Sitecore Habitat

Habitat and the tools and processes in it is a Sitecore solution example built on the [Helix architecture principles](http://helix.sitecore.net).  
The architecture and methodology focuses on:

* Simplicity - *A consistent and discoverable architecture*
* Flexibility - *Change and add quickly and without worry*
* Extensibility - *Simply add new features without steep learning curve*

For getting started, please check out the [Habitat Wiki](../../wiki).  
For more information on **Helix**, please go to [helix.sitecore.net](http://helix.sitecore.net).

## Installing Habitat

### Prerequisites

1. Clone this local repo in `c:\projects\sitecore.habitat\`
1. Check the [prerequisites for Sitecore v9](http://mfrtm1dk1.dk.sitecore.net/developer-workstation-guide-external/#install-prerequisite-software)
1. Check the following prerequisites for Habitat:
    * Visual Studio 2017
    * .NET 4.6.2
    * [Node.JS 4+](https://nodejs.org/) 
        * _NOTE: as of August 7, 2017, Node.JS 8.2.1 does not work, please use 6.11.2 instead._
1. Copy a Sitecore license file to `./assets/license.xml`
1. Download and place the following files in `./assets/`
    * [Sitecore 9.0.0.170916](https://screleasemanagement.blob.core.windows.net/sxp/SXP9.0.0/Sitecore%209.0.0%20rev.%20170916_331/OnPrem/xp0/Sitecore%209.0.0%20rev.%20170916%20(OnPrem)_single.scwdp.zip?sv=2016-05-31&sr=b&sig=Hn8wqD13Immzf4%2BdlEeZhkjV3K8MkjONRjLj7KAA0x4%3D&se=2017-11-15T06%3A26%3A43Z&sp=r)
    * [xConnect 9.0.0.170916](https://screleasemanagement.blob.core.windows.net/sxp/SXP9.0.0/Sitecore%209.0.0%20rev.%20170916_331/OnPrem/xp0/Sitecore%209.0.0%20rev.%20170916%20(OnPrem)_xp0xconnect.scwdp.zip?sv=2015-04-05&sr=b&sig=AaY%2BE5x3tRSmh0W1pF%2F3FKHmOGlYrJ81eEVng3UBw3Q%3D&se=2017-11-15T06%3A28%3A25Z&sp=r)

### *Standalone* Sitecore and Habitat setup

Check that your local machine, locations etc. alignbs with the settings specified in `.\settings.ps1`. This includes:
* web site root folder
* Sitecore XP version
* Sitecore Installer version
* SQL server name and login

Run the following in a PowerShell command line with admin rights:
1. Run `./install-xp0.ps1`
1. Run `npm install`
1. Run `gulp`
1. Visit the site on https://habitat.dev.local/sitecore
1. [Rebuild all search indexes](https://doc.sitecore.net/sitecore_experience_platform/setting_up_and_maintaining/search_and_indexing/indexing/rebuild_search_indexes)
1. [Deploy marketing definitions](https://doc.sitecore.net/sitecore_experience_platform/developing/marketing_operations/deploy_marketing_definitions)

To uninstall, run `.\uninstall-xp0.ps1`
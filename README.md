#Sitecore Habitat

Habitat is a Sitecore solution example built on a modular architecture.
The architecture and methodology focuses on:

* Simplicity - *A consistent and discoverable architecture*
* Flexibility - *Change and add quickly and without worry*
* Extensibility - *Simply add new features without steep learning curve*

For more information, please check out the [Habitat Wiki](../../wiki)

## Differences than the original

This version of Habitat is using Team Development For Sitecore for Item Serialization and Deployments.

Team Development for Sitecore is a Visual Studio plug-in managed by Hedgehog Development. At its core, it provides companies with the ability to automate their Sitecore builds or set up a continuous deployment scenarios. TDS provides several additional features its users find valuable, for more information visit: www.teamdevelopmentforsitecore.com.

##To Install:

1. Clone this repository to your local file system.
2. Set up a clean Sitecore 8.1 website in the URL http://habitat.local/ and the location C:\Websites\Habitat.local\ (We recommend using Sitecore Instance Manager).
3. Install the Webforms for Marketers module
4. Restore npm modules
  - Make sure you have the version 4+ of node.js Download here
  - Open an administrator command-line and run 'npm install' in the root of repository.
5. Open the solution in Visual Studio.
6. (optional) Configuring your settings if you are using other settings than default:
To change the standard location of source, website files and website URL modify the following files:
  - /Configuration/z.Habitat.DevSettings.config
  - /Configuration/gulp-config.js
  - /Configuration/TdsGlobal.config
7. Open the Visual Studio 2015 Task Runner Explorer (View | Other Windows | Task Runner Explorer).
8. Run the 01-Copy-Sitecore-Lib task.
9. Deploy the solution in Visual Studio 2015 to deploy all Sitecore Items and Project Items.
10. Run the 03_Apply-Xml-Transform task.
11. Be productive!

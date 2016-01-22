#Sitecore Habitat

Habitat is a Sitecore solution example built on a modular architecture.
The architecture and methodology focuses on:

* Simplicity - *A consistent and discoverable architecture*
* Flexibility - *Change and add quickly and without worry*
* Extensibility - *Simply add new features without steep learning curve*

For more information, please check out the [Habitat Wiki](../../wiki)

## This version of Habitat is based on Tag 1 and uses TDS as main Item Serialization and Deployment Provider

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
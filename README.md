#Habitat
========

##What is the goal of this project?
--------------------------------
Habitat is a Sitecore solution framework focusing on three aspects:

* Simplicity - *A consistent and discoverable architecture*
* Flexibility - *Change and add quickly and without worry*
* Extensibility - *Simply add new features without steep learning curve*

##Index
---------------
The solution consists of the following modules:

* Site.Common
* Theme.Common

Domain modules
* [Article](/Sitecore/Habitat/tree/master/src/Domain/Article/readme.md)
* [Identity](/Sitecore/Habitat/tree/master/src/Domain/Identity/readme.md)
* [Metadata](/Sitecore/Habitat/tree/master/src/Domain/Metadata/readme.md)
* [Navigation](/Sitecore/Habitat/tree/master/src/Domain/Navigation/readme.md)
* [News](/Sitecore/Habitat/tree/master/src/Domain/News/readme.md)
* [Social](/Sitecore/Habitat/tree/master/src/Domain/Social/readme.md)
* [Teasers](/Sitecore/Habitat/tree/master/src/Domain/Teasers/readme.md)

Framework modules
* [Assets](/Sitecore/Habitat/tree/master/src/Framework/Assets/readme.md)
* [SitecoreExtensions](/Sitecore/Habitat/tree/master/src/Framework/SitecoreExtensions/readme.md)
* [Taxonomy](/tree/master/src/Framework/Taxonomy/readme.md)

##Getting started
---------------

-   Clone this repository to your local file system.
-   Using SIM (Sitecore Instance Manager) set up a clean Sitecore website in the URL http://habitat/
-   Copy all assemblies from the Sitecore bin folder to the /lib/Sitecore folder
-   Open visual studio and copy the Habitat.Site.Common/App_Config/Include/z.Habitat.DevSettings.config.sample to z.Habitat.DevSettings.config
-   Change the setting to fit with your configuration (see below)
-   Build the solution in Visual Studio
-   Publish all projects using the Habitat publish settings
-   Open [/unicorn.aspx](http://habitat/unicorn.aspx) and synchronize the items to your project

###Configuring your settings

Please note that the project assumes the following settings:

*Source location:* C:\projects\Habitat\

*Website location:* C:\websites\Habitat\

To change these settings modify the following file:

Habitat.Site.Common/App_Config/Include/z.Habitat.DevSettings.config.sample

Resources
---------
-   [Sitecore Instance Manager](https://marketplace.sitecore.net/modules/sitecore_instance_manager.aspx)
-   Bug-tracking: [GitHub](https://github.com/Sitecore/Habitat/issues)
-   Build server:
-   Code quality:
-   Builds:

How can I contribute?
---------------------

Contact [Thomas Eldblom](mailto:the@sitecore.net)

For contributors
----------------

1. Use [meaningful names](http://blog.goyello.com/2013/05/17/express-names-in-code-bad-vs-clean/)
2. Write [clean code](http://www.amazon.com/Clean-Code-Handbook-Software-Craftsmanship/dp/0132350882)
3. Be a [boy scout](http://deviq.com/boy-scout-rule/)

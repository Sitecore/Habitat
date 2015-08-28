#Habitat
========

##What is the goal of this project?
--------------------------------

##Getting started
---------------

-   Clone this repository to your local file system.
-   Using SIM (Sitecore Instance Manager) set up a website in the URL http://habitat/
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
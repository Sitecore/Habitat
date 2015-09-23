#Habitat
========

##What is the goal of this project?
--------------------------------
Habitat is a Sitecore solution framework focusing on three aspects:

* Simplicity - *A consistent and discoverable architecture*
* Flexibility - *Change and add quickly and without worry*
* Extensibility - *Simply add new features without steep learning curve*

Architecture Introduction: [Video](https://youtu.be/2CELqflPhm0)  
Introduction to Modules: [Video](https://youtu.be/DgPrikqFe4s)  
Introduction to Layers: [Video](https://youtu.be/XKLpTMuQT4Y)

##Index
---------------
The solution consists of the following modules:

Project modules

* [Website](src/Project/Website/)  
* [Design](src/Project/Design/)

Domain modules

* [Accounts](src/Domain/Accounts/)
* [Identity](src/Domain/Identity/)
* [Language](src/Domain/Language/)
* [Media](src/Domain/Media/)
* [Metadata](src/Domain/Metadata/)
* [Navigation](src/Domain/Navigation/)
* [News](src/Domain/News/)
* [Search](src/Domain/Search/)
* [Social](src/Domain/Social/)
* [StandardContent](src/Domain/StandardContent/)

Framework modules

* [Assets](src/Framework/Assets/)
* [Indexing](src/Framework/Indexing/)
* [Serialization](src/Framework/Serialization/)
* [SitecoreExtensions](src/Framework/SitecoreExtensions/)
* [Taxonomy](src/Framework/Taxonomy/)

##Getting started
---------------

###Habitat uses the following:

* Gulp [gulpjs.com](http://gulpjs.com/)
* Sass [sass-lang.com](http://sass-lang.com/install)
* Node (npm) [npmjs.com](https://www.npmjs.com/)

###To install:

1.   Clone this repository to your local file system.
2.  Using SIM (Sitecore Instance Manager) set up a clean Sitecore website in the URL http://habitat/
3.  Copy all assemblies from the Sitecore bin folder to the /lib/Sitecore folder
4.  Open visual studio and copy the Habitat.Website/App_Config/Include/z.Habitat.DevSettings.config.sample to z.Habitat.DevSettings.config
5.  Change the setting to fit with your configuration (see below)
6.  Build the solution in Visual Studio
7.  Publish all projects using the Habitat publish settings
8.  Open [/unicorn.aspx](http://habitat/unicorn.aspx) and synchronize the items to your project

The project is configured to run Gulp theough the taskrunner in Visual Studio 2015. 

* The **Publish** task will publish all projects to the website location (see configuration below)
* The **Auto-Publish-Design** automatically publishes .css files when changed (Configure Sass compilation in Visual Studio)

###Configuring your settings

Please note that the project assumes the following settings:

*Source location:* C:\projects\Habitat\  
*Website location:* C:\websites\Habitat\

To change these settings modify the following files:

* Habitat.Website/App_Config/Include/z.Habitat.DevSettings.config.sample  
* Build/gulp.config.js  
* Build/publishsettings.targets  

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

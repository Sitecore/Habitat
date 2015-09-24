#Sitecore Habitat

##What is the goal of this project?
--------------------------------
Habitat is a Sitecore solution framework focusing on three aspects:

* Simplicity - *A consistent and discoverable architecture*
* Flexibility - *Change and add quickly and without worry*
* Extensibility - *Simply add new features without steep learning curve*

Video introductions:  

1. Architecture Introduction: [Video](https://youtu.be/2CELqflPhm0)  
2. Introduction to Modules: [Video](https://youtu.be/DgPrikqFe4s)  
3. Introduction to Layers: [Video](https://youtu.be/XKLpTMuQT4Y)

##Modules
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

[Watch a Getting Started video here](https://youtu.be/SIh4bLGTaLE)

> **Please note** that the project assumes the following settings:
> 
> *Source location:* C:\projects\Habitat\  
> *Website location:* C:\websites\Habitat\  
> *Website URL:* http://habitat  
>
> To change these settings see the "Configuring your settings" below


###Habitat uses the following:

* Visual Studio **2015**
* ASP.NET MVC
* NuGet
* Web Essentials
* Sitecore Rocks
* [Unicorn](https://github.com/kamsar/Unicorn)
* Gulp [gulpjs.com](http://gulpjs.com/)
* Sass [sass-lang.com](http://sass-lang.com/install)
* Node (npm) [npmjs.com](https://www.npmjs.com/)

###To install:

1.  Clone this repository to your local file system.
2.  Set up a clean Sitecore website in the URL http://habitat/ and the location C:\Websites\Habitat (We recommend using [Sitecore Instance Manager](https://marketplace.sitecore.net/Modules/S/Sitecore_Instance_Manager.aspx))
3.  Copy all assemblies from the Sitecore bin folder in your clean install (C:\Websites\Habitat\Website\bin), to /lib/Sitecore folder in the source repository (C:\Projects\Habitat\lib\Sitecore)
4.  Rebuild the solution in Visual Studio 2015
5.  Open the Visual Studio 2015 Task Runner Explorer (View | Other Windows | Task Runner Explorer)
6.  Run the Publish task
7.  Open [/unicorn.aspx](http://habitat/unicorn.aspx) and synchronize the items to your project
8.  Be productive!

###Gulp

The project is configured to run Gulp through the taskrunner in Visual Studio 2015. 

* The **Publish** task will publish all projects to the website location (see configuration below)
* The **Auto-Publish-Design** automatically publishes .css files when changed (Configure Sass compilation in Visual Studio)

###Configuring your settings

To change these settings modify the following files:

* /Project/Habitat.Website/App_Config/Include/z.Habitat.DevSettings.config.sample  
* /Solution Items/gulp.config.js  
* /Solution Items/publishsettings.targets  

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

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

##Getting started
---------------

[Watch a Getting Started video here (but please read below as configuration might have changed)](https://youtu.be/SIh4bLGTaLE)

> **Please note** that the project assumes the following settings:
> 
> *Source location:* C:\projects\Habitat\  
> *Website location:* C:\websites\Habitat.local\  
> *Website URL:* http://habitat.local/
>
> To change these settings see the "Configuring your settings" below


###Habitat uses the following:

* Sitecore XP 8.1 
* Visual Studio **2015**
* ASP.NET MVC
* NuGet
* Web Essentials
* Sitecore Rocks
* [Unicorn 3](https://github.com/kamsar/Unicorn)
* Gulp [gulpjs.com](http://gulpjs.com/)
* Sass [sass-lang.com](http://sass-lang.com/install)
* Node (npm) [npmjs.com](https://www.npmjs.com/)

###To install:

1.  Clone this repository to your local file system.
2.  Set up a clean Sitecore 8.1 website in the URL http://habitat.local/ and the location C:\Websites\Habitat.local\ (We recommend using [Sitecore Instance Manager](https://marketplace.sitecore.net/Modules/S/Sitecore_Instance_Manager.aspx))
3.  Open the solution in Visual Studio
4.  Open the Visual Studio 2015 Task Runner Explorer (View | Other Windows | Task Runner Explorer)
5.  Run the Copy-Sitecore-Lib task
6.  Rebuild the solution in Visual Studio 2015
7.  Run the Publish-Framework-Projects task
8.  Run the Publish-Domain-Projects task
9.  Run the Publish-Project-Projects task
10.  Open [/unicorn.aspx](http://habitat/unicorn.aspx) and synchronize the items to your project. **Start with the Habitat configuration**
11.  Be productive!

###Gulp

The project is configured to run Gulp through the taskrunner in Visual Studio 2015. 

* The **Publish** tasks will publish all projects to the website location (see configuration below)
* The **Auto-Publish-Design** automatically publishes .css files when changed (Configure Sass compilation in Visual Studio)

###Configuring your settings

To change these settings modify the following files:

* /Solution Items/z.Habitat.DevSettings.config
* /Solution Items/gulp-config.js  
* /Solution Items/publishsettings.targets  

##Modules
---------------
The solution consists of the following modules:

###Project modules

* [Website](src/Project/Website/)  
* [Design](src/Project/Design/)

###Domain modules

* [Accounts](src/Domain/Accounts/)
* [Identity](src/Domain/Identity/)
* [Language](src/Domain/Language/)
* [Media](src/Domain/Media/)
* [Metadata](src/Domain/Metadata/)
* [Navigation](src/Domain/Navigation/)
* [News](src/Domain/News/)
* [Search](src/Domain/Search/)
* [Social](src/Domain/Social/)
* [PageContent](src/Domain/PageContent/)

###Framework modules

* [Assets](src/Framework/Assets/)
* [Indexing](src/Framework/Indexing/)
* [Serialization](src/Framework/Serialization/)
* [SitecoreExtensions](src/Framework/SitecoreExtensions/)
* [Taxonomy](src/Framework/Taxonomy/)

##Resources
---------
-   [Sitecore Instance Manager](https://marketplace.sitecore.net/modules/sitecore_instance_manager.aspx)
-   Bug-tracking: [GitHub](https://github.com/Sitecore/Habitat/issues)
-   Build server:
-   Code quality:
-   Builds:

##How can I contribute?
---------------------

Contact [Thomas Eldblom](mailto:the@sitecore.net)

###For contributors
----------------

1. Use [meaningful names](http://blog.goyello.com/2013/05/17/express-names-in-code-bad-vs-clean/)
2. Write [clean code](http://www.amazon.com/Clean-Code-Handbook-Software-Craftsmanship/dp/0132350882)
3. Be a [boy scout](http://deviq.com/boy-scout-rule/)

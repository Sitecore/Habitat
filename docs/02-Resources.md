# Tools and Resources

These are the tools and practices currently used by the development team in Habitat.

You must install these to get Habitat running on your computer:

* [PowerShell version 5 or later](https://www.microsoft.com/en-us/download/details.aspx?id=50395)
* [Visual Studio 2017](https://www.visualstudio.com/downloads/) and .NET 4.6.
  * Within VS2017, install these extensions:
  * [Web Compiler](https://marketplace.visualstudio.com/items?itemName=MadsKristensen.WebCompiler)
* ASP.NET MVC for the user interface patterns
  * On Windows 10, this is enabled by going to:
    * **Turn Windows Features On or Off**
    * Select **ASP.NET 4.7** from **Internet Information Services** > **World Wide Web Services** > **Application Development Features**
* [Node.JS 4+](https://nodejs.org/) NOTE: as of August 7, 2017, Node.JS 8.2.1 does not work. Use 6.11.2 instead.

These are additional tools used in Habitat's creation. You do not need to install these just to run Habitat:

* Css Extension Language: [Sass](http://sass-lang.com/install)
* Front end framework: [Bootstrap 3](http://getbootstrap.com/)
* Package management: NuGet, [Node (npm)](https://nodejs.org/) and [Bower](https://www.bower.io/)
* Build scripts: [Gulp](http://gulpjs.com/)
* Item serialisation: [Unicorn 4](https://github.com/kamsar/Unicorn)
  * Windows PowerShell 4.0+ required to sync Unicorn via Gulp task
* Bug-tracking: [GitHub](https://github.com/Sitecore/Habitat/issues)
* CI server: TeamCity
* Unit tests: xUnit
* Specification Testing: SpecFlow
* Environment: [Visual C++ Redistributable Packages for Visual Studio 2013](https://www.microsoft.com/en-us/download/details.aspx?id=40784) (required only for Foundation.Installer module)

Sitecore does not endorse any particular third-party tool. The tools in the current release were chosen because of a number of reasons including popularity and ease of licensing. This does not preclude using other tools in Habitat or changing any third-party tool for another depending on feedback from the Sitecore community.

The project uses the Bootstrap theme implemented in [Sitecore.Demo.Theme](https://github.com/Sitecore/Sitecore.Demo.Theme)

## Alternates

### Team Development for Sitecore

A separate fork of the Habitat project with support for [Team Development for Sitecore (TDS)](https://www.teamdevelopmentforsitecore.com) is maintained by Hedgehog Development. [The Habitat TDS branch can be found here](https://github.com/HedgehogDevelopment/Habitat/tree/TDS)

### ORM

If you are interested in using an ORM tool like Glass or Synthesis with Habitat, please refer to these examples:
https://github.com/kamsar/Habitat/tree/HabitatSynthesis
https://github.com/muso31/Habitat-Glass.Mapper
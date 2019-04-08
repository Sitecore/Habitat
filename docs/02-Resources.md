# Tools and Resources

These are the tools and practices currently used by the development team in Habitat.

You must install these before installing Habitat:

* [PowerShell version 5 or later](https://www.microsoft.com/en-us/download/details.aspx?id=50395)
* [.NET Framework 4.7.1 Developer Pack](https://www.microsoft.com/en-us/download/details.aspx?id=56119)
* [Visual Studio 2017](https://www.visualstudio.com/downloads/)
  * **Be sure you are using VS17 v15.5 or higher in order to get necessary updates to MSBuild. Update your IDE if needed.**
  * Within VS2017, install these extensions:
    * [Web Compiler](https://marketplace.visualstudio.com/items?itemName=MadsKristensen.WebCompiler)
* ASP.NET MVC for the user interface patterns
  * On Windows 10, this is enabled by going to:
    * **Turn Windows Features On or Off**
    * Select **ASP.NET 4.7** from **Internet Information Services** > **World Wide Web Services** > **Application Development Features**
* [Node.JS 4+](https://nodejs.org/). Please use the latest LTS version of Node (as of publishing, this is 8.11.3)

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

Sitecore does not endorse any particular third-party tool. The tools in the current release were chosen because of a number of reasons, including popularity and ease of licensing. Habitat is an example to show you how you can use these tools to accomplish a feature complete and functional solution. This does not preclude using other tools in your Helix solutions, or changing any third-party tool for another depending on feedback from the Sitecore community.

The project uses the Bootstrap theme implemented in [Sitecore.Demo.Theme](https://github.com/Sitecore/Sitecore.Demo.Theme)

## Habitat forks using other tools

### Team Development for Sitecore

A separate fork of the Habitat project with support for [Team Development for Sitecore (TDS)](https://www.teamdevelopmentforsitecore.com) is maintained by Hedgehog Development. [The Habitat TDS branch can be found here](https://github.com/HedgehogDevelopment/Habitat/tree/TDS)

### ORM

If you are interested in using an ORM tool like Glass or Synthesis with Habitat, please refer to these examples:
* https://github.com/kamsar/Habitat/tree/HabitatSynthesis
* https://github.com/muso31/Habitat-Glass.Mapper

## SitecoreHabitatTheme
Design and frontend for [Sitecore Habitat](https://github.com/Sitecore/Habitat) Theme

### Development precursor
The project utilizes the following list of technologies to output sane stylesheets, markup and scripts. The list also entails tools used in the build process.

 - [Node.js + npm](https://nodejs.org/en/) - the base platform for running the gulp file and handling environment package management, which makes it the foundation for the development environment
 - [Sass](http://sass-lang.com/) - styling
 - [Nunjucks](https://mozilla.github.io/nunjucks/) - markup
 - [Gulp](http://gulpjs.com/) - build, serve and watch tasks
 - [Babel](https://babeljs.io/) - enabling ES2015 features in the gulp file
 - [Bower](http://bower.io/) - front-end dependency and package management

### Theme structure and guidelines
The theme structure presented in the project is based on the idea that each theme should have a master SASS file that contains no code, but references components and other files in a directory named the same as the master file, e.g. default.scss imports files from a folder called default, ocean.scss imports files from a folder called ocean and so on.

Inside each theme folder should be a folder called components, with subfolders named in relation to the Atomic design guidelines, i.e. atoms, molecules, organisms, templates and pages. This structure should mirror the structure given in this projects app/layouts/components folder.

Sometimes things doesn't fit into this component structure, and/or you want to define variables, global styling (styling of elements on a purely tag based level, e.g. body, section, and should be kept very abstract and reusable), custom fonts, mixins and so on. For each of these scenarios, a file named appropriately should be created in the root of the theme folder, e.g. default/_global.scss, default/_variables.scss, default/_fonts.scss and default/_mixins.scss. These files should then be imported in the master scss file at an appropriate time (i.e. in a proper specificity and inheritance structure). The fonts file should be loaded before you start using it, the same with variables and mixins and global should at least be loaded before you start importing the custom styles for the individual modules. For an example, see default.scss.

##### Defaults
For ease of use, a defaults.scss file has been set up which encapsulates all custom variables defined on top of the standard bootstrap variables. It is advised that a new theme imports these defaults and defines new variables or overrides accordingly.

#### Sub-variations and sub-themes
##### Amendments and extra files
As a general rule, all styling should be attempted to be contained in its relevant component file, and if amendments are to be made, they should be changed directly in the components code, as any theming changes constitutes a direct change to the theme, and as such, it is per definition a new theme (Note: updating a theme to include new styling doesn't make it a new theme, but only if the changes are made as a contribution to the original theme, not as an alteration). Any new sub-variations of an element should either override existing bootstrap styling, or should be appended to the existing component file as a new class, or sub-variation of an existing class. Creating new files such as _buttons_alt.scss is not recommended unless the original _buttons.scss file has enough additions that it becomes unmanageable.

##### Sub-theming
Instead of creating new files inside of the predefined structure, a sub-theme, with the same structure, should be created. A sub-theme should only be created if it adds a globally different theming to aspects of the theme it is attempting to enhance. It should NOT be created to facilitate individual component styling that has no common design guideline.

###### Naming of sub-theme
The name of the sub-theme should accurately describe which parent theme it is adding style to. A sub-theme follows the same structural guidelines as an ordinary theme, with the excerption that both the folder, and the master scss file should be prefixed with the parent themes name, e.g. if you were to make a sub-theme for a theme called ocean, you could call it ocean-deepsea and thus have a ocean-deepsea scss file and folder, e.g.:
```
+-- ocean.scss
+-- ocean-deepsea.scss
+-- ocean
|   +-- _variables.scss
|   +-- components
|   |   +-- atoms
|   |   |   +-- _buttons.scss
+-- ocean-deepsea
|   +-- _variables.scss
|   +-- components
|   |   +-- atoms
|   |   |   +-- _buttons.scss
```

##### Individual sub-variation
If a sub-theme/sub-variation with a distinct individual style for specific elements are needed, and these are not amendments to the existing theme or are too specific/individual to fit inside a sub-theme, e.g. additional states, color schemes or sizing needs (e.g. margin/padding) or overrides, it should be specified either inside a <style></style> tag on header, or a separate CSS file, possibly named custom.css or customizations.css, that is loaded, e.g. in the head of the document:

```html
<link rel="stylesheet" href="theme.css">
<link rel="stylesheet" href="subtheme.css">
<link rel="stylesheet" href="customizations.css">
<style>.btn-primary {background-color: red; }</style>
```

##### Coding style for individual components
Each individual component should define its own variables, import its own related component from bootstrap, and apply any custom styling underneath the import statement. Step by step, it looks like this:
- Define variables
- Import component
- Define any custom styling

This code structure ensures that components are as isolated and modularized as possible, and prevents the usual clutter that having a global _variables.scss file usually introduces. This is not to say that a global _variables.scss file, located inside the theme folder, should not be utilized, but it should NOT contain any variables related to components defined inside the existing structure. Instead, it should define global variables, such as grid padding, base colors, and font styling (Note: font styling may also be moved to a separate _fonts.scss file, if needs arise in relation to custom fonts, however be aware that _fonts.scss should in that case be imported directly after the variables file to ensure stability).

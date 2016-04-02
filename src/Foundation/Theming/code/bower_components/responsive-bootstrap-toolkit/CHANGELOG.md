### Changelog

**2.5.1**
Delaying the injection of visibility div container into `body` until `$(document).ready()`, and thus allowing the inclusion of library inside `<head>` section.

**2.5.0**
Introduced `use` method allowing to use custom visibility classes. Added built-in Foundation 5 support.

More changes:
* Changing mechanism initializing the library
* Re-organizing the demos
* Removing SASS-related part of the project

**2.4.2**
Refactoring 'changed' method (and updating usage examples in main.js) to solve issue [#14](https://github.com/maciej-gurban/responsive-bootstrap-toolkit/issues/14).

Delaying the injection of visibility divs into `<body>` until `$(document).ready()`, and thus allowing the inclusion of library inside `<head>` section.

**2.4.1**

Updating Bootstrap visibility classes for future compliancy, updating documentation to reflect changes in version 2.4.0, and small code refactoring.

**2.4.0**

Introducing comparison operators in the form of `viewport.is(">md").

**2.3.0**

Removing the requirement to insert visibility divs into the document.

**2.2.0**

Introducing `current` method returning breakpoint alias, and `breakpoints` property allowing to specify your own breakpoint names.

**2.1.0**

Introducing `set` SASS mixin, making it easier to set different CSS property values per breakpoint.

**2.0.0**

Version 2.0.0 introduces internal method and property name changes. Using this version without making appropriate changes to your scripts will break them. Please proceed with caution.

| old name            | new name |
| ------------------- | -------- |
| method `isBreakpoint`        | `is`     |
| method `waitForFinalEvent`   | `changed`|
| property `clock`      | `interval`|
| property `timeString` | `timer`|

For your convenience, version 1.5.0 of Responsive Bootstrap Toolkit is still kept inside this repository. You can find it at https://github.com/maciej-gurban/responsive-bootstrap-toolkit/blob/master/js/bootstrap-toolkit-1.5.0.js

**1.5.0**

Name-spacing functionalities, code improvements.

**1.0.0**

Initial realease containing bare JavaScript functions.

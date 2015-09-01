# gulp-msbuild
[![Build Status][travis-image]][travis-url]  [![Coverage Status][coveralls-image]][coveralls-url] [![Dependency Status][depstat-image]][depstat-url]

[![NPM version](https://nodei.co/npm/gulp-msbuild.png)](https://www.npmjs.org/package/gulp-msbuild)

> msbuild plugin for [gulp](https://github.com/wearefractal/gulp).
> Inspired by [grunt-msbuild](https://github.com/stevewillcock/grunt-msbuild)

## Usage

First, install `gulp-msbuild` as a development dependency:

```shell
npm install --save-dev gulp-msbuild
```

Then, add it to your `gulpfile.js`:

```javascript
var gulp = require("gulp");
var msbuild = require("gulp-msbuild");

gulp.task("default", function() {
	return gulp.src("./project.sln")
		.pipe(msbuild());
});
```

### Options

__Example__

```javascript
var gulp = require("gulp");
var msbuild = require("gulp-msbuild");

gulp.task("default", function() {
	return gulp.src("./project.sln")
		.pipe(msbuild({
			targets: ['Clean', 'Build'],
			toolsVersion: 3.5
			})
		);
});
```

#### stdout

> Show output of msbuild

**Default:** false

#### stderr

> Show errors of msbuild

**Default:** true

#### errorOnFail

> If the MSBuild job fails with an error, this will cause the gulp-msbuild stream to return an error thus causing the gulp task to fail. This is useful if using an automated build server such as [Jenkins](http://jenkins-ci.org/) where a failing MSBuild should also cause the overall build job to fail.

**Default:** false

#### logCommand

> Logs the msbuild command that will be executed.

**Default:** false

#### maxBuffer

> Specifies the largest amount of data allowed on stdout or stderr - if this value is exceeded then the msbuild child process is killed.

**Default:** 500*1024

#### targets

> Specify Build Targets

**Default:**
```javascript
['Rebuild']
```

#### configuration

> Specify Build Configuration (Release or Debug)

**Default:** Release

**Hint:** You can also specify the Build Configuration using the *properties* option
```json
properties: { Configuration: 'Debug' }
```

#### toolsVersion

> Specify the .NET Tools-Version

**Default:** 4.0

**Possible Values:** 1.0, 1.1, 2.0, 3.5, 4.0, 12.0, 14.0

#### architecture

> Specify the Architecture

**Default:** Auto-detected

**Possible Values:** x86, x64

**Example:**
```javascript
msbuild({ architecture: 'x86' })
```

#### properties

> Specify Custom Build Properties

**Default:** none

**Example:**
```javascript
msbuild({ properties: { WarningLevel: 2 } })
```

#### verbosity

> Specify the Build Verbosity

**Default:** normal

**Possible Values:** quiet, minimal, normal, detailed, diagnostic

#### maxcpucount

> Specify Maximal CPU-Count to use

**Default:** 0 = Automatic selection

**Possible Values:** -1 (MSBuild Default), 0 (Automatic), > 0 (Concrete value)

#### nologo

> Suppress Startup Banner and Copyright Message of MSBuild

**Default:** false

#### fileLoggerParameters

> Specify the parameters for the MSBuild File Logger.

**Default:** None

**Example:**
```javascript
msbuild({ fileLoggerParameters: 'LogFile=Build.log;Append;Verbosity=diagnostic' })
```

#### consoleLoggerParameters

> Specify the parameters for the MSBuild Console Logger. (See fileLoggerParameters for a usage example)

**Default:** None

### MSBuild Command-Line Reference

For a more detailed description of each MSBuild Option see the [MSBuild Command-Line Reference](http://msdn.microsoft.com/en-us/library/ms164311.aspx)

## License

[MIT License](http://en.wikipedia.org/wiki/MIT_License)

[travis-url]: http://travis-ci.org/hoffi/gulp-msbuild
[travis-image]: https://secure.travis-ci.org/hoffi/gulp-msbuild.png?branch=master

[coveralls-url]: https://coveralls.io/r/hoffi/gulp-msbuild?branch=master
[coveralls-image]: https://img.shields.io/coveralls/hoffi/gulp-msbuild.svg

[depstat-url]: https://david-dm.org/hoffi/gulp-msbuild
[depstat-image]: https://david-dm.org/hoffi/gulp-msbuild.png

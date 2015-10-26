# gulp-debug [![Build Status](https://travis-ci.org/sindresorhus/gulp-debug.svg?branch=master)](https://travis-ci.org/sindresorhus/gulp-debug)

> Debug [vinyl](https://github.com/wearefractal/vinyl) file streams to see what files are run through your gulp pipeline

![](screenshot.png)


## Install

```
$ npm install --save-dev gulp-debug
```


## Usage

```js
var gulp = require('gulp');
var debug = require('gulp-debug');

gulp.task('default', function () {
	return gulp.src('foo.js')
		.pipe(debug({title: 'unicorn:'}))
		.pipe(gulp.dest('dist'));
});
```


## API

### debug(options)

#### options

##### title

Type: `string`  
Default: `'gulp-debug:'`

Give it a custom title so it's possible to distinguish the output of multiple instances logging at once.

##### minimal

Type: `boolean`  
Default: `true`

By default only relative paths are shown. Turn off minimal mode to also show `cwd`, `base`, `path`.

The [`stat` property](http://nodejs.org/api/fs.html#fs_class_fs_stats) will be shown when you run gulp in verbose mode: `gulp --verbose`.


## License

MIT © [Sindre Sorhus](http://sindresorhus.com)

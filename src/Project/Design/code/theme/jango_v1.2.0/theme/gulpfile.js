'use strict';

var gulp = require('gulp'),
	sass = require('gulp-sass');

gulp.task('sass', function () {
	gulp.src('./sass/**/*.scss')
		.pipe(sass())
		.pipe(gulp.dest('./assets/base/css/'));
});

gulp.task('sass:watch', function () {
	gulp.watch('./sass/**/*.scss', ['sass']);
});
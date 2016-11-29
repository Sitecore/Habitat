// generated on 2015-12-14 using generator-gulp-webapp 1.0.3
import gulp from 'gulp';
import gulpLoadPlugins from 'gulp-load-plugins';
import browserSync from 'browser-sync';
import del from 'del';
import {stream as wiredep} from 'wiredep';
import fs from 'fs';
import path from 'path';
import nodeSass from 'node-sass';
import glob from 'glob';

const $ = gulpLoadPlugins({
  rename: {
    "gulp-css-connoisseur": "connoisseur",
    "gulp-sass-glob": "sassGlob"
  }
});
const reload = browserSync.reload;

gulp.task('styles', () => {
  return gulp.src('sass/*.scss')
    .pipe($.plumber())
    .pipe($.sassGlob())
    .pipe($.sourcemaps.init())
    .pipe($.sass.sync({
      outputStyle: 'expanded',
      precision: 10,
      includePaths: ['.']
    }).on('error', $.sass.logError))
    .pipe($.autoprefixer({browsers: ['ie 8', 'ie 9', '> 1%', 'last 2 versions', 'Firefox ESR']}))
    .pipe($.sourcemaps.write())
    .pipe(gulp.dest('.tmp/styles'))
    .pipe(browserSync.stream())
    ;
});

const sassOptionsDefault = {
  includePaths: ['.'],
  outputStyle: 'expanded',
  precision: 10
}

const sassVariable = (name, value) => {
  return name + ': ' + value + ';';
}

const sassVariables = (variablesObj) => {
  return Object.keys(variablesObj).map(name => {
    return sassVariable(name, variablesObj[name]);
  }).join('\n');
}
const sassImport = (path) => {
  return "@import '" + path +"';"
}

const dynamicSass = (scssEntry, variables, handleSuccess, handleError) => {
  const dataString =
    sassVariables(variables) +
    sassImport(scssEntry);
  const sassOptions = Object.assign({}, sassOptionsDefault, {
    data: dataString
  });

  nodeSass.render(sassOptions, (err, res) => {
    return (err) ? handleError(err) : handleSuccess(res.css.toString());
  });
}


function lint(files, options) {
  return () => {
    return gulp.src(files)
      .pipe(reload({stream: true, once: true}))
      .pipe($.eslint(options))
      .pipe($.eslint.format())
      .pipe($.if(!browserSync.active, $.eslint.failAfterError()));
  };
}

const testLintOptions = {
  env: {
    mocha: true
  }
};

gulp.task('lint', lint('scripts/**/*.js'));

gulp.task('html', ['views', 'styles'], () => {
  return gulp.src(['demo.src/*.html', '.tmp/*.html'])
    .pipe($.eol())
    .pipe($.useref({
      searchPath: ['demo.src', '.tmp', '.']
     }))
    .pipe($.uniquePath())
    .pipe($.if(/\.js$/, $.uglify()))
    .pipe($.if(/\.css$/, $.cleanCss()))
    .pipe($.if(/\.html$/, $.htmlmin({collapseWhitespace: true, conservativeCollapse: true, preserveLineBreaks: true})))
    .pipe(gulp.dest('demo'))
    .pipe(gulp.dest('.tmp'));
});

gulp.task('images', () => {
  return gulp.src('demo.src/images/**/*')
    .pipe($.if($.if.isFile, $.cache($.imagemin({
      progressive: true,
      interlaced: true,
      // don't remove IDs from SVGs, they are often used
      // as hooks for embedding and styling
      svgoPlugins: [{cleanupIDs: false}]
    }))
    .on('error', function (err) {
      console.log(err);
      this.end();
    })))
    .pipe(gulp.dest('demo/images'));
});

gulp.task('views', () => {
  $.nunjucksRender.nunjucks.configure(['demo.src/'], {watch: true});

  var componentsJson = JSON.parse(fs.readFileSync(__dirname + '/demo.src/components.json'));
  var templatesJson = JSON.parse(fs.readFileSync(__dirname + '/demo.src/templates.json'));

  return gulp.src(['demo.src/*.html', 'demo.src/layouts/pages/*.html', 'demo.src/layouts/templates/*.html'])
    .pipe($.plumber())
    .pipe($.nunjucksRender({
      componentsToLoad: componentsJson,
      templatesToLoad: templatesJson
    }))
    .pipe(gulp.dest('.tmp'))
});

gulp.task('fonts', () => {
  return gulp.src(require('main-bower-files')('**/*.{eot,svg,ttf,woff,woff2}', function (err) {})
    .concat('fonts/**/*'))
    .pipe(gulp.dest('.tmp/fonts'))
    .pipe(gulp.dest('demo/fonts'))
    ;

});

gulp.task('extras', () => {
  return gulp.src([
    'demo.src/*.*',
    '!demo.src/*.html',
    '!demo.src/*.json'
  ], {
    dot: true
  })
  .pipe(gulp.dest('demo'));
});

gulp.task('scripts', () => {
  return gulp.src([
    'scripts/*.*'
  ], {
    dot: true
  })
  .pipe(gulp.dest('demo/scripts')).pipe(gulp.dest('.tmp/scripts'));;
});

gulp.task('clean', del.bind(null, ['.tmp', 'demo']));

gulp.task('serve', ['views', 'styles', 'fonts'], () => {
  browserSync({
    notify: false,
    port: 9000,
    server: {
      baseDir: ['.tmp', 'demo.src'],
      routes: {
        '/bower_components': 'bower_components'
      }
    }
  });

  gulp.watch([
    '.tmp/*.html',
    'scripts/**/*.js',
    'demo.src/images/**/*',
    '.tmp/fonts/**/*'
  ]).on('change', reload);

  gulp.watch('demo.src/components.json', ['views', reload]);
  gulp.watch('demo.src/templates.json', ['views', reload]);
  gulp.watch('demo.src/**/*.html', ['views', reload]);
  gulp.watch('demo.src/**/*.nj', ['views', reload]);
  gulp.watch('sass/**/*.scss', ['styles', reload]);
  gulp.watch('fonts/**/*', ['fonts']);
  gulp.watch('bower.json', ['wiredep', 'fonts']);
});

gulp.task('serve:demo', () => {
  browserSync({
    notify: false,
    port: 9000,
    server: {
      baseDir: ['demo']
    }
  });
});

// inject bower components
gulp.task('wiredep', () => {
  gulp.src('sass/*.scss')
    .pipe($.clipEmptyFiles())
    .pipe(wiredep({
      ignorePath: /^(\.\.\/)+/
    }))
    .pipe(gulp.dest('sass'));

  gulp.src('demo.src/layouts/*.html')
    .pipe($.clipEmptyFiles())
    .pipe(wiredep({
      exclude: ['jquery', 'bootstrap-sass', 'bootstrap'],
      ignorePath: /^(\.\.\/)*\.\./
    }))
    .pipe(gulp.dest('demo.src/layouts'));
});

gulp.task('build', ['views', 'html', 'images', 'fonts', 'scripts', 'extras'], () => {
  return gulp.src('demo/**/*').pipe($.size({title: 'build', gzip: true}));
});

gulp.task('default', ['clean'], () => {
  return gulp.start('build');
});

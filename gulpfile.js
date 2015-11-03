/// <binding AfterBuild='Publish-Assemblies' ProjectOpened='Auto-Publish-Css, Auto-Publish-Views' />
var gulp = require("gulp");
var msbuild = require("gulp-msbuild");
var debug = require("gulp-debug");
var foreach = require("gulp-foreach");
var rename = require("gulp-rename");
var watch = require("gulp-watch");
var newer = require("gulp-newer");
var util = require("gulp-util");
var runSequence = require('run-sequence');
var fs = require("fs");
var path = require("path");
var config = require("./gulp-config.js")();
var items = [];
var websiteRootBackup = config.websiteRoot;

var publishProjects = function (location, dest) {
  dest = dest || config.websiteRoot;
  console.log("publish to " + dest + " folder");
  return gulp.src([location + "/**/*.csproj", "!" + location + "/**/*Tests.csproj"])
        .pipe(foreach(function (stream, file) {
          return stream
              .pipe(debug({ title: "Building project:" }))
              .pipe(msbuild({
                targets: ["Clean", "Build"],
                configuration: "Debug",
                logCommand: false,
                verbosity: "minimal",
                maxcpucount: 0,
                toolsVersion: 14.0,
                properties: {
                  DeployOnBuild: "true", DeployDefaultTarget: "WebPublish", WebPublishMethod: "FileSystem", DeleteExistingFiles: "false", publishUrl: dest
                }
              }));
        }));
};

var deleteFolderRecursive = function (path) {
  if (fs.existsSync(path)) {
    fs.readdirSync(path).forEach(function (file, index) {
      var curPath = path + "/" + file;
      if (fs.lstatSync(curPath).isDirectory()) { // recurse
        deleteFolderRecursive(curPath);
      } else { // delete file
        fs.unlinkSync(curPath);
      }
    });
    fs.rmdirSync(path);
  }
};

gulp.task("01-Copy-Sitecore-Lib", function () {
  console.log("Copying Sitecore Libraries");
  gulp.src(config.sitecoreLibraries + "/**/*")
      .pipe(gulp.dest("./lib/Sitecore"));
  console.log("Finished copying sitecore libraries", "color:red");
});

gulp.task("02-Publish-Framework-Projects", function () {
  return publishProjects("./src/Framework");
});

gulp.task("03-Publish-Domain-Projects", function () {
  return publishProjects("./src/Domain");
});

gulp.task("04-Publish-Project-Projects", function () {
  return publishProjects("./src/Project");
});

gulp.task("Auto-Publish-Css", function () {
  var root = "./src/project/design";
  return gulp.watch(root + "/**/*.css", function (event) {
    if (event.type === "changed") {
      console.log("publish this file " + event.path);
      gulp.src(event.path, { base: root }).pipe(gulp.dest(config.websiteRoot));
    }
    console.log("published " + event.path);
  });
});

gulp.task("Auto-Publish-Views", function () {
  var root = "./src";
  var roots = [root + "/**/Views", "!" + root + "/**/obj/**/Views"];
  var files = "/**/*.cshtml";
  var destination = config.websiteRoot + "\\Views";
  gulp.src(roots, { base: root }).pipe(
      foreach(function (stream, rootFolder) {
        gulp.watch(rootFolder.path + files, function (event) {
          if (event.type === "changed") {
            console.log("publish this file " + event.path);
            gulp.src(event.path, { base: rootFolder.path }).pipe(gulp.dest(destination));
          }
          console.log("published " + event.path);
        });
        return stream;
      })
  );
});

gulp.task("Publish-Assemblies", function () {
  var root = "./src";
  var binFiles = root + "/**/bin/Habitat.*.{dll,pdb}";
  var destination = config.websiteRoot + "/bin/";
  return gulp.src(binFiles, { base: root })
      .pipe(rename({ dirname: "" }))
      .pipe(newer(destination))
      .pipe(debug({ title: "Copying " }))
      .pipe(gulp.dest(destination));
});

gulp.task("Publish-All-Views", function () {
  var root = "./src";
  var roots = [root + "/**/Views", "!" + root + "/**/obj/**/Views"];
  var files = "/**/*.cshtml";
  var destination = config.websiteRoot + "\\Views";
  return gulp.src(roots, { base: root }).pipe(
      foreach(function (stream, file) {
        console.log("Publishing from " + file.path);
        gulp.src(file.path + files, { base: file.path })
            .pipe(newer(destination))
            .pipe(debug({ title: "Copying " }))
            .pipe(gulp.dest(destination));
        return stream;
      })
  );
});

//CI stuff
gulp.task("CI-Publish", function (callback) {

  items = [];
  config.websiteRoot = path.resolve("./temp");
  fs.mkdirSync(config.websiteRoot);
  runSequence(
    "02-Publish-Framework-Projects",
    "03-Publish-Domain-Projects",
    "04-Publish-Project-Projects", callback);
});


gulp.task("CI-Enumerate-Files", function () {

  config.websiteRoot = websiteRootBackup;
  return gulp.src(path.resolve("./temp") + "/**/*.*", { base: "temp" })
    .pipe(foreach(function (stream, file) {
      items.push(file.relative.replace(/\\/g,"//"));
      return stream;
    }));
});


gulp.task("CI-Update-Xml", function (cb) {
  var destination = path.resolve("./package.xml");

  //TODO: find a tool to modify XML instead of string replacement

  var str = "<Entries>";
  for (var idx in items) {
    str += "<x-item>" + items[idx] + "</x-item>";
  }
  str += "</Entries>";
 
  fs.readFile(destination, 'utf8', function (err, data) {
    if (err) {
      return console.log(err);
    }
    var result = data.replace("<Entries></Entries>", str);
    fs.writeFile(destination, result, 'utf8', cb);
  });
});

gulp.task("CI-Clean", function (callback) {
  deleteFolderRecursive(path.resolve("./temp"));
  callback();
});


gulp.task("CI-Do-magic", function (callback) {
  runSequence('CI-Clean', 'CI-Publish', 'CI-Enumerate-Files', 'CI-Clean', 'CI-Update-Xml', callback);
});

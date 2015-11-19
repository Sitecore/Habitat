var gulp = require("gulp");
var msbuild = require("gulp-msbuild");
var debug = require("gulp-debug");
var foreach = require("gulp-foreach");
var rename = require("gulp-rename");
var watch = require("gulp-watch");
var newer = require("gulp-newer");
var util = require("gulp-util");
var rimraf = require("gulp-rimraf");
var runSequence = require("run-sequence");
var fs = require("fs");
var path = require("path");
var config = require("./gulp-config.js")();
var websiteRootBackup = config.websiteRoot;

/*****************************
  Initial setup
*****************************/
gulp.task("01-Copy-Sitecore-Lib", function () {
  console.log("Copying Sitecore Libraries");
  gulp.src(config.sitecoreLibraries + "/**/*")
    .pipe(gulp.dest("./lib/Sitecore"));
  console.log("Finished copying sitecore libraries", "color:red");
});

gulp.task("02-Publish-All-Projects", function (callback) {
  runSequence(
    "Publish-Framework-Projects",
    "Publish-Domain-Projects",
    "Publish-Project-Projects", callback);
});

/*****************************
  Publish
*****************************/
var publishProjects = function (location, dest) {
  dest = dest || config.websiteRoot;
  console.log("publish to " + dest + " folder");
  return gulp.src([location + "/**/*.csproj", "!" + location + "/**/*Tests.csproj", "!" + location + "/**/*Specflow.csproj"])
    .pipe(foreach(function (stream, file) {
      return stream
        .pipe(debug({ title: "Building project:" }))
        .pipe(msbuild({
          targets: ["Clean", "Build"],
          configuration: config.buildConfiguration,
          logCommand: false,
          verbosity: "minimal",
          maxcpucount: 0,
          toolsVersion: 14.0,
          properties: {
            DeployOnBuild: "true",
            DeployDefaultTarget: "WebPublish",
            WebPublishMethod: "FileSystem",
            DeleteExistingFiles: "false",
            publishUrl: dest,
            _FindDependencies: "false"
          }
        }));
    }));
};

gulp.task("Apply-Xml-Transform", function () {
  return gulp.src("./src/Project/**/*Website.csproj")
    .pipe(foreach(function (stream, file) {
      return stream
        .pipe(debug({ title: "Applying transform project:" }))
        .pipe(msbuild({
          targets: ["ApplyTransform"],
          configuration: config.buildConfiguration,
          logCommand: true,
          verbosity: "normal",
          maxcpucount: 0,
          toolsVersion: 14.0,
          properties: {
            WebConfigToTransform: config.websiteRoot + "\\web.config"
          }
        }));
    }));

});

gulp.task("Publish-Framework-Projects", function () {
  return publishProjects("./src/Framework");
});

gulp.task("Publish-Domain-Projects", function () {
  return publishProjects("./src/Domain");
});

gulp.task("Publish-Project-Projects", function () {
  return publishProjects("./src/Project");
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

/*****************************
 Watchers
*****************************/
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

gulp.task("Auto-Publish-Assemblies", function () {
  var root = "./src";
  var roots = [root + "/**/code/bin"];
  var files = "/**/Habitat.*.{dll,pdb}";;
  var destination = config.websiteRoot + "/bin/";
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

/*****************************
 CI stuff
*****************************/
var packageFiles = [];
gulp.task("CI-Publish", function (callback) {
  packageFiles = [];
  config.websiteRoot = path.resolve("./temp");
  config.buildConfiguration = "Release";
  fs.mkdirSync(config.websiteRoot);
  runSequence(
    "Publish-Framework-Projects",
    "Publish-Domain-Projects",
    "Publish-Project-Projects", callback);
});

gulp.task("CI-Prepare-Package-Files", function (callback) {
  var foldersToExclude = [config.websiteRoot + "\\App_config\\include\\Unicorn"];
  foldersToExclude.forEach(function (item, index, array) {
    rimraf(config.websiteRoot + item);
  });

  var excludeList = [
    config.websiteRoot + "\\bin\\{Sitecore,Lucene,Newtonsoft,Unicorn,Kamsar,Rainbow,System,Microsoft.Web.Infrastructure}*dll",
    config.websiteRoot + "\\compilerconfig.json.defaults",
    config.websiteRoot + "\\packages.config",
    config.websiteRoot + "\\App_Config\\Include\\Rainbow*",
    config.websiteRoot + "\\App_Config\\Include\\Unicorn\\*",
    config.websiteRoot + "\\App_Config\\Include\\Habitat\\*Serialization.config",
    "!" + config.websiteRoot + "\\bin\\{Sitecore.Support}*dll"

  ];

  return gulp.src(excludeList, { read: false }).pipe(rimraf({ force: true }));
});

gulp.task("CI-Enumerate-Files", function () {
  config.websiteRoot = websiteRootBackup;

  return gulp.src(path.resolve("./temp") + "/**/*.*", { base: "temp", read: false })
    .pipe(foreach(function (stream, file) {
      var item = "/" + file.relative.replace(/\\/g, "/");
      console.log("Added to the package:" + item);
      packageFiles.push(item);
      return stream;
    }));
});


gulp.task("CI-Update-Xml", function (cb) {
  var destination = path.resolve("./package.xml");

  //TODO: find a tool to modify XML instead of string replacement

  var str = "<Entries>";
  for (var idx in packageFiles) {
    str += "<x-item>" + packageFiles[idx] + "</x-item>";
  }
  str += "</Entries>";

  fs.readFile(destination, "utf8", function (err, data) {
    if (err) {
      return console.log(err);
    }
    var result = data.replace("<Entries></Entries>", str);
    fs.writeFile(destination, result, "utf8", cb);
  });
});

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

gulp.task("CI-Clean", function (callback) {
  deleteFolderRecursive(path.resolve("./temp"));
  callback();
});

gulp.task("CI-Do-magic", function (callback) {
  runSequence("CI-Clean", "CI-Publish", "CI-Prepare-Package-Files", "CI-Enumerate-Files", "CI-Clean", "CI-Update-Xml", callback);
});
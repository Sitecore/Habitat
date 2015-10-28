/// <binding AfterBuild='Publish-Assemblies' ProjectOpened='Auto-Publish-Css, Auto-Publish-Views' />
var gulp = require("gulp");
var msbuild = require("gulp-msbuild");
var debug = require("gulp-debug");
var foreach = require("gulp-foreach");
var rename = require("gulp-rename");
var watch = require("gulp-watch");
var newer = require("gulp-newer");
var util = require("gulp-util");
var config = require("./gulp-config.js")();

var publishProjects = function(location) {
  gulp.src([location + "/**/*.csproj", '!'+location + "/**/*Tests.csproj"])
        .pipe(foreach(function (stream, file) {
            return stream
                .pipe(debug({title: "Building project:"}))
                .pipe(msbuild({
                targets: ["Clean", "Build"], 
                configuration: "Debug",
                logCommand: false,
                verbosity: "minimal",
                maxcpucount: 0,
                toolsVersion:14.0,
                properties: {
                   DeployOnBuild: "true", DeployDefaultTarget: "WebPublish", WebPublishMethod: "FileSystem", DeleteExistingFiles: "false", publishUrl: config.websiteRoot
                }
            }));
        }));
};

gulp.task("02-Publish-Framework-Projects", function () {
    publishProjects("./src/Framework");
});

gulp.task("03-Publish-Domain-Projects", function () {
    publishProjects("./src/Domain");
});

gulp.task("04-Publish-Project-Projects", function () {
    publishProjects("./src/Project");
});

gulp.task("01-Copy-Sitecore-Lib", function () {
    console.log("Copying Sitecore Libraries");
    gulp.src(config.sitecoreLibraries + "/**/*")
        .pipe(gulp.dest("./lib/Sitecore"));
    console.log("Finished copying sitecore libraries", "color:red");
});

gulp.task("Auto-Publish-Css", function() {
    var root = "./src/project/design";
    gulp.watch(root + "/**/*.css", function(event) {
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
    gulp.src(binFiles, { base: root })
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
    gulp.src(roots, { base: root }).pipe(
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

var publishSolution = function (location) {
  gulp.src(location + "*.sln")
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
                DeployOnBuild: "true", DeployDefaultTarget: "WebPublish", WebPublishMethod: "FileSystem", DeleteExistingFiles: "false", publishUrl: config.websiteRoot
              }
            }));
      }));
};

gulp.task("Publish-Solution", function () {
  publishSolution("./");
});
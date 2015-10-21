/// <binding />
var gulp = require("gulp");
var msbuild = require("gulp-msbuild");
var foreach = require("gulp-foreach");
var rename = require("gulp-rename");
var watch = require("gulp-watch");
var config = require("./gulp-config.js")();

var publishProjects = function(location) {
    gulp.src(location + "/**/*.csproj")
        .pipe(foreach(function (stream, file) {
            return stream.pipe(msbuild({
                targets: ["Clean", "Build"], 
                configuration: "Debug",
                logCommand: false,
                verbosity: "normal",
                properties: { DeployOnBuild: "true", DeployDefaultTarget: "WebPublish", WebPublishMethod: "FileSystem", DeleteExistingFiles: "false", publishUrl: config.websiteRoot }
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

gulp.task("Auto-Publish-Design", function() {
    var root = "./src/project/design";
    gulp.watch(root + "/**/*.css", function(event) {
        if (event.type === "changed") {
            console.log("publish this file " + event.path);
            gulp.src(event.path, { base: root }).pipe(gulp.dest(config.websiteRoot));
        }
        console.log("published " + event.path);
    });
});
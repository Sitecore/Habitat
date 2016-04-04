var gulp = require("gulp");
var foreach = require("gulp-foreach");
var rimrafDir = require("rimraf");
var rimraf = require("gulp-rimraf");
var runSequence = require("run-sequence");
var fs = require("fs");
var path = require("path");
var xmlpoke = require("xmlpoke");
var config = require("./gulpfile.js").config;
var websiteRootBackup = config.websiteRoot;

var packageFiles = [];
gulp.task("CI-Publish", function (callback) {
    packageFiles = [];
    config.websiteRoot = path.resolve("./temp");
    config.buildConfiguration = "Release";
    fs.mkdirSync(config.websiteRoot);
    runSequence(
      "Publish-Foundation-Projects",
      "Publish-Feature-Projects",
      "Publish-Project-Projects", callback);
});

gulp.task("CI-Prepare-Package-Files", function (callback) {
    var foldersToExclude = [config.websiteRoot + "\\App_config\\include\\Unicorn"];
    foldersToExclude.forEach(function (item, index, array) {
        rimrafDir.sync(config.websiteRoot + item);
    });

    var excludeList = [
      config.websiteRoot + "\\bin\\{Sitecore,Lucene,Newtonsoft,System,Microsoft.Web.Infrastructure}*dll",
      config.websiteRoot + "\\compilerconfig.json.defaults",
      config.websiteRoot + "\\packages.config",
      config.websiteRoot + "\\App_Config\\Include\\{Feature,Foundation,Project}\\*Serialization.config",
      "!" + config.websiteRoot + "\\bin\\Sitecore.Support*dll",
      "!" + config.websiteRoot + "\\bin\\Sitecore.{Feature,Foundation,Habitat,Demo,Common}*dll"
    ];
    console.log(excludeList);

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
    xmlpoke("./package.xml", function (xml) {
        for (var idx in packageFiles) {
            xml.add("project/Sources/xfiles/Entries/x-item", packageFiles[idx]);
        }
    });
    cb();
});

gulp.task("CI-Clean", function (callback) {
    rimrafDir.sync(path.resolve("./temp"));
    callback();
});

gulp.task("CI-Do-magic", function (callback) {
    runSequence("CI-Clean", "CI-Publish", "CI-Prepare-Package-Files", "CI-Enumerate-Files", "CI-Clean", "CI-Update-Xml", callback);
});

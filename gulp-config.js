module.exports = function () {
  var instanceRoot = "C:\\websites\\Habitat.dev.local";
  var config = {
    websiteRoot: instanceRoot + "\\Website",
    sitecoreLibraries: instanceRoot + "\\Website\\bin",
    licensePath: instanceRoot + "\\Data\\license.xml",
    solutionName: "Habitat",
    buildConfiguration: "Debug",
    buildPlatform: "AnyCpu",
    runCleanBuilds: false
  };
  return config;
}

"use strict";
var xml2js = require("xml2js");
var fs = require("fs");

var habitat = {};

habitat.getConfiguration = function getConfigFile(filename) {
  var data = fs.readFileSync(filename);

  var parser = new xml2js.Parser();
  var content;
  parser.parseString(data, function (err, result) {
    if (err !== null) throw err;
    content = result;
  });
  return content;
};

habitat.getSiteUrl = function getSiteUrl(options) {
  if (!options) options = {};

    var publishFile = options.publishingSettingsFile ? options.publishingSettingsFile : habitat.getPublishingSettingsFile();
  try {
    var configuration = habitat.getConfiguration(publishFile);
    return configuration.Project.PropertyGroup[0].publishUrl[0];
  } catch(error) {
    error.message = "Could not get the Habitat site URL from '" + publishFile + "'. Error:" + error.message;
    throw(error);
  }
};

habitat.getPublishingSettingsFile = function getPublishingSettingsFile() {
    if (fs.existsSync("./publishsettings.targets.user"))
        return "./publishsettings.targets.user";
    return "./publishsettings.targets";
}

module.exports = habitat;
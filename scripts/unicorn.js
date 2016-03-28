"use strict";
var exec = require("child_process").exec;
var xml2js = require("xml2js");
var glob = require("glob");
var async = require("async");
var fs = require("fs");

module.exports = function (callback, options) {
  var getUnicornSecret = function () {
    if (options.secret)
      return options.secret;    
    var unicornConfigFile = options.authenticationConfigFile;

    var data = fs.readFileSync(unicornConfigFile);

    var parser = new xml2js.Parser();
    var secret;
    parser.parseString(data, function (err, result) {
      if (err !== null) throw err;

      secret = result.configuration.sitecore[0].unicorn[0].authenticationProvider[0].SharedSecret[0];
    });
    return secret;
  }

  var getUnicornConfiguration = function (configFile) {
    var data = fs.readFileSync(configFile);

    var configuration;
    var parser = new xml2js.Parser();
    parser.parseString(data, function (err, result) {
      if (err !== null) throw err;

      var configurationNodes = result.configuration.sitecore[0].unicorn[0].configurations[0].configuration;
      for (var i = 0; i < configurationNodes.length; i++) {
        configuration = (configuration ? configuration + "^" : "") + configurationNodes[i].$.name;
      } 
    });
    return configuration;
  }

  var getUnicornConfigurations = function (filesGlob) {
    var configurations;
    
    var files = glob.sync(filesGlob);
    for (var i = 0; i < files.length; i++) {
      var file = files[i];
      var configuration = getUnicornConfiguration(file);
      if (configuration) {
        configurations = (configurations ? configurations + "^" : "") + configuration;
      }
    }
    return configurations;
  };

  var getOrderedUnicornConfigurations = function (filesGlobs) {
    if (options.configurations)
      return options.configurations;

    var allConfigurations;
    
    for (var i = 0; i < filesGlobs.length; i++) {
      var configurations = getUnicornConfigurations(filesGlobs[i]);
      if (configurations) {
        allConfigurations = (allConfigurations ? allConfigurations + "^" : "") + configurations;
      }
    }
    return allConfigurations;
  }

  var secret = getUnicornSecret();
  var url = options.siteHostName + "/unicorn.aspx";
  var configurations = getOrderedUnicornConfigurations(options.configurationConfigFiles);

  var syncScript = "./Sync.ps1 -secret " + secret + " -url " + url + " -configurations " + configurations;
  var options = { cwd: __dirname + "/Unicorn/" };
  return exec("powershell -executionpolicy unrestricted \"" + syncScript + "\"", options, function(err, stdout, stderr) {
    if (err !== null) throw err;
    console.log(stdout);
    callback();
  });
};

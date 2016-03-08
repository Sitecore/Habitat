"use strict";
var exec = require("child_process").exec;
var xml2js = require("xml2js");
var glob = require("glob");
var async = require("async");
var fs = require("fs");

module.exports = function (callback, options) {
  var getUnicornSecret = function (callback) {
    var unicornConfigFile = options.serializationSettingsConfig;
    return fs.readFile(unicornConfigFile, function (err, data) {
      if (err !== null) return callback(err);

      var parser = new xml2js.Parser();
      parser.parseString(data, function (err, result) {
        if (err !== null) return callback(err);

        var secret = result.configuration.sitecore[0].unicorn[0].authenticationProvider[0].SharedSecret[0];
        return callback(err, secret);
      });
    });
  }

  var getUnicornUrl = function (callback) {
    var publishFile = options.publishingSettingsFiles;
    return fs.readFile(publishFile, function (err, data) {
      if (err !== null) return callback(err);      

      var parser = new xml2js.Parser();
      parser.parseString(data, function (err, result) {
        if (err !== null) return callback(err);

        var url = result.Project.PropertyGroup[0].publishUrl[0];
        return callback(err, url += "/unicorn.aspx");
      });
    });
  }

  var getUnicornConfiguration = function (configFile, callback) {
    return fs.readFile(configFile, function (err, data) {
      if (err !== null) return callback(err);      

      var parser = new xml2js.Parser();
      parser.parseString(data, function (err, result) {
        if (err !== null) return callback(err);

        var configuration = result.configuration.sitecore[0].unicorn[0].configurations[0].configuration[0].$.name;
        return callback(err, configuration);
      });
    });
  }

  var getUnicornConfigurations = function (filesGlob, callback) {
    var configurations;
    
    glob(filesGlob, function(err, files) {
      async.each(
        files,
        function(file, cb) {
          getUnicornConfiguration(file, function(err, configuration) {
            if (err)
              return callback(err);
            if (configuration) {
              configurations = (configurations ? configurations + "^" : "") + configuration;
            }
            cb();
          });
        },
        function (err) {
          callback(null, configurations);
        }
      )}
    );
  }

  var getAllUnicornConfigurations = function (filesGlobs, callback) {
    var allConfigurations;
    
    async.eachSeries(
      filesGlobs,
      function(filesGlob, cb) {
        getUnicornConfigurations(filesGlob, function(err, configurations) {
          if (err)
            return callback(err);
          if (configurations) {
            allConfigurations = (allConfigurations ? allConfigurations + "^" : "") + configurations;
          }
          cb();
        });
      },
      function (err) {
        callback(null, allConfigurations);
      }      
    )
  }

  return getUnicornSecret(function (err, secret) {
    if (err !== null) throw err;

    return getUnicornUrl(function(err, url) {
      if (err !== null) throw err;

      return getAllUnicornConfigurations(
        options.serializationConfigFiles,
        function(err, configurations) 
        {
          if (err !== null) throw err;

          var syncScript = "./Sync.ps1 -secret " + secret + " -url " + url + " -configurations " + configurations;
          var options = { cwd: __dirname + "/Unicorn/" };
          return exec("powershell \"" + syncScript + "\"", options, function(err, stdout, stderr) {
            if (err !== null) throw err;
            console.log(stdout);
            callback();
          });
      });
    });
  });  
};

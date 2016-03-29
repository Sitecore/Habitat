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

  var secret = getUnicornSecret();
  var url = options.siteHostName + "/unicorn.aspx";

  var syncScript = "./Sync.ps1 -secret " + secret + " -url " + url;
  var options = { cwd: __dirname + "/Unicorn/" };
  return exec("powershell \"" + syncScript + "\"", options, function(err, stdout, stderr) {
    if (err !== null) throw err;
    console.log(stdout);
    callback();
  });
};

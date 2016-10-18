"use strict";
var exec = require("child_process").exec;
var xml2js = require("xml2js");
var glob = require("glob");
var async = require("async");
var fs = require("fs");

var unquote = function(str){
  return str.replace(/^"(.*)"$/,"$1");
}

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

  var syncScript =__dirname + "/Unicorn/./Sync.ps1 -secret " + secret + " -url " + url;
  var options = { cwd: __dirname + "/Unicorn/", maxBuffer: 1024 * 500 };
  return exec("powershell -executionpolicy unrestricted \"" + syncScript + "\"", options, function (err, stdout, stderr) {
    if (err !== null) throw err;
    console.log(stdout);
    callback();
  });
};

module.exports.getFullItemPath = function (itemFile) {
  var fileContent = itemFile.contents.toString();
  var path = unquote(fileContent.match(/Path:\s*(.*)$/m)[1]);
  var id = unquote(fileContent.match(/ID:\s*(.*)$/m)[1]);
  var dbMatch = fileContent.match(/DB:\s*(.*)$/m);
  if (dbMatch) {
    var db = dbMatch[1];
  } else {
    var db = "master";
  }
  return "/" + db + path + "/{" + id + "}/invariant/0"
}

module.exports.getUserPath = function (userFile) {
   var fileContent = userFile.contents.toString();
   var userName = fileContent.match(/username:\s*(.*)$/m)[1];
   return "users:"+userName;
}

module.exports.getRolePath = function (roleFile) {
   var fileContent = roleFile.contents.toString();
   var roleName = fileContent.match(/Role:\s*(.*)$/m)[1];
   return "roles:"+roleName;
}

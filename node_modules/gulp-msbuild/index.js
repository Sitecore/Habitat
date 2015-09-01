'use strict';

var through = require('through2'),
    _ = require('lodash'),
    constants = require('./lib/constants'),
    msbuildRunner = require('./lib/msbuild-runner');

function mergeOptionsWithDefaults(options) {
  return _.extend(constants.DEFAULTS, options);
}

module.exports = function(options) {
  var mergedOptions = _.cloneDeep(mergeOptionsWithDefaults(options));

  var stream = through.obj(function(file, enc, callback) {
    var self = this;
    if (file.isNull()) {
      self.push(file);
      return callback();
    }

    return msbuildRunner.startMsBuildTask(mergedOptions, file, function(err) {
      if (err) return callback(err);
      self.push(file);
      return callback();
    });
  });

  return stream;
};

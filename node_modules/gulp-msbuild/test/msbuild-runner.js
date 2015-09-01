/*global describe, it, beforeEach*/
'use strict';

var chai          = require('chai'),
    Stream        = require('stream'),
    childProcess  = require('child_process'),
    constants     = require('../lib/constants'),
    gutil         = require('gulp-util'),
    expect        = chai.expect;

chai.use(require('sinon-chai'));
require('mocha-sinon');

var commandBuilder = require('../lib/msbuild-command-builder');
var msbuildRunner = require('../lib/msbuild-runner');

var defaults;

var execCallbackArg;

describe('msbuild-runner', function () {

  beforeEach(function () {
    defaults = JSON.parse(JSON.stringify(constants.DEFAULTS));

    this.sinon.stub(childProcess, 'exec', function (command, options, callback) {
      process.nextTick(function() { callback(execCallbackArg); });

      var stream = new Stream();
      stream.pipe = function() {};

      return {
        stdout: stream,
        stderr: stream
      };
    });

    this.sinon.stub(commandBuilder, 'construct').returns('msbuild /nologo');
    this.sinon.stub(gutil, 'log');
  });

  it('should execute the msbuild command', function (done) {
    defaults.stdout = true;
    msbuildRunner.startMsBuildTask(defaults, {}, function () {
      expect(gutil.log).to.have.been.calledWith(gutil.colors.cyan('Build complete!'));
      done();
    });

    expect(childProcess.exec).to.have.been.calledWith('msbuild /nologo', { maxBuffer: defaults.maxBuffer });
  });

  it('should log the command when the logCommand option is set', function(done) {
    defaults.logCommand = true;
    msbuildRunner.startMsBuildTask(defaults, {}, function () {
      expect(gutil.log).to.have.been.calledWith('Using msbuild command: msbuild /nologo');
      done();
    });
  });

  it('should log the error when the msbuild command failed', function (done) {
    execCallbackArg = 'test';

    msbuildRunner.startMsBuildTask(defaults, {}, function () {
      expect(gutil.log).to.have.been.calledWith(execCallbackArg);
      expect(gutil.log).to.have.been.calledWith(gutil.colors.red('Build failed!'));
      done();
    });
  });

  it('should log the error and return the error in the callback when the msbuild command failed', function (done) {
    defaults.errorOnFail = true;
    execCallbackArg = 'test';

    msbuildRunner.startMsBuildTask(defaults, {}, function (err) {
      expect(err).to.be.equal(execCallbackArg);
      expect(gutil.log).to.have.been.calledWith(execCallbackArg);
      expect(gutil.log).to.have.been.calledWith(gutil.colors.red('Build failed!'));
      done();
    });
  });
});

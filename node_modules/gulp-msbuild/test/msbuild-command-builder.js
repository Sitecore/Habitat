/*global describe, it, beforeEach*/
'use strict';

var chai          = require('chai'),
    constants     = require('../lib/constants'),
    gutil         = require('gulp-util'),
    expect        = chai.expect;

chai.use(require('sinon-chai'));
require('mocha-sinon');

var commandBuilder = require('../lib/msbuild-command-builder');
var msbuildFinder = require('../lib/msbuild-finder');

var defaults;

describe('msbuild-command-builder', function () {

  beforeEach(function () {
    defaults = JSON.parse(JSON.stringify(constants.DEFAULTS));

    this.sinon.stub(gutil, 'log');
  });

  describe('buildArguments', function () {
    it('should build arguments with default options', function () {
      var result = commandBuilder.buildArguments(defaults);

      expect(result).to.be.equal('"/target:Rebuild" /verbosity:normal /toolsversion:4.0 /nologo /maxcpucount /property:Configuration="Release"');
    });

    it('should build arguments without nologo', function () {
      var options = defaults;
      options.nologo = undefined;
      var result = commandBuilder.buildArguments(options);

      expect(result).to.be.equal('"/target:Rebuild" /verbosity:normal /toolsversion:4.0 /maxcpucount /property:Configuration="Release"');
    });

    it('should build arguments with maxcpucount by default', function () {
      var options = defaults;
      var result = commandBuilder.buildArguments(options);

      expect(result).to.be.equal('"/target:Rebuild" /verbosity:normal /toolsversion:4.0 /nologo /maxcpucount /property:Configuration="Release"');
    });

    it('should build arguments with maxcpucount equal zero', function () {
      var options = defaults;
      options.maxcpucount = 0;
      var result = commandBuilder.buildArguments(options);

      expect(result).to.be.equal('"/target:Rebuild" /verbosity:normal /toolsversion:4.0 /nologo /maxcpucount /property:Configuration="Release"');
    });

    it('should build arguments with positive maxcpucount', function () {
      var options = defaults;
      options.maxcpucount = 4;
      var result = commandBuilder.buildArguments(options);

      expect(result).to.be.equal('"/target:Rebuild" /verbosity:normal /toolsversion:4.0 /nologo /maxcpucount:4 /property:Configuration="Release"');
    });

    it('should build arguments with negative maxcpucount', function () {
      var options = defaults;
      options.maxcpucount = -1;
      var result = commandBuilder.buildArguments(options);

      expect(result).to.be.equal('"/target:Rebuild" /verbosity:normal /toolsversion:4.0 /nologo /property:Configuration="Release"');
    });

    it('should build arguments excluding maxcpucount when using xbuild', function () {
      var options = defaults;
      options.maxcpucount = 4;
      options.msbuildPath = 'xbuild';
      var result = commandBuilder.buildArguments(options);

      expect(result).to.be.equal('"/target:Rebuild" /verbosity:normal /toolsversion:4.0 /nologo /property:Configuration="Release"');
    });

    it('should build arguments with custom properties', function () {
      var options = defaults;
      options.properties = { WarningLevel: 2 };
      var result = commandBuilder.buildArguments(options);

      expect(result).to.be.equal('"/target:Rebuild" /verbosity:normal /toolsversion:4.0 /nologo /maxcpucount /property:Configuration="Release" /property:WarningLevel="2"');
    });

    it('should add Configuration Property when Configuration-Option is specified', function () {
      var options = defaults;
      options.configuration = 'Debug';
      var result = commandBuilder.buildArguments(options);

      expect(result).to.be.equal('"/target:Rebuild" /verbosity:normal /toolsversion:4.0 /nologo /maxcpucount /property:Configuration="Debug"');
    });

    it('should use Configuration Property in the custom properties list when specified', function () {
      var options = defaults;
      options.properties = { Configuration: 'Debug' };
      var result = commandBuilder.buildArguments(options);

      expect(result).to.be.equal('"/target:Rebuild" /verbosity:normal /toolsversion:4.0 /nologo /maxcpucount /property:Configuration="Debug"');
    });

    it('should use fileLoggerParameters when specified', function () {
      var options = defaults;
      options.fileLoggerParameters = 'LogFile=Build.log';
      var result = commandBuilder.buildArguments(options);

      expect(result).to.be.equal('"/target:Rebuild" /verbosity:normal /toolsversion:4.0 /nologo /flp:LogFile=Build.log /maxcpucount /property:Configuration="Release"');
    });

    it('should use consoleLoggerParameters when specified', function () {
      var options = defaults;
      options.consoleLoggerParameters = 'Verbosity=minimal';
      var result = commandBuilder.buildArguments(options);

      expect(result).to.be.equal('"/target:Rebuild" /verbosity:normal /toolsversion:4.0 /nologo /clp:Verbosity=minimal /maxcpucount /property:Configuration="Release"');
    });
  });



  describe('construct', function () {
    it('should fail with no options', function () {
      var func = function () {
        return commandBuilder.construct({}, {});
      };

      expect(func).to.be.throw('No options specified!');
    });

    it('should find msbuild when not specified', function () {
      this.sinon.stub(msbuildFinder, 'find').returns('');

      commandBuilder.construct({}, defaults);

      expect(msbuildFinder.find).to.have.been.calledWith(defaults);
    });

    it('should use msbuildpath if specified', function () {
      this.sinon.stub(msbuildFinder, 'find')  ;

      var options = defaults;
      options.msbuildPath = 'here';
      var command = commandBuilder.construct({}, options);

      expect(msbuildFinder.find).to.not.have.been.calledWith(options);
      expect(command).to.be.match(/here(.+)/);
    });

    it('should construct a valid command', function () {
      var options = defaults;
      options.msbuildPath = 'here';
      var command = commandBuilder.construct({ path: 'test.sln' }, options);

      expect(command).to.be.match(/"here" "test.sln" (.+)/);
    });
  });

});

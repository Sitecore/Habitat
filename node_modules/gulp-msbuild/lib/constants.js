'use strict';

var os = require('os');

module.exports = {
  PLUGIN_NAME: 'gulp-msbuild',

  MSBUILD_VERSIONS: {
    1.0: 'v1.0.3705',
    1.1: 'v1.1.4322',
    2.0: 'v2.0.50727',
    3.5: 'v3.5',
    4.0: 'v4.0.30319',
    12.0: '12.0',
    14.0: '14.0'
  },

  DEFAULTS: {
    stdout: false,
    stderr: true,
    errorOnFail: false,
    maxBuffer: 500 * 1024,
    logCommand: false,
    targets: ['Rebuild'],
    configuration: 'Release',
    toolsVersion: 4.0,
    properties: {},
    verbosity: 'normal',
    maxcpucount: 0,
    nologo: true,
    platform: process.platform,
    architecture: detectArchitecture(),
    windir: process.env.WINDIR,
    msbuildPath: '',
    fileLoggerParameters: undefined,
    consoleLoggerParameters: undefined
  }
};

function detectArchitecture() {
  if (process.platform.match(/^win/)) {
    return process.env.hasOwnProperty('ProgramFiles(x86)') ? 'x64' : 'x86';
  }

  return os.arch();
}


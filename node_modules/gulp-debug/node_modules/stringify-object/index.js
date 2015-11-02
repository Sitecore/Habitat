'use strict';
var isRegexp = require('is-regexp');
var isPlainObj = require('is-plain-obj');

module.exports = function (val, opts, pad) {
	var seen = [];

	return (function stringify(val, opts, pad) {
		opts = opts || {};
		opts.indent = opts.indent || '\t';
		pad = pad || '';

		if (val === null ||
			val === undefined ||
			typeof val === 'number' ||
			typeof val === 'boolean' ||
			typeof val === 'function' ||
			isRegexp(val)) {
			return String(val);
		}

		if (val instanceof Date) {
			return 'new Date(\'' + val.toISOString() + '\')';
		}

		if (Array.isArray(val)) {
			if (val.length === 0) {
				return '[]';
			}

			return '[\n' + val.map(function (el, i) {
				var eol = val.length - 1 === i ? '\n' : ',\n';
				return pad + opts.indent + stringify(el, opts, pad + opts.indent) + eol;
			}).join('') + pad + ']';
		}

		if (isPlainObj(val)) {
			if (seen.indexOf(val) !== -1) {
				return '"[Circular]"';
			}

			var objKeys = Object.keys(val);

			if (objKeys.length === 0) {
				return '{}';
			}

			seen.push(val);

			var ret = '{\n' + objKeys.map(function (el, i) {
				if (opts.filter && !opts.filter(val, el)) {
					return '';
				}

				var eol = objKeys.length - 1 === i ? '\n' : ',\n';
				var key = /^[a-z$_][a-z$_0-9]*$/i.test(el) ? el : stringify(el, opts);
				return pad + opts.indent + key + ': ' + stringify(val[el], opts, pad + opts.indent) + eol;
			}).join('') + pad + '}';

			seen.pop(val);

			return ret;
		}

		val = String(val).replace(/[\r\n]/g, function (x) {
			return x === '\n' ? '\\n' : '\\r';
		});

		if (opts.singleQuotes === false) {
			return '"' + val.replace(/"/g, '\\\"') + '"';
		}

		return '\'' + val.replace(/'/g, '\\\'') + '\'';
	})(val, opts, pad);
};

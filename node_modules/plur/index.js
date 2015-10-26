'use strict';
module.exports = function (str, plural, count) {
	if (typeof plural === 'number') {
		count = plural;

		plural = (str.replace(/(?:s|x|z|ch|sh)$/i, '$&e').replace(/y$/i, 'ie') + 's')
			.replace(/i?e?s$/i, function (m) {
				var isTailLowerCase = str.slice(-1) === str.slice(-1).toLowerCase();
				return isTailLowerCase ? m.toLowerCase() : m.toUpperCase();
			});
	}

	return count === 1 ? str : plural;
};

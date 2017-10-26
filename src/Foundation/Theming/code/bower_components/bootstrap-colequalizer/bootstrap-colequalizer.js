/* global window, module, define, jQuery, require */
/*
 * Bootstrap ColEqualizer v1.1.0
 * https://github.com/megasmack/bootstrap-colequalizer
 */

;(function(factory) {
    'use strict';

    if (typeof define === 'function' && define.amd) {
        define(['jquery'], factory);
    } else if (typeof exports !== 'undefined') {
        module.exports = factory(require('jquery'));
    } else {
        factory(jQuery);
    }

}(function($) {
    'use strict';

    var ColEqualizer = window.ColEqualizer || {};

    ColEqualizer = (function () {

        function ColEqualizer(element) {

            var _ = this;

            _.el = element;

            _.colHeight(_.el);

            _.winLoad();

        }

        return ColEqualizer;

    }());

    ColEqualizer.prototype.colHeight = function(element) {

        var _ = this;

        $(element).each(function (index,el) {
            _.colReset(el);
            var tallest = 0;
            $('> [class*=col-]',el).each(function (i,e) {
                var testHeight = $(e).height();
                if (testHeight > tallest) {
                    tallest = testHeight;
                }
            });
            $('> [class*=col-]',el).height(tallest);
        });

    };

    ColEqualizer.prototype.colReset = function(el) {

        $('> [class*=col-]',el).height('auto');

    };

    ColEqualizer.prototype.resizeWindow = function() {

        var _ = this;
        var viewWidth = Math.max(document.documentElement.clientWidth, window.innerWidth || 0);

        // If set, minWidth shows/hides the nav based on the size of the browser
        // If minWidth not set, nav will always show
        if (768 <= viewWidth) {
            _.colHeight(_.el);
        } else {
            _.colReset(_.el);
        }

    };

    ColEqualizer.prototype.winLoad = function() {

        var _ = this;
        var $win = $(window);

        $win.load(function () {
            // Run function on window resize
            $win.on('resize', function () {
                _.resizeWindow();
            });
            // Run function the first time
            _.resizeWindow();
        });

    };

    // jQuery Plugin
    $.fn.colequalizer = function () {

        var _ = this;

        return _.each(function (index, element) {
            element.navinator = new ColEqualizer(element);
        });

    };
}));

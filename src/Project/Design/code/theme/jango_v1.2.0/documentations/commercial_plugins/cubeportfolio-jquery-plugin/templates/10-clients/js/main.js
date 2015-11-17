(function($, window, document, undefined) {
    'use strict';

    // init cubeportfolio
    $('#grid-container').cubeportfolio({
        layoutMode: 'slider',
        drag: true,
        auto: true,
        autoTimeout: 3000,
        autoPauseOnHover: true,
        showNavigation: false,
        showPagination: true,
        rewindNav: true,
        scrollByPage: false,
        gridAdjustment: 'responsive',
        mediaQueries: [{
            width: 1100,
            cols: 5
        }, {
            width: 800,
            cols: 4
        }, {
            width: 500,
            cols: 3
        }, {
            width: 320,
            cols: 2
        }],
        gapHorizontal: 0,
        gapVertical: 5,
        caption: 'opacity',
        displayType: 'lazyLoading',
        displayTypeSpeed: 100,
    });

})(jQuery, window, document);

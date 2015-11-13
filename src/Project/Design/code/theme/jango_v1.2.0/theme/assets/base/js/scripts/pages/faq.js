(function($, window, document, undefined) {
    'use strict';

    // init cubeportfolio
    $('#grid-container').cubeportfolio({
        filters: '#filters-container',
        defaultFilter: '*',
        animationType: 'sequentially',
        gridAdjustment: 'responsive',
        displayType: 'default',
        caption: 'expand',
        mediaQueries: [{
            width: 1,
            cols: 1
        }],
        gapHorizontal: 0,
        gapVertical: 0
    });

})(jQuery, window, document);

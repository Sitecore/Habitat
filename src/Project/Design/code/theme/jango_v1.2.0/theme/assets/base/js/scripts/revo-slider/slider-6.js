$(document).ready(function() {
    var api;
    var slider = $('.c-layout-revo-slider .tp-banner');
    var cont = $('.c-layout-revo-slider .tp-banner-container');    
    var onepageMode = $('.c-mega-menu-onepage-dots').size() > 0 ? true : false;

    if (onepageMode) {
        api = slider.show().revolution({
            delay: 15000,    
            startwidth:1170,
            startheight: 1000,
           
            navigationType: "hide",
            navigationArrows: "solo",

            touchenabled: "on",
            onHoverStop: "on",

            keyboardNavigation: "off",

            navigationType:"bullet",
            navigationArrows:"",
            navigationStyle:"round c-tparrows-hide c-theme",
            navigationHAlign: cont.attr('data-bullets-pos') === "center" ? 'center' : 'right',
            navigationVAlign: "bottom", 
            navigationHOffset: cont.attr('data-bullets-pos') === "center" ? 0 : 60,
            navigationVOffset:60,

            spinner: "spinner2",

            fullScreen: 'on',   
            fullScreenAlignForce: 'on', 
            fullScreenOffsetContainer: '',

            shadow: 0,
            fullWidth: "off",
            forceFullWidth: "off",
            hideTimerBar:"on",

            hideThumbsOnMobile: "on",
            hideNavDelayOnMobile: 1500,
            hideBulletsOnMobile: "on",
            hideArrowsOnMobile: "on",
            hideThumbsUnderResolution: 0
        });
    } else {
        api = slider.show().revolution({
            delay: 15000,    
            startwidth:1170,
            startheight: App.getViewPort().height,
           
            navigationType: "hide",
            navigationArrows: "solo",

            touchenabled: "on",
            onHoverStop: "on",

            keyboardNavigation: "off",

            navigationStyle: "circle",
            navigationHAlign: "center",
            navigationVAlign: "bottom",

            spinner: "spinner2",

            fullScreen: "on",   
            fullScreenAlignForce:"on",
            fullScreenOffsetContainer: '',

            shadow: 0,
            fullWidth: "off",
            forceFullWidth: "off",
            hideTimerBar:"on",

            hideThumbsOnMobile: "on",
            hideNavDelayOnMobile: 1500,
            hideBulletsOnMobile: "on",
            hideArrowsOnMobile: "on",
            hideThumbsUnderResolution: 0
        });
    }
}); //ready
$(document).ready(function() {
    
    var slider = $('.c-layout-revo-slider .tp-banner');

    var cont = $('.c-layout-revo-slider .tp-banner-container');

    var api = slider.show().revolution({
        delay: 15000,    
        startwidth:1170,
        startheight: 680,
       
        navigationType: "hide",
        navigationArrows: "solo",

        touchenabled: "on",
        onHoverStop: "on",

        keyboardNavigation: "off",

        navigationStyle: "circle",
        navigationHAlign: "center",
        navigationVAlign: "center",

        fullScreenAlignForce:"off",

        shadow: 0,
        fullWidth: "on",
        fullScreen: "off",       

        spinner: "spinner2",

        forceFullWidth: "on",
        hideTimerBar:"on",

        hideThumbsOnMobile: "on",
        hideNavDelayOnMobile: 1500,
        hideBulletsOnMobile: "on",
        hideArrowsOnMobile: "on",
        hideThumbsUnderResolution: 0,
        
        videoJsPath: "rs-plugin/videojs/",
    });
}); //ready
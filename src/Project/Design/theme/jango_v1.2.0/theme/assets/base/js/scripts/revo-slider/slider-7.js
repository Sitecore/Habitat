$(document).ready(function() {
    var slider = $('.c-layout-revo-slider .tp-banner');
    var cont = $('.c-layout-revo-slider .tp-banner-container');

    var api = slider.show().revolution({
        delay: 15000,    
        startwidth:1170,
        startheight: 620,
       
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

    api.bind("revolution.slide.onchange",function (e,data) {

        $('.c-layout-header').removeClass('hide');   

        setTimeout(function(){
            $('.c-singup-form').fadeIn(); 
        }, 1500);
    });
}); //ready
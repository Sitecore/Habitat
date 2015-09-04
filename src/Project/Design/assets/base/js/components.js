/**
Core layout handlers and component wrappers
**/

// BEGIN: Layout Brand
var LayoutBrand = function() {

    return {
        //main function to initiate the module
        init: function() {
            $('body').on('click', '.c-hor-nav-toggler', function() {
                var target = $(this).data('target');
                $(target).toggleClass("c-shown");
            });
        }

    };
}();
// END

// BEGIN: Layout Header
var LayoutHeader = function() {
    var offset = parseInt($('.c-layout-header').attr('data-minimize-offset') > 0 ? parseInt($('.c-layout-header').attr('data-minimize-offset')) : 0);
    var _handleHeaderOnScroll = function() {
        if ($(window).scrollTop() > offset) {
            $("body").addClass("c-page-on-scroll");
        } else {
            $("body").removeClass("c-page-on-scroll");
        }
    }

    var _handleTopbarCollapse = function() {
        $('.c-layout-header .c-topbar-toggler').on('click', function(e) {
            $('.c-layout-header-topbar-collapse').toggleClass("c-topbar-expanded");
        });
    }

    return {
        //main function to initiate the module
        init: function() {
            if ($('body').hasClass('c-layout-header-fixed-non-minimized')) {
                return; 
            }

            _handleHeaderOnScroll();
            _handleTopbarCollapse();

            $(window).scroll(function() {
                _handleHeaderOnScroll();
            });
        }
    };
}();
// END

// BEGIN: Layout Mega Menu
var LayoutMegaMenu = function() {

    return {
        //main function to initiate the module
        init: function() {
            $('.c-mega-menu').on('click', '.c-toggler', function(e) {
                if (App.getViewPort().width < App.getBreakpoint('md')) {
                    e.preventDefault();
                    $(this).closest("li").toggleClass("c-open");
                }
            });

            $('.c-layout-header .c-hor-nav-toggler:not(.c-quick-sidebar-toggler)').on('click', function() {
                $('.c-layout-header').toggleClass('c-mega-menu-shown');  

                if ($('body').hasClass('c-layout-header-mobile-fixed')) {
                    var height = App.getViewPort().height - $('.c-layout-header').outerHeight(true) - 60;
                    $('.c-mega-menu').css('max-height', height);
                }
            });
        }
    };
}();
// END

// BEGIN: Layout Mega Menu
var LayoutSidebarMenu = function() {

    return {
        //main function to initiate the module
        init: function() {
            $('.c-layout-sidebar-menu > .c-sidebar-menu .c-toggler').on('click', function(e) {
                e.preventDefault();
                $(this).closest('.c-dropdown').toggleClass('c-open'); 
            });
        }
    };
}();
// END

// BEGIN: Layout Mega Menu
var LayoutQuickSearch = function() {

    return {
        //main function to initiate the module
        init: function() {
            // desktop mode
            $('.c-layout-header').on('click', '.c-mega-menu .c-search-toggler', function(e) {
                e.preventDefault();

                $('body').addClass('c-layout-quick-search-shown');

                if (App.isIE() === false) {
                    $('.c-quick-search > .form-control').focus();
                }
            });

            // mobile mode
            $('.c-layout-header').on('click', '.c-brand .c-search-toggler', function(e) {
                e.preventDefault();

                $('body').addClass('c-layout-quick-search-shown');

                if (App.isIE() === false) {
                    $('.c-quick-search > .form-control').focus();
                }
            });

            // handle close icon for mobile and desktop
            $('.c-quick-search').on('click', '> span', function(e) {
                e.preventDefault();
                $('body').removeClass('c-layout-quick-search-shown');
            });
        }
    };
}();
// END

// BEGIN: Layout Mega Menu
var LayoutQuickSidebar = function() {

    return {
        //main function to initiate the module
        init: function() {
            // desktop mode
            $('.c-layout-header').on('click', '.c-quick-sidebar-toggler', function(e) {
                e.preventDefault();
                e.stopPropagation();

                if ($('body').hasClass("c-layout-quick-sidebar-shown")) {
                    $('body').removeClass("c-layout-quick-sidebar-shown");
                } else {
                    $('body').addClass("c-layout-quick-sidebar-shown");
                }
            });

            $('.c-layout-quick-sidebar').on('click', '.c-close', function(e) {
                e.preventDefault();

                $('body').removeClass("c-layout-quick-sidebar-shown");
            });

            $('.c-layout-quick-sidebar').on('click', function(e) {
                e.stopPropagation();
            });

            $(document).on('click', '.c-layout-quick-sidebar-shown', function(e) {
                $(this).removeClass("c-layout-quick-sidebar-shown");
            });
        }
    };
}();
// END

// BEGIN: Layout Go To Top
var LayoutGo2Top = function() {
    
    var handle = function() {
        var currentWindowPosition = $(window).scrollTop(); // current vertical position
        if (currentWindowPosition > 300) {
            $(".c-layout-go2top").show();
        } else {
            $(".c-layout-go2top").hide();
        }
    };

    return {

         //main function to initiate the module
        init: function() {
            
            handle(); // call headerFix() when the page was loaded

            if (navigator.userAgent.match(/iPhone|iPad|iPod/i)) {
                $(window).bind("touchend touchcancel touchleave", function(e) {
                    handle();
                });
            } else {
                $(window).scroll(function() {
                    handle();
                });
            }

            $(".c-layout-go2top").on('click', function(e) {
                e.preventDefault();
                $("html, body").animate({
                    scrollTop: 0
                }, 600);
            });
        }

    };
}();
// END: Layout Go To Top

// BEGIN: Onepage Nav
var LayoutOnepageNav = function() {
    
    var handle = function() {
        var offset;
        var scrollspy;
        var speed;
        var nav;

        $('body').addClass('c-page-on-scroll');
        offset = $('.c-layout-header-onepage').outerHeight(true);
        $('body').removeClass('c-page-on-scroll');
        
        if ($('.c-mega-menu-onepage-dots').size() > 0) {
            if ($('.c-onepage-dots-nav').size() > 0) {
                $('.c-onepage-dots-nav').css('margin-top', -($('.c-onepage-dots-nav').outerHeight(true) / 2));
            }
            scrollspy = $('body').scrollspy({
                target: '.c-mega-menu-onepage-dots',
                offset: offset
            });
            speed = parseInt($('.c-mega-menu-onepage-dots').attr('data-onepage-animation-speed'));
        } else {
            scrollspy = $('body').scrollspy({
                target: '.c-mega-menu-onepage',
                offset: offset
            });
            speed = parseInt($('.c-mega-menu-onepage').attr('data-onepage-animation-speed'));
        }        

        scrollspy.on('activate.bs.scrollspy', function() {
            $(this).find('.c-onepage-link.c-active').removeClass('c-active');
            $(this).find('.c-onepage-link.active').addClass('c-active');
        });
        
        $('.c-onepage-link > a').on('click', function(e) {
            var section = $(this).attr('href');
            var top = 0;

            if (section !== "#home") {
                top = $(section).offset().top - offset;
            }

            $('html, body').stop().animate({
                scrollTop: top,
            }, speed, 'easeInExpo');

            e.preventDefault();

            if (App.getViewPort().width < App.getBreakpoint('md')) {
                $('.c-hor-nav-toggler').click();
            }
        });
    };

    return {

         //main function to initiate the module
        init: function() {  
            handle(); // call headerFix() when the page was loaded
        }

    };
}();
// END: Onepage Nav

// BEGIN: Handle Theme Settings
var LayoutThemeSettings = function() {
    
    var handle = function() {

        $('.c-settings .c-color').on('click', function(){
            var val = $(this).attr('data-color');
            $('#style_theme').attr('href', 'assets/base/css/themes/' + val + '.css');

            $('.c-settings .c-color').removeClass('c-active');
            $(this).addClass('c-active');
        });

        $('.c-setting_header-type').on('click', function(){
            var val = $(this).attr('data-value');
            if (val == 'fluid') {
                $('.c-layout-header .c-topbar > .container').removeClass('container').addClass('container-fluid');
                $('.c-layout-header .c-navbar > .container').removeClass('container').addClass('container-fluid');
            } else {
                $('.c-layout-header .c-topbar > .container-fluid').removeClass('container-fluid').addClass('container');
                $('.c-layout-header .c-navbar > .container-fluid').removeClass('container-fluid').addClass('container');
            }   
            $('.c-setting_header-type').removeClass('active');
            $(this).addClass('active');
        });

        $('.c-setting_header-mode').on('click', function(){
            var val = $(this).attr('data-value');
            if (val == 'static') {
                $('body').removeClass('c-layout-header-fixed').addClass('c-layout-header-static');
            } else {
                $('body').removeClass('c-layout-header-static').addClass('c-layout-header-fixed');
            }   
            $('.c-setting_header-mode').removeClass('active');
            $(this).addClass('active');
        });

        $('.c-setting_font-style').on('click', function(){
            var val = $(this).attr('data-value');

            if (val == 'light') {
                $('.c-font-uppercase').addClass('c-font-uppercase-reset').removeClass('c-font-uppercase');
                $('.c-font-bold').addClass('c-font-bold-reset').removeClass('c-font-bold');

                $('.c-fonts-uppercase').addClass('c-fonts-uppercase-reset').removeClass('c-fonts-uppercase');
                $('.c-fonts-bold').addClass('c-fonts-bold-reset').removeClass('c-fonts-bold');
            } else {
                $('.c-font-uppercase-reset').addClass('c-font-uppercase').removeClass('c-font-uppercase-reset');
                $('.c-font-bold-reset').addClass('c-font-bold').removeClass('c-font-bold-reset');

                $('.c-fonts-uppercase-reset').addClass('c-fonts-uppercase').removeClass('c-fonts-uppercase-reset');
                $('.c-fonts-bold-reset').addClass('c-fonts-bold').removeClass('c-fonts-bold-reset');
            } 

            $('.c-setting_font-style').removeClass('active');
            $(this).addClass('active');
        });

        $('.c-setting_megamenu-style').on('click', function(){
            var val = $(this).attr('data-value');
            if (val == 'dark') {
                $('.c-mega-menu').removeClass('c-mega-menu-light').addClass('c-mega-menu-dark');
            } else {
                $('.c-mega-menu').removeClass('c-mega-menu-dark').addClass('c-mega-menu-light');
            } 
            $('.c-setting_megamenu-style').removeClass('active');
            $(this).addClass('active');
        });
    
    };

    return {

         //main function to initiate the module
        init: function() {
            
            handle();
        }

    };
}();
// END: Handle Theme Settings

// BEGIN: OwlCarousel
var ContentOwlcarousel = function() {

    var _initInstances = function() {
        $("[data-slider='owl'] .owl-carousel").each(function() { 
            var parent = $(this).parent();

            var items;
            var itemsDesktop;
            var itemsDesktopSmall;
            var itemsTablet;
            var itemsTabletSmall;
            var itemsMobile;

            if (parent.attr("data-single-item") == "true") {
                items = 1;
                itemsDesktop = 1;
                itemsDesktopSmall = 1;
                itemsTablet = 1;
                itemsTabletSmall = 1;
                itemsMobile = 1;
            } else {
                items = parent.attr('data-items');
                itemsDesktop = [1199, parent.attr('data-desktop-items') ? parent.attr('data-desktop-items') : items];
                itemsDesktopSmall = [979, parent.attr('data-desktop-small-items') ? parent.attr('data-desktop-small-items') : 3];
                itemsTablet = [768, parent.attr('data-tablet-items') ? parent.attr('data-tablet-items') : 2];
                itemsMobile = [479, parent.attr('data-mobile-items') ? parent.attr('data-mobile-items') : 1];
            }

            $(this).owlCarousel({

                items: items,
                itemsDesktop: itemsDesktop,
                itemsDesktopSmall: itemsDesktopSmall,
                itemsTablet: itemsTablet,
                itemsTabletSmall: itemsTablet,
                itemsMobile: itemsMobile,
                
                navigation : parent.attr("data-navigation") ? true : false,
	            navigationText : false,
                slideSpeed : parent.attr('data-slide-speed', 400),
                paginationSpeed : parent.attr('data-pagination-speed', 800),  
                singleItem: parent.attr("data-single-item") ? true : false,
                autoPlay: parent.attr("data-auto-play")
            });
        });
    };

    return {

         //main function to initiate the module
        init: function() {
            
            _initInstances();
        }

    };
}();
// END: OwlCarousel

// BEGIN: ContentCubeLatestPortfolio
var ContentCubeLatestPortfolio = function() {

    var _initInstances = function() {

        // init cubeportfolio
        $('.c-content-latest-works').cubeportfolio({
            filters: '#filters-container',
            loadMore: '#loadMore-container',
            loadMoreAction: 'click',
            layoutMode: 'grid',
            defaultFilter: '*',
            animationType: 'quicksand',
            gapHorizontal: 20,
            gapVertical: 23,
            gridAdjustment: 'responsive',
            mediaQueries: [{
                width: 1100,
                cols: 4
            }, {
                width: 800,
                cols: 3
            }, {
                width: 500,
                cols: 2
            }, {
                width: 320,
                cols: 1
            }],
            caption: 'zoom',
            displayType: 'lazyLoading',
            displayTypeSpeed: 100,

            // lightbox
            lightboxDelegate: '.cbp-lightbox',
            lightboxGallery: true,
            lightboxTitleSrc: 'data-title',
            lightboxCounter: '<div class="cbp-popup-lightbox-counter">{{current}} of {{total}}</div>',

            // singlePage popup
            singlePageDelegate: '.cbp-singlePage',
            singlePageDeeplinking: true,
            singlePageStickyNavigation: true,
            singlePageCounter: '<div class="cbp-popup-singlePage-counter">{{current}} of {{total}}</div>',
            singlePageCallback: function(url, element) {
                // to update singlePage content use the following method: this.updateSinglePage(yourContent)
                var t = this;

                $.ajax({
                        url: url,
                        type: 'GET',
                        dataType: 'html',
                        timeout: 5000
                    })
                    .done(function(result) {
                        t.updateSinglePage(result);
                    })
                    .fail(function() {
                        t.updateSinglePage("Error! Please refresh the page!");
                    });
            },
        });

        $('.c-content-latest-works-fullwidth').cubeportfolio({
            loadMoreAction: 'auto',
            layoutMode: 'grid',
            defaultFilter: '*',
            animationType: 'fadeOutTop',
            gapHorizontal: 0,
            gapVertical: 0,
            gridAdjustment: 'responsive',
            mediaQueries: [{
                width: 1600,
                cols: 5
            }, {
                width: 1200,
                cols: 4
            }, {
                width: 800,
                cols: 3
            }, {
                width: 500,
                cols: 2
            }, {
                width: 320,
                cols: 1
            }],
            caption: 'zoom',
            displayType: 'lazyLoading',
            displayTypeSpeed: 100,

            // lightbox
            lightboxDelegate: '.cbp-lightbox',
            lightboxGallery: true,
            lightboxTitleSrc: 'data-title',
            lightboxCounter: '<div class="cbp-popup-lightbox-counter">{{current}} of {{total}}</div>',
        });

    };

    return {

         //main function to initiate the module
        init: function() {
            _initInstances();
        }

    };
}();
// END: ContentCubeLatestPortfolio

// BEGIN: CounterUp
var ContentCounterUp = function() {

    var _initInstances = function() {

        // init counter up
        $("[data-counter='counterup']").counterUp({
            delay: 10,
            time: 1000
        });
    };

    return {

         //main function to initiate the module
        init: function() {
            _initInstances();
        }

    };
}();
// END: CounterUp

// BEGIN: Fancybox
var ContentFancybox = function() {

    var _initInstances = function() {
        // init fancybox
        $("[data-lightbox='fancybox']").fancybox();
    };

    return {

         //main function to initiate the module
        init: function() {
            _initInstances();
        }

    };
}();
// END: Fancybox

// BEGIN: Twitter
var ContentTwitter = function () {

	var _initInstances = function () {
		// init twitter
		if ($(".twitter-timeline")[0]) {
			!function (d, s, id) {
				var js, fjs = d.getElementsByTagName(s)[0], p = /^http:/.test(d.location) ? 'http' : 'https';
				if (!d.getElementById(id)) {
					js = d.createElement(s);
					js.id = id;
					js.src = p + "://platform.twitter.com/widgets.js";
					fjs.parentNode.insertBefore(js, fjs);
				}
			}(document, "script", "twitter-wjs");
		}
	};

	return {

		//main function to initiate the module
		init: function () {
			_initInstances();
		}

	};
}();
// END: Twitter

// Main theme initialization
$(document).ready(function() {
    // init layout handlers 
    LayoutBrand.init();    
    LayoutHeader.init();
    LayoutMegaMenu.init();
    LayoutSidebarMenu.init();
    LayoutQuickSearch.init();
    LayoutQuickSidebar.init();
    LayoutGo2Top.init();    
    LayoutOnepageNav.init();
    LayoutThemeSettings.init();
    
    // init plugin wrappers    
    ContentOwlcarousel.init();
    ContentCubeLatestPortfolio.init();
    ContentCounterUp.init();
    ContentFancybox.init();
    ContentTwitter.init();
});
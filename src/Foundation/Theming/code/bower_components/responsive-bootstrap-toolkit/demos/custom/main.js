// Your own scripts

(function($, document, window, viewport){

    var visibilityDivs = {
        'alias-1': $('<div class="device-alias-1 visible-custom-1"></div>'),
        'alias-2': $('<div class="device-alias-2 visible-custom-2"></div>'),
        'alias-3': $('<div class="device-alias-3 visible-custom-3"></div>')
    };

    viewport.use('Custom', visibilityDivs);



    if( viewport.is('alias-1') ) {
        console.log('Matching: alias-1');
    }

    if( viewport.is('>=alias-2') ) {
        console.log('Matching: >=alias-2');
    }



    // Executes once whole document has been loaded
    $(document).ready(function() {
        console.log('Current breakpoint:', viewport.current());
    });

    $(window).resize(
        viewport.changed(function(){
            console.log('Current breakpoint:', viewport.current());
        })
    );

})(jQuery, document, window, ResponsiveBootstrapToolkit);

// Your own scripts

(function($, document, window, viewport){

    viewport.use('Foundation');

    if( viewport.is('small') ) {
        console.log('Matching: small');
    }

    if( viewport.is('>=medium') ) {
        console.log('Matching: >=medium');
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

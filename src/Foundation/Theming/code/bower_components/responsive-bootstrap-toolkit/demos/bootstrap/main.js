// Your own scripts

(function($, document, window, viewport){

    var highlightBox = function( className ) {
        $(className).addClass('active');
    }

    var highlightBoxes = function() {
        $('.comparison-operator').removeClass('active');

        if( viewport.is("<=sm") ) {
            highlightBox('.box-1');
        }

        if( viewport.is("md") ) {
            highlightBox('.box-2');
        }

        if( viewport.is(">md") ) {
            highlightBox('.box-3');
        }
    }

    // Executes once whole document has been loaded
    $(document).ready(function() {

        highlightBoxes();

        console.log('Current breakpoint:', viewport.current());

    });

    $(window).resize(
        viewport.changed(function(){
            highlightBoxes();

            console.log('Current breakpoint:', viewport.current());
        })
    );

})(jQuery, document, window, ResponsiveBootstrapToolkit);

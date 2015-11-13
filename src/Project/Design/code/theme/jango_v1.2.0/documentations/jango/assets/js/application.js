// NOTICE!! DO NOT USE ANY OF THIS JAVASCRIPT
// IT'S ALL JUST JUNK FOR OUR DOCS!
// ++++++++++++++++++++++++++++++++++++++++++

!function ($) {

  $(function(){

    var $window = $(window)

    // Disable certain links in docs
    $('section [href^=#]').click(function (e) {
      e.preventDefault()
    })

    $('.clickable').click(function() {
      var pos = ($($(this).attr("data-section")).offset().top)-40;
      jQuery('html, body').animate({
          scrollTop: pos
      }, 'slow');
    });

    $('.page-header > a').click(function(){
      jQuery('html, body').animate({
          scrollTop: 0
      }, 'slow');
    });

    // make code pretty
    window.prettyPrint && prettyPrint()

  })

// Modified from the original jsonpi https://github.com/benvinegar/jquery-jsonpi

}(window.jQuery)
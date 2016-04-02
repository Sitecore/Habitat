jQuery.noConflict();

(function($) {
  $(function() {
    $('[data-toggle-class]').on('click', function(e) {
      e.preventDefault();
      switch ($(this).data('toggle-class')) {
        case "is-searching":
          $('#siteNavbar').collapse('hide');
          break;
      }
      $($(this).attr('data-target')).toggleClass($(this).attr('data-toggle-class'));
    });
  });
})(jQuery);

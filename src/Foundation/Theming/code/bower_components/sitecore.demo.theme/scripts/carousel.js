(function($) {
  $(function() {

    $('.owl-carousel').each(function(){
      var options = (typeof $(this).attr('data-options') !== "undefined") ? $.parseJSON($(this).attr('data-options')) : {};
      options.navigationText = [
        '<span class="glyphicon glyphicon-chevron-left" aria-hidden="true"></span>',
        '<span class="glyphicon glyphicon-chevron-right" aria-hidden="true"></span>'
      ];
      $(this).owlCarousel(options);
    });
    
  });
})(jQuery);

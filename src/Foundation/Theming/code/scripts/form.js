jQuery.noConflict();

(function($) {
  $(function() {
    $('input[type="file"]').on('change', function(e) {
      var fileName = $(this).val().split('\\')[$(this).val().split('\\').length-1];
      $('label[for="'+$(this).attr('id')+'"]').html(fileName).addClass('active');
    });

  });
})(jQuery);

(function($) {
  $(function() {
    $('[href="styles/white-wonder.css"]').attr('data-themetype', 'main');
    $('[href="styles/dayfrost.css"]').attr('data-themetype', 'main');
    $('[href="styles/niteflight.css"]').attr('data-themetype', 'main');
    
    $('[data-toggle="theme"]').on('click', function() {
      $('[data-themetype="'+$(this).attr('data-themetype')+'"]').attr('disabled', 'disabled');
      $('[href="styles/'+$(this).attr('data-theme')+'.css"]').removeAttr('disabled');
    });
  });
})(jQuery);

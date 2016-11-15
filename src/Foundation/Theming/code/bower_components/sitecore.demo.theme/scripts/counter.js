(function($) {
  $(function() {
    $('.counter').each(function() {
      var valTo = $(this).data('to');
      var increment = valTo / ($(this).data('speed')/100);
      var _this = this;
      var interval = setInterval(function() {
        var newVal = Math.ceil(parseInt($(_this).html()) + increment);
        $(_this).html(newVal);
        if (newVal >= valTo) {
          $(_this).html(valTo);
          clearInterval(interval);
        }
      }, 100);
    });
  });
})(jQuery);

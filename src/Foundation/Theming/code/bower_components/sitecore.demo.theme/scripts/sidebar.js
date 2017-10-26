(function($) {
  $(function() {
    $('.sidebar.sidebar-dynamic ul li a').on('click', function(e) {
      e.preventDefault();
      $(this).parent().toggleClass('open');
    });
    
    $('body').on('click', '[data-toggle="sidebar"]', function(e) {
      e.preventDefault();
      console.log(this);
      $('html').toggleClass('show-sidebar-left', $(this).attr('data-side') == 'left' && !$('html').hasClass('show-sidebar-left'));
      $('html').toggleClass('show-sidebar-right', $(this).attr('data-side') == 'right' && !$('html').hasClass('show-sidebar-right'));
      $('.sidebar-closed, .sidebar-opened').toggle();
    });
    
    !$('html').hasClass('show-sidebar-left') && !$('html').hasClass('show-sidebar-left') ? $('.sidebar-closed').show() && $('.sidebar-opened').hide() : $('.sidebar-closed').hide() && $('.sidebar-opened').show();
  });
})(jQuery);

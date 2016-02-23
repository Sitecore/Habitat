if(!window.console){
  console={};
  console.log = function(){};
}

jQuery.noConflict();

(function($) {
  $(function() {
    var mq;

    var $mqElement = $($.parseHTML('<span id="mq-detector"><span class="visible-xs"></span><span class="visible-sm"></span><span class="visible-md"></span><span class="visible-lg"></span></span>'));
    $mqElement.css('visibility', 'hidden');

    $('body').append($mqElement);
    $mqElement.children().each(function() {
      if($(this).is(':visible')) {
        mq = $(this).attr('class').substring($(this).attr('class').length-2);
      };
    });

    $('[href="styles/white-wonder.css"]').attr('data-themetype', 'main');
    $('[href="styles/niteflight.css"]').attr('data-themetype', 'main');

    $('.sidebar.sidebar-dynamic ul li a').on('click', function(e) {
      e.preventDefault();
      $(this).parent().toggleClass('open');
    });

    $('body').on('click', function(e) {
      //$(e.target).toggleClass('bg-white');
    });

    $('input[type="file"]').on('change', function(e) {
      var fileName = $(this).val().split('\\')[$(this).val().split('\\').length-1];
      $('label[for="'+$(this).attr('id')+'"]').html(fileName).addClass('active');
    });

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

    $('[data-toggle="theme"]').on('click', function() {
      $('[data-themetype="'+$(this).attr('data-themetype')+'"]').attr('disabled', 'disabled');
      $('[href="styles/'+$(this).attr('data-theme')+'.css"]').removeAttr('disabled');
    });

    $('.owl-carousel').each(function(){
      var options = (typeof $(this).attr('data-options') !== "undefined") ? $.parseJSON($(this).attr('data-options')) : {};
      options.navigationText = [
        '<span class="glyphicon glyphicon-chevron-left" aria-hidden="true"></span>',
        '<span class="glyphicon glyphicon-chevron-right" aria-hidden="true"></span>'
      ];
      $(this).owlCarousel(options);
    });

    $('.grid-filter').each(function() {
      var $grid = $($(this).attr('data-grid-filter'));
      // Options - not yet relevant, can be utilized later on.
      //var options = (typeof $(this).attr('data-filter-options') !== "undefined") ? $.parseJSON($(this).attr('data-filter-options')) : null;
      var $filters = $(this).find('[data-group]');
      $grid.shuffle({
        itemSelector: '[data-groups]'
      });
      $filters.on('click', function(e) {
        $filters.removeClass('active');
        $(this).addClass('active');
        $grid.shuffle('shuffle', $(this).attr('data-group'));
      });
      $(this).find('.grid-filter-sort').on('change', function() {
        var reverse = $(this).data('reverse') || false;
        var sort = this.value,
            opts = {
              reverse: !reverse,
              by: function($el) {
                return $el.attr('data-'+sort);
              }
            };
        $grid.shuffle('sort', opts);
      });
      $(this).find('.grid-filter-search').on('keyup change', function(e) {
        var value = this.value.toLowerCase();
        $grid.shuffle('shuffle', function($el, shuffle) {
          return $el.attr('data-search-term').toLowerCase().indexOf(value) !== -1;
        });
      });
    });

    $('[data-toggle-class]').on('click', function(e) {
      e.preventDefault();
      switch ($(this).data('toggle-class')) {
        case "is-searching":
          $('#siteNavbar').collapse('hide');
          break;
      }
      $($(this).attr('data-target')).toggleClass($(this).attr('data-toggle-class'));
    });

    $('[data-toggle="popover"]').popover();
    $('[data-toggle="tooltip"]').tooltip();
    $('.code').each(function(){
      var encoded = document.createTextNode($.trim($(this)[0].innerHTML));
      $(this).html(encoded);
      var editor = ace.edit($(this).attr('id'));
      editor.setFontSize(13);
      editor.getSession().setMode("ace/mode/html");
      //$(this).html(encoded);
    });
    $('body').on('click', '[data-toggle="sidebar"]', function(e) {
      e.preventDefault();
      console.log(this);
      $('html').toggleClass('show-sidebar-left', $(this).attr('data-side') == 'left' && !$('html').hasClass('show-sidebar-left'));
      $('html').toggleClass('show-sidebar-right', $(this).attr('data-side') == 'right' && !$('html').hasClass('show-sidebar-right'));
      $('.sidebar-closed, .sidebar-opened').toggle();
    });
    $(document).delegate('*[data-toggle="lightbox"]', 'click', function (event) {
      event.preventDefault();
      $(this).ekkoLightbox();
    });

    !$('html').hasClass('show-sidebar-left') && !$('html').hasClass('show-sidebar-left') ? $('.sidebar-closed').show() && $('.sidebar-opened').hide() : $('.sidebar-closed').hide() && $('.sidebar-opened').show();
  });
})(jQuery);

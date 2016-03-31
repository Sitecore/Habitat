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

  });
})(jQuery);

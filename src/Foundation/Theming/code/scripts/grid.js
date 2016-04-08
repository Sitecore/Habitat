jQuery.noConflict();

(function($) {
  $(function() {
    $('.grid-filter').each(function() {
      var $grid = $($(this).attr('data-grid-filter'));
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
  });
})(jQuery);

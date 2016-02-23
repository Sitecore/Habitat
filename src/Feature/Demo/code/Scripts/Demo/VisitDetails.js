jQuery(function() {
  jQuery("head").on("visit-info:refresh", function() {
    var panel = jQuery(".visit-info");
    jQuery.ajax(
    {
      url: "/api/Demo/VisitDetails",
      method: "get",
      cache: false,
      success: function(data) {
        panel.replaceWith(data);
      }
    });
  });
});
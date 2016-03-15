jQuery(function() {
  jQuery("head").on("visit-info:refresh", function() {
    var panel = jQuery("#experiencedata");
    jQuery.ajax(
    {
      url: "/api/Demo/ExperienceData",
      method: "get",
      cache: false,
      success: function (data) {
        panel.replaceWith(data);
      }
    });
  });
});
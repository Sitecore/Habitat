function refreshVisitDetails(button) {
    var container = jQuery(button).closest(".panel");
  jQuery.ajax(
  {
    url: "/api/Demo/VisitDetails",
    method: "get",
    success: function (data) {
        container.html(data);
    }
  });
}
function refreshVisitDetails(button) {
  var container = $(button).closest(".panel");
  $.ajax(
  {
    url: "/api/Demo/VisitDetails",
    method: "get",
    success: function (data) {
        container.html(data);
    }
  });
}
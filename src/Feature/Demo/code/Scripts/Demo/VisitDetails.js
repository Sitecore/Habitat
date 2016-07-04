jQuery.noConflict();
(function ($) {
  $(function () {
    $(".refresh-sidebar")
      .click(function () {
        var panel = $("#experiencedata");
        $.ajax(
          {
            url: "/api/Demo/ExperienceDataContent",
            method: "get",
            cache: false,
            success: function (data) {
              panel.replaceWith(data);
            }
          });
      });
    $(".end-visit")
      .click(function () {
       $.ajax(
          {
            url: "/api/Demo/EndVisit",
            method: "get",
            cache: false,
            success: function () {
              setTimeout(function () {
                window.location.href = "/";
              },
                1000);
            }
          });
      });
  });
})(jQuery);
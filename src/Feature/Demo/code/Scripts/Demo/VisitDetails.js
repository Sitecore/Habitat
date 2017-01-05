(function($) {
    var refreshExperincePanel = function() {
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
    };
    $(function() {
        $(".refresh-sidebar")
            .click(function () {
                $(this).fadeOut();
                refreshExperincePanel();
                $(this).fadeIn();
            });
        $(".end-visit")
            .click(function() {
                $(this).fadeOut();
                $.ajax(
                {
                    url: "/api/Demo/EndVisit",
                    method: "get",
                    cache: false,
                    success: function() {
                        refreshExperincePanel();
                    }
                });
                $(this).fadeIn();
            });
    });
})(jQuery);
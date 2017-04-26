function ToggleFacet(query, facets, facetName, facetValue) {
    var ajaxPost = function (responseFunction, sender) {
        var token = $('#_CRSFform input[name=__RequestVerificationToken]').val();
        $.ajax({
            type: "POST",
            url: "/api/feature/search/togglefacet",
            cache: false,
            headers: { "__RequestVerificationToken": token },
            contentType: "application/json; charset=utf-8",
            data: "{\"Query\":\"" + query + "\", \"Facets\":\"" + facets + "\", \"FacetName\":\"" + facetName + "\", \"FacetValue\":\"" + facetValue + "\"}",
            success: function (data) {
                if (responseFunction != null) {
                    responseFunction(data, true, sender);
                }
            },
            error: function (data) {
                if (responseFunction != null) {
                    responseFunction(data, false, sender);
                }
            }
        });
    };

    ajaxPost(function (data, success, sender) {
        if (success)
            window.location = data.url;
    });
    return false;
}

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

/* -------------
 * Global search
 * -------------*/

var lastQuery = null;
$(document).ready(function () {
    var delay = (function () {
        var timer = 0;
        return function (callback, ms) {
            clearTimeout(timer);
            timer = setTimeout(callback, ms);
        };
    })();

    $("#searchInputBox").keyup(function () {
        var query = $(this).val();
        if (lastQuery === query)
            return false;
        lastQuery = query;

        if (query.length < 3) {
            $("#navbar-activity-search-results").hide();
            return false;
        }
        showPanels();

        delay(function () {
            getResultsTypeAhead(query);
        },
            500);
    });
    $(".navbar-activity-search .navbar-toggle-search").click(function () {
        $("#navbar-activity-search-results").hide();
    });
});

function fillResults(results) {
    $("#navbar-activity-search-results .results").empty();

    if (results == null || results.length === 0)
        return;
    var listHtml = "";
    $.each(results,
        function (i, result) {
            listHtml +=
                "<li class='media'>" + (result.Image ?
                    "<div class='media-left'>" +
                        "<a href='" + result.Url + "' >" +
                            "<img src='" + result.Image + "' alt='" + result.Title + "'/>" +
                        "</a>" : "") +
                    "</div>" +
                    "<div class='media-body'>" +
                        "<span class='label label-info pull-right'>" +
                            result.ContentType +
                        "</span>" +
                        "<h6 class='media-heading'>" +
                            "<a href='" + result.Url + "' >" +
                                result.Title +
                            "</a>" +
                        "</h6>" +
                        "<p class='small'>" +
                            result.Description +
                        "</p>" +
                    "</div>" +
                "</li>";
        });

    $("#navbar-activity-search-results .results").append(listHtml);
    showPanels("results");
}

function fillFacetValues(facet, facetValues) {
    $("#navbar-activity-search-results .facet-values").empty();
    if (facetValues == null || facetValues.length === 0)
        return;
    var listHtml = "";
    $.each(facetValues,
        function (i, facetValue) {
            listHtml +=
                "<li>" +
                    "<h6 class='text-muted'>" +
                        "<a href='#' onclick=\"ToggleFacet($('#searchInputBox').val(), '', '" + facet.FieldName + "', '" + facetValue.Value + "')\">" +
                            facetValue.Title +
                        "</a>&#160;" +
                        "<span class='badge badge-default'>" +
                            facetValue.Count +
                        "</span>" +
                    "</h6>" +
                "</li>";
        });

    $("#navbar-activity-search-results .facet-values").append(listHtml);
    showPanels("facet-values");
}

function showPanels(panel) {
    if (panel) {
        $("#navbar-activity-search-results .panel-" + panel).show();
        $("#navbar-activity-search-results .panel-loading").hide();
    } else {
        if (!$("#navbar-activity-search-results").is(":visible")) {
            $("#navbar-activity-search-results .panel-results").hide();
            $("#navbar-activity-search-results .panel-facet-values").hide();
            $("#navbar-activity-search-results .panel-result-count").hide();
            $("#navbar-activity-search-results .panel-loading").show();
        }
    }
    $("#navbar-activity-search-results").show();
}

function getResultsTypeAhead(query) {
    //TODO: delay timer
    var ajaxPost = function (responseFunction, sender) {
        var token = $('#_CRSFform input[name=__RequestVerificationToken]').val();
        $.ajax({
            type: "POST",
            url: "/api/feature/search/ajaxsearch",
            cache: false,
            headers: { "__RequestVerificationToken": token },
            contentType: "application/json; charset=utf-8",
            data: "{\"Query\":\"" + query + "\"}",
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
        if (!success)
            return;
        console.debug(data);
        $("#navbar-activity-search-results .result-count").text(data.Count);
        showPanels("result-count");

        fillResults(data.Results.Results);
        fillFacetValues(data.Results.Facet, data.Results.FacetValues);
    });
    return false;
}
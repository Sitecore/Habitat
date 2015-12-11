(function (window, $) {
  $(window.document).ready(function () {
    loadJsMap();
  });

  var map = {};

  function loadJsMap() {
    var apiKey = $("#googleMapApiKey").val();
    if (!apiKey) {
      return;
    }
    var fileref = window.document.createElement("script");
    fileref.setAttribute("type", "text/javascript");
    fileref.setAttribute("src", "https://maps.googleapis.com/maps/api/js?key=" + apiKey + "&callback=initMaps");
    fileref.setAttribute("async", "");
    fileref.setAttribute("defer", "");

    window.document.getElementsByTagName("head")[0].appendChild(fileref);
  }

  window.initMaps = function () {
    initMapContainers();
    getMapPoints();
  };

  function initMapContainers() {
    //TODO: enable multiple map in one page support
    var elements = $(".map-container");
    var renderingParams = eval("("+$("#mapRenderingParameters").val()+")");
    $.each(elements, function (index, element) {
      var mapProperties = {
        center: { lat: -5.055576, lng: 3.803790 },
        zoom: 0,
        mapTypeId: google.maps.MapTypeId.HYBRID,
        zoomControl: true,
        mapTypeControl: true,
        scaleControl: true,
        streetViewControl: true,
        rotateControl: true
      };
      setMapProperties(mapProperties, renderingParams);
      map = new google.maps.Map(element, mapProperties);
    });
  }

  function setMapProperties(mapProperties, renderingParams) {
    if (renderingParams) {
      if (renderingParams.CenterLatitude && renderingParams.CenterLongitude) {
        mapProperties.center = { lat: parseFloat(renderingParams.CenterLatitude), lng: parseFloat(renderingParams.CenterLongitude) };
      }
      if (renderingParams.ZoomLevel) {
        mapProperties.zoom = parseInt(renderingParams.ZoomLevel);
      }
    }

    return renderingParams;
  }
  function getMapPoints() {
    var contextItemId = $("#mapContextItem").val();
    if (!contextItemId) {
      return;
    }

    $.ajax(
    {
      url: "/api/Maps/GetMapPoints",
      method: "POST",
      data: {
        itemId: contextItemId
      },
      success: function (data) {
        var infoWindow = new google.maps.InfoWindow({
          content: ""
        });
        $.each(data, function (index, office) {
          var contentString = "<h2>" + office.Name + "</h2>" +
            "<p>" + office.Address + "</p>";
          var marker = new google.maps.Marker({
            position: { lat: parseFloat(office.Latitude), lng: parseFloat(office.Longitude) },
            map: map,
            title: office.Name
          });
          google.maps.event.addListener(marker, "click", function () {
            infoWindow.setContent(contentString);
            infoWindow.open(map, marker);
          });
        });
      }
    });
  }
})(window, jQuery);
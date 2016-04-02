﻿(function (window, $, google) {
  "use strict";

  window.MapModule = window.MapModule || {};
  window.MapModule.markerClusters = [];

  $(window.document).ready(function () {
    var isPageEditor = $('body.pagemode-edit');
    if (isPageEditor.length)
      return;
    loadMapJsScript();
  });

  window.MapModule.initMaps = function () {
    initMapContainers();
  };
  window.MapModule.zoomToMapPoint = function (mapId, latitude, longitude) {
    var map = window.MapModule.getMap(mapId);
    if (map) {
      map.zoomToMapPoint(new google.maps.LatLng(latitude, longitude), 16);
    }
  };
  window.MapModule.getMap = function (mapId) {
    var mapFound;
    $.each(window.MapModule.markerClusters, function (index, markerCluster) {
      if (markerCluster.map.Id == mapId) {
        mapFound = markerCluster.map;
        return false;
      }
    });

    return mapFound;
  };

  function loadMapJsScript() {
    var scriptTag = $('script[data-key="gmapapi"]');
    if (scriptTag.length == 0) {
      var fileref = window.document.createElement("script");
      fileref.setAttribute("type", "text/javascript");
      fileref.setAttribute("src", "https://maps.googleapis.com/maps/api/js?callback=MapModule.initMaps");
      fileref.setAttribute("async", "");
      fileref.setAttribute("defer", "");
      fileref.setAttribute("data-key", "gmapapi");

      window.document.getElementsByTagName("head")[0].appendChild(fileref);
    }
  }

  function initMapContainers() {
    setMapPrototypes();
    var $elements = $(".map-canvas");
    $.each($elements, function (index, element) {
      var $element = $(element);
      var mapProperties = {
        center: { lat: -5.055576, lng: 3.803790 },
        zoom: 0,
        mapTypeId: google.maps.MapTypeId.ROADMAP,
        zoomControl: true,
        mapTypeControl: true,
        scaleControl: true,
        streetViewControl: true,
        rotateControl: true,
        centerMapControl: true
      };
      var $renderingParamsEl = $element.siblings('input[id="mapRenderingParameters"]');
      var renderingParams = {};
      if ($renderingParamsEl) {
        renderingParams = eval("(" + $renderingParamsEl.val() + ")");
        setMapProperties(mapProperties, renderingParams);
      }

      var map = new google.maps.Map(element, mapProperties);
      //assign unique id to map instance
      map.Id = Date.now();
      map.setCustomProperties(renderingParams);
      //render custom controls if any
      map.renderCustomControls();
      map.setDefaultView(mapProperties.center, mapProperties.zoom);
      map.set("styles", [{ "featureType": "water", "elementType": "geometry.fill", "stylers": [{ "color": "#d3d3d3" }] }, { "featureType": "transit", "stylers": [{ "color": "#808080" }, { "visibility": "off" }] }, { "featureType": "road.highway", "elementType": "geometry.stroke", "stylers": [{ "visibility": "on" }, { "color": "#b3b3b3" }] }, { "featureType": "road.highway", "elementType": "geometry.fill", "stylers": [{ "color": "#ffffff" }] }, { "featureType": "road.local", "elementType": "geometry.fill", "stylers": [{ "visibility": "on" }, { "color": "#ffffff" }, { "weight": 1.8 }] }, { "featureType": "road.local", "elementType": "geometry.stroke", "stylers": [{ "color": "#d7d7d7" }] }, { "featureType": "poi", "elementType": "geometry.fill", "stylers": [{ "visibility": "on" }, { "color": "#ebebeb" }] }, { "featureType": "administrative", "elementType": "geometry", "stylers": [{ "color": "#a7a7a7" }] }, { "featureType": "road.arterial", "elementType": "geometry.fill", "stylers": [{ "color": "#ffffff" }] }, { "featureType": "road.arterial", "elementType": "geometry.fill", "stylers": [{ "color": "#ffffff" }] }, { "featureType": "landscape", "elementType": "geometry.fill", "stylers": [{ "visibility": "on" }, { "color": "#efefef" }] }, { "featureType": "road", "elementType": "labels.text.fill", "stylers": [{ "color": "#696969" }] }, { "featureType": "administrative", "elementType": "labels.text.fill", "stylers": [{ "visibility": "on" }, { "color": "#737373" }] }, { "featureType": "poi", "elementType": "labels.icon", "stylers": [{ "visibility": "off" }] }, { "featureType": "poi", "elementType": "labels", "stylers": [{ "visibility": "off" }] }, { "featureType": "road.arterial", "elementType": "geometry.stroke", "stylers": [{ "color": "#d6d6d6" }] }, { "featureType": "road", "elementType": "labels.icon", "stylers": [{ "visibility": "off" }] }, {}, { "featureType": "poi", "elementType": "geometry.fill", "stylers": [{ "color": "#dadada" }] }]);

      var mapDataSourceItemId = $element.siblings('input[id="mapContextItem"]').val();
      if (mapDataSourceItemId) {
        getMapPoints(map, mapDataSourceItemId, function (markers) {          
          var markerCluster = new MarkerClusterer(map, markers);
          window.MapModule.markerClusters.push(markerCluster);
        });
      }
    });
  }

  function setMapPrototypes() {
    google.maps.Map.prototype.zoomToMapPoint = function (latlng, zoom) {
      this.setCenter(latlng);
      this.setZoom(zoom);
    };
    google.maps.Map.prototype.setDefaultView = function (latlng, zoom) {
      this.defaultCenter = latlng;
      this.defaultZoom = zoom;
    };
    google.maps.Map.prototype.resetToDefaultView = function (scope) {
      var $this = scope || this;
      $this.zoomToMapPoint($this.defaultCenter, $this.defaultZoom);
    };
    google.maps.Map.prototype.renderCustomControls = function () {
      // setCustomProperties() has to be called beforehand
      if (this.centerMapControl) {
        var centerMapControl = new CenterMapControl(this.resetToDefaultView, this);
        this.controls[google.maps.ControlPosition.TOP_CENTER].push(centerMapControl);
      }
    };
    google.maps.Map.prototype.setCustomProperties = function (properties) {
      this.centerMapControl = properties.EnableCenterMapControl;
    }
  }

  function setMapProperties(mapProperties, renderingParams) {
    if (renderingParams) {
      if (renderingParams.CenterLocation) {
        mapProperties.center = parseCoordinate(renderingParams.CenterLocation);
      }
      if (renderingParams.ZoomLevel) {
        var zoomLevel = parseInt(renderingParams.ZoomLevel);
        if (zoomLevel < 1)
          zoomLevel = 1;
        if (zoomLevel > 21)
          zoomLevel = 21;
        mapProperties.zoom = zoomLevel;
      }
      mapProperties.zoomControl = getCheckboxBooleanValue(renderingParams.EnableZoomControl);
      mapProperties.mapTypeControl = getCheckboxBooleanValue(renderingParams.EnableMapTypeControl);
      mapProperties.scaleControl = getCheckboxBooleanValue(renderingParams.EnableScaleControl);
      mapProperties.streetViewControl = getCheckboxBooleanValue(renderingParams.EnableStreetViewControl);
      mapProperties.rotateControl = getCheckboxBooleanValue(renderingParams.EnableRotateControl);
      mapProperties.MapTypeId = renderingParams.MapType;
    }

    return renderingParams;
  }

  function getCheckboxBooleanValue(value) {
    return value == "1" ? true : false;
  }
  function getMapPoints(map, mapDataSourceItemId, callback) {
    if (!map || !mapDataSourceItemId) {
      return;
    }

    $.ajax(
    {
      url: "/api/sitecore/Maps/GetMapPoints",
      method: "POST",
      data: {
        itemId: mapDataSourceItemId
      },
      success: function (data) {
        if (data.length == 1) {
          var marker = getMarker(map, data[0]);
          callback([marker]);
          map.setCenter(parseCoordinate(data[0].Location));
        } else {
          var markers = [];
          $.each(data, function (index, mapPoint) {
            var marker = getMarker(map, mapPoint);
            markers.push(marker);
          });
          callback(markers);
        }
      }
    });

    function getMarker(map, mapPoint) {
      var latlng = parseCoordinate(mapPoint.Location);
      if (latlng) {
        var marker = new google.maps.Marker({
          position: latlng,
          title: mapPoint.Name,
          icon: "http://maps.google.com/mapfiles/kml/pal4/icon56.png"
        });

        var contentString = "<h2>" + mapPoint.Name + "</h2>" +
          "<p>" + mapPoint.Address + "</p>" +
          "<a href='javascript:void(0)' onclick='MapModule.zoomToMapPoint(" + map.Id + "," + latlng.lat() + "," + latlng.lng() + ")'><span class='glyphicon glyphicon-zoom-in'/></a>";

        google.maps.event.addListener(marker, "click", function () {
          var infoWindow = new google.maps.InfoWindow({
            content: contentString
          });
          infoWindow.open(map, marker);
        });

        return marker;
      }
    }
  }

  function parseCoordinate(latlngLiteral) {
    if (latlngLiteral && latlngLiteral.split(",").length === 2) {
      var coordinates = latlngLiteral.split(",");
      var latitude = parseFloat(coordinates[0]);
      var longitude = parseFloat(coordinates[1]);

      return new google.maps.LatLng(latitude, longitude);
    }

    return null;
  }
  /*map custom controls*/
  function CenterMapControl(clickHandler, scope) {
    var $this = scope;
    var controlDiv = document.createElement('div');
    controlDiv.style.margin = "10px";

    // Set CSS for the control border.
    var controlUI = document.createElement("div");
    controlUI.style.backgroundColor = "#fff";
    controlUI.style.border = "2px solid #fff";
    controlUI.style.borderRadius = "3px";
    controlUI.style.boxShadow = "0 2px 6px rgba(0,0,0,.3)";
    controlUI.style.cursor = "pointer";
    controlUI.style.marginBottom = "22px";
    controlUI.style.textAlign = "center";
    controlUI.title = "Click to recenter the map";
    controlDiv.appendChild(controlUI);
    controlDiv.index = 1;

    // Set CSS for the control interior.
    var controlText = document.createElement("div");
    controlText.style.color = "rgb(25,25,25)";
    controlText.style.fontFamily = "Roboto,Arial,sans-serif";
    controlText.style.fontSize = "11px";
    controlText.style.lineHeight = "28px";
    controlText.style.paddingLeft = "5px";
    controlText.style.paddingRight = "5px";
    controlText.innerHTML = "Center Map";
    controlUI.appendChild(controlText);

    // Setup the click event listeners: 
    controlUI.addEventListener("click", function () {
      clickHandler($this);
    });

    return controlDiv;
  }
})(window, jQuery, google = window.google || {});
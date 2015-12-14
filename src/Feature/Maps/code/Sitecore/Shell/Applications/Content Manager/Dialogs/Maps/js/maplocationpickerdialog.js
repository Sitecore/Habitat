(function (window, google, $) {
  $(document).ready(function () {
    initializeMap();
    registerEventHandlers();
  });

  var map;
  var geocoder;
  var marker;

  //return current position value by using Latitude Longitude Format
  function getCurrentLocation() {
    var currentLoc = $("input#TxtSelectedLocation").val();

    return currentLoc;
  }

  window.initializeMap = function () {

    var currentLoc = getCurrentLocation();
    var lat = -34.397;
    var lng = 150.644;

    if (currentLoc != "") {
      var position = currentLoc.split(",");

      if (position.length == 2) {
        lat = parseFloat(position[0]);
        lng = parseFloat(position[1]);
      }
    }

    if (map == null) {
      var mapOptions = {
        zoom: 8,
        center: new google.maps.LatLng(lat, lng),
        mapTypeId: google.maps.MapTypeId.ROADMAP,
        panControl: false,
        zoomControl: true,
        scaleControl: true,
        streetViewControl: false
      };

      var mapDiv = $("div.mapCanvas").get(0);

      map = new google.maps.Map(mapDiv, mapOptions);
      placeMarker(mapOptions.center);

      //add event listener
      google.maps.event.addListener(map, "click", function (event) {
        if (marker != null) {
          placeMarker(event.latLng);
        }
      });

      google.maps.event.addListener(marker, "dragend", function (event) {
        map.panTo(event.latLng);

        updateLocation(event.latLng);
      });
    }
  };
  function updateMapLocation(address) {
    if (address != "") {
      if (geocoder == null) {
        geocoder = new google.maps.Geocoder();
      }

      geocoder.geocode({ 'address': address }, function (result, status) {
        if (status == google.maps.GeocoderStatus.OK) {
          placeMarker(result[0].geometry.location);
        } else {
          alert("Geocode was not successfull for the following reason: " + status);
        }
      });
    }
  };

  function placeMarker(latLng) {
    if (marker == null) {
      marker = new google.maps.Marker({
        map: map,
        draggable: true
      });
    }

    marker.setPosition(latLng);
    marker.setAnimation(google.maps.Animation.DROP);

    map.panTo(latLng);
    updateLocation(latLng);
  }

  function updateLocation(latLng) {
    $("input#TxtSelectedLocation").val(latLng.lat().toString() + "," + latLng.lng().toString());
  }

  function registerEventHandlers() {
    $('#btnUpdateMap').click(function (e) {
      e.preventDefault();

      var $addressTextbox = $('input#txtSearchAddress');
      if ($addressTextbox.val() == "") {
        $addressTextbox.closest('div.form-element').addClass('error');

        return;
      }

      $addressTextbox.closest('div.form-element').removeClass('error');
      updateMapLocation($addressTextbox.val());
    });
    $('input#txtSearchAddress').keypress(function (e) {
      if (e.keyCode == 13) {
        $('#btnUpdateMap').click();
      }
    });
  } 
})(window, window.google, jQuery);
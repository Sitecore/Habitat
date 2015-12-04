(function (window, $) {
  var map = {};

  window.initMaps = function () {
    initMaps();
    initOffice();
  };

  function initMaps() {
    var elements = $(".map-container");
    $.each(elements, function (index, element) {
      map = new google.maps.Map(element, {
        center: { lat: -5.055576, lng: 3.803790 },
        zoom: 2
      });
    });
  }

  function initOffice() {
    var contextItemId = $("#mapContextItem").val();
    if (!contextItemId) {
      return;
    }

    $.ajax(
    {
      url: "/api/Office/GetOfficeCoordinates",
      method: "POST",
      data: {
        itemId: contextItemId
      },
      success: function (data) {        
        var infoWindow = new google.maps.InfoWindow({
          content: ""
        });
        $.each(data, function (index, office) {
          var contentString = '<h1>' + office.Name + '</h1>' +
            '<p>' + office.Address + '</p>';          
          var marker = new google.maps.Marker({
            position: { lat: parseFloat(office.Latitude), lng: parseFloat(office.Longitude) },
            map: map,
            title: office.Name
          });
          google.maps.event.addListener(marker, 'click', function () {
            infoWindow.setContent(contentString);
            infoWindow.open(map, marker);
          });
        });
      }
    });
  }
})(window, jQuery);
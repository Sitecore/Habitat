// CONTACT MAP

var PageContact = function() {

	var _init = function() {

		var mapbg = new GMaps({
			div: '#gmapbg',
			lat: 3.118823,
			lng: 101.676084,
			scrollwheel: false
		});


		mapbg.addMarker({
			lat: 3.118823,
			lng: 101.676084,
			title: 'Your Location',
			infoWindow: {
				content: '<h3>Jango Inc.</h3><p>25, Lorem Lis Street, Orange C, California, US</p>'
			}
		});
	}

    return {
        //main function to initiate the module
        init: function() {

            _init();

        }

    };
}();

$(document).ready(function() {
    PageContact.init();
});
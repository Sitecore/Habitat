// CONTACT MAP

var PageContact2 = function() {

	var _init = function() {

		var mapbg = GMaps.createPanorama({
			el: '#gmapbg',
			lat: 2.921318,
			lng: 101.6559349,
			scrollwheel: false
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
    PageContact2.init();
});
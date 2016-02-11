jQuery(function () {
    $ = jQuery;
  var $carouselArray = $(".carousel");
  $carouselArray.each(function () {
    var $carousel = $(this);
    var $videos = $carousel.find("video");
    $videos.bind("ended", function () {
      $carousel.carousel('next');
      $carousel.carousel('cycle');
    });

    $videos.each(function () {
      var videoControl = this;
      $carousel.on('slid.bs.carousel', function (e) {
        if ($(e.relatedTarget).is($(videoControl).parent())) {
          if (videoControl.readyState === 4) {
            $carousel.carousel('pause');
            videoControl.play();
          }
        };
      });
    });
  });
});
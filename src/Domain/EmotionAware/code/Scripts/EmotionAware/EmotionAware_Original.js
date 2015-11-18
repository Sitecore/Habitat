// Normalize getUserMedia and URL
// https://gist.github.com/f2ac64ed7fc467ccdfe3

//normalize window.URL
window.URL || (window.URL = window.webkitURL || window.msURL || window.oURL);

//normalize navigator.getUserMedia
navigator.getUserMedia || (navigator.getUserMedia = navigator.webkitGetUserMedia || navigator.webkitGetUserMedia || navigator.mozGetUserMedia || navigator.msGetUserMedia);

if (typeof navigator.getUserMedia === "function") {

    (function () {

        var video = document.createElement('video'),
            canvas = document.createElement('canvas'),
            context = canvas.getContext('2d'),
            localMediaStream = null,
            snap = false;

        // toString for older gUM implementation, see comments on https://gist.github.com/f2ac64ed7fc467ccdfe3
        gUMOptions = { video: true, toString: function () { return "video"; } };

        canvas.setAttribute('width', 400);
        canvas.setAttribute('height', 320);

        video.setAttribute('autoplay', true);

        context.fillStyle = "rgba(0, 0, 200, 0.5)";
        navigator.getUserMedia(gUMOptions, handleWebcamStream, errorStartingStream);

        function handleWebcamStream(stream) {
            video.src = (window.URL && window.URL.createObjectURL) ? window.URL.createObjectURL(stream) : stream;
            localMediaStream = stream;
            processWebcamVideo();
        }

        function errorStartingStream() {
            alert('Uh-oh, the webcam didn\'t start. Do you have a webcam? Did you give it permission? Refresh to try again.');
        }

        function snapshot(faces) {
            if (!faces) {
                return false;
            }

            for (var i = 0; i < faces.length; i++) {
                var face = faces[i];

                if (face.height > 35) {
                    if (localMediaStream) {

                        // Other browsers will fall back to image/png.
                        document.querySelector('img').src = canvas.toDataURL('image/jpeg', 0.5);
                        $("#stringBase").val(canvas.toDataURL('image/jpeg', 0.5));
                        return true;
                    }
                }
            }

            return false;


        }

        function processWebcamVideo() {

            var startTime = +new Date();

            context.drawImage(video, 0, 0, canvas.width, canvas.height);

            var faces = detectFaces();

            if (!snap) {
                snap = snapshot(faces);
            }

            // Log process time
            console.log(+new Date() - startTime);

            // And repeat.
            if (!snap) {
                setTimeout(processWebcamVideo, 50);
            } else {
                localMediaStream.stop();
            }
        }

        function detectFaces() {
            // What do these parameters mean?
            // I couldn't find any documentation, and used what was found here:
            // https://github.com/liuliu/ccv/blob/unstable/js/index.html

            return ccv.detect_objects({ canvas: (ccv.pre(canvas)), cascade: cascade, interval: 2, min_neighbors: 1 });
        }


        /* Face object example:
        {
            confidence: 0.16752329000000035,
            height: 48.500000000000014,
            neighbors: 1,
            width: 48.500000000000014,
            x: 80.50000000000001,
            y: 104.50000000000003
        }
        */

    })();
}

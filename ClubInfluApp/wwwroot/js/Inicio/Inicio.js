$(document).ready(function () {
    $("#play-button").on("click", function () {
        var $videoPlayer = $("#video-player");
        var $videoThumbnail = $("#video-thumbnail");
        var $playButton = $("#play-button");

        $videoThumbnail.addClass("d-none");
        $playButton.addClass("d-none");

        $videoPlayer.removeClass("d-none")[0].play();
    });
});

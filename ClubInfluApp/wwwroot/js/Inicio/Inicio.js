document.getElementById("play-button").addEventListener("click", function () {
    var videoPlayer = document.getElementById("video-player");
    var videoThumbnail = document.getElementById("video-thumbnail");
    var playButton = document.getElementById("play-button");

    videoThumbnail.classList.add("d-none");
    playButton.classList.add("d-none");

    videoPlayer.classList.remove("d-none");
    videoPlayer.play();
});
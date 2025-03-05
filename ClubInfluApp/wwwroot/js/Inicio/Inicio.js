document.getElementById("play-button").addEventListener("click", function () {
    var videoPlayer = document.getElementById("video-player");
    var videoThumbnail = document.getElementById("video-thumbnail");
    var playButton = document.getElementById("play-button");

    // Ocultar la imagen y el botón de play
    videoThumbnail.classList.add("d-none");
    playButton.classList.add("d-none");

    // Mostrar el video y reproducirlo
    videoPlayer.classList.remove("d-none");
    videoPlayer.play();
});
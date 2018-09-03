var styles = {

    width: "600px",
    height: "500px"
};
var styles_none = {

    width: "auto",
    height: "auto"
};
var styles_rig = {

    width: "580px",
    height: "425px"
};
var styles_left = {

    width: "540px",
    height: "400px"
};
var styles_initialPosition = {

    width: "100%"
};
function openModal() {
    document.getElementById("myModal").style.display = "block";
}

function closeModal() {
    document.getElementById("myModal").style.display = "none";
}

var slideIndex = 1;
showSlides(slideIndex);

function plusSlides(n) {
    showSlides(slideIndex += n);
}

function currentSlide(n) {
    showSlides(slideIndex = n);
}

function showSlides(n) {
    var i;
    var slides = document.getElementsByClassName("mySlides");
    var dots = document.getElementsByClassName("demo");
    var captionText = document.getElementById("caption");
    if (n > slides.length) { slideIndex = 1 }
    if (n < 1) { slideIndex = slides.length }
    for (i = 0; i < slides.length; i++) {
        slides[i].style.display = "none";
    }
    for (i = 0; i < dots.length; i++) {
        dots[i].className = dots[i].className.replace(" active", "");
    }
    slides[slideIndex - 1].style.display = "block";
    dots[slideIndex - 1].className += " active";
    captionText.innerHTML = dots[slideIndex - 1].alt;
    //$(".imagenClase").each(function () {
    //    alert(this.src);
    //});
}

function rotateRight() {
    $(".mySlides").removeClass("image_left");

    $(".mySlides").addClass("image_right");
    $(".imgClass").css(styles_rig);
    $("#modalClass").css(styles);
}

function rotateLeft() {
    $(".mySlides").removeClass("image_right");
    $(".mySlides").addClass("image_left");
    $(".imgClass").css(styles_left);
    $("#modalClass").css(styles);
}

function initialPosition() {
    $(".mySlides").removeClass("image_right");
    $(".mySlides").removeClass("image_left");
    $(".imgClass").css(styles_initialPosition);
    $("#modalClass").css(styles_none);
}

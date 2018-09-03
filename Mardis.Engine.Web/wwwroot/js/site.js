// Write your Javascript code.
jQuery(document).ready(function() {
   $(".navigation-toggler").on("click", function () {
        if (!$("body").hasClass("navigation-small")) {
            $("body").addClass("navigation-small");
        } else {
            $("body").removeClass("navigation-small");
        };
   });

   $(".dropdown-toggle").dropdown();
});

function clickMenu() {
    $(".navigation-toggler").click();
}


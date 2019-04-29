// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {

    // This is a javascript comment.
    $(".navShowHide").on("click", function () {

        var main = $("#mainSectionContainer");
        var nav = $("#sideNavContainer");

        if (main.hasClass("leftPadding")) {
            nav.hide();
        }
        else {
            nav.show();
        }

        main.toggleClass("leftPadding");

    });

});
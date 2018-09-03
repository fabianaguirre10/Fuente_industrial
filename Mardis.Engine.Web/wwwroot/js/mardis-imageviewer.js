$(function () {
    "use strict";

    debugger;

    var console = window.console || { log: function () { } };
    var $images = $(".docs-pictures");
    var options = {
        // inline: true,
        url: "data-original",
        build: function (e) {
            console.log(e.type);
        },
        built: function (e) {
            console.log(e.type);
        },
        show: function (e) {
            console.log(e.type);
        },
        shown: function (e) {
            console.log(e.type);
        },
        hide: function (e) {
            console.log(e.type);
        },
        hidden: function (e) {
            console.log(e.type);
        },
        view: function (e) {
            console.log(e.type);
        },
        viewed: function (e) {
            console.log(e.type);
        }
    };

    //$images.viewer();

    $images.on({
        'build.viewer': function (e) {
            console.log(e.type);
        },
        'built.viewer': function (e) {
            console.log(e.type);
        },
        'show.viewer': function (e) {
            console.log(e.type);
        },
        'shown.viewer': function (e) {
            console.log(e.type);
        },
        'hide.viewer': function (e) {
            console.log(e.type);
        },
        'hidden.viewer': function (e) {
            console.log(e.type);
        },
        'view.viewer': function (e) {
            console.log(e.type);
        },
        'viewed.viewer': function (e) {
            console.log(e.type);
        }
    }).viewer(options);

});
function LoadMarkers(localizations) {

    if (!localizations[0].LatitudeBranch) {
        return;
    }

    if (localizations.length === 1) {
        centro = new window.google.maps.LatLng(localizations[0].LatitudeBranch,
            localizations[0].LenghtBranch);
    }

    Initmap();

    var data = localizations;
    $.each(data, function (i, item) {
        var latitud = parseFloat(item.LatitudeBranch);
        var longitud = parseFloat(item.LenghtBranch);
        var marker = new window.google.maps.Marker({
            'position': new window.google.maps.LatLng(latitud, longitud),
            'map': map,
            'title': item.Name
        });

        marker.setIcon('http://maps.google.com/mapfiles/ms/icons/blue-dot.png');

        var infowindow = new window.google.maps.InfoWindow({
            content: "<div class='infoDiv'><h2>" + item.Name + "</h2></div>"
        });
    });
}

function Initmap() {
    var mapOptions = {
        zoom: 8,
        center: centro,
        mapTypeId: window.google.maps.MapTypeId.ROADMAP
    };
    window.google.maps.visualRefresh = true;
    map = new window.google.maps.Map(document.getElementById("map_canvas"), mapOptions);
    window.google.maps.event.trigger(map, "resize");
}

var markersArray = [];
var map;
//Se pone como centro del mapa a Quito
var centro = new window.google.maps.LatLng(-2.125474455, -79.89589785);

function NewMap(point) {

    if (point) {
        var marker = new window.google.maps.Marker({
            'position': new window.google.maps.LatLng(point.latitud, point.longitud),
            'map': map,
            'title': point.Name
        });
    }

    Initmap();

    window.google.maps.event.addListener(map, "click", function (event) {
        // place a marker
        placeMarker(event.latLng);

        $("#Latitude").val(event.latLng.lat);
        $("#Longitude").val(event.latLng.lat);
    });
}

function placeMarker(location) {

    deleteOverlays();

    var marker = new window.google.maps.Marker({
        position: location,
        map: map
    });

    markersArray.push(marker);
}

// Deletes all markers in the array by removing references to them
function deleteOverlays() {
    if (markersArray) {
        for (i in markersArray) {
            markersArray[i].setMap(null);
        }
        markersArray.length = 0;
    }
}

function UploadImage() {

}

function DrawCompleteMap(localizations) {
    Initmap();
    var data = localizations;
    $.each(data, function (i, item) {
        var latitud = parseFloat(item.Latitude);
        var longitud = parseFloat(item.Longitude);
        var marker = new window.google.maps.Marker({
            'position': new window.google.maps.LatLng(latitud, longitud),
            'map': map,
            'title': item.Title
        });

        marker.setIcon(item.IconUrl);

        var infowindow = new window.google.maps.InfoWindow({
            content: GetBody(item)
        });

        window.google.maps.event.addListener(marker, 'click', function () {
            infowindow.open(map, marker);
        });
    });
}

function GetBody(data) {
    var body = "" +
        "<div class='infoDiv'>" +
        "   <h4>" + data.Title + "</h4>" +
        "   <a href='/Campaign/TaskPoll?idTask=" + data.task + "' target='_blank'>Ir a Tarea</a>" +
        "   <div class='row'>" +
      //  "       <div class='col-sm-3'>" +
       // "           <image src='" + data.ImageUrl + "'></image>" +
    //    "       </div>" +
        "       <div class='col-sm-9'>" +
        "           <div class='row'>" +
        "               <div class='col-sm-4'><strong>Local</strong></div>" +
        "               <div class='col-sm-8'>(" + data.CodeBranch + ") " + data.NameBranch + "</div>" +
        "           </div>" +
        "           <div class='row'>" +
        "              <div class='col-sm-4'></div>" +
        "              <div class='col-sm-8'></div>" +
        "           </div>" +
        "       </div>" +
        "   </div>" +
        "</div>";
    return body;
}
"use strict";

function initMap() {
    let leicester = { lat: 52.6333, lng: -1.1333 };
    let dmu = { lat: 52.629311, lng: -1.137836 };
    /*  let uol = {lat: 52.620956, lng: -1.124049};*/
    let mapDemo = document.getElementById("map");

    // this sets the default location for when the map is first loaded
    let map = new google.maps.Map(mapDemo, {
        zoom: 13,
        center: leicester
    });

    // these set different markers you want to show on your map
    let markerDMU = new google.maps.Marker({
        position: dmu,
        map: map,
        title: 'The Good Guys'
    });

}
$(document).ready(function () {


    $("#wait").css("display", "block");
    $("#googleMap").css("display", "none");
    $.ajax({
        type: "post",
        url: "/Location/LocatioAdmin?date=",
        datatype: "json",
        traditional: true,
        success: function (data) {
            $("#wait").css("display", "none");
            $("#googleMap").css("display", "block");
            if (data.length == 0) {
                var map = new google.maps.Map(document.getElementById('googleMap'), {
                    zoom: 10,
                    center: new google.maps.LatLng($('#deflat').val(), $('#deflang').val()),
                    mapTypeId: google.maps.MapTypeId.ROADMAP
                });

            }
            var map = new google.maps.Map(document.getElementById('googleMap'), {

                center: new google.maps.LatLng(data[0].lat, data[0].log),
                mapTypeId: google.maps.MapTypeId.ROADMAP
            });

            var infowindow = new google.maps.InfoWindow();
            bounds = new google.maps.LatLngBounds();
            var marker, i;
            // Get Addres
            for (i = 0; i < data.length; i++) {
                marker = new google.maps.Marker({
                    position: new google.maps.LatLng(data[i].lat, data[i].log),
                    map: map,

                    icon: {
                        labelOrigin: new google.maps.Point(16, 65),
                        url: "../Content/images/img/marker24.png"
                    },
                    label: {
                        text: data[i].userName,
                        color: "black",
                        fontWeight: "bold",
                        fontSize: "13px",
                        margin: "55px"
                    }
                });

                loc = new google.maps.LatLng(data[i].lat, data[i].log),

                    bounds.extend(loc);

                google.maps.event.addListener(marker, 'click', (function (marker, i) {
                    return function () {
                        infowindow.setContent('<div class=infowindow> <h5 style="font-size:13px">' + data[i].userName + '</h5></div>');
                        infowindow.open(map, marker);
                    }
                })(marker, i));


            }
            map.fitBounds(bounds);
            map.panToBounds(bounds);

        }

    });

    
    $("#demoGrid").DataTable({
        "sDom": "ltipr",
        "order": [[2, "desc"]],
        "processing": true, // for show progress bar
        "serverSide": true, // for process server side
        "filter": true, // this is for disable filter (search box)
        "orderMulti": false, // for disable multiple column at once
        "pageLength": 10,

        "ajax": {
            "url": "/Datable/GetJqGridJson?rn=AdminCount",
            "type": "POST",
            "datatype": "json"
        },

        "columnDefs":
            [],

        "columns": [
            { "data": "Name", "name": "Name", "autoWidth": false },
            { "data": "attenemplyee", "name": "attenemplyee", "autoWidth": false },
            { "data": "total", "name": "total", "autoWidth": false },
            { "data": "mixed", "name": "mixed", "autoWidth": false },
            { "data": "bybufer", "name": "bybufer", "autoWidth": false },
            { "data": "notrecivedcol", "name": "notrecivedcol", "autoWidth": false },
            { "data": "notspecified", "name": "notspecified", "autoWidth": false },
            { "data": "TotalLiquidCount", "name": "TotalLiquidCount", "autoWidth": false },
            { "data": "TotalStreetCount", "name": "TotalStreetCount", "autoWidth": false },


        ]
    });

    //setInterval(function () {
    //    Search()
    //}, 6000000);


});




function Search() {
    var value = ",,," + $("#s").val();//txt_fdate + "," + txt_tdate + "," + UserId + "," + Client + "," + NesEvent + "," + Product + "," + catProduct + "," + 1;
    // alert(value );
    oTable = $('#demoGrid').DataTable();
    oTable.search(value).draw();
    oTable.search("");
    //document.getElementById('USER_ID_FK').value = -1;
}



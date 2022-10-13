
$(document).ready(function () {
    if ($(window).width() < 500) {
        $('.col').addClass('col-6');
        $('.col').append('<br/><br/>');

    }
    //if ($("#asdf").text() != "namratas@appynitty.com!") {
    //    $(".custom_col_hide").css("display", "block")
    //    $(".custom_col").removeClass('col-xl-3')
    //    $(".custom_col").addClass('col-xl-4')
    //}
    if ($("#asdf").text() == "ralegaon@np.com!" || $("#asdf").text() == "badnapur@appynitty.com!" || $("#asdf").text() == "jamkhed@np.com!" || $("#asdf").text() == "shevgaon@np.com!") {

        $(".custom_col_hide").css("display", "none")
        $(".custom_col").removeClass('col-xl-3')
        $(".custom_col").addClass('col-xl-4')
    }
    //if ($("#asdf").text() == "satana@np.com!") {
    //    $(".custom_col_hide").css("display", "block")
    //    $(".custom_col").removeClass('col-xl-3')
    //    $(".custom_col").addClass('col-xl-4')
    //}
    $('#refresh').click(function () {

        myMap2();

    });
    $('#refresh2').click(function () {

        myMapHouse();

    });
})
//Dashboardmap 1 (attendance)
function myMap2() {
    $("#wait").css("display", "block");
    $("#googleMap").css("display", "none");
    $.ajax({
        type: "post",
        url: "/Liquid/LiquidLocation/LocatioList?date=" + $('#txt_fdate').val(),
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
                        url: "../../Areas/Liquid/Content/images/img/marker24.png"
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

                //New Marker Click
                google.maps.event.addListener(marker, 'click', (function (marker, i) {
                    return function () {
                        
                        var latlng = new google.maps.LatLng(data[i].lat, data[i].log);
                        var geocoder = geocoder = new google.maps.Geocoder();
                        geocoder.geocode({ 'latLng': latlng }, function (results, status) {
                            if (status == google.maps.GeocoderStatus.OK) {
                                
                                if (results[0]) {
                                    //alert("Location: " + results[1].formatted_address);
                                    var address = results[0].formatted_address;
                                    infowindow.setContent('<div class=infowindow> <h3>' + data[i].userName + '</h3><h5><b>Details:</b></h5><p><b>Date:</b>' + data[i].date + '</p><p><b>Time:</b>' + data[i].time + '</p><p><b>Mobile:</b>' + data[i].userMobile + '<p><b>Vehicle No:</b>' + data[i].vehcileNumber + '</p><div style="height:auto; width:150px"><p><b>Address:</b>' + address + '</p></div></div>');
                                    infowindow.open(map, marker);
                                }
                                else {
                                    var address = "Not Mention";
                                    infowindow.setContent('<div class=infowindow> <h3>' + data[i].userName + '</h3><h5><b>Details:</b></h5><p><b>Date:</b>' + data[i].date + '</p><p><b>Time:</b>' + data[i].time + '</p><p><b>Mobile:</b>' + data[i].userMobile + '<p><b>Vehicle No:</b>' + data[i].vehcileNumber + '</p><div style="height:auto; width:150px"><p><b>Address:</b>' + address + '</p></div></div>');
                                    infowindow.open(map, marker);
                                }
                            }
                        });


                    }
                })(marker, i));

                //Old Marker Click
                //google.maps.event.addListener(marker, 'click', (function (marker, i) {
                //    return function () {
                //        infowindow.setContent('<div class=infowindow> <h3>' + data[i].userName + '</h3><h5><b>Details:</b></h5><p><b>Date:</b>' + data[i].date + '</p><p><b>Time:</b>' + data[i].time + '</p><p><b>Mobile:</b>' + data[i].userMobile + '<p><b>Vehicle No:</b>' + data[i].vehcileNumber + '</p><div style="height:auto; width:150px"><p><b>Address:</b>' + data[i].address + '</p></div></div>');
                //        infowindow.open(map, marker);
                //    }
                //})(marker, i));


            }
            map.fitBounds(bounds);
            map.panToBounds(bounds);

        }

    });
}
//dashboard map2 (house on map)
function myMapHouse() {
    $("#waithouse").css("display", "block");
    $("#googleMapHouse").css("display", "none");
    $.ajax({
        type: "post",
        url: "/Location/HouseLocationList?date=" + $('#txt_fdate').val(),
        datatype: "json",
        traditional: true,
        success: function (data) {
            $("#waithouse").css("display", "none");
            $("#googleMapHouse").css("display", "block");
            if (data.length == 0) {
                var map = new google.maps.Map(document.getElementById('googleMapHouse'), {
                    zoom: 10,
                    center: new google.maps.LatLng($('#deflathouse').val(), $('#deflanghouse').val()),
                    mapTypeId: google.maps.MapTypeId.ROADMAP
                });

            }
            var map = new google.maps.Map(document.getElementById('googleMapHouse'), {

                center: new google.maps.LatLng(data[0].houseLat, data[0].houseLong),
                mapTypeId: google.maps.MapTypeId.ROADMAP
            });

            var infowindow = new google.maps.InfoWindow();
            bounds = new google.maps.LatLngBounds();
            var marker, i;
            // Get Addres
            for (i = 0; i < data.length; i++) {
                if (data[i].houseOwnerName == null) {
                    data[i].houseOwnerName = 'Not Available';
                }
                if (data[i].houseOwnerMobile == null) {
                    data[i].houseOwnerMobile = '';
                }
                if (data[i].houseAddress == null) {
                    data[i].houseAddress = '';
                }
                if (data[i].garbageType == 0) {
                    marker = new google.maps.Marker({
                        position: new google.maps.LatLng(data[i].houseLat, data[i].houseLong),
                        map: map,

                        icon: {
                            labelOrigin: new google.maps.Point(16, 65),
                            url: "../../Areas/Liquid/Content/images/img/segregationImg/icn_mixed_garbage.png"
                        },
                        label: {
                            //text: data[i].userName,
                            color: "black",
                            fontWeight: "bold",
                            fontSize: "13px",
                            margin: "55px"
                        }
                    });

                    loc = new google.maps.LatLng(data[i].houseLat, data[i].houseLong),

                bounds.extend(loc);

                    google.maps.event.addListener(marker, 'click', (function (marker, i) {
                        return function () {
                            infowindow.setContent('<div class=infowindow style="max-width:190px"> <h3>' + data[i].houseOwnerName + '</h3><p><b>House Id:</b>' + data[i].ReferanceId + '</p><p><b>Mobile:</b>' + data[i].houseOwnerMobile + '</p><p><b>Address:</b>' + data[i].houseAddress + '<p><b>Date:</b>' + data[i].gcDate + '</p><div style="height:auto; width:150px"><p><b>Time:</b>' + data[i].gcTime + '</p></div></div>');
                            infowindow.open(map, marker);
                        }
                    })(marker, i));
                }
                if (data[i].garbageType == 1) {
                    marker = new google.maps.Marker({
                        position: new google.maps.LatLng(data[i].houseLat, data[i].houseLong),
                        map: map,

                        icon: {
                            labelOrigin: new google.maps.Point(16, 65),
                            url: "../../Areas/Liquid/Content/images/img/segregationImg/icn_segregated_garbage.png"
                        },
                        label: {
                            //text: data[i].userName,
                            color: "black",
                            fontWeight: "bold",
                            fontSize: "13px",
                            margin: "55px"
                        }
                    });

                    loc = new google.maps.LatLng(data[i].houseLat, data[i].houseLong),

                bounds.extend(loc);

                    google.maps.event.addListener(marker, 'click', (function (marker, i) {
                        return function () {
                            infowindow.setContent('<div class=infowindow style="max-width:190px"> <h3>' + data[i].houseOwnerName + '</h3><p><b>House Id:</b>' + data[i].ReferanceId + '</p><p><b>Mobile:</b>' + data[i].houseOwnerMobile + '</p><p><b>Address:</b>' + data[i].houseAddress + '<p><b>Date:</b>' + data[i].gcDate + '</p><div style="height:auto; width:150px"><p><b>Time:</b>' + data[i].gcTime + '</p></div></div>');
                            infowindow.open(map, marker);
                        }
                    })(marker, i));
                }
                if (data[i].garbageType == 2) {
                    marker = new google.maps.Marker({
                        position: new google.maps.LatLng(data[i].houseLat, data[i].houseLong),
                        map: map,

                        icon: {
                            labelOrigin: new google.maps.Point(16, 65),
                            url: "../../Areas/Liquid/Content/images/img/segregationImg/icn_garbage_not_recevied.png"
                        },
                        label: {
                            //text: data[i].userName,
                            color: "black",
                            fontWeight: "bold",
                            fontSize: "13px",
                            margin: "55px"
                        }
                    });

                    loc = new google.maps.LatLng(data[i].houseLat, data[i].houseLong),

                bounds.extend(loc);

                    google.maps.event.addListener(marker, 'click', (function (marker, i) {
                        return function () {
                            infowindow.setContent('<div class=infowindow style="max-width:190px"> <h3>' + data[i].houseOwnerName + '</h3><p><b>House Id:</b>' + data[i].ReferanceId + '</p><p><b>Mobile:</b>' + data[i].houseOwnerMobile + '</p><p><b>Address:</b>' + data[i].houseAddress + '<p><b>Date:</b>' + data[i].gcDate + '</p><div style="height:auto; width:150px"><p><b>Time:</b>' + data[i].gcTime + '</p></div></div>');
                            infowindow.open(map, marker);
                        }
                    })(marker, i));
                }
                if (data[i].garbageType == 3) {
                    marker = new google.maps.Marker({
                        position: new google.maps.LatLng(data[i].houseLat, data[i].houseLong),
                        map: map,

                        icon: {
                            labelOrigin: new google.maps.Point(16, 65),
                            url: "../../Areas/Liquid/Content/images/img/segregationImg/icn_not_specified.png"
                        },
                        label: {
                            //text: data[i].userName,
                            color: "black",
                            fontWeight: "bold",
                            fontSize: "13px",
                            margin: "55px"
                        }
                    });

                    loc = new google.maps.LatLng(data[i].houseLat, data[i].houseLong),

                bounds.extend(loc);

                    google.maps.event.addListener(marker, 'click', (function (marker, i) {
                        return function () {
                            infowindow.setContent('<div class=infowindow> <h3>' + data[i].houseOwnerName + '</h3><p><b>House Id:</b>' + data[i].ReferanceId + '</p><p><b>Mobile:</b>' + data[i].houseOwnerMobile + '</p><p><b>Address:</b>' + data[i].houseAddress + '<p><b>Date:</b>' + data[i].gcDate + '</p><div style="height:auto; width:150px"><p><b>Time:</b>' + data[i].gcTime + '</p></div></div>');
                            infowindow.open(map, marker);
                        }
                    })(marker, i));
                }
                if (data[i].garbageType == null) {
                    marker = new google.maps.Marker({
                        position: new google.maps.LatLng(data[i].houseLat, data[i].houseLong),
                        map: map,

                        icon: {
                            labelOrigin: new google.maps.Point(16, 65),
                            url: "../../Areas/Liquid/Content/images/img/segregationImg/icn_house.png"
                        },
                        label: {
                            //text: data[i].userName,
                            color: "black",
                            fontWeight: "bold",
                            fontSize: "13px",
                            margin: "55px"
                        }
                    });

                    loc = new google.maps.LatLng(data[i].houseLat, data[i].houseLong),

                bounds.extend(loc);

                    google.maps.event.addListener(marker, 'click', (function (marker, i) {
                        return function () {
                            infowindow.setContent('<div class=infowindow style="max-width:190px"> <h3>' + data[i].houseOwnerName + '</h3><p><b>House Id:</b>' + data[i].ReferanceId + '</p><p><b>Mobile:</b>' + data[i].houseOwnerMobile + '</p><p><b>Address:</b>' + data[i].houseAddress + '</div>');
                            infowindow.open(map, marker);
                        }
                    })(marker, i));
                }





            }
            map.fitBounds(bounds);
            map.panToBounds(bounds);

        }

    });
}


//Bar Chart

$(document).ready(function () {

    var UserId = -1;

    $.ajax({
        type: "post",
        url: "/GarbageCollection/UserCount",
        data: { userId: UserId, },
        datatype: "json",
        traditional: true,
        success: function (data) {

            var ary = []
            var ary2 = []
            for (var i = 0; i < data.length; i++) {
                var name = data[i].UserName;
                name = name.trim();
                var fname = name.substring(0, name.indexOf(" "));
                ary.push({ label: fname, y: data[i]._Count, z: data[i].StartTime });

                //bb.push(data[i].UserName);

                // alert(data[i]._Count);

            }
            //console.log(ary);
            // console.log(ary2);

            var chart = new CanvasJS.Chart("chartContainer", {
                theme: "light1", // "light1", "ligh2", "dark1", "dark2"
                animationEnabled: true,
                axisY: {
                    title: "Liquid Waste Cleaning Point",
                },
                title: {
                    // text: "घर संकलन"
                },
                toolTip: {
                    content: "In Time: {z}",
                },
                data: [{
                    type: "column",
                    indexLabel: "{y}",
                    indexLabelFontSize: 14,
                    indexLabelFontColor: "black",
                    dataPoints: ary
                }]
            });
            showDefaultText(chart, "No Data available");
            chart.render();
            function showDefaultText(chart, text) {
                var isEmpty = !(chart.options.data[0].dataPoints && chart.options.data[0].dataPoints.length > 0);

                if (!chart.options.subtitles)
                    (chart.options.subtitles = []);

                if (isEmpty)
                    chart.options.subtitles.push({
                        text: text,
                        verticalAlign: 'center',
                    });
                else
            	    (chart.options.subtitles = []);
            }


        }
    });;
    chart.render();


});

//Bifurcation Pie Chart



$(document).ready(function () {
    
    var not_coll = $('#not_coll').val();
    var mixed_coll = $('#mixed_coll').val();
    var bif_coll = $('#bif_coll').val();
    var not_spec_coll = $('#not_spec_coll').val();
   // var TotalDryWaste_coll = $('#TotalDryWaste_coll').val();
   // var TotalWetWaste_coll = $('#TotalWetWaste_coll').val();

    var tot_house_null_check = $('#tot_house_coll').val();
    var tot_house_coll;
    if (tot_house_null_check == 0) {
        tot_house_coll = null;
    } else {
        tot_house_coll = $('#tot_house_coll').val();
    }

    var res_mixed_coll = mixed_coll * 100 / tot_house_coll;
    var res_bif_coll = bif_coll * 100 / tot_house_coll;
    var res_not_coll = not_coll * 100 / tot_house_coll;
    var res_not_spec_coll = not_spec_coll * 100 / tot_house_coll;

   // var res_TotalDryWaste_coll = TotalDryWaste_coll * 100 / tot_house_coll;
    //var res_TotalWetWaste_coll = TotalWetWaste_coll * 100 / tot_house_coll;


    var ary3 = []
    ary3.push({ v: bif_coll });
    ary3.push({ v: mixed_coll });
    ary3.push({ v: not_coll });
    ary3.push({ v: not_spec_coll });
    //ary3.push({ v: TotalDryWaste_coll });
    //ary3.push({ v: TotalWetWaste_coll });


    //console.log(ary3);
    var chart = new CanvasJS.Chart("chartContainerPie", {
        theme: "light2",
        animationEnabled: true,
        title: {
            //text: "विलगिकरण प्रकार ",
            fontSize: 24,
            padding: 10
        },
        subtitles: [{
            //text: "United Kingdom, 2016",
            //fontSize: 16
        }],
        toolTip: {
            content: "In Numbers {hover_number}",
        },
        legend: {
            maxWidth: 300,
            itemWidth: 60,
            fontSize: 14,
            // horizontalAlign: "right", // left, center ,right 
            //verticalAlign: "center",
        },
        data: [{
            type: "pie",
            indexLabelFontSize: 12,
            showInLegend: true,
            legendText: "{hover_number}",
            radius: 60,
            indexLabel: "{label} - {y}",
            yValueFormatString: "###0.0\"%\"",
            click: explodePie,
            dataPoints: [
                //{ y: res_bif_coll, label: "वर्गीकृत कचरा", hover_number: bif_coll, color: '#388e3c' },
                //{ y: res_mixed_coll, label: "मिश्र कचरा", hover_number: mixed_coll, color: '#f44336' },
                //{ y: res_not_coll, label: "कचरा मिळाला नाही", hover_number: not_coll, color: '#fe9436' },
                //{ y: res_not_spec_coll, label: "वर्णन उपलब्ध नाही", hover_number: not_spec_coll, color: '#0086c3' },

               /*  { y: res_bif_coll, label: "Segregated Garbage", hover_number: bif_coll, color: '#388e3c' },*/
                { y: res_mixed_coll, label: "Liquid Collection Count", hover_number: mixed_coll, color: '#f44336' },
                { y: res_not_coll, label: "Garbage not received", hover_number: not_coll, color: '#fe9436' },
                { y: res_not_spec_coll, label: "Garbage type not specified", hover_number: not_spec_coll, color: '#0086c3' },
               // { y: res_TotalDryWaste_coll, label: "Dry Waste Garbage", hover_number: TotalDryWaste_coll, color: '#0462EA' },
                //{ y: res_TotalWetWaste_coll, label: "Wet Waste Garbage", hover_number: TotalWetWaste_coll, color: '#186634' },
            ],
        }]
    });
    showDefaultText(chart, "No Data available");
    chart.render();
    function showDefaultText(chart, text) {
        var isEmpty = !(tot_house_coll && chart.options.data[0].dataPoints && chart.options.data[0].dataPoints.length > 0);



        if (isEmpty) {
            chart.options.subtitles.push({
                text: text,
                verticalAlign: 'center',
            });
            (chart.options.data[0].dataPoints = []);
        }



    }
    function explodePie(e) {
        for (var i = 0; i < e.dataSeries.dataPoints.length; i++) {
            if (i !== e.dataPointIndex)
                e.dataSeries.dataPoints[i].exploded = false;
        }
    }

});


//target chart

$(document).ready(function () {

    
    $.ajax({
        type: "post",
        url: "/Liquid/LiquidHome/EmployeeTargetCount",
        //data: { userId: UserId, },
        datatype: "json",
        traditional: true,
        success: function (data) {
            //console.log(data);
            var ary = [];
            var ary2 = [];
            for (var i = 0; i < data.length; i++) {

                var name = data[i].UserName;
                name = name.trim();
                var fname = name.substring(0, name.indexOf(" "));
                // alert(data[i]._Count)
                ary.push({ y: data[i]._Count, label: fname });
                ary2.push({ y: parseInt(data[i].Target), label: fname });

            }

            var chart = new CanvasJS.Chart("chartContainerTarget",
       {

           axisY: {
               title: "Employee Target",
           },
           legend: {
               fontSize: 14
           },
           data: [
                       {
                           type: "column",
                           color: "#9bbb59",
                           toolTipContent: "Achieved: {y}",
                           showInLegend: true,
                           legendText: "Achieved",
                           indexLabelFontSize: 14,
                           indexLabel: "{y}",
                           dataPoints:
                               ary

                       }, {
                           type: "column",
                           color: "#c0504d",
                           toolTipContent: "Target: {y}",
                           showInLegend: true,
                           legendText: "Target",
                           indexLabelFontSize: 14,
                           indexLabelPlacement: "outside",
                           indexLabel: "{y}",
                           dataPoints:
                              ary2

                       }
           ]
       });
            showDefaultText(chart, "No Data available");
            chart.render();
            function showDefaultText(chart, text) {
                var isEmpty = !(chart.options.data[0].dataPoints && chart.options.data[0].dataPoints.length > 0);

                if (!chart.options.subtitles)
                    (chart.options.subtitles = []);

                if (isEmpty)
                    chart.options.subtitles.push({
                        text: text,
                        verticalAlign: 'center',
                    });
                else 
            	    (chart.options.subtitles = []);
            }


        }
    });
    chart.render();
});


//Dump Pie Chart


$(document).ready(function () {
    
    


    var tot_house_null_check = $('#TotalLiquidProperty_Count').val();
    var TotalLiquid_Count = $('#TotalLiquid_Count').val();
    var tot_house_coll;
    if (tot_house_null_check == 0) {
        tot_house_coll = null;
    } else {
        tot_house_coll = $('#TotalLiquidProperty_Count').val();
    }

    
    var res_TotalLiquid_Count = TotalLiquid_Count * 100 / tot_house_coll;
    var res_TotalLiquidProperty_Count1 = tot_house_coll - TotalLiquid_Count;
    var res_TotalLiquidProperty_Count = res_TotalLiquidProperty_Count1 * 100 / tot_house_coll


    var ary3 = []
    

    ary3.push({ v: res_TotalLiquid_Count });
    ary3.push({ v: res_TotalLiquidProperty_Count });


    //console.log(ary3);
    var chart = new CanvasJS.Chart("chartContainerPie", {
        theme: "light2",
        animationEnabled: true,
        title: {
            //text: "विलगिकरण प्रकार ",
            fontSize: 24,
            padding: 10
        },
        subtitles: [{
            //text: "United Kingdom, 2016",
            //fontSize: 16
        }],
        toolTip: {
            content: "In Numbers {hover_number}",
        },
        legend: {
            maxWidth: 300,
            itemWidth: 60,
            fontSize: 14,
            // horizontalAlign: "right", // left, center ,right 
            //verticalAlign: "center",
        },
        data: [{
            type: "pie",
            indexLabelFontSize: 12,
            showInLegend: true,
            legendText: "{hover_number}",
            radius: 60,
            indexLabel: "{label} - {y}",
            yValueFormatString: "###0.0\"%\"",
            click: explodePie,
            dataPoints: [

                { y: res_TotalLiquid_Count, label: "Liquid Waste Scan", hover_number: TotalLiquid_Count, color: '#0086c3' },
                { y: res_TotalLiquidProperty_Count, label: "Not Scan", hover_number: res_TotalLiquidProperty_Count1, color: '#f44336' },

            ],
        }]
    });
    showDefaultText(chart, "No Data available");
    chart.render();
    function showDefaultText(chart, text) {
        var isEmpty = !(tot_house_coll && chart.options.data[0].dataPoints && chart.options.data[0].dataPoints.length > 0);



        if (isEmpty) {
            chart.options.subtitles.push({
                text: text,
                verticalAlign: 'center',
            });
            (chart.options.data[0].dataPoints = []);
        }



    }
    function explodePie(e) {
        for (var i = 0; i < e.dataSeries.dataPoints.length; i++) {
            if (i !== e.dataPointIndex)
                e.dataSeries.dataPoints[i].exploded = false;
        }
    }


});

$(document).ready(function () {
    
    var dry_count = $('#dry_count').val();
    var wet_count = $('#wet_count').val();
    var tot_dump_null_check = $('#tot_dump_count').val();
    var tot_dump_count;
    if (tot_dump_null_check == 0) {
        tot_dump_count = null;
    } else {
        tot_dump_count = $('#tot_dump_count').val();
    }

    var res_dry_count = parseFloat(dry_count) * 100 / parseFloat(tot_dump_count);
    var res_wet_count = parseFloat(wet_count) * 100 / parseFloat(tot_dump_count);

    var ary3 = []
    ary3.push({ v: dry_count });
    ary3.push({ v: wet_count });


    //console.log(ary3);
    var chart = new CanvasJS.Chart("chartContainerPieDump", {
        theme: "light2",
        animationEnabled: true,
        title: {
            //text: "विलगिकरण प्रकार ",
            fontSize: 24,
            padding: 10
        },
        subtitles: [{
            //text: "United Kingdom, 2016",
            //fontSize: 16
        }],
        toolTip: {
            content: "In Numbers {hover_number} Ton",
        },
        legend: {
            maxWidth: 180,
            itemWidth: 75,
            fontSize: 12,
            // horizontalAlign: "right", // left, center ,right 
            //verticalAlign: "center",
        },
        data: [{
            type: "pie",
            indexLabelFontSize: 12,
            showInLegend: true,
            legendText: "{hover_number}",
            radius: 60,
            indexLabel: "{label} - {y}",
            yValueFormatString: "###0.0\"%\"",
            click: explodePie,
            dataPoints: [
                //{ y: res_dry_count, label: "एकुण वजन (सुका कचरा)", hover_number: dry_count, color: '#0086c3' },
                //{ y: res_wet_count, label: "एकुण वजन (ओला कचरा)", hover_number: wet_count, color: '#01ad35' },

                { y: res_dry_count, label: "Total Weight (Dry Waste)", hover_number: dry_count, color: '#0086c3' },
                { y: res_wet_count, label: "Total Weight (Wet Waste)", hover_number: wet_count, color: '#01ad35' },


            ],
        }]
    });
    showDefaultText(chart, "No Data available");
    chart.render();
    function showDefaultText(chart, text) {
        var isEmpty = !(tot_dump_count && chart.options.data[0].dataPoints && chart.options.data[0].dataPoints.length > 0);



        if (isEmpty) {
            chart.options.subtitles.push({
                text: text,
                verticalAlign: 'center',
            });
            (chart.options.data[0].dataPoints = []);
        }



    }
    function explodePie(e) {
        for (var i = 0; i < e.dataSeries.dataPoints.length; i++) {
            if (i !== e.dataPointIndex)
                e.dataSeries.dataPoints[i].exploded = false;
        }
    }

});


  var date = new Date();

var day = date.getDate();
var month = date.getMonth() + 1;
var year = date.getFullYear();

if (month < 10) month = "0" + month;
if (day < 10) day = "0" + day;

var today = month+ "/" + day+ "/" + year;

document.getElementById('txt_fdate').value = today;







  $('.datepicker').datepicker({
      format: 'mm/dd/yyyy',
      weekStart: 1,
      color: 'red',
      pickTime: false
  }).on('changeDate', function (e) {

      $(this).datepicker('hide');

  });
//hide  show on hover
$('#txt_fdate').focus(function () {
    $('.dtpk_drpdwn').eq(1).hide();
});
 

// by neha 8 july 2019
$(document).ready(function () {
    
    $.ajax({
        type: "post",
        url: "/Liquid/LiquidHome/EmployeeLiquidCollectionType",
        //data: { userId: UserId, },
        datatype: "json",
        traditional: true,
        success: function (data) {
            console.log(data);
            var not_spec = [];
            var not_coll = [];
            var mixed = [];
            var seg = [];
            var LiquidCollectionCount = [];
            //var dry = [];
            //var wet = [];
            var emp_tar = [];
            for (var i = 0; i < data.length; i++) {
                // alert(data[i].inTime);
                var name = data[i].userName;
                name = name.trim();
                var lastname_array = name.split(' ');
                var lastname_firstchar;
                if (lastname_array.length == 1) {
                    //if condition lastname_array[1] == undefined
                    lastname_firstchar = ""
                } else {
                    lastname_firstchar = lastname_array[1][0];
                }
                

                //var fname = name.substring(0, name.indexOf(" "));
                var fname = name.replace(/ .*/, ' ');
                // alert(data[i]._Count)
                //not_spec.push({ y: data[i].NotSpecidfied, label: 'Not Specified', color: '#0086c3', intime: data[i].inTime });
                //not_coll.push({ y: data[i].NotCollected, label: 'Not Collected', color: '#fe9436', intime: data[i].inTime });
                LiquidCollectionCount.push({ y: data[i].LiquidCollectionCount, label: 'Liquid Collection', color: '#f44336', intime: data[i].inTime });
             /*   seg.push({ y: data[i].Bifur, label: 'Segregated', color: '#388e3c', intime: data[i].inTime });*/
                //dry.push({ y: data[i].DryWaste, label: 'Dry Waste', color: '#0462EA', intime: data[i].inTime });
                //wet.push({y: data[i].WetWaste, label: 'Wet Waste', color: '#186634', intime: data[i].inTime });
                emp_tar.push({ y: parseInt(data[i].gcTarget), label: fname + lastname_firstchar, z: data[i].Count, intime: data[i].inTime });
                // ary2.push({ y: parseInt(data[i].gcTarget), label: data[i].userName });

            }

            var chart = new CanvasJS.Chart("chartContainerTarget2",
       {
           //title: {
           //    text: "Grouped Stacked Chart"
           //},
           theme: "theme3",
           // interval :1,
           axisY: {
               labelFontSize: 10,
               labelFontColor: "dimGrey",
               interval: 1
           },
           axisX: {
               labelAngle: -10,
               labelFontSize: 10,
               interval: 1
           },
           axisY: {
               title: "Liquid Waste Cleaning Point",
           },

           data: [

                 //{
                 //    //indexLabel: "#total",
                 //    //indexLabelPlacement: "outside",
                 //    //indexLabelPlacement: "outside",
                 //    type: "stackedColumn",
                 //    showInLegend: true,
                 //    legendText: "Segregated",
                 //    toolTipContent: "InTime:{intime} <br>{label}:{y} ",
                 //    color: "#388e3c",
                 //    dataPoints: seg
                 //},
                 {
                     //indexLabel: "#total",
                     //indexLabelPlacement: "outside",
                     type: "stackedColumn",
                     showInLegend: true,
                   legendText: "LiquidCollectionCount",
                     toolTipContent: "InTime:{intime} <br>{label}:{y} ",
                     color: "#f44336",
                   dataPoints: LiquidCollectionCount
                 },
               //  {
               //      //indexLabel: "#total",
               //      //indexLabelPlacement: "outside",
               //      type: "stackedColumn",
               //      showInLegend: true,
               //      legendText: "NotCollected",
               //      toolTipContent: "InTime:{intime} <br>{label}:{y} ",
               //      color: "#fe9436",
               //      dataPoints: not_coll
               //  },
               //  {
               //      indexLabel: "#total",
               //      indexLabelPlacement: "outside",
               //      type: "stackedColumn",
               //      showInLegend: true,
               //      legendText: "NotSpecified",
               //      toolTipContent: "InTime:{intime} <br>{label}:{y} ",
               //      color: "#0086c3",
               //      dataPoints: not_spec
               //},

               //{
               //    //indexlabel: "#total",
               //    //indexlabelplacement: "outside",
               //    //indexlabelplacement: "outside",
               //    type: "stackedcolumn",
               //    showinlegend: true,
               //    legendtext: "dry waste",
               //    tooltipcontent: "intime:{intime} <br>{label}:{y} ",
               //    color: "#0462ea",
               //    datapoints: dry
               //},
               //{
               //    //indexlabel: "#total",
               //    //indexlabelplacement: "outside",
               //    //indexlabelplacement: "outside",
               //    type: "stackedcolumn",
               //    showinlegend: true,
               //    legendtext: "wet waste",
               //    tooltipcontent: "intime:{intime} <br>{label}:{y} ",
               //    color: "#186634",
               //    datapoints: wet
               //},
                     {
                         type: "line",
                         color: "#c0504d",
                         dataPoints: emp_tar,
                         // indexLabel: "{y}",
                         showInLegend: true,
                         name: "Target",
                     }
                 //{
                 //    type: "stackedColumn",
                 //    color: "#c0504d",
                 //    axisYType: "secondary",
                 //    toolTipContent: "Target:{y} <br> InTime:{intime}",
                 //    showInLegend: true,
                 //    legendText: "Target",
                 //    indexLabelFontSize: 14,
                 //    indexLabelPlacement: "outside",
                 //    indexLabel: "{z}/{y}",
                 //    dataPoints: emp_tar
                 //}


           ]
       });
            showDefaultText(chart, "No Data available");
            chart.render();
            function showDefaultText(chart, text) {
                var isEmpty = !(chart.options.data[0].dataPoints && chart.options.data[0].dataPoints.length > 0);

                if (!chart.options.subtitles)
                    (chart.options.subtitles = []);

                if (isEmpty)
                    chart.options.subtitles.push({
                        text: text,
                        verticalAlign: 'center',
                    });
                else
            	    (chart.options.subtitles = []);
            }
        }
    });
    chart.render();


});

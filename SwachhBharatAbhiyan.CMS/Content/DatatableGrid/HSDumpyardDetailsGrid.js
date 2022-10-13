//Dump Code Start

function loadGridDump() {
    
   
    let appName = document.getElementById("ulb_name").innerHTML;
    $("#demoGrid1").dataTable().fnDestroy();
    $("#demoGrid1").DataTable({
        buttons: [

            {
                extend: 'excel', className: 'btn btn-sm btn-success filter-button-style', title: appName + ' Dump Yard Report', text: 'Export to Excel', exportOptions: { columns: [0, 1, 2, 3, 4, 5] }
            },
        ],
        //"sDom": "ltipr",
        dom: 'lBfrtip',
        lbFilter: false,
        //"sDom": "ltipr",
        //"aaSorting": [],
        //   "order": [[11, "desc"]],
        //"order": [],
        "processing": true, // for show progress bar
        "serverSide": true, // for process server side
        "filter": true, // this is for disable filter (search box)
        "orderMulti": false, // for disable multiple column at once
        //"pageLength": 10,

        "ajax": {
            "url": "/Datable/GetJqGridJson?rn=HSDumpyardDetails",
            "type": "POST",
            "datatype": "json"
        },
        //"drawCallback": function (settings) {

        //    var api = this.api();
        //    var rowData = api.rows().data();
        //    arrDumpIDs = [];
        //    for (var i = 0; i < rowData.length; i++) {
        //        arrDumpIDs.push(rowData[i]['dumpId']);
        //    }
        //    console.log(arrDumpIDs);
        //},
        "columnDefs":
            [{
                "targets": [0],
                "visible": false,
                "searchable": false
            },
            {
                "targets": [6],
                "visible": true,

                "render": function (data, type, full, meta) {
                    if (full["QRCodeImage"] != null) {
                        return "<div style='cursor:pointer;display:inline-flex;'  onclick=PopImagesDump(this)><img alt='Photo Not Found'  src='" + data +
                            "' style='height:35px;width:35px;cursor:pointer;margin-left:0px;'></img><span><ul class='dt_pop'  style='margin:2px -5px -5px -5px; padding:0px;list-style:none;display:none;'><li  class='li_date datediv' >" + full["Name"] + "</li><li  class='li_lat datediv' >" + full["HouseLat"] + "</li></li><li  class='li_long datediv' >" + full["HouseLong"] + "</li><li class='addr-length' style='margin:0px 0px 0px 10px;'>"
                            + full["ReferanceId"] + "</li><li class='date_time'>" + full["modifiedDate"] + "</li><li style='display:none' class='li_title' >HouseScanify QR Image </li><li class='li_houseId'>" + full["dumpId"] + "</li><li class='li_QRStatus'>" + full["QRStatus"] + "</li></ul></span></div>";
                    }
                    else {

                        return "<img alt='Photo Not Found' onclick='noImageNotification()' src='/Images/default.png' style='height:35px;width:35px;cursor:pointer;'></img>";
                    }
                },
                },
                {
                    "targets": [7],
                    "visible": true,

                    "render": function (data, type, full, meta) {
                        if (full["QRStatus"] != null) {
                            if (full["QRStatus"] == true) {
                                return "<span>Approved</span>";

                            }
                            else {
                                return "<span>Reject</span>";
                            }
                        }
                        else {

                            return "<span>Not Verified</span>";
                        }
                    },
                }
            ],


        "columns": [
            { "data": "dumpId", "name": "dumpId", "autoWidth": true },
            { "data": "modifiedDate", "name": "modifiedDate", "autoWidth": true },
            { "data": "ReferanceId", "name": "ReferanceId", "autoWidth": true },
            { "data": "Name", "name": "Name", "autoWidth": true },
            { "data": "HouseLat", "name": "HouseLat", "autoWidth": true },
            { "data": "HouseLong", "name": "HouseLong", "autoWidth": true },
            { "data": "QRCodeImage", "name": "QRCodeImage", "autoWidth": true },
            { "data": "QRStatus", "name": "QRStatus", "autoWidth": true },
            { "data": "QRStatusDate", "name": "QRStatusDate", "autoWidth": true },

        ],

    });


    var tableDump = $('#demoGrid1').DataTable();
    $("#target1").click(function () {
        //alert("Handler for .click() called.");
        var tableDump = $('#demoGrid1').DataTable();
        tableDump.ajax.reload(null, false);
    });
    $('#demoGrid1').on('order.dt', function () {

        var txt_fdate, txt_tdate, UserId, QRStatus, searchString, sortColumn, sortOrder;
        sortColumn = "";
        sortOrder = "";
        var name = [];

        var arr = [$('#txt_fdate').val(), $('#txt_tdate').val()];

        for (var i = 0; i <= arr.length - 1; i++) {
            name = arr[i].split("/");
            arr[i] = name[1] + "/" + name[0] + "/" + name[2];
        }

        txt_fdate = arr[0];
        txt_tdate = arr[1];
        UserId = $('#selectnumber').val();
        QRStatus = $('#selectQRStatus').val();
        searchString = $("#sDump").val();
        // This will show: "Ordering on column 1 (asc)", for example
        var order = tableDump.order();
        //console.log(order);
        if (order.length > 0) {
            var sortColumn = tableDump.settings().init().columns[order[0][0]].name;
            var sortOrder = order[0][1];
        }
        //alert(sortColumn);
        //alert(order[0][1]);
        //$('#orderInfo').html('Ordering on column ' + order[0][0] + ' (' + order[0][1] + ')');


        $.ajax({
            type: "GET",
            url: "/HouseScanifyEmp/GetHSDumpDetailsID?fdate=" + txt_fdate + "&tdate=" + txt_tdate + "&userId=" + UserId + "&searchString=" + searchString + "&qrStatus=" + QRStatus + "&sortColumn=" + sortColumn + "&sortOrder=" + sortOrder,
            datatype: "json",
            traditional: true,
            success: function (data) {
                arrDumpIDs = data;
                console.log(arrDumpIDs);
                //var arrIDs = JSON.parse(data);
                //console.log(arrIDs);
            }
        });
    });


    //SearchDump();
}

    //Search();







function noImageNotification() {
    document.getElementById("snackbar").innerHTML = "Image not uploaded...";
    var x = document.getElementById("snackbar");
    x.className = "show";
    setTimeout(function () { x.className = x.className.replace("show", ""); }, 3000);
}

//function PopImages(cel) {

//    $('#myModal_Image').modal('toggle');

//    var addr = $(cel).find('.addr-length').text();
//    var date = $(cel).find('.li_date').text();
//    var imgsrc = $(cel).find('img').attr('src');
//    var head = $(cel).find('.li_title').text();
//    jQuery("#latlongData").text(addr);
//    jQuery("#dateData").text(date);
//    jQuery("#imggg").attr('src', imgsrc);
//    //jQuery("#latlongData").text(cellValue);
//    jQuery("#header_data").html(head);
//}


function user_route(id) {
    window.location.href = "/HouseScanify/HSUserRoute?qrEmpDaId=" + id;
};
//////////////////////////////////////////////////////////////////////////////
function showInventoriesGrid() {
    Search();
}

function Search() {
    var value = ",,," + $("#sDump").val();//txt_fdate + "," + txt_tdate + "," + UserId + "," + Client + "," + NesEvent + "," + Product + "," + catProduct + "," + 1;
    // alert(value );
    oTable = $('#demoGrid1').DataTable();
    oTable.search(value).draw();
    oTable.search("");
    document.getElementById('USER_ID_FK').value = -1;
}


function SearchDump() {
    var txt_fdate, txt_tdate, Client, UserId, QRStatus;
    var name = [];
    var arr = [$('#txt_fdate').val(), $('#txt_tdate').val()];

    for (var i = 0; i <= arr.length - 1; i++) {
        name = arr[i].split("/");
        arr[i] = name[1] + "/" + name[0] + "/" + name[2];
    }

    txt_fdate = arr[0];
    txt_tdate = arr[1];
    UserId = $('#selectnumber').val();
    QRStatus = $('#selectQRStatus').val();
    Client = " ";
    NesEvent = " ";
    var Product = "";
    var catProduct = "";
    var value = txt_fdate + "," + txt_tdate + "," + UserId + "," + $("#sDump").val() + "," + QRStatus;//txt_fdate + "," + txt_tdate + "," + UserId + "," + Client + "," + NesEvent + "," + Product + "," + catProduct + "," + 1;
    // alert(value );
    oTable = $('#demoGrid1').DataTable();
    oTable.search(value).draw();
    oTable.search("");
    document.getElementById('USER_ID_FK').value = -1;
}

//Dump Code End


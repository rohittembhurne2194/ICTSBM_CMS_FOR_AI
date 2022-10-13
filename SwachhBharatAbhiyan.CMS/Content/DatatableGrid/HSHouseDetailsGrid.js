
function loadGridHouse() {
    
    let appName = document.getElementById("ulb_name").innerHTML;
    $("#demoGrid").dataTable().fnDestroy();
    $("#demoGrid").DataTable({
        buttons: [

            {
                extend: 'excel', className: 'btn btn-sm btn-success filter-button-style', title: appName + ' House Report', text: 'Export to Excel', exportOptions: { columns: [0, 1, 2, 3, 4, 5,7,8] }
            },
        ],
        //"sDom": "ltipr",
        dom: 'lBfrtip',
        lbFilter: false,
        //"sDom": "ltipr",
        //"order": [[0, "desc"]],
        "processing": true, // for show progress bar
        "serverSide": true, // for process server side
        "filter": true, // this is for disable filter (search box)
        "orderMulti": false, // for disable multiple column at once
        "pageLength": 10,
        "pagingType": "input",
        "ajax": {
            "url": "/Datable/GetJqGridJson?rn=HSHouseDetails",
            "type": "POST",
            "datatype": "json"
        },

        "columnDefs":
            [{
                "targets": [0],
                "visible": false,
                "searchable": false
            },
                {
                    "targets": [10],
                    "visible": false,
                    "searchable": false
                },
                {
                    "targets": [11],
                    "visible": false,
                    "searchable": false
                },
                {
                    "targets": [6],
                    "visible": true,

                    "render": function (data, type, full, meta) {
                        if (full["QRCodeImage"] != null) {
                            return "<div style='cursor:pointer;display:inline-flex;'  onclick=PopImagesHouse(this)><img alt='Photo Not Found'  src='" + data +
                                "' style='height:35px;width:35px;cursor:pointer;margin-left:0px;'></img><span><ul class='dt_pop'  style='margin:2px -5px -5px -5px; padding:0px;list-style:none;display:none;'><li  class='li_date datediv' >" + full["Name"] + "</li><li  class='li_lat datediv' >" + full["HouseLat"] + "</li></li><li  class='li_long datediv' >" + full["HouseLong"] + "</li><li class='addr-length' style='margin:0px 0px 0px 10px;'>"
                                + full["ReferanceId"] + "</li><li class='date_time'>" + full["modifiedDate"] + "</li><li style='display:none' class='li_title' >HouseScanify QR Image </li><li class='li_houseId'>" + full["houseId"] + "</li><li class='li_QRStatus'>" + full["QRStatus"] + "</li></ul></span></div>";
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
                },
                {
                    "targets": [9],
                    "visible": true,

                    "render": function (data, type, full, meta) {
                        if (full["QRStatus"] != null) {
                           
                          
                            if (full["QRStatus"] == false && full["QRStatusDate1"] < full["modifiedDate1"]) {
                                return "<span>Re-Scan Done</span>";

                            }
                            else if (full["QRStatus"] == false && full["QRStatusDate1"] > full["modifiedDate1"]) {
                                return "<span>Re-Scan Not Done</span>";
                            }
                            else {
                                return "<span>Approved</span>";
                            }
                        }
                        else {

                            return "<span>Not Verified</span>";
                        }
                    },
                }
            ],


        "columns": [
            { "data": "houseId", "name": "houseId", "autoWidth": true },
            { "data": "modifiedDate", "name": "modifiedDate", "autoWidth": true },
            { "data": "ReferanceId", "name": "ReferanceId", "autoWidth": true },
            { "data": "Name", "name": "Name", "autoWidth": true },
            { "data": "HouseLat", "name": "HouseLat", "autoWidth": true },
            { "data": "HouseLong", "name": "HouseLong", "autoWidth": true },
            { "data": "QRCodeImage", "name": "QRCodeImage", "autoWidth": true },
            { "data": "QRStatus", "name": "QRStatus", "autoWidth": true },
            { "data": "QRStatusDate", "name": "QRStatusDate", "autoWidth": true },
            { "data": "QRStatus", "name": "QRStatus", "autoWidth": true },
            { "data": "modifiedDate1", "name": "modifiedDate1", "autoWidth": true },
            { "data": "QRStatusDate1", "name": "QRStatusDate1", "autoWidth": true },
        ],

      

    });
    var tableHouse = $('#demoGrid').DataTable();
   
    //setInterval(function () {
    //    tableHouse.ajax.reload(null, false); // user paging is not reset on reload
    //}, 300000);
    //function close() {
    //    //LoadGrid();
    //    debugger
    //    var tableHouse = $('#demoGrid').DataTable();
    //    tableHouse.ajax.reload(null, false);
    //}
    $("#target").click(function () {
        //alert("Handler for .click() called.");
        var tableHouse = $('#demoGrid').DataTable();
        tableHouse.ajax.reload(null, false);
    });



    $('#demoGrid').on('order.dt', function () {

        var txt_fdate, txt_tdate, UserId, QRStatus, searchString;

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
        searchString = $("#sHouse").val();
        // This will show: "Ordering on column 1 (asc)", for example
        var order = tableHouse.order();
        //console.log(order);
        var sortColumn = tableHouse.settings().init().columns[order[0][0]].name;
        var sortOrder = order[0][1];
        //alert(sortColumn);
        //alert(order[0][1]);
        //$('#orderInfo').html('Ordering on column ' + order[0][0] + ' (' + order[0][1] + ')');


        $.ajax({
            type: "GET",
            url: "/HouseScanifyEmp/GetHSHouseDetailsID?fdate=" + txt_fdate + "&tdate=" + txt_tdate + "&userId=" + UserId + "&searchString=" + searchString + "&qrStatus=" + QRStatus + "&sortColumn=" + sortColumn + "&sortOrder=" + sortOrder,
            datatype: "json",
            traditional: true,
            success: function (data) {
                arrHouseIDs = data;
                //console.log(arrHouseIDs);
                //var arrIDs = JSON.parse(data);
                //console.log(arrIDs);
            }
        });
    });
    //SearchHouse();

}






function noImageNotification() {
    document.getElementById("snackbar").innerHTML = "Image not uploaded...";
    var x = document.getElementById("snackbar");
    x.className = "show";
    setTimeout(function () { x.className = x.className.replace("show", ""); }, 3000);
}

    function user_route(id) {
        window.location.href = "/HouseScanify/HSUserRoute?qrEmpDaId=" + id;
    };
    //////////////////////////////////////////////////////////////////////////////
    function showInventoriesGrid() {
        Search();
    }


    function Search() {
        var value = ",,," + $("#sHouse").val();//txt_fdate + "," + txt_tdate + "," + UserId + "," + Client + "," + NesEvent + "," + Product + "," + catProduct + "," + 1;
        // alert(value );
        oTable = $('#demoGrid').DataTable();
        oTable.search(value).draw();
        oTable.search("");
        document.getElementById('USER_ID_FK').value = -1;
    }




    function SearchHouse() {
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
        //alert(QRStatus);
        Client = " ";
        NesEvent = " ";
        var Product = "";
        var catProduct = "";
        var value = txt_fdate + "," + txt_tdate + "," + UserId + "," + $("#sHouse").val() + "," + QRStatus;//txt_fdate + "," + txt_tdate + "," + UserId + "," + Client + "," + NesEvent + "," + Product + "," + catProduct + "," + 1;
        
        oTable = $('#demoGrid').DataTable();
        oTable.search(value).draw();
        oTable.search("");
        //document.getElementById('USER_ID_FK').value = -1;
    }

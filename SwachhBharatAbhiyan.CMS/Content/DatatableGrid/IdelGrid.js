//$(document).ready(function () {
//    $('#demoGrid_filter').hide();
//    var UserId = $('#selectnumber').val();

//    $.ajax({
//        type: "post",
//        url: "/Location/UserList",
//        data: { userId: UserId, },
//        datatype: "json",
//        traditional: true,
//        success: function (data) {
//            district = '<option value="-1">Select Employee</option>';
//            for (var i = 0; i < data.length; i++) {
//                district = district + '<option value=' + data[i].Value + '>' + data[i].Text + '</option>';
//            }
//            //district = district + '</select>';
//            $('#selectnumber').html(district);
//        }
//    });
//    $("#demoGrid").DataTable({
//        buttons: [

//                       { extend: 'excel', className: 'btn btn-sm btn-success filter-button-style', title: 'Ghanta Gadi Ideal time Report',  text: 'Export to Excel', },
//        ],
//        //"sDom": "ltipr",
//         dom: 'lBfrtip',
//         lbFilter: false,

//        //"sDom": "ltipr",
//        "order": [[0, "desc"]],
//        "processing": true, // for show progress bar
//        "serverSide": true, // for process server side
//        "filter": true, // this is for disable filter (search box)
//        "orderMulti": false, // for disable multiple column at once
//        "pageLength": 10,
//        "ajax": {
//            "url": "/Datable/GetJqGridJson?rn=UserIdel",
//            "type": "POST",
//            "datatype": "json"
//        },

//        "columnDefs":
//            [
//                {
//                    "targets": [0],
//                    "visible": false,
//                    "searchable": false
//                },
//                {
//                    "targets": [6],
//                    "visible": false,
//                    "searchable": false
//                },
//               {
//                   "targets": [7],
//                   "visible": false,
//                   "searchable": false
//               }, {
//                   "targets": [8],
//                   "visible": false,
//                   "searchable": false
//               },
//            {
//                "targets": [9],
//                "visible": false,
//                "searchable": false
//            },

//            ],

//        "columns": [
//               { "data": "userId", "name": "userId", "autoWidth": true },
//              { "data": "UserName", "name": "UserName", "autoWidth": false },
//              { "data": "Date", "name": "Date", "autoWidth": false },
//              { "data": "StartTime", "name": "StartTime", "autoWidth": false },
//              { "data": "EndTime", "name": "EndTime", "autoWidth": false },
//              { "data": "IdelTime", "name": "IdelTime", "autoWidth": false },
//              { "data": "startLat", "name": "startLat", "autoWidth": true },
//              { "data": "startLong", "name": "startLong", "autoWidth": true },
//              { "data": "EndLat", "name": "EndLat", "autoWidth": true },
//              { "data": "EndLong", "name": "EndLong", "autoWidth": true },

//                 {
//                     "render": function (data, type, full, meta) {

//                         date_str[full["userId"]] = full["Date"];
//                         return '<a  data-toggle="modal" class="tooltip1" style="cursor:pointer" onclick="user_route(' + full["userId"] + ')" ><i class="material-icons location-icon">location_on</i><span class="tooltiptext1">Route</span> </a>';
//                     }, "width": "10%"
//                 },
//                { "data": "StartAddress", "name": "StartAddress", "autoWidth": false },

//              { "data": "EndAddress", "name": "EndAddress", "autoWidth": false },

//        ]
//    });

//    $("#demoGrid").DataTable().buttons().container()
//           .appendTo('#demoGrid_wrapper .col-md-6:eq(1)').addClass('float-right');


//});


//var date_str = {};
//function user_route(id) {
//    //alert(date_str[Date]);
//    var fdate = date_str[id];
//    alert(id);
//    window.location.href = "/GarbageCollection/IdleTime_Route?userId=" + id + "&date=" + fdate;
//};

//function showInventoriesGrid() {
//    Search();
//}


//function Search() {
//    var txt_fdate, txt_tdate, Client, UserId;
//    var name = [];
//    var arr = [$('#txt_fdate').val(), $('#txt_tdate').val()];

//    for (var i = 0; i <= arr.length - 1; i++) {
//        name = arr[i].split("/");
//        arr[i] = name[1] + "/" + name[0] + "/" + name[2];
//    }

//    txt_fdate = arr[0];
//    txt_tdate = arr[1];
//    UserId = $('#selectnumber').val();
//    Client = " ";
//    NesEvent = " ";
//    var Product = "";
//    var catProduct = "";
//    var value = txt_fdate + "," + txt_tdate + "," + UserId + "," + $("#s").val();//txt_fdate + "," + txt_tdate + "," + UserId + "," + Client + "," + NesEvent + "," + Product + "," + catProduct + "," + 1;
//    // alert(value );
//    oTable = $('#demoGrid').DataTable();
//    oTable.search(value).draw();
//    oTable.search("");
//    document.getElementById('USER_ID_FK').value = -1;
//}
$(document).ready(function () {

    $('#demoGrid_filter').hide();
    var UserId = $('#selectnumber').val();



    $.ajax({
        type: "post",
        url: "/Location/UserList",
        data: { userId: UserId, },
        datatype: "json",
        traditional: true,
        success: function (data) {
            district = '<option value="-1">Select Employee</option>';
            for (var i = 0; i < data.length; i++) {
                district = district + '<option value=' + data[i].Value + '>' + data[i].Text + '</option>';
            }
            //district = district + '</select>';
            $('#selectnumber').html(district);
        }
    });
    $("#demoGrid").DataTable({
        buttons: [

                       { extend: 'excel', className: 'btn btn-sm btn-primary filter-button-style', title: 'Ghanta Gadi Ideal time Report', text: 'Export to Excel', },
        ],
        //"sDom": "ltipr",
        dom: 'lBfrtip',
        lbFilter: false,

        //"sDom": "ltipr",
        "order": [[13, "desc"]],
        "processing": true, // for show progress bar
        "serverSide": true, // for process server side
        "filter": true, // this is for disable filter (search box)
        "orderMulti": false, // for disable multiple column at once
        "pageLength": 10,
        "ajax": {
            "url": "/Datable/GetJqGridJson?rn=UserIdel",
            "type": "POST",
            "datatype": "json"
        },

        "columnDefs":
        [
            {
                "targets": [0],
                "visible": false,
                "searchable": false
            },
            {
                "targets": [6],
                "visible": false,
                "searchable": false
            },
           {
               "targets": [7],
               "visible": false,
               "searchable": false
           }, {
               "targets": [8],
               "visible": false,
               "searchable": false
           },
        {
            "targets": [9],
            "visible": false,
            "searchable": false
        },
        {
            "targets": [13],
            "visible": false,
            "searchable": false
        },

        ],

        "columns": [
              { "data": "userId", "name": "userId", "autoWidth": true },
              { "data": "UserName", "name": "UserName", "autoWidth": false },
              { "data": "Date", "name": "Date", "autoWidth": false },
              { "data": "StartTime", "name": "StartTime", "autoWidth": false },
              { "data": "EndTime", "name": "EndTime", "autoWidth": false },
              { "data": "IdelTime", "name": "IdelTime", "autoWidth": false },
              { "data": "startLat", "name": "startLat", "autoWidth": true },
              { "data": "startLong", "name": "startLong", "autoWidth": true },
              { "data": "EndLat", "name": "EndLat", "autoWidth": true },
              { "data": "EndLong", "name": "EndLong", "autoWidth": true },

                 {
                     "render": function (data, type, full, meta) {
                        
                         //date_str[full["userId"]] = full["Date"];
                         date_str[full["EndLat"]] =  full ;

                         //alert(date_str[full["userId"]]);
                         return '<a  data-toggle="modal" class="tooltip1" style="cursor:pointer" onclick="user_route(' + full["EndLat"] + ')" ><i class="material-icons location-icon">location_on</i><span class="tooltiptext1">Route</span> </a>';
                     }, "width": "10%"
                 },
                { "data": "StartAddress", "name": "StartAddress", "autoWidth": false },

              { "data": "EndAddress", "name": "EndAddress", "autoWidth": false }, 
              { "data": "daDateTIme", "name": "daDateTIme", "autoWidth": true }

        ]
    });

    $("#demoGrid").DataTable().buttons().container()
           .appendTo('#demoGrid_wrapper .col-md-6:eq(1)').addClass('float-right');


});


var date_str = {};
function user_route(id) {
    //console.log(date_str[id]);
    //
    var objIdle = JSON.stringify(date_str[id]);
    window.localStorage.setItem("mJson", objIdle);
    window.location.href = "/GarbageCollection/IdleTime_Route";
};
function showInventoriesGrid() {
    Search();
}


function Search() {
    var txt_fdate, txt_tdate, Client, UserId;
    var name = [];
    var arr = [$('#txt_fdate').val(), $('#txt_tdate').val()];

    for (var i = 0; i <= arr.length - 1; i++) {
        name = arr[i].split("/");
        arr[i] = name[1] + "/" + name[0] + "/" + name[2];
    }

    txt_fdate = arr[0];
    txt_tdate = arr[1];
    UserId = $('#selectnumber').val();
    Client = " ";
    NesEvent = " ";
    var Product = "";
    var catProduct = "";
    var value = txt_fdate + "," + txt_tdate + "," + UserId + "," + $("#s").val();//txt_fdate + "," + txt_tdate + "," + UserId + "," + Client + "," + NesEvent + "," + Product + "," + catProduct + "," + 1;
    // alert(value );
    oTable = $('#demoGrid').DataTable();
    oTable.search(value).draw();
    oTable.search("");
    document.getElementById('USER_ID_FK').value = -1;
}
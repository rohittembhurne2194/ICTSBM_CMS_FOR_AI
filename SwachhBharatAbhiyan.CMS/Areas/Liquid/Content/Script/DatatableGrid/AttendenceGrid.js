$(document).ready(function () {
    
    var UserId = $('#selectnumber').val();
    $.ajax({
        type: "post",
        url: "/Location/UserList?rn=L",
        data: { userId: UserId },
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

    $('#selecttype').html('<option value=0>Select Monitoring Type</option><option value=S>Waste Collection Monitoring Technology</option><option value=SS>Street Sweeping Monitoring System</option><option value=L>Liquid Waste Cleaning Monitoring System</option>');
   
    $("#demoGrid").DataTable({
        "sDom": "ltipr",
        "order": [[15, "desc"]],
        "processing": true, // for show progress bar
        "serverSide": true, // for process server side
        "filter": true, // this is for disable filter (search box)
        "orderMulti": false, // for disable multiple column at once
        //"pageLength": 10,

        "ajax": {
            "url": "/Datable/GetJqGridJson?rn=LiquidAttendence",
            "type": "POST",
            "datatype": "json"
        },

        "columnDefs":
        [{
            "targets": [0],
            "visible": false,
            "searchable": false
        }, {
            "targets": [6],
            "visible": false,
            "searchable": false
        }, {
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
             "targets": [10],
             "orderable": false
        },
        {
            "targets": [14],
            "visible": false,
            "searchable": false
        },
        {
            "targets": [15],
            "visible": false,
            "searchable": false,
            "type": "date-eu"
        }
        ],


        "columns": [
              { "data": "daID", "name": "daID", "autoWidth": true },
              { "data": "userName", "name": "userName", "autoWidth": true },
              { "data": "daDate", "name": "daDate", "autoWidth": true },
              { "data": "startTime", "name": "startTime", "autoWidth": true },
              { "data": "daEndDate", "name": "daEndDate", "autoWidth": true },
              { "data": "endTime", "name": "endTime", "autoWidth": true },
              { "data": "startLat", "name": "endstartLatTime", "autoWidth": true },
              { "data": "startLong", "name": "startLong", "autoWidth": true },
              { "data": "endLat", "name": "endLat", "autoWidth": true },
              { "data": "endLong", "name": "endLong", "autoWidth": true },
        
              { "render": function (data, type, full, meta) { return '<a  data-toggle="modal" class="tooltip1" style="cursor:pointer" onclick="house_route(' + full["daID"] + ')" ><i class="material-icons location-icon">location_on</i><span class="tooltiptext1">Route</span> </a>'; }, "width": "10%" },
              { "render": function (data, type, full, meta) { return '<a  data-toggle="modal" class="tooltip1" style="cursor:pointer" onclick="user_route(' + full["daID"] + ')" ><i class="material-icons location-icon">location_on</i><span class="tooltiptext1">Route</span> </a>'; }, "width": "10%" },

              { "data": "vtId", "name": "vtId", "autoWidth": true },
              { "data": "vehicleNumber", "name": "vehicleNumber", "autoWidth": true },
              { "data": "CompareDate", "name": "daID", "autoWidth": true },
              { "data": "daDateTIme", "name": "daDateTIme", "autoWidth": true },
             
        ],
       // Sort: "locId DESC"
    });

 
});

function test(id) {
    window.location.href = "/Liquid/LiquidAttendence/Location?daId="+id;
};

function user_route(id) {
    window.location.href = "/Liquid/LiquidAttendence/UserRoute?daId=" + id;
};

function house_route(id) {
    window.location.href = "/Liquid/LiquidAttendence/HouseRoute?daId=" + id;
};
function map(a) {
    window.location.href = "/Liquid/LiquidAttendence/viewLocation?teamId=" + a;

};
//////////////////////////////////////////////////////////////////////////////
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
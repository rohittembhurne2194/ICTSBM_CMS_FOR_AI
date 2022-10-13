$(document).ready(function () {
    
    $.get("/houseScanify/GetAppNames", null, house);

    function house(data) {
        var qqq = $('#appid').val();
        for (var i = 0; i < data.length; i++) {

            if (data[i].AppId == qqq) {
                $('#ulb_name').html(data[i].AppName);
            }
        }

    }


    //var UserId = $('#selectnumber').val();
    var UserId = $('#appid').val();
    // alert(UserId);
    $.ajax({
        type: "post",
        url: "/HouseScanify/UserListByAppId?AppId=" + UserId,
        data: { userId: UserId },
        datatype: "json",
        traditional: true,
        success: function (data) {
            district = '<option value="-1">Select Employee</option>';
            console.log(data);
            for (var i = 0; i < data.length; i++) {
                district = district + '<option value=' + data[i].qrEmpId + '>' + data[i].qrEmpName + '</option>';
            }
            //district = district + '</select>';
            $('#selectnumber').html(district);
        }
    });


    $("#demoGrid").DataTable({
        "sDom": "ltipr",
       "order": [[10, "desc"]],
        "processing": true, // for show progress bar
        "serverSide": true, // for process server side
        "filter": true, // this is for disable filter (search box)
        "orderMulti": false, // for disable multiple column at once
        //"pageLength": 10,

        "ajax": {
            "url": "/Datable/GetJqGridJson?rn=HSAttendance",
            "type": "POST",
            "datatype": "json"
        },

        "columnDefs":
        [{
            "targets": [0],
            "visible": false,
            "searchable": false
        },
        //{
        //    "targets": [9],
        //    "visible": false,
        //    "searchable": false
        //}
        ],


        "columns": [
              { "data": "qrEmpDaId", "name": "qrEmpDaId", "autoWidth": true },
              { "data": "userName", "name": "userName", "autoWidth": true },
              { "data": "startDate", "name": "startDate", "autoWidth": true },
              { "data": "startTime", "name": "startTime", "autoWidth": true },
              { "data": "endDate", "name": "endDate", "autoWidth": true },
              { "data": "endTime", "name": "endTime", "autoWidth": true }, 
              { "data": "HouseCount", "name": "HouseCount", "autoWidth": true },  
             
              { "data": "LiquidCount", "name": "LiquidCount", "autoWidth": true },
            { "data": "StreetCount", "name": "StreetCount", "autoWidth": true },
            { "data": "DumpYardCount", "name": "DumpYardCount", "autoWidth": true },
            { "data": "daDateTIme", "name": "daDateTIme", "autoWidth": true },
              { "render": function (data, type, full, meta) { return '<a  data-toggle="modal" class="tooltip1" style="cursor:pointer" onclick="user_route(' + full["qrEmpDaId"] + ')" ><i class="material-icons location-icon">location_on</i><span class="tooltiptext1">Route</span> </a>'; }, "width": "10%" },
          
              //{ "render": function (data, type, full, meta) { return '<a  data-toggle="modal" class="tooltip1" style="cursor:pointer"   onclick="Edit(' + full["qrEmpId"] + ')"  ><i class="material-icons edit-icon">edit</i><span class="tooltiptext1">Edit</span> </a>'; }, "width": "10%" },


        ],

    });

   

    Search();

});

function user_route(id) {
    window.location.href = "/HouseScanify/HSUserRoute?qrEmpDaId=" + id;
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
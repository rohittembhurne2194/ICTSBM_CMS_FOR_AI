$(document).ready(function () {
    var UserId = $('#selectnumber').val();
    $.ajax({
        type: "post",
        url: "/Location/UserList?rn=NULL",
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
   
    $("#demoGrid").DataTable({
     
        "sDom": "ltipr",
        //"order": [[10, "desc"]],
        "processing": true, // for show progress bar
        "serverSide": true, // for process server side
        "filter": true, // this is for disable filter (search box)
        "orderMulti": false, // for disable multiple column at once
        //"pageLength": 10,
        width: 670,
        "ajax": {
            "url": "/Datable/GetJqGridJson?rn=EmployeeSummary",
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
            "targets": [12],
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
              { "data": "daID", "name": "daID", "autoWidth": true },
              { "data": "UserName", "name": "userName", "autoWidth": true },
              { "data": "daDate", "name": "daDate", "autoWidth": true },
              { "data": "StartTime", "name": "startTime", "autoWidth": true },
              { "data": "DaEndDate", "name": "daEndDate", "autoWidth": true },
              { "data": "EndTime", "name": "endTime", "autoWidth": true },
              { "data": "Totalhousecollection", "name": "Totalhousecollection", "autoWidth": true },
            { "data": "TotalHouseScanTimeHours", "name": "TotalHouseScanTimeHours", "autoWidth": true },
              { "data": "Totaldumpyard", "name": "totaldumpyard", "autoWidth": true },
            { "data": "TotalDumpScanTimeHours", "name": "TotalDumpScanTimeHours", "autoWidth": true },
              { "data": "InBatteryStatus", "name": "InBatteryStatus", "autoWidth": true },
              { "data": "OutBatteryStatus", "name": "OutBatteryStatus", "autoWidth": true },
              { "data": "Totaldistance", "name": "Totaldistance", "autoWidth": true },       
              { "data": "daDateTIme", "name": "daDateTIme", "autoWidth": true },
        ],
        //Sort: "locId DESC"
    });

 
});

function test(id) {
    window.location.href = "/Attendence/Location?daId="+id;
};

function user_route(id) {
    window.location.href = "/Attendence/UserRoute?daId=" + id;
};
function map(a) {
    window.location.href = "/Location/viewLocation?teamId=" + a;

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
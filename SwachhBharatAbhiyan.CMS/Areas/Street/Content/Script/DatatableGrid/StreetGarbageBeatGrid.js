$(document).ready(function () {
    debugger;
    var UserId = $('#selectnumber').val();

    $.ajax({
        type: "post",
        url: "/Location/UserList?rn=S",
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
       // "order": [[10, "desc"]],
        "processing": true, // for show progress bar
        "serverSide": true, // for process server side
        "filter": true, // this is for disable filter (search box)
        "orderMulti": false, // for disable multiple column at once
        "pageLength": 10,

        "ajax": {
            "url": "/Datable/GetJqGridJson?rn=StreetSweepBeatMap",
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
                    "targets": [1],
                    "visible": false,
                    "searchable": false
                },
            ],

        "columns": [
            { "data": "RowCounts", "name": "RowCounts", "autoWidth": false },
            { "data": "userId", "name": "userId", "autoWidth": false },
            { "data": "Date", "name": "Date", "autoWidth": false },
            { "data": "EmpName", "name": "EmpName", "autoWidth": false },
            { "data": "ReferanceId1", "name": "ReferanceId1", "autoWidth": false },
            { "data": "ReferanceId2", "name": "ReferanceId2", "autoWidth": false },
            { "data": "ReferanceId3", "name": "ReferanceId3", "autoWidth": false },
            { "data": "Status", "name": "Status", "autoWidth": false },
            { "render": function (data, type, row) { return '<a  data-toggle="modal" class="tooltip1" style="cursor:pointer"  onclick="View(' + row.userId + ',' + row.RowCounts + ')" >View</a>'; }, "width": "10%" },
        ]
    });
});
function View(userId, RowCounts) {
    debugger
    //alert("Aa");
    var fdate = document.getElementById('txt_fdate').value;
    var tdate = document.getElementById('txt_tdate').value;
    if (userId != null) {
        var url = "/Street/StreetGarbage/MenuStreetDetailGarbageIndex?teamId=" + userId + "&fdate=" + fdate + "&tdate=" + tdate + "&param1=" + RowCounts;
        window.location.href = url;
    }
};
function DownloadQRCode(Id) {
    window.location.href = "/GarbagePoint/Export?Id=" + Id;
};

function noImageNotification() {
    document.getElementById("snackbar").innerHTML = "Image not uploaded...";
    var x = document.getElementById("snackbar");
    x.className = "show";
    setTimeout(function () { x.className = x.className.replace("show", ""); }, 3000);
}

function PopImages(cel) {

    $('#myModal_Image').modal('toggle');

    var addr = $(cel).find('.addr-length').text();
    var date = $(cel).find('.li_date').text();
    var imgsrc = $(cel).find('img').attr('src');
    var head = $(cel).find('.li_title').text();
    jQuery("#latlongData").text(addr);
    jQuery("#dateData").text(date);
    jQuery("#imggg").attr('src', imgsrc);
    //jQuery("#latlongData").text(cellValue);
    jQuery("#header_data").html(head);
}
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
    //ZoneId = $('#ZoneId').val();
    //WardId = $('#WardNo').val();
    //AreaId = $('#AreaId').val();
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
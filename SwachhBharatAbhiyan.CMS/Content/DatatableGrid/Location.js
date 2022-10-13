$(document).ready(function () {
    var UserId = $('#selectnumber').val();
    $.ajax({
        type: "post",
        url: "/Location/UserList",
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
        "order": [[ 5, "desc" ]],
        "processing": true, // for show progress bar
        "serverSide": true, // for process server side
        "filter": true, // this is for disable filter (search box)
        "orderMulti": false, // for disable multiple column at once      
       "pageLength": 10,

        "ajax": {
            "url": "/Datable/GetJqGridJson?rn=Location",
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
            "targets": [5],
            "visible": false,
            "searchable": false
        }
        ],

        "columns": [
              { "data": "locId", "name": "locId", "autoWidth": true },
              { "data": "userName", "name": "userName", "autoWidth": true },
              { "data": "date", "name": "date", "autoWidth": true },
              { "data": "time", "name": "time", "autoWidth": true },
              { "data": "latlong", "name": "latlong", "autoWidth": true },
              { "data": "CompareDate", "name": "CompareDate", "autoWidth": true },
            //  { "render": function (data, type, full, meta) { return '<a  data-toggle="modal" class="tooltip1" href="#"  onclick="test(' + full["locId"] + ')">View Map</span> </a>'; } },
              { "render": function (data, type, full, meta) { return '<a  data-toggle="modal" class="tooltip1" style="cursor:pointer"  onclick="test(' + full["locId"] + ')" ><i class="material-icons location-icon">location_on</i><span class="tooltiptext1">View Map</span> </a>'; }, "width": "10%" },

        ]
    });
});

function test(a)
{
    window.location.href = "/Location/viewLocation?teamId="+a;
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
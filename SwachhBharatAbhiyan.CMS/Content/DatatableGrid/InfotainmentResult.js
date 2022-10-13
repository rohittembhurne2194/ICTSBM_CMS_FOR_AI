$(document).ready(function () {
    var UserId = $('#selectnumber').val();
    $.ajax({
        type: "post",
        url: "/Infotainment/GameList",
        data: { userId: UserId },
        datatype: "json",
        traditional: true,
        success: function (data) {
            console.log(data);
            district = '<option value="-1">Select Game</option>';
            for (var i = 0; i < data.length; i++) {
                district = district + '<option value=' + data[i].GameId + '>' + data[i].GameName + '</option>';
            }
            //district = district + '</select>';
            $('#selectnumber').html(district);
        }
    });
    $("#demoGrid").DataTable({
        "sDom": "ltipr",
        "order": [[6, "desc"]],
        "processing": true, // for show progress bar
        "serverSide": true, // for process server side
        "filter": true, // this is for disable filter (search box)
        "orderMulti": false, // for disable multiple column at once      
        "pageLength": 10,

        "ajax": {
            "url": "/Datable/GetJqGridJson?rn=InfotainmentPlayerDetails",
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
            "targets": [6],
            "visible": false,
            "searchable": false
        },
        
        ],

        "columns": [
              { "data": "ID", "name": "ID", "autoWidth": true },
              { "data": "Name", "name": "Name", "autoWidth": true },
              { "data": "GameId", "name": "GameId", "autoWidth": true },
              { "data": "Mobile", "name": "Mobile", "autoWidth": true },
              { "data": "Score", "name": "Score", "autoWidth": true },
              { "data": "Created", "name": "Created", "autoWidth": true },
              { "data": "DisplayDateTime", "name": "DisplayDateTime", "autoWidth": true }
           
            //  { "render": function (data, type, full, meta) { return '<a  data-toggle="modal" class="tooltip1" href="#"  onclick="test(' + full["locId"] + ')">View Map</span> </a>'; } },
              //{ "render": function (data, type, full, meta) { return '<a  data-toggle="modal" class="tooltip1" style="cursor:pointer"  onclick="test(' + full["locId"] + ')" ><i class="material-icons location-icon">location_on</i><span class="tooltiptext1">View Map</span> </a>'; }, "width": "10%" },

        ]
    });

   // Search();
});

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
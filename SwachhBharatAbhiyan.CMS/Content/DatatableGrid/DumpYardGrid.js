$(document).ready(function () {
    $("#demoGrid").DataTable({
        "sDom": "ltipr",
        "order": [[0, "desc"]],
        "processing": true, // for show progress bar
        "serverSide": true, // for process server side
        "filter": true, // this is for disable filter (search box)
        "orderMulti": false, // for disable multiple column at once
        "pageLength": 10,

        "ajax": {
            "url": "/Datable/GetJqGridJson?rn=DumpYard",
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
            "targets": [3],
            "visible": false,
            "searchable": false
        }
        ],

        "columns": [
              { "data": "Id", "name": "Id", "autoWidth": false },
               { "data": "ReferanceId", "name": "ReferanceId", "autoWidth": false },
              { "data": "Name", "name": "Name", "autoWidth": false },
              { "data": "NameMar", "name": "NameMar", "autoWidth": false },
               {
                   "data": "QrCode", "name": "QrCode", "render": function (data, type, full, meta) {
                       return "<img src=\"" + data + "\" height=\"50\"/>";
                   }
               },
                 { "render": function (data, type, full, meta) { return '<input  class="btn btn-link" type="button" onclick="DownloadQRCode(' + full["Id"] + ')" value="Download" />'; } },
              { "data": "Zone", "autoWidth": false },
              { "data": "Ward", "autoWidth": false },
             { "data": "Area", "autoWidth": false },
              { "data": "Address", "autoWidth": false },
             { "render": function (data, type, full, meta) { return '<a  data-toggle="modal" class="tooltip1" style="cursor:pointer"  onclick="Edit(' + full["Id"] + ')" ><i class="material-icons edit-icon">edit</i><span class="tooltiptext1">Edit</span> </a>'; }, "width": "10%" },
             //<a  data-toggle="modal" style="cursor:pointer;margin-left:10px;" class="tooltip1" style="cursor:pointer" onclick="Delete(' + full["Id"] + ')" ><i class="material-icons delete-icon">delete</i><span class="tooltiptext1">Delete</span> </a>
        ]
    });
});

function DownloadQRCode(Id) {
    window.location.href = "/DumpYard/Export?id=" + Id;
};

function Edit(Id) {
    //alert("Aa");
    if (Id != null) {
        var url = "/DumpYard/AddDumpYardDetails?teamId=" + Id;
        window.location.href = url;
    }
};

function Delete(Id) {
    if (Id != null && Id != '') {

        if (confirm("Do you want delete selected Garbage Point?")) {
            var url = "/GarbagePoint/DeleteGarbagePoint?teamId=" + Id;
            window.location.href = url;
        }
    }
};



function Search() {
    var value = ",,," + $("#s").val();//txt_fdate + "," + txt_tdate + "," + UserId + "," + Client + "," + NesEvent + "," + Product + "," + catProduct + "," + 1;
    // alert(value );
    oTable = $('#demoGrid').DataTable();
    oTable.search(value).draw();
    oTable.search("");
    document.getElementById('USER_ID_FK').value = -1;
}

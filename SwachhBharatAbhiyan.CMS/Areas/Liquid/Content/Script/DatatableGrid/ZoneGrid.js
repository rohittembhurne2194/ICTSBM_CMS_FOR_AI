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
            "url": "/Datable/GetJqGridJson?rn=LiquidZoneDetail",
            "type": "POST",
            "datatype": "json"
        },

        "columnDefs":
        [{
            "targets": [0],
            "visible": false,
            "searchable": false
        }],

        "columns": [
            { "data": "LWzoneId", "name": "LWzoneId", "autoWidth": false },
            { "data": "LWname", "name": "LWname", "autoWidth": false },
            { "render": function (data, type, full, meta) { return '<a  data-toggle="modal" class="tooltip1" style="cursor:pointer" onclick="Edit(' + full["LWzoneId"] + ')" ><i class="material-icons edit-icon">edit</i><span class="tooltiptext1">Edit</span> </a>'; }, "width": "10%" },
       //<a  data-toggle="modal" style="cursor:pointer;margin-left:10px;" class="tooltip1" style="cursor:pointer" onclick="Delete(' + full["Id"] + ',' + full["Name"] + ')" ><i class="material-icons delete-icon">delete</i><span class="tooltiptext1">Delete</span> </a>
        ]
    });
});

function Edit(LWzoneId) {

    if (LWzoneId != null) {
        
        var url = "/Liquid/LiquidMainMaster/AddZoneDetails?teamId=" + LWzoneId;
        window.location.href = url;
    }
};

function Delete(LWzoneId) {
    if (LWzoneId != null && LWzoneId != '') {

        if (confirm("Do you want delete selected Area?")) {
            var url = "/LiquidMainMaster/DeleteArea?teamId=" + LWzoneId;
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

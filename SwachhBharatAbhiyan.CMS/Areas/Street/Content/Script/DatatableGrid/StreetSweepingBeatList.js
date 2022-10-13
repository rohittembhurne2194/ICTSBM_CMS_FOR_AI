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
            "url": "/Datable/GetJqGridJson?rn=StreetSweepBeat",
            "type": "POST",
            "datatype": "json"
        },


        "columns": [
            { "data": "Id", "name": "Id", "autoWidth": false },
            { "data": "ReferanceId1", "name": "ReferanceId1", "autoWidth": false },
            { "data": "ReferanceId2", "name": "ReferanceId2", "autoWidth": false },
            { "data": "ReferanceId3", "name": "ReferanceId3", "autoWidth": false },
            //{ "data": "ReferanceId4", "name": "ReferanceId4", "autoWidth": false },
            //{ "data": "ReferanceId5", "name": "ReferanceId5", "autoWidth": false },

        ]
    });
});

function DownloadQRCode(Id) {
    window.location.href = "/Street/StreetSweeping/Export?id=" + Id;
};

function Edit(Id) {
    //alert("Aa");
    if (Id != null) {
        var url = "/Street/StreetSweeping/AddStreetSweeping?teamId=" + Id;
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

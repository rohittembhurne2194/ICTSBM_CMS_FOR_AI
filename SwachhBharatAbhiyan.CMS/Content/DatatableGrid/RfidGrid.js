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
            "url": "/Datable/GetJqGridJson?rn=RfidDetail",
            "type": "POST",
            "datatype": "json"
        },

        "columnDefs":
        [
          //  {
          //  "targets": [0],
          //  "visible": false,
          //  "searchable": false
          //}
            {
                "targets": [4],

                "visible": true,

                "render": function (data, type, full, meta) {
                    if (full["type1"] == "0") {
                        return "<div class='circle' style='height: 20px;width: 20px;background-color: #f44336;border-radius: 50%;    vertical-align: middle;display: inline-flex;'></div> (Mixed Garbage)";
                    }
                    else if (full["type1"] == "1") {
                        return "<div class='circle' style='height: 20px;width: 20px;background-color: #388e3c;border-radius: 50%;vertical-align: middle;display: inline-flex;'></div> (Segregated Garbage)";

                    }

                    else {
                        return "<div class='circle' style='height: 20px;width: 20px;background-color: #fe9436;border-radius: 50%;vertical-align: middle;display: inline-flex;'></div> (Garbage not Received)";

                    }

                },
            },
       ],

        "columns": [
              { "data": "RFIDReaderId", "name": "RFIDReaderId", "autoWidth": false },
              { "data": "RFIDTagId", "name": "RFIDTagId", "autoWidth": false },
              { "data": "Lat", "name": "Lat", "autoWidth": false },
              { "data": "Long", "name": "Long", "autoWidth": false },
                 { "data": "type1", "name": "type1", "autoWidth": false },
              { "data": "attandDate", "name": "attandDate", "autoWidth": false },
        //{ "render": function (data, type, full, meta) { return '<a  data-toggle="modal" class="tooltip1" style="cursor:pointer"  onclick="Edit(' + full["Id"] + ')" ><i class="material-icons edit-icon">edit</i><span class="tooltiptext1">Edit</span> </a>'; }, "width": "10%" },
            ]
    });
});

function Edit(Id) {

    if (Id != null) {
        var url = "/MainMaster/AddStateDetails?teamId=" + Id;
        window.location.href = url;
    }
};

function Delete(Id) {
    if (Id != null && Id != '') {

        if (confirm("Do you want delete selected State")) {
            var url = "/MainMaster/DeleteState?teamId=" + Id;
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


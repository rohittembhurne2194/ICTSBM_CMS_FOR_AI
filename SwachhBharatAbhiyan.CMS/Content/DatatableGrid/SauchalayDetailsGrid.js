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
            "url": "/Datable/GetJqGridJson?rn=SauchalayDetail",
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
              { "data": "SauchalayFeedback_ID", "name": "SauchalayFeedback_ID", "autoWidth": false },
              { "data": "Date", "name": "Date", "autoWidth": false },
              { "data": "SauchalayID", "name": "SauchalayID", "autoWidth": false },
              { "data": "Fullname", "name": "Fullname", "autoWidth": false },
              { "data": "Address", "name": "Address", "autoWidth": false },
              { "data": "MobileNo", "name": "MobileNo", "autoWidth": false },
              { "data": "que1", "name": "que1", "autoWidth": false },
              { "data": "que2", "name": "que2", "autoWidth": false },
              { "data": "que3", "name": "que3", "autoWidth": false },
              { "data": "Rating", "name": "Rating", "autoWidth": false },
              { "data": "Feedback", "name": "Feedback", "autoWidth": false },



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

//function Search() {
//    var value = ",,," + $("#s").val();//txt_fdate + "," + txt_tdate + "," + UserId + "," + Client + "," + NesEvent + "," + Product + "," + catProduct + "," + 1;
//    // alert(value );
//    oTable = $('#demoGrid').DataTable();
//    oTable.search(value).draw();
//    oTable.search("");
//    document.getElementById('USER_ID_FK').value = -1;
//}
function Search() {
    var txt_fdate, txt_tdate, Client;
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
    var value = txt_fdate + "," + txt_tdate + "," +","+ $("#s").val();//txt_fdate + "," + txt_tdate + "," + UserId + "," + Client + "," + NesEvent + "," + Product + "," + catProduct + "," + 1;
    // alert(value );
    oTable = $('#demoGrid').DataTable();
    oTable.search(value).draw();
    oTable.search("");
    document.getElementById('USER_ID_FK').value = -1;
}

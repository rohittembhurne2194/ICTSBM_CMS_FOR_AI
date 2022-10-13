$(document).ready(function () {
    var UserId = $('#selectnumber').val();

    $.ajax({
        type: "post",
        url: "/WasteManagement/UserList",
        data: { userId: UserId, },
        datatype: "json",
        traditional: true,
        success: function (data) {
            var dataFilter;
            dataFilter = data.sort(function (a, b) {
                return a.Value - b.Value;
            });
            district = '<optgroup label="All User Type"><option value="-1">All User</option><option value="-3">Admin</option><option value="-2">Employees</option></optgroup><optgroup label="Employee Wise">';
            //for (var i = 0; i < data.length; i++) {
            for (var i = 0; i < dataFilter.length; i++) {
                district = district + '<option value=' + dataFilter[i].Value + '>' + dataFilter[i].Text + '</option>';

            }
            district = district + ' </optgroup>';
            //district = district + '</select>';
            $('#selectnumber').html(district);
        }
    });
    $("#demoGrid").DataTable({
        "sDom": "ltipr",
        "order": [[5, "desc"]],
        "processing": true, // for show progress bar
        "serverSide": true, // for process server side
        "filter": true, // this is for disable filter (search box)
        "orderMulti": false, // for disable multiple column at once
        "pageLength": 10,

        "ajax": {
            "url": "/Datable/GetJqGridJson?rn=WM_WetWaste",
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
        },
        ],

        "columns": [
              { "data": "GarbageDetailsID", "name": "GarbageDetailsID", "autoWidth": false },
              { "data": "SubCategoryName", "name": "SubCategoryName", "autoWidth": false },
              { "data": "Weight", "name": "Weight", "autoWidth": false },
              { "data": "UserName", "name": "UserName", "autoWidth": false },
              { "data": "CreatedDate", "name": "CreatedDate", "autoWidth": false },
              { "data": "DisplayTime", "name": "DisplayTime", "autoWidth": false },


        ]
    });


});


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
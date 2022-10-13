
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
    AttendenceSettings();
    // $('#demoGrid').css("display", "block");
});

function AttendenceSettings() {
    $("#demoGrid").DataTable({
        "sDom": "ltipr",
        "order": [[0, "desc"]],
        "processing": true, // for show progress bar
        "serverSide": true, // for process server side
        "filter": true, // this is for disable filter (search box)
        "orderMulti": false, // for disable multiple column at once
        "pageLength": 10,
        destroy: true,
        "ajax": {
            "url": "/Datable/GetJqGridJson?rn=AttendenceSettingsDetail",
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
            { "data": "userId", "name": "userId", "autoWidth": false },
            { "data": "userName", "name": "userName", "width": "30%" },
            { "data": "InTime", "name": "InTime", "width": "20%" },

            { "data": "NotificationTime", "name": "NotificationTime", "width": "13%" },

            { "data": "NotificationMobileNumber", "name": "NotificationMobileNumber", "width": "13%" },
            { "render": function (data, type, full, meta) { return '<a  data-toggle="modal" class="tooltip1" style="cursor:pointer"   onclick="Edit(' + full["userId"] + ')"  ><i class="material-icons edit-icon">edit</i><span class="tooltiptext1">Edit</span> </a>'; }, "width": "10%" },
            //<a  data-toggle="modal" style="cursor:pointer;margin-left:10px;" class="tooltip1" style="cursor:pointer"onclick="Delete(' + full["userId"] + ')" ><i class="material-icons delete-icon">delete</i><span class="tooltiptext1">Delete</span> </a>
        ]
    })
}

function noImageNotification() {
    document.getElementById("snackbar").innerHTML = "Image not uploaded...";
    var x = document.getElementById("snackbar");
    x.className = "show";
    setTimeout(function () { x.className = x.className.replace("show", ""); }, 3000);
}

function PopImages(cel) {

    $('#myModal_Image').modal('toggle');
    var imgsrc = $(cel).find('img').attr('src');
    var head = $(cel).find('.li_title').text();

    jQuery("#imggg").attr('src', imgsrc);
    //jQuery("#latlongData").text(cellValue);
    jQuery("#header_data").html(head);
}
function Edit(Id) {
    window.location.href = "/MainMaster/EditAttendenceSettingsDetail?teamId=" + Id;
};



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

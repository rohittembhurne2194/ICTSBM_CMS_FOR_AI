var myBoolean = 'True'; // (string)
$(document).ready(function () {
    debugger;
    Employee();
    // $('#demoGrid').css("display", "block");
    $('#demoGridNonActive').css("display", "none");
    $('#demoGridNonActive_wrapper').css("display", "none");
    $('#btn').hide();
   
   
    $('#NotActivebtn').on('click', function (e) {
        debugger;
        $('#demoGridNonActive').css("display", "block");
        $('#demoGrid').css("display", "none");
        $('#demoGrid_wrapper').css("display", "none");
        $('#change').text('Non Active Employee');
        $('#btn').show();
        NotActiveEmployee();
        e.preventDefault();
        myBoolean = myBoolean !== 'false'; //false (boolean)
    });

});
function goBack() {
    location.reload();
}
function Employee() {
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
            "url": "/Datable/GetJqGridJson?rn=ActiveDumpyardEmployee",
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
                "targets": [2],
                "visible": false,
                "searchable": false
            },
            {
                "targets": [5],
                "visible": true,

                "render": function (data, type, full, meta) {
                    if (full["userProfileImage"] != "/Images/default_not_upload.png") {
                        return "<div style='cursor:pointer;display:inline-flex;'  onclick=PopImages(this)><img alt='Photo Not Found'  src='" + data +
                            "' style='height:35px;width:35px;cursor:pointer;margin-left:0px;'></img><span><ul class='dt_pop'  style='margin:2px -5px -5px -5px; padding:0px;list-style:none;display:none;'><li  class='li_date datediv' >" + full["attandDate"] + "</li><li class='addr-length' style='margin:0px 0px 0px 10px;'>"
                            + full["Address"] + "</li><li style='display:none' class='li_title' >Image </li></ul></span></div>";
                    }
                    else {

                        return "<img alt='Photo Not Found' onclick='noImageNotification()' src='/Images/default_not_upload.png' style='height:35px;width:35px;cursor:pointer;'></img>";
                    }
                },
            }
            ],

        "columns": [
            { "data": "userId", "name": "userId", "autoWidth": false },
            { "data": "userName", "name": "userName", "width": "30%" },
            { "data": "userNameMar", "name": "userNameMar", "width": "20%" },
            { "data": "userMobileNumber", "name": "userMobileNumber", "width": "13%" },
            { "data": "userEmployeeNo", "name": "userEmployeeNo", "width": "15%" },
            { "data": "userProfileImage", "name": "userProfileImage", "width": "15%" },
            { "data": "userAddress", "name": "userAddress", "width": "25%" },
            { "data": "bloodGroup", "name": "bloodGroup", "width": "25%" },
            { "data": "isActive", "name": "isActive", "width": "25%" },
            { "data": "gcTarget", "name": "gcTarget", "width": "25%" },
            { "render": function (data, type, full, meta) { return '<a  data-toggle="modal" class="tooltip1" style="cursor:pointer"   onclick="Edit(' + full["userId"] + ')"  ><i class="material-icons edit-icon">edit</i><span class="tooltiptext1">Edit</span> </a>'; }, "width": "10%" },
            //<a  data-toggle="modal" style="cursor:pointer;margin-left:10px;" class="tooltip1" style="cursor:pointer"onclick="Delete(' + full["userId"] + ')" ><i class="material-icons delete-icon">delete</i><span class="tooltiptext1">Delete</span> </a>
        ]
    })
}




function NotActiveEmployee() {

    $("#demoGridNonActive").DataTable({
        "sDom": "ltipr",
        "order": [[0, "desc"]],
        "processing": true, // for show progress bar
        "serverSide": true, // for process server side
        "filter": true, // this is for disable filter (search box)
        "orderMulti": false, // for disable multiple column at once
        "pageLength": 10,
        destroy: true,

        "ajax": {
            "url": "/Datable/GetJqGridJson?rn=NotActiveDumpyardEmployee",
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
                "targets": [2],
                "visible": false,
                "searchable": false
            },
            {
                "targets": [5],
                "visible": true,

                "render": function (data, type, full, meta) {
                    if (full["userProfileImage"] != "/Images/default_not_upload.png") {
                        return "<div style='cursor:pointer;display:inline-flex;'  onclick=PopImages(this)><img alt='Photo Not Found'  src='" + data +
                            "' style='height:35px;width:35px;cursor:pointer;margin-left:0px;'></img><span><ul class='dt_pop'  style='margin:2px -5px -5px -5px; padding:0px;list-style:none;display:none;'><li  class='li_date datediv' >" + full["attandDate"] + "</li><li class='addr-length' style='margin:0px 0px 0px 10px;'>"
                            + full["Address"] + "</li><li style='display:none' class='li_title' >Image </li></ul></span></div>";
                    }
                    else {

                        return "<img alt='Photo Not Found' onclick='noImageNotification()' src='/Images/default_not_upload.png' style='height:35px;width:35px;cursor:pointer;'></img>";
                    }
                },
            }
            ],

        "columns": [
            { "data": "userId", "name": "userId", "autoWidth": false },
            { "data": "userName", "name": "userName", "width": "30%" },
            { "data": "userNameMar", "name": "userNameMar", "width": "20%" },
            { "data": "userMobileNumber", "name": "userMobileNumber", "width": "13%" },
            { "data": "userEmployeeNo", "name": "userEmployeeNo", "width": "15%" },
            { "data": "userProfileImage", "name": "userProfileImage", "width": "15%" },
            { "data": "userAddress", "name": "userAddress", "width": "25%" },
            { "data": "bloodGroup", "name": "bloodGroup", "width": "25%" },
            { "data": "isActive", "name": "isActive", "width": "25%" },
            { "data": "gcTarget", "name": "gcTarget", "width": "25%" },
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
    window.location.href = "/DumpyardEmployee/AddEmployeeDetails?teamId=" + Id;
};
function Delete(Id) {
    window.location.href = "/DumpyardEmployee/DeleteEmployee?teamId=" + Id;
};


//function Search() {
   
//        var value = ",,," + $("#s").val();//txt_fdate + "," + txt_tdate + "," + UserId + "," + Client + "," + NesEvent + "," + Product + "," + catProduct + "," + 1;
//        // alert(value );
//        oTable = $('#demoGrid').DataTable();
//        //  oTable = $('#demoGridNonActive').DataTable();
//        oTable.search(value).draw();
//        oTable.search("");
//        document.getElementById('USER_ID_FK').value = -1;
   
//}


function Search() {
    debugger;
    if (myBoolean=== "True") {
        var value = ",,," + $("#s").val();//txt_fdate + "," + txt_tdate + "," + UserId + "," + Client + "," + NesEvent + "," + Product + "," + catProduct + "," + 1;
        // alert(value );
        oTable = $('#demoGrid').DataTable();
        //  oTable = $('#demoGridNonActive').DataTable();
        oTable.search(value).draw();
        oTable.search("");
        document.getElementById('USER_ID_FK').value = -1;
    }
    else {
        var value = ",,," + $("#s").val();//txt_fdate + "," + txt_tdate + "," + UserId + "," + Client + "," + NesEvent + "," + Product + "," + catProduct + "," + 1;
        // alert(value );
        oTable = $('#demoGridNonActive').DataTable();
        oTable.search(value).draw();
        oTable.search("");
        document.getElementById('USER_ID_FK').value = -1;

    }
}

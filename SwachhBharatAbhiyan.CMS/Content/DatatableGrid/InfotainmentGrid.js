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
            "url": "/Datable/GetJqGridJson?rn=Infotainment",
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
                 "visible": true,

                 "render": function (data, type, full, meta) {
                     if (full["Image"] != "/Images/default_not_upload.png") {
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
              { "data": "GameDetailsId", "name": "GameDetailsId", "autoWidth": false },
              { "data": "GameName", "name": "GameName", "width": "30%" },
              //{ "data": "GameNameMar", "name": "GameNameMar", "width": "30%" },
              { "data": "Slogan", "name": "Slogan", "width": "20%" },
              { "data": "Image", "name": "Image", "width": "15%" },
              { "data": "RightAnswer", "name": "RightAnswer", "width": "25%" },
              { "data": "Points", "name": "Points", "width": "25%" },
        { "render": function (data, type, full, meta) { return '<a  data-toggle="modal" class="tooltip1" style="cursor:pointer"   onclick="Edit(' + full["GameDetailsId"] + ')"  ><i class="material-icons edit-icon">edit</i><span class="tooltiptext1">Edit</span> </a>'; }, "width": "10%" },
         ]
    });
});
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
    window.location.href = "/Infotainment/AddInfotainmentDetails?ID=" + Id;
};
//function Delete(Id) {
//    window.location.href = "/Infotainment/DeleteInfotainmentDetails?ID=" + Id;
//};


function Search() {
    var value = ",,," + $("#s").val();//txt_fdate + "," + txt_tdate + "," + UserId + "," + Client + "," + NesEvent + "," + Product + "," + catProduct + "," + 1;
    // alert(value );
    oTable = $('#demoGrid').DataTable();
    oTable.search(value).draw();
    oTable.search("");
    document.getElementById('USER_ID_FK').value = -1;
}

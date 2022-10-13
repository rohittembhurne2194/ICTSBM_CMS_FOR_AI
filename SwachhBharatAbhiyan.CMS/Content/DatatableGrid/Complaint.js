$(document).ready(function () {
  
    $("#demoGrid").DataTable({
        "sDom": "ltipr",
        "order": [[0, "desc"]],
        "processing": true, // for show progress bar
        "serverSide": true, // for process server side
        "filter": true, // this is for disable filter (search box)
        "orderMulti": false, // for disable multiple column at once
        //"pageLength": 5,

        "ajax": {
            "url": "/Datable/GetJqGridJson?rn=Complaint",
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
            "targets": [8],
            "visible": false,
            "searchable": false
        }
        ],


        "columns": [
              { "data": "complaintId", "name": "complaintId", "autoWidth": true },
               { "data": "date", "name": "date", "autoWidth": true },
                { "data": "houseNumber", "name": "houseNumber", "autoWidth": true },           
              { "data": "place", "name": "place", "autoWidth": true },
              { "data": "wardNo", "name": "wardNo", "autoWidth": true },
              { "data": "details", "name": "details", "autoWidth": true },
              { "data": "status", "name": "status", "autoWidth": true },           
               {
                   "data": "startImage", "name": "startImage", "render": function (data, type, full, meta) {
                       return "<div style='cursor:pointer;display:inline-flex;'  onclick=PopImages(this)><img alt='No Photo'  src='" + data +
                       "' style='height:35px;width:35px;cursor:pointer;margin-left:0px;'></img><span><ul class='dt_pop'  style='margin:2px -5px -5px -5px; padding:0px;list-style:none;display:none;'><li  class='li_date datediv' >" + full["date"] + "</li><li class='addr-length' style='margin:0px 0px 0px 10px;'>"
                       + full["address"] + "</li><li style='display:none' class='li_title' >Photo </li></ul></span></div>";
                           }
               },

                 //{
                 //    "data": "endImage", "name": "endImage", "render": function (data, type, full, meta) {
                 //        return "<div style='cursor:pointer;display:inline-flex;'  onclick=PopImages(this)><img alt='No Photo '  src='" + data +
                 //        "' style='height:35px;width:35px;cursor:pointer;margin-left:0px;'></img><span><ul class='dt_pop'  style='margin:2px -5px -5px -5px; padding:0px;list-style:none;display:none;'><li  class='li_date datediv' >" + full["date"] + "</li><li class='addr-length' style='margin:0px 0px 0px 10px;'>"
                 //        + full["address"] + "</li><li style='display:none' class='li_title' > अपडेटेड फोटो / Updated Photo </li></ul></span></div>";
                 //        }
                 //},
               
              { "data": "comment", "name": "comment", "autoWidth": true },
              { "data": "tips", "name": "tips", "autoWidth": true },
              { "data": "refId", "name": "refId", "autoWidth": true },
              { "data": "typeMar", "name": "typeMar", "autoWidth": true },
              { "data": "address", "name": "address", "autoWidth": true },
            
        ],
        Sort: "locId DESC"
    });
});



function PopImages(cel) {

    $('#myModal_Image').modal('toggle');

    var addr = $(cel).find('.addr-length').text();
    var date = $(cel).find('.li_date').text();
    var imgsrc = $(cel).find('img').attr('src');
    var head = $(cel).find('.li_title').text();
    jQuery("#latlongData").text(addr);
    jQuery("#dateData").text(date);
    jQuery("#imggg").attr('src', imgsrc);
    //jQuery("#latlongData").text(cellValue);
    jQuery("#header_data").html(head);
}
function test(id) {
    window.location.href = "/Attendence/Location?daId="+id;
};


function map(a) {
    window.location.href = "/Location/viewLocation?teamId=" + a;

};
//////////////////////////////////////////////////////////////////////////////
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
    var value = txt_fdate + "," + txt_tdate + ",-1," + $("#s").val();//txt_fdate + "," + txt_tdate + "," + UserId + "," + Client + "," + NesEvent + "," + Product + "," + catProduct + "," + 1;
    // alert(value );
    oTable = $('#demoGrid').DataTable();
    oTable.search(value).draw();
    oTable.search("");
    document.getElementById('USER_ID_FK').value = -1;
}
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
            "url": "/Datable/GetJqGridJson?rn=LeagueDetail",
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
            "targets": [7],
            "visible": false,
            "searchable": false
        }

        ],

        "columns": [
              { "data": "ID", "name": "ID", "autoWidth": false },
              { "data": "HouseId", "name": "HouseId", "autoWidth": false },
              { "data": "Name", "name": "Name", "autoWidth": false },
              { "data": "Zone", "name": "Zone", "autoWidth": false },
              { "data": "Ward", "name": "Ward", "autoWidth": false },
              { "data": "Area", "name": "Area", "autoWidth": false },
              { "data": "AnsDate", "name": "AnsDate", "autoWidth": false },
              { "data": "JSon", "name": "JSon", "autoWidth": false },
              {
                  "render": function (data, type, full, meta) {
                      JSONString[full["ID"]] = JSON.parse(full["JSon"]);
                      return '<a  data-toggle="modal" class="tooltip1" style="cursor:pointer;color:blue;" onclick="PopDetails(' + full["ID"] + ')"><span class="tooltiptext1">View</span> View More</a>';
                  }, "width": "10%"
              },
        
        ]
    });
});

var JSONString = {};

//function Edit(ID) {

//    if (Id != null) {
//        var url = "/League/ViewDetails?ID=" + Id;
//        window.location.href = url;
//    }
//};

function PopDetails(JSon) {

  //  var json_obj = JSON.parse(JSon);
    //console.log(JSONString[JSon]);
    var data = JSONString[JSon];
    $('#myModal_Image').modal('toggle');

    // var date = $(cel).find('.li_date').text();
    jQuery("#que").html('');

    for (i = 0; i <= data.length; i++) {
        jQuery("#que").append("<p style='text-align:left;'> <b>Question : </b>" + data[i].QuestionID + "</p>");

        jQuery("#que").append("<p style='text-align:left;'> <b>Answer : </b>" + data[i].Answer + "</p>");

        if ((i + 1) != data.length) {
            jQuery("#que").append("<hr style='height:5px;'>");
        }

        //jQuery("#que").append('<p>(JSon[i].QuestionId)</p>');
       // jQuery("#ans").append((JSon[i].Answer);
    }
    
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
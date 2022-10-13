$(document).ready(function () {
    
    $("#demoGrid").DataTable({
        "sDom": "ltipr",
        "order": [[0, "desc"]],
        "processing": true, // for show progress bar
        "serverSide": true, // for process server side
        "filter": true, // this is for disable filter (search box)
        "orderMulti": false, // for disable multiple column at once
        "pageLength": 10,
        "pagingType": "input",
        "oLanguage": {
            "sInfo": "Showing _START_ to _END_ ",// text you want show for info section
        },

        "ajax": {
            "url": "/Datable/GetJqGridJson?rn=HouseDetail",
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
            
              { "data": "houseId", "name": "houseId", "autoWidth": false },
              { "data": "ReferanceId", "name": "ReferanceId", "autoWidth": false },
              { "data": "Name", "name": "Name", "autoWidth": false },
              {
                   "data": "QRCode", "name": "QRCode", "render": function (data, type, full, meta) {
                       return "<img src=\"" + data + "\" height=\"50\"/><span><input class=\"btn btn-link\" type=\"button\" onclick='SaveQRCode(" + full["houseId"] + ")'  value=\"Send Link\"/></span>";
                   }
               },
              { "render": function (data, type, full, meta) { return '<input  class="btn btn-link" type="button" onclick="DownloadQRCode(' + full["houseId"] + ')" value="Download" />'; } },

              
            { "data": "zone", "name": "zone", "autoWidth": false },
            { "data": "WardNo", "name": "WardNo","autoWidth": false },
              { "data": "Area", "name": "Area", "autoWidth": false },
            { "data": "houseNo", "name": "houseNo", "autoWidth": false },
            { "data": "Mobile", "name": "Mobile","autoWidth": false },
            { "data": "Address", "name": "Address", "autoWidth": false },
            { "data": "Property_Type", "name": "Property_Type", "autoWidth": false },
            { "data": "OccupancyStatus", "name": "OccupancyStatus", "autoWidth": false },
            //   { "render": function (data, type, full, meta) { return '<input class="btn btn-primary btn-sm" type="button" onclick="Edit(' + full["houseId"] + ')" value="Edit" /> <input style="margin-left:2px" class="btn btn-danger btn-sm" type="button" onclick="Delete(' + full["houseId"] + ',' + full["Name"] + ')" value="Delete" />'; } }
        { "render": function (data, type, full, meta) { return '<a  data-toggle="modal" class="tooltip1" style="cursor:pointer"  onclick="Edit(' + full["houseId"] + ')" ><i class="material-icons edit-icon">edit</i><span class="tooltiptext1">Edit</span> </a>'; }, "width": "10%" },
      //<a  data-toggle="modal" style="cursor:pointer;margin-left:10px;" class="tooltip1" style="cursor:pointer" onclick="Delete(' + full["houseId"] + ')" ><i class="material-icons delete-icon">delete</i><span class="tooltiptext1">Delete</span> </a>
        ]
    });
});

function DownloadQRCode(Id) {
    window.location.href = "/HouseMaster/Export?Id=" + Id;
};

function Edit(Id) {
    //alert("Aa");
    if (Id != null) {
        var url = "/HouseMaster/AddHouseDetails?teamId=" + Id;
       
        window.location.href = url;
     
    }
};

function Delete(Id) {
    if (Id != null && Id != '') {

        if (confirm("Do you want delete selected House Details")) {
            var url = "/HouseMaster/DeleteHouse?teamId=" + Id;
            window.location.href = url;
        }
    }
};

function SaveQRCode(Id) {
  
    var UserId = $('#AreaId').val();
    $('#sendlink_pop').modal('toggle');
    $("#send_msg").css('color', 'red');
    //$('#send_msg').html('कृपया थांबा / Please Wait...');
    $('#send_msg').html('Please Wait...');

    $.ajax({
        type: "post",
        url: "/HouseMaster/Save?id="+Id,
        data: { userId: UserId },
        datatype: "json",
        traditional: true,
        success: function (data) {
           
            //alert("Send Successfully");
            //$('#send_msg').html('आपला संदेश पाठवला गेला आहे / Your message has been sent...');
            $('#send_msg').html('Your message has been sent...');
                    $("#send_msg").css('color', 'green');
        }
    });


};

function Search() {
    var value = ",,," + $("#s").val();//txt_fdate + "," + txt_tdate + "," + UserId + "," + Client + "," + NesEvent + "," + Product + "," + catProduct + "," + 1;
    // alert(value );
    oTable = $('#demoGrid').DataTable();
    oTable.search(value).draw();
    oTable.search("");
    document.getElementById('USER_ID_FK').value = -1;
}

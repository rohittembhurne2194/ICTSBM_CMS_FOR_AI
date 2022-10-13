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
            "url": "/Datable/GetJqGridJson?rn=onepointsixDetail",
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
                { "data": "INSERT_ID", "name": "INSERT_ID", "autoWidth": false },
                {
                    "data": "INSERT_DATE", "name": "INSERT_DATE", "render": function dateTimeFormat(dateTimeValue) {
                        var dt = new Date(parseInt(dateTimeValue.replace(/(^.*\()|([+-].*$)/g, '')));
                        var dateTimeFormat = dt.getDate() + "/" + (dt.getMonth() + 1) + "/" + dt.getFullYear() + ' ' + dt.getHours() + ":" + dt.getMinutes() + ":" + dt.getSeconds();
                        return dateTimeFormat;
                    }
                },




            { "render": function (data, type, full, meta) { return '<a  data-toggle="modal" class="tooltip1" style="cursor:pointer"  onclick="View(' + full["INSERT_ID"] + ')" > <i class="material-icons">zoom_in</i><span class="tooltiptext1">View</span> </a>'; }, "width": "10%" },
            { "render": function (data, type, full, meta) { return '<a  data-toggle="modal" class="tooltip1" style="cursor:pointer"  onclick="Edit(' + full["INSERT_ID"] + ')" ><i class="material-icons edit-icon">edit</i><span class="tooltiptext1">Edit</span> </a>'; }, "width": "10%" },

        ]
    });
});



function View(INSERT_ID) {
    if (INSERT_ID != null) {
        var url = "/Report/OnePointSix20201?INSERT_ID=" + INSERT_ID;
        window.location.href = url;
    }
};

function Edit(INSERT_ID) {
    if (INSERT_ID != null) {
        var url = "/Report/MenuOnePointSixDetails?INSERT_ID=" + INSERT_ID;
        window.location.href = url;
    }
};



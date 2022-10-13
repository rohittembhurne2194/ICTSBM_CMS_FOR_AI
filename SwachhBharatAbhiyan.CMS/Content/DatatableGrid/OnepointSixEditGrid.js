$(document).ready(function () {

    var INSERT_ID = $("#HIDDEN_INDEX_ID").val()
    $("#demoGrid").DataTable({
        "sDom": "ltipr",
        "order": [[0, "desc"]],
        "processing": true, // for show progress bar
        "serverSide": true, // for process server side
        "filter": true, // this is for disable filter (search box)
        "orderMulti": false, // for disable multiple column at once
        "pageLength": 10,

        "ajax": {
            "url": "/Datable/GetJqGridJson?rn=onepointSixEdit&INSERT_ID=" + INSERT_ID,
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
              "targets": [1],
              "visible": false,
              "searchable": false
          },

    {
        "targets": [2],
        "visible": false,
        "searchable": false
    }],



        "columns": [

                { "data": "ANS_ID", "name": "ANS_ID", "autoWidth": false },
                { "data": "INSERT_ID", "name": "INSERT_ID", "autoWidth": false },
                { "data": "Q_ID", "name": "Q_ID", "autoWidth": false },

                {
                    "data": "INSERT_DATE", "name": "INSERT_DATE", "render": function dateTimeFormat(dateTimeValue) {
                        var dt = new Date(parseInt(dateTimeValue.replace(/(^.*\()|([+-].*$)/g, '')));
                        var dateTimeFormat = dt.getDate() + "/" + (dt.getMonth() + 1) + "/" + dt.getFullYear() + ' ' + dt.getHours() + ":" + dt.getMinutes() + ":" + dt.getSeconds();
                        return dateTimeFormat;
                    }
                },
                { "data": "TOTAL_COUNT", "name": "TOTAL_COUNT", "autoWidth": false },

            { "render": function (data, type, full, meta) { return '<a  data-toggle="modal" class="tooltip1" style="cursor:pointer"  onclick="Edit(' + full["ANS_ID"] + ')" ><i class="material-icons edit-icon">edit</i><span class="tooltiptext1">Edit</span> </a>'; }, "width": "10%" },

        ]
    });
});


function Edit(ANS_ID) {
    if (ANS_ID != null) {
        var url = "/Report/MenuOnePointSixEdit?ANS_ID=" + ANS_ID;
        window.location.href = url;
    }
};



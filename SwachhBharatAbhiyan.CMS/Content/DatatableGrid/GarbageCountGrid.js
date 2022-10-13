
$(document).ready(function () {

    $('#demoGrid_filter').hide();
    var UserId = $('#selectnumber').val();
    
    $.ajax({
        type: "post",
        url: "/Location/UserList",
        data: { userId: UserId, },
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
    $("#demoGrid").DataTable({
        buttons: [

                       { extend: 'excel', className: 'btn btn-sm btn-success filter-button-style', title: 'Ghanta Gadi Garbage Count Report', text: 'Export to Excel', },
        ],
        //"sDom": "ltipr",
        dom: 'lBfrtip',
        lbFilter: false,

        //"sDom": "ltipr",
        "order": [[0, "desc"]],
        "processing": true, // for show progress bar
        "serverSide": true, // for process server side
        "filter": true, // this is for disable filter (search box)
        "orderMulti": false, // for disable multiple column at once
        "pageLength": 10,
        "ajax": {
            "url": "/Datable/GetJqGridJson?rn=GarbageCount",
            "type": "POST",
            "datatype": "json"
        },

        "columnDefs":
        [
        ],

        "columns": [
              { "data": "UserName", "name": "UserName", "autoWidth": false },
               { "data": "_Count", "name": "_Count", "autoWidth": false },
              { "data": "FromDate", "name": "FromDate", "autoWidth": false },
              { "data": "ToDate", "name": "ToDate", "autoWidth": false },
             { "data": "StartTime", "name": "ToDate", "autoWidth": false }
             
        ]
    });

    $("#demoGrid").DataTable().buttons().container()
           .appendTo('#demoGrid_wrapper .col-md-6:eq(1)').addClass('float-right');


    'use strict';

    'use strict';


    $.ajax({
        type: "post",
        url: "/GarbageCollection/UserCount",
        data: { userId: UserId, },
        datatype: "json",
        traditional: true,
        success: function (data) {

            var ary = []
            var ary2 = []
            for (var i = 0; i < data.length; i++) {

                ary.push({ label: data[i].UserName, y: data[i]._Count });

                //bb.push(data[i].UserName);

               // alert(data[i]._Count);

            }
            console.log(ary);
            // console.log(ary2);

            var chart = new CanvasJS.Chart("chartContainer", {
                theme: "light1", // "light1", "ligh2", "dark1", "dark2"
                animationEnabled: true,
                title: {
                    text: "घर संकलन"
                },

                data: [{
                    type: "column",
                    indexLabel: "{y}",
                    indexLabelFontColor: "red",
                    dataPoints: ary
                }]
            });
            chart.render();


        }
    });;
    chart.render();


});



function showInventoriesGrid() {
    Search();
}



function Search1() {
    var txt_fdate, txt_tdate;
    var name = [];
    var arr = [$('#txt_fdate').val(), $('#txt_tdate').val()];

    for (var i = 0; i <= arr.length - 1; i++) {
        name = arr[i].split("/");
        arr[i] = name[1] + "/" + name[0] + "/" + name[2];
    }

    txt_fdate = arr[0];
    txt_tdate = arr[1];


    UserId = $('#selectnumber').val();
    $.ajax({
        type: "post",
        url: "/GarbageCollection/UserCount?fDate=" + txt_fdate + "&tdate=" + txt_tdate + "&userId=" + UserId,
        data: { userId: UserId, },
        datatype: "json",
        traditional: true,
        success: function (data) {
            
            var ary = []
            var ary2 = []
            for (var i = 0; i < data.length; i++) {

                ary.push({ label: data[i].UserName, y: data[i]._Count });

                //bb.push(data[i].UserName);

               // alert(data[i]._Count);

            }
       //     console.log(ary);
            // console.log(ary2);

            var chart = new CanvasJS.Chart("chartContainer", {
                theme: "light1", // "light1", "ligh2", "dark1", "dark2"
                animationEnabled: true,
                title: {
                    text: "Household Collection"
                },

                data: [{
                    type: "column",
                    indexLabel: "{y}",
                    indexLabelFontColor: "red",
                    dataPoints: ary
                }]
            });
            chart.render();           
        }
    });
    chart.render();

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
    var value = txt_fdate + "," + txt_tdate + "," + UserId + "," + $("#s").val();//txt_fdate + "," + txt_tdate + "," + UserId + "," + Client + "," + NesEvent + "," + Product + "," + catProduct + "," + 1;
    // alert(value );
    
    oTable = $('#demoGrid').DataTable();
 
  
    oTable.search(value).draw();
    oTable.search("");
    Search1();
    document.getElementById('USER_ID_FK').value = -1;
  

}
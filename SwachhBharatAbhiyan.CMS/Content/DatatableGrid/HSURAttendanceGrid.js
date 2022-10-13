function LoadAGridP() {
    debugger;

    var UserId = $('#selectnumber').val();
    $.ajax({
        type: "post",
        url: "/HouseScanifyEmp/HSEmployeeList?rn=P",
        data: { userId: UserId },
        datatype: "json",
        traditional: true,
        success: function (data) {
            district = '<option value="-1">Select Employee</option>';
            for (var i = 0; i < data.length; i++) {
                if (data[i].Value != 0) {
                    district = district + '<option value=' + data[i].Value + '>' + data[i].Text + '</option>';
                }
            }
            //district = district + '</select>';
            $('#selectnumber').html(district);
        }
    });

    $("#demoGridP").dataTable().fnDestroy();
    $("#demoGridP").DataTable({
        "sDom": "ltipr",
        //   "order": [[11, "desc"]],
        "processing": true, // for show progress bar
        "serverSide": true, // for process server side
        "filter": true, // this is for disable filter (search box)
        "orderMulti": false, // for disable multiple column at once
        "pageLength": 10,

        "ajax": {
            "url": "/Datable/GetJqGridJson?rn=HSURAttendance&clientName=P",
            "type": "POST",
            "datatype": "json"
        },


        "columnDefs":
            [{
                "targets": [0],
                "visible": false,
                "searchable": false
            },

            ],


        "columns": [
            { "data": "EmpId", "name": "EmpId", "autoWidth": true },
         
            { "data": "EmpName", "name": "EmpName", "autoWidth": true },
            //{ "data": "startDate", "name": "startDate", "autoWidth": true },
            //{ "data": "StartTime", "name": "StartTime", "autoWidth": true },
            {
                "data": "startDate", "name": "startDate", "render": function (data, type, full, meta) {
                    var SDT = full.startDate + ' ' + full.StartTime;
                    return SDT;
                }
            },
            //{ "data": "EndDate", "name": "EndDate", "autoWidth": true },
            //{ "data": "endTime", "name": "endTime", "autoWidth": true },
            {
                "data": "EndDate", "name": "EndDate", "render": function (data, type, full, meta) {
                    if (full["EndDate"] == null || full["EndDate"] == "") {
                        return '';
                    }
                    else {
                      
                        return full.EndDate + ' ' + full.endTime;
                    }
                }
            },
            {
                "data": "EmployeeType", "name": "EmployeeType", "render": function (data, type, full, meta) {
                    if (full["EmployeeType"] == 'A') {
                        return 'Admin';
                    }
                    else if (full["EmployeeType"] == 'SA') {
                        return 'Supervisor';
                    }
                    else if (full["EmployeeType"] == 'SUA') {
                        return 'Super Admin';
                    }
                    else {
                        return 'Not Available';
                    }
                }
            },

            {
                "data": "isActive", "name": "isActive", "render": function (data, type, full, meta) {
                    if (full["isActive"] == '1') {
                        return 'Active';
                    }

                    if (full["isActive"] == '0') {
                        return 'Non Active';
                    }

                    else {
                        return 'Not Available';
                    }
                }
            },
            { "data": "ipaddress", "name": "ipaddress", "autoWidth": true },
            {
                "data": "type", "name": "type", "render": function (data, type, full, meta) {
                    if (full["logindevice"] == 'MB') {
                        return 'Mobile Browser';
                    }

                    if (full["logindevice"] == 'PC') {
                        return 'PC';
                    }

                    else {
                        return 'Not Available';
                    }
                }
            },
            { "data": "HostName", "name": "HostName", "autoWidth": true },
            //{
            //    "render": function (data, type, full, meta) {

            //        if (full["type"] == 'SA') {
            //            return '<a  data-toggle="modal" class="tooltip1" style="cursor:pointer"   onclick="Edit(' + full["EmpId"] + ')"  ><i class="material-icons edit-icon">edit</i><span class="tooltiptext1">Edit</span> </a>';

            //        }
            //        else {
            //            return '<a  data-toggle="modal" class="tooltip1" style="cursor:pointer"   onclick="myFunction()"  ><i class="material-icons edit-icon">edit</i><span class="tooltiptext1">Edit</span> </a>';
            //        }
            //    }, "width": "10%"
            //},


        ],

    });

}

function myFunction() {
    alert("Admin ID Are Not Available To Edit");
}

function LoadAGridM() {
    debugger;


    var UserId = $('#selectnumber').val();
    $.ajax({
        type: "post",
        url: "/HouseScanifyEmp/HSEmployeeList?rn=M",
        data: { userId: UserId },
        datatype: "json",
        traditional: true,
        success: function (data) {
            district = '<option value="-1">Select Employee</option>';
            for (var i = 0; i < data.length; i++) {
                if (data[i].Value != 0) {
                    district = district + '<option value=' + data[i].Value + '>' + data[i].Text + '</option>';
                }
               
            }
            //district = district + '</select>';
            $('#selectnumber').html(district);
        }
    });

    $("#demoGrid").dataTable().fnDestroy();
    $("#demoGrid").DataTable({
        "sDom": "ltipr",
        //   "order": [[11, "desc"]],
        "processing": true, // for show progress bar
        "serverSide": true, // for process server side
        "filter": true, // this is for disable filter (search box)
        "orderMulti": false, // for disable multiple column at once
        "pageLength": 10,

        "ajax": {
            "url": "/Datable/GetJqGridJson?rn=HSURAttendance&clientName=M",
            "type": "POST",
            "datatype": "json"
        },

        "columnDefs":
            [{
                "targets": [0],
                "visible": false,
                "searchable": false
            },

            ],


        "columns": [
            { "data": "EmpId", "name": "EmpId", "autoWidth": true },
            { "data": "EmpName", "name": "EmpName", "autoWidth": true },
            //{ "data": "startDate", "name": "startDate", "autoWidth": true },
            //{ "data": "StartTime", "name": "StartTime", "autoWidth": true },
            {
                "data": "creator", "name": "startDate", "render": function (data, type, full, meta) {
                    return full.startDate + ' ' + full.StartTime;
                }
            },
            //{ "data": "EndDate", "name": "EndDate", "autoWidth": true },
            //{ "data": "endTime", "name": "endTime", "autoWidth": true },
            {
                "data": "creator", "name": "EndDate", "render": function (data, type, full, meta) {
                    if (full["EndDate"] != "" || full["EndDate"] != null) {
                        return full.EndDate + ' ' + full.endTime;
                    }
                    else {
                        return ' ';
                    }
                }
            },
            //{ "data": "startLat", "name": "endstartLatTime", "autoWidth": true },
            //{ "data": "startLong", "name": "startLong", "autoWidth": true },
            //{ "data": "endLat", "name": "endLat", "autoWidth": true },
            //{ "data": "endLong", "name": "endLong", "autoWidth": true },

            {
                "data": "EmployeeType", "name": "EmployeeType", "render": function (data, type, full, meta) {
                    if (full["EmployeeType"] == 'A') {
                        return 'Admin';
                    }
                    else if (full["EmployeeType"] == 'SA') {
                        return 'Supervisor';
                    }
                    else if (full["EmployeeType"] == 'SUA') {
                        return 'Super Admin';
                    }
                    else {
                        return 'Not Available';
                    }
                }
            },

            {
                "data": "isActive", "name": "isActive",  "render": function (data, type, full, meta) {
                    if (full["isActive"] == '1') {
                        return 'Active';
                    }

                    if (full["isActive"] == '0') {
                        return 'Non Active';
                    }

                    else {
                        return 'Not Available';
                    }
                }
            },
            {
                "data": "type", "name": "type","render": function (data, type, full, meta) {
                    if (full["logindevice"] == '' || full["logindevice"] == null) {
                        return 'Mobile';
                    }

                    if (full["logindevice"] == 'PC') {
                        return 'PC';
                    }

                    else {
                        return 'Not Available';
                    }
                }
            },
            { "render": function (data, type, full, meta) { return '<a  data-toggle="modal" class="tooltip1" style="cursor:pointer" onclick="user_route(' + full["daID"] + ')" ><i class="material-icons location-icon">location_on</i><span class="tooltiptext1">Route</span> </a>'; }, "width": "10%" },

        ],

    });

}

function user_route(id) {
    window.location.href = "/HouseScanifyEmp/HSUREMPRoute?daId=" + id;
};
//////////////////////////////////////////////////////////////////////////////
function showInventoriesGrid() {
    // Search();
}

function Edit(Id) {
    window.location.href = "/HouseScanifyEmp/AddHSUREmployeeDetails?teamId=" + Id;

};
function AppList(Id) {
    // alert(Id);
    if (Id != null) {
        var url = "/HouseScanifyEmp/HSUserList?AppId=" + Id;
        window.location.href = url;
    }
};





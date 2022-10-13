

        $('form').each(function () { this.reset() });
        $('#houseAddress').change(function () {
            var Addr = $("#houseAddress").val();
            numberOfLineBreaks = (Addr.match(/\n/g) || []).length;

            var regex = /<br\s*[\/]?>/gi;
            Addr = Addr.replace(regex, "\n");
            Addr = Addr.replace(/[^ -~]/g, '');
            Addr = Addr.replace('<br>', '');
            Addr = Addr.replace('</br>', '');
            Addr = Addr.replace('&nbsp;', '');
            Addr = Addr.replace('\"', '');
            Addr = Addr.replace('\r\n', '');
            Addr = Addr.replace('\n', '');
            Addr = Addr.replace('\r', '');
            Addr = Addr.replace(/^(\r\n)|(\n)/, '');
            numberOfLineBreaks = (Addr.match(/\n/g) || []).length;
            if ($.trim(Addr) != '') {
                $.ajax({
                    url: "/HouseMaster/Address?location=" + Addr,
                    method: "POST",
                    datatype: "json",
                    success: function (data) {
                        // alert(data);
                        var obj = jQuery.parseJSON(data);
                        var lat = obj.CLIENT_LAT;
                        var log = obj.CLIENT_LOG;
                        //alert(obj.CLIENT_LAT + ' ' + obj.CLIENT_LOG);
                        document.getElementById('houseLat').value = obj.houseLat;
                        document.getElementById('houseLong').value = obj.houseLong;
                    },
                    error: function () {
                        document.getElementById('houseAddress').value = "";
                        swal('Please provide proper address with city.');
                    }
                });
            } else {
                swal('Please type address to get latitude and longitude.');
            }

        });



//********************************

    function PopArea(cel) {

        $('#myModal').modal('toggle');
        //window.location.href = "MainMaster/AddAreaDetails";
        //   $('#searchResults').load('/HouseMaster/AreaMaster');

    }

            
function PopWard(cel) {

    $('#wardModal').modal('toggle');
    //window.location.href = "MainMaster/AddAreaDetails";
    //   $('#searchResults').load('/HouseMaster/AreaMaster');

}
//********************************



$('#trash_badge').click(function () {
    $("#FileUpload_Preview").html("");
});
$('#filesUpload').change(function () {
    $("#FileUpload_Preview").html("");
    $("#FileUpload_Preview").show();
    $("#FileUpload_Preview").append("<img />");

    if (typeof (FileReader) != "undefined") {
        var reader = new FileReader();
        reader.onload = function (e) {
            //-----------------------------------
            // Preview image
            //-----------------------------------
            $("#FileUpload_Preview img").attr("src", e.target.result);
            $("#FileUpload_Preview img").attr("width", "auto");
            $("#FileUpload_Preview img").attr("height", "100%");
        }
        reader.readAsDataURL($(this)[0].files[0]);
    }
});
//********************************

$(document).ready(function () {
    var id = $('#houseId').val();
    if (id > 0) {
        $('#buttonName').text("Save Changes");
        $('.title_change').text("घर तपशील सादर बदला / Edit House Details");
    } else {
        $('#buttonName').text("Save");
        $('.title_change').text("घर तपशील सादर करा / Add House Details");
    }

        area();
        ward();   
       
    });
 $("#houseMobile").keypress(function (e) {
            //if the letter is not digit then display error and don't type anything
            if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57) && e.which != 44) {
                //display error message
                $("#err_phone").html("केवल अंक | Digits Only").show().delay(1500).show().fadeOut('slow');
                return false;
            }
        });
$("#btnSubmit").click(function () {
             
    var bool = validation();
               
    if (bool == false) {
        return false;
    } else {
        alert($("#FileUpload_Preview img").attr('src'));
        $("#houseOwner").change(function () {
            //$("#FileUpload_Preview img").attr('src') === "/Images/QRcode.png";
            $("#FileUpload_Preview img").attr("src", "/Images/QRcode.png");
            $("input #filesUpload").attr("src", "/Images/QRcode.png");
            $('#Download1').hide();
            return false;
        });

        $("#houseNumber").change(function () {
            //$("#FileUpload_Preview img").attr('src') === "/Images/QRcode.png";
            $("#FileUpload_Preview img").attr("src", "/Images/QRcode.png");
            $("input #filesUpload").attr("src", "/Images/QRcode.png");
            $('#Download1').hide();
            return false;
        });
        if ($("#FileUpload_Preview img").attr('src') == "/Images/QRcode.png") {
            $("#err_generate").html("स्कॅनिफाय कोड व्युत्पन्न करा नंतर सेव करा / Generate Scaniffy code then save").delay(1500).show().fadeOut('slow');
            return false;
        }

        return true;
    }
    //if ($('#QRCOdeGenrate').css('display') === 'none') {
    //    //alert($(".add-image").attr("src"));"Save Changes"
    //    if ($(".add-image").attr("src") == "/Images/QRcode.png" && $('#buttonName').text() == "Save") {
    //        $('#QRCOdeGenrate').css("display", "block");
    //        $("#err_generate").html("स्कॅनिफाय कोड व्युत्पन्न करा नंतर सेव करा / Generate Scaniffy code then save").show();
    //        $('#btnSubmit').prop('disabled', true);
    //        return false;
    //    }
    //    else if ($(".add-image").attr("src") == "/Images/default_not_upload.png"
    //        && $('#buttonName').text() == "Save Changes") {
    //        $('#QRCOdeGenrate').css("display", "block");
    //        $("#err_generate").html("स्कॅनिफाय कोड व्युत्पन्न करा नंतर सेव करा / Generate Scaniffy code then save").show();
    //        $('#btnSubmit').prop('disabled', true);
    //        return false;
    //    } else { 
               
});
function validation() {

    if ($.trim($("#houseOwner").val()) == '') {
        $("#err_name").html("नाव आवश्यक आहे / Name is Required").delay(1500).show().fadeOut('slow');
        $('#houseOwner').focus();
        return false;
    }
    if ($.trim($("#houseOwnerMar").val()) == '') {
        $("#err_name_mar").html("नाव मराठी आवश्यक आहे / Name Marathi is Required").delay(1500).show().fadeOut('slow');
        $('#houseOwnerMar').focus();
        return false;
    }
    if ($.trim($("#houseNumber").val()) == '') {
        $("#err_house_num").html("घर क्रमांक आवश्यक आहे / House number is Required").delay(1500).show().fadeOut('slow');
        $('#houseNumber').focus();
        return false;
    }

    if ($.trim($("#selectArea").val()) == "-1" || $.trim($("#selectArea").val()) == "0") {
        $("#err_area").html("क्षेत्र आवश्यक आहे / Area is Required").delay(1500).show().fadeOut('slow');
        $('#selectArea').focus();
        return false;
    }
    if ($.trim($("#selectWard").val()) == "-1" || $.trim($("#selectWard").val()) == "0") {
        $("#err_ward").html("वार्ड क्रमांक आवश्यक आहे / Ward No. is Required").delay(1500).show().fadeOut('slow');
        $('#selectWard').focus();
        return false;
    }
    if ($.trim($("#houseAddress").val()) == '') {
        $("#err_address").html("पत्ता आवश्यक आहे / Address is Required").delay(1500).show().fadeOut('slow');
        $('#houseAddress').focus();
        return false;
    }
    if ($.trim($("#Vehican_No").val()) == '') {
        $("#err_vnumber").html("Vehican_No is Required").delay(1500).show().fadeOut('slow');
        $('#Vehican_No').focus();
        return false;
    }
    
    //$("#AreaId").val() == $("#selectArea").val()
    // $("#AreaId").val() == $("#selectArea").val()
    alert($("#selectArea").val());
    alert($("#selectWard").val());
    $('#AreaId').val($("#selectArea").val());
    $('#WardNo').val($("#selectWard").val());
       
    return true;
              
}
$("#QRCOdeGenrate").click(function () {

    var bool = validation();
               
    if (bool == true) {
        $("#err_generate").html("स्कॅनिफाय कोड व्युत्पन्न करा नंतर सेव करा / Generate Scaniffy code then save").delay(1).show().fadeOut('slow');
        $('#prog_bar').css("display", "block");
        $("#prog_bar").delay(3000).show().fadeOut('slow');

        //genrate QRCode image 
        var a = $('#n').val();
        if (a != null && a != "") {
            var src_qr = 'https://api.qrserver.com/v1/create-qr-code/?data=' + a + '.jpg'
            $("#FileUpload_Preview img").attr("src", src_qr);
            $("input #filesUpload").attr("src", src_qr);
            //alert($("#FileUpload_Preview img").attr('src'));
                 

            // cpde end
            $('#btnSubmit').prop('disabled', false);
            //   $('#QRCOdeGenrate').css('display', 'none');
            $('#Download1').show();
            move();
        }
    }
});

function move() {
    var elem = document.getElementById("myBar");
    var width = 40;
    var id = setInterval(frame, 10);
    function frame() {
        if (width >= 100) {
            clearInterval(id);
        } else {
            width++;
            elem.style.width = width + '%';
            elem.innerHTML = width * 1 + '%';
        }
    }
} 
            
//$(".add_popup_clear").click(function () {
//    $('#myModal').find('input:text').val('');
//    $('#wardModal').find('input:text').val('');
//});
$("#btnPost").click(function () {
    if ($.trim($("#txtName").val()) == '') {
        $("#err_name_pop").html("नाव आवश्यक आहे / Name is Required").delay(1500).show().fadeOut('slow');
        $('#txtName').focus();
        return false;
    }
    if ($.trim($("#txtDesignation").val()) == '') {
        $("#err_name_mar_pop").html("नाव मराठी आवश्यक आहे / Name Marathi is Required").delay(1500).show().fadeOut('slow');
        $('#txtDesignation').focus();
        return false;
    }
    var employee = new Object();
    employee.Name = $('#txtName').val();
    employee.NameMar = $('#txtDesignation').val();
    if (employee != null) {
        $.ajax({
            type: "POST",
            url: "/HouseMaster/AreaMaster",
            data: JSON.stringify(employee),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {                         
                var UserId = $('#selectArea').val();
                $.ajax({
                    type: "post",
                    url: "/HouseMaster/AreaList",
                    data: { userId: UserId },
                    datatype: "json",
                    traditional: true,
                    success: function (data) {
                        var district;
                        for (var i = 0; i < data.length; i++) {
                            district = district + '<option value=' + data[i].Value + '>' + data[i].Text + '</option>';
                        }
                        //district = district + '</select>';
                        $('#selectArea').html(district);
                        $('#myModal').modal('hide');
                    }
                });
                           
            }
        });
    }
});



$("#btnWard").click(function () {
    if ($.trim($("#txtWard").val()) == '') {
        $("#err_ward_pop").html(" वार्ड क्रमांक आवश्यक आहे / Ward number is Required ").delay(1500).show().fadeOut('slow');
        $('#txtName').focus();
        return false;
    }
             
    var employee = new Object();
    employee.WardNo = $('#txtWard').val();
              
    if (employee != null) {
        $.ajax({
            type: "POST",
            url: "/HouseMaster/WardMaster",
            data: JSON.stringify(employee),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {                           
                var UserId = $('#selectWard').val();
                $.ajax({
                    type: "post",
                    url: "/HouseMaster/WardList",
                    data: { userId: UserId },
                    datatype: "json",
                    traditional: true,
                    success: function (data) {
                        var district;
                        for (var i = 0; i < data.length; i++) {                                     
                            if (data[i].Value == $('#WardNo').val()) {
                                district = district + '<option value=' + data[i].Value + ' selected>' + data[i].Text + '</option>';
                            } else { district = district + '<option value=' + data[i].Value + '>' + data[i].Text + '</option>'; }
                        }
                        //district = district + '</select>';
                        $('#selectWard').html(district);
                        $('#wardModal').modal('hide');
                    }
                });

            }
        });
    }
});
//********************************



    $('#Download1').click(function () {
        if ($.trim($("#houseOwner").val()) != '' && $.trim($("#houseNumber").val()) != '') {

            var name = $("#houseOwner").val();
            var Refid = $("#n").val();
            var Number = $("#houseNumber").val();
            window.location.href = "/HouseMaster/GenratePDF?name=" + name + "&number=" + Number + "&ReferanceId=" + Refid;
        } else {
            $("#err_name").html("नाव आवश्यक आहे / Name is Required").delay(1500).show().fadeOut('slow');
            $("#err_house_num").html("घर क्रमांक आवश्यक आहे / House number is Required").delay(1500).show().fadeOut('slow');
            $('#houseOwner').focus();
        }
    })



function area() {

    var UserId = $('#selectArea').val();
    $.ajax({
        type: "post",
        url: "/HouseMaster/AreaList",
        data: { userId: UserId },
        datatype: "json",
        traditional: true,
        success: function (data) {
            var district;
            for (var i = 0; i < data.length; i++) {

                if (data[i].Value == $('#AreaId').val()) {
                    district = district + '<option value=' + data[i].Value + ' selected>' + data[i].Text + '</option>';
                } else { district = district + '<option value=' + data[i].Value + '>' + data[i].Text + '</option>'; }

            }
            //district = district + '</select>';
            $('#selectArea').html(district);
        }
    });

}

function ward() {

    var UserId = $('#selectWard').val();
    $.ajax({
        type: "post",
        url: "/HouseMaster/WardList",
        data: { userId: UserId },
        datatype: "json",
        traditional: true,
        success: function (data) {
            var district;
            for (var i = 0; i < data.length; i++) {

                if (data[i].Value == $('#WardNo').val()) {
                    district = district + '<option value=' + data[i].Value + ' selected>' + data[i].Text + '</option>';
                } else { district = district + '<option value=' + data[i].Value + '>' + data[i].Text + '</option>'; }
            }
            //district = district + '</select>';
            $('#selectWard').html(district);
        }
    });


}

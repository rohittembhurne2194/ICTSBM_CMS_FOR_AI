
function callAjax() {
    $.ajax({
        type: 'POST',
        url: "/Log/Tracking",
        data: $(this).serialize(),
        dataType: 'json',
        //traditional: true,
        success: function (data) {
            //$('#hidden').val(data);// first set the value 

            for (var i = 0; i < data.length; i++) {

                
                if ($('Label[name=' + data[i].gcId + ']').length) {
                    //alert('Label exists');
                }
                else {

                    $('#txt_track').append('<Label name=' + data[i].gcId + '>&nbsp;&nbsp;' + data[i].Logstring + '</Label><br>');
                    $('#txt_track').animate({ scrollTop: $('#txt_track').prop("scrollHeight") });
                }
             }
            
        }
    });
}

setInterval(function () {
    callAjax()

}, 1000);
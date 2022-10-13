function ValidateSize(file) {
        var FileSize = file.files[0].size / 1024 / 1024; // in MB
        if (FileSize > 2) {
            alert('File size should be less than 2 MB');
            location.reload();
             $(file).val(''); //for clearing with Jquery
        } else {

        }
}

$(document).ready(function () {
    $('input').attr('autocomplete', 'off');

});
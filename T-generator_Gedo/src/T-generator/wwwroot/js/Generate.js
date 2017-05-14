// Write your Javascript code.
$(document).ready(function () {
    $("#AmazonAccountID").change(function () {

        var Selected = $(this)[0].value;
        var url = "/Generate/GetProducts/" + Selected;

        $.ajax({
            url: url,
            success: function (data) {
                $('#Products').find('option').remove();
                $.each(data, function(key, val) {
                    $('#Products').append('<option value="' + val + '">' + key + '</option>');
                });
                }
        });
        });
    });

$(function() {
    $('input[type=submit]').click(function() {
        if ($('#AvailableBranch').val() == null) {
            $('#error').html('Please select branch to add.');
            $("#error").addClass("error ui-state-error ui-corner-all");
            return false;
        }
        $('#error').html('');
        $("#error").removeClass("error ui-state-error ui-corner-all");        
    });
});
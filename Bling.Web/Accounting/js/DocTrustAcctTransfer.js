$(function() {
    $(':submit').click(function() {

        var msg = "";
        $('input[type=text]').each(function() {
            if ($(this).val() == "") {
                msg = msg + $(this).prev().html() + ' should not be empty.<br />';
            }
        });

        if (msg != "") {
            $('#message').html("");
            $("#message").removeClass("message ui-state-highlight ui-corner-all");
            $('#error').html(msg);
            $("#error").addClass("error ui-state-error ui-corner-all");
            return false;
        } else {
            $('#error').html("");
            $("#error").removeClass("error ui-state-error ui-corner-all");
        }
    });
});

$(function() {
    $('input[type=text]').each(function() {
        $(this).datepicker();
    });
});

$(function() {
    $('table').addClass('t1');
    $('table tr:first').addClass('yellow');
});
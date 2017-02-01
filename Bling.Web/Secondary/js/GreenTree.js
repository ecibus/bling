$(document).ready(function () {
    $('#txtStart').datepicker();
    $('#txtEnd').datepicker();
    $('#btnCreate').button();

    $('#btnCreate').bind('click', function () {
        $('*').css('cursor', 'wait');
        $.ajax({
            type: 'POST',
            url: '../handlers/greentree.ashx',
            data: {'start': $('#txtStart').val(), 'end': $('#txtEnd').val()},
            success: function (data) {
                $('*').css('cursor', 'default');
                $('#dvResults').html(data);
            },
            error: function (data) {
                $('*').css('cursor', 'default');
            }
        });
    });
});
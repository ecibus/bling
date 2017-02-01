$(document).ready(function () {
    $('#txtPstart').datepicker();
    $('#txtPend').datepicker();
    $('#btnCreate').button();

    $('#btnCreate').bind('click', function () {
        $('*').css('cursor', 'wait');
        $.ajax({
            type: 'POST',
            url: '../handlers/create_csv.ashx',
            data: {'start': $('#txtPstart').val(), 'end': $('#txtPend').val(), 'selectBy': $('#selOptions :selected').val()},
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
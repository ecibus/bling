$(function () {
    $('#btnValidate').live('click', function () {
        //console.log('Validate');

        $.post('AjaxValidateTimeCard.aspx',
                {
                    type: 'validate',
                    start: $('#txtStart').val(),
                    end: $('#txtEnd').val(),
                    r: Math.floor(Math.random() * 1001)
                },
                ViewReportCallback
            );

    });
});

function ViewReportCallback(o) {
    $('#list').html(o);
    $.post('AjaxValidateTimeCard.aspx',
                {
                    type: 'validate1',
                    start: $('#txtStart').val(),
                    end: $('#txtEnd').val(),
                    r: Math.floor(Math.random() * 1001)
                },
                ViewReportCallback2
            );
}

function ViewReportCallback2(o) {
    $('#list').append(o);
}
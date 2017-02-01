$(function() {
    $('#txtFrom').datepicker();
    $('#txtTo').datepicker();
});


$(function() {
    $('#btnUpdate').click(function() {
                
        $.post('AjaxCensusDateRange.aspx',
        {
            type: 'update',
            from: $('#txtFrom').val(),
            to: $('#txtTo').val()
        },
        function(data, result) {
            if (data != "") {
                new DisplayMessage(data).AsWarning();
            } else {
                new DisplayMessage('Done updating.').AsInfo();
            }
        }
        );

    });


});
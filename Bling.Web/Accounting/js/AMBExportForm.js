
$(function () {
    $('input[type=text]').each(function () {
        $(this).datepicker();
    });
});


$(function () {
    $('#btnGenerate').bind('click', function () {
        //btnGenerate = $(this).busy();
        GenerateCSV();
    });
});


function GenerateCSV() {
    var from = $('#txtFundedFrom').val();
    var to = $('#txtFundedTo').val();

    //$('#xmlLink').html('');
    $.post('AjaxAMBExportForm.aspx',
            {
                type: 'generate',
                fundedFrom: from,
                fundedTo: to,
                r: Math.floor(Math.random() * 1001)
            },
        GenerateCallback
    );
}

function GenerateCallback(o) {
    $('#csvLink').html(o);
}
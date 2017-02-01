
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
    var from = $('#txtFrom').val();
    var to = $('#txtTo').val();
    var reportType = $('#optReportType').val();

    //$('#xmlLink').html('');
    $.post('AjaxAMBImportForm.aspx',
            {
                type: 'generate',
                reportType: reportType,
                from: from,
                to: to,
                includeByte: $('#chkIncludeByteLoans').is(':checked'),
                r: Math.floor(Math.random() * 1001)
            },
        GenerateCallback
    );
}

function GenerateCallback(o) {

    $('#csvLink').html(o)
    .effect("pulsate", { times: 1 });
}

$(function () {
    $('input[class=date]').each(function () {
        $(this).datepicker();
    });
});


$(function () {
    $('#btnGenerate').bind('click', function () {
        GenerateCSV();
    });
});


function GenerateCSV() {
    var from = $('#txtFrom').val();
    var to = $('#txtTo').val();
    var includeByte = $('#chkIncludeByteLoans').is(':checked')
    var includeDataTrac = $('#chkIncludeDataTracLoans').is(':checked')
    var loans = $('#txtLoans').val();
    var dateType = $('#dateType').val();

    $.post('AjaxQCExportForm.aspx',
            {
                type: 'generate',
                from: from,
                to: to,
                includeByte: includeByte,
                includeDataTrac: includeDataTrac,
                loans: loans,
                dateType: dateType,
                r: Math.floor(Math.random() * 1001)
            },
        GenerateCallback
    );
}

function GenerateCallback(o) {

    $('#csvLink').html(o)
    .effect("pulsate", { times: 1 });
}
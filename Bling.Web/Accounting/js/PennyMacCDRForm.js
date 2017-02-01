$(function () {
    $('#btnGenerateCDR').bind('click', function () {
        GenerateCSV();
    });
});

$(function () {
    $('#btnGenerateEPP').bind('click', function () {
        GenerateEPP();
    });
});

function GenerateCSV() {
    var loans = $('#txtLoans').val();

    $.post('AjaxPennyMacCDRForm.aspx',
            {
                type: 'generate',
                loans: loans,
                csvtype: 'cdr',
                targetFile: 'PennyMacCDR.csv',
                r: Math.floor(Math.random() * 1001)
            },
        GenerateCallback
    );
}

function GenerateEPP() {
    var loans = $('#txtLoans').val();

    $.post('AjaxPennyMacCDRForm.aspx',
            {
                type: 'generate',
                loans: loans,
                csvtype: 'epp',
                targetFile: 'PennyMacEPP.csv',
                r: Math.floor(Math.random() * 1001)
            },
        GenerateCallback
    );
}

function GenerateCallback(o) {

    $('#csvLink').html(o)
    .effect("pulsate", { times: 1 });
}

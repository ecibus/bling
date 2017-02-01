

$(function () {
    $('input[type=text]').each(function () {
        $(this).datepicker();
    });
});


$(function () {
    $('#btnGenerate').bind('click', function () {
        GenerateCSV2();
    });
});

function GenerateCSV2() {
    var fromSettleDate = $('#txtFromSettleDate').val();
    var toSettleDate = $('#txtToSettleDate').val();
    var fromPairOffDate = $('#txtFromPairOffDate').val();
    var toPairOffDate = $('#txtToPairOffDate').val();
    var status = $('#optStatus').val();
    var sortBy = $('#optSortBy').val();

    $.post('AjaxTradeForm.aspx',
            {
                type: 'generate2',
                fromSettleDate: fromSettleDate,
                toSettleDate: toSettleDate,
                fromPairOffDate: fromPairOffDate,
                toPairOffDate: toPairOffDate,
                status: status,
                sortBy: sortBy,
                r: Math.floor(Math.random() * 1001)
            },
        GenerateCallback
    );
}

function GenerateCSV() {
    var from = $('#txtFrom').val();
    var to = $('#txtTo').val();
    var reportType = $('#optReportType').val();
    var dateForRange = $('#optDateForRange').val();
    var sortBy = $('#optSortBy').val();

    $.post('AjaxTradeForm.aspx',
            {
                type: 'generate',
                reportType: reportType,
                from: from,
                to: to,
                dateForRange: dateForRange,
                sortBy: sortBy,
                r: Math.floor(Math.random() * 1001)
            },
        GenerateCallback
    );
}

function GenerateCallback(o) {

    $('#csvLink').html(o)
    .effect("pulsate", { times: 1 });
}
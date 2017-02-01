
$(function () {
    $('input[type=text]').each(function () {
        $(this).datepicker();
    });
    jQuery().busy("preload");
});

var btnViewReport;

$(function () {
    $('#btnViewReport').bind('click', function () {
        btnViewReport = $(this).busy();
        ViewReport();
    });
});


$(function () {
    $('#btnExportReport').bind('click', function () {
        btnViewReport = $(this).busy();
        ExportToCSV();
    });
});

function ViewReport() {
    var from = $('#txtFrom').val();
    var to = $('#txtTo').val();
    var reportType = $('#optReportType').val();
    $.post('AjaxAMBReport.aspx',
            {
                type: 'viewreport',
                reportType: reportType,
                from: from,
                to: to,
                r: Math.floor(Math.random() * 1001)
            },
        ViewReportCallback
    );
}

function ViewReportCallback(o) {

    //alert(o.length);
    //alert("[" + o + "]");
    btnViewReport.busy('hide');
    if (o.length > 2) {
        new DisplayMessage(o).AsInfo();
        return;
    }

    window.open('Report/AppraisalBalance.pdf?r=' + Math.floor(Math.random() * 1001), 'Report');

}

function ExportToCSV() {
    var from = $('#txtFrom').val();
    var to = $('#txtTo').val();
    var reportType = $('#optReportType').val();

    $.post('AjaxAMBReport.aspx',
            {
                type: 'exportreport',
                reportType: reportType,
                from: from,
                to: to,
                r: Math.floor(Math.random() * 1001)
            },
        ExportToCSVCallback
    );
}

function ExportToCSVCallback(o) {
    btnViewReport.busy('hide');
    $('#csvLink').html(o)
    .effect("pulsate", { times: 1 });
}
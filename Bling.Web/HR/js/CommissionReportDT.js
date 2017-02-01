
$(function () {
    $('#txtFundedFrom').datepicker();
    $('#txtFundedTo').datepicker();
    jQuery().busy("preload");
});

var btnViewReport;
var guid;

$(function () {
    $('#btnViewReport').bind('click', function () {
        new DisplayMessage("").Clear();
        var report = $('#optReport').val();
        var valid = false;
        guid = createUUID();

        if (report == null) {
            new DisplayMessage("Please choose the report you want to view.").AsInfo();
            return;
        }

        switch (report) {
            case "crlo":
                valid = ValidateCommissionReportByLO();
                break;
            case "crbr":
                valid = ValidateCommissionReportByBranch();
                break;
        }

        if (valid) {
            btnViewReport = $(this).busy();
            $.post('AjaxCommissionReport.aspx',
            {
                type: 'viewdt',
                report: report,
                start: $('#txtFundedFrom').val(),
                end: $('#txtFundedTo').val(),
                lo: $('#LO').val(),
                branch: $('#txtBranchNo').val(),
                guid: guid,
                reporttype: report == "crlo" ? "1" : "2",
                r: Math.floor(Math.random() * 1001)
            },
            ViewReportCallback
        );
        }
    });
});

function ViewReportCallback() {
    btnViewReport.busy('hide');
    window.open('Report/' + $('#optReport').val() + '-' + guid + '.pdf?r=' + Math.floor(Math.random() * 1001), 'Report');

}

function ValidateCommissionReportByLO() {
    if (!ValidLO()) {
        return false;
    }
    return true;
}

function ValidateCommissionReportByBranch() {
    if (!ValidBranch()) {
        return false;
    }
    return true;
}

function ValidLO() {
    if ($('#LO').val() == '') {
        new DisplayMessage("Loan Officer is required.").AsInfo();
        return false;
    }
    return true;
}

function ValidBranch() {
    if ($('#txtBranchNo').val() == '') {
        new DisplayMessage("Branch is required.").AsInfo();
        return false;
    }
    return true;
}

function createUUID() {
    // http://www.ietf.org/rfc/rfc4122.txt
    var s = [];
    var hexDigits = "0123456789abcdef";
    for (var i = 0; i < 36; i++) {
        s[i] = hexDigits.substr(Math.floor(Math.random() * 0x10), 1);
    }
    s[14] = "4";  // bits 12-15 of the time_hi_and_version field to 0010
    s[19] = hexDigits.substr((s[19] & 0x3) | 0x8, 1);  // bits 6-7 of the clock_seq_hi_and_reserved to 01
    s[8] = s[13] = s[18] = s[23] = "-";

    var uuid = s.join("");
    return uuid;
}

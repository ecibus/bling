$(function () {
    $('#txtPayDate').datepicker();
});


$(function () {
    $('#btnLoad').bind('click', function () {
        LoadLoan($('#txtLoanNumber').val());
    });
});

$(function () {
    Clear();

    if ($('#txtLoanNumber').val() != "") {
        $('#btnLoad').click();
    }
});


$(function () {
    $('#btnClear').bind('click', function () {
        Clear();
    });
});

$(function () {
    $('#btnSave').bind('click', function () {
        Save();
    });
});

function Save() {
    var status = $('#Status').val();
    var comment = $('#Comment').val();
    var loanNumber = $('#LoanNumber').html();
    var payDate = $('#txtPayDate').val();
    //alert(status);
    
   
    
    if (status == '') {
        new DisplayMessage("Status is Required.").AsWarning();
        return;
    }

    var loginName = $('#LoanOfficer').attr('LoginName');

    $.post('AjaxCommissionAnalysis.aspx',
        {
            type: 'savecommissionanalysis',
            LoanNumber: loanNumber,
            Status: status,
            ApprovedLO: loginName,
            Comment: comment,
            Paydate: payDate,
            r: Math.floor(Math.random() * 1001)
        },
        SaveCallback,
        "json"
    );
}

function SaveCallback(o) {
    if (o.Message) {
        new DisplayMessage(o.Message).AsWarning();
        return;
    }

    var loanNumber = $('#LoanNumber').html();
    LoadLoan(loanNumber); 
}

function LoadLoan(loanNumber) {

    if (loanNumber == "") {
        new DisplayMessage("Loan Number is Required.").AsWarning();
        return;
    }

    $.post('AjaxCommissionAnalysis.aspx',
        {
            type: 'loadloan',
            LoanNumber: loanNumber,
            r: Math.floor(Math.random() * 1001)
        },
        LoadLoanCallback,
        "json"
    );
}

function Clear() {
    new DisplayMessage("").Clear();
    $('#LoanNumber').html('');
    $('#BorrowerName').html('');
    $('#LoanOfficer').html('');
    $('#ApplicationDate').html('');
    $('#FundedDate').html('');
    $('#LoanAmount').html('');
    $('#txtPayDate').val('');
    $('#Status').val('');
    $('#Comment').val('');
    $('#ApprovedLO').html('');
    $('#ReleasedDate').html('');
    $('#HoldDate').html('');
    $('#BrokeredLoan').html('');
    $('#Commission').html('');

    $('#LoanOfficer').attr('LoginName', '');

    $('#Status').attr('disabled', true);
    $('#Comment').attr('disabled', true);
    $('#txtPayDate').attr('disabled', true);
    $('#btnSave').attr('disabled', true);
}

function LoadLoanCallback(o) {
    Clear();

    if (o.Message) {
        new DisplayMessage(o.Message).AsWarning();
        return;
    }

    $('#LoanNumber').html(o.LoanNumber);
    $('#BorrowerName').html(o.Borrower);
    $('#LoanOfficer').html(o.LoanOfficer);
    $('#ApplicationDate').html(o.ApplicationDate);
    $('#FundedDate').html(o.FundedDate);
    $('#LoanAmount').html(o.LoanAmount);
    $('#txtPayDate').val(o.CommissionStatusDate);
    $('#Status').val(o.CommissionStatus);
    $('#Comment').val(o.Comment);
    $('#ApprovedLO').html(o.ApprovedLO);
    $('#ReleasedDate').html(o.ReleasedDate);
    $('#HoldDate').html(o.HoldDate);
    $('#BrokeredLoan').html(o.BrokeredLoan);
    $('#Commission').html(o.Commission);

    $('#LoanOfficer').attr('LoginName', o.LoginName);

    $('#Status').attr('disabled', false);
    $('#Comment').attr('disabled', false);
    $('#txtPayDate').attr('disabled', false);
    $('#btnSave').attr('disabled', false);
}
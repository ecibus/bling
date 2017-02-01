
function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    var regexS = "[\\?&]" + name + "=([^&#]*)";
    var regex = new RegExp(regexS);
    var results = regex.exec(window.location.href);
    if (results == null)
        return "";
    else
        return decodeURIComponent(results[1].replace(/\+/g, " "));
}


$(function () {

    $('#btnChangesAccepted').hide();

    $('#btnClear').bind('click', function (e) {
        e.preventDefault();
        Clear();

        $('#txtLoanNumber').val('');
    });
});


$(function () {
    $('#btnLoad').bind('click', function () {
        var loanNumber = $('#txtLoanNumber').val();
        Clear();
        if (loanNumber == '') {
            //new DisplayMessage('Loan Number is required.').AsInfo();
            return;
        }
        $.post(
            'AjaxLPE.aspx',
            { type: 'loadloan', LoanNumber: loanNumber },
            LoadLoanCallback,
            "json"
        );
    });
});

function LoadLoanCallback(o) {

    if (o.Message) {
        //new DisplayMessage(o.Message).AsInfo();
        alert(o.Message);
        return;
    }

    $('#btnChangesAccepted').show();

    //alert(o.GEMLoanFeeCharged);
    $('#LoanNumber').html(o.LoanNumber);
    $('#LinkedLoan').html(o.LinkedLoan);
    $('#Borrower').html(o.Borrower);
    $('#LoanType').html(o.LoanType);
    $('#LoanAmount').html(o.LoanAmount).removeClass('red');
    $('#GEMLoanFeeCharged').html(o.GEMLoanFeeCharged).removeClass('red');
    $('#LoanOriginationFeeCharged').html(o.LoanOriginationFeeCharged).removeClass('red');
    $('#LoanOfficerPrice').html(o.LoanOfficerPrice).removeClass('red');
    $('#BorrowerPaidDiscount').html(o.BorrowerPaidDiscount).removeClass('red');
    $('#LenderCredit').html(o.LenderCredit).removeClass('red');
    $('#FICOScore').html(o.FICOScore);
    $('#ApplicationDate').html(o.ApplicationDate);
    $('#LockedDate').html(o.LockedDate);
    $('#NoOfBorrower').html(o.NoOfBorrower);
    $('#ProgramType').html(o.ProgramType);
    $('#TransactionType').html(o.TransactionType);
    $('#FinalNetPricePoint').html(o.FinalNetPricePoint);
    $('#EvaluatorMessage').html(o.EvaluatorMessage);
    $('#ReviewComplete').html(o.ReviewComplete);
    if (o.Reason) {
        $('#Reason').html(o.Reason);
    }

    CheckForChanges()
}

function Clear() {
    new DisplayMessage("").Clear();
    $('#btnChangesAccepted').hide();
    $('#LoanNumber').html('&nbsp;');
    $('#LinkedLoan').html('&nbsp;');
    $('#Borrower').html('&nbsp;');
    $('#LoanType').html('&nbsp;');
    $('#LoanAmount').html('&nbsp;');
    $('#GEMLoanFeeCharged').html('&nbsp;');
    $('#LoanOriginationFeeCharged').html('&nbsp;');
    $('#LoanOfficerPrice').html('&nbsp;');
    $('#BorrowerPaidDiscount').html('&nbsp;');
    $('#LenderCredit').html('&nbsp;');
    $('#FICOScore').html('&nbsp;');
    $('#ApplicationDate').html('&nbsp;');
    $('#LockedDate').html('&nbsp;');
    $('#NoOfBorrower').html('&nbsp;');
    $('#ProgramType').html('&nbsp;');
    $('#TransactionType').html('&nbsp;');
    $('#FinalNetPricePoint').html('&nbsp;');
    $('#EvaluatorMessage').html('&nbsp;');
    $('#Reason').html('&nbsp;');
    $('#ReviewComplete').html('&nbsp;');
}

$(function () {
    var loanNumber = getParameterByName('loanNumber');

    if (jQuery.trim(loanNumber) != "") {
        $('#txtLoanNumber').val(loanNumber);
        $('#btnLoad').click();
    }
});

$(function () {
    $('tr').not('.yellow').hover(
        function () {
            $(this).addClass('hover');
        },
        function () {
            $(this).removeClass('hover');
        }
    );
});

$(function () {
    $('tr').not('.yellow').bind('click', function (e) {
        //window.location.replace("LPEDetail.aspx?loanNumber=" + $(this).find('td').html());
        window.location = "LPEDetail.aspx?loanNumber=" + $(this).find('td').html();
    });
});

$(function () {
    $('#chkReadyForDocs').bind('click', function (e) {
        var loanNumber = $('#LoanNumber').html();
        var newValue = $(this).attr('checked') ? 1 : 0;
        var pos = loanNumber.indexOf(' ');
        loanNumber = loanNumber.substring(0, pos);
        $.post(
            'AjaxLPE.aspx',
            { type: 'readyfordocs', LoanNumber: loanNumber, NewValue: newValue },
            ReadyForDocsCallback,
            "json"
        );
    });
});

function ReadyForDocsCallback(o) {
    if (o.Message) {
        new DisplayMessage(o.Message).AsInfo();
        return;
    }
    $('<div class="blinkingMessage">Updating Ready for Docs.</div>')
        .insertAfter(
            $('#chkReadyForDocs')
        )
        .effect("pulsate", { times: 2 })
        .fadeIn2('slow')
        .animate({ opacity: 1.0 }, 3000)
        .fadeOut2('slow', function () {
            $(this).remove();
        });
}

$(function () {
    $('#btnSave').live('click', function (e) {
        var loanNumber = $('#LoanNumber').html();
        var reasonId = $('#ddlReason').val();
        var comment = $('#Comment').val();

        var pos = loanNumber.indexOf(' ');
        loanNumber = loanNumber.substring(0, pos);

        //alert (loanNumber + ' - ' + reasonId + ' - ' + comment )
        //return;
        $.post(
        'AjaxLPE.aspx',
        { type: 'updatereasonandcomment', LoanNumber: loanNumber, reasonId: reasonId, comment: comment },
        SaveReasonAndCommentCallback,
        "json"
    );
    });
});


function SaveReasonAndCommentCallback(o) {
    if (o.Message) {
        new DisplayMessage(o.Message).AsInfo();
        return;
    }
    $('<div class="blinkingMessage">Updating Reason and Comment.</div>')
        .insertAfter(
            $('#btnSave')
        )
        .effect("pulsate", { times: 2 })
        .fadeIn2('slow')
        .animate({ opacity: 1.0 }, 3000)
        .fadeOut2('slow', function () {
            $(this).remove();
        });
}


$(function () {
    $('#btnReviewComplete').live('click', function (e) {
        var loanNumber = $('#LoanNumber').html();

        //alert(loanNumber);
        var pos = loanNumber.indexOf(' ');
        loanNumber = loanNumber.substring(0, pos);

        $.post(
            'AjaxLPE.aspx',
            { type: 'initialreviewcomplete', LoanNumber: loanNumber },
            InititalReviewCallback,
            "json"
        );
    });
});

function InititalReviewCallback(o) {
    if (o.Message) {
        new DisplayMessage(o.Message).AsInfo();
        return;
    }
    $('#btnLoad').click();
    //window.location = "LPE.aspx";
}

$(function () {
    $('#btnChangesAccepted').bind('click', function (e) {
        var loanNumber = $('#LoanNumber').html();
        var pos = loanNumber.indexOf(' ');
        loanNumber = loanNumber.substring(0, pos);

        $.post(
            'AjaxLPE.aspx',
            {
                type: 'addhistory',
                LoanNumber: loanNumber,
                Borrower: $('#Borrower').html(),
                LoanType: $('#LoanType').html(),
                LoanAmount: $('#LoanAmount').html(),
                GEMLoanFeeCharged: $('#GEMLoanFeeCharged').html(),
                LoanOriginationFeeCharged: $('#LoanOriginationFeeCharged').html(),
                LoanOfficerPrice: $('#LoanOfficerPrice').html(),
                BorrowerPaidDiscount: $('#BorrowerPaidDiscount').html(),
                LenderCredit: $('#LenderCredit').html(),
                FICOScore: $('#FICOScore').html(),
                ApplicationDate: $('#ApplicationDate').html(),
                LockedDate: $('#LockedDate').html(),
                NoOfBorrower: $('#NoOfBorrower').html(),
                ProgramType: $('#ProgramType').html(),
                TransactionType: $('#TransactionType').html(),
                FinalNetPricePoint: $('#FinalNetPricePoint').html()
            },
            AddHistoryCallback,
            "json"
        );
    });
});

function AddHistoryCallback(o) {
    $('#btnLoad').click();
}

function CheckForChanges() {
    var loanNumber = $('#LoanNumber').html();
    var pos = loanNumber.indexOf(' ');
    loanNumber = loanNumber.substring(0, pos);

    $.post(
      'AjaxLPE.aspx',
      { type: 'getlastchanges', LoanNumber: loanNumber },
      CheckForChangesCallback,
      "json"
  );
}

function CheckForChangesCallback(o) {

    if (o.Message) {
        return;
    }

   
    MakeItRed('#LoanAmount', o.LoanAmount);
    MakeItRed('#GEMLoanFeeCharged', o.GEMLoanFeeCharged);
    MakeItRed('#LoanOriginationFeeCharged', o.LoanOriginationFeeCharged);
    MakeItRed('#LoanOfficerPrice', o.LoanOfficerPrice);
    MakeItRed('#BorrowerPaidDiscount', o.BorrowerPaidDiscount);
    MakeItRed('#LenderCredit', o.LenderCredit);

}

function MakeItRed(id, lastValue) {

    if ($(id).html() != lastValue) {
        //alert (lastValue);
        $(id).addClass('red');
    }

}
$(function () {
    $('#txtFrom').datepicker();
    $('#txtTo').datepicker();
    $('#ddLOMaster').focus();
});

function IsNumeric(input) {
    return (input - 0) == input && input.length > 0;
}

var thisButton;

$(function () {
    $('#btnViewReport').live('click', function () {
        var lo = $('#ddLOMaster').val();
        var from = $('#txtFrom').val();
        var to = $('#txtTo').val();

        if (lo == '...') {
            new DisplayMessage("Loan Officer is Required.").AsInfo();
            return false;
        }

        //alert(lo + ' ' + from + ' ' + to);

        new DisplayMessage("").Clear();

        thisButton = $(this).busy();

        $.post('AjaxLOAdjustment.aspx',
                {
                    type: 'viewreport',
                    lo: lo,
                    from: from,
                    to: to,
                    r: Math.floor(Math.random() * 1001)
                },
                ViewReportCallback
            );
        });
    });

function ViewReportCallback() {
    thisButton.busy('hide');
    window.open('Report/LOAdjustment.pdf?r=' + Math.floor(Math.random() * 1001), 'Report');
}

$(function () {
    $('#btnAdd').live('click', function () {
        if (Validated()) {

            AddAdjustment();
            
        }
    });

});

function AddAdjustment() {
    var loCode = $('#ddLOMaster').val();
    var payDate = $('#txtPayDate').val();
    var amount = $('#txtAmount').val();
    var comment = $('#txtComment').val();
    var description = $('#optDescription').val();
    var loanNumber = $('#txtLoanNumber').val();

    $.post('AjaxLOAdjustment.aspx',
        {
            type: 'addadjustment',
            loCode: loCode,
            description: description,
            loanNumber: loanNumber,
            payDate: payDate,
            amount: amount,
            comment: comment,
            r: Math.floor(Math.random() * 1001)
        },
        AddAdjustmentCallback
    );
}

function AddAdjustmentCallback() {
    ClearField();
    LoadAdjustment();
}

function ClearField() {
    $('#txtLoanNumber').val('');
    $('#txtAmount').val('');
    $('#txtComment').val('');
}

function Validated() {  
    var payDate = $('#txtPayDate').val();
    var amount = $('#txtAmount').val();

    if (payDate == '') {
        new DisplayMessage("PayDate is Required.").AsInfo();
        return false;
    }

    if (amount == '') {
        new DisplayMessage("Amount is Required.").AsInfo();
        return false;
    }

    if (!IsNumeric(amount)) {
        new DisplayMessage('Amount is not a valid number').AsInfo();
        return false;
    }

    new DisplayMessage("").Clear();
    return true;
}

$(function () {
    $('#ddLOMaster').change(function () {
        //var loName = $('#ddLOMaster').val().split('|')[1];
        //$('#lblLOName').html(loName);

        LoadAdjustment();

    });
});


function LoadAdjustment() {
    var loCode = $('#ddLOMaster').val();

    $.post('AjaxLOAdjustment.aspx',
        {
            type: 'getallbylo',
            loCode: loCode,
            r: Math.floor(Math.random() * 1001)
        },
        GetAllByLOCallback
    );
}

function GetAllByLOCallback(o) {
    $('#list').html(o);
    $('#txtPayDate').datepicker();

    $('.delete-loadjust').click(function () {
        DeleteAdjustment( $(this).attr('id'));
    });
}

function DeleteAdjustment(id) {
    
    //alert(id);


    //return;
    $.post('AjaxLOAdjustment.aspx',
        {
            type: 'deleteadjustment',
            id: id,
            r: Math.floor(Math.random() * 1001)
        },
        DeleteAdjustmentCallback
    );
}

function DeleteAdjustmentCallback() {
    LoadAdjustment();
}

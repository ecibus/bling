Number.prototype.formatMoney = function (c, d, t) {
    var n = this, c = isNaN(c = Math.abs(c)) ? 2 : c, d = d == undefined ? "," : d, t = t == undefined ? "." : t, s = n < 0 ? "-" : "", i = parseInt(n = Math.abs(+n || 0).toFixed(c)) + "", j = (j = i.length) > 3 ? j % 3 : 0;
    return s + (j ? i.substr(0, j) + t : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + t) + (c ? d + Math.abs(n - i).toFixed(c).slice(2) : "");
};


$(function () {
    $('#txtDepositDate').datepicker();
    
});

$(function () {
    $('#btnLoad').bind('click', function () {
        Clear();
        var inputdate = $('#txtDepositDate').val();
        $.post('AjaxCashDepositEntry.aspx',
            {
                type: 'load',
                inputdate: inputdate
            },
            LoadCashDepositCallback
        );
    });
});

function CreateTotal() {
    var total = 0;
    var total1003 = 0;
    var total1001 = 0;
    var total1011 = 0;
    var total1002 = 0;
    var total1008 = 0;
    var totalcc = 0;
    var counter = 0;
    var total1012 = 0;
    var total1013 = 0;
    var total1014 = 0;

    $('.dollarAmount').each(function (i, v) {
        total += parseFloat($(v).html().replace(',', ''));
        counter++;
    });

    $('.bankAccount').each(function (i, v) {
        if ($(v).html() == '1003 R General') {
            total1003 += parseFloat($(v).prev().html().replace(',', ''));
        }
    });

    $('.bankAccount').each(function (i, v) {
        if ($(v).html() == '1001') {
            total1001 += parseFloat($(v).prev().html().replace(',', ''));
        }
    });

    $('.bankAccount').each(function (i, v) {
        if ($(v).html() == '1011') {
            total1011 += parseFloat($(v).prev().html().replace(',', ''));
        }
    });

    $('.bankAccount').each(function (i, v) {
        if ($(v).html() == '1002 R Trust') {
            total1002 += parseFloat($(v).prev().html().replace(',', ''));
        }
    });

    $('.bankAccount').each(function (i, v) {
        if ($(v).html() == '1008-990 Discovery') {
            total1008 += parseFloat($(v).prev().html().replace(',', ''));
        }
    });

    $('.bankAccount').each(function (i, v) {
        if ($(v).html() == 'Credit Card') {
            totalcc += parseFloat($(v).prev().html().replace(',', ''));
        }
    });

    $('.bankAccount').each(function (i, v) {
        if ($(v).html() == '1012 Impounds') {
            total1012 += parseFloat($(v).prev().html().replace(',', ''));
        }
    });

    $('.bankAccount').each(function (i, v) {
        if ($(v).html() == '1013 MIP/PMI/VAFF') {
            total1013 += parseFloat($(v).prev().html().replace(',', ''));
        }
    });

    $('.bankAccount').each(function (i, v) {
        if ($(v).html() == '1014 Other') {
            total1014 += parseFloat($(v).prev().html().replace(',', ''));
        }
    });


    $('#count').html(counter);
    $('#Total').html(total.formatMoney(2, '.', ','));
    $('#Total1003').html(total1003.formatMoney(2, '.', ','));
    $('#Total1001').html(total1001.formatMoney(2, '.', ','));

    $('#Total1011').html(total1011.formatMoney(2, '.', ','));
    $('#Total1002').html(total1002.formatMoney(2, '.', ','));
    $('#Total1008').html(total1008.formatMoney(2, '.', ','));
    $('#TotalCreditCard').html(totalcc.formatMoney(2, '.', ','));

    $('#Total1012').html(total1012.formatMoney(2, '.', ','));
    $('#Total1013').html(total1013.formatMoney(2, '.', ','));
    $('#Total1014').html(total1014.formatMoney(2, '.', ','));


}


function LoadCashDepositCallback(o) {
    $('#cashdeposit').html(o);
    CreateTotal();
}


$(function () {
    if ($('#txtDepositDate').val() != "") {
        $('#btnLoad').click();
    }
});

$(function () {
    $('#btnAdd').bind('click', function () {
        var loanNo = $('#txtLoanNo').val();
        var branchNo = $('#txtBranchNo').val();
        var accountNo = $('#ddlCashDepositAccount').val();
        var amount = $('#txtDollarAmount').val();
        var bankAccount = $('#ddlBankAccount').val();
        var depositDate = $('#txtDepositDate').val();

        if (!Validated()) {
            return;
        }

        $.post('AjaxCashDepositEntry.aspx',
            {
                type: 'add',
                loanNo: loanNo,
                branchNo: branchNo,
                accountNo: accountNo,
                amount: amount,
                bankAccount: bankAccount,
                depositDate: depositDate
            },
            AddCashDepositCallback
        );

        //alert(loanNo + ' ' + branchNo + ' ' + accountNo + ' ' + amount + ' ' + bankAccount + ' ' + depositDate);
    });
});

function AddCashDepositCallback() {
        $('#btnLoad').click();
}


function Clear() {
    $('#txtLoanNo').val('');
    $('#txtBranchNo').val('');
    $('#ddlCashDepositAccount').val('');
    $('#txtDollarAmount').val('');
    $('#txtLoanNo').focus();
}

function IsNumeric(input) {
    return (input - 0) == input && input.length > 0;
}

function Validated() {
    var loanNo = $('#txtLoanNo').val();
    var branchNo = $('#txtBranchNo').val();
    var accountNo = $('#ddlCashDepositAccount').val();
    var amount = $('#txtDollarAmount').val();
    var bankAccount = $('#ddlBankAccount').val();
    var depositDate = $('#txtDepositDate').val();

    if (depositDate == '') {
        alert('Deposit Date is required.');
        return false;
    }

    if (loanNo == '') {
        alert('Loan Number is required.');
        return false;
    }

    if (accountNo == '') {
        alert('Account Number is required.');
        return false;
    }

    if (amount == '') {
        alert('Amount is required.');
        return false;
    }

    if (!IsNumeric(amount)) {
        alert ('Amount should be a number.');
        return false;
    }

    return true;
}
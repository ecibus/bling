function IsNumeric(input) {
    return (input - 0) == input && input.length > 0;
}

$(function () {
    $('.date').each(function () {
        $(this).datepicker();
    });
});

$(function () {
    ClearTextBox();
    Load();
});

$(function () {
    $('.update').live('click', function () {
        var id = $(this).attr('id').split('_')[1];
        var monthEnd = $('#txtME_' + id).val();
        var currentMonth = $('#txtCM_' + id).val();
        var currentMonthMinus1 = $('#txtCMM1_' + id).val();
        var currentMonthMinus2 = $('#txtCMM2_' + id).val();
        var fytd = $('#txtFYTD_' + id).val();

        if (!Validate(monthEnd, currentMonth, currentMonthMinus1, currentMonthMinus2, fytd)) {
            return false;
        }

        $.post('AjaxActiveBranch.aspx',
            {
                type: 'update',
                id: id,
                monthEnd: monthEnd,
                currentMonth: currentMonth,
                currentMonthMinus1: currentMonthMinus1,
                currentMonthMinus2: currentMonthMinus2,
                fytd: fytd
            },
            UpdateActiveBranchCallback,
            "json"
        );

    });
});

function UpdateActiveBranchCallback(o) {
    new DisplayMessage("").Clear();

    if (o.Message) {
        new DisplayMessage(o.Message).AsWarning();
        return;
    }
    Load();
}

$(function () {
    $('.edit').live('click', function () {
        var id = $(this).attr('id').split('_')[1];
        var oldMonthEnding = $('#me_' + id).html();
        var oldCurrentMonth = $('#cm_' + id).html();
        var oldCurrentMonthMinus1 = $('#cmm1_' + id).html();
        var oldCurrentMonthMinus2 = $('#cmm2_' + id).html();
        var oldFYTD = $('#fytd_' + id).html();

        var txtMonthEnding = "<input id='txtME_" + id + "' type='text' value='" + oldMonthEnding + "' class='span-3 date' />";
        var txtCurrentMonth = "<input id='txtCM_" + id + "' type='text' value='" + oldCurrentMonth + "' class='span-3' />";
        var txtCurrentMonthMinus1 = "<input id='txtCMM1_" + id + "' type='text' value='" + oldCurrentMonthMinus1 + "' class='span-3' />";
        var txtCurrentMonthMinus2 = "<input id='txtCMM2_" + id + "' type='text' value='" + oldCurrentMonthMinus2 + "' class='span-3' />";
        var txtFYTD = "<input id='txtFYTD_" + id + "' type='text' value='" + oldFYTD + "' class='span-3' />";
        var linkUpdate = "<a href='#' class='update' id='update_" + id + "'>Update</a> ";

        $('#me_' + id).html(txtMonthEnding);
        $('#cm_' + id).html(txtCurrentMonth);
        $('#cmm1_' + id).html(txtCurrentMonthMinus1);
        $('#cmm2_' + id).html(txtCurrentMonthMinus2);
        $('#upd_' + id).html(linkUpdate);
        $('#fytd_' + id).html(txtFYTD);

    });
});

$(function () {
    $('.del').live('click', function () {
        var id = $(this).attr('id').split('_')[1];
        Delete(id);
    });
});

$(function () {
    $('#btnAdd').bind('click', function () {

        if (!Validated()) {
            return false;
        }

        var monthEnd = $('#txtMonthEnd').val();
        var currentMonth = $('#txtCurrentMonth').val();
        var currentMonthMinus1 = $('#txtCurrentMonthM1').val();
        var currentMonthMinus2 = $('#txtCurrentMonthM2').val();
        var fytd = $('#txtFYTD').val();

        $.post('AjaxActiveBranch.aspx',
            {
                type: 'add',
                monthEnd: monthEnd,
                currentMonth: currentMonth,
                currentMonthMinus1: currentMonthMinus1,
                currentMonthMinus2: currentMonthMinus2,
                fytd: fytd
            },
            AddActiveBranchCallback,
            "json"
        );
    });
});

function AddActiveBranchCallback(o) {
    new DisplayMessage("").Clear();

    if (o.Message) {
        new DisplayMessage(o.Message).AsWarning();
        return;
    }
    Load();
    ClearTextBox();

}

function Delete(id) {
    $.post('AjaxActiveBranch.aspx',
        {
            type: 'delete',
            id: id
        },
        DeleteActiveBranchCallback
    );
}

function DeleteActiveBranchCallback() {
    Load();
}

function Load() {
    $.post('AjaxActiveBranch.aspx',
        {
            type: 'load'
        },
        LoadActiveBranchCallback
    );
}

function LoadActiveBranchCallback(o) {
    $('#data').html(o);
}

function ClearTextBox() {
    $('#txtMonthEnd').val('');
    $('#txtCurrentMonth').val('');
    $('#txtCurrentMonthM1').val('');
    $('#txtCurrentMonthM2').val('');
    $('#txtFYTD').val('');
}

function Validate(monthEnd, currentMonth, currentMonthM1, currentMonthM2, fytd) {
    if (monthEnd == '') {
        alert('Current Month is required.');
        return false;
    }

    if (currentMonth == '') {
        alert('Current Month is required.');
        return false;
    }

    if (currentMonthM1 == '') {
        alert('Current Month - 1 is required.');
        return false;
    }

    if (currentMonthM2 == '') {
        alert('Current Month - 2 is required.');
        return false;
    }

    if (fytd == '') {
        alert('FYTD is required.');
        return false;
    }

    if (!IsNumeric(currentMonth)) {
        alert('Current Month must be a number.');
        return false;
    }


    if (!IsNumeric(currentMonthM1)) {
        alert('Current Month - 1 must be a number.');
        return false;
    }

    if (!IsNumeric(currentMonthM2)) {
        alert('Current Month - 2 must be a number.');
        return false;
    }

    if (!IsNumeric(fytd)) {
        alert('FYTD must be a number.');
        return false;
    }

    return true;

}

function Validated() {
    var monthEnd = $('#txtMonthEnd').val();
    var currentMonth = $('#txtCurrentMonth').val();
    var currentMonthM1 = $('#txtCurrentMonthM1').val();
    var currentMonthM2 = $('#txtCurrentMonthM2').val();
    var fytd = $('#txtFYTD').val();

    return Validate(monthEnd, currentMonth, currentMonthM1, currentMonthM2, fytd);
}

/// <reference path="../../js/jquery-1.3.2.min.js" />
/// <reference path="../../js/Master.js" />

function init() {
    new DisplayMessage("").Clear();
    $('#LoanNumber').html("");
    $('#Borrower').html("");
    $('#Status').html("");
    $('#LoanProgram').html("");
    $('#Underwriter').html("");
    $('#Funder').html("");
    $('#Processor').html("");
    $('#Reviewer').html("");
    $('#ReviewType').html("");
    $('#DateReviewed').html("");
    $('#FileId').val("");
    $('#fields').html("");
    $('#State').html("");
}

$(function () {
    $('#txtLoanNumber').keypress(function (e) {
        if ((e.which && e.which == 13) || (e.keyCode && e.keyCode == 13)) {
            LoadLoan();
            return false;
        } else {
            return true;
        }
    });

});

$(function () {
    $('#btnLoad').bind('click', function () {
        LoadLoan();
    });
});

function LoadLoan() {
    var loanNumber = $('#txtLoanNumber').val();

    init();

    if (loanNumber == "") {
        new DisplayMessage("Loan Number is required.").AsInfo();
        return;
    }

    $.post(
            'AjaxDIRW.aspx',
            { type: 'load', LoanNumber: loanNumber },
            LoadLoanCallback,
            "json"
        );

}
function LoadLoanCallback(o) {
    if (o.Message) {
        new DisplayMessage(o.Message).AsInfo();
        return;
    }

    $('#LoanNumber').html(o.LoanNumber);
    $('#Borrower').html(o.Borrower);
    $('#Status').html(o.Status);
    $('#LoanProgram').html(o.LoanProgram);
    $('#Underwriter').html(o.Underwriter);
    $('#Funder').html(o.Funder);
    $('#Processor').html(o.Processor);
    $('#ReviewType').html(o.ReviewType);
    $('#Reviewer').html(o.Reviewer);
    $('#DateReviewed').html(o.DateReviewed);
    $('#FileId').val(o.FileId);
    //$('#State').html(o.State);


    if (o.ReviewType != "")
        LoadField(o.FileId, o.State, o.ReviewType);

    //LoadFinal1003(o.FileId);
}

function LoadFinal1003(fileid) {
    $.post(
        'AjaxDIRW.aspx',
        { type: 'loadfinal1003', fileid: fileid, r: Math.floor(Math.random() * 9999) },
        LoadFinal1003Data
    );
}

function LoadFinal1003Data(html) {
    $('#Extra_7').html(html);

    BindEvent();
}

function BindEvent() {
    $(':radio:disabled:checked').each(function (n) {
        $(this).parent().children('.fieldchange').show();
    });

    $('.calendar').each(function () {
        $(this).datepicker();
    });

    $('.newvalue').bind('change', function () {
        $(this).parent().addClass('in-process');
    });

    $('.newvalue').bind('keypress', function () {
        $(this).parent().addClass('in-process');
    });
}

function LoadField(fileid, state, reviewType) {
    $.post(
        'AjaxDIRW.aspx',
        { type: 'loadfield', fileid: fileid, state: state, reviewType: reviewType },
        LoadFieldData
    );
}

function LoadFieldData(html) {
    $('#fields').html(html);

    BindEvent();
}


$(function () {
    $(':radio').live('click', function () {
        var yn = $(this).val();
        if (yn == 'n') {
            $(this).parent().children('.fieldchange').slideDown('slow');
            $(this).parent().children('.fieldchange').addClass('in-process');
        } else {
            $(this).parent().children('.fieldchange').slideUp('slow');
            $(this).parent().children('.fieldchange').removeClass('in-process');

            var fieldid = $(this).attr('name').split('_')[1];
            var fileid = $('#FileId').val();
            var keyid = $(this).attr('name').split('_')[2];
            var oldvalue = $(this).parent().children('.fieldcontent').children('.currentdata').html();
                        
            $.post(
                'AjaxDIRW.aspx',
                {
                    type: 'markasyes',
                    fileid: fileid,
                    oldvalue: oldvalue,
                    fieldid: fieldid,
                    keyid: keyid
                },
                ShowMessage,
                "json"
            );
        }
    });
})

$(function () {
    $('#btnSave').live('click', function () {
        var fileid = $('#FileId').val();
        var notes = $('#txtNotes').val();
        var reviewType = $('#ReviewType').html();
        $.post(
            'AjaxDIRW.aspx',
            {
                type: 'savereview',
                fileid: fileid,
                notes: notes,
                reviewType: reviewType
            },
            AfterReviewSave,
            "json"
        );
    });
});

function AfterReviewSave(o, status) {
    //$('#btnLoad').click();

    if (o.Message) {
        new DisplayMessage(o.Message).AsInfo();
        return;
    }

    if (o.Untouched) {
        alert(o.Untouched);
        return;
    }

    var loanNumber = $('#txtLoanNumber').val();
    $.post(
        'AjaxDIRW.aspx',
        { type: 'load', LoanNumber: loanNumber },
        LoadReviewer,
        "json"
    );
}

function LoadReviewer(o) {
    if (o.Message) {
        new DisplayMessage(o.Message).AsInfo();
        return;
    }
    $('#Reviewer').html(o.Reviewer);
    $('#DateReviewed').html(o.DateReviewed);

    $('<div class="blinkingMessage">Review Saved</div>')
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

var thisSave;

$(function () {
    $(':checkbox').live('click', function () {
        $(this).parent().addClass('in-process');
    });

    $('a.save').live('click', function (e) {
        e.preventDefault();
        new DisplayMessage("").Clear();

        if ($(this).parent().hasClass('in-process') == false)
            return;


        var idval = $(this).attr('fieldid') //$(this).attr('href').split(' ')[0];
        var keyid = $(this).attr('keyid') //$(this).attr('href').split(' ')[1];
        var newvalueid = '#newvalue_' + idval;
        var value = $(this).parent().children('.newvalue').val();
        var fileid = $('#FileId').val();

        var elevated = $(this).parent().children(':checkbox').attr('checked') ? 1 : 0;
        var oldvalue = $(this).parent().parent().children('.fieldcontent').children('.currentdata').html();

        var dropDownText = "";
        if ($(newvalueid).attr('type') == 'select-one') {
            //dropDownText = $(newvalueid + ' :selected').text();
            dropDownText = $(this).parent().children().children(newvalueid + ' :selected').text(); 
        }

        //$('.msg_' + fileid).slideUp('slow');

        thisSave = $(this);
        $.post(
            'AjaxDIRW.aspx',
            {
                type: 'savefield',
                fileid: fileid,
                fieldid: idval,
                newvalue: value,
                oldvalue: oldvalue,
                elevated: elevated,
                dropdownText: dropDownText,
                keyid: keyid
            },
            AfterSave,
            "json"
        );
    });
});


function AfterSave(o, status) {
    if (o.Message) {
        $(thisSave).parent().parent().children('.saveerror').html(o.Message).slideDown('slow');
        return;
    }

    //$('.msg_' + o.FieldId).slideUp('slow');

    $(thisSave).parent().removeClass('in-process');
    $(thisSave).parent().parent().children(':radio').attr('disabled', 'disabled');

    $('<div class="blinkingMessage">Updated</div>')
        .insertAfter(
            $(thisSave)
        )
        .effect("pulsate", { times: 2 })
        .fadeIn2('slow')
        .animate({ opacity: 1.0 }, 3000)
        .fadeOut2('slow', function () {
            $(this).remove();
        });

    if (o.NewData) {
        $(thisSave).parent().parent().children('.fieldcontent').children('.currentdata').html(o.NewData);
        if ($(thisSave).parent().parent().children('.fieldcontent').children('.olddata').html() == "")
            $(thisSave).parent().parent().children('.fieldcontent').children('.olddata').html("(" + o.OldData + ")");
    }

    if (o.CountyCode) {
        $('#CountyCode').html(o.CountyCode);
    }

    if (o.MSACode) {
        $('#MSACode').html(o.MSACode);
    }

    if (o.StateCode) {
        $('#StateCode').html(o.StateCode);
    }
}

function ShowMessage(o) {
    if (o.Message) {
        new DisplayMessage(o.Message).AsInfo();
        return;
    }
}
$(function () {
    $('#txtLoanNumber').focus();
    $('#AuditDate').datepicker();
    $('#SubmittedDate').datepicker();    
});

function init() {
    new DisplayMessage("").Clear();

    $('#LoanNumber').html('&nbsp;');
    $('#LinkedLoanNumber').html('&nbsp;');
    $('#Borrower').html('&nbsp;');
    $('#LORep').html('&nbsp;');
    $('#Processor').html('&nbsp;');
    $('#Status').html('&nbsp;');
    $('#Program').html('&nbsp;');
    $('#LoanAmount').html('&nbsp;');
    $('#InterestRate').html('&nbsp;');
    $('#Locked').html('&nbsp;');
    $('#Expires').html('&nbsp;');
    $('#Days').html('&nbsp;');
    $('#InitialAuditor').val('');
    $('#AuditDate').val('');
    $('#SubmittedDate').val('');

    $('#InitialAuditor').attr("disabled", true);
    $('#AuditDate').attr("disabled", true);
    $('#SubmittedDate').attr("disabled", true);
    $('#btnSaveAuditor').attr("disabled", true);
    $('#btnCreateNote').attr("disabled", true);

    $('#LoanNumber').attr('FileId', '');

    $(':checkbox').attr("disabled", true);
    $(':checkbox').attr("checked", false);

    $('.Comment').val('');
    $('.Comment').parent().hide();

    $('.ItemTypeDropDown').val('');
    $('.ItemTypeDropDown').parent().hide();
    $('.SaveCommentAndType').hide();
}

$(function () {
    init();

    $(function () {
        $('#txtLoanNumber').keypress(function (e) {
            if ((e.which && e.which == 13) || (e.keyCode && e.keyCode == 13)) {
                $('#btnLoad').click();
                return false;
            } else {
                return true;
            }
        });
    });
});


$(function () {
    $('#btnSaveAuditor').click(function () {
        $.post(
            'AjaxAuditScoreCard.aspx',
            {
                type: 'saveauditor',
                LoanNumber: $('#txtLoanNumber').val(),
                InitialAuditor: $('#InitialAuditor').val(),
                InitialAuditorValue: $('#InitialAuditor :selected').text(),
                AuditDate: $('#AuditDate').val(),
                SubmittedDate: $('#SubmittedDate').val(),
                r: Math.floor(Math.random() * 1001)
            },
            SaveAuditorCallback,
            "json"
            );

        return false;
    });
});

function SaveAuditorCallback(o) {

    new DisplayMessage("").Clear();
    
    if (o.Message) {
        new DisplayMessage(o.Message).AsInfo();
        return;
    }


    $('<div class="blinkingMessage">Saving Initial Auditor, Audit Date and Submitted Date...</div>')
        .insertAfter(
            $('.LoanInfo')
        )
        //.effect("pulsate", { times: 2 })
        .fadeIn2('slow')
        .animate({ opacity: 1.0 }, 3000)
        .fadeOut2('slow', function () {
            $(this).remove();
        });

}

$(function () {
    $('#btnLoad').click(function () {
        init();

        if ($('#txtLoanNumber').val() == "") {
            new DisplayMessage("Loan Number is required.").AsInfo();
            return;
        }

        //$('#busy').show();
        $.post(
            'AjaxAuditScoreCard.aspx',
            {
                type: 'load',
                LoanNumber: $('#txtLoanNumber').val(),
                r: Math.floor(Math.random() * 1001)
            },
            LoadCallback,
            "json"
            );
        //$('#busy').hide();

        return false;

    });
});

function LoadCallback(o) {

    if (o.Message) {
        new DisplayMessage(o.Message).AsInfo();
        return;
    }

    $(':checkbox').attr("disabled", false);

    $('#LoanNumber').html(o.LoanNumber);
    $('#LinkedLoanNumber').html(o.LinkedLoanNumber);
    $('#Borrower').html(o.Borrower);
    $('#LORep').html(o.LORep);
    $('#Processor').html(o.Processor);
    $('#Status').html(o.Status);
    $('#Program').html(o.Program);
    $('#LoanAmount').html(o.LoanAmount);
    $('#InterestRate').html(o.InterestRate);
    $('#Locked').html(o.Locked);
    $('#Expires').html(o.Expires);
    $('#Days').html(o.Days);
    $('#InitialAuditor').val(o.InitialAuditor);
    $('#AuditDate').val(o.AuditDate);
    $('#SubmittedDate').val(o.SubmittedDate);

    $('#InitialAuditor').attr("disabled", false);
    $('#AuditDate').attr("disabled", false);
    $('#SubmittedDate').attr("disabled", false);
    $('#btnSaveAuditor').attr("disabled", false);
    $('#btnCreateNote').attr("disabled", false);

    $('#LoanNumber').attr('FileId', o.FileId);

    for (x = 0; x < o.ScoreIds.length; x++) {
        var id = o.ScoreIds[x];
        $('#chk_' + id).attr('checked', true);

        if ($('#chk_' + id).hasClass('CommentBox')) {
            $('#comment_' + id).parent().show();
            $('#comment_' + id).show();
            $('#saveCommentAndType_' + id).show();
        }

        if ($('#chk_' + id).hasClass('ItemType')) {
            $('#itemtype_' + id).parent().show();
            $('#itemtype' + id).show();
        }
    }

    SetScore(o);
    SetNoFindings(o);
    SetOtherScore(o);
    SetComment(o);
    SetItemType(o);
}

function SetNoFindings(o) {
    for (x = 0; x < o.NoFindings.length; x++) {
        $('#chkNF_' + o.NoFindings[x]).attr('checked', true);
        $("#chkNF_" + o.NoFindings[x] + "+ul li input[type='checkbox']").attr("disabled", true);
        $("#chkNF_" + o.NoFindings[x] + "+ul li input[type='checkbox']").attr("checked", false);
    }
}

function SetOtherScore(o) {
    for (x = 0; x < o.Other.length; x++) {
        $('#score_' + o.Other[x].ScoreId).html(o.Other[x].Score.toFixed(2));
    }
}

$(function () {   
    $('#btnCreateNote').live('click', function () {
    //$('#btnTest').live('click', function () {
        $.post(
            'AjaxAuditScoreCard.aspx',
            { type: 'createnote', FileId: $('#LoanNumber').attr('FileId') },
            CreateNoteCallback,
            "json"
            );
    });
});

function CreateNoteCallback(o) {
    if (o.Message) {
        alert (o.Message);
        return;
    }

    var missedGroup = "";
    var counter = 0;
    $('.NoFindings').each(function (a, b) {
        if ($(this).attr('checked') == false) {
            var hasNoCheck = true;

            //console.log($(b).prev().text());

            $(this).parent().children('ul').children('li').children("input[type='checkbox']").each(function (a, b) {
                if ($(b).attr('checked') == true) {
                    hasNoCheck = false;
                }
            });

            if (hasNoCheck) {
                missedGroup += '  - ' + $(b).prev().text() + "\r\n";
                //console.log('  - ' + $(b).prev().text());
                counter++;
            }
        }
    });


    if (missedGroup != "") {
        var s = counter > 1 ? "s" : "";
        alert("Please review the following group" + s + ":\r\n" + missedGroup);
        return;
    }
    

    $('<span class="blinkingMessage">&nbsp;&nbsp;Creating notes....</span>')
            .insertAfter($('#btnCreateNote'))
            .effect("pulsate", { times: 3 })
            .fadeIn2('slow')
            .animate({ opacity: 1.0 }, 3000)
            .fadeOut2('slow', function () {
                $(this).remove();
            });
}

$(function () {
    //$(':checkbox').click(function () {
    $('.SaveCommentAndType').live('click', function () {
        var id = $(this).attr('id').split('_')[1];
        var comment = $('#comment_' + id).val();
        var itemtype = $('#itemtype_' + id).val();

        if (itemtype != undefined) {
            if (itemtype == "") {
                alert("Please choose the Type (PTD or PTF)");
                return;
            }
        }

        $.post(
            'AjaxAuditScoreCard.aspx',
            { type: 'savecommentanditemtype', FileId: $('#LoanNumber').attr('FileId'), ItemId: id, Comment: comment, ItemType: itemtype },
            SaveCommentAndTypeCallback,
            "json"
            );
    });
});

function SaveCommentAndTypeCallback(o) {
    $('<span class="blinkingMessage">&nbsp;&nbsp;Saving....</span>')
            .insertAfter($('#saveCommentAndType_' + o.Id))
            .effect("pulsate", { times: 3 })
            .fadeIn2('slow')
            .animate({ opacity: 1.0 }, 3000)
            .fadeOut2('slow', function () {
                $(this).remove();
            })     
}


$(function () {
    //$(':checkbox').click(function () {
    $(':checkbox').live('click', function () {

        var idName = $(this).attr('id');
        if (idName.substring(0, 5) == 'chkNF') {
            NoFindings(idName);
            return;
        }

        var id = $(this).attr('id').split('_')[1];
        var score = parseFloat($('#score_' + id).text());
        var scoreItem = $('#ScoreCardItem_' + id).text();


        if ($('#score_' + id).hasClass('GetScore')) {
            if ($(this).attr('checked') == true) {

                newScore = prompt('Enter Score for ' + scoreItem, '0');
                if (!newScore.match(/[0-9]*\.?[0-9]+/)) {
                    alert('Please enter Score as numeric.');
                    $(this).attr('checked', false);
                    return false;
                }
                changeScore(id, newScore);
            } else {
                changeScore(id, '0');
            }
        }


        if ($(this).attr('checked') == true) {
            if ($('#chk_' + id).hasClass('CommentBox')) {
                $('#comment_' + id).parent().show();
                $('#comment_' + id).show();
                $('#saveCommentAndType_' + id).show();
            }
            if ($('#chk_' + id).hasClass('ItemType')) {
                $('#itemtype_' + id).parent().show();
                $('#itemtype' + id).show();
            }
        } else {
            if ($('#chk_' + id).hasClass('CommentBox')) {
                $('#comment_' + id).parent().hide();
                $('#comment_' + id).hide();
                $('#saveCommentAndType_' + id).hide();
            }
            if ($('#chk_' + id).hasClass('ItemType')) {
                $('#itemtype_' + id).parent().hide();
                $('#itemtype' + id).hide();
            }
        }

        score = $('#score_' + id).text();

        if ($(this).attr('checked') == true) {
            $.post(
                'AjaxAuditScoreCard.aspx',
                { type: 'savescore', FileId: $('#LoanNumber').attr('FileId'), ScoreId: id, Score: score },
                SetScore,
                "json"
            );
        } else {
            $.post(
                'AjaxAuditScoreCard.aspx',
                { type: 'removescore', FileId: $('#LoanNumber').attr('FileId'), ScoreId: id },
                SetScore,
                "json"
            );
        }


    });
});

function SetComment(o) {
    for (x = 0; x < o.Comments.length; x++) {
        $('#comment_' + o.Comments[x].ItemId).val(o.Comments[x].Comment);
    }
}

function SetItemType(o) {
    for (x = 0; x < o.ItemTypes.length; x++) {
        $('#itemtype_' + o.ItemTypes[x].ItemId).val(o.ItemTypes[x].ItemType);
    }
}

function SetScore(o) {
    var total = 0;
    for (x = 0; x < o.SubTotal.length; x++) {
        $('#total_' + o.SubTotal[x].GroupId).html(o.SubTotal[x].Score.toFixed(2));
        total += o.SubTotal[x].Score;
    }
    $('#TotalScore').html(total.toFixed(2));
}

function changeScore(id, newScore) {
    $('#score_' + id)
        .text(parseFloat(newScore).toFixed(2))
        .effect("pulsate", { times: 3 })
        .fadeIn2();
}

function NoFindings(idName) {
    var groupId = idName.split('_')[1];
    if ($('#' + idName).attr('checked') == true) {
        $("#" + idName + "+ul li input[type='checkbox']").attr("disabled", true);
        $.post(
            'AjaxAuditScoreCard.aspx',
            { type: 'addcategorywithnofindings', FileId: $('#LoanNumber').attr('FileId'), groupId: groupId },
            NoFindingsCallback,
            "json"
        );
    } else {
        $("#" + idName + "+ul li input[type='checkbox']").attr("disabled", false);
        $.post(
            'AjaxAuditScoreCard.aspx',
            { type: 'removecategorywithfindings', FileId: $('#LoanNumber').attr('FileId'), groupId: groupId },
            RemoveNoFindingsCallback,
            "json"
        );
    }
}

function NoFindingsCallback(o) {
    if (o.Message) {
        new DisplayMessage(o.Message).AsInfo();
        return;
    }

        //$("#chkNF_" + o.NoFindings[x] + "+ul li input[type='checkbox']").attr("disabled", true);
    $("#chkNF_" + o.Id + "+ul li input[type='checkbox']").attr("checked", false);
    $("#chkNF_" + o.Id + "+ul li input[type='checkbox']").click();
}


function RemoveNoFindingsCallback(o) {
    if (o.Message) {
        new DisplayMessage(o.Message).AsInfo();
        return;
    }
}

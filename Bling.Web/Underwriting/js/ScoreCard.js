$(function() {
    init();

    $(function() {
        $('#txtLoanNumber').keypress(function(e) {
            if ((e.which && e.which == 13) || (e.keyCode && e.keyCode == 13)) {
                $('#btnLoad').click();
                return false;
            } else {
                return true;
            }
        });
    });
});


$(function() {
    $('#btnPrintPreview').click(function() {
        $.post('AjaxScoreCard.aspx',
            {
                type: 'printpreview',
                loanNumber: $('#LoanNumber').html()
            },
            function() {
                window.open('Report/ScoreCard.pdf?r=' + Math.floor(Math.random() * 1001), 'Report');
            }
        );
    });
});

function init() {
    $(':checkbox').attr("disabled", true);
    $(':checkbox').attr("checked", false);
    $('span.Comment textarea').attr("disabled", true);

    $('#LoanNumber').html('');
    $('#Borrower').html('');
    $('#Underwriter').html('');
    $('#203K').html('');
    $('#Perfect').html('');
    $('#LoanNumber').attr('FileId', '');

    $(':button[value="Save"]').attr("disabled", true);
    $('#btnPrintPreview').hide();
    $('#busy').hide();
    
    var x = 1;

    while ($('#total_' + x).html() != null) {
        $('#total_' + x).html('0.00');
        $('#comment_' + x).val('');
        x += 1;
    }
    $('#TotalScore').html('0.00');
    
}

$(function() {
    $(':button[value="Save"]').click(function() {

        var id = $(this).attr('id').split('_')[1];
        var comment = $('#comment_' + id).val();

        $.post(
            'AjaxScoreCard.aspx',
            { type: 'savecomment', FileId: $('#LoanNumber').attr('FileId'), GroupId: id, Comment: comment },
            foo,
            "json"
            );

        $('<span class="blinkingMessage">&nbsp;&nbsp;Comment saved.</span>')
            .insertAfter($(this))
            .effect("pulsate", { times: 3 })
            .fadeIn2('slow')
            .animate({ opacity: 1.0 }, 3000)
            .fadeOut2('slow', function() {
                $(this).remove();
            })        
    });
});

$(function() {
    $('#btnLoad').click(function() {
        init();

        if ($('#txtLoanNumber').val() == "") {
            new DisplayMessage("Loan Number is required.").AsInfo();
            return;
        }

        $('#busy').show();
        $.post(
            'AjaxScoreCard.aspx',
            { type: 'load', LoanNumber: $('#txtLoanNumber').val() },
            Load,
            "json"
            );       
        $('#busy').hide();

        return false;

    });
});

function foo(o) {
}

function Load(o) {

    if (o.Message) {
        new DisplayMessage(o.Message).AsInfo();
        return;
    }

    $(':checkbox').attr("disabled", false);
    $('span.Comment textarea').attr("disabled", false);
    $(':button[value="Save"]').attr("disabled", false);
    new DisplayMessage('');
    
    $('#LoanNumber').html(o.LoanNumber);
    $('#Borrower').html(o.Borrower);
    $('#Underwriter').html(o.Underwriter);
    $('#LoanOfficer').html(o.LoanOfficer);
    $('#Processor').html(o.Processor);
    $('#203K').html(new String(o.Is203K).toUpperCase());
    $('#LoanNumber').attr('FileId', o.FileId);

    /*$('#Perfect').html('<input type="checkbox" id="chkPerfect" />');*/

    /*if (o.IsPerfect) {
        $("#chkPerfect").attr('checked', true);
        $(":checkbox[id!='chkPerfect']").attr("disabled", "disabled");
    }*/ 
    for (x = 0; x < o.ScoreIds.length; x++) {
        $('#chk_' + o.ScoreIds[x]).attr('checked', true);
    }

    SetScore(o);
    SetComment(o);
    SetOtherScore(o);
    SetNoFindings(o);
    $('#btnPrintPreview').show();

    /*$('#chkPerfect').each(function(a, b) {
        $(b).bind('click', PerfectCheckBoxClicked);
    });*/
}


function SetNoFindings(o) {
    for (x = 0; x < o.NoFindings.length; x++) {
        $('#chkNF_' + o.NoFindings[x]).attr('checked', true);
        $("#chkNF_" + o.NoFindings[x] + "+ul li input[type='checkbox']").attr("disabled", true);
    }
}

function SetScore(o) {
    var total = 0;
    for (x = 0; x < o.SubTotal.length; x++) {
        $('#total_' + o.SubTotal[x].GroupId).html(o.SubTotal[x].Score.toFixed(2));
        total += o.SubTotal[x].Score;
    }
    $('#TotalScore').html(total.toFixed(2));
    
    /*if (total > 0) {
        $("#chkPerfect").attr("disabled", "disabled");
    } else {
        $("#chkPerfect").removeAttr("disabled")
    }*/
}

function SetComment(o) {
    for (x = 0; x < o.Comments.length; x++) {
        $('#comment_' + o.Comments[x].GroupId).val(o.Comments[x].Comment);        
    }
}

function SetOtherScore(o) {
    for (x = 0; x < o.Other.length; x++) {
        $('#score_' + o.Other[x].ScoreId).html(o.Other[x].Score.toFixed(2));
    }
}

function PerfectCheck(o) {
    if (o.Message) {
        new DisplayMessage(o.Message).AsInfo();
        return;
    }

    $('<span class="blinkingMessage">&nbsp;&nbsp;Updated.</span>')
        .insertAfter($("#chkPerfect"))
        .effect("pulsate", { times: 1 })
        .fadeIn2('slow')
        .animate({ opacity: 1.0 }, 3000)
        .fadeOut2('slow', function() {
            $(this).remove();
        })   
}


function PerfectCheckBoxClicked () {   
    if ($(this).attr('checked') == true) {
        $(":checkbox[id!='chkPerfect']").attr("disabled", "disabled");
        $.post(
            'AjaxScoreCard.aspx',
            { type: 'addperfectloan', FileId: $('#LoanNumber').attr('FileId') },
            PerfectCheck,
            "json"
        );
    } else {
        $(":checkbox[id!='chkPerfect']").removeAttr("disabled");
        $.post(
            'AjaxScoreCard.aspx',
            { type: 'removeperfectloan', FileId: $('#LoanNumber').attr('FileId') },
            PerfectCheck,
            "json"
        );
    }
}

function NoFindingsCallback(o) {
    if (o.Message) {
        new DisplayMessage(o.Message).AsInfo();
        return;
    }

    return;
    $('<span class="blinkingMessage">&nbsp;&nbsp;Updated.</span>')
        .insertAfter($("#chkNF_" + o.Id))
        .effect("pulsate", { times: 1 })
        .fadeIn2('slow')
        .animate({ opacity: 1.0 }, 3000)
        .fadeOut2('slow', function () {
            $(this).remove();
        })
    }

function NoFindings(idName) {
    var groupId = idName.split('_')[1];
    if ($('#' + idName).attr('checked') == true) {
        $("#" + idName + "+ul li input[type='checkbox']").attr("disabled", true);
        $.post(
            'AjaxScoreCard.aspx',
            { type: 'addcategorywithnofindings', FileId: $('#LoanNumber').attr('FileId'), groupId: groupId },
            NoFindingsCallback,
            "json"
        );
    } else {
        $("#" + idName + "+ul li input[type='checkbox']").attr("disabled", false);
        $.post(
            'AjaxScoreCard.aspx',
            { type: 'removecategorywithfindings', FileId: $('#LoanNumber').attr('FileId'), groupId: groupId },
            NoFindingsCallback,
            "json"
        );
    }
}

$(function () {
    $(':checkbox').click(function () {

        var idName = $(this).attr('id');
        if (idName.substring(0, 5) == 'chkNF') {
            NoFindings(idName);
            return;
        }
        var id = $(this).attr('id').split('_')[1];
        var scoreText = $('#ScoreText_' + id).text();

        if (scoreText == 'Other') {
            if ($(this).attr('checked') == true) {

                newScore = prompt('Enter Score for Other', '0');
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

        var score = $('#score_' + id).text();

        if ($(this).attr('checked') == true) {
            $.post(
                'AjaxScoreCard.aspx',
                { type: 'savescore', FileId: $('#LoanNumber').attr('FileId'), ScoreId: id, Score: score },
                SetScore,
                "json"
            );
        } else {
            $.post(
                'AjaxScoreCard.aspx',
                { type: 'removescore', FileId: $('#LoanNumber').attr('FileId'), ScoreId: id },
                SetScore,
                "json"
            );
        }

    });
});


function changeScore(id, newScore) {
    $('#score_' + id)
        .text(parseFloat(newScore).toFixed(2))
        .effect("pulsate", { times: 3 })
        .fadeIn2() ;
}


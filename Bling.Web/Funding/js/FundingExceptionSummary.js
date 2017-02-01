$(function () {
    $('#btnLoadReport').bind('click', function () {
        var month = $('#_month').val();
        var year = $('#_year').val();

        new DisplayMessage("").Clear();

        //alert(month + ' - ' + year);

        $.post('AjaxFundingExceptionSummaryForm.aspx',
        {
            type: 'loaddata',
            m: month,
            y: year,
            r: Math.floor(Math.random() * 1001)
        },
        LoadDataCallback
    );
    });
});

function LoadDataCallback(o) {
    $('#list').html(o);
}

var saveButton;

$(function () {
    $('.saveComment').live('click', function () {
        saveButton = this;
        var id = $(this).attr('id');
        var brokerId = id.split('_')[0];
        var month = id.split('_')[1];
        var year = id.split('_')[2];
        //var comment = $('#' + brokerId.replace('\\', '\\\\')).val();
        //var comment = $('#' + brokerId.replace(/\\/g, "\\\\") ).attr('value');
        var comment = document.getElementById(brokerId).value;
        //alert(brokerId + ' - ' + month + ' - ' + year + ' - ' + comment);

        $.post('AjaxFundingExceptionSummaryForm.aspx',
            {
                type: 'savecomment',
                m: month,
                y: year,
                brokerId: brokerId,
                comment: comment,
                r: Math.floor(Math.random() * 1001)
            },
            SaveCommentCallback
        );

    });
});

function SaveCommentCallback() {
    $('<div class="blinkingMessage">Saving Comment...</div>')
    .insertAfter(
        saveButton
    )
    .effect("pulsate", { times: 2 })
    .fadeIn2('slow')
    .animate({ opacity: 1.0 }, 3000)
    .fadeOut2('slow', function () {
        $(this).remove();
    });
}
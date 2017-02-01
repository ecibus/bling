$(function () {
    $('.fundedDate').each(function () {
        var d = new Date();
        var x = (d.getMonth() + 1) + '/' + d.getDate() + '/' + d.getFullYear();

        $(this).datepicker({ defaultDate: x });
    });
});

$(function () {
    $('#btnPreview').bind('click', function () {
        PreviewCSV();
    });
});

function PreviewCSV() {

    $('#csvLink').html('');

    new DisplayMessage("").Clear();
    var fundedFrom = $('#txtFrom').val();
    var fundedTo = $('#txtTo').val();
    var batchno = $('#txtBatchNo').val();
    var includeByte = $('#chkIncludeByteLoans').is(':checked');

    if (fundedFrom == '') {
        new DisplayMessage("Funded From is required.").AsInfo();
        return;
    }

    if (fundedTo == '') {
        new DisplayMessage("Funded To is required.").AsInfo();
        return;
    }

    if (batchno == '') {
        new DisplayMessage("Batch No is required.").AsInfo();
        return;
    }


    $.post('AjaxCNBForm.aspx',
            {
                type: 'previewcsv',
                from: fundedFrom,
                to: fundedTo,
                batchno: batchno,
                includeByte: includeByte,
                r: Math.floor(Math.random() * 1001)
            },
        PreviewCSVCallback
    );
}

function PreviewCSVCallback(o) {
    $('#csvLink').html(o);
}

$(function () {
    $('#btnGenerate').bind('click', function () {
        GenerateCSV();
    });
});

function GenerateCSV() {

    $('#csvLink').html('');

    new DisplayMessage("").Clear();
    var fundedFrom = $('#txtFrom').val();
    var fundedTo = $('#txtTo').val();
    var batchno = $('#txtBatchNo').val();
    var includeByte = $('#chkIncludeByteLoans').is(':checked');

    if (fundedFrom == '') {
        new DisplayMessage("Funded From is required.").AsInfo();
        return;
    }

    if (fundedTo == '') {
        new DisplayMessage("Funded To is required.").AsInfo();
        return;
    }

    if (batchno == '') {
        new DisplayMessage("Batch No is required.").AsInfo();
        return;
    }

    $.post('AjaxCNBForm.aspx',
            {
                type: 'generatecsv',
                from: fundedFrom,
                to: fundedTo,
                batchno: batchno,
                includeByte: includeByte,
                r: Math.floor(Math.random() * 1001)
            },
        GenerateCSVCallback
    );
}

function GenerateCSVCallback(o) {
    $('#csvLink').html(o);
}
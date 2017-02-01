$(function () {
    $('.fundedDate').each(function () {
        $(this).datepicker();
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


    $.post('AjaxJPMorganForm.aspx',
            {
                type: 'previewcsv',
                from: fundedFrom,
                to: fundedTo,
                batchno: batchno,
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


    $.post('AjaxJPMorganForm.aspx',
            {
                type: 'generatecsv',
                from: fundedFrom,
                to: fundedTo,
                batchno: batchno,
                r: Math.floor(Math.random() * 1001)
            },
        GenerateCSVCallback
    );
}

function GenerateCSVCallback(o) {
    $('#csvLink').html(o);
}
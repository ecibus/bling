$(function () {
    $('#txtStart').datepicker();
    $('#txtEnd').datepicker();
    jQuery().busy("preload");
});


$(function () {
    LoadDate();
});

$(function () {
    $('#optWS').bind('change', function () {
        LoadDate();
    });
});

var thisButton;

$(function () {
    $('#btnGenerateCSV').bind('click', function () {
        thisButton = $(this).busy();
        $.post('Ajax401k.aspx',
        {
            type: 'generatecsv',
            isWeekly: $('#optWS').val(),
            start: $('#txtStart').val(),
            end: $('#txtEnd').val(),
            r: Math.floor(Math.random() * 1001)
        },
       GenerateCSVCallback,
       "json"
    );

    });
});

function GenerateCSVCallback(o) {
    thisButton.busy('hide');
    if (o.Error) {
        new DisplayMessage(o.Error).AsInfo();
    }
    $('#Message').html(o.Message);

}
$(function () {
    $('#btnViewReport').bind('click', function () {
        thisButton = $(this).busy();
        $.post('Ajax401k.aspx',
        {
            type: 'view401kreport',
            isWeekly: $('#optWS').val(),
            start: $('#txtStart').val(),
            end: $('#txtEnd').val(),
            r: Math.floor(Math.random() * 1001)
        },
       ViewReportCallback,
       'json'
    );

    });
});

function ViewReportCallback(o) {
    window.open('Report/401k.pdf?r=' + Math.floor(Math.random() * 1001), 'Report');
    thisButton.busy('hide');
}

function LoadDate() {
    $.post('Ajax401k.aspx',
        {
            type: 'LoadDates',
            reportType: $('#optWS').val(),
            r: Math.floor(Math.random() * 1001)
        },
       LoadDatesCallback,
       'json'
    );
}

    function LoadDatesCallback(o) {
        $('#txtStart').val(o.Start);
        $('#txtEnd').val(o.End);
    //alert(o);
    }

/*
$(function () {
    $('#btnCompute').bind('click', function () {
        /*
        var payDate = $('#txtPayDate').val();
        var fundedAsOf = $('#txtFundedAsOf').val();

        if (payDate == '') {
            new DisplayMessage("PayDate is Required.").AsInfo();
            return;
        }
        if (fundedAsOf == '') {
            new DisplayMessage("Funded As Of is Required.").AsInfo();
            return;
        }
        * /
        new DisplayMessage("").Clear();

        $.post('Ajax401k.aspx',
            {
                type: 'loadDates',
                reportType: 'w',
                r: Math.floor(Math.random() * 1001)
            },
            ComputeLOCommissionCallback
        );
    });
});
*/
var btnGenerate;

$(function () {
    jQuery().busy("preload"); 
});

$(function () {
    $('#btnGenerate').bind('click', function () {
        btnGenerate = $(this).busy();
        GenerateXML();
    });
});


function GenerateXML() {
    var year = $('#_year').val();
    var quarter = $('#_quarter').val();

    $('#xmlLink').html('');
    $.post('AjaxMCRForm.aspx',
            {
                type: 'generate',
                year: year,
                quarter: quarter,
                state: ''
        },
        GenerateCallback
    );
}

function GenerateCallback(o) {
    //window.open(o);
    btnGenerate.busy('hide');
    $('#xmlLink').html(o);
}

$(function () {
    $('#_year').bind('change', function () {
        //GetEndingInfo();
    });

    $('#_quarter').bind('change', function () {
        //GetEndingInfo();
    });
});


function GetEndingInfo() {
    var year = $('#_year').val();
    var quarter = $('#_quarter').val();

    $.post('AjaxMCRForm.aspx',
            {
                type: 'getendinginfo',
                year: year,
                quarter: quarter
            },
            GetEndingInfoCallback,
            "json"
        );

    //alert(quarter);
}

function GetEndingInfoCallback(o) {
    alert(o.Average);
}
$(function() {
    $('#txtLoanNumber').blur(function() {
        $('#Changes').fadeOut2().hide();
        $('#Changes').load('los.aspx', { type: 'gethmdachangesbyloannumber', loannumber: $('#txtLoanNumber').val() },
            function(data) { SetTableBehavior(data); }
        )
    });
});

$(function() {
    $('#btnAddChanges').click(function() {
        $('#Changes').fadeOut2().hide();
        $('#Changes').load(
            'los.aspx',
            {
                type: 'addhmdachanges',
                loannumber: $('#txtLoanNumber').val(),
                reportyear: $('select:first').val(),
                fieldname: $('select:last').val(),
                newdata: $('#txtNewData').val()
            },
            function(data) { SetTableBehavior(data); }
        )
    })
});

function SetTableBehavior(data) {
    $('#Changes').html(data).fadeIn2();
    $('table').addClass('t1');
    $('table tr:first').addClass('yellow');
    $('table tr td img').css('cursor', 'pointer').click(function() {
        var id = '#hmda' + $(this).attr('id');

        $.get('los.aspx', { type: 'deletehmdachanges', idtodelete: $(this).attr('id') },
            function() {
                $(id).find("td").fadeOut("slow");
            }
         );

    });
}
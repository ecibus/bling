$(function() {
    $('#btnHide').click(function() {
        ShowHide(1, "hidden");
    });
    $('#btnShow').click(function() {
        ShowHide(0, "available");
    });
});


function ShowHide(hide, status) {
    var investor = $('#ddlInvestor').val();
    if (investor == "") {
        $('#message').text('')
                        .removeClass("message ui-state-highlight ui-corner-all");
        $('#error').text('Please select investor.')
                        .addClass("error ui-state-error ui-corner-all");
        return;
    }
    $('#error').text('')
            .removeClass("error ui-state-error ui-corner-all");

    $('#summary').fadeOut2().hide();
    $('#summary').load('AjaxShowHideProgramCode.aspx',
            { type: "showhidebyinvestor", investor: investor, hide: hide, t: $('input:checked').val() },
                function(data) {
                    SetTableBehavior();
                    $('#message').text(investor + ' is now ' + status + ' in GEMLock')
                        .addClass("message ui-state-highlight ui-corner-all");
                }
        );
}

$(function() {
    $(':radio').click(function() {
        $('#summary').fadeOut2().hide();
        $('#summary').load('AjaxShowHideProgramCode.aspx',
            { type: "getprogramcodebyinvestor", t: $(this).val() },
                function(data) { SetTableBehavior(data); }
        );
    });
});

$(function() {
    SetTableBehavior();
});

function SetTableBehavior() {
    $('#summary table td').css('text-align', 'right');
    $('#summary table td:nth-child(1)').css('text-align', 'left');
    $('#summary table td:nth-child(3)').each(function(a, b) {
        if ($(b).html() == 0)
            $(this).parent().css('color', '#009900');
    });

    $('#summary table td:nth-child(4)').each(function(a, b) {
        if ($(b).html() == 0)
            $(this).parent().css('color', 'red');
    });
    $('#summary').fadeIn2();
}

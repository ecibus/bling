$(function() {
    $('#txtLoanNumber').focus();
});

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

$(function() {
    $('#btnLoad').click(function() {
        var loanNumber = $('#txtLoanNumber').val();
        if (loanNumber == "") {
            $('#error').html("Application/Loan Number is required.");
            $("#error").addClass("error ui-state-error ui-corner-all");
            return;
        }
        $('#error').html("");
        $("#error").removeClass("error ui-state-error ui-corner-all");

        $('#IncomeBreakdown').fadeOut2('10').hide();
        $('#IncomeBreakdown').load('IncomeBreakdownServer.aspx', { type: 'getincomebreakdown', loannumber: loanNumber },
            function(data) { SetTableBehavior(data); }
        )
        
    });
});

function SetTableBehavior(data) {
    $('#IncomeBreakdown').html(data).fadeIn2();
    $('#table2 td:nth-child(1)').css('font-weight', 'bolder');
    $('#table2 td:nth-child(2)').css('text-align', 'right');
    $('#table2 td:nth-child(3)').css('text-align', 'right');
    $('#table2 td:nth-child(4)').css('font-weight', 'bolder');
    $('#table2 td:nth-child(5)').css('text-align', 'right');

    $('#table3 td:nth-child(1)').css('text-align', 'right');
    $('#table4 td:nth-child(2)').css('text-align', 'right');
}
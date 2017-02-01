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

/*
$(function() {
    $('#btnLoad').click(function() {
        $('#ProgramCode').fadeOut2().hide();
        $('#ProgramCode').load('AjaxChangeProgramCode.aspx',
            { type: "loadloan", LoanNumber: $('#txtLoanNumber').val() },
                function(data) { SetTableBehavior(data); }
        );
    });
});
*/

$(function() {
$('#optInvestor').change(function() {
        //alert(unescape($("#lblProgramId").html()));
        //return;

        $.getJSON('AjaxChangeProgramCode.aspx',
            {
                type: 'getlsprogramdescription',
                ProgramId: unescape($("#lblProgramId").html()),
                Investor: $('#optInvestor').val(),
                r: Math.floor(Math.random() * 9999)
            },
            function(data) {
                $('#optProgramDescription').attr('length', 0);
                $('<option>-- Choose New Program Description --</option>').appendTo('#optProgramDescription');
                $.each(data.ProgramDescription, function(i, j) {
                    $('<option>' + j + '</option>').appendTo('#optProgramDescription');
                });

            }
        );

    });

});

$(function() {
    $('#btnLoad').click(function() {

        $.getJSON('AjaxChangeProgramCode.aspx',
        {
            type: 'loadloan',
            LoanNumber: $('#txtLoanNumber').val(),
            r: Math.floor(Math.random() * 9999)
        },
        function(data) {

            $('fieldset span').fadeOut2().hide();
            $("#lblLoanNumber").html(data.LoanNumber);
            $("#lblBorrower").html(data.Borrower);
            $("#lblLoanAmount").html(data.LoanAmount);
            $("#lblProgram").html(data.Program);
            $("#lblProgramId").html(escape(data.ProgramId));
            $("#lblStage").html(data.Stage);
            $("#lblLockExpiration").html(data.LockExpiration);
            $("#lblLoanOfficer").html(data.LoanOfficer);
            $("#lblInvestor").html(data.Investor);
            $("#lblDescription").html(data.Description);

            $('fieldset span').fadeIn2('slow');

            LoadInvestor();
        }
        );
    });
});


function LoadInvestor() {
    $.getJSON('AjaxChangeProgramCode.aspx',
        {
            type: 'getlsinvestor',
            ProgramId: data.ProgramId,
            r: Math.floor(Math.random() * 9999)
        },
        function(data) {
            $('#optInvestor').attr('length', 0);
            $('<option>-- Choose New Investor --</option>').appendTo('#optInvestor');
            $('#optProgramDescription').attr('length', 0);
            $('<option>-- Choose New Program Description --</option>').appendTo('#optProgramDescription');
            $.each(data.Investor, function(i, j) {
                $('<option>' + j + '</option>').appendTo('#optInvestor');
            });

        }
    );
 
}
/*
function SetTableBehavior(data) {

    $('table tr:even').addClass('odd');
    $('table').addClass('t1');
    $('#ProgramCode').fadeIn2();
}

$(function() {
    
});
*/
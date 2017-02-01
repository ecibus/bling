$(function() {
    $('#btnMap').click(function() {
        $('select').each(function(a, b) {
            var selectId = $(b).attr('id');
            var investorId = $(this).val();
            $('#Mapping').fadeOut2().hide();
            $('#Mapping').load('AjaxLoanSolution.aspx',
                { type: 'updateinvestor', lsinvestor: selectId, dtinvestor: investorId },
                function(data) { SetTableBehavior(data); }
            );
        });
    });
});

$(function() {
    if ($('table').size() == 0)
        $(':button').hide();
});

$(function() {
    $('table').addClass('t1');
    $('table tr:first').addClass('yellow');
});

$(function() {
    $('#Mapping').hide();
    $('#Mapping').load('AjaxLoanSolution.aspx',
        { type: 'getcurrentmapping' },
        function(data) { SetTableBehavior(data); }
    );
});

function SetTableBehavior(data) {
    $('#Mapping').html(data);
    $('#Mapping table').addClass('t1');
    $('#Mapping table tr:first').addClass('yellow');
    $('#Mapping').fadeIn2();

    if (data != "") {
        $('#Mapping').append('<p><input id="btnAdd" type="button" value="Add Loan Solution Program to GEMLock"/></p>');
        $('#btnAdd').click(function() {
            $('#Mapping').load('AjaxLoanSolution.aspx',
                { type: 'addloansolutionprogramtogemlock' });
        });
    }
}
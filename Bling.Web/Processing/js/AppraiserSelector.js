$(function() {
    $('#Load').bind('click', LoadLoan);
    $('#btnSelectAppraiser').bind('click', AddSelectedAppraiser);
    $('#LoanInfo').hide();
});


$(function() {
    /*
    $('#LoanNumber').keyup(function(e) {
        var loanNumber = $.trim($(this).val());

        if (loanNumber.length == 10) {    
            LoadLoan();
        }
    });
    */

    $('#LoanNumber').keypress(function(e) {
        if ((e.which && e.which == 13) || (e.keyCode && e.keyCode == 13)) {
            LoadLoan();
            return false;
        } else {
            return true;
        }
    });

});

function LoadLoan() {
    
    new DisplayMessage().Clear();

    $.getJSON('AjaxAppraiserSelector.aspx',
        {
            type: 'loadloan',
            LoanNumber: $('#LoanNumber').val(),
            r: Math.floor(Math.random() * 9999)
        },
        function(data) {
            if (data.LoanNumber) {
                $("#LoadedLoanNumber").html(data.LoanNumber);
                $("#Borrower").html(data.Borrower);
                $("#LoanType").html(data.LoanType);

                LoadAppraiser();
            } else {
                new DisplayMessage(data.Message).AsWarning();
                $('#LoanInfo').hide();
            }
        }
   );
}

function LoadAppraiser() {

    $.get('AjaxAppraiserSelector.aspx',
        {
            type: 'getappraiser',
            LoanNumber: $('#LoadedLoanNumber').html(),
            LoanType: $("#LoanType").html(),
            r: Math.floor(Math.random() * 9999)
        },
        function(data) {
            $('#Appraiser').html(data);
            if (data == 'No Available Appraiser') {
                $('#btnSelectAppraiser').hide();
            } else {
                $('#btnSelectAppraiser').show();
            }
            $('#LoanInfo').show();
            $('#TicketNo').val('');
            $('#chkUpdateDTAndPoint').attr('checked', false);
        }
   );
}

function AddSelectedAppraiser() {

    new DisplayMessage().Clear();

    var ticketno = "";

    var ticketno = $('#TicketNo').val();
    var updateDTAndPoint = $('#chkUpdateDTAndPoint').is(':checked');

    if (updateDTAndPoint) {
        if (ticketno == '') {
            new DisplayMessage('Ticket No is Required.').AsWarning();
            return;
        }
    } else {
        ticketno = "";
    }      

    $.post('AjaxAppraiserSelector.aspx',
        {
            type: 'addselectedappraiser',
            LoanNumber: $('#LoadedLoanNumber').html(),
            AppraiserId: $('#ddAppraiser').val(),
            TicketNo: ticketno
        },
        function(data) {
            if (data == 'Added') {
                new DisplayMessage($('#ddAppraiser  option:selected').text() + ' has been assigned to loan ' + $('#LoadedLoanNumber').html()).AsInfo();
            } else {
                new DisplayMessage(data).AsWarning();
            }
        }
    );
    
}
$(function () {
    $('.date').each(function () {
        $(this).datepicker();
    });
});


$(function () {
    $('#btnGenerate').bind('click', function () {
        Generate();
    });
});

function Generate() {
    var start = $('#txtStart').val();
    var end = $('#txtEnd').val();
    var loans = $('#txtLoans').val();

    if (loans == '') {
        if (start == "") {
            new DisplayMessage("Funded From is required.").AsInfo();
            return;
        }

        if (end == "") {
            new DisplayMessage("Funded To is required.").AsInfo();
            return;
        }
    }

    new DisplayMessage("").Clear();

    $.post(
        'AjaxComplianceEase.aspx',
        { type: 'generate', start: start, end: end, loans: loans, r: Math.floor(Math.random() * 1001) },
        GenerateCallback,
        "json"
    );
}

function GenerateCallback(o) {
    //window.open('report/' + o.FileName);
    $('#Link').html("Click <a href='Report/ComplianceEase.csv?r=" + Math.floor(Math.random() * 1001) +"'>this link</a> to open the generated file.");
}


$(function () {
    $('#txtEnd').keypress(function (e) {
        if ((e.which && e.which == 13) || (e.keyCode && e.keyCode == 13)) {
            LoadLoan();
            return false;
        } else {
            return true;
        }
    });

});
$(function () {
    $('#txtFundedFrom').datepicker();
    $('#txtFundedTo').datepicker();
});


$(function () {
    $('#btnGenerate').bind('click', function () {
        Generate();
    });
});

function Generate() {

    var fundedFrom = $('#txtFundedFrom').val();
    var fundedTo = $('#txtFundedTo').val();

    $.post('AjaxHedgeGMACExtractForm.aspx',
        {
            type: 'generate',
            start: fundedFrom,
            end: fundedTo,
            r: Math.floor(Math.random() * 1001)
        },
        GenerateCallback
    );

}

function GenerateCallback(o) {
    $('#linkToFile').html(o);
}

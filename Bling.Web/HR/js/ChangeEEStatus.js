$('#UpdateEEStatus').bind('click', function(e) {
    var recid = $('#recid').val();
    var newValue = $('#EEStatus').val();

    $.post('AjaxInsuranceEnrollment.aspx',
    {
        type: 'updateeestatus',
        recid: recid,
        newValue: newValue
    }
    );

    DropDownChange();

    $.nyroModalRemove();
    return false;

});
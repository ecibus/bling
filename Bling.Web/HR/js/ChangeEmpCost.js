
$('#UpdateEmpCost').bind('click', function(e) {
    var empCost = $('#EmpCost').val();
    var recId = $('#recid').val();

    if (!IsNumeric(empCost)) {
        alert('Employee Cost should be a number.');
        return false;
    }
    
    $.post('AjaxInsuranceEnrollment.aspx',
        {
            type: 'updateempcost',
            recid: recId,
            empCost: empCost
        }
    );
    DropDownChange();
    $.nyroModalRemove();
    return false;
});

function IsNumeric(input) {
    return (input - 0) == input && input.length > 0;
}
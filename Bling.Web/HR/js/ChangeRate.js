
$('#Update').bind('click', function(e) {
    var recid = $('#recid').val();
    var newValue = $('#InsuranceRates').val();
    var fieldNo = $('#fieldNo').val();
    $.post('AjaxInsuranceEnrollment.aspx',
    {
        type: 'updateenrollment',
        recid: recid,
        fieldNo: fieldNo,
        newValue: newValue
    }
    );
    DropDownChange();
    
    $.nyroModalRemove();
    return false;

});

$('#AddNewRate').bind('click', function(e) {
    var plan = $('#plan').html();
    var newRate = $('#newRate').val();
    var fieldNo = $('#fieldNo').val();
    var recid = $('#recid').val();

    if (newRate == '') {
        alert('Please enter new rate.');
        return false;
    }

    if (!IsNumeric(newRate)) {
        alert('Please enter a valid rate.');
        return false;
    }

    var rateExist = false;
    jQuery.each($('#InsuranceRates option'), function(a, b) {
        if (($(b).val() - 0) == (newRate - 0)) {
            alert(newRate + ' is already in the drop down.');
            rateExist = true;
            return false;
        }
    });

    if (rateExist) {
        return false;
    }

    $.post('AjaxInsuranceEnrollment.aspx', {
        type: 'addnewrate',
        insurancetype: fieldNo,
        newRate: newRate
    });

    $.post('AjaxInsuranceEnrollment.aspx', {
        type: 'updateenrollment',
        recid: recid,
        fieldNo: fieldNo,
        newValue: newRate
    });

    DropDownChange();

    $.nyroModalRemove();
    return false;
});

function IsNumeric(input) {
   return (input - 0) == input && input.length > 0;
}

$('#optEmpName').bind('change', function() {
    var bdate = $('#optEmpName').val();
    $('#BirthDate').html(bdate);
});

$('#EnrollEmployee').bind('click', function() {
    var empName = $('#optEmpName :selected').text();
    var branchNo = $('#BranchNoAdd').html();
    var birthDate = $('#optEmpName').val();
    var isLO = $('#chkIsLO').attr('checked') ? 1 : 0;
    var eeStatus = $('#EEStatus_Add').val();
    var rate1 = $('#InsuranceRates_1').val();
    var rate3 = $('#InsuranceRates_3').val();
    var rate4 = $('#InsuranceRates_4').val();
    var rate5 = $('#InsuranceRates_5').val();
    var rate6 = $('#InsuranceRates_6').val();
    var rate7 = $('#InsuranceRates_7').val();
    var rate9 = $('#InsuranceRates_9').val();
    var rate10 = $('#InsuranceRates_10').val();
    var rate11 = $('#InsuranceRates_11').val();
    var rate12 = $('#InsuranceRates_12').val();
    var empCost = $('#EmpCostAdd').val();
    var ym = $('#YearMonthAdd').val();

    if (empName == "") {
        alert("Please choose Employee to Add");
        return;
    }

    if (!IsNumeric(empCost)) {
        alert('Employee Cost should be a number.');
        return false;
    }    

    $.post('AjaxInsuranceEnrollment.aspx',
        {
            type: 'enrollemployee',
            ym: ym,
            empName: empName,
            branchNo: branchNo,
            birthDate: birthDate,
            isLO: isLO,
            eestatus: eeStatus,
            rate1: rate1,
            rate3: rate3,
            rate4: rate4,
            rate5: rate5,
            rate6: rate6,
            rate7: rate7,
            rate9: rate9,
            rate10: rate10,
            rate11: rate11,
            rate12: rate12,
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
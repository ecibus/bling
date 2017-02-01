$(function() {
    $('#InsuranceEnrollment').hide();
    $('#YearMonth').bind('change', DropDownChange);
    $('#Branch').bind('change', DropDownChange);
});


var currentTitle;

$(function() {
    $('#InsuranceEnrollment td.Title').bind('click', function() {
        currentTitle = this;
        $.nyroModalManual({
            width: 100,
            height: 100,
            modal: false,
            url: 'ChangeTitle.aspx?CurrentTitle=' + currentTitle.innerHTML +
                '&col=' + currentTitle.id + 
                '&ym=' + $('#YearMonth').val() +
                '&r=' + Math.floor(Math.random() * 1001), 
            beforeHideContent: function(elts, settings, callback) {
            callback();
            }
        });
    });
});

$(function() {
    $('#btnAddMonth').bind('click', function() {
        var yearmonth = $('#YearMonth').val();
        var month = $('#hr_month').val();
        var year = $('#hr_year').val();

        new DisplayMessage().Clear();

        $.post('AjaxInsuranceEnrollment.aspx', {
            type: 'addnewmonth',
            yearmonth: yearmonth,
            year: year,
            month: month
        },
        function(data, result) {
            if (data != "") {
                new DisplayMessage(data).AsInfo();
            }

            if (data.indexOf('created in the insurance enrollment database.') > 0) {
                $('<option></option>').val(year + month).html(year + month)
                    .insertAfter('#YearMonth option[value=""]');

                $.getScript('AjaxInsuranceEnrollment.aspx?type=load', function(data) { });
            }
        }
        );
    });
});

$(function() {
    $('#InsuranceEnrollment').hide();
});

$(function() {
    $.getScript('AjaxInsuranceEnrollment.aspx?type=load', function(data) { });
});

$(function() {
    $('#LOEnroll').bind('click', function() {
        DisplayAllBranch(1);
    });
});

$(function() {
    $('#InsEnroll').bind('click', function() {
        DisplayAllBranch(0);
    });
});

$(function() {
    $('#BranchEnroll').bind('click', function() {
        DisplayBranch();
    });
});

$(function() {
    $('#LOEnrollPB').bind('click', function() {
        DisplayAllBranchPB(1);
    });
});

$(function() {
    $('#InsEnrollPB').bind('click', function() {
        DisplayAllBranchPB(0);
    });
});

function DisplayBranch() {
    var yearmonth = $('#YearMonth').val();
    var branchno = $('#Branch').val();
    new DisplayMessage().Clear();

    if (yearmonth == '') {
        new DisplayMessage('Please choose the Month and Year of the Report').AsInfo();
        return;
    }

    if (branchno == '') {
        new DisplayMessage('Please choose the Branch of the Report').AsInfo();
        return;
    }

    DisplayReport(yearmonth, branchno, 0);

}

function DisplayAllBranch(islo) {
    var yearmonth = $('#YearMonth').val();
    var branchno = $('#Branch').val();
    new DisplayMessage().Clear();

    if (yearmonth == '') {
        new DisplayMessage('Please choose the Month and Year of the Report').AsInfo();
        return;
    }

    DisplayReport(yearmonth, 'all', islo);
}

function DisplayAllBranchPB(islo) {
    var yearmonth = $('#YearMonth').val();
    var branchno = $('#Branch').val();
    new DisplayMessage().Clear();

    if (yearmonth == '') {
        new DisplayMessage('Please choose the Month and Year of the Report').AsInfo();
        return;
    }

    DisplayReportPB(yearmonth, 'all', islo);
}


function DisplayReport(yearmonth, branch, islo) {
    $.post('AjaxInsuranceEnrollment.aspx',
            {
                type: 'displayreport',
                yearmonth: yearmonth,
                branch: branch,
                islo: islo
            },
            function() {
                window.open('Report/InsEnrol.pdf?r=' + Math.floor(Math.random() * 1001), 'Report');
            }
        );
}

function DisplayReportPB(yearmonth, branch, islo) {
    $.post('AjaxInsuranceEnrollment.aspx',
            {
                type: 'displayreportpb',
                yearmonth: yearmonth,
                branch: branch,
                islo: islo
            },
            function() {
                window.open('Report/InsEnrolPB.pdf?r=' + Math.floor(Math.random() * 1001), 'Report');
            }
        );
}

function DropDownChange() {
    var yearmonth = $('#YearMonth').val();
    var branchno = $('#Branch').val();
    $('#InsuranceEnrollment').hide();
    
    if (yearmonth != '' && branchno != '') {
        $('#InsuranceEnrollment').show();
        ChangeTitle(yearmonth);
        GetEnrolees(yearmonth, branchno);
        DisplayMonthAndYear(yearmonth);
    }
}

function DisplayMonthAndYear(yearmonth) {

    var month = new Array(12);
    month['01'] = "January";
    month['02'] = "February";
    month['03'] = "March";
    month['04'] = "April";
    month['05'] = "May";
    month['06'] = "June";
    month['07'] = "July";
    month['08'] = "August";
    month['09'] = "September";
    month['10'] = "October";
    month['11'] = "November";
    month['12'] = "December";

    var y = yearmonth.substr(0, 4);
    var m = yearmonth.substr(4, 2);

    $('#yearMonthLabel').html(month[m] + ' ' + y);
}

function GetEnrolees(yearmonth, branchno) {

    $.post('AjaxInsuranceEnrollment.aspx',
        {
            type: 'getenrollment',
            YearMonth: yearmonth,
            BranchNo: branchno
        },
        function(data, result) {
            $('#enrollment').html(data);

            BindInsuranceRate();
            BindInsuranceData();
            BindLOCheckBox();
            BindAddEnrolee();
            BindEmpCost();
            BindDelete();
        }
    );        
}

function BindDelete() {

    $('#InsuranceEnrollment tbody tr td img').css('cursor', 'pointer').click(function() {
        var recid = $(this).attr('id');        
        var empName = $(this).attr('empname');

        var remove = confirm("Do you want to remove enrollment of " + empName + "?")
        if (remove) {
            $.post('AjaxInsuranceEnrollment.aspx', {
                type: 'deleteenrollmentbyid',
                recid: recid
            }
            );
            DropDownChange();
        }   
        
        return false;
    });
}

function BindAddEnrolee() {
    $('#AddEnrolee').bind('click', function() {
        var yearmonth = $('#YearMonth').val(); ;
        var branchno = $('#Branch').val();
        $.nyroModalManual({
            width: 500,
            height: 450,
            modal: false,
            url: 'AddEnrolee.aspx?' +
                'branchNo=' + branchno +
                '&yearmonth=' + yearmonth +
                '&r=' + Math.floor(Math.random() * 1001),
            beforeHideContent: function(elts, settings, callback) {
                callback();
            },
            endShowContent: function(elts, settings) {
                //$('#nyroModalContent').fadeIn3(100);
            }
        });
    });
}

function BindLOCheckBox() {
    $('input.islo').bind('click', function() {
        var recid = $(this).attr('id').split('_')[1];
        var islo = $(this).attr('checked') ? 1 : 0;

        $.post('AjaxInsuranceEnrollment.aspx',
            {
                type: 'updateislo',
                recid: recid,
                islo: islo
            }
        );
    });
}


function BindInsuranceData() {

    $('#InsuranceEnrollment td.insdata').bind('click', function() {
        var field = $(this).attr("field");
        var value = $(this).html();
        var plan = $('#HR_Ins8_Title').html();
        var recid = $(this).attr("recid");

        $.nyroModalManual({
            width: 100,
            height: 100,
            modal: false,
            url: 'ChangeEEStatus.aspx?' +
                    '&val=' + value +
                    '&recid=' + recid +
                    '&r=' + Math.floor(Math.random() * 1001),
            beforeHideContent: function(elts, settings, callback) {
                callback();
            }
        });
    }); 
}

function BindEmpCost() {
    $('#InsuranceEnrollment td.empcost').bind('click', function() {
        var recid = $(this).attr("recid");
        var value = $(this).html();
        
        $.nyroModalManual({
            width: 100,
            height: 100,
            modal: false,
            url: 'ChangeEmpCost.aspx?' +
                    '&EmpCost=' + value +
                    '&recid=' + recid +
                    '&r=' + Math.floor(Math.random() * 1001),
            beforeHideContent: function(elts, settings, callback) {
                callback();
            }
        });
    });
}

function BindInsuranceRate() {
    $('#InsuranceEnrollment td.insrate').bind('click', function () {
        var field = $(this).attr("field");
        var value = $(this).html();
        var plan = $('#HR_' + field + '_Title').html();
        var recid = $(this).attr("recid");

        $.nyroModalManual({
            width: 100,
            height: 100,
            modal: false,
            url: 'ChangeRate.aspx?' +
                    '&plan=' + plan +
                    '&type=' + field.replace("Ins", "") +
                    '&field=' + field +
                    '&val=' + value +
                    '&recid=' + recid +
                    '&r=' + Math.floor(Math.random() * 1001),
            beforeHideContent: function (elts, settings, callback) {
                callback();
            }
        });
    });

}

function ChangeTitle(yearmonth) {
    for (var i = 0; i < Titles.length; i++) {
        if (yearmonth == Titles[i][0]) {
            $('#HR_Ins8_Title').html(Titles[i][8]);
            $('#HR_Ins1_Title').html(Titles[i][1]);
            $('#HR_Ins3_Title').html(Titles[i][3]);
            $('#HR_Ins4_Title').html(Titles[i][4]);
            $('#HR_Ins5_Title').html(Titles[i][5]);
            $('#HR_Ins6_Title').html(Titles[i][6]);
            $('#HR_Ins7_Title').html(Titles[i][7]);
            $('#HR_Ins9_Title').html(Titles[i][9]);
            $('#HR_Ins10_Title').html(Titles[i][10]);
            $('#HR_Ins11_Title').html(Titles[i][11]);
            $('#HR_Ins12_Title').html(Titles[i][12]);
            break;
        }
    }
}

function daysInMonth(iMonth, iYear) {
    return 32 - new Date(iYear, iMonth, 32).getDate();
}
 $(function () {
    var month = $('#_month').val();
    var year = $('#_year').val();
    var accepted = $('#chkAccepted').attr('checked');


    GenerateTable();
    //alert(daysInMonth(3, 2011));
});

$(function () {
    $('#Rejected').live('click', function () {
        var submitId = $(this).parent().attr('submitid');
        
         var jqxhr = $.ajax({
            type: "POST",
            url: 'AjaxTimeCardForm.aspx',
            data:
            {
                type: 'rejecttimecard',
                submitId: submitId,
                r: Math.floor(Math.random() * 1001)
                //,                empId: $('.container').attr('EmpId')
            },
            success: RejectTimeCardCallback,
            error: RejectTimeCardErrorCallback,
            dataType: "json"
        });

        //$('.tc_' + submitId).fadeOut('2000');
    });
});

function RejectTimeCardCallback(o) {
    GenerateTable();
}
function RejectTimeCardErrorCallback(a, b, c) {
    alert(b);
}

$(function () {
    $('#_month').change(function () {
        GenerateTable();
    });
    $('#_year').change(function () {
        GenerateTable();
    });

    $('#chkAccepted').click(function () {
        GenerateTable();
    });
});

function GenerateTable() {

    var accepted = $('#chkAccepted').attr('checked');

    $('colgroup').html('');
    $('#calendarHead').html('');
    $('#calendarBody').html('');
    CreateHeader();
    GetData();
}

function CreateHeader() {

    var mm = $('#_month').val();
    var yyyy = $('#_year').val();

    var noOfDays = daysInMonth(mm - 1, yyyy);
    var weekday = new Array(7);
    weekday[0] = "S";
    weekday[1] = "M";
    weekday[2] = "T";
    weekday[3] = "W";
    weekday[4] = "Th";
    weekday[5] = "F";
    weekday[6] = "S";

    var daysTD = '';
    var colGroup = '<col></col><col></col>';
    //alert(noOfDays);

    for (var x = 1; x <= noOfDays; x++) {
        daysTD += '<td>' + x + '<br />' + weekday[new Date(yyyy, mm - 1, x).getDay()] + '</td>';
        colGroup += '<col' + (weekday[new Date(yyyy, mm - 1, x).getDay()] == "S" ? ' class="weekend"' : '') + '></col>';
    }

    colGroup += '<col class="totalHours"></col><col></col><col></col><col></col>';
    daysTD += '<td>Tot</td><td></td><td></td>';

    //daysTD += '<td>&nbsp;&nbsp;&nbsp;</td><td>&nbsp;&nbsp;&nbsp;</td>';
    $(colGroup).appendTo('colgroup');
    $('<tr><td class="nameColumn">Employee Name</td><td></td>' + daysTD + '</tr>').appendTo('#calendarHead');


}



function GetData() {
    var mm = $('#_month').val();
    var yyyy = $('#_year').val();
    var accepted = $('#chkAccepted').attr('checked');

    var jqxhr = $.ajax({
        type: "POST",
        url: 'AjaxTimeCardForm.aspx',
        data:
            {
                type: 'gettimecard',
                month: mm,
                year: yyyy,
                accepted: accepted
            },
        success: GetBranchTimeCardCallback,
        error: GetBranchTimeCardErrorCallback,
        dataType: "json"
    });
}

function GetBranchTimeCardCallback(o) {
    //alert(o[0].Hours[4]);
    var noOfEmployee = o.length;

    //$('<tr><td><br /></td></tr>').appendTo('#calendarBody');
    for (var x = 0; x < noOfEmployee; x++) {
        CreateBody(o[x]);
    }
}

function GetBranchTimeCardErrorCallback(a, b, c) {
    alert(b);
}


function CreateBody(o) {
    var mm = $('#_month').val();
    var yyyy = $('#_year').val();

    var noOfDays = daysInMonth(mm - 1, yyyy);

    var regTD = '';
    var totalReg = 0;
    for (var x = 0; x < noOfDays; x++) {
        regTD += '<td>' + o.Reg[x] + '</td>';
        totalReg += o.Reg[x];
    }
    //alert(o.IsAccepted);

    var classIsSubmitted = "";
    if (o.IsSubmitted == 1) {
        classIsSubmitted = " submitted ";
    }

    var classAccepted = "";
    regTD += '<td class="totalHours">' + totalReg + '<td>';
    if (o.IsAccepted == 1) {
        regTD += "<td id='Rejected' ><img alt='Reject' src='/Images/reject.png' title='Reject' /></td>";
        classAccepted = " accepted ";
        classIsSubmitted = "";
    }

    var ovtTD = '';
    var totalOvt = 0;
    for (var x = 0; x < noOfDays; x++) {
        ovtTD += '<td>' + o.Ovt[x] + '</td>';
        totalOvt += o.Ovt[x];
    }
    ovtTD += '<td class="totalHours">' + totalOvt + '<td>';

    var dblTD = '';
    var totalDbl = 0;
    for (var x = 0; x < noOfDays; x++) {
        dblTD += '<td>' + o.Dbl[x] + '</td>';
        totalDbl += o.Dbl[x];
    }
    dblTD += '<td class="totalHours">' + totalDbl + '<td>';

    $(
        '<tr submitId="' + o.TimeCardSubmitId + '" class="tc_' + o.TimeCardSubmitId + classAccepted + classIsSubmitted + '">' +
            '<td class="nameColumn">' + o.Name + '</td>' +
            '<td>Reg</td>' +
            regTD +
        '</tr>'
     ).appendTo('#calendarBody');

    $(
        '<tr class="tc_' + o.TimeCardSubmitId + classAccepted + classIsSubmitted + '">' +
            '<td></td>' +
            '<td>Ovt</td>' +
            ovtTD +
        '</tr>'
     ).appendTo('#calendarBody');

    $(
        '<tr class="tc_' + o.TimeCardSubmitId + classAccepted + classIsSubmitted + '">' +
            '<td></td>' +
            '<td>Dbl</td>' +
            dblTD +
        '</tr>'
     ).appendTo('#calendarBody');

    // $('#calendarHead').html('<tr><td>' + o.Name + '</td>' + daysTD + '</tr>');
}

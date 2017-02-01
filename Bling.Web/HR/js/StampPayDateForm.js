$(function () {
    $('#txtPayDate').datepicker();
    $('#txtEndDate').datepicker();
});

$(function () {
    LoadLoanForStamping();
});

$(function () {
    $('#isWeekly, #isSalary').bind('click', function () {
        var isWeekly = $('#isWeekly').attr('checked')  ;
        //LoadLoanForStamping();
    });
});


$(function () {
    $('#btnStamp').bind('click', function () {
        StampPayDate();
    });
});

function StampPayDate() {
    var payDate = $('#txtPayDate').val();
    var endDate = $('#txtEndDate').val();

    //alert(payDate + ' - ' + endDate);

    var length = $('#list table tr').length;

    $("#list table td:nth-child(9):contains('Approved')").next(':empty').parent('tr td a').css("background-color", "#bbbbff");
    //$("#list table td:contains('Approved')").next(':empty').parent().css("background-color", "#bbbbff");

    $("#list table td:nth-child(9):contains('Approved')").next(':empty').parent()
        .each(function (index, e) {
            //alert($(e).find('a').html());
            //$(e).find('td:nth-child(10)').html(payDate);
            //return false;
            $.post('AjaxStampDateForm.aspx',
                {
                    type: 'stamp',
                    loanNumber: $(e).find('a').html(),
                    payDate: payDate,
                    r: Math.floor(Math.random() * 1001)
                },
                StampPayDateCallback,
                'json'
            );

            //return false;
        });

    //        LoadLoanForStamping();
    //alert ('a');
    //alert(length);
    return;
    //

}

function StampPayDateCallback(o) {
    var a = $("#list table td:nth-child(1):contains('" + o.LoanNumber + "')")
        .parent()
        .find('.paydate').html(o.StampDate);
}

$(function () {
    $('#btnLoad').bind('click', function () {
        LoadLoanForStamping();
    });
});


function LoadLoanForStamping() {
    var payDate = $('#txtPayDate').val();
    var endDate = $('#txtEndDate').val();
    var isWeekly = $('#isWeekly').val();

    if ($('#isWeekly').attr('checked') == true) {
        isWeekly = "1"; ;
    } else {
        isWeekly = "0";
    }

    

    //alert(payDate + ' - ' + endDate + ' - ' + isWeekly);
    //return;
    $.post('AjaxStampDateForm.aspx',
        {
            type: 'load',
            paydate: payDate,
            endDate: endDate,
            isWeekly: isWeekly,
            r: Math.floor(Math.random() * 1001)
        },
        LoadLoanForStampingCallback
    );
}


function LoadLoanForStampingCallback(o) {
    $('#list').html(o);
}


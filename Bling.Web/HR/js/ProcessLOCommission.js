
$(function () {
    $('#txtPayDate').datepicker();
    $('#txtFundedAsOf').datepicker();
    $('#txtDeadline').datepicker();
    $('#txtEndingDate').datepicker();
    $('#ddlBranchNo').addClass('span-2');
    jQuery().busy("preload"); 
});
var _guid;

$(function () {
        $('#btnCompute').bind('click', function () {
            var payDate = $('#txtPayDate').val();
            var fundedAsOf = $('#txtFundedAsOf').val();

            if (payDate == '') {
                new DisplayMessage("PayDate is Required.").AsInfo();
                return;
            }
            if (fundedAsOf == '') {
                new DisplayMessage("Funded As Of is Required.").AsInfo();
                return;
            }

            new DisplayMessage("").Clear();

            ComputeCommission();
        });
    });


    function ComputeCommission() {

        var payDate = $('#txtPayDate').val();
        var fundedAsOf = $('#txtFundedAsOf').val();
        var isWeekly = $('#isWeekly').attr('checked');

        //alert(new Date(payDate).getTime());
        //alert(new Date(fundedAsOf).getTime());
        //alert(1000 * 60 * 60 * 24);
        //alert(           (new Date(payDate).getTime() - new Date(fundedAsOf).getTime()) / (1000 * 60 * 60 * 24));
        //alert(Math.round((new Date(payDate).getTime() - new Date(fundedAsOf).getTime()) / (1000 * 60 * 60 * 24)));
        if (Math.round((new Date(payDate).getTime() - new Date(fundedAsOf).getTime()) / (1000 * 60 * 60 * 24)) < 13) {
            alert('Funded As Of should be at least 14 days to Pay Date.  Please change the Funded As Of Date and try again.');
            return;
        }

        //alert('continue');
        //return;
			
        $('#msgCompute').html('Computing LO Commission...').show();
        $.post('AjaxProcessLOCommission.aspx',
            {
                type: 'lo',
                payDate: payDate,
                fundedAsOf: fundedAsOf,
                isWeekly: isWeekly,
                r: Math.floor(Math.random() * 1001)
            },
            ComputeLOCommissionCallback
        );
    }

    function ComputeLOCommissionCallback(o) {

        //alert(o.length + ' - [' + o + ']');
        if (o.length > 2 ) {
            new DisplayMessage(o).AsWarning();
            return;
        }
        var payDate = $('#txtPayDate').val();
        var fundedAsOf = $('#txtFundedAsOf').val();
        $('#msgCompute').html('Computing Bonus Commission...');
        $.post('AjaxProcessLOCommission.aspx',
            {
                type: 'lo_oldloans',
                payDate: payDate,
                fundedAsOf: fundedAsOf,
                r: Math.floor(Math.random() * 1001)
            },
            //ComputeLOOldLoansCallback
            ComputeManagerCommissionCallback
        );
    }

    function ComputeLOOldLoansCallback(o) {

        if (o.length > 2) {
            new DisplayMessage(o).AsWarning();
            return;
        }

        var payDate = $('#txtPayDate').val();
        var fundedAsOf = $('#txtFundedAsOf').val();
        $('#msgCompute').html('Computing Manager Commission...');
        $.post('AjaxProcessLOCommission.aspx',
            {
                type: 'manager',
                payDate: payDate,
                fundedAsOf: fundedAsOf,
                r: Math.floor(Math.random() * 1001)
            },
            ComputeManagerCommissionCallback
        );
    }

    function ComputeManagerCommissionCallback(o) {

        if (o.length > 2) {
            new DisplayMessage(o).AsWarning();
            return;
        }

        var payDate = $('#txtPayDate').val();
        var fundedAsOf = $('#txtFundedAsOf').val();
        $('#msgCompute').html('Computing Overrides...');
        $.post('AjaxProcessLOCommission.aspx',
            {
                type: 'override',
                payDate: payDate,
                fundedAsOf: fundedAsOf,
                r: Math.floor(Math.random() * 1001)
            },
            //ComputeManagerOverrideCallback
            ComputeManagerTierCallback
        );
    }

//    function ComputeManagerOverrideCallback(o) {
//        if (o.length > 2) {
//            new DisplayMessage(o).AsWarning();
//            return;
//        }

//        var payDate = $('#txtPayDate').val();
//        var fundedAsOf = $('#txtFundedAsOf').val();
//        $('#msgCompute').html('Computing Manager Tier...');
//        $.post('AjaxProcessLOCommission.aspx',
//            {
//                type: 'lo_oldloans',
//                payDate: payDate,
//                fundedAsOf: fundedAsOf,
//                r: Math.floor(Math.random() * 1001)
//            },
//            ComputeManagerTierCallback
//        );
//    }

    function ComputeManagerTierCallback(o) {
        if (o.length > 2) {
            new DisplayMessage(o).AsWarning();
            return;
        }

        $('#msgCompute').html('Done.')
            .fadeOut(5000, function () {
                $(this).hide();
            });
        ;
    }

    var thisViewSummaryReportButton;

    $(function () {
        $('#btnViewSummaryReport').bind('click', function () {
            var payDate = $('#txtPayDate').val();
            var isWeekly = $('#isWeekly').attr('checked');
            _guid = createUUID();
            if (payDate == '') {
                new DisplayMessage("Pay Date is Required.").AsInfo();
                return;
            }

            new DisplayMessage("").Clear();

            thisViewSummaryReportButton = $(this).busy();

            $('#msgCompute').html('Generating report...').show();
            $.post('AjaxProcessLOCommission.aspx',
            {
                type: 'viewsummaryreport',
                payDate: payDate,
                isWeekly: isWeekly,
                guid: _guid,
                r: Math.floor(Math.random() * 1001)
            },
            ViewSummaryCallback
        );
        });
    });

    function ViewSummaryCallback() {
        $('#msgCompute').html('Done.')
            .fadeOut(5000, function () {
                $(this).hide();
            });
        ;
        thisViewSummaryReportButton.busy('hide');
        var payDate = $('#txtPayDate').val();
        window.open('Report/CommissionSummary-' + payDate.replace(/\//g, '-') + '-' + _guid + '.pdf?r=' + Math.floor(Math.random() * 1001), 'Report');
    }

    var thisViewCommissionReportButton;

    $(function () {
        $('#btnViewCommissionReport').bind('click', function () {
            var payDate = $('#txtPayDate').val();
            var branchno = $('#ddlBranchNo').val();
            var fundedAsOf = $('#txtFundedAsOf').val();
            var isWeekly = $('#isWeekly').attr('checked');
            _guid = createUUID();

            if (payDate == '') {
                new DisplayMessage("Pay Date is Required.").AsInfo();
                return;
            }

            if (fundedAsOf == '') {
                new DisplayMessage("Funded As Of is Required.").AsInfo();
                return;
            }

            new DisplayMessage("").Clear();

            thisViewCommissionReportButton = $(this).busy();

            $('#msgCompute').html('Generating report...').show();
            $.post('AjaxProcessLOCommission.aspx',
            {
                type: 'viewcommissionreport',
                payDate: payDate,
                fundedAsOf: fundedAsOf,
                branchno: branchno,
                guid: _guid,
                isWeekly: isWeekly,
                r: Math.floor(Math.random() * 1001)
            },
            ViewCommissionReportCallback
        );
        });
    });

    function ViewCommissionReportCallback() {
        $('#msgCompute').html('Done.')
            .fadeOut(5000, function () {
                $(this).hide();
            });
        ;
        thisViewCommissionReportButton.busy("hide"); 
        var branchno = $('#ddlBranchNo').val();
        window.open('Report/Commission-' + branchno + '-' +  _guid + '.pdf?r=' + Math.floor(Math.random() * 1001), 'Report');
    }

    // from here
    var thisEmailCommissionReportButton;

    $(function () {
        $('#btnEMailCommissionReport').bind('click', function () {
            var payDate = $('#txtPayDate').val();
            var branchno = $('#ddlBranchNo').val();
            var fundedAsOf = $('#txtFundedAsOf').val();
            

            if (payDate == '') {
                new DisplayMessage("Pay Date is Required.").AsInfo();
                return;
            }

            if (fundedAsOf == '') {
                new DisplayMessage("Funded As Of is Required.").AsInfo();
                return;
            }

            EmailCommissionReport(branchno)
        });
    });

    function EmailCommissionReport(branchno) {
        var payDate = $('#txtPayDate').val();        
        var fundedAsOf = $('#txtFundedAsOf').val();
        var isWeekly = $('#isWeekly').attr('checked');
        var deadline = $('#txtDeadline').val();
        var isWeekly = $('#isWeekly').attr('checked');
        var sendToManager = $('#chkSendToManager').attr('checked');
        var resend = $('input[name=resend]:checked').val();

        new DisplayMessage("").Clear();

        thisEmailCommissionReportButton = $('#btnEMailCommissionReport').busy();

        $.ajax({
          type: 'POST',
          url: 'AjaxProcessLOCommission.aspx',
          data: 
            {
                type: 'emailcommissionreport',
                payDate: payDate,
                fundedAsOf: fundedAsOf,
                branchno: branchno,
                isWeekly: isWeekly,
                deadline: deadline,
                sendToManager: sendToManager,
                resend: resend,
                r: Math.floor(Math.random() * 1001)
            },
          success: EMailCommissionReportCallback
          
        });

    }

    function EMailCommissionReportCallback(o) {
        //$('#msgCompute').html(o).show();        
        thisEmailCommissionReportButton.busy("hide");
        if ($.trim(o) != '') {
            new DisplayMessage(o).AsWarning();
        }
    }

    // to here



    //////
    $(function () {
        $('#btnEMailCommissionReportLO2').bind('click', function () {
            var payDate = $('#txtPayDate').val();
            var branchno = $('#ddlBranchNo').val();
            var fundedAsOf = $('#txtFundedAsOf').val();


            if (payDate == '') {
                new DisplayMessage("Pay Date is Required.").AsInfo();
                return;
            }

            if (fundedAsOf == '') {
                new DisplayMessage("Funded As Of is Required.").AsInfo();
                return;
            }

            EmailCommissionReportLO2(branchno)
        });
    });

    var intervalId = 0;
    function EmailCommissionReportLO2() {

        new DisplayMessage("").Clear();
        thisEmailCommissionReportButton = $('#btnEMailCommissionReportLO2').busy();

        //EmailCommissionToLO();
        intervalId = window.setInterval(EmailCommissionToLO, 10000);
        //intervalId = window.setTimeout(EmailCommissionToLO, 5000);
    }

    function EmailCommissionToLO() {
        var payDate = $('#txtPayDate').val();
        var fundedAsOf = $('#txtFundedAsOf').val();
        var isWeekly = $('#isWeekly').attr('checked');
        var deadline = $('#txtDeadline').val();
        var isWeekly = $('#isWeekly').attr('checked');
        var sendToManager = $('#chkSendToManager').attr('checked');
        var branchno = $('#ddlBranchNo').val();

        //console.log('Email...' + intervalId);

        $.ajax({
            type: 'POST',
            url: 'AjaxProcessLOCommission.aspx',
            data:
            {
                type: 'emailunsendreporttolo',
                payDate: payDate,
                fundedAsOf: fundedAsOf,
                branchno: branchno,
                isWeekly: isWeekly,
                deadline: deadline,
                sendToManager: sendToManager,
                intervalId: intervalId,
                r: Math.floor(Math.random() * 1001)
            },
            success: EMailUnsendCommissionToLOCallback
        });
    }

    function EMailUnsendCommissionToLOCallback(o) {
        var lo = o.replace(/\r?\n|\r/, "");
        //console.log("username: '" + lo + "'");
        if (lo === '') {
            window.clearInterval(intervalId);
            thisEmailCommissionReportButton.busy("hide");
        }
    }

    /////

    //
    $(function () {
        $('#btnEMailCommissionReportLO').bind('click', function () {
            var payDate = $('#txtPayDate').val();
            var branchno = $('#ddlBranchNo').val();
            var fundedAsOf = $('#txtFundedAsOf').val();


            if (payDate == '') {
                new DisplayMessage("Pay Date is Required.").AsInfo();
                return;
            }

            if (fundedAsOf == '') {
                new DisplayMessage("Funded As Of is Required.").AsInfo();
                return;
            }

            EmailCommissionReportLO(branchno)
        });
    });

    function EmailCommissionReportLO(branchno) {
        var payDate = $('#txtPayDate').val();
        var fundedAsOf = $('#txtFundedAsOf').val();
        var isWeekly = $('#isWeekly').attr('checked');
        var deadline = $('#txtDeadline').val();
        var isWeekly = $('#isWeekly').attr('checked');
        var sendToManager = $('#chkSendToManager').attr('checked');
        var resend = $('input[name=resend]:checked').val();

        new DisplayMessage("").Clear();

        thisEmailCommissionReportButton = $('#btnEMailCommissionReportLO').busy();

        $.ajax({
            type: 'POST',
            url: 'AjaxProcessLOCommission.aspx',
            data:
            {
                type: 'emailcommissionreportlo',
                payDate: payDate,
                fundedAsOf: fundedAsOf,
                branchno: branchno,
                isWeekly: isWeekly,
                deadline: deadline,
                sendToManager: sendToManager,
                resend: resend,
                r: Math.floor(Math.random() * 1001)
            },
            success: EMailCommissionReportLOCallback

        });

    }

    function EMailCommissionReportLOCallback(o) {
        //$('#msgCompute').html(o).show();        
        thisEmailCommissionReportButton.busy("hide");
        if ($.trim(o) != '') {
            new DisplayMessage(o).AsWarning();
        }
    }


    //

    function sleep(ms) {
        var dt = new Date();
        dt.setTime(dt.getTime() + ms);
        while (new Date().getTime() < dt.getTime());
    }

    

    $(function () {
        $('#Button1').bind("click", function () {
            var that = $(this).busy();
            setTimeout(function () {
                that.busy("hide"); 
                }, 2000); 
        });
    });

    var thisViewISHReportButton;

    $(function () {
        $('#btnViewISHReport').bind('click', function () {            
            var branchno = $('#ddlBranchNo').val();
            var month = $('#_month').val();
            var year = $('#_year').val();

            new DisplayMessage("").Clear();

            thisViewISHReportButton = $(this).busy();

            $('#msgCompute').html('Generating report...').show();
            $.post('AjaxProcessLOCommission.aspx',
            {
                type: 'viewishreport',                
                month: month,
                branchno: branchno,
                year: year,
                r: Math.floor(Math.random() * 1001)
            },
            ViewISHReportCallback
        );
        });
    });

    function ViewISHReportCallback() {
        $('#msgCompute').html('Done.')
            .fadeOut(5000, function () {
                $(this).hide();
            });
        ;
        thisViewISHReportButton.busy("hide");
        var branchno = $('#ddlBranchNo').val();
        window.open('Report/InsideSalesHourly-' + branchno + '.pdf?r=' + Math.floor(Math.random() * 1001), 'Report');
    }

    var thisEmailISHReportButton;

    $(function () {
        $('#btnEMailISHReport').bind('click', function () {
            var branchno = $('#ddlBranchNo').val();
            var month = $('#_month').val();
            var year = $('#_year').val();
            var deadline = $('#txtDeadline').val();
            var sendToManager = $('#chkSendToManager').attr('checked');

            new DisplayMessage("").Clear();

            thisEMailISHReportButton = $(this).busy();

            $('#msgCompute').html('Generating report...').show();
            $.post('AjaxProcessLOCommission.aspx',
            {
                type: 'emailishreport',
                month: month,
                branchno: branchno,
                year: year,
                deadline: deadline,
                sendToManager: sendToManager,
                r: Math.floor(Math.random() * 1001)
            },
            EMailISHReportCallback
        );
        });
    });

    function EMailISHReportCallback() {
        $('#msgCompute').html('Done.')
            .fadeOut(5000, function () {
                $(this).hide();
            });
        ;
        thisEMailISHReportButton.busy("hide");
        //var branchno = $('#ddlBranchNo').val();
        //window.open('Report/InsideSalesHourly-' + branchno + '.pdf', 'Report');
    }
    var thisButton;

    $(function () {
        $('#btnCA').bind('click', function () {
            var endingDate = $('#txtEndingDate').val();
            
            new DisplayMessage("").Clear();

            thisButton = $(this).busy();

            $('#msgCompute').html('Generating report...').show();
            $.post('AjaxProcessLOCommission.aspx',
            {
                type: 'viewcommissionsaccrual',
                endingDate: endingDate,
                r: Math.floor(Math.random() * 1001)
            },
            ViewCACallback
        );
        });
    });

    function ViewCACallback() {
        $('#msgCompute').html('Done.')
            .fadeOut(5000, function () {
                $(this).hide();
            });
        ;
        thisButton.busy("hide");

        window.open('Report/CommissionsAccrual.pdf?r=' + Math.floor(Math.random() * 1001), 'Report');
    }

    $(function () {
        $('#btnCAGLT').bind('click', function () {
            var endingDate = $('#txtEndingDate').val();

            new DisplayMessage("").Clear();

            thisButton = $(this).busy();

            $('#msgCompute').html('Generating report...').show();
            $.post('AjaxProcessLOCommission.aspx',
            {
                type: 'viewcommissionsaccrualgltotals',
                endingDate: endingDate,
                r: Math.floor(Math.random() * 1001)
            },
            ViewCAGLTCallback
        );
        });
    });

    function ViewCAGLTCallback() {
        $('#msgCompute').html('Done.')
            .fadeOut(5000, function () {
                $(this).hide();
            });
        ;
        thisButton.busy("hide");

        window.open('Report/CommissionsAccrualGLTotals.pdf?r=' + Math.floor(Math.random() * 1001), 'Report');
    }

    var thisViewOverrideReportButton;

    $(function () {
        $('#btnViewOverrideReport').bind('click', function () {
            var payDate = $('#txtPayDate').val();
            var branchno = $('#ddlBranchNo').val();
            var fundedAsOf = $('#txtFundedAsOf').val();
            var isWeekly = $('#isWeekly').attr('checked');

            if (payDate == '') {
                new DisplayMessage("Pay Date is Required.").AsInfo();
                return;
            }

            if (fundedAsOf == '') {
                new DisplayMessage("Funded As Of is Required.").AsInfo();
                return;
            }

            new DisplayMessage("").Clear();

            thisViewOverrideReportButton = $(this).busy();

            $('#msgCompute').html('Generating report...').show();
            $.post('AjaxProcessLOCommission.aspx',
            {
                type: 'viewoverridereport',
                payDate: payDate,
                fundedAsOf: fundedAsOf,
                branchno: branchno,
                isWeekly: isWeekly,
                r: Math.floor(Math.random() * 1001)
            },
            ViewOverrideReportCallback
        );
        });
    });

    function ViewOverrideReportCallback() {
        $('#msgCompute').html('Done.')
            .fadeOut(5000, function () {
                $(this).hide();
            });
        ;
        thisViewOverrideReportButton.busy("hide");
        var branchno = $('#ddlBranchNo').val();
        window.open('Report/CommissionOverride-' + branchno + '.pdf?r=' + Math.floor(Math.random() * 1001), 'Report');
    }

    function createUUID() {
        // http://www.ietf.org/rfc/rfc4122.txt
        var s = [];
        var hexDigits = "0123456789abcdef";
        for (var i = 0; i < 36; i++) {
            s[i] = hexDigits.substr(Math.floor(Math.random() * 0x10), 1);
        }
        s[14] = "4";  // bits 12-15 of the time_hi_and_version field to 0010
        s[19] = hexDigits.substr((s[19] & 0x3) | 0x8, 1);  // bits 6-7 of the clock_seq_hi_and_reserved to 01
        s[8] = s[13] = s[18] = s[23] = "-";

        var uuid = s.join("");
        return uuid;
    }

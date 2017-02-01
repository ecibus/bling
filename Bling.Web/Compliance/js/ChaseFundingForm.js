var chase = (function () {
    var 
        loadCallback = function (o) {
            if (o.Message) {
                new DisplayMessage(o.Message).AsInfo();
                return;
            }

            $("#ccode").html("C3G54");
            $("#deliveryContactName").html("suspense@gemcorp.com");
            $("#contactPhone").html("(661) 283-1269");
            $("#contactFax").html("N/A");

            $("#fileId").val(o.FileId);
            $("#loanNumber").html(o.LoanNumber);
            $("#underwriter").html(o.Underwriter + "<br/>" + o.UnderwriterEmail);
            $("#investorLoanNumber").html(o.InvestorLoanNumber);
            $("#commitmentNo").html(o.CommitmentNo);
            $("#underwritingPhoneNo").html(o.UnderwritingPhoneNo);
            $("#productDescription").html(o.ProductDescription);
            $("#correspondentName").html(o.CorrespondentName);
            $("#applicationDate").html(o.ApplicationDate);
            $("#txtPurchaseProperty").val(o.PurchaseProperty);
            $("#txtItem2").val(o.Item2);

            if (o.OccInvestmentYes === '1') {
                $("#chkOccInvestmentYes").attr('checked', true);
            } else {
                $("#chkOccInvestmentYes").attr('checked', false);
            }

            if (o.OccInvestmentNo === '1') {
                $("#chkOccInvestmentNo").attr('checked', true);
            } else {
                $("#chkOccInvestmentNo").attr('checked', false);
            }

            if (o.OccInvestmentNA === '1') {
                $("#chkOccInvestmentNA").attr('checked', true);
            } else {
                $("#chkOccInvestmentNA").attr('checked', false);
            }


            $("#armIndex").html(o.ARMIndex);
            $("#armMargin").html(o.ARMMargin);

            $("#txtAPORPcnt").val(o.APORPcnt);

            if (o.Item7AYes === '1') {
                $("#Item7AYes").attr('checked', true);
            } else {
                $("#Item7AYes").attr('checked', false);
            }

            if (o.Item7ANo === '1') {
                $("#Item7ANo").attr('checked', true);
            } else {
                $("#Item7ANo").attr('checked', false);
            }

            if (o.Item7BYes === '1') {
                $("#Item7BYes").attr('checked', true);
            } else {
                $("#Item7BYes").attr('checked', false);
            }

            if (o.Item7BNo === '1') {
                $("#Item7BNo").attr('checked', true);
            } else {
                $("#Item7BNo").attr('checked', false);
            }

            if (o.Item8Yes === '1') {
                $("#Item8Yes").attr('checked', true);
            } else {
                $("#Item8Yes").attr('checked', false);
            }

            if (o.Item8No === '1') {
                $("#Item8No").attr('checked', true);
            } else {
                $("#Item8No").attr('checked', false);
            }


            if (o.IsHPMLQM === '1') {
                $("#IsHPMLQMYes").attr('checked', true);
                $("#IsHPMLQMNo").attr('checked', false);
            } else {
                $("#IsHPMLQMNo").attr('checked', true);
                $("#IsHPMLQMYes").attr('checked', false);
            }
            $("#chkSameLenderNo").attr('checked', true);
            $("#txtPrepaymentPenaltyAmount").val(o.PrepaymentPenalty);

            $("#chkQMSafeHarbor").attr('checked', o.QMSafeHarbor === '1');
            $("#chkQMRebuttablePresumption").attr('checked', o.QMRebuttablePresumption === '1');
            $("#chkNonQM").attr('checked', o.NonQM === '1');
            $("#chkQMNotApplicable").attr('checked', o.QMNotApplicable === '1');

            $("#hoepaAPR").val(o.HoepaAPR);
            if (o.PointsExcluded === '1') {
                $("#rdoPointsExcludedYes").attr('checked', true);
            } else {
                $("#rdoPointsExcludedNo").attr('checked', true);
            }

            if (o.FeesImposed === '1') {
                $("#rdoFeesImposedYes").attr('checked', true);
            } else {
                $("#rdoFeesImposedNo").attr('checked', true);
            }

            $("#Item15Percent").val(o.Item15Percent);
            $("#Item15Amount").val(o.Item15Amount);
            $("#txtHOEPAQMPcnt").val(o.HOEPAQMPcnt);
            $("#txtHOEPAQMAmount").val(o.HOEPAQMAmount);
            $("#txtStatePcnt").val(o.StatePcnt);
            $("#txtStateAmount").val(o.StateAmount);

            $('#btnPrintPreview').removeAttr('disabled');
        },

        saveGeneralInfo = function (o) {
            console.log(o);
        },

        displaySaving = function (btnId) {
            $('<span class="blinkingMessage">&nbsp;&nbsp;Saving....</span>')
                .insertAfter($('#' + btnId))
                .effect("pulsate", { times: 3 })
                .fadeIn2('slow')
                .animate({ opacity: 1.0 }, 3000)
                .fadeOut2('slow', function () {
                    $(this).remove();
                });
        },


        validated = function () {
            var loanNumber = $('#txtLoanNumber').val();
            new DisplayMessage('').Clear();
            if (!loanNumber) {
                new DisplayMessage('Loan Number is required').AsWarning();
                return false;
            }
            return true;
        },

        printPreview = function () {
            //console.log('printpreview');
            window.open('Report/CFF.pdf?r=' + Math.floor(Math.random() * 1001), 'Report');
        };

    return {
        validated: validated,
        loadCallback: loadCallback,
        saveGeneralInfo: saveGeneralInfo,
        displaySaving: displaySaving,
        printPreview: printPreview
    };
})();

$(function () {
    $('#btnSaveGeneralInformation').click(function () {
        //console.log('Saving' + $('#fileId').val());
        //console.log($('#txtPurchaseProperty').val());
        chase.displaySaving('btnSaveGeneralInformation');
        $.post('AjaxChaseFundingForm.aspx',
            {
                type: 'savegeneralinfo',
                fileId: $('#fileId').val(),
                purchaseProperty: $('#txtPurchaseProperty').val(),
                item2: $('#txtItem2').val(),
                chkOccInvestmentYes: $('#chkOccInvestmentYes').attr('checked') ? '1' : '0',
                chkOccInvestmentNo: $('#chkOccInvestmentNo').attr('checked') ? '1' : '0',
                chkOccInvestmentNA: $('#chkOccInvestmentNA').attr('checked') ? '1' : '0',
                r: Math.floor(Math.random() * 1001)
            },
            chase.saveGeneralInfo,
            "json"
        );
    });
});

$(function () {
    $('#btnSaveATRQM').click(function () {
        chase.displaySaving('btnSaveATRQM');
        console.log('NA : ' + ($('#chkQMNotApplicable').attr('checked') ? '1' : '0'));
        $.post('AjaxChaseFundingForm.aspx',
        {
            type: 'saveatrqm',
            fileId: $('#fileId').val(),
            aporPcnt: $('#txtAPORPcnt').val(),
            qmSafeHarbor: $('#chkQMSafeHarbor').attr('checked') ? '1' : '0',
            qmRebuttablePresumption: $('#chkQMRebuttablePresumption').attr('checked') ? '1' : '0',
            nonQM: $('#chkNonQM').attr('checked') ? '1' : '0',
            qmNotApplicable: $('#chkQMNotApplicable').attr('checked') ? '1' : '0',
            Item7AYes: $('#Item7AYes').attr('checked') ? '1' : '0',
            Item7ANo: $('#Item7ANo').attr('checked') ? '1' : '0',
            Item7BYes: $('#Item7BYes').attr('checked') ? '1' : '0',
            Item7BNo: $('#Item7BNo').attr('checked') ? '1' : '0',
            Item8Yes: $('#Item8Yes').attr('checked') ? '1' : '0',
            Item8No: $('#Item8No').attr('checked') ? '1' : '0',
            r: Math.floor(Math.random() * 1001)
        },
        chase.saveGeneralInfo,
        "json"
    );
    });
});

$(function () {
    $('#btnSaveHighCost').click(function () {
        chase.displaySaving('btnSaveHighCost');
        $.post('AjaxChaseFundingForm.aspx',
        {
            type: 'savehighcost',
            fileId: $('#fileId').val(),
            prepaymentPenalty: $('#txtPrepaymentPenaltyAmount').val(),
            r: Math.floor(Math.random() * 1001)
        },
        chase.saveGeneralInfo,
        "json"
    );
    });
});

$(function () {
    $('#btnSaveHighCostContinued').click(function () {
        chase.displaySaving('btnSaveHighCostContinued');

        var pointsExcluded = $('input[name="rdoPointsExcluded"]:checked').val();
        var feesImposed = $('input[name="rdoFeesImposed"]:checked').val();

        if (pointsExcluded === undefined) pointsExcluded = '0';
        if (feesImposed === undefined) feesImposed = '0';

        $.post('AjaxChaseFundingForm.aspx',
        {
            type: 'savehighcostcontinued',
            fileId: $('#fileId').val(),
            pointsExcluded: pointsExcluded,
            feesImposed: feesImposed,
            hoepaAPR: $('#hoepaAPR').val(),
            r: Math.floor(Math.random() * 1001)
        },
        chase.saveGeneralInfo,
        "json"
    );
    });
});


$(function () {
    $('#btnSaveSpecialFeature').click(function () {
        chase.displaySaving('btnSaveSpecialFeature');

        $.post('AjaxChaseFundingForm.aspx',
        {
            type: 'savespecialfeature',
            fileId: $('#fileId').val(),
            SFFannieMae: $('#SFFannieMae').attr('checked') ? '1' : '0',
            SFFreddieMac: $('#SFFreddieMac').attr('checked') ? '1' : '0',
            MIMonthlyPremium: $('#MIMonthlyPremium').attr('checked') ? '1' : '0',
            MISinglePremium: $('#MISinglePremium').attr('checked') ? '1' : '0',
            r: Math.floor(Math.random() * 1001)
        },
        chase.saveGeneralInfo,
        "json"
    );
    });
});


$(function () {
    $('#btnExcludedBonafide').click(function () {
        chase.displaySaving('btnExcludedBonafide');
        $.post('AjaxChaseFundingForm.aspx',
        {
            type: 'saveexcludebonafide',
            fileId: $('#fileId').val(),
            item15Percent: $('#Item15Percent').val(),
            item15Amount: $('#Item15Amount').val(),
            HOEPAQMPcnt: $('#txtHOEPAQMPcnt').val(),
            HOEPAQMAmount: $('#txtHOEPAQMAmount').val(),
            StatePcnt: $('#txtStatePcnt').val(),
            StateAmount: $('#txtStateAmount').val(),
            r: Math.floor(Math.random() * 1001)
        },
        chase.saveGeneralInfo,
        "json"
    );
    });
});

$(function () {
    $('#btnLoad').click(function () {
        //console.log($('#txtLoanNumber').val());
        if (chase.validated()) {
            $.post('AjaxChaseFundingForm.aspx',
                {
                    type: 'load',
                    LoanNumber: $('#txtLoanNumber').val(),
                    r: Math.floor(Math.random() * 1001)
                },
                chase.loadCallback,
                "json"
                );
        }
        return false;

    });
});

$(function () {
    $('#btnPrintPreview').click(function () {
        //chase.displaySaving('btnSaveHighCost');
        $.post('AjaxChaseFundingForm.aspx',
        {
            type: 'printpreview',
            LoanNumber: $('#txtLoanNumber').val(),
            r: Math.floor(Math.random() * 1001)
        },
        function () {
            window.open('Report/CFF.pdf?r=' + Math.floor(Math.random() * 1001), 'Report');
        }
        );

    });
});

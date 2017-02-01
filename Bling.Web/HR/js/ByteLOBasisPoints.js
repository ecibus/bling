

function IsNumeric(input) {
    return (input - 0) == input && input.length > 0;
}

$(function () {
    $('#txtEffectiveDate').datepicker();
});



$(function () {
    $('#LO').bind('change', function () {
        LoadBasisPointsByLO();
        ClearFields();
    });
});

function LoadBasisPointsByLO() {
    new DisplayMessage("").Clear();

    var empId = $('#LO').val().split("|")[0];
    var branchno = $('#LO').val().split("|")[2];

    if (empId == '') {
        $('#branch').html('');
        $('#btnAdd').attr('disabled', 'disabled');
        return;
    }
    $('#branch').html($('#LO').val().split("|")[1]);
    $('#btnAdd').attr('disabled', '');
    $.post('AjaxLOBasisPoints.aspx',
        {
            type: 'getbytelobasispoints',
            empId: empId,
            branchNo: branchno,
            r: Math.floor(Math.random() * 1001)
        },
        GetLOBasisPointsCallback
    );
}

function GetLOBasisPointsCallback(o) {
    $('#list').html(o);
}

var thisButton;
/*
$(function () {
$('#btnVBPR').bind('click', function () {
var branchNo = $('#txtBranchNo').val();
var insideSales = $('#insideSales').attr('checked') ? '1' : '0';

thisButton = $(this).busy();


$.post('AjaxLOBasisPoints.aspx',
{
type: 'viewlobasispointreport',
branchNo: branchNo,
insideSales: insideSales,
r: Math.floor(Math.random() * 1001)
},
ViewLOBasisPointReportCallback
);
});
});


function ViewLOBasisPointReportCallback() {
thisButton.busy('hide');
window.open('Report/BasisPoints.pdf', 'Report');
}
*/

$(function () {
    $('#btnBasisPointHistory').bind('click', function () {
        var branchNo = $('#txtBranchNo').val();
        var insideSales = $('#insideSales').attr('checked') ? '1' : '0';

        thisButton = $(this).busy();


        $.post('AjaxLOBasisPoints.aspx',
        {
            type: 'viewbytelobasispointhistory',
            branchNo: branchNo,
            insideSales: insideSales,
            r: Math.floor(Math.random() * 2001)
        },
        ViewLOBasisPointHistoryCallback
    );
    });
});

function ViewLOBasisPointHistoryCallback() {
    thisButton.busy('hide');
    window.open('Report/ByteBasisPointsHistory.pdf?r=' + Math.floor(Math.random() * 1001), 'Report');
}

$(function () {
    $('#btnBasisPointCurrent').bind('click', function () {
        var branchNo = $('#txtBranchNo').val();
        var insideSales = $('#insideSales').attr('checked') ? '1' : '0';
        thisButton = $(this).busy();
        $.post('AjaxLOBasisPoints.aspx',
        {
            type: 'viewbytelobasispointcurrent',
            branchNo: branchNo,
            insideSales: insideSales,
            r: Math.floor(Math.random() * 3001)
        },
        ViewLOBasisPointCurrentCallback
    );
    });
});

$(function () {
    $('#btnLOBpCurrent').bind('click', function () {
        var val = $('#LO option:selected').val();
        var vals = val.split('|');
        var empID = vals[0];
        var insideSales = $('#insideSales').attr('checked') ? '1' : '0';
        thisButton = $(this).busy();
        $.post('AjaxLOBasisPoints.aspx',
		{
		    type: 'viewbytelobpcurrent',
		    empID: empID,
		    insideSales: insideSales,
		    r: Math.floor(Math.random() * 3001)
		},
		ViewLoBpCurrentCallback
		);
    });
});

$(function () {
    $('#btnLOBpHistory').bind('click', function () {
        var val = $('#LO option:selected').val();
        var vals = val.split('|');
        var empID = vals[0];
        var insideSales = $('#insideSales').attr('checked') ? '1' : '0';
        thisButton = $(this).busy();
        $.post('AjaxLOBasisPoints.aspx',
		{
		    type: 'viewbytelobphistory',
		    empID: empID,
		    insideSales: insideSales,
		    r: Math.floor(Math.random() * 3001)
		},
		ViewLoBpHistoryCallback
		);
    });
});

function ViewLOBasisPointCurrentCallback() {
    thisButton.busy('hide');
    window.open('Report/ByteBasisPointsCurrent.pdf?r=' + Math.floor(Math.random() * 1001), 'Report');
}
function ViewLoBpCurrentCallback() {
    thisButton.busy('hide');
    window.open('Report/ByteLoBpCurrent.pdf?r=' + Math.floor(Math.random() * 1001), 'Report');
}
function ViewLoBpHistoryCallback() {
    thisButton.busy('hide');
    window.open('Report/ByteLoBpHistory.pdf?r=' + Math.floor(Math.random() * 1001), 'Report');
}
$(function () {
    $('#btnAdd').bind('click', function () {
        var empId = $('#LO').val().split("|")[0];
        var brokerId = $('#LO').val().split("|")[2];
        var insideSalesRep = $('#chkInside').attr('checked');
        var manager = $('#chkManager').attr('checked');
        var effectiveDate = $('#txtEffectiveDate').val();
        var baseCommission = $('#txtBaseCommission').val();
        var minimum = $('#txtMinimum').val();
        var maximum = $('#txtMaximum').val();
        var tier1 = $('#txtTier1').val();
        var tier2 = $('#txtTier2').val();
        var tier3 = $('#txtTier3').val();
        var tier4 = $('#txtTier4').val();
        var tier5 = $('#txtTier5').val();
        var tier6 = $('#txtTier6').val();
        var branchOverride = $('#txtBranchOverride').val();
        var brokeredLoans = $('#txtBrokeredLoans').val();
        var weekly = $('#chkWeekly').attr('checked');
        new DisplayMessage("").Clear();

        if (!Validated()) {
            return false;
        }

        $.post('AjaxLOBasisPoints.aspx',
        {
            type: 'addbyte',
            empId: empId,
            brokerId: brokerId,
            insideSalesRep: insideSalesRep,
            effectiveDate: effectiveDate,
            baseCommission: baseCommission,
            minimum: minimum == "" ? "0" : minimum,
            maximum: maximum == "" ? "0" : maximum,
            tier1: tier1 == "" ? "0" : tier1,
            tier2: tier2 == "" ? "0" : tier2,
            tier3: tier3 == "" ? "0" : tier3,
            tier4: tier4 == "" ? "0" : tier4,
            tier5: tier5 == "" ? "0" : tier5,
            tier6: tier6 == "" ? "0" : tier6,
            branchOverride: branchOverride == "" ? "0" : branchOverride,
            brokeredLoans: brokeredLoans == "" ? "0" : brokeredLoans,
            manager: manager,
            weekly: weekly,
            r: Math.floor(Math.random() * 1001)
        },
        AddLOBasisPointsCallback
        );
    });
});

function AddLOBasisPointsCallback(o) {
    LoadBasisPointsByLO();
    ClearFields();
}

function ClearFields() {
    $('#chkInside').attr('checked', false);
    $('#chkManager').attr('checked', false);
    $('#chkWeekly').attr('checked', false);
    $('#txtEffectiveDate').val('');
    $('#txtBaseCommission').val('');
    $('#txtMinimum').val('');
    $('#txtMaximum').val('');
    $('#txtTier1').val('');
    $('#txtTier2').val('');
    $('#txtTier3').val('');
    $('#txtTier4').val('');
    $('#txtTier5').val('');
    $('#txtTier6').val('');
    $('#txtBranchOverride').val('');
    $('#txtBrokeredLoans').val('');

}

function Validated() {
    var empId = $('#LO').val();
    var effectiveDate = $('#txtEffectiveDate').val();
    var baseCommission = $('#txtBaseCommission').val();
    var minimum = $('#txtMinimum').val();
    var maximum = $('#txtMaximum').val();
    var tier1 = $('#txtTier1').val();
    var tier2 = $('#txtTier2').val();
    var tier3 = $('#txtTier3').val();
    var tier4 = $('#txtTier4').val();
    var tier5 = $('#txtTier5').val();
    var tier6 = $('#txtTier6').val();
    var overrides = $('#txtBranchOverride').val();
    var brokeredLoans = $('#txtBrokeredLoans').val();

    tier1 = tier1 == "" ? "0" : tier1;
    tier2 = tier2 == "" ? "0" : tier2;
    tier3 = tier3 == "" ? "0" : tier3;
    tier4 = tier4 == "" ? "0" : tier4;
    tier5 = tier5 == "" ? "0" : tier5;
    tier6 = tier6 == "" ? "0" : tier6;

    brokeredLoans = brokeredLoans == "" ? "0" : brokeredLoans;
    overrides = overrides == "" ? "0" : overrides;


    if (empId == '0') {
        new DisplayMessage("Please choose Loan Officer").AsInfo();
        return false;
    }
    if (effectiveDate == '') {
        new DisplayMessage("Effective Date is Required").AsInfo();
        return false;
    }
    if (baseCommission == '') {
        new DisplayMessage("Base Commission is Required").AsInfo();
        return false;
    }


    if (!IsNumeric(tier1)) {
        new DisplayMessage("Tier 1 should be a number").AsInfo();
        return false;
    }

    if (!IsNumeric(tier2)) {
        new DisplayMessage("Tier 2 should be a number").AsInfo();
        return false;
    }

    if (!IsNumeric(tier3)) {
        new DisplayMessage("Tier 3 should be a number").AsInfo();
        return false;
    }

    if (!IsNumeric(tier4)) {
        new DisplayMessage("Tier 4 should be a number").AsInfo();
        return false;
    }

    if (!IsNumeric(brokeredLoans)) {
        new DisplayMessage("Brokered Loans should be a number").AsInfo();
        return false;
    }

    if (!IsNumeric(overrides)) {
        new DisplayMessage("Overrides should be a number").AsInfo();
        return false;
    }

    return true;
}


$('.delete-bp').live('click', function () {
    var bpId = $(this).attr('id');

    //alert(id);

    //$(this).parent().parent().fadeOut('2000');

    $.post('AjaxLOBasisPoints.aspx',
        {
            type: 'removebytelobasispoints',
            bpId: bpId,
            r: Math.floor(Math.random() * 1001)
        },
        RemoveLOBasisPointsCallback
    );


});

function RemoveLOBasisPointsCallback() {
    LoadBasisPointsByLO();
}


$('.cbUpdate').live('click', function () {
    var id = $(this).attr('id');
    var item = id.split("_")[0];
    var bpId = id.split("_")[1];
    var newValue = $(this).attr('checked') ? 1 : 0;
    //alert(item + ' - ' + bpId + ' - ' + newValue);

    $.post('AjaxLOBasisPoints.aspx',
        {
            type: 'updatebytelobasispoint',
            bpId: bpId,
            item: item,
            newValue: newValue,
            r: Math.floor(Math.random() * 1001)
        },
        UpdateLOBasisPointsCallback
    );

});

function UpdateLOBasisPointsCallback(o) {

    if ($.trim(o) != '') {
        new DisplayMessage(o).AsInfo();
        return;
    }

    $('#msgUpdate').html('Done Updating.')
            .show()
            .fadeOut(3000, function () {
                $(this).hide();
            });
    ;

    LoadBasisPointsByLO();

}
function IsNumeric(input) {
    return (input - 0) == input && input.length > 0;
}

$(function () {
    ClearTextBox();
    Load();
    jQuery().busy("preload"); 
});

$(function () {
    var now = new Date();
    var year = now.getFullYear();
    var month = now.getMonth();
    if (month == 0) {
        month = 11;
    }
    $('#optMonth').val(month);
    $('#txtYear').val(year);
});

var thisButton;

/*
$(function () {
    $('#btnSendEmail').live('click', function () {
        var cb = $(".sendEmail").each(function () 
        alert(cb);
    });
});
*/

/*
$(function () {
    $('#btnSendEmail').live('click', function () {
        var ids = "";
        $('input[type=checkbox]').each(function () {
            if ($(this).is(':checked')) {
                var id = $(this).attr('id').split('_')[1];
                if (id == null)
                    return;
                ids = id + ",";
                //thisButton = $(this).busy();
                jQuery.ajaxSetup({ async: false }); 

                $.post('AjaxReserveRequirement.aspx',
                    {
                        type: 'emailreport',
                        ids: ids,
                        month: $("#optMonth").val(),
                        year: $("#txtYear").val(),
                        r: Math.floor(Math.random() * 1001)
                    },
                    EMailReportCallback,
                    "json"
                );
                jQuery.ajaxSetup({ async: true }); 

            }
            });
            jQuery.ajaxSetup({ async: true }); 

    });
});
*/

$(function () {
    $('#btnConsolidateGL').live('click', function () {

        thisButton = $('#btnConsolidateGL').busy();
        
        $.post('AjaxReserveRequirement.aspx',
            {
                type: 'consolidategl',
                r: Math.floor(Math.random() * 1001)
            },
            ConsolidateGLCallback,
            "json"
        );

    });
});

function ConsolidateGLCallback(o) {
    thisButton.busy('hide');
    
    if (o.Message) {
        new DisplayMessage(o.Message).AsWarning();
        return;
    }
}

$(function () {
    $('#btnRefreshData').live('click', function () {
        ClearTables();
		//console.clear();
        RefreshAMBData(1);
    });
});

function ClearTables() {
    $.post('AjaxReserveRequirement.aspx',
        {
            type: 'clearambdata',
            r: Math.floor(Math.random() * 1001)
        },
        ClearAMBDataCallback,
        "json"
    );
}

function ClearAMBDataCallback(o) {
}

function RefreshAMBData(counter) {
    $('#RefreshMessage').hide();
    $('#RefreshMessage').html("Processing " + counter + " of 9").show();
    thisButton = $('#btnRefreshData').busy();
	try
	{
		//$.post('AjaxReserveRequirement.aspx',
		//	{
		//		type: 'refreshambdata',
		//		counter: counter,
		//		r: Math.floor(Math.random() * 1001)
		//	},
		//	RefreshAMBDataCallback,
		//	"json"
		//);
		
		$.ajax({
			type: 'POST',
			url: 'AjaxReserveRequirement.aspx',
			data: {type:'refreshambdata', counter:counter, r: Math.floor(Math.random()*1001)},
			async:true,
			success:RefreshAMBDataCallback,
			dataType: 'json'
		});
	}
	catch(err)
	{
		alert(err);
	}
}

function RefreshAMBDataCallback(o) {
    //console.log('counter = ' + o.NextSql);
    thisButton.busy('hide');
    //return;
    if (o.NextSql > 0) {
        RefreshAMBData(o.NextSql);
    } else {
        $('#RefreshMessage').html('Done.')
            .fadeOut(1000, function () {
                $(this).hide();
            });
        ;
    }
}

$(function () {
    $('#btnSendEmail').live('click', function () {
        thisButton = $(this).busy();
        RefreshPLRecapData();
    });
});

function RefreshPLRecapData() {
    $.post('AjaxReserveRequirement.aspx',
        {
            type: 'refreshplrecapdata',
            month: $("#optMonth").val(),
            year: $("#txtYear").val(),
            r: Math.floor(Math.random() * 1001)
        },
        RefreshPLRecapDataCallback,
        "json"
    );

}

function RefreshPLRecapDataCallback(o) {

    thisButton.busy('hide');
    new DisplayMessage("").Clear();

    if (o.Message) {
        new DisplayMessage(o.Message).AsWarning();
        return;
    }
    //chris

    var ids = "";
    $('input[type=checkbox]').each(function () {
        if ($(this).is(':checked')) {
            var id = $(this).attr('id').split('_')[1];
            if (id == null)
                return;
            ids += id + ",";
            //jQuery.ajaxSetup({ async: false });
        }
    });
    
    
    $.post('AjaxReserveRequirement.aspx',
            {
                type: 'emailreport',
                ids: ids,
                month: $("#optMonth").val(),
                year: $("#txtYear").val(),
                emailtome: $('#chkSendToMe').attr('checked') ? 1 : 0,
                r: Math.floor(Math.random() * 1001)
            },
            EMailReportCallback,
            "json"
        );

}

function EMailReportCallback(o) {
    thisButton.busy('hide');
    new DisplayMessage("").Clear();

    if (o.Message) {
        new DisplayMessage(o.Message).AsWarning();
        return;
    }
    //alert('done');
}

$(function () {
    $('#chkAll').live('click', function () {
        var isChecked = $('#chkAll').attr('checked') ? 1 : 0;
        if (isChecked == 1) {
            $('.sendEmail').attr('checked', true);
        } else {
            $('.sendEmail').attr('checked', false);
        }
    });
});

$(function () {
    $('.update').live('click', function () {
        var id = $(this).attr('id').split('_')[1];
        var costCenter = $('#txtCC_' + id).val();
        var recipient = $('#txtR_' + id).val();
        var reserveMinimum = $('#txtRM_' + id).val();
        var fixedReserve = $('#txtFR_' + id).val();

        if (!Validate(costCenter, reserveMinimum, fixedReserve)) {
            return false;
        }

        $.post('AjaxReserveRequirement.aspx',
            {
                type: 'update',
                id: id,
                costCenter: costCenter,
                reserveMinimum: reserveMinimum,
                fixedReserve: fixedReserve,
                recipient: recipient,
                r: Math.floor(Math.random() * 1001)
            },
            UpdateReserveRequirementCallback,
            "json"
        );
        
    });
});

function UpdateReserveRequirementCallback(o) {
    new DisplayMessage("").Clear();

    if (o.Message) {
        new DisplayMessage(o.Message).AsWarning();
        return;
    }
    Load();
}

$(function () {
    $('.edit').live('click', function () {
        var id = $(this).attr('id').split('_')[1];
        var oldCostCenter = $('#cc_' + id).html();
        var oldRecipient = $('#r_' + id).html();
        var oldReserveMinimum = $('#rm_' + id).html().replace(/,/g, '');
        var oldFixedReserve = $('#fr_' + id).html().replace(/,/g, '');

        var txtCostCenter = "<input id='txtCC_" + id + "' type='text' value='" + oldCostCenter + "' class='span-2' />";
        var txtRecipient = "<input id='txtR_" + id + "' type='text' value='" + oldRecipient + "' class='span-6' />";
        var txtReserveMinimum = "<input id='txtRM_" + id + "' type='text' value='" + oldReserveMinimum + "' class='span-3' />";
        var txtFixedReserve = "<input id='txtFR_" + id + "' type='text' value='" + oldFixedReserve + "' class='span-3' />";
        var linkUpdate = "<a href='#' class='update' id='update_" + id + "'>Update</a> ";
        //alert(oldCostCenter);

        $('#cc_' + id).html(txtCostCenter);
        $('#r_' + id).html(txtRecipient);
        $('#rm_' + id).html(txtReserveMinimum);
        $('#fr_' + id).html(txtFixedReserve);
        $('#upd_' + id).html(linkUpdate);

        $('#txtCC_' + id).focus();
    });
});

$(function () {
    $('.del').live('click', function () {
        var id = $(this).attr('id').split('_')[1];
        Delete(id);
    });
});

$(function () {
    $('#btnAdd').bind('click', function () {

        if (!Validated()) {
            return false;
        }

        var costCenter = $('#txtCostCenter').val();
        var reserveMinimum = $('#txtReserveMinimum').val();
        var fixedReserve = $('#txtFixedReserve').val();
        var recipient = $('#txtRecipient').val();

        $.post('AjaxReserveRequirement.aspx',
            {
                type: 'add',
                costCenter: costCenter,
                recipient: recipient,
                reserveMinimum: reserveMinimum,
                fixedReserve: fixedReserve,
                r: Math.floor(Math.random() * 1001)
            },
            AddReserveRequirementCallback,
            "json"
        );



    });
});

function AddReserveRequirementCallback(o) {
    new DisplayMessage("").Clear();

    if (o.Message) {
        new DisplayMessage(o.Message).AsWarning();
        return;
    }
    Load();
    ClearTextBox();

}

function Delete(id) {
    $.post('AjaxReserveRequirement.aspx',
        {
            type: 'delete',
            id: id,
            r: Math.floor(Math.random() * 1001)
        },
        DeleteReserveRequirementCallback
    );
}

function DeleteReserveRequirementCallback() {
    Load();
}

function Load() {
    $.post('AjaxReserveRequirement.aspx',
        {
            type: 'load',
            r: Math.floor(Math.random() * 1001)
        },
        LoadReserveRequirementCallback
    );
}

function LoadReserveRequirementCallback(o) {
    $('#data').html(o);
}

function ClearTextBox() {
    $('#txtCostCenter').val('');
    $('#txtReserveMinimum').val('');
    $('#txtFixedReserve').val('');
    $('#txtRecipient').val('');
    $('#txtCostCenter').focus();
}

function Validate(costCenter, reserveMinimum, fixedReserve) {
    if (costCenter == '') {
        alert('Cost Center is required.');
        return false;
    }

    if (costCenter.length != 4) {
        alert('No of digits for Cost Center must be 4.');
        return false;
    }

    /*
    if (reserveMinimum == '') {
        alert('Reserve Minimum is required.');
        return false;
    }

    if (fixedReserve == '') {
        alert('Fixed Reserve is required.');
        return false;
    }
    */


    if (!IsNumeric(costCenter)) {
        alert('Cost Center should be a digit.');
        return false;
    }

    if (reserveMinimum != '') {
        if (!IsNumeric(reserveMinimum)) {
            alert('Reserve Minimum should be a number.');
            return false;
        }
    }

    if (fixedReserve != '') {
        if (!IsNumeric(fixedReserve)) {
            alert('Fixed Reserve should be a number.');
            return false;
        }
    }

    return true;

}

function Validated() {
    var costCenter = $('#txtCostCenter').val();
    var reserveMinimum = $('#txtReserveMinimum').val();
    var fixedReserve = $('#txtFixedReserve').val();

    return Validate(costCenter, reserveMinimum, fixedReserve);
}

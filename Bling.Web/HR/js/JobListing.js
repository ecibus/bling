function DropDownChange () {
    $.getJSON('AjaxJobListing.aspx',
        {
            type: 'getjob',
            id: $('#Jobs').val(),
            r: Math.floor(Math.random() * 1001)
        },
        function (data) {
            $("#txtPosition").val(data.Title);
            $("#txtQualification").val(data.Qualification);
            $("#txtDescription").val(data.Description);
            $("#txtSkills").val(data.Skills);
            $("#txtEducation").val(data.Education);
            $("#txtFilename").val(data.Filename);
            $("#txtLocationCity").val(data.LocationCity);
            $("#txtLocationBranch").val(data.LocationBranch);
            $("#txtSalary").val(data.Salary);
            $("#txtHourly").val(data.Hourly);
            $("#txtBenefits").val(data.Benefits);
            $("#txtCloseDate").val(data.CloseDate);
            $("#txtPostDate").val(data.PostDate);
            $("#txtFillDate").val(data.FillDate);
            $("#txtStartDate").val(data.StartDate);
            $("#txtComment").val(data.StartDateText);
            $("#optAvailablePdf").val(data.Attachment);


            if (data.Title != "") {
                $("#btnSave").removeAttr('disabled');
                $("#btnCreate").attr('disabled', 'disabled');
            } else {
                $("#btnSave").attr('disabled', 'disabled');
                $("#btnCreate").removeAttr('disabled');
            }

            new DisplayMessage('');
        }
    );
}

$(function () {
    $('#btnPrintJob').click(function () {

        var guid = createUUID();

        $.get('AjaxJobListing.aspx',
            {
                type: 'printjob',
                jobId: $('#Jobs').val(),
                guid: guid,
                r: Math.floor(Math.random() * 1001)
            },
            function () {
                window.open('Report/' + $('#txtPosition').val().replace(/ /g, '').replace('/', '') + '-' + guid + '.pdf?r=' + Math.floor(Math.random() * 1001), 'Report');
            }
        );
    });
});

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
$(function() {
    $('#btnEmailJob').click(function() {
        $.post('AjaxJobListing.aspx',
            {
                type: 'emailjob',
                jobId: $('#Jobs').val(),
                r: Math.floor(Math.random() * 1001)
            },
            function(data) {
                new DisplayMessage(data).AsInfo();
            }
        );
    });
});

    $(function () {
        $('#btnEmailJobToMe').click(function () {
            $.post('AjaxJobListing.aspx',
            {
                type: 'emailjobtome',
                jobId: $('#Jobs').val(),
                r: Math.floor(Math.random() * 1001)
            },
            function (data) {
                new DisplayMessage(data).AsInfo();
            }
        );
        });
    });

$(function() {
    $('#Jobs').bind('change', DropDownChange);
});

$(function() {    
    $('#txtCloseDate').datepicker();
    $('#txtPostDate').datepicker();
    $('#txtFillDate').datepicker();
    $('#txtStartDate').datepicker();
    $('form').validate({
        errorElement: "div",
        errorClass: "invalid"
    });
    if ($("#txtHourly").val() == "")
        $("#txtHourly").val("0");
    $('#txtPosition').focus();
});

$(function() {
    $('#rdoOpen').click(function() {
        $('#OpenJob').load('AjaxJobListing.aspx',
            {
                type: 'getopenjob'
            },
            function() {
                ClearEntries();
                $('#Jobs').bind('change', DropDownChange);                
                $("#btnSave").attr('disabled', 'disabled');
                $("#btnCreate").removeAttr('disabled');                
            }
        );
    });
    $('#rdoClose').click(function() {
        $('#OpenJob').load('AjaxJobListing.aspx',
            {
                type: 'getclosejob'
            },
            function() {
                ClearEntries();
                $('#Jobs').bind('change', DropDownChange);
                $("#btnSave").attr('disabled', 'disabled');
                $("#btnCreate").removeAttr('disabled');                
            }
        );
    });
});

$(function() {
    $('#btnCreate').click(function() {
        if ($("#txtPosition").val() == "") {
            new DisplayMessage('Position is Required.').AsWarning();
            return false;
        } else {
            new DisplayMessage('');
        }

        $.post('AjaxJobListing.aspx',
        {
            type: "createjob",
            position: $("#txtPosition").val(),
            qualification: $("#txtQualification").val(),
            description: $("#txtDescription").val(),
            skills: $("#txtSkills").val(),
            education: $("#txtEducation").val(),
            filename: $("#txtFilename").val(),
            locationCity: $("#txtLocationCity").val(),
            locationBranch: $("#txtLocationBranch").val(),
            salary: $("#txtSalary").val(),
            hourly: $("#txtHourly").val(),
            benefits: $("#txtBenefits").val(),
            closeDate: $("#txtCloseDate").val(),
            postDate: $("#txtPostDate").val(),
            fillDate: $("#txtFillDate").val(),
            startDate: $("#txtStartDate").val(),
            startDateText: $("#txtComment").val()
        },
        function(data, result) {
            if (data != "") {
                new DisplayMessage(data).AsWarning();
            } else {
                new DisplayMessage($("#txtPosition").val() + ' has been created.').AsInfo();
            }

        });

        $('#OpenJob').load('AjaxJobListing.aspx',
        {
            type: 'getopenjob'
        },
        function() {
            $('#Jobs').val($("#txtPosition").val());
            $("#btnSave").removeAttr('disabled');
            $("#btnCreate").attr('disabled', 'disabled');
            $("#rdoOpen").attr('checked', 'checked');
        }
        );

    });
});

$(function() {
    $('#btnSave').click(function() {
        $.post('AjaxJobListing.aspx',
        {
            type: "updatejob",
            id: $('#Jobs').val(),
            position: $("#txtPosition").val(),
            qualification: $("#txtQualification").val(),
            description: $("#txtDescription").val(),
            skills: $("#txtSkills").val(),
            education: $("#txtEducation").val(),
            filename: $("#txtFilename").val(),
            locationCity: $("#txtLocationCity").val(),
            locationBranch: $("#txtLocationBranch").val(),
            salary: $("#txtSalary").val(),
            hourly: $("#txtHourly").val(),
            benefits: $("#txtBenefits").val(),
            closeDate: $("#txtCloseDate").val(),
            postDate: $("#txtPostDate").val(),
            fillDate: $("#txtFillDate").val(),
            startDate: $("#txtStartDate").val(),
            startDateText: $("#txtComment").val(),
            attachment: $('#optAvailablePdf').val()
        },
        function(data, result) {
            if (data != "") {
                new DisplayMessage(data).AsWarning();
            } else {
                new DisplayMessage($("#txtPosition").val() + ' has been updated.').AsInfo();
            }

        });
    });

});

function ClearEntries() {
    $("#txtPosition").val("");
    $("#txtQualification").val("");
    $("#txtDescription").val("");
    $("#txtSkills").val("");
    $("#txtEducation").val("");
    $("#txtFilename").val("");
    $("#txtLocationCity").val("");
    $("#txtLocationBranch").val("");
    $("#txtSalary").val("");
    $("#txtHourly").val("0");
    $("#txtBenefits").val("");
    $("#txtCloseDate").val("");
    $("#txtPostDate").val("");
    $("#txtFillDate").val("");
    $("#txtStartDate").val("");
    $("#optAvailablePdf").val("");
}

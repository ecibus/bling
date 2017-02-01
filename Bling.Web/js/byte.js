$(function () {
    //$('#start').datepicker();
    //$('#end').datepicker();
});

$(function () {
    _.each(reports, function (e, i, l) {
        $('#rptList').append('<option value="' + i + '">' + e.Title + '</option>');
    });

});

function GetFilesCallback(o) {

}

$(function () {
    $('#btnViewReport').live('click', function () {
        var param = _.filter($('#rptParameter').val().split(','), function (e) {
            return e !== '';
        });

        var types = _.filter($('#rptType').val().split(','), function (e) {
            return e !== '';
        });

        var parameters = '';

        _.each(param, function (e, i) {
            //console.log($('#' + e).val());
            parameters += '@' + e + '|' + $('#' + e).val() + '|' + types[i] + ',';
        });

        var reportName = $('#rptFileName').val();
        $.post('AjaxByteReportForm.aspx',
            {
                type: 'viewreport',
                reportName: reportName,
                pdfName: reportName.replace('.rpt', '.pdf'),
                parameters: parameters,
                r: Math.floor(Math.random() * 1001),
                s: Math.floor(Math.random() * 1001)
            },
            ViewReportCallback,
            "json"
        );
    });

    $('#btnSaveToByte').live('click', function () {
        var param = _.filter($('#rptParameter').val().split(','), function (e) {
            return e !== '';
        });

        var types = _.filter($('#rptType').val().split(','), function (e) {
            return e !== '';
        });

        var parameters = '';

        _.each(param, function (e, i) {
            //console.log(e);
            //console.log($('#' + e).val());
            parameters += '@' + e + '|' + $('#' + e).val() + '|' + types[i] + ',';
        });

        //console.log(parameters);
        var reportName = $('#rptFileName').val();
        $.post('AjaxByteReportForm.aspx',
            {
                type: 'savetobyte',
                reportName: reportName,
                pdfName: reportName.replace('.rpt', '.pdf'),
                parameters: parameters,
                r: Math.floor(Math.random() * 1001),
                s: Math.floor(Math.random() * 1001)
            },
            SaveToByteCallback,
            "json"
        );
    });

});

function SaveToByteCallback(o) {
    if (o.Message) {
        new DisplayMessage(o.Message).AsInfo();
    }

    if (o.Error) {
        new DisplayMessage(o.Message).AsWarning();
        return;
    }
}

function ViewReportCallback() {
    var filename = $('#rptFileName').val().replace('.rpt', '.pdf');
    filename = filename.substr(filename.lastIndexOf('\\') + 1);
    window.open('Byte\\' + filename + '?r=' + Math.floor(Math.random() * 1001), 'Report');
}

$(function () {
    $('#rptList').live('click', function () {
        var reportTitle = $(this).find(':selected').text();
        var reportName = $(this).find(':selected').val();

        var rpt = _.find(reports, function (p) {
            return p.Title === reportTitle;
        });

        var parameter = '<h4>' + reportTitle + '</h4>';
        parameter += '<input type="hidden" id="rptFileName" value="' + rpt.FileName + '" />';
        var paramId = '';
        var types = '';
        var hasDatePicker = false;
        _.each(rpt.Parameters, function (p) {
            if (p.Name === 'start' || p.Name === 'end') {
                hasDatePicker = true;
            }
            parameter += '<b>' + p.Prompt + '</b>' +
                '<br />' +
                '<input type="text" id="' + p.Name + '" />' +
                '<br />'
            paramId += p.Name + ',';
            types += p.Type + ',';
        });


        parameter += '<input type="hidden" id="rptParameter" value="' + paramId + '" />';
        parameter += '<input type="hidden" id="rptType" value="' + types + '" />';
        $('#parameters').html(parameter);

        if (hasDatePicker) {
            $('#start').datepicker();
            $('#end').datepicker();
            $('#dstart').datepicker();
            $('#dend').datepicker();
        }

        $('#btnViewReport').show();
        if (reportTitle == 'Pre-Close Compliance Audit Results') {
            $('#btnSaveToByte').show();
        } else {
            $('#btnSaveToByte').hide();
        }
    });
});

function GetParametersCallback() {
}

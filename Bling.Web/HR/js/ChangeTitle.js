
$('#Update').bind('click', function(e) {
    $.post('AjaxInsuranceEnrollment.aspx',
        {
            type: 'updatetitle',
            YearMonth: $('#YearMonth').val(),
            Column: $('#col').val(),
            NewTitle: $('#newTitle').val()
        }
    );
    $.getScript('AjaxInsuranceEnrollment.aspx?type=load', function(data) { });
    $('#' + $('#col').val()).html($('#newTitle').val());
    $.nyroModalRemove();
    return false;
});


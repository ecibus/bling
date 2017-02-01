$(function() {
    $('#data').hide();
});

$(function() {
    $('#ddlLoanCode').change(function(a) {
        var code = $('#ddlLoanCode').val();
        var display = $(':radio:checked').val();
        
        $('#data').fadeOut2().hide();
        $('#programcode').load('AjaxShowHideProgramCode.aspx',
            { type: "getprogrambyprogramcode", code: code, display: display },
            function(data) {
                SetTableBehavior();
            }
        );
    });
});

function SetTableBehavior() {
    $('#data').fadeIn2();
    $(':checkbox').each(function(a, b) {
        $(b).bind('click', CheckBoxClicked);
    });

}

function CheckBoxClicked() {
    var id = $(this).attr('id');
    var hide = $(this).attr('checked') ? 1 : 0;
    var code = $('#ddlLoanCode').val();
    
    $.post('AjaxShowHideProgramCode.aspx',
        { type: "updateprogramcode", code: code, id: id, hide: hide }
    );
    
    
    $('<td class=blinkingMessage>Updated</td>')
        .insertAfter($(this).parent())
        .effect("pulsate", { times:2 })
        .fadeIn2('slow')
        .animate({opacity: 1.0}, 3000)
        .fadeOut2('slow', function() {
          $(this).remove();
        });
        
}

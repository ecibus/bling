$(function() {
    $('#txtApplicationNumber').focus();
});

$(function() {
    $('#btnLoad').click(function() {
        $('#TrustAccount').fadeOut2().hide();
        $('#TrustAccount').load('AjaxTrustAccount.aspx', { type: 'gettrustaccountbyappnumber', appnumber: $('#txtApplicationNumber').val() },
            function(data) { SetTableBehavior(data); }
        )
    });
});

$(function () {
    $('#txtApplicationNumber').keypress(function (e) {        
        if ((e.which && e.which == 13) || (e.keyCode && e.keyCode == 13)) {  
            $('#btnLoad').click();  
            return false;  
        } else {  
            return true;  
        } 
    });
});

function SetTableBehavior(data) {    
    $('#TrustAccount').html(data).fadeIn2();
    $('table').addClass('t1');
    $('table tr:first').addClass('yellow');
    $('table tr:last').addClass('yellow');
    $('table tr td img').css('cursor', 'pointer').click(function() {
        var id = '#doc' + $(this).attr('id');

        $.get('AjaxTrustAccount.aspx', { type: 'deleteentrybyid', id: $(this).attr('id') },
            function() {
                $(id).find("td").fadeOut("slow");
            }
        );

        var total = parseFloat($('#total').html().replace(',', '').substr(1));
        var amount = parseFloat($(id).find("td:nth-child(4)").html().replace(',', '').substr(1));
        $('#total').text('$ ' + addCommas((total - amount).toFixed(2).toString()));
    });

    $('table tr td:nth-child(4)').css('text-align', 'right');
}

function addCommas(nStr) {
    nStr += '';
    x = nStr.split('.');
    x1 = x[0];
    x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    }
    return x1 + x2;
}

$(function() {
    SetTableBehavior();
});

function SetTableBehavior() {
    $('table').addClass('t1');
    $('table tr:first').addClass('yellow');
}

$(function() {
    $('input[type=text]').each(function() {
        $(this).datepicker();
    });
});

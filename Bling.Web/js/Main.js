$(function() {

});

$(function() {
    //$('#content').height(600);
});

$(function() {
    $('ul#main li img').addClass('seeThrough');
    $('ul#main li').addClass('seeThroughText');
    $('ul#submain li img').addClass('seeThrough');
    $('ul#submain li').addClass('seeThroughText');
});

$(function() {
    $.each(allowed, function(a, b) {
        var link = $('#App' + b).attr('href') + '?a=' + b + '&r=' + Math.floor(Math.random() * 1001);
        $('#App' + b + ' img').removeClass('seeThrough');
        $('#App' + b).removeClass('seeThroughText');
        $('#App' + b).click(function() {
            location.href = link;
        });
        $('#App' + b).hover(
            function(event) { $(this).addClass('selected'); },
            function(event) { $(this).removeClass('selected'); }
        );
        
    });
});

$(function() {
    $('#main li:not(li.seeThroughText)').click(function() {
    });
});


$(function() {
   
});
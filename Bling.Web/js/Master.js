var GEM = {}

GEM.DisplayMessage = {
    
};

var DisplayMessage = Base.extend({
    constructor: function(text) {
        this.Text = text;
        $("#message")
            .html('')
            .hide()
            .fadeOut2('slow')
            .removeClass("error ui-state-error message ui-state-highlight ui-corner-all")
        ;

        this.Show = function(c) {
            $('#message')
                .addClass(c)
                .html(this.Text)
                .fadeIn2('slow')
                ;
        };
    },

    AsInfo: function() {
        this.Show('message ui-state-highlight ui-corner-all');
    },

    AsWarning: function() {
        this.Show('error ui-state-error ui-corner-all');
    },

    Clear: function() {
        $("#message")
            .html('')
            .hide();
    }
});


$(function() {
    if ($("#message").html() != "") {
        $("#message").addClass("message ui-state-highlight ui-corner-all");
    }
    if ($("#error").html() != "") {
        $("#error").addClass("error ui-state-error ui-corner-all");
    }
});

jQuery.fn.fadeIn2 = function(speed, callback) {
    return this.animate({ opacity: 'show' }, speed, function() {
        if (jQuery.browser.msie)
            this.style.removeAttribute('filter');
        if (jQuery.isFunction(callback))
            callback();
    });
};

jQuery.fn.fadeOut2 = function(speed, callback) {
    return this.animate({ opacity: 'hide' }, speed, function() {
        if (jQuery.browser.msie)
            this.style.removeAttribute('filter');
        if (jQuery.isFunction(callback))
            callback();
    });
};

jQuery.fn.fadeTo2 = function(speed, to, callback) {
    return this.animate({ opacity: to }, speed, function() {
        if (to == 1 && jQuery.browser.msie)
            this.style.removeAttribute('filter');
        if (jQuery.isFunction(callback))
            callback();
    });
};


(function($) {
    $.fn.fadeIn3 = function(speed, callback) {
        $(this).fadeIn(speed, function() {
            if (jQuery.browser.msie)
                $(this).get(0).style.removeAttribute('filter');
            if (callback != undefined)
                callback();
        });
    };
    $.fn.fadeOut3 = function(speed, callback) {
        $(this).fadeOut(speed, function() {
            if (jQuery.browser.msie)
                $(this).get(0).style.removeAttribute('filter');
            if (callback != undefined)
                callback();
        });
    };
})(jQuery);

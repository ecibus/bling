$(function () {
    //ClearTextBox();
    Load();
});

$(function () {
    $('input[type="text"]').keydown(function (event) {
        if (event.keyCode == 13) {
            event.preventDefault();
            return false;
        }
    });
});

function Load() {
    $.post('AjaxTurningPoint.aspx',
        {
            type: 'load'
        },
        LoadTurningPointCallback
    );
}

function LoadTurningPointCallback(o) {
    $('#tpuser').html(o);
}

$(function () {
    $('#btnSearch').live('click', function (e) {
        e.preventDefault();
        Search($('#txtSearch').val());
    });
});

function Search(crit) {
    if (!crit) {
        new DisplayMessage('Search string is required').AsWarning();
        return;
    }

    new DisplayMessage("").Clear();

    $.post('AjaxTurningPoint.aspx',
            {
                type: 'search',
                crit: crit
            },
            SearchUserCallback
        );
}

function SearchUserCallback(o) {
    if (o.length < 5) {
        new DisplayMessage('No user found with Lastname or Username of "' + $('#txtSearch').val() + '"').AsInfo();
        return;
    }
    $('#searchresult').html(o);
}

$(function () {
    $('.add').live('click', function (e) {
        var username = $(this).attr('id');
        $.post('AjaxTurningPoint.aspx',
            {
                type: 'add',
                username: username
            },
            AddUserCallback
        );
    });
});

function AddUserCallback () {
    new DisplayMessage('Done adding user.').AsInfo();
    Load();
}

$(function () {
    $('.remove').live('click', function (e) {
        var username = $(this).attr('id');
        console.log('Removing ' + username);
        $.post('AjaxTurningPoint.aspx',
            {
                type: 'remove',
                username: username
            },
            RemoveUserCallback
        );
    });
});

function RemoveUserCallback() {
    new DisplayMessage('Done removing user.').AsInfo();
    Load();
}
$(function () {
    LoadAwaitingApproval();
});

function LoadAwaitingApproval() {

    $.post('AjaxCommissionAnalysis.aspx',
        {
            type: 'loadawaitingapproval',
            r: Math.floor(Math.random() * 1001)
        },
        LoadAwaitingApprovalCallback
    );
}


function LoadAwaitingApprovalCallback(o) {
    $('#list').html(o);
}

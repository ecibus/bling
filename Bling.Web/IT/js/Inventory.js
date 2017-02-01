function DropDownChange() {
    var branch = $('#ddUser').val().split("|")[1];
    $("#branch").text(branch);
}

$(function() {
    $('#ddUser').bind('change', DropDownChange);       
});

$(function() {
    $('#txtIssuedOn').each(function() {
        $(this).datepicker();
    });
});

function SaveData() {
    var branch = $('#ddUser').val().split("|")[1];
    var issueTo = $('#ddUser').val().split("|")[0];
    $.post('AjaxInventory.aspx',
        {
            type: 'add',
            issuedon: $('#txtIssuedOn').val(),
            make: $('#txtMake').val(),
            model: $('#txtModel').val(),
            serialnumber: $('#txtSerialNumber').val(),
            quantity: $('#txtQuantity').val(),
            issuedto: issueTo,
            branchname: branch
        },
        function(data, result) {
            if (data != "") {
                new DisplayMessage(data).AsWarning();
            } else {
                new DisplayMessage('Record added.').AsInfo();
            }
        }
        );
}

function DisplayData(page) {
    $("#data").load('AjaxInventory.aspx',
        {
            type: 'display',
            page: page,
            r: Math.floor(Math.random() * 1001)
        },
        function(data) { SetTableBehavior(data); }
    );
}

function LoadInventoryUser() {
    $("#Users").load('AjaxInventory.aspx',
        {
            type: 'getusers',
            r: Math.floor(Math.random() * 1001)
        },
        function(data) { $('#ddAssignTo').bind('change', FilterData); }
    );
}

function LoadInventoryBranches() {
        $("#Branches").load('AjaxInventory.aspx',
        {
            type: 'getbranches',
            r: Math.floor(Math.random() * 1001)
        },
        function(data) { $('#ddBranches').bind('change', FilterData); }
    );
}

$(function() {
    $('#btnAdd').click(function() {
        SaveData();
        DisplayData(1);
        LoadInventoryUser();
        LoadInventoryBranches();        
    });
});

$(function() {
    DisplayData(1);
    LoadInventoryUser();
    LoadInventoryBranches();    
});

function SetTableBehavior(data) {    
    $('table').addClass('t1');
    $('table tr:first').addClass('yellow');
    $('table tr td img').css('cursor', 'pointer').click(function() {
        var id = '#inv_' + $(this).attr('id');        
        $.post('AjaxInventory.aspx', { type: 'delete', idtodelete: $(this).attr('id') },
            function() {
                $(id).find("td").fadeOut("slow");
            }
        );

    });
}

$(function() {
    Pager.CurrentPage = 1;
});

var Pager = new Object();

function Page(page) {
    var assignTo = $('#ddAssignTo').val();
    var branch = $('#ddBranches').val();
    var search = $('#txtSearch').val();

    if (search != "")
        DisplaySearchData(page);
    else if (assignTo != "" | branch != "")
        DisplayFilteredData(page);
    else
        DisplayData(page);
}

function FilterData() {
    
    var assignTo = $('#ddAssignTo').val();
    var branch = $('#ddBranches').val();
    DisplayFilteredData(1);
}

function DisplayFilteredData(page) {    
    $("#data").load('AjaxInventory.aspx',
        {
            type: 'display', //'displayfiltereddata',
            page: page,
            //assignto: $('#ddAssignTo').val(),
            //branch: $('#ddBranches').val(),
            r: Math.floor(Math.random() * 1001)
        },
        function(data) { SetTableBehavior(data); }
    );
    }

function DisplaySearchData(page) {
    $("#data").load('AjaxInventory.aspx',
    {
        type: 'search',
        page: page,
        searchstring: $('#txtSearch').val(),
        r: Math.floor(Math.random() * 1001)
    },
    function (data) { SetTableBehavior(data); }
);
}

$(function () {
    $('#btnSearch').click(function () {
        $("#data").load('AjaxInventory.aspx',
        {
            type: 'search',
            page: 1,
            searchstring: $('#txtSearch').val(),
            r: Math.floor(Math.random() * 1001)
        },
        function (data) { SetTableBehavior(data); }
    );
    });
});
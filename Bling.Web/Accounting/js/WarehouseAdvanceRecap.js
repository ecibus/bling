$(function () {
    (function () {
        var 
        init = function () {
            console.log('init');
            wireEvents();
        },

        wireEvents = function () {
            console.log('wireEvents');
            btnGenerateClick();
        },

        generateCsv = function () {
            console.log('in generateCsv');
            $.post('AjaxWarehouseAdvanceRecap.aspx',
                    {
                        r: Math.floor(Math.random() * 1001)
                    },
                generateCallback
            );
        },

        generateCallback = function (o) {
            $('#csvLink').html(o)
            .effect("pulsate", { times: 1 });
        },

        btnGenerateClick = function () {
            console.log('btnGenerateClick');
            $('#btnGenerate').bind('click', function () {
                //btnGenerate = $(this).busy();
                generateCsv();
            });
        }
        ;

        init();
    })();
});
//$(function () {
//    
//});


//function GenerateCSV() {
//    var from = $('#txtFrom').val();
//    var to = $('#txtTo').val();
//    var reportType = $('#optReportType').val();

//    
//    //$('#xmlLink').html('');
//    $.post('AjaxWarehouseAdvanceRecap.aspx',
//            {
//                type: 'generate',                
//                r: Math.floor(Math.random() * 1001)
//            },
//        GenerateCallback
//    );
//}

//function GenerateCallback(o) {

//    $('#csvLink').html(o)
//    .effect("pulsate", { times: 1 });
//}
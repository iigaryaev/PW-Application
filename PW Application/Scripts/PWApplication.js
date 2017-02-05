function UpdateBalance()
{
    $.ajax({
        url: updateBalanceUrl,
        method : "POST",
        success: function(data)
        {
            $('span#balance').text(data.Balance);
            SwitchToOnline();
        },
        async: false,
        error:
            function()
            {
                SwitchToOffline();
            }
    })
}

function UpdateHistory() {
    $.ajax({
        url: updateHistoryUrl,
        method: "POST",
        async: false,
        success: function (data) {
            $('div#history').html(data);
            SwitchToOnline();
        },
        error:
            function () {
                SwitchToOffline();
            }
    })
}

function SwitchToOnline()
{
    $('span#isOnline').text('Online');
    $('span#isOnline').removeClass('isOffline');
    $('span#isOnline').addClass('isOnline');
}

function SwitchToOffline() {
    $('span#isOnline').text('Offline');
    $('span#isOnline').addClass('isOffline');
    $('span#isOnline').removeClass('isOnline');
}

function UpdateAccountState()
{
    UpdateBalance();
    UpdateHistory();
}

function RepeatPayment(historyId)
{
    var ammount = $('.historyItem#' + historyId).find('#ammount').text();
    var userName = $('.historyItem#' + historyId).find('#userName').text();
    $('input#Ammount').val(ammount);
    $('input#CorrespondentName').val(userName);
}
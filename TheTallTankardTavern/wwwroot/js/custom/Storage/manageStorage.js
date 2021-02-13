function depositItem(cid, inventoryID) {
    event.stopPropagation();
    console.log("Deposit item button pressed");
    $.ajax({
        url: '/Storage/DepositItem',
        data: { cid: cid, inventoryID: inventoryID },
        dataType: 'html',
        success: function (response) { refreshStorage(response); },
        error: function (xhr, status, error) { logErrorInConsolve(xhr, status, error); }
    })
}

function withdrawItem(cid, inventoryID) {
    event.stopPropagation();
    console.log("Withdraw item button pressed");
    $.ajax({
        url: '/Storage/WithdrawItem',
        data: { cid: cid, inventoryID: inventoryID },
        dataType: 'html',
        success: function (response) { refreshStorage(response); },
        error: function (xhr, status, error) { logErrorInConsolve(xhr, status, error); }
    })
}

function refreshStorage(response) {
    $('#storage-item-management').html(response);
}

function logErrorInConsolve(xhr, status, error) {
    console.log('xhr: ' + JSON.stringify(xhr));
    console.log('status: ' + status);
    console.log('error: ' + error);
}
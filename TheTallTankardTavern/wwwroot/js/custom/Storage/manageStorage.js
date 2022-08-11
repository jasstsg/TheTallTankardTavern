function depositItem(cid, inventoryID) {
    event.stopPropagation();
    console.log("Deposit item button pressed");
    $.ajax({
        url: '/Storage/DepositItem',
        data: { cid: cid, inventoryID: inventoryID },
        dataType: 'html',
        success: function (response) { refreshStorageItems(response); },
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
        success: function (response) { refreshStorageItems(response); },
        error: function (xhr, status, error) { logErrorInConsolve(xhr, status, error); }
    })
}

function removeItem(inventoryID) {
    event.stopPropagation();
    console.log("Remove item button pressed");
    $.ajax({
        url: '/Storage/RemoveItem',
        data: { inventoryID: inventoryID },
        dataType: 'html',
        success: function (response) { refreshStorageItems(response); },
        error: function (xhr, status, error) { logErrorInConsolve(xhr, status, error); }
    })
}

function refreshStorageItems(response) {
    $('#storage-item-management').html(response);
}

function logErrorInConsolve(xhr, status, error) {
    console.log('xhr: ' + JSON.stringify(xhr));
    console.log('status: ' + status);
    console.log('error: ' + error);
}
function unequipArmour(cid, inventoryID) {
    event.stopPropagation();
    console.log("UnequipArmour button pressed");
    $.ajax({
        url: '/Character/UnequipArmour',
        data: { cid: cid, inventoryID: inventoryID },
        dataType: 'html',
        success: function (response) { refreshCharacterItems(response); }
    })
}

function unequipShield(cid, inventoryID) {
    event.stopPropagation();
    console.log("UnequipShield button pressed");
    $.ajax({
        url: '/Character/UnequipShield',
        data: { cid: cid, inventoryID: inventoryID },
        dataType: 'html',
        success: function (response) { refreshCharacterItems(response); }
    })
}

function unequipTwoHand(cid, inventoryID) {
    event.stopPropagation();
    console.log("UnequipTwoHand button pressed");
    $.ajax({
        url: '/Character/UnequipTwoHand',
        data: { cid: cid, inventoryID: inventoryID },
        dataType: 'html',
        success: function (response) { refreshCharacterItems(response); }
    })
}

function unequipMainHand(cid, inventoryID) {
    event.stopPropagation();
    console.log("UnequipMainHand button pressed");
    $.ajax({
        url: '/Character/UnequipMainHand',
        data: { cid: cid, inventoryID: inventoryID },
        dataType: 'html',
        success: function (response) { refreshCharacterItems(response); }
    })
}

function unequipOffHand(cid, inventoryID) {
    event.stopPropagation();
    console.log("UnequipOffHand button pressed");
    $.ajax({
        url: '/Character/UnequipOffHand',
        data: { cid: cid, inventoryID: inventoryID },
        dataType: 'html',
        success: function (response) { refreshCharacterItems(response); }
    })
}

function equipItem(cid, inventoryID) {
    console.log(inventoryID);
    event.stopPropagation();
    console.log("EquipItem button pressed");
    $.ajax({
        url: '/Character/EquipItem',
        data: { cid: cid, inventoryID: inventoryID },
        dataType: 'html',
        success: function (response) { refreshCharacterItems(response); }
    })
}

function removeItemFromEquipment(cid, inventoryID) {
    event.stopPropagation();
    console.log("Remove Equipment button pressed");
    $.ajax({
        url: '/Character/RemoveItemFromEquipment',
        data: { cid: cid, inventoryID: inventoryID },
        dataType: 'html',
        success: function (response) { refreshCharacterItems(response); }
    })
}

function removeItemFromInventory(cid, inventoryID) {
    event.stopPropagation();
    console.log("Remove Inventory button pressed");
    $.ajax({
        url: '/Character/RemoveItemFromInventory',
        data: { cid: cid, inventoryID: inventoryID },
        dataType: 'html',
        success: function (response) { refreshCharacterItems(response); }
    })
}

function refreshCharacterItems(response) {
    $('#character-items').html(response);
}

/* 
 * cid : Character ID
 * coin: Copper, Silver, Gold, or Platinum
 * thisElement: this
 */
/*
function quickSaveCoins(cid, coin, thisElement) {
    var data = {
        cid: cid,
        coin: coin,
        pieces: $(thisElement).val()
    };
    console.log(data);
    $.post("/Character/QuickSaveCoins", data, function (response) {
        showAutoSaveMessage(response.success);
        console.log("Quick save coins request completed with success status: " + response.success);
    });
};
*/

function updateCoinPurse(cid, coin, thisElement) {
    event.stopPropagation();
    $.ajax({
        url: '/Character/UpdateCoinPurse',
        data: {
            cid: cid,
            coin: coin,
            pieces: $(thisElement).val()
        },
        dataType: 'html',
        success: function (response) {
            $('#character-items-coins').html(response);
        }
    })
};
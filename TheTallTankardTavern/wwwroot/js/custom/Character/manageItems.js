function unequipItem(cid, iid) {
    event.stopPropagation();
    console.log("UnequipItem button pressed");
    $.ajax({
        url: '/Character/UnequipItem',
        data: { cid: cid, iid: iid },
        dataType: 'html',
        success: function (response) {
            $('#character-items').html(response);
        }
    })
}

function equipItem(cid, iid) {
    event.stopPropagation();
    console.log("EquipItem button pressed");
    $.ajax({
        url: '/Character/EquipItem',
        data: { cid: cid, iid: iid },
        dataType: 'html',
        success: function (response) {
            $('#character-items').html(response);
        }
    })
}

function removeItemFromEquipment(cid, iid) {
    event.stopPropagation();
    console.log("Remove Equipment button pressed");
    $.ajax({
        url: '/Character/RemoveItemFromEquipment',
        data: { cid: cid, iid: iid },
        dataType: 'html',
        success: function (response) {
            $('#character-items').html(response);
        }
    })
}

function removeItemFromInventory(cid, iid) {
    event.stopPropagation();
    console.log("Remove Inventory button pressed");
    $.ajax({
        url: '/Character/RemoveItemFromInventory',
        data: { cid: cid, iid: iid },
        dataType: 'html',
        success: function (response) {
            $('#character-items').html(response);
        }
    })
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
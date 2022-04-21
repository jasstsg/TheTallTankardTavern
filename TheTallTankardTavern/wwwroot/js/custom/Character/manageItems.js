function unequipArmour(cid, inventoryID) {
    event.stopPropagation();
    console.log("UnequipArmour button pressed");
    $.ajax({
        url: '/Character/UnequipArmour',
        data: { cid: cid, inventoryID: inventoryID },
        dataType: 'html',
        success: function (response) { refreshCharacter(response); }
    })
}

function unequipSpellCastingFocus(cid, inventoryID) {
    event.stopPropagation();
    console.log("UnequipSpellCastingFocus button pressed");
    $.ajax({
        url: '/Character/UnequipSpellCastingFocus',
        data: { cid: cid, inventoryID: inventoryID },
        dataType: 'html',
        success: function (response) { refreshCharacter(response); }
    })
}

function unequipShield(cid, inventoryID) {
    event.stopPropagation();
    console.log("UnequipShield button pressed");
    $.ajax({
        url: '/Character/UnequipShield',
        data: { cid: cid, inventoryID: inventoryID },
        dataType: 'html',
        success: function (response) { refreshCharacter(response); }
    })
}

function unequipTwoHand(cid, inventoryID) {
    event.stopPropagation();
    console.log("UnequipTwoHand button pressed");
    $.ajax({
        url: '/Character/UnequipTwoHand',
        data: { cid: cid, inventoryID: inventoryID },
        dataType: 'html',
        success: function (response) { refreshCharacter(response); }
    })
}

function unequipMainHand(cid, inventoryID) {
    event.stopPropagation();
    console.log("UnequipMainHand button pressed");
    $.ajax({
        url: '/Character/UnequipMainHand',
        data: { cid: cid, inventoryID: inventoryID },
        dataType: 'html',
        success: function (response) { refreshCharacter(response); }
    })
}

function unequipOffHand(cid, inventoryID) {
    event.stopPropagation();
    console.log("UnequipOffHand button pressed");
    $.ajax({
        url: '/Character/UnequipOffHand',
        data: { cid: cid, inventoryID: inventoryID },
        dataType: 'html',
        success: function (response) { refreshCharacter(response); }
    })
}

function unequipAttunableItem(cid, inventoryID) {
    event.stopPropagation();
    console.log("UnequipAttunableItem button pressed");
    $.ajax({
        url: '/Character/UnequipAttunableItem',
        data: { cid: cid, inventoryID: inventoryID },
        dataType: 'html',
        success: function (response) { refreshCharacter(response); }
    })
}

function equipItem(cid, inventoryID) {
    event.stopPropagation();
    console.log("EquipItem button pressed");
    $.ajax({
        url: '/Character/EquipItem',
        data: { cid: cid, inventoryID: inventoryID },
        dataType: 'html',
        success: function (response) { refreshCharacter(response); }
    })
}

function removeItemFromEquipment(cid, inventoryID) {
    event.stopPropagation();
    console.log("Remove Equipment button pressed");
    $.ajax({
        url: '/Character/RemoveItemFromEquipment',
        data: { cid: cid, inventoryID: inventoryID },
        dataType: 'html',
        success: function (response) { refreshCharacter(response); }
    })
}

function removeItemFromInventory(cid, inventoryID) {
    event.stopPropagation();
    console.log("Remove Inventory button pressed");
    $.ajax({
        url: '/Character/RemoveItemFromInventory',
        data: { cid: cid, inventoryID: inventoryID },
        dataType: 'html',
        success: function (response) { refreshCharacter(response); }
    })
}

function giveItem(cid, inventoryId) {
    event.stopPropagation();
    console.log("Give Item button pressed");
    location.href = '/Character/GiveItem?cid=' + cid + '&inventoryId=' + inventoryId;
}


function refreshCharacter(response) {
    //Refresh the character's details (this makes the first tab active again)
    $('#character-details').html(response);

    //Make the first tab inactive
    $('#character-stats-and-info').removeClass('active');
    $('#character-stats-and-info-tab').removeClass('active');

    //Make the item tab active
    $('#character-items').addClass('active');
    $('#character-items-tab').addClass('active');
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
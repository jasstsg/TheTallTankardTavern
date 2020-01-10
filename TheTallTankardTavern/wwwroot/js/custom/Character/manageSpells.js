function quickSaveSpellSlots(cid, spellSlotLevel, thisElement) {
    var data = {
        ID: cid,
        SpellSlotLevel: spellSlotLevel,
        SpellSlotsRemaining: $(thisElement).val()
    };
    $.post("/Character/QuickSaveSpellSlots", data, function (response) {
        showAutoSaveMessage(response.success);
        console.log("Quick save spell slots request completed with success status: " + response.success);
    });
};

function removeSpell(cid, sid, thisElement) {
    event.stopPropagation();
    var data = {
        cid: cid,
        sid: sid
    };
    $.post("/Character/RemoveSpell", data, function (response) {
        showAutoSaveMessage(response.success);
        console.log("Quick save forget spell request completed with success status: " + response.success);

        if (response.success == true) {
            var tableRow = thisElement.parentElement.parentElement; //Get the table row
            tableRow.parentElement.removeChild(tableRow);           //Remove the table row
        }
    });
};

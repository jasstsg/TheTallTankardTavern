function quickSaveMemberInitiative(id, cid, thisElement) {
    var data = {
        id: id,
        cid: cid,
        initiative: $(thisElement).val()
    };
    $.post("/Party/QuickSaveMemberInitiative", data, function (response) {
        showAutoSaveMessage(response.success);
        console.log("Quick save member initiative request completed with success status: " + response.success);
    });
};

function quickSaveDate(id, thisElement) {
    var data = {
        id: id,
        date: $(thisElement).val()
    };
    $.post("/Party/QuickSaveDate", data, function (response) {
        showAutoSaveMessage(response.success);
        console.log("Quick save date request completed with success status: " + response.success);
    });
};

function reloadPartyTable(id) {
    console.log('reloading party table...')
    $.ajax({
        url: '/Party/ReloadPartyTable/' + id, 
        success: function (data) {
            console.log('reload succeeded!');
            $('#party-table').html(data);
        },
        error: function () {
            console.log('reload failed!');
        }
    })
}

function quickSavePartyMemberHitPoints(id, cid, thisElement) {
    //Save hit points
    var data = {
        id: id, 
        cid: cid,
        hitPointsRemaining: $(thisElement).val()
    };
    $.post("/Character/QuickSaveHitPoints", data, function (response) {
        showAutoSaveMessage(response.success);
        console.log("Quick save hit points request completed with success status: " + response.success);

        if (response.success) {
            //Reload table
            reloadPartyTable(id);
        }
    });
};
function quickSavePartyMemberConditions(id, cid, thisElement) {
    var data = {
        id: id, 
        cid: cid,
        conditions: $(thisElement).val()
    };
    $.post("/Party/QuickSaveConditions", data, function (response) {
        showAutoSaveMessage(response.success);
        console.log("Quick save conditions request completed with success status: " + response.success);

        if (response.success) {
            //Reload table
            reloadPartyTable(id);
        }
    });
};
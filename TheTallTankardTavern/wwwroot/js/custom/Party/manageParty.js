function quickSaveMemberInitiative(cid, thisElement) {
    var data = {
        cid: cid,
        initiative: $(thisElement).val()
    };
    $.post("/Party/QuickSaveMemberInitiative", data, function (response) {
        showAutoSaveMessage(response.success);
        console.log("Quick save member initiative request completed with success status: " + response.success);
    });
};

function quickSaveDate(thisElement) {
    var data = {
        date: $(thisElement).val()
    };
    $.post("/Party/QuickSaveDate", data, function (response) {
        showAutoSaveMessage(response.success);
        console.log("Quick save date request completed with success status: " + response.success);
    });
};

function reloadPartyTable() {
    console.log('reloading party table...')
    $.ajax({
        url: '/Party/ReloadPartyTable', 
        success: function (data) {
            console.log('reload succeeded!');
            $('#party-table').html(data);
        },
        error: function () {
            console.log('reload failed!');
        }
    })
}

function quickSavePartyMemberHitPoints(cid, thisElement) {
    //Save hit points
    var data = {
        cid: cid,
        hitPointsRemaining: $(thisElement).val()
    };
    $.post("/Character/QuickSaveHitPoints", data, function (response) {
        showAutoSaveMessage(response.success);
        console.log("Quick save hit points request completed with success status: " + response.success);

        if (response.success) {
            //Reload table
            reloadPartyTable();
        }
    });
};
function quickSavePartyMemberConditions(cid, thisElement) {
    var data = {
        cid: cid,
        conditions: $(thisElement).val()
    };
    $.post("/Party/QuickSaveConditions", data, function (response) {
        showAutoSaveMessage(response.success);
        console.log("Quick save conditions request completed with success status: " + response.success);

        if (response.success) {
            //Reload table
            reloadPartyTable();
        }
    });
};
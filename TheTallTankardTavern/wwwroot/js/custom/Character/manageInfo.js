function quickSaveHitPoints(cid, thisElement) {
    var data = {
        cid: cid,
        hitPointsRemaining: $(thisElement).val()
    };
    $.post("/Character/QuickSaveHitPoints", data, function (response) {
        showAutoSaveMessage(response.success);
        console.log("Quick save hit points request completed with success status: " + response.success);
    });
};

function quickSaveHitDice(cid, thisElement) {
    var data = {
        cid: cid,
        hitDiceRemaining: $(thisElement).val()
    };
    $.post("/Character/QuickSaveHitDice", data, function (response) {
        showAutoSaveMessage(response.success);
        console.log("Quick save hit dice request completed with success status: " + response.success);
    });
};

function quickSaveTempHitPoints(cid, thisElement) {
    var data = {
        cid: cid,
        tempHitPoints: $(thisElement).val()
    };
    $.post("/Character/QuickSaveTemporaryHitPoints", data, function (response) {
        showAutoSaveMessage(response.success);
        console.log("Quick save temporary hit points request completed with success status: " + response.success);
    });
};

function quickSaveKiPoints(cid, thisElement) {
    var data = {
        cid: cid,
        kiPoints: $(thisElement).val()
    };
    $.post("/Character/QuickSaveKiPoints", data, function (response) {
        showAutoSaveMessage(response.success);
        console.log("Quick save ki points request completed with success status: " + response.success);
    });
};

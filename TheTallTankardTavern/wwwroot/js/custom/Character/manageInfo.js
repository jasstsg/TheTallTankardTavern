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

function quickSaveSorceryPoints(cid, thisElement) {
    var data = {
        cid: cid,
        sorceryPoints: $(thisElement).val()
    };
    $.post("/Character/QuickSaveSorceryPoints", data, function (response) {
        showAutoSaveMessage(response.success);
        console.log("Quick save sorcery points request completed with success status: " + response.success);
    });
};

function quickSaveWildShapes(cid, thisElement) {
    var data = {
        cid: cid,
        wildShapes: $(thisElement).val()
    };
    $.post("/Character/QuickSaveWildShapes", data, function (response) {
        showAutoSaveMessage(response.success);
        console.log(response.success);
        console.log("Quick save wild shapes request completed with success status: " + response.success);
    });
};

function updateCoinPurse(cid, coin, thisElement) {
    console.log('Update coin purse');
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
            $('#character-coins').html(response);
        }
    })
};
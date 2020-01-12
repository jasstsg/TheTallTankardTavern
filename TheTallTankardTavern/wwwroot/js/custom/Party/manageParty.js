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
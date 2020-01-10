
function removeFeature(cid, fid, thisElement) {
    event.stopPropagation();
    var data = {
        cid: cid,
        fid: fid
    };
    $.post("/Character/RemoveFeature", data, function (response) {
        showAutoSaveMessage(response.success);
        console.log("Removing feature..." + response.success);

        if (response.success == true) {
            var tableRow = thisElement.parentElement.parentElement; //Get the table row
            tableRow.parentElement.removeChild(tableRow);           //Remove the table row
        }
    });
};

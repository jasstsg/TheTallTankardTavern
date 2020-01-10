


function quickSaveRemoveFeature(cid, featureName, thisElement) {
    event.stopPropagation();
    var data = {
        CharacterID: cid,
        FeatureName: featureName
    };
    $.post("/Character/QuickSaveRemoveFeature", data, function (response) {
        showAutoSaveMessage(response.success);
        console.log("Quick save remove feature request completed with success status: " + response.success);

        if (response.success == true) {
            var tableRow = thisElement.parentElement.parentElement; //Get the table row
            tableRow.parentElement.removeChild(tableRow);           //Remove the table row
        }
    });
};

//Used in the quicksave functions 
function showAutoSaveMessage(success) {
    var autoSaveMessage = $('#auto-save-message');
    autoSaveMessage.show();

    if (success) {
        autoSaveMessage.text('Auto saving changes...');
    }
    else {
        autoSaveMessage.text('Failed to auto save...');
    }

    autoSaveMessage.delay(1000).fadeOut();
}
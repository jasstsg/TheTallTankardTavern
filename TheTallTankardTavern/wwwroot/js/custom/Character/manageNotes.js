﻿function saveNotes(cid) {
    console.log("Saving notes...");
    $.ajax({
        url: '/Character/SaveNotes',
        data: {
            cid: cid,
            notes: $('#character-notes-textarea').val()
        },
        success: function (response) {
            var saveMessage = $('#save-message');
            saveMessage.show();
            saveMessage.text(response.success ? "Notes Saved!" : "Notes failed to save!");
            saveMessage.delay(1000).fadeOut();
        }
    })
}
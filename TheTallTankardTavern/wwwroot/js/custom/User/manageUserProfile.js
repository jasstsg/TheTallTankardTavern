function saveChanges(uid) {
    console.log("Saving user...");
    $.ajax({
        url: '/User/SaveChanges',
        data: {
            uid: uid,
            username: $('#Username').val(),
            message: $('#user-message').val()
        },
        success: function (response) {
            var saveMessage = $('#save-message');
            saveMessage.text(response.success ? "Saved changes!" : "Failed to save changes!");
            saveMessage.delay(2000).fadeOut();
        }
    })
}
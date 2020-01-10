function updateSubraceDropdown() {
    $.ajax({
        url: '/ConfigurationApi/GetSubraces',
        data: { selected: $('#character-race').val() },
        success: function (response) {
            $('#character-subrace').empty();
            response.forEach(function (item, index) {
                $('#character-subrace').append('<option>' + item.text + '</option>');
            })
        }
    })
};

function updateSubclassDropdown() {
    $.ajax({
        url: '/ConfigurationApi/GetSubclasses',
        data: { selected: $('#character-class').val() },
        success: function (response) {
            $('#character-subclass').empty();
            response.forEach(function (item, index) {
                $('#character-subclass').append('<option>' + item.text + '</option>');
            })
        }
    })
};
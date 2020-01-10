$('#popup-modal').on('show.bs.modal', function (event) {
    console.log('Opening popup modal');

    // Button that triggered the modal
    var button = $(event.relatedTarget);

    // Extract info from data-* attributes
    var title = button.data('title')
    var desc = button.data('desc')
    var higherLevel = button.data('higher-level')

    // Set the data-* attributes
    var modal = $(this)
    modal.find('.modal-title').text(title)
    modal.find('.modal-desc').text(desc)

    //Used for Spells only
    _hideIfEmpty(higherLevel, $('.modal-higher-level'));
})

function _hideIfEmpty(value, node) {
    if (value) {
        node.show();
        node.text(value);
    }
    else {
        node.hide();
    }
}
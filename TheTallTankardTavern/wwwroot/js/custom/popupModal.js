$('#popup-modal').on('show.bs.modal', function (event) {
    console.log('Opening popup modal');

    // Button that triggered the modal
    var button = $(event.relatedTarget);

    // Extract info from data-* attributes
    var title = button.data('title')
    var desc = button.data('desc')
    var higherLevel = button.data('higher-level')
    var equipment = button.data('equipment')

    // Set the data-* attributes
    var modal = $(this)
    modal.find('.modal-title').text(title)
    modal.find('.modal-desc').text(desc)

    _hideIfEmpty(higherLevel, $('.modal-higher-level'));
    _hideIfEmpty(equipment, $('.modal-equipment'));
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
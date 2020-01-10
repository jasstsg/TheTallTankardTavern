function fadeMessage(nodeId) {
    var fadeNode = $(nodeId);
    fadeNode.show();
    fadeNode.delay(1000).fadeOut();
}
$(document).ready(fadeMessage('#long-rest-message'));
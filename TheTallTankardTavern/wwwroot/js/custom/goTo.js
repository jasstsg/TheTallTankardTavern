function goTo(controllerName, actionName, id) {
    event.stopPropagation();
    var url = "/" + controllerName + "/" + actionName + "/" + id;
    console.log("Going to " + url);
    location.href = url;
}
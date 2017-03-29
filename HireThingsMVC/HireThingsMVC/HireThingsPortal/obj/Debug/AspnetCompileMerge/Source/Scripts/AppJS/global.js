function pressEnter(e, foo) {
    if (e.keyCode == 13) {
        foo;
        return false;
    }
}

$(document).keypress(function (e) {
    if (e.which == 13) {
        //console.log("Enter Pressed");
    }
});
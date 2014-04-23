function readMessages() {
    $.ajax({
        url: "/User/MarkMessagesRead/",
        type: "POST",
        dataType: "text"
    })
    .success(function (data) {

    })
    .error(function () {

    });
}

$("#suggest-button").click(function (e) {

    var moviename = $("#suggestion-name").val();
    var info = $("#suggestion-info").val();

    $.ajax({
        url: "/User/Suggest",
        type: "POST",
        data: AddAntiForgeryToken({
            moviename: moviename,
            info: info
        }),
        dataType: "text"
    })
    .success(function (data) {
        var obj = $.parseJSON(data);
        if (obj.result == "ok") {
            //alert("ok");
            toastr.success(obj.message);
        }
        else {
            //alert("fail");
            toastr.error(obj.message);
        }
    })
    .error(function () {
        //alert("die");
        toastr.error("Could not connect! Check your connection.");
    });

    e.preventDefault();
    $("#suggestModal").foundation('reveal', 'close');
});

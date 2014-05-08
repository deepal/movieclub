function deletemovie(movieid) {
    $.ajax({
        url: "/Admin/DeleteMovie/?movieid=" + movieid,
        type: "POST",
        data: AddAntiForgeryToken({}),
        dataType: "text"
    })
    .success(function (data) {
        var obj = $.parseJSON(data);
        if (obj.result == "ok") {
            toastr.success(obj.message)
            $.ajax({
                url: "/Admin/ManageMovies/?q=" + $("#search-movie-admin").val(),
                type: "GET"
            })
            .success(function (data) {
                $("#movies-list").html(data);
            })
            .error(function () {
                $("#movies-list").html("Error loading movies list");
            });
        }
        else if (obj.result == "error") {
            toastr.error("Could not delete movie!")
        }
    })
    .error(function () {
        toastr.error("Could not delete movie! Check your connection.")
    });
}



$("#search-movie-admin").keyup(function (event) {
    $.ajax({
        url: "/Admin/ManageMovies/?q=" + $("#search-movie-admin").val(),
        type: "GET"
    })
    .success(function (data) {
        $("#movies-list").html(data);
    })
    .error(function () {
        $("#movies-list").html("Error loading movies list. Try refreshing the page.");
    });;
});


function updateMoviesList() {
    $.ajax({
        url: "/Admin/ManageMovies",
        type: "GET"
    })
    .success(function (data) {
        $("#movies-list").html(data);
    })
    .error(function () {
        $("#movies-list").html("Error loading movies list");
    });
}

$(".featuredcheckbox").change(function (e) {

    $.ajax({
        url: "/Admin/FeatureMovie/?",
        type: "post",
        data: AddAntiForgeryToken({
            movieid: $(this).attr("data-movieid"),
            featured: $(this).is(":checked")
        }),
        dataType: "text"
    })
    .success(function (data) {
        var obj = $.parseJSON(data);
        if (obj.result == "ok") {
            toastr.success(obj.message);
        }
        else if (obj.result == "error") {
            $(this).removeAttr("checked");
            toastr.warning(obj.message);
        }
    })
    .error(function () {
        toastr.error("Error occured! Check your connection.");
    });
});
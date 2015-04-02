function updateReservations() {
    $.ajax({
        type: "GET",
        url: "/Admin/ReservedMovies"
    })
    .success(function (data) {
        $("#table-reservations tbody").html(data);
    })
    .error(function () {
        $("#table-reservations tbody").html("<tr><td colspan='5'>Error loading reservations. Try refreshing the page.</td></tr>");
    });
}

function updatePendingReturns() {
    $.ajax({
        url: "/Admin/PendingReturns",
        type: "GET"
    })
    .success(function (data) {
        $("#table-returns tbody").html(data);
    })
    .error(function () {
        $("#table-returns tbody").html("<tr><td colspan='5'><p>Error loading pending returns!</p></td></tr>");
    });
}

function updatePaymentDues() {
    $.ajax({
        url: "/Admin/GetPaymentDues",
        type: "GET"
    })
    .success(function (data) {
        $("#table-pending-payments tbody").html(data);
    })
    .error(function () {
        $("#table-pending-payments tbody").html("<tr><td colspan='3'><p>Error loading pending payments!</p></td></tr>");
    });
}

function updateUsers() {
    $.ajax({
        url: "/Admin/Users",
        type: "GET"
    })
    .success(function (data) {
        $("#table-users tbody").html(data);
    })
    .error(function () {
        $("#table-users tbody").html("<tr><td colspan='6'><p>Error loading users!</p></td></tr>");
    });
}

function updatePaymentHistory() {
    $.ajax({
        url: "/Admin/GetPaymentHistory",
        type: "GET"
    })
    .success(function (data) {
        $("#table-payments-history tbody").html(data);
    })
    .error(function () {
        $("#table-payments-history tbody").html("<tr><td colspan='4'><p>Error loading payment history!</p></td></tr>");
    });
}

function updateSuggestedMovies() {
    $.ajax({
        url: "/Admin/SuggestedMovies",
        type: "GET"
    })
    .success(function (data) {
        $("#table-suggestions tbody").html(data);
    })
    .error(function () {
        $("#table-suggestions tbody").html("<tr><td colspan='5'>Error loading suggestions!<td></tr>");
    });
}

function updatePendingReviews() {
    $.ajax({
        url: "/Admin/PendingReviews",
        type: "GET"
    })
    .success(function (data) {
        $("#table-pending-reviews tbody").html(data);
    })
    .error(function () {
        $("#table-suggestions tbody").html("<tr><td colspan='5'>Error loading pending reviews!<td></tr>");
    });
}



$(".sugchecked").click(function (e) {
    $.ajax({
        url: "/Admin/CheckSuggestion/?sid=" + $(this).attr("data-suggestion"),
        type: "POST",
        data: AddAntiForgeryToken({}),
        dataType: "text"
    })
    .success(function (data) {
        var obj = $.parseJSON(data);
        if (obj.result == "error") {
            toastr.error("Error occured! Try again later");
        }
        else if (obj.result == "ok") {
            updateSuggestedMovies();
        }
    })
    .error(function () {
        //alert("Error occured! Try again later");
        toastr.error("Could not connect! Check your connection.");
    });
});

function reloadUsersTable() {
    $.ajax({
        url: "/Admin/Users",
        type: "GET"
    })
    .success(function (data) {
        $("#table-users tbody").html(data);
    })
    .error(function () {
        $("#table-users tbody").html("<p>Error loading users!</p>");
    });
}

function removeadmin(userid) {
    $.ajax({
        url: "/Admin/RemoveAdmin/?u=" + userid,
        type: "POST",
        data: AddAntiForgeryToken({}),
        dataType: "text"
    })
    .success(function (data) {
        var obj = $.parseJSON(data);
        if (obj.result == "ok") {
            toastr.success(obj.message);
            reloadUsersTable();
        }
        else if (obj.result == "error") {
            toastr.warning(obj.message);
        }
    })
    .error(function () {
        toastr.error("Could not connect! Check your connection.");
    });
}

function makeadmin(userid) {
    $.ajax({
        url: "/Admin/MakeAdmin/?u=" + userid,
        type: "POST",
        data: AddAntiForgeryToken({}),
        dataType: "text"
    })
    .success(function (data) {
        var obj = $.parseJSON(data);
        if (obj.result == "ok") {
            toastr.success(obj.message);
            reloadUsersTable();
        }
        else if (obj.result == "error") {
            toastr.warning(obj.message);
        }
    })
    .error(function () {
        toastr.error("Could not connect! Check your connection.");
    });
}

//add movie functions

function disableAdding() {
    $("#addmovie-button").addClass("disabled");
    $("#btn-clear").addClass("disabled");
}

function enableAdding() {
    $("addmovie-button").removeClass("disabled");
    $("btn-clear").removeClass("disabled");
}

function clearform() {
    $("#notice").attr({ style: "display:none" });
    $("#ImdbId").val("");
    $("#Name").val("");
    $("#Year").val("");
    $("#Genre").val("");
    $("#ReleaseDate").val("");
    $("#Runtime").val("");
    $("#Writer").val("");
    $("#Director").val("");
    $("#Actors").val("");
    $("#PlotShort").val("");
    $("#PlotFull").val("");
    $("#Country").val("");
    $("#Language").val("");
    $("#Awards").val("");
    $("#PosterURL").val("");
    $("#moviecover").attr({ src: "/Content/images/poster.jpg" });
    $("#ImdbRatings").val("0");
    $("#ImdbVotes").val("0");
    $("#MovieClubRatings").val("0");
    $("#MovieClubRentCount").val("0");

    //$("#tupload").replaceWith($("#tupload") = $("#tupload").clone(true));

}

$("#btn-clear").click(function (e) {
    clearform();
    e.preventDefault();
});

function showSuggestions(displaytext, moviename) {
    resetNotice("secondary");
    $.ajax({
        url: "http://www.omdbapi.com/?",
        type: "GET",
        data: {
            s: moviename,
            r: "json"
        }
    })
    .success(function (data) {
        var obj = data;
        if (obj.Search.length > 0) {
            $("#notice span").html(displaytext + " :")
        }
        for (var i = 0, len = obj.Search.length; i < len; i++) {
            $("#notice ul").append("<li><a class=\"suggestion\" href=\"" + obj.Search[i].imdbID + "\">" + obj.Search[i].Title + "(" + obj.Search[i].Year + ")</a></li>");
        }

        $("#notice").removeAttr("style");

        $(".suggestion").click(function (e) {
            fetchImdbDataByID($(this).attr("href"));
            e.preventDefault();
        });

    });
}

function resetNotice(classname) {
    $("#notice span").html("");
    $("#notice ul").html("");
    $("#notice").removeAttr("class");
    $("#notice").addClass("alert-box");
    $("#notice").addClass("tiny");
    $("#notice").addClass(classname);
}

function fillImdbData(movieobj) {
    $("#notice").attr({ style: "display:none" });
    $("#ImdbId").val(movieobj.imdbID);
    $("#Name").val(movieobj.Title);
    $("#Year").val(movieobj.Year);
    $("#Genre").val((movieobj.Genre).replace(" ", ""));
    $("#ReleaseDate").val(movieobj.Released);
    $("#Runtime").val((movieobj.Runtime).substr(0, (movieobj.Runtime).indexOf(" min")));
    $("#Writer").val(movieobj.Writer);
    $("#Director").val(movieobj.Director);
    $("#Actors").val(movieobj.Actors);
    $("#PlotShort").val(movieobj.Plot);
    $("#Country").val(movieobj.Country);
    $("#Language").val(movieobj.Language);
    $("#Awards").val(movieobj.Awards);
    $("#PosterURL").val(movieobj.Poster);
    if (movieobj.Poster != "N/A" && movieobj.Poster != null) {
        $("#moviecover").attr({ src: movieobj.Poster });
    }
    else {
        $("#moviecover").attr({ src: "/Content/images/poster-na.jpg" });
    }
    $("#ImdbRatings").val(movieobj.imdbRating);
    $("#ImdbVotes").val((movieobj.imdbVotes).replace(",", ""));
    $("#MovieClubRatings").val("0");
    $("#MovieClubRentCount").val("0");

    $("#PlotFull").val("Loading...");

    $.ajax({
        type: "GET",
        url: "http://www.omdbapi.com/?",
        data: {
            t: movieobj.Title,
            plot: "full",
            r: "json"
        }
    })
    .success(function (data) {
        var fullplotobj = data;
        if (fullplotobj.Response == "False") {
            $("#PlotFull").val("");
        }
        else {
            $("#PlotFull").val(fullplotobj.Plot);
        }
    })
    .error(function () {
        $("#PlotFull").val("");
    });
}

function showMessage(message, classname) {
    resetNotice(classname);
    $("#notice span").html(message);
    $("#notice").removeAttr("style");
}

function fetchImdbDataByID(imdbid) {
    $.ajax({
        type: "GET",
        url: "http://www.omdbapi.com/?",
        data: {
            i: imdbid,
            plot: "short",
            r: "json",
            dataType: "text"
        }
    })
    .success(function (data) {
        $("#btn-fetch-imdb").html("Fetch IMDb data");
        $("#btn-fetch-imdb").removeClass("disabled");
        var movieobj = data;
        if (movieobj.Response == "False") {
            clearform();
            showMessage(movieobj.Error, "warning");
        }
        else {
            showSuggestions("More suggestions", movieobj.Title);
            fillImdbData(movieobj);
        }
    })
    .error(function () {
        $("#btn-fetch-imdb").html("Fetch IMDb data");
        $("#btn-fetch-imdb").removeClass("disabled");
        $("#notice span").html("Cannot connect to IMDb right now !");
        $("#notice").removeAttr("style");
    });
}

function fetchImdbData(moviename) {
    $.ajax({
        type: "GET",
        url: "http://www.omdbapi.com/?",
        data: {
            t: moviename,
            plot: "short",
            r: "json"
        }
    })
    .success(function (data) {
        $("#btn-fetch-imdb").html("Fetch IMDb data");
        $("#btn-fetch-imdb").removeClass("disabled");
        var movieobj = data;
        if (movieobj.Response == "False") {
            clearform();
            showMessage(movieobj.Error, "warning");
        }
        else {
            showSuggestions("More suggestions", moviename);
            fillImdbData(movieobj);
        }
    })
    .error(function () {
        $("#btn-fetch-imdb").html("Fetch IMDb data");
        $("#btn-fetch-imdb").removeClass("disabled");
        $("#notice span").html("Cannot connect to IMDb right now !");
        $("#notice").removeAttr("style");
    });
}

$("#btn-fetch-imdb").click(function (e) {
    $("#moviecover").attr("src", "/Content/images/ajax-loader-circle.gif");
    var moviename = $("#Name").val();
    $("#btn-fetch-imdb").html("Fetching...");
    $("#btn-fetch-imdb").addClass("disabled");
    e.preventDefault();
    if (moviename != "" && moviename != null) {
        fetchImdbData(moviename);
    }
    else {
        $("#Name").attr({
            placeholder: "Enter Movie Name here"
        });
        $("#notice").removeAttr("style");
        resetNotice("warning");
        $("#notice span").html("Enter Movie Name to search in IMDb !");
        $("#btn-fetch-imdb").html("Fetch IMDb data");
        $("#btn-fetch-imdb").removeClass("disabled");

    }
});

//add movie functions end

function approveReview(rid) {
    $.ajax({
        url: "/Admin/ApproveReview/?rid=" + rid,
        type: "POST",
        data: AddAntiForgeryToken({}),
        dataType: "text"
    })
    .success(function (data) {
        var obj = $.parseJSON(data);
        if (obj.result == "ok") {
            updatePendingReviews();
            toastr.success(obj.message);
        }
        else if (obj.result == "error") {
            toastr.success(obj.message);
        }
    })
    .error(function () {
        toastr.error("Could not connect! Check your connection.");
    });
}

function rejectReview(rid) {
    $.ajax({
        url: "/Admin/RejectReview/?rid=" + rid,
        type: "POST",
        data: AddAntiForgeryToken({}),
        dataType: "text"
    })
    .success(function (data) {
        var obj = $.parseJSON(data);
        if (obj.result == "ok") {
            updatePendingReviews();
            toastr.success(obj.message);
        }
        else if (obj.result == "error") {
            toastr.success(obj.message);
        }
    })
    .error(function () {
        toastr.error("Could not connect! Check your connection.");
    });
}

function issueMovie(movieid, userid) {
    $.ajax({
        url: "/Admin/Issue/?u=" + userid + "&&movie=" + movieid,
        data: AddAntiForgeryToken({}),
        type: "POST",
        dataType: "text"
    })
        .success(function (data) {
            var obj = $.parseJSON(data);
            if (obj.result == "ok") {

                $.ajax({
                    type: "GET",
                    url: "/Admin/ReservedMovies"
                })
                .success(function (data) {
                    $("#table-reservations tbody").html(data);
                })
                .error(function () {
                    $("#table-reservations tbody").html("<p>Error loading reservations. Try refreshing the page.</p>");
                });

                //refresh pending returns view
                updatePendingReturns();

                //$.ajax({
                //    url: "/Admin/PendingReturns",
                //    type: "GET"
                //})
                //.success(function (data) {
                //    $("#table-returns tbody").html(data);
                //})
                //.error(function () {
                //    $("#table-returns tbody").html("<p>Error loading pending returns!</p>");
                //});

                toastr.success(obj.message);
            }
            else if (obj.result == "error") {
                toastr.warning(obj.message);
            }
            else {
                toastr.error("Unknown error occured!");
            }
        })
        .error(function () {
            toastr.error("Error occured! Check your connection.");
        });
}

function markAsReturned(movieid, u) {
    $.ajax({
        url: "/Admin/MarkAsReturned/?movieid=" + movieid + "&&u=" + u,
        data: AddAntiForgeryToken({}),
        type: "POST",
        dataType: "text"
    })
    .success(function (data) {
        var obj = $.parseJSON(data);

        updatePendingReturns();
        updatePaymentDues();

        //$.ajax({
        //    url: "/Admin/PendingReturns",
        //    type: "GET"
        //})
        //.success(function (data) {
        //    $("#table-returns tbody").html(data);
        //})
        //.error(function () {
        //    $("#table-returns tbody").html("<p>Error loading pending returns!</p>");
        //});


        if (obj.result == "ok") {
            toastr.success(obj.message);
        }
    })
    .error(function () {
        toastr.error("Error occured! Check your connection.");
    });
}

//detailed payment



////////////////detailed payment end

///issue unreserved



$("#issueUnres").click(function (e) {
    $('#myModalIssueUnreserved').foundation('reveal', 'close');
    $.ajax({
        url: "/Admin/Issue",
        data: AddAntiForgeryToken({
            u: $("#userslist").val(),//$("#userlist option:selected", this).val(),
            movie: $("#movieslist").val(),//$("#movieslist option:selected", this).val()
            unreserved: true
        }),
        type: "POST",
        dataType: "text"
    })
    .success(function (data) {
        var obj = $.parseJSON(data);
        if (obj.result == "ok") {
            toastr.success(obj.message);
            updatePendingReturns();
        }
        else {
            toastr.warning(obj.message);
        }
    })
    .error(function () {
        toastr.error("Failed! Check your connection.");
    });
    e.preventDefault();
});

//////////////////
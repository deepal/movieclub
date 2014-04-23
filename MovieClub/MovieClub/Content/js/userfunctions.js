function readMessages() {
    $.ajax({
        url: "/User/MarkMessagesRead/",
        type: "POST",
        dataType: "text"
    })
}


function userUpdateRecommendations(){
    $.ajax({
        url: "/User/Recommendations",
        type: "GET",
    })
    .success(function (data) {
        $("#recom-table tbody").html(data);
    })
    .error(function () {
        $("#recom-table tbody").html("<p>Loading recommendations failed!</p>");
    });
}


function userUpdateFavorites() {
    $.ajax({
        url: "/User/Favorites",
        type: "GET"
    })
    .success(function (data) {
        $("#fav-table tbody").html(data);
    })
    .error(function () {
        $("#fav-table tbody").html("<p>Loading favorites failed!</p>");
    });
}


function userUpdateWatchlist() {
    $.ajax({
        url: "/User/Watchlist",
        type: "GET"
    })
    .success(function (data) {
        $("#watchlist-table tbody").html(data);
    })
    .error(function () {
        $("#watchlist-table tbody").html("<p>Loading favorites failed!</p>");
    });
}


function userUpdateReservations() {
    $.ajax({
        url: "/User/Reservations",
        type: "GET"
    })
    .success(function (data) {
        $("#reservations-table tbody").html(data);
    })
    .error(function () {
        $("#reservations-table tbody").html("<p>Error loading reservations!</p>");
    });
}


function userUpdateCurrentRents() {
    $.ajax({
        url: "/User/CurrentRents",
        type: "GET"
    })
    .success(function (data) {
        $("#rents-table-pending tbody").html(data);
    })
    .error(function () {
        $("#rents-table-pending tbody").html("<p>Error loading current rents!</p>");
    });
}


function userUpdateRentsHistory() {
    $.ajax({
        url: "/User/RentsHistory",
        type: "GET"
    })
    .success(function (data) {
        $("#rents-table tbody").html(data);
    })
    .error(function () {
        $("#rents-table tbody").html("<p>Error loading rents history!</p>");
    });
}


function userUpdateMessages() {
    $.ajax({
        url: "/User/Messages",
        type: "GET"
    })
    .success(function (data) {
        $("#inbox-table tbody").html(data);
    })
    .error(function () {
        $("#inbox-table tbody").html("<p>Error loading Inbox!</p>");
    });
}
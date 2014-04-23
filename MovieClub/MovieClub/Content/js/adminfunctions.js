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
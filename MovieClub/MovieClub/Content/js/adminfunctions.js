function updateReservations() {
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
        $("#table-returns tbody").html("<p>Error loading pending returns!</p>");
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
        $("#table-pending-payments tbody").html("<p>Error loading pending payments!</p>");
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
        $("#table-users tbody").html("<p>Error loading users!</p>");
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
        $("#table-payments-history tbody").html("<p>Error loading payment history!</p>");
    });
}
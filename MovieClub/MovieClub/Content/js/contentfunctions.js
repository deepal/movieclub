function updateReviews(mid) {
    $.ajax({
        url: "/Content/GetReviews?mid="+mid,
        type: "GET"
    })
    .success(function (data) {
        $("#old-reviews").html(data);
    })
    .error(function () {
        $("#old-reviews").html("<p style='font-size:12px'>Could not retrieve reviews! Refresh the page to retry!</p>")
    });
}
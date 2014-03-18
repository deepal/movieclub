
$(document).ready(function () {
    
    $(".page-link").click(function (e) {
        e.preventDefault();

    });

    $.ajax({
        type: "GET",
        url: "/Content/Collection/"+""+"/?",
        data:{ Page : 1, Method : 1}
    })
    .success(function (data) {
        $("#category-content").html(data);
    })
    .error(function () {
        $("#category-content").html("Error Loading Category content!");
    });

});

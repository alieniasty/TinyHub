function addSearchHoverIn() {
    $("#searchBtn").after("<a href='#' class='searchAdvancedBtnText'> Advanced search</a>");
    $('.searchAdvancedBtnText').addClass("animated flipInX");
}

function addSearchHoverOut() {
    $('.searchAdvancedBtnText').remove();
}

$(document)
    .ready(function () {
        if ($("tbody").children().length == 0) {
            $('.table').remove();
            $(document.body).append("<div id='info' style='text-align: center; margin-top: 10px;'>");
            $("#info")
                .append("<p style='font-size: 140%; '>We are sorry. We couldn't locate what you seek</p>");
            $("#info")
                .append("<i class='fa fa-search fa-3x'></i>");
            $("#info")
                .append("<a href='SearchAdvanced' style='font-size: 110%; display: block; margin-top: 10px;'>Try advanced search</a>");
        }
        else {
            $(document.body)
                .append("<div class='searchAdvancedBtn' id='searchBtn'>" +
                    "<a href='SearchAdvanced' class='fa fa-search fa-2x' id='magnifierBtn' onmouseover='addSearchHoverIn()' onmouseout='addSearchHoverOut()'></a>" +
                    "</div>");

            $('.searchAdvancedBtn').addClass("animated zoomInUp");
        }
    })
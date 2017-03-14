$(document)
    .ready(function() {
        $('a#project')
            .mouseenter(function() {
                var i = $(this).find("#folder");
                i.removeClass('fa fa-folder');
                i.addClass('fa fa-folder-open');
            })
            .mouseleave(function() {
                var i = $(this).find("#folder");
                i.removeClass('fa fa-folder-open');
                i.addClass('fa fa-folder');
            });

        if ($("#myProjectsColumns").children().length == 0) {
            $(document.body)
                .append("<div id='info' style='text-align: center; margin-top: 10px;'></div>");
            $("#info")
                .append("<p style='font-size: 140%; '>Currently you don't have any projects</p>");
            $("#info")
                .append("<i class='fa fa-smile-o fa-3x'></i>");
            $("#info")
                .append("<a href='NewProject' style='font-size: 110%; display: block; margin-top: 10px;'>How about creating one?</a>");
        }


    });
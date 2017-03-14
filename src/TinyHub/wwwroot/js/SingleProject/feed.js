function onNotifDelete(id, el) {
    var settings = {
        url: "/api/notification/notificationdelete" + "/" + id,
        success: function () {
            $(el).parents().eq(1).remove();
            fetchNotificationsNumber();
        },
        method: "DELETE",
        contentType: "application/x-www-form-urlencoded"
    }

    $.ajax(settings);
}

function generateNotificationsTable() {
    $.getJSON("/api/notification/notifications",
        {projectName : $("#activeProject").html() },
        function (notifications) {

            $("#rightPanel").append("<div class='selectedContent animated zoomInUp'>");

            if (notifications.length === 0) {
                $('.selectedContent').append("<p style='font-size: 140%; text-align: center'>Currently you don't have any notifications</p>");
                $('.selectedContent').append("<i class='fa fa-smile-o fa-3x' style='margin-left:48%'></i>");
                return;
            }

            $('.selectedContent').append("<table id='notificationsTable' class='table table-hover'>");
            $("#bugsTable").append("<thead>");
            $("thead").append("<tr>");

            $("<th>").appendTo("tr").html("");
            $("<th>").appendTo("tr").html("Type");
            $("<th>").appendTo("tr").html("Time");
            $("<th>").appendTo("tr").html("From User");

            $("#notificationsTable").append("<tbody>");

            for (var i = notifications.length - 1; i >= 0; i--) {
                $("<tr id='row" + i + "'>").appendTo("tbody");

                var notificationMessage = "";

                $("<td>").appendTo("tr#row" + i).html("<button type='submit' class='fa fa-times notificationActionBtn' id='notifDeleteBtn' " +
                    "onclick='onNotifDelete("+notifications[i].id+", this)'></button>");

                switch (notifications[i].type) {
                    case 1:
                        notificationMessage += "Bug was updated by user ";
                    break;
                    case 2:
                        notificationMessage += "Bug was created by user ";
                        break;
                    case 3:
                        notificationMessage += "Bug was deleted by user ";
                        break;
                    case 4:
                        notificationMessage += "File was uploaded by user ";
                        break;
                    case 5:
                        notificationMessage += "File was deleted by user ";
                        break;
                }

                notificationMessage += notifications[i].user + " on " + notifications[i].timeOfAppear.replace("T", " at ");

                $("<td>").appendTo("tr#row" + i).html(notificationMessage);
            }
        });
}

$(document)
    .ready(function() {
        $("#feed")
            .click(function() {
                $("#rightPanel").html("");
                generateNotificationsTable();
            });
    })
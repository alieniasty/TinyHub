function fetchNotificationsNumber() {
    $.getJSON("/api/notification/notifications",
        { projectName: $("a#activeProject").html() },
        function (notifications) {
            if (notifications.length > 0) {
                $('.js-notification-count')
                    .text(notifications.length)
                    .removeClass("hide")
                    .addClass("animated fadeInDown");
            }
        });
}
var programmingFiles = [
    '.c', '.class', '.cmd', '.cpp', '.db', '.h', '.cs', '.js', '.hpp', '.py', '.html', '.css', '.java', '.php', '.php3', '.json', '.dll'
];

function deleteProject() {
    var projectName = $("a#activeProject").html();

    var settings = {
        url: "/api/project/delete" + "/" + projectName,
        success: function () {
            window.location.replace("/Home/MyProjects");
        },
        method: "DELETE",
        contentType: "application/x-www-form-urlencoded"
    }

    $.ajax(settings);
}

function confirmDeleteFile(file, folder, el) {
    $(el).confirmation('show');
    $(el)
        .on("confirmed.bs.confirmation",
            function() {
                deleteFile(file, folder, this);
            });
}

function confirmDeleteProject(el) {
    $($(el).parents().eq(0)).confirmation('show');
    $($(el).parents().eq(0))
        .on("confirmed.bs.confirmation",
            function () {
                deleteProject();
            });
}

function deleteFile(fileName, projectFolder, el) {
    
    var settings = {
        url: "/api/project/deletefile/" + fileName + "/" + projectFolder,
        success: function () {
            $(el).parents().eq(1).remove();
            fetchNotificationsNumber();
        },
        method: "DELETE",
        contentType: "application/x-www-form-urlencoded"
    }

    $.ajax(settings);
}

function deleteHoverIn() {
    $("#deleteProject").after("<a href='#' class='deleteProjectBtnText'> Delete project</a>");
    $('.deleteProjectBtnText').addClass('animated flipInX');
}

function deleteHoverOut() {
    $('.deleteProjectBtnText').remove();
}

function addFilesHoverIn() {
    $("#addFiles").after("<a href='#' class='addFilesBtnText'> Add files to project</a>");
    $('.addFilesBtnText').addClass('animated flipInX');
}

function addFilesHoverOut() {
    $('.addFilesBtnText').remove();
}

function addFilesSelect() {
    $("#filesInput").toggle();
    $("#filesInput").addClass("animated fadeInLeft");
}

function generateCodeButtons() {
    $("#rightPanel").append("<div class='deleteProjectButton' id='deleteProject'>" +
        "<a href='#' class='fa-stack fa-lg' id='trashBtn' onmouseover='deleteHoverIn()' " +
        "onmouseout='deleteHoverOut()' data-toggle='confirmation' data-placement='left' onclick='confirmDeleteProject(this)'>" +
        "<i class='fa fa-circle fa-stack-2x'></i>" +
        "<i class='fa fa-trash fa-stack-1x fa-inverse'></i>" +
        "</a>" +
        "</div>");

    $("#rightPanel").append("<div class='addFilesButton' id='addFiles'>" +
        "<a href='#' class='fa-stack fa-lg' id='filesBtn' onmouseover='addFilesHoverIn()' onmouseout='addFilesHoverOut()' onclick='addFilesSelect()'>" +
        "<i class='fa fa-circle fa-stack-2x'></i>" +
        "<i class='fa fa-code fa-stack-1x fa-inverse'></i>" +
        "</a>" +
        "</div>");

    $('.deleteProjectButton').addClass('animated zoomInUp');
    $('.addFilesButton').addClass('animated zoomInUp');
}

function genereateFileIcons() {
    var numberOfFiles = $("tbody").children().size();

    for (var i = 0; i < numberOfFiles; i++) {
        var fileName = $("tbody").children().eq(i).children().eq(1).children().eq(0).html().toLowerCase();
        var fileExtension = fileName.substr(fileName.indexOf("."));
        var icon = $("tbody").children().eq(i).children().eq(0).children();

        if (fileExtension.indexOf('.png') >= 0 || fileExtension.indexOf('.jpg') >= 0 || fileExtension.indexOf('.jpeg') >= 0 || fileExtension.indexOf('.gif') >= 0) {
            icon.addClass('fa fa-file-image-o fa-2x');
        }

        if (fileExtension.indexOf('.rar') >= 0 || fileExtension.indexOf('.zip') >= 0 || fileExtension.indexOf('.7z') >= 0 || fileExtension.indexOf('.tar') >= 0) {
            icon.addClass('fa fa-file-archive-o fa-2x');
        }

        if (programmingFiles.indexOf(fileExtension) >= 0) {
            icon.addClass('fa fa-file-code-o fa-2x');
        }
    }
}

function uploadWait() {
    $(document.body).append("<i class='fa fa-spinner fa-pulse fa-4x fa-fw' id='load'></i>");
    fetchNotificationsNumber();
}

$(document)
    .ready(function() {
        generateCodeButtons();
        genereateFileIcons();
        fetchNotificationsNumber();

        $("#files")
            .change(function() {
                var numFiles = $("#files")[0].files.length;
                $("label[for='files']").html(numFiles + " files selected");
            });
    })
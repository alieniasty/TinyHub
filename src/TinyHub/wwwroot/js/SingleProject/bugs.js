function createTable() {
    
    $('.selectedContent').addClass('animated zoomInUp');
    $('.selectedContent').append("<table id='bugsTable' class='table table-hover'>");
    $("#bugsTable").append("<thead>");
    $("thead").append("<tr>");

    $("<th>").appendTo("tr").html("");
    $("<th>").appendTo("tr").html("");
    $("<th>").appendTo("tr").html("Description");
    $("<th>").appendTo("tr").html("Priority");
    $("<th>").appendTo("tr").html("From User");

    $("#bugsTable").append("<tbody>");
}

function populateTable(data) {
    for (var i = 0; i < data.length; i++) {
        var priority = data[i].priority;

        $("<tr id='row" + i + "'>").appendTo("tbody");
        
        $("<td>").appendTo("tr#row" + i).html("<button type='submit' class='fa fa-trash-o bugActionBtn' id='bugDelete' onclick='onDeleteBug(this)'></button>");
        $("<td>").appendTo("tr#row" + i).html("<button type='submit' class='fa fa-pencil-square-o bugActionBtn' id='bugEdit' onclick='onEditBug(this)'></button>");
        $("<td>").appendTo("tr#row" + i).html(data[i].description);

        if (priority > 3) {
            $("<td>").appendTo("tr#row" + i).html("<i class='fa fa-exclamation-triangle' style='color: #ed5a5a;'</i>");
        } else if (priority === 3){
            $("<td>").appendTo("tr#row" + i).html("High");
        } else if (priority === 2) {
            $("<td>").appendTo("tr#row" + i).html("Medium");
        } else {
            $("<td>").appendTo("tr#row" + i).html("Low");
        }

        $("<td>").appendTo("tr#row" + i).html(data[i].userCalling);

    }
}

function editBug(project, user, newDesc, oldDesc) {
    var settings = {
        url: "/api/bugs/updatebug",
        method: "POST",
        success: function() {
            fetchNotificationsNumber();
        },
        contentType: "application/json",
        data: JSON.stringify({
            fromProject: project,
            userCalling: user,
            description: oldDesc,
            newDescription: newDesc
        })
    };

    $.ajax(settings);
}

function onEditBug(el) {
    var oldDescription = $(el).parents().eq(1).children().eq(2).html();
    var fromUser = $(el).parents().eq(1).children().eq(4).html();

    $(el).parents().eq(1).children().eq(2).html("");
    $(el).parents().eq(1).children().eq(2).append("<form><textarea rows='1' class='form-control' id='editedDescription'></textarea></form>");

    $("#editedDescription").html(oldDescription);
    $("#editedDescription")
        .keydown(function (e) {
            if (e.keyCode === 13 && !e.shiftKey) {

                var newDescription = $("#editedDescription").val();
                $(el).parents().eq(1).children().eq(2).html("");
                $(el).parents().eq(1).children().eq(2).html(newDescription);

                editBug($("#activeProject").html(), fromUser, newDescription, oldDescription);
            }

            return true;
        });
}

function addBugHoverIn() {
    $("#submitBug").after("<a href='#' class='submitNewBugBtnText'> Submit new bug</a>");
    $('.submitNewBugBtnText').addClass('animated flipInX');
}


function addBugHoverOut() {
    $('.submitNewBugBtnText').remove();
}

function tableToPdfHoverIn() {
    $("#toPdf").after("<a href='#' class='tableToPdfBtnText'> Table to PDF</a>");
    $('.tableToPdfBtnText').addClass('animated flipInX');
}

function tableToPdfHoverOut() {
    $('.tableToPdfBtnText').remove();
}

function generateBugButtons() {
    $("#rightPanel").append("<div class='submitNewBugButton' id='submitBug'>" +
        "<a href='#' class='fa-stack fa-lg' id='telegramBtn' onmouseover='addBugHoverIn()' onmouseout='addBugHoverOut()' onclick='slideInBug()'>" +
        "<i class='fa fa-circle fa-stack-2x'></i>" +
        "<i class='fa fa-telegram fa-stack-1x fa-inverse'></i>" +
        "</a>" +
        "</div>");

    $("#rightPanel").append("<div class='tableToPdfBtn' id='toPdf'>" +
        "<a href='#' class='fa-stack fa-lg' id='pdfBtn' onmouseover='tableToPdfHoverIn()' onmouseout='tableToPdfHoverOut()' onclick='generatePdf()'>" +
        "<i class='fa fa-circle fa-stack-2x'></i>" +
        "<i class='fa fa-file-pdf-o fa-stack-1x fa-inverse'></i>" +
        "</a>" +
        "</div>");

    $('.submitNewBugButton').addClass('animated zoomInUp');
    $('.tableToPdfBtn').addClass('animated zoomInUp');
}

function slideInBug() {
    $('.bug-Input').toggle();
    $('.bug-Input').addClass("animated zoomIn");

    if ($('.bug-Input').css("display") === "none") {
        $('.selectedContent').css("width", "66%");
    } else {
        $('.selectedContent').css("width", "40%");
    }
}

function fetchBugs() {
    if ($("#bugsTable").length === 0) {
        $.getJSON("/api/bugs/getbugs",
                    { projectName: $("a#activeProject").html() },
                    function (data) {
                        $("#rightPanel").append("<div class='selectedContent'>");
                        createTable();
                        populateTable(data);
                    });
    }
}

function submitBug() {
    var settings = {
        url: "/api/bugs/createbug",
        method: "POST",
        success: function() {
            $('.selectedContent').remove();
            fetchBugs();
            $('.bug-Input').toggle();
            fetchNotificationsNumber();
        },
        contentType: "application/json",
        data: JSON.stringify({
            description: $("#newDescription").val(),
            priority: $("#newPriority")[0].selectedIndex + 1,
            fromProject: $("#activeProject").html()
        })
    };

    $.ajax(settings);
}

function deleteBug(description, row) {
    var settings = {
        url: "/api/bugs/deletebug" + "/" + description,
        success: function() {
            $(row).remove();
            fetchNotificationsNumber();
        },
        method: "DELETE",
        contentType: "application/x-www-form-urlencoded"
    };

    $.ajax(settings);
}

function onDeleteBug(el) {
    var parent = $(el).parents().eq(1);
    var description = $(parent).children().eq(2).html();
    deleteBug(description, parent);
}

function getDataFromTable() {
    var data = [[{ text: 'Description', bold: true }, { text: 'Priority', bold: true }, { text: 'From', bold: true }]];

    for (var i = 0; i < $("tbody tr").length; i++) {

        if ($($("tbody").children().eq(i).children().eq(3).html()).hasClass("fa fa-exclamation-triangle")) {
            
            data.push([
            $("tbody").children().eq(i).children().eq(2).html(),
            'Urgent',
            $("tbody").children().eq(i).children().eq(4).html()
            ]);
        } else {
            data.push([
            $("tbody").children().eq(i).children().eq(2).html(),
            $("tbody").children().eq(i).children().eq(3).html(),
            $("tbody").children().eq(i).children().eq(4).html()
            ]);
        }
    }

    return data;
}

function generatePdf() {
    var data = getDataFromTable();
    var pdf = {
        content: [
    {
        table: {
            headerRows: 1,
            widths: ['auto', 'auto', 'auto'],
            body: data
        }
    }
        ]
    };

    pdfMake.createPdf(pdf).open();
}

$(document)
    .ready(function() {
        $('#bugs')
            .click(function () {
                $("#rightPanel").html("");
                fetchBugs();
                generateBugButtons();

                $("#rightPanel")
                        .append("<div class='bug-Input'>" +
                    "<div class='row'>" +
                        "<div class='form-group col-xs-12'>" +
                            "<div class='input-group'>" +
                                "<span class='input-group-addon'><i class='fa fa-comment'></i></span>" +
                                "<textarea rows='3' class='form-control' placeholder='Description' id='newDescription'></textarea>" +
                            "</div>" +
                        "</div>" +
                   "</div>" +

                    "<div class='row'>" +
                        "<div class='form-group col-xs-12'>" +
                            "<div class='input-group'>" +
                                "<span class='input-group-addon'><i class='fa fa-fire'></i></span>" +
                                "<select class='selectpicker form-control' id='newPriority'>" +
                                    "<option>Low</option>" +
                                    "<option>Medium</option>" +
                                    "<option>High</option>" +
                                    "<option>Urgent</option>" +
                                "</select>" +
                            "</div>" +
                        "</div>" +
                    "</div>" +

                    "<div class='row'>" +
                        "<div class='col-xs-4'>" +
                            "<div class='btn btn-success custom-btn' id='addButton'>Add</div>" +
                        "</div>" +
                    "</div>" +
                "</div>");

                    $("#addButton")
                            .click(function () {
                                submitBug();
                            });
            });

        
    });



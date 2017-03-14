$(document)
    .ready(function () {
        $('.selectpicker')
            .selectpicker({
                style: 'btn-default',
                size: 5
            });
    });

$('#licenseType')
    .change(function () {

        if ($('.selectpicker').val() === "MIT License")
        {
            $('#projectLicenseType').text("Permission is hereby granted, free of charge, to any person obtaining a copy of " +
                "this software and associated documentation files...");

            $("#projectLicenseType").append("<a href='https://opensource.org/licenses/MIT'> Find out more </a>");
        }

        if ($('.selectpicker').val() === "GNU GPL")
        {
            $('#projectLicenseType').text("The GNU General Public License is a widely used free " +
                "software license, which guarantees end users the freedom to run, study, share and modify the software...");

            $("#projectLicenseType").append("<a href='https://www.gnu.org/licenses/gpl-3.0.en.html'> Find out more </a>");
        }

        if ($('.selectpicker').val() === "Mozilla Public License 2.0")
        {
            $('#projectLicenseType').text("It is a weak copyleft license, characterized as a middle ground between permissive free software licenses and " +
                "GNU General Public License (GPL), that seeks to balance the concerns of proprietary and open source developers...");

            $("#projectLicenseType").append("<a href='https://www.mozilla.org/en-US/MPL/2.0/'> Find out more </a>");
        }

        if ($('.selectpicker').val() === "Apache License")
        {
            $('#projectLicenseType').text("The Apache License requires preservation of the copyright notice and disclaimer. Like other free software " +
                "licenses, the license allows the user of the software the freedom to use the software for any purpose...");

            $("#projectLicenseType").append("<a href='https://www.apache.org/licenses/LICENSE-2.0'> Find out more </a>");
        }

        if ($('.selectpicker').val() === "Unlicensed")
        {
            $('#projectLicenseType').text(" ");
        }

    });

$("#projectAccess")
    .change(function () {
        $("#projectAccessDescription").html("");

        if ($("#projectAccess").val() === "Public") {
            $("#projectAccessDescription").append("<i class='fa fa-unlock fa-2x'></i><br>");
            $("#projectAccessDescription")
                .append("Your project will be visible through the search form. The anonymous accessibility narrows only to the code.");
        }

        if ($("#projectAccess").val() === "Private") {
            $("#projectAccessDescription").append("<i class='fa fa-lock fa-2x'></i><br>");
            $("#projectAccessDescription")
                .append("Your project is not visible through the search form and is not accessible by any user, except those you grant access.");
        }

    });

$('#projectName').keyup(function() {
    $('#projectNameDescription').html($(this).val());
});

$('#addButton')
    .click(function () {

        $(document.body).append("<i class='fa fa-spinner fa-pulse fa-4x fa-fw' id='load'></i>");

        var settings = {
            url: "/api/project/create",
            method: "POST",
            contentType: "application/json",
            data: JSON.stringify({
                name: $("#projectName").val(),
                isPrivateProject: $("#projectAccess")[0].selectedIndex,
                license: $("#licenseType")[0].selectedIndex
            })
        }

        $.ajax(settings)
            .done(function () {
                window.location.replace("/Home/MyProjects");
            });
    });
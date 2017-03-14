$(document).ready(function() {
    $('#date-container input').datepicker({
        format: "yyyy-mm-dd"
    });

    $('.selectpicker')
        .selectpicker({
           style: 'btn-default',
           size: 6
    });
})
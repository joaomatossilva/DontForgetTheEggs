$(document).ready(function () {

    // init the datepickers
    $('.datepicker').datepicker({
        autoclose: true,
        language: "pt"
    });

    $('.timepicker').timepicker({
        showMeridian: false,
        showInputs: false,
        minuteStep: 1,
        defaultTime: false
    });

});
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

    _initSelect2($(".search-ingredient"), function (obj) {
        obj.text = obj.text || obj.Name + " - " + obj.CategoryName;
        obj.id = obj.id || obj.Id;
        return obj;
    });

    function _initSelect2(elem, mapFunction) {

        //there's no element. just return
        if (elem.length === 0) {
            return;
        }


        elem.select2({
            ajax: {
                dataType: 'json',

                data: function (params) {
                    var query = {
                        SearchText: params.term,
                        Page: 1
                    }

                    return query;
                },

                processResults: function (data) {
                    var items = $.map(data, mapFunction);
                    return {
                        results: items
                    }
                }
            }
        })
            .on('select2:select', function (e) {
                var selectedDataElementName = elem.data("selected-element");
                var selectedDataElement = $("#" + selectedDataElementName);
                if (selectedDataElement.length === 0) {
                    return;
                }
                selectedDataElement.val(e.params.data.text);
            });
    }
});
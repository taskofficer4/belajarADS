function showToast(message) {
    $("#toastSuccess .toast-body").text(message)
    $("#toastSuccess").toast('show');
}

function showToastError(message) {
    $("#toastError .toast-body").text(message)
    $("#toastError").toast('show');
}


var anElements = {};
function initInputTextNumeric() {

    var lsNumeric = $(".autoNumeric");
    listAutonumeric = [];
    for (var i = 0; i < lsNumeric.length; i++) {

        var e = lsNumeric[i];

        var max = 100000000000000;
        var min = 0;
        var optMax = $(e).attr('max');
        if (optMax) {
            max = optMax;
        }

        var optMin = $(e).attr('min');
        if (optMin) {
            min = optMin;
        }

        var option = {
            unformatOnSubmit: true,
            maximumValue: max,
            minimumValue: min
        };

        var suffixText = $(e).attr('suffixText');
        if (suffixText) {
            option.suffixText = suffixText;
        }

        var decimalPlaces = $(e).attr('decimalPlaces');
        if (decimalPlaces) {
            option.decimalPlaces = decimalPlaces;
        }


        var anElement = new AutoNumeric(e, option);
        anElements[e.getAttribute('id')] = anElement;
    }
}


function initTextEditor() {

    var lsTxt = $(".text-editor");

    for (var i = 0; i < lsTxt.length; i++) {
        var e = lsTxt.eq(i);
        var placeholder = e.attr("placeholder");
        if (!placeholder) {
            placeholder = "Select " + ToTitleCase(e.attr('id'));
        }
        if (e.hasClass("input-validation-error")) {
            e.parent().addClass("input-validation-error");
        }

        var isDisabled = $(e).closest('fieldset').attr("disabled");
        if (isDisabled) {
            e.summernote({
                toolbar: [],
                placeholder: placeholder,
                tabsize: 2,
                height: 80,
            });
            e.summernote('disable');
        } else {
            e.summernote({
                toolbar: [
                    ['style', ['bold', 'italic', 'underline', 'strikethrough']],
                    ['para', ['ol', 'ul']],

                ],
                placeholder: placeholder,
                tabsize: 2,
                height: 80,
                callbacks: {
                    onChange: function () {
                        var containerDiv = $(this).closest(".mb-3");
                        containerDiv.removeClass("input-validation-error")
                        containerDiv.find(".field-validation-error").hide();
                    },
                    onImageUpload: function (data) {
                        data.pop();
                    }
                }
            });
        }


    }

    $(".text-editor")
}

function ToTitleCase(text) {
    const result = text.replace(/([A-Z])/g, " $1");
    const finalResult = result.charAt(0).toUpperCase() + result.slice(1);

    return finalResult;
}

function initSelect2() {

    var lsSelect2 = $(".init-select2");

    for (var i = 0; i < lsSelect2.length; i++) {
        var e = lsSelect2.eq(i);
        var placeholder = e.attr("placeholder");
        if (!placeholder) {

            if (e.attr('id')) {
                placeholder = "Select " + ToTitleCase(e.attr('id'));
            }

        }
        if (e.hasClass("input-validation-error")) {
            e.parent().addClass("input-validation-error");
        }





        var select2Option = {
            placeholder: placeholder,
            allowClear: true,
            width: "100%"
        };


        var tag = e.attr("tags");
        if (tag) {
            select2Option.tags = true;
            select2Option.templateSelection = function (selection) {
                if (selection.selected == undefined) {
                    return selection.text;
                }
                if (selection.selected) {
                    return selection.text;
                }
                else {
                    return $.parseHTML('<div class="d-flex align-items-center"> <div>' + selection.text + '</div><div class="ms-auto select2-onthefly-item">New</div></div>');
                }
            };

            select2Option.createTag = function (params) {
                var term = $.trim(params.term);

                if (term === '') {
                    return null;
                }

                return {
                    id: term,
                    text: term,
                    newTag: true // add additional parameters
                }
            };

            select2Option.templateResult = function (item) {
                if (!item) {
                    return;
                }

                if (item.newTag) {
                    return $.parseHTML('<div class="d-flex align-items-center"> <div>' + item.text + '</div><div class="ms-auto select2-onthefly-item me-0">New</div></div>');
                }
                
                return item.text;
            };
        }

        var parentModal = e.closest('.modal');
        if (parentModal.length > 0) {
            select2Option.dropdownParent = parentModal;
        }


        e.select2(select2Option).on('select2:unselecting', function () {
            $(this).data('unselecting', true);
        }).on('select2:opening', function (e) {
            if ($(this).data('unselecting')) {
                $(this).removeData('unselecting');
                e.preventDefault();
            }
        }).on("change", function (e) {
            $(e.target).closest('.mb-3').removeClass('input-validation-error');
            $(e.target).closest('.mb-3').find(".input-validation-error").removeClass('input-validation-error');
            $(e.target).closest('.mb-3').find(".field-validation-error").hide()
        });




    }

}

function initMultiselect() {
    var lsSelect2 = $(".init-multiselect");

    for (var i = 0; i < lsSelect2.length; i++) {
        var e = lsSelect2.eq(i);

        var placeholder = e.attr("placeholder");
        if (!placeholder) {
            if (e.attr('id')) {
                placeholder = "Select " + ToTitleCase(e.attr('id')).trim();
            }
        }
        let option = {
            placeholder: placeholder,
            closeOnSelect: false,
            dropdownCssClass: 'multicheckbox',
            allowClear: true,
            width: "100%"
        };

        var maxSelectionLength = e.attr("maximumSelectionLength");
        if (maxSelectionLength) {

            option.maximumSelectionLength = maxSelectionLength;
        }

        e.select2(option)
            .on('select2:unselecting', function () {
                $(this).data('unselecting', true);
            }).on('select2:opening', function (e) {
                if ($(this).data('unselecting')) {
                    $(this).removeData('unselecting');
                    e.preventDefault();
                }
            }).on('select2:opening select2:closing', function (event) {
          
            }).on("change", function (e) {
                $(e.target).closest('.mb-3').removeClass('input-validation-error');
                $(e.target).closest('.mb-3').find(".input-validation-error").removeClass('input-validation-error');
                $(e.target).closest('.mb-3').find(".field-validation-error").hide()
            });


    }



}

var listDatePicker = [];
function initDatePicker(elementId) {
    var listMonthPicker = $(".init-datepicker");


    function syncDateToHiddenEl(event) {
        let rawDate = $(event.currentTarget).datepicker('getDate');


        var btnClear = $(event.currentTarget).next();
        if (btnClear.hasClass('btn-input-clear')) {
            if (rawDate) {
                btnClear.show();
            } else {
                btnClear.hide();
            }
        }


        let formatedDate = moment(rawDate).format('YYYY-MM-DD');
        var originalId = event.currentTarget.getAttribute('original-id')
        if (rawDate) {
            $("#" + originalId).val(formatedDate);
        } else { $("#" + originalId).val(''); }

    }



    function initOneDatePicker(elementId) {
        let el = $("#" + elementId);
        el.removeClass("init-datepicker");
        el.addClass("datepicker");
        var originalValue = $(el).val();
        var originalElement = '<input type="hidden" id="@id" value="@value" name="@id"  />'
        originalElement = originalElement
            .replaceAll("@id", elementId)
            .replaceAll("@value", originalValue);
      
        $(el).parent().append(originalElement);
        $(el).attr('id', elementId + "_datepicker");
        $(el).attr('original-id', elementId);
        $(el).datepicker({
            autoclose: true,
            orientation: "bottom auto",
            templates: {
                leftArrow: '<i class="fa-solid fa-angle-left"></i>',
                rightArrow: '<i class="fa-solid fa-angle-right"></i>'
            },
            maxViewMode: 2,
            format: 'dd-M-yyyy',
            weekStart: 1

        }).on('changeDate', function (e) {
            syncDateToHiddenEl(e);
            // clear warning if any
            $(this).removeClass('input-validation-error');
            $(this).parent().parent().find(".field-validation-error").hide();

            $(this).closest('input-validation-error').removeClass('input-validation-error');
        });

        if (originalValue) {
            $(el).datepicker('setDate', new Date(originalValue));
        }
    }

    // define one el
    if (!!elementId == true) {
        initOneDatePicker(elementId);
        return;
    }

    for (var i = 0; i < listMonthPicker.length; i++) {
        var el = listMonthPicker[i];
        var elementId = $(el).attr('id');
        initOneDatePicker(elementId);
    }
}



function initMonthPicker() {
    var listMonthPicker = $(".monthpicker");


    function syncDateToHiddenEl(event) {
        let rawDate = $(event.currentTarget).datepicker('getDate');
        let formatedDate = moment(rawDate).format('YYYY-MM-DD');
        var originalId = event.currentTarget.getAttribute('original-id')
        if (rawDate) {
            $("#" + originalId).val(formatedDate);
        } else { $("#" + originalId).val(''); }

    }
    for (var i = 0; i < listMonthPicker.length; i++) {
        var el = listMonthPicker[i];
        var elementId = $(el).attr('id');

        var originalValue = $(el).val();
        var originalElement = '<input type="hidden" id="@id" value="@value" name="@id"  />'
        originalElement = originalElement
            .replaceAll("@id", elementId)
            .replaceAll("@value", originalValue);

        $(el).parent().append(originalElement);
        $(el).attr('id', elementId + "_datepicker");
        $(el).attr('original-id', elementId);


        var option = {
            autoclose: true,
            orientation: "bottom auto",
            templates: {
                leftArrow: '<i class="fa-solid fa-angle-left"></i>',
                rightArrow: '<i class="fa-solid fa-angle-right"></i>'
            },
            minViewMode: 1,
            maxViewMode: 2,
            format: 'M yyyy'

        };

        var minDate = $(el).attr("minDate");
        if (minDate) {
            option.startDate = new Date(minDate);
        }

        var maxDate = $(el).attr("maxDate");
        if (maxDate) {
            option.endDate = new Date(maxDate);
        }

        $(el).datepicker(option).on('changeDate', function (e) {
            syncDateToHiddenEl(e);
        });

        $(el).datepicker('setDate', new Date(originalValue));

        //$(el).change(function (event) {
        //    syncDateToHiddenEl(e);
        //});
    }
}


function initYearPicker() {
    var listMonthPicker = $(".yearpicker");

    function showHideClearBtn(e) {
        var rawDate = $(e).val();
        var btnClear = $(e).next();
        if (btnClear.hasClass('btn-input-clear')) {
            if (rawDate) {
                btnClear.show();
            } else {
                btnClear.hide();
            }
        }
    }

    for (var i = 0; i < listMonthPicker.length; i++) {
        var el = listMonthPicker[i];
        showHideClearBtn(el);
        $(el).datepicker({
            autoclose: true,
            orientation: "bottom auto",
            templates: {
                leftArrow: '<i class="fa-solid fa-angle-left"></i>',
                rightArrow: '<i class="fa-solid fa-angle-right"></i>'
            },
            minViewMode: 2,
            maxViewMode: 2,
            format: 'yyyy'
        }).on('changeDate', function (e) {
            showHideClearBtn(e.currentTarget);
            
            // clear warning if any
            $(this).removeClass('input-validation-error');
            $(this).parent().parent().find(".field-validation-error").hide();

            $(this).closest('input-validation-error').removeClass('input-validation-error');
        });
    }
}


/**
 * Format bytes as human-readable text.
 * 
 * @param bytes Number of bytes.
 * @param dp Number of decimal places to display.
 * 
 * @return Formatted string.
 */

function humanFileSize(bytes, dp = 1) {
    const thresh = 1024;

    if (Math.abs(bytes) < thresh) {
        return bytes + ' B';
    }

    const units = ['kB', 'MB', 'GB', 'TB', 'PB', 'EB', 'ZB', 'YB']
    let u = -1;
    const r = 10 ** dp;

    do {
        bytes /= thresh;
        ++u;
    } while (Math.round(Math.abs(bytes) * r) / r >= thresh && u < units.length - 1);


    return bytes.toFixed(dp) + ' ' + units[u];
}


function initClearValidationError() {

    // text input
    $('input.input-validation-error').on('keyup change', function () {
        $(this).removeClass('input-validation-error');
        $(this).parent().find(".field-validation-error").hide();
    });

    $("select.input-validation-error").on('change', function () {
        $(this).closest('input-validation-error').removeClass('input-validation-error');
    });

}

function clearDate(e) {
    $(e).prev().datepicker('setDate', '');
}
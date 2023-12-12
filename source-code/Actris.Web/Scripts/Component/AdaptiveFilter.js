let createAdaptiveFilters = function (gridId, param) {
    let grid = adsGrid[gridId];

    let url = param;
    let modal = {};
    let modalIds = [];
    let filterColumns = [];
    let filterServerFormats = [];

    const prefixFilterListId = "#" + gridId + "_FilterList-";
    const prefixModalId = "#" + gridId + "_FilterModal-"

    function getModalElement(columnId) {
        return $(prefixModalId + columnId);
    }
    function convertToServerFormat() {
        filterServerFormats = [];
        filterColumns.map(filterColumn => {

            let filterPerColumn = [];
            filterColumn.values.map(val => {
                let oneFilter = {
                    Name: filterColumn.columnId,
                    Value: val,
                    FilterType: 'Includes'
                };
                filterPerColumn.push(oneFilter);
            })
            if (filterPerColumn.length > 0) {
                filterServerFormats.push(filterPerColumn);
            }
        });

        return filterServerFormats;
    }

    function updateFilter(columnId) {

        var listChekbox = $(prefixFilterListId + columnId).find(".form-check-list input");
        var oneColumn = filterColumns.find(o => o.columnId == columnId);
        if (!oneColumn) {
            oneColumn = {
                columnId: columnId,
                values: []
            };
            filterColumns.push(oneColumn);
        }

        oneColumn.values = [];
        listChekbox.map((index, item) => {
            var oneChekbox = $(item);

            if (oneChekbox.is(":checked")) {
                oneColumn.values.push(oneChekbox.val());;
            }
        });
        grid.resetPage();
        convertToServerFormat();
    }

    // balikin lagi ke value sebelum submit
    function reset(columnId) {
        let prevFilter = filterColumns.find(o => o.columnId == columnId);

        // clear all checkbox
        modalIds.map(modalId => {
            $(modalId).find("input").prop('checked', false);
        });

        if (!prevFilter) {
            return;
        }

        let modalEl = getModalElement(columnId);
        // check selected value
        prevFilter.values.map(val => {
            modalEl.find("input[value='" + val + "']").prop('checked', true);
        });

    }


    return {
        data: function () {
            return {
                filterColumns,
                filterServerFormats
            }
        },

        getFilters: function () {
            return filterServerFormats;
        },
        showDialog: function (columnId) {
            if (!modal[columnId]) {
                let modalId = prefixModalId + columnId;
                modalIds.push(modalId)

                let modalElement = $(modalId);


                modal[columnId] = new bootstrap.Modal(modalElement);

                let additionalParam = grid.additionalParam;
                $.post(url, { columnId, additionalParam }, function (response) {
                    $(prefixFilterListId + columnId).html($(response));
                })
            }

            // balikin lagi ke value sebelum submit untuk case centang2 tapi ga di submit malah cancel
            reset(columnId);

            modal[columnId].show()
        },

        selectAll: function (e) {
            $(e).parent().parent().find(".form-check-list input").prop('checked', e.checked);
        },

        clearAll: function () {
            // clear variable
            filterColumns = [];
            filterServerFormats = [];

            // uncheck dom
            modalIds.map(modalId => {
                $(modalId).find("input").prop('checked', false);
            });
        },

        // di panggil di luar
        clear: function (columnId) {
            filterColumns = filterColumns.filter(o => o.columnId != columnId);
            $(prefixModalId + columnId).find("input").prop('checked', false);
            updateFilter(columnId);
        },

        submit: function (columnId) {
            updateFilter(columnId);
            grid.reload();
            modal[columnId].hide();
        }
    }
}

let createTextFilters = function (gridId) {
    let grid = adsGrid[gridId];

    let modal = {};
    let inputElementList = [];
    let filterColumns = [];
    let filterServerFormats = [];

    const prefixFilterField = '#TextFilterField-';
    const prefixFilterInput = "#TextFilterInput-";
    const prefixModalId = "#" + gridId + "_TextFilterModal-";

    function convertToServerFormat() {
        filterServerFormats = [];
        filterColumns.map(filterColumn => {

            let filterPerColumn = [];
            filterPerColumn.push(filterColumn);
            filterServerFormats.push(filterPerColumn);
        });

        return filterServerFormats;
    }

    function updateFilter(columnId) {

        var inputValue = $(prefixFilterInput + columnId).val();
        var filterType = $(prefixFilterField + columnId).val();

        if (inputValue) {
            var oneColumn = filterColumns.find(o => o.Name == columnId);
            if (!oneColumn) {
                oneColumn = {
                    Name: columnId,
                    Value: inputValue,
                    FilterType: filterType
                };
                filterColumns.push(oneColumn);
            } else {
                oneColumn.Value = inputValue;
                oneColumn.FilterType = filterType;
            }
        } else {
            filterColumns = filterColumns.filter(o => o.Name != columnId);
        }

        convertToServerFormat();
    }

    return {
        data: function () {
            return {
                filterColumns,
                filterServerFormats
            }
        },

        getFilters: function () {
            return filterServerFormats;
        },
        showDialog: function (columnId) {
            if (!modal[columnId]) {
                let inputElement = prefixFilterInput + columnId;
                inputElementList.push(inputElement);

                let modalId = prefixModalId + columnId;

                let modalElement = $(modalId);
                modal[columnId] = new bootstrap.Modal(modalElement);
            }

            modal[columnId].show()
        },
        // di panggil di luar
        clear: function (columnId) {
            filterColumns = filterColumns.filter(o => o.Name != columnId);
            $(prefixFilterInput + columnId).val(null);
            updateFilter(columnId);
        },
        clearAll: function () {
            // clear variable
            filterColumns = [];
            filterServerFormats = [];

            // uncheck dom
            inputElementList.map(inputElId => {
                $(inputElId).val(null);
            });
        },
        submit: function (columnId) {
            grid.resetPage();
            updateFilter(columnId);
            grid.reload();
            modal[columnId].hide();
        }
    }

}

let createNumberFilters = function (gridId) {
    let grid = adsGrid[gridId];

    let modal = {};
    let inputElementList = [];
    let inputElement2List = [];
    let filterColumns = [];
    let filterServerFormats = [];

    const prefixFilterField = '#NumberFilterField-';
    const prefixFilterInput = "#NumberFilterInput-";
    const prefixModalId = "#" + gridId + "_NumberFilterModal-";
    function convertToServerFormat() {
        filterServerFormats = [];
        filterColumns.map(filterColumn => {

            let filterPerColumn = [];
            if (filterColumn.FilterType == 'Between') {
                filterPerColumn = [{
                    Name: filterColumn.Name,
                    Value: filterColumn.Value[0],
                    FilterType: 'GreaterThanOrEqual'
                }];
                filterServerFormats.push(filterPerColumn);
                filterPerColumn = [{
                    Name: filterColumn.Name,
                    Value: filterColumn.Value[1],
                    FilterType: 'LessThanOrEqual'
                }];
                filterServerFormats.push(filterPerColumn);
            } else {
                filterPerColumn.push(filterColumn);
                filterServerFormats.push(filterPerColumn);
            }
        });

        return filterServerFormats;
    }

    function updateFilter(columnId) {

        var inputValue = $(prefixFilterInput + columnId).val();
        var inputValue2 = $(prefixFilterInput + columnId + '-2').val();
        var filterType = $(prefixFilterField + columnId).val();

        if (filterType == 'Between' && inputValue && inputValue2) {
            var oneColumn = filterColumns.find(o => o.Name == columnId);
            if (!oneColumn) {
                oneColumn = {
                    Name: columnId,
                    Value: [inputValue, inputValue2],
                    FilterType: filterType
                };
                filterColumns.push(oneColumn);
            } else {
                oneColumn.Value = [inputValue, inputValue2];
                oneColumn.FilterType = filterType;
            }
        } else if (filterType == 'Empty' || filterType == 'NotEmpty' || inputValue) {
            var oneColumn = filterColumns.find(o => o.Name == columnId);
            if (!oneColumn) {
                oneColumn = {
                    Name: columnId,
                    Value: inputValue,
                    FilterType: filterType
                };
                filterColumns.push(oneColumn);
            } else {
                oneColumn.Value = inputValue;
                oneColumn.FilterType = filterType;
            }
        } else {
            filterColumns = filterColumns.filter(o => o.Name != columnId);
        }

        convertToServerFormat();
    }

    return {
        data: function () {
            return {
                filterColumns,
                filterServerFormats
            }
        },

        getFilters: function () {
            return filterServerFormats;
        },
        showDialog: function (columnId) {
            if (!modal[columnId]) {
                let inputElement = prefixFilterInput + columnId;
                inputElementList.push(inputElement);
                inputElement2List.push(inputElement + '-2');

                let modalId = prefixModalId + columnId;

                let modalElement = $(modalId);
                modal[columnId] = new bootstrap.Modal(modalElement);
            }

            modal[columnId].show()
        },
        // di panggil di luar
        clear: function (columnId) {
            filterColumns = filterColumns.filter(o => o.Name != columnId);
            $(prefixFilterInput + columnId).val(null);
            $(prefixFilterInput + columnId + '-2').val(null);
            updateFilter(columnId);
        },
        clearAll: function () {
            // clear variable
            filterColumns = [];
            filterServerFormats = [];

            // uncheck dom
            inputElementList.map(inputElId => {
                $(inputElId).val(null);
            });
            inputElement2List.map(inputElId => {
                $(inputElId).val(null);
            });
        },
        submit: function (columnId) {
            updateFilter(columnId);
            grid.resetPage();
            grid.reload();
            modal[columnId].hide();
        },
        fieldSelectChange: function (columnId) {
            var filterType = $(prefixFilterField + columnId).val();
            var inputEl1Id = prefixFilterInput + columnId;
            var inputEl2Id = prefixFilterInput + columnId + '-2';

            if (filterType == 'Between') {
                $(inputEl1Id).show();
                $(inputEl1Id).val(null);

                $(inputEl2Id).show();
                $(inputEl2Id).val(null);
                $("label[for='" + inputEl2Id.substring(1) + "']").show();
            } else if (filterType == 'Empty' || filterType == 'NotEmpty') {
                $(inputEl1Id).hide();
                $(inputEl1Id).val(null);

                $(inputEl2Id).hide();
                $(inputEl2Id).val(null);
                $("label[for='" + inputEl2Id.substring(1) + "']").hide();
            } else {
                $(inputEl1Id).show();
                $(inputEl1Id).val(null);

                $(inputEl2Id).hide();
                $(inputEl2Id).val(null);
                $("label[for='" + inputEl2Id.substring(1) + "']").hide();
            }
        }
    }

}

let createDateFilters = function (gridId) {
    let grid = adsGrid[gridId];

    let modal = {};
    let inputElementList = [];
    let filterColumns = [];
    let filterServerFormats = [];

    const prefixFromDate = '#FromDateFilterInput-';
    const prefixToDate = "#ToDateFilterInput-";
    const prefixModalId = "#" + gridId + "_DateFilterModal-";

    function convertToServerFormat() {
        filterServerFormats = [];
        filterColumns.map(filterColumn => {
            let filterPerColumn = [];
            filterPerColumn.push(filterColumn);
            filterServerFormats.push(filterPerColumn);
        });

        return filterServerFormats;
    }

    function updateFilter(columnId) {
        var fromDateVal = $(prefixFromDate + columnId).val();
        var endDateVal = $(prefixToDate + columnId).val();
        debugger;
        filterColumns = filterColumns.filter(o => o.Name != columnId);

        if (fromDateVal && endDateVal && fromDateVal == endDateVal) {
            filterColumns.push({
                Name: columnId,
                Value: fromDateVal,
                FilterType: 'Equal'
            });
        } else {
            if (fromDateVal) {
                filterColumns.push({
                    Name: columnId,
                    Value: fromDateVal,
                    FilterType: 'GreaterThanOrEqual'
                });
            }

            if (endDateVal) {
                filterColumns.push({
                    Name: columnId,
                    Value: endDateVal,
                    FilterType: 'LessThanOrEqual'
                });
            }
        }
        convertToServerFormat();
    }

   

    return {
        handleStartDate: function(columnId) {
            var elStartDate = $(prefixFromDate + columnId + "_datepicker");
            var elEndDate = $(prefixToDate + columnId + "_datepicker");

            var startDateVal = elStartDate.datepicker('getDate');
            if (!startDateVal) {
                elEndDate.datepicker('setStartDate', null);
                return;
            }

            
            

            var endDateVal = elEndDate.datepicker('getDate');
            if (endDateVal) {
                var mEnd = moment(endDateVal);
                var mStart = moment(startDateVal);
                if (mStart > mEnd) {
                    elEndDate.datepicker('setDate', startDateVal);
                }
            }

            elEndDate.datepicker('setStartDate', startDateVal);
        },
        data: function () {
            return {
                filterColumns,
                filterServerFormats
            }
        },

        getFilters: function () {
            return filterServerFormats;
        },
        showDialog: function (columnId) {
            if (!modal[columnId]) {
                let startDateEl = prefixFromDate + columnId;
                inputElementList.push(startDateEl);

                let endDateEl = prefixToDate + columnId;
                inputElementList.push(endDateEl);

                let modalId = prefixModalId + columnId;

                let modalElement = $(modalId);
                modal[columnId] = new bootstrap.Modal(modalElement);

                initDatePicker();
            }

            modal[columnId].show()
        },
        // di panggil di luar
        clear: function (columnId) {
            filterColumns = filterColumns.filter(o => o.Name != columnId);
            $(prefixFromDate + columnId + "_datepicker").datepicker('setDate', '');
            $(prefixToDate + columnId + "_datepicker").datepicker('setDate', '');
            updateFilter(columnId);
        },
        clearAll: function () {
            // clear variable
            filterColumns = [];
            filterServerFormats = [];

            // clear dom
            inputElementList.map(inputElId => {
                $(inputElId + "_datepicker").datepicker('setDate', '');
            });
        },
        submit: function (columnId) {
            grid.resetPage();
            updateFilter(columnId);
            grid.reload();
            modal[columnId].hide();
        }
    }

}


let createAdvanceSearch = function (gridId) {
    let grid = adsGrid[gridId];

    let filterColumns = [];
    let filterServerFormats = [];

    let advanceSearchOpened = false;
    let lastFilterValues = [];

    const prefixAdvanceSearchSectionId = gridId + "_searchCondition-";
    const prefixFieldSelect = "advancedSearchColumnSelect";
    const prefixOperationSelect = "advancedSearchOperationSelect";
    const prefixInputValue = "advancedSearchValueInput";

    let availableAdvanceSearch = 0;
    let fieldOptionsHtml = '';
    const optionsTextType = '<option value="Equal" selected>equal</option>'
        + '<option value="NotEqual" > not equal</option >'
        + '<option value="BeginWith">begin with</option>'
        + '<option value="NotBeginWith">not begin with</option>'
        + '<option value="Contains">contains</option>'
        + '<option value="NotContains">not contain</option>'
        + '<option value="EndWith">end with</option>'
        + '<option value="NotEndWith">not end with</option>';

    const optionsNumberType = '<option value="Equal" selected>equal</option>'
        + '<option value="NotEqual" > not equal</option>'
        + '<option value="LessThan">less than</option>'
        + '<option value="GreaterThan">greater than</option>'
        + '<option value="LessThanOrEqual">less than or equal</option>'
        + '<option value="GreaterThanOrEqual">greater than or equal</option>'
        + '<option value="Between">between</option>'
        + '<option value="NotEmpty">not empty</option>'
        + '<option value="Empty">empty</option>';

    function updateFilter() {
        var allSearches = $('#advanceSearchSection #operationSection').children('.search-condition');

        filterColumns = [];
        for (var i = 0; i < allSearches.length; i++) {
            var searchDiv = allSearches[i];

            var columnSelect = $.parseJSON($(searchDiv).find('#' + prefixFieldSelect).val());
            var operationSelect = $(searchDiv).find('#' + prefixOperationSelect).val();
            var inputVal = $(searchDiv).find('#' + prefixInputValue).val();

            var input2 = $(searchDiv).find('#' + prefixInputValue + "2");

            if (operationSelect === 'Between' && input2 && input2.val()) {
                let oneColumn = {
                    Name: columnSelect.columnId,
                    Value: inputVal,
                    FilterType: 'GreaterThanOrEqual'
                };
                filterColumns.push(oneColumn);

                let secondColumn = {
                    Name: columnSelect.columnId,
                    Value: input2.val(),
                    FilterType: 'LessThanOrEqual'
                };
                filterColumns.push(secondColumn);

            } else if (operationSelect === 'Empty' || operationSelect === 'NotEmpty' || inputVal) {
                let oneColumn = {
                    Name: columnSelect.columnId,
                    Value: inputVal,
                    FilterType: operationSelect
                };
                filterColumns.push(oneColumn);
            }

        }

        filterServerFormats = [];
        filterColumns.map(filterColumn => {

            let filterPerColumn = [];
            filterPerColumn.push(filterColumn);
            filterServerFormats.push(filterPerColumn);
        });
    };

    function buildNewAdvanceSearch(optionValues) {
        availableAdvanceSearch += 1;
        var newOperationHtml = '<div class="d-flex align-items-center search-condition mt-2 mb-2" id="' + prefixAdvanceSearchSectionId + availableAdvanceSearch + '">'
            + '<select class="form-select me-2" id="advancedSearchColumnSelect" onchange="adsGrid[\'' + gridId + '\'].advanceSearchFilter.setupAdvanceSearchFieldSelect(this, ' + availableAdvanceSearch + ')">'
            + "<option value='{}' selected disabled>Choose filter..</option>";

        if (fieldOptionsHtml.length === 0) {
            fieldOptionsHtml = optionValues;
        }
        newOperationHtml += fieldOptionsHtml;

        newOperationHtml += '</select>'
            + '<select class="form-select ms-2 me-2" id="' + prefixOperationSelect + '" onchange="adsGrid[\'' + gridId + '\'].advanceSearchFilter.setupAdvanceSearchOperationSelect(this, ' + availableAdvanceSearch + ')" disabled>'
            + '<option value=\'Equal\'>Equal</option>'
            + '</select>'
            + '<input class="form-control text-box single-line ms-2 me-2" id="' + prefixInputValue + '" name="" type="text" value="" placeholder="Enter value.." disabled>'
            + '<button class="btn btn-outline-danger btn-square ms-2" onclick="adsGrid[\'' + gridId + '\'].advanceSearchFilter.removeAdvanceSearch(' + availableAdvanceSearch + ')"><i class="fa fa-minus"></i></button>'
            + '</div>';


        $("#operationSection #newSearchSection").before(newOperationHtml);

        return availableAdvanceSearch;
    };

    function setupFilterByFieldSelect(elem, id) {
        var jsonVal = $.parseJSON($(elem).val());
        var selectedValType = jsonVal ? jsonVal.columnType : null;

        var operationSelectIdentifier = "#" + prefixAdvanceSearchSectionId + id + " #" + prefixOperationSelect;
        var inputIdentifier = "#" + prefixAdvanceSearchSectionId + id + " #" + prefixInputValue;

        if (selectedValType === 'String') {
            $(operationSelectIdentifier).html(optionsTextType);
            $(inputIdentifier).prop('type', 'text');

            $(operationSelectIdentifier).prop('disabled', false);
            $(inputIdentifier).prop('disabled', false);
        } else if (selectedValType === 'Number') {
            $(operationSelectIdentifier).html(optionsNumberType);
            $(inputIdentifier).prop('type', 'number');

            $(operationSelectIdentifier).prop('disabled', false);
            $(inputIdentifier).prop('disabled', false);
        } else {
            $(operationSelectIdentifier).html('<option value="">-</option>');
        }
    };

    function setupFilterByOperationSelect(elem, id) {
        var operationVal = $(elem).val();

        var fieldSelectIdentifier = "#" + prefixAdvanceSearchSectionId + id + " #" + prefixFieldSelect;
        var fieldJsonVal = $.parseJSON($(fieldSelectIdentifier).val());

        var fieldValType = fieldJsonVal ? fieldJsonVal.columnType : null;

        var inputIdentifier = "#" + prefixAdvanceSearchSectionId + id + " #" + prefixInputValue;


        function removeMultiInput() {
            var multiInputEl = $("#" + prefixAdvanceSearchSectionId + id + " #multiInput");

            if (multiInputEl) {
                var newInputHtml = '<input class="form-control text-box single-line ms-2 me-2" id="' + prefixInputValue + '" name="" type="text" value="" placeholder="Enter value..">';
                multiInputEl.before(newInputHtml);
                multiInputEl.remove();
            }
        }

        if (fieldValType === 'Number') {
            if (operationVal === 'Empty' || operationVal === 'NotEmpty') {
                removeMultiInput();
                $(inputIdentifier).val(null);
                $(inputIdentifier).attr('placeholder', null);
                $(inputIdentifier).prop('disabled', true);

            } else if (operationVal === 'Between') {
                var inputElem = $(inputIdentifier);

                var firstInputHtml = '<input class="form-control text-box single-line" id="' + prefixInputValue + '" name="" type="number" value="" placeholder="Enter value..">';
                var secondInputHtml = '<input class="form-control text-box single-line" id="' + prefixInputValue + '2" name="" type="number" value="" placeholder="Enter value..">';

                var multiInputHtml = '<div class="input-group ms-2 me-2" id="multiInput">'
                    + firstInputHtml
                    + secondInputHtml
                    + '<div>';

                inputElem.before(multiInputHtml);
                inputElem.remove();

                //inputElem.after(secondInputHtml);

            } else {
                removeMultiInput();
                $(inputIdentifier).attr('placeholder', 'Enter value..');
                $(inputIdentifier).prop('disabled', false);
            }

            $(inputIdentifier).prop('type', 'number');

        } else if (fieldValType === 'String') {
            removeMultiInput();

            $(inputIdentifier).prop('type', 'text');
            $(inputIdentifier).prop('disabled', false);

            $(inputIdentifier).prop('type', 'text');

        } else {
            removeMultiInput();
            $(operationSelectIdentifier).html('<option value="">-</option>');
            $(inputIdentifier).prop('disabled', true);
        }
    };

    function saveLastFilterValues() {
        var allSearches = $('#advanceSearchSection #operationSection').children('.search-condition');

        lastFilterValues = [];
        for (var i = 0; i < allSearches.length; i++) {
            var searchDiv = allSearches[i];

            var columnSelectVal = $(searchDiv).find('#' + prefixFieldSelect).val();
            var operationSelectVal = $(searchDiv).find('#' + prefixOperationSelect).val();
            var inputVal = $(searchDiv).find('#' + prefixInputValue).val();
            var input2 = $(searchDiv).find('#' + prefixInputValue + '2');

            if (inputVal) {
                let oneFilter = {
                    "columnSelect": columnSelectVal,
                    "operationSelect": operationSelectVal,
                    "input": inputVal
                };

                if (input2) {
                    oneFilter["input2"] = input2.val();
                }

                lastFilterValues.push(oneFilter);
            }

        }
    };

    return {
        data: function () {
            return {
                filterColumns,
                filterServerFormats
            }
        },

        getFilters: function () {
            return filterServerFormats;
        },

        // di panggil di luar
        addAdvanceSearch: function (optionValues) {
            buildNewAdvanceSearch(optionValues);
        },

        removeAdvanceSearch: function (id) {
            var advanceSearchIdentifier = "#" + prefixAdvanceSearchSectionId + id;
            $(advanceSearchIdentifier).remove();
        },

        setupAdvanceSearchFieldSelect: function (elem, id) {
            setupFilterByFieldSelect(elem, id);
        },

        setupAdvanceSearchOperationSelect: function (elem, id) {
            setupFilterByOperationSelect(elem, id);
        },

        clearAll: function () {
            // clear variable
            filterColumns = [];
            filterServerFormats = [];

            var allSearches = $('#advanceSearchSection #operationSection').children('.search-condition');

            for (var i = 0; i < allSearches.length; i++) {
                var searchDiv = allSearches[i];

                $(searchDiv).remove();
            }
        },
        submit: function () {
            updateFilter();
            grid.reload();
        },

        updateAdvanceSearchSectionState: function () {
            var e = $('#advanceSearchSection');
            if (e.length > 0) {
                if (e.attr('class').includes("show")) {
                    advanceSearchOpened = true;
                }

                saveLastFilterValues();
            }

        },

        setAdvanceSearchState() {
            if (advanceSearchOpened) {
                $('#advanceSearchSection').addClass("show");
            };

            for (let i = 0; i < lastFilterValues.length; i++) {
                let oneFilter = lastFilterValues[i];

                let newFilterHtmlId = buildNewAdvanceSearch(fieldOptionsHtml);

                var columnSelect = $("#" + prefixAdvanceSearchSectionId + newFilterHtmlId + " #" + prefixFieldSelect);
                columnSelect.val(oneFilter.columnSelect);
                setupFilterByFieldSelect(columnSelect, newFilterHtmlId);

                var operationSelect = $("#" + prefixAdvanceSearchSectionId + newFilterHtmlId + " #" + prefixOperationSelect);
                operationSelect.val(oneFilter.operationSelect);
                setupFilterByOperationSelect(operationSelect, newFilterHtmlId);

                var inputElement = $("#" + prefixAdvanceSearchSectionId + newFilterHtmlId + " #" + prefixInputValue);
                var input2Element = $("#" + prefixAdvanceSearchSectionId + newFilterHtmlId + " #" + prefixInputValue + "2");
                inputElement.val(oneFilter.input);
                if (input2Element) {
                    input2Element.val(oneFilter.input2);
                }

            }
        }
    }

}
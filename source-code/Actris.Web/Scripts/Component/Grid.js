// public variable collection of grid instance
let adsGrid = {};

// initOneGrid and add to adsGrid
let initGrid = function (gridId, controllerName, sizeParam, orderBy) {
    
    // public var
    let _g = {};
    _g.additionalParam = "";
    _g.isForLookup = false;

    // add to grid collection
    if (!adsGrid[gridId]) {
        adsGrid[gridId] = _g;
    }

    let url = {
        gridList: controllerName + '/GridList',
        exportToExcel: controllerName + '/ExportToExcel',
        getAdaptiveFilter: controllerName + '/GetAdaptiveFilter',
        exportToPdf: controllerName + '/ExportToPDF',
    };


    let page = 1;
    let size = sizeParam;

    // filter
    let adaptiveFilter = createAdaptiveFilters(gridId, url.getAdaptiveFilter);
    let textFilter = createTextFilters(gridId);
    let numberFilter = createNumberFilters(gridId);
    let dateFilter = createDateFilters(gridId);
    let quickFindFilter = null;
    let advanceSearchFilter = createAdvanceSearch(gridId);

    // grid parameter
    function buildGridParam() {
        let filters = [];
        let filterIncludes = adaptiveFilter.getFilters();
        let filterText = textFilter.getFilters();
        let filterNumber = numberFilter.getFilters();
        let filterAdvanceSearch = advanceSearchFilter.getFilters();
        let filterDate = dateFilter.getFilters();

        filters = filters.concat(filterIncludes);
        filters = filters.concat(filterText);
        filters = filters.concat(filterNumber);
        filters = filters.concat(filterDate);
        filters = filters.concat(filterAdvanceSearch);

        if (quickFindFilter) {
            filters.push(quickFindFilter);
        }

        let param = {
            isForLookup: _g.isForLookup,
            gridId: gridId,
            page: page,
            size: size,
            orderBy: orderBy,
            filterItems: filters,
            additionalParam: _g.additionalParam,
            readOnly: readOnly
        };


        return param;
    }

    function showGridLoading(isShow) {
        if (isShow) {
            $(".grid-loading-overlay").addClass('show');
        } else {
            $(".grid-loading-overlay").removeClass('show');
        }
    }
    // reload grid ke server
    function reload() {

        let gridParam = buildGridParam();
        console.log(gridParam);

        advanceSearchFilter.updateAdvanceSearchSectionState();

        showGridLoading(true);
        $.post(url.gridList, gridParam, function (newGridList) {
            showGridLoading(false);
            $('#' + gridId).html($(newGridList));
            setTotalItemToTitle();
            advanceSearchFilter.setAdvanceSearchState();
        }).fail(function () {
            showGridLoading(false);
            showToastError('Something went wrong. Please try again later.');
        });
    }

    function showLoading(el, isShow) {
        
        if (isShow) {
            $(el).prop('disabled', true);
            $(el).find("i").hide();
            $(el).find(".spinner-border").show();
        } else {
            $(el).prop('disabled', false);
            $(el).find("i").show();
            $(el).find(".spinner-border").hide();
        }

    }

    // download excel
    function downloadSpreadsheet(e) {
        let gridParam = buildGridParam();
        showLoading(e, true);
        $.ajax({
            url: url.exportToExcel,
            data: gridParam,
            method: 'POST',
            xhrFields: {
                responseType: 'blob'
            },
            success: function (data) {
                var a = document.createElement('a');
                var url = window.URL.createObjectURL(data);
                a.href = url;

                var filename = controllerName.replaceAll("/", " ");
                filename = filename.trim();
                a.download = filename + '-report.xlsx';
                document.body.append(a);
                a.click();
                a.remove();
                window.URL.revokeObjectURL(url);
                showLoading(e, false);
                showToast("Export excel success");
            }
        });

    }

    function downloadPdf() {
        let gridParam = buildGridParam();

        var timeOptions = { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric' };
        let param = {
            filterList: gridParam,
            headerText: new Date().toLocaleDateString('en-EN', timeOptions),
            tableHeaderSizes: [30, 0, 0, 0, 0, 0, 50]
        };
        $.ajax({
            url: url.exportToPdf,
            data: param,
            method: 'POST',
            xhrFields: {
                responseType: 'blob'
            },
            success: function (data) {
                var a = document.createElement('a');
                var url = window.URL.createObjectURL(data);
                a.href = url;
                a.download = controllerName + ' Report.pdf';
                document.body.append(a);
                a.click();
                a.remove();
                window.URL.revokeObjectURL(url);
            }
        });
    }

    // quick find
    function quickFind(formElement) {
        let keyword = $(formElement).find("input").val();
        if (keyword) {
            quickFindFilter = [{
                Name: "AnyField",
                Value: keyword,
                FilterType: 'Contains'
            }];
        } else {
            quickFindFilter = null;
        }
        resetPage();
        reload();
        return false;
    }

    // sorting
    function setOrderBy(columnId, order) {
        orderBy = columnId + " " + order;
        reload();
    }

    // clear all filter
    function clearAllFilter() {
        adaptiveFilter.clearAll();
        textFilter.clearAll();
        numberFilter.clearAll();
        advanceSearchFilter.clearAll();
        dateFilter.clearAll();
        quickFindFilter = null;
        reload();
    }

    // clear one filter
    function clearFilter(columnId) {
        adaptiveFilter.clear(columnId);
        textFilter.clear(columnId);
        numberFilter.clear(columnId);
        dateFilter.clear(columnId);
        reload();
    }

    // page change
    function onPageChange(newPage) {
        page = newPage;
        reload();
    };

    // page size change
    function onPageSizeChange(element) {
        page = 1;
        size = element.value;
        reload();
    };


    // display total item to table title
    function setTotalItemToTitle() {
        if (_g.isForLookup == false) {
            let total = $('#' + gridId).find('.total-label').attr('data-total');

            if (total) {
                $("#crud-grid-count").text("(" + total + ")");
            }

        }
    }
    setTotalItemToTitle();

    var readOnly = false;
    function setReadOnly() {
        readOnly = true;
    }

    function resetPage() {
        page = 1;
    }
    // public property and method
    _g.reload = reload;
    _g.resetPage = resetPage;
    _g.downloadSpreadsheet = downloadSpreadsheet;
    _g.downloadPdf = downloadPdf;
    _g.quickFind = quickFind;
    _g.setOrderBy = setOrderBy;
    _g.clearAllFilter = clearAllFilter;
    _g.clearFilter = clearFilter;
    _g.onPageChange = onPageChange;
    _g.onPageSizeChange = onPageSizeChange;
    _g.adaptiveFilter = adaptiveFilter;
    _g.textFilter = textFilter;
    _g.numberFilter = numberFilter;
    _g.dateFilter = dateFilter;
    _g.advanceSearchFilter = advanceSearchFilter;
    _g.setReadOnly = setReadOnly;
}












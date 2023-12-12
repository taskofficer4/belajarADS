let createCrud = function (crudParam) {
    let controllerName = crudParam.controllerName;
    let crudId = crudParam.crudId;
    let gridId = crudParam.gridId;

    //default param
    let url = {
        create: controllerName + '/Create',
        edit: controllerName + '/Edit',
        detail: controllerName + '/Detail',
        delete: controllerName + '/Delete',
        gridList: controllerName + '/GridList',
        exportToExcel: controllerName + '/ExportToExcel',
        getAdaptiveFilter: controllerName + '/GetAdaptiveFilter',
        createChild: controllerName + '/CreateChild',
    };

    // overide url from param
    if (crudParam.urlCreate) {
        url.create = crudParam.urlCreate;
    }


    let additionalInitForm = null;
    // custom init form
    if (crudParam.additionalInitForm) {
        additionalInitForm = crudParam.additionalInitForm;
    }

    // initiate grid
    initGrid(gridId, controllerName, crudParam.gridConfig.size, crudParam.gridConfig.orderBy);

    // listener form submit setelah form di append di modal
    function initForm() {

        initTextEditor();
        initSelect2();
        initInputTextNumeric();
        initClearValidationError();
        initDatePicker();
        initYearPicker();
        initClearValidationError();
        if (additionalInitForm) {
            additionalInitForm();
        }
        
        // SUBMIT FORM CREATE / UPDATE
        $("#ModalForm form").eq(0).submit(function (e) {

            e.preventDefault(); // avoid to execute the actual submit of the form.

            var form = $(this);
            var actionUrl = form.attr('action');
            var formState = $("#ModalForm form").attr("form-state");
            setModalLoading(true);

        
          
            var formSerialize = form.serialize();
          
            $.ajax({
                type: "POST",
                url: actionUrl,
                data: formSerialize,
                success: function (partialView) {
                    setModalLoading(false);
                    if (partialView) {
                        if (!hasValidResponse(partialView)) {
                            $("#ModalForm").modal('hide');
                            return;
                        }

                        $("#ModalForm .append-here").html($(partialView));
                        initForm();
                        scrollToInputError();
                    } else {
                        var successMessage = "Data has been added";
                        if (formState == "Edit") {
                            successMessage = "Data has been updated"
                        }

                        showToast(successMessage);
                        $("#ModalForm").modal('hide');
                        setTimeout(function () {
                            adsGrid[gridId].reload();
                        }, 300);
                       
                    }
                },
                error: function (data) {
                    setModalLoading(false);
                    handleHttpError(data);
                }

            });

        });
    }

   

    // show modal sambil nunggu form dari server
    function showCreateForm() {
        clearForm("#ModalForm");
        $("#ModalForm").modal('show');
        $.get(url.create, function (partialView) {
            if (!hasValidResponse(partialView)) {
                setTimeout(function () {
                    $("#ModalForm").modal('hide');
                }, 500);
                return;
            }
            $("#ModalForm .append-here").html($(partialView));
            initForm();
        }).fail(function (response) {
            
            handleHttpError(response);

            setTimeout(function () {
                $("#ModalForm").modal('hide');
            }, 500);
        });
    }



    // show modal sambil nunggu form render dari server
    function showEditForm(id) {
        clearForm("#ModalForm");
        $("#ModalForm").modal('show');
        $.get(url.edit + "/" + id, function (partialView) {
            if (!hasValidResponse(partialView)) {
                setTimeout(function () {
                    $("#ModalForm").modal('hide');
                }, 500);
                return;
            }

            $("#ModalForm .append-here").html($(partialView));
            initForm();
        }).fail(function (response) {
            handleHttpError(response);

            setTimeout(function () {
                $("#ModalForm").modal('hide');
            }, 500);
        });
    }

    // show modal sambil nunggu form render dari server
    function showReadonlyForm(id) {
        clearForm("#ModalForm");
        $("#ModalForm").modal('show');
        $.get(url.detail + "/" + id, function (partialView) {
            if (!hasValidResponse(partialView)) {
                setTimeout(function () {
                    $("#ModalForm").modal('hide');
                }, 500);
                return;
            }

            $("#ModalForm .append-here").html($(partialView));


            initTextEditor();
            initInputTextNumeric();

            if (additionalInitForm) {
                additionalInitForm();
            }

        }).fail(function (response) {
            handleHttpError(response);
            setTimeout(function () {
                $("#ModalForm").modal('hide');
            }, 500);
        });
    }



    // show modal confirm delete
    let selectedId = null;
    function showConfirmDelete(id, name) {
        $("#ModalDelete").modal('show');
        $("#ModalDelete .delete-text-name").text(name);
        selectedId = id;
    }



    // confirm delete ke server
    function confirmDelete() {
        setModalLoading(true);

        $.post(url.delete, { id: selectedId }, function (response) {
            setModalLoading(false);
            selectedId = null;
            $("#ModalDelete").modal('hide');
            adsGrid[gridId].reload();

            if (hasValidResponse(response)) {
                showToast('Item has been deleted');
            }
            
        }).fail(function () {
            debugger;
            setModalLoading(false);
            alert("Something went wrong. Please try again later.");
            showToastError('Something went wrong. Please try again later.');
        });
    }


    // listener form submit setelah child form di append di modal
    function initChildForm() {

        // SUBMIT FORM CREATE / UPDATE
        $("form.modal-child-form").submit(function (e) {

            e.preventDefault(); // avoid to execute the actual submit of the form.

            var form = $(this);
            var actionUrl = form.attr('action');
            var formState = form.attr("form-state");
            var formSerialize = form.serialize();

            var listChild = $("#" + currentChildFormId + " input").serialize();
            listChild = listChild.replaceAll(currentChildFormId, "");

            var fieldIdParam = "&fieldId=" + currentChildFormId;
            $.ajax({
                type: "POST",
                url: actionUrl,
                data: formSerialize + "&" + listChild + fieldIdParam,
               
                success: function (partialView) {                    
                    if (partialView.indexOf("modal-child-form") < 0) {
                        $("#" + currentChildFormId).find('.append-child-grid-here').html($(partialView));
                    } else {
                        $(".modal-child-form").remove();

                    }

                    hideChildForm();
                },
                error: function (data) {
                    setModalLoading(false);
                    handleHttpError(data);
                }

            });

        });
    }

    let currentChildFormId = null;
    // show modal sambil nunggu form dari server
    function showChildCreateForm(fieldTypeId) {

        currentChildFormId = fieldTypeId;
        clearForm(".modal-child-form");

        // main modal to left
        $("#ModalForm .modal-content").eq(0).addClass("modal-toleft");
        $(".modal-block-overlay").show();

        // new modal active
        $.get(url.createChild + "/" + fieldTypeId, function (partialView) {
            $("#ModalForm .append-here").append($(partialView));
            initChildForm();
            setTimeout(function () {
                $(".modal-child-form").addClass('show');
            }, 5);

        })
    }


    function hideChildForm() {
        $("#ModalForm .modal-content").eq(0).removeClass("modal-toleft");
        $(".modal-block-overlay").hide();
        $(".modal-child-form.show").removeClass('show');

        setTimeout(function () {
            $(".modal-child-form").remove();
        }, 300);



    }

    function deleteChildRow(e) {
        let rowToBeDelete = $(e).closest('tr');
        rowToBeDelete.remove();
    }

    function toggleModalSize(e) {
        $("#ModalForm .modal-dialog").toggleClass("modal-xl");
        
      
        $(e).find("i").toggleClass("fa-expand");
        $(e).find("i").toggleClass("fa-compress");
    }


    function scrollToInputError() {
        var position = $('.input-validation-error').position();
        $("#ModalForm .modal-body").scrollTop(position.top - 40);
    }

    return {
        url,
        initForm,

        showCreateForm,
        showEditForm,
        showReadonlyForm,
        grid: adsGrid[gridId],
        showConfirmDelete,
        confirmDelete,

        showChildCreateForm,
        deleteChildRow,
        hideChildForm,
        toggleModalSize,

        clearForm
    }
}

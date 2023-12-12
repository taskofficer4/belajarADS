function showLookupModal(e, controllerName, orderBy) {
    $(e).addClass("lookup-active");
    $(".modal-lookup").addClass('show');
    $("#ModalForm .modal-content").eq(0).addClass("modal-toleft");
    $(".modal-block-overlay").show();

    let gridId = controllerName + "Controller_grid";
    if (adsGrid[gridId]) {
        adsGrid[gridId] = null;
    }
    if (!adsGrid[gridId]) {
        $.post(controllerName + "/InitLookupGrid", {
            orderBy
        }, function (response) {
            $('.modal-lookup .modal-body').html($(response));
        });
        initGrid(gridId, controllerName, 10, orderBy);
        adsGrid[gridId].isForLookup = true;
    }
}

function selectLookupRow(value, text) {
    $(".select-lookup.lookup-active").prev().val(value);
    $(".select-lookup.lookup-active").val(text);

    hideLookupModal();
}

function hideLookupModal() {
    $(".lookup-active").removeClass("lookup-active");
    $(".modal-lookup").removeClass('show');
    $("#ModalForm .modal-content").eq(0).removeClass("modal-toleft");
    $(".modal-block-overlay").hide();
}
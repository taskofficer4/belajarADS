// clear modal form ketika mau di show dengan tampilan loading spinner
function showLoading(id) {
    var loadingOverlay = '<div class="modal-body text-center"><div class="spinner-border my-5"></div></div>';
    $(id + " .append-here").html(loadingOverlay);
}

function showCaListModal(actionTrackingId) {
    showLoading("#ModalListCa");
    $("#ModalListCa").modal('show');

    $.get("ActionTrackingReport/GetCaList?actionTrackingId=" + actionTrackingId, function (partialView) {
        $("#ModalListCa .append-here").html($(partialView));

    }).fail(function (response) {

        setTimeout(function () {
            $("#ModalListCa").modal('hide');
        }, 500);
    });
}
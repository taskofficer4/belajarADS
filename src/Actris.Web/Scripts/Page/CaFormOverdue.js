// show modal sambil nunggu form dari server
function showOverdueModal() {
   var modalId = "#ModalOverdue";
   clearForm(modalId);
   $(modalId).modal('show');

   var caId = $("#CorrectiveActionID").val();

   var url = baseController + "/OverdueShowForm/" + caId;

   $.ajax({
      type: "GET",
      url: url,
      success: function (partialView) {
         $(modalId + " .append-here").html($(partialView));
         initOverdueForm();
      },
      error: function (data) {
         setModalLoading(false);
         handleHttpError(data);
      }
   });
}



function initOverdueForm() {
   initSelect2();
   setupTextAreaCounter('#OverdueMitigation');
   setupTextAreaCounter('#OverdueReason');
   initClearValidationError();

   var modalId = "#ModalOverdue";

   // SUBMIT CA FORM CREATE / UPDATE
   $(modalId + " form").eq(0).submit(function (e) {

      e.preventDefault(); // avoid to execute the actual submit of the form.

      var form = $(this);
      var actionUrl = form.attr('action');

      setModalLoading(true);

      var formSerialize = form.serialize();

      formSerialize += "&CorrectiveActionID=" + $("#CorrectiveActionID").val();
      $.ajax({
         type: "POST",
         url: actionUrl,
         data: formSerialize,
         success: function (partialView) {
            if (partialView) {
               if (!hasValidResponse(partialView)) {
                  return;
               }
               $(modalId + " .append-here").html($(partialView));
               initOverdueForm();
            } else {
               var url = baseHostUrl + "TaskList/Overdue";
               window.location.href = url;
            }
         },
         error: function (data) {
            setModalLoading(false);
            handleHttpError(data);
         }
      });
   });
}


$(document).ready(function () {

   initDatePicker();
   initSelect2();
   initClearValidationError();

   var isSubmited = false;
   $('form').submit(function () {
      if (isSubmited) {
         e.preventDefault(); // avoid to execute the actual submit of the form.
         return;
      }
      isSubmited = true;
      showLoadingOverlay();
   });


});
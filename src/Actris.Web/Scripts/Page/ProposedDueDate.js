// show modal sambil nunggu form dari server
function showProposedDueDateModal() {
   var modalId = "#ProposedDueDateModal";
   clearForm(modalId);
   $(modalId).modal('show');

   var caId = $("#CorrectiveActionID").val();

   var url = baseController + "/ProposedDueDateEdit/" + caId;

   $.ajax({
      type: "GET",
      url: url,
      success: function (partialView) {
         $(modalId + " .append-here").html($(partialView));
         initProposedDueDateForm();
      },
      error: function (data) {
         setModalLoading(false);
         handleHttpError(data);
      }
   });
}



function initProposedDueDateForm() {
   
   initDatePicker();
   setupTextAreaCounter('#ProposedDueDateData');
   initClearValidationError();

   var modalId = "#ProposedDueDateModal";

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
               initProposedDueDateForm();
            } else {
               var url = baseHostUrl + "TaskList/NeedAction";
               window.location.href =  url;
            }
         },
         error: function (data) {
            setModalLoading(false);
            handleHttpError(data);
         }
      });
   });
}

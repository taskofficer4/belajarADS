function onSaveClick() {
   $("#IsSubmit").val("False");
   $('#mainForm').submit();
}

function onSubmitClick() {
   $("#IsSubmit").val("True");
   $('#mainForm').submit();
}

$(document).ready(function () {

   initDatePicker();
   initSelect2();
   initClearValidationError();
   setupTextAreaCounter('#FollowUpPlan');

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
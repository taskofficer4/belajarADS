function confirmRemarkOnChange() {
   var remark = $("#ConfirmRemark").val();

   if (!remark) {
      $("#ApprovalSubmitBtn").attr("disabled", true);
   } else {
      $("#ApprovalSubmitBtn").removeAttr("disabled");
   }
}

function showApprovalModal(approvalAction) {
   $("#ApprovalSubmitBtn").attr("disabled", true);
   $("#ConfirmRemark").val('');
   $("#ModalConfirmApproval").modal('show');
   $("#ApprovalAction").val(approvalAction);

   if (approvalAction == "Cancel") {
      approvalAction = "Cancel Corrective Action";
   }
   $("#ApprovalSubmitBtn").text(approvalAction);
}

function onApprovalSubmit() {
   var approvalAction = $("#ApprovalAction").val();

   var confirmMessage = "";
   if (approvalAction == "Cancel") {
      confirmMessage = "Cancelled";
   } else if (approvalAction == "Approve") {
      confirmMessage = "Approved";
   } else if (approvalAction == "Reject") {
      confirmMessage = "Rejected";
   }
   if (confirm('Request will be ' + confirmMessage +' and it will be sent to related PIC to enable submit for completion\nAre you sure to continue?')) {
      $("#Remark").val($("#ConfirmRemark").val());
      $('#mainForm').submit();
   }
}

$(document).ready(function () {

   initDatePicker();
   setupTextAreaCounter('#FollowUpPlan');
   setupTextAreaCounter('#ConfirmRemark');

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
// show modal sambil nunggu form dari server
function showApprovalResult(partialView) {
	var modalId = "#ApprovalResultModal";
	$(modalId + " .append-here").html($(partialView));
	$(modalId).modal('show');
}

function countInputCheck() {
	var listCheckInput = $(".form-check-input");
	var checkCounter = 0;

	for (var i = 0; i < listCheckInput.length; i++) {
		var el = listCheckInput[i];
		if (el.checked) {
			checkCounter++;
		}
	}


	return checkCounter;
}
function listData() {

	var listWillSubmit = [];

	var listCheckedInput = $("input:checked");
	for (var i = 0; i < listCheckedInput.length; i++) {
		var inputEl = listCheckedInput.eq(i);
		var tr = inputEl.closest("tr");
		listWillSubmit.push({
			CorrectiveActionID: tr.attr("CorrectiveActionID"),
			ApprovalAction: inputEl.attr('title'),
			Remark: tr.find('.Remark').val(),
		});
	}
	return listWillSubmit;

}
function showApprovalConfirmationModal() {
	var modalId = "#ApprovalConfirmationModal";
	$(modalId).modal('show');

	var listWillSubmit = listData();

	var listRow = $("<div></div>");
	listWillSubmit.forEach(item => {
		var templateRow = '';
		if (item.ApprovalAction == 'Approve') {
			templateRow = $("#templateApproveRow").html();
		} else {
			templateRow = $("#templateRejectRow").html();
		}
		templateRow = $(templateRow);
		templateRow.find(".approvalAction").text(item.ApprovalAction);

		var remark = item.Remark;
		if (remark) {
			templateRow.find(".remark").html(remark);
		}
		
		templateRow.find(".caId").text(item.CorrectiveActionID);
		listRow.append(templateRow);
	});
	
	$(modalId + " .append-here").html(listRow);
}

function onSubmitClick() {
	var data = listData();
	setModalLoading(true);
	$.ajax({
		type: "POST",
		url: baseController+ '/BulkApproval',
		data: JSON.stringify(data),
		contentType: "application/json; charset=utf-8",
		success: function (partialView) {
			$("#ApprovalConfirmationModal").modal('hide');
			showApprovalResult(partialView);
		}
	});
}
function setupSubmitBtn() {
	var counter = countInputCheck();
	var btnSubmit = $("#btnSubmit");
	if (counter == 0) {
		btnSubmit.attr('disabled', true)
		btnSubmit.text('Submit');
	} else {
		btnSubmit.removeAttr('disabled')
		btnSubmit.text('Submit (' + counter + ')');
	}

}

function onApproveChange(e) {
	var parent = $(e).closest("tr");

	if (e.checked) {
		parent.find('.IsReject').prop('checked', false);
	}
	setupSubmitBtn();
	
}


function onRejectChange(e) {
	var parent = $(e).closest("tr");

	if (e.checked) {
		parent.find('.IsApprove').prop('checked', false);
	}
	setupSubmitBtn();
}


$(document).ready(function () {
	$(".form-check-input.IsApprove").change(function () {
		onApproveChange(this);
	});

	$(".form-check-input.IsReject").change(function () {
		onRejectChange(this);
	});
});
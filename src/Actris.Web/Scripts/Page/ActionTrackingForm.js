var baseController = '';


function onSaveClick() {
	$("#IsSubmit").val("False");
	if (confirm('Action Tracking & Corrective Action will be saved and can\'t be deleted. \nAre you sure to save as a draft?')) {
		$('#mainForm').submit();
	}
}

function onTes() {
	$("#IsSubmit").val("False");
	if (confirm('example tes ?')) {
		//$('#mainForm').submit();
	}
}


function onSubmitClick() {
	$("#IsSubmit").val("True");
	if (true) {

	}

	if (!isCaAny()) {
		alert('Please add Corrective Action before submit');
		window.scrollTo(0, document.body.scrollHeight);
		return;
	}

	if (confirm('Corrective Action will be sent to selected Manager and can\'t be cancelled. \nAre you sure to submit ? ')) {
		$('#mainForm').submit();
	}
}

function isCaAny() {
	if ($("#CaListTable tr").length > 0) {
		return true;
	} 
	return false;
}


function scrollToInputError() {
	var position = $('.input-validation-error').position();
	$("#CaModal .modal-body").scrollTop(position.top - 40);
}




function getParamLookup() {
	return {
		directorateID: $("#DirectorateRegionalID").val(),
		divisionID: $("#DivisiZonaID").val(),
		companyCode: $("#CompanyCode").val()
	};
}


function loadDivisiZonaList() {
	var $elTarget = $("#DivisiZonaID");
	var url = baseController + "/GetDivisionList";
	$elTarget.html('');
	loadCompanyList();
	var paramLookup = getParamLookup();
	if (!!paramLookup.directorateID == false) {
		return;
	}

	$.get(url, getParamLookup(), function (lookupList) {
		//$(".activity-loading").hide();
		$elTarget.append("<option></option>")

		for (var i = 0; i < lookupList.Items.length; i++) {
			var item = lookupList.Items[i];
			if (item.Value) {
				$elTarget.append("<option value=\"" + item.Value + "\">" + item.Text + "</option>");
			}
		}
	});
}

function loadCompanyList() {
	var $elTarget = $("#CompanyCode");

	var url = baseController + "/GetCompanyList";
	$elTarget.html('');
	loadWilayahKerjaList();
	loadLocationList();
	var paramLookup = getParamLookup();
	if (!!paramLookup.divisionID == false) {
		return;
	}
	$.get(url, paramLookup, function (lookupList) {
		//$(".activity-loading").hide();
		$elTarget.append("<option></option>")

		for (var i = 0; i < lookupList.Items.length; i++) {
			var item = lookupList.Items[i];
			if (item.Value) {
				$elTarget.append("<option value=\"" + item.Value + "\">" + item.Text + "</option>");
			}
		}
	});
}


function loadWilayahKerjaList() {
	var $elTarget = $("#WilayahkerjaID");

	var url = baseController + "/GetWilayahKerjaList";
	$elTarget.html('');
	var paramLookup = getParamLookup();
	if (!!paramLookup.companyCode == false) {
		return;
	}

	$.get(url, paramLookup, function (lookupList) {
		//$(".activity-loading").hide();
		$elTarget.append("<option></option>")

		for (var i = 0; i < lookupList.Items.length; i++) {
			var item = lookupList.Items[i];
			if (item.Value) {
				$elTarget.append("<option value=\"" + item.Value + "\">" + item.Text + "</option>");
			}
		}
	});
}



function loadLocationList() {
	var $elTarget = $("#LocationID");
	$elTarget.html('');
	var paramLookup = getParamLookup();
	if (!!paramLookup.companyCode == false) {
		return;
	}
	$.get(baseController + "/GetLocationCompanyList", paramLookup, function (lookupList) {
		//$(".activity-loading").hide();
		$elTarget.append("<option></option>")

		for (var i = 0; i < lookupList.Items.length; i++) {
			var item = lookupList.Items[i];
			if (item.Value) {
				$elTarget.append("<option value=\"" + item.Value + "\">" + item.Text + "</option>");
			}
		}
	});
}


$(document).ready(function () {
	initSelect2();
	initDatePicker();
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

	// setup text area counter
	setupTextAreaCounter('#FindingDesc');
	setupTextAreaCounter('#Nct');
	setupTextAreaCounter('#Rca');

});
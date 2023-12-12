var baseHostUrl = "";

function showToast(message) {
   $("#toastSuccess .toast-body").text(message)
   $("#toastSuccess").toast('show');
}

function showToastError(message) {
   $("#toastError .toast-body").text(message)
   $("#toastError").toast('show');
}


var anElements = {};
function initInputTextNumeric() {

   var lsNumeric = $(".autoNumeric");
   listAutonumeric = [];
   for (var i = 0; i < lsNumeric.length; i++) {

      var e = lsNumeric[i];

      var max = 100000000000000;
      var min = 0;
      var optMax = $(e).attr('max');
      if (optMax) {
         max = optMax;
      }

      var optMin = $(e).attr('min');
      if (optMin) {
         min = optMin;
      }

      var option = {
         unformatOnSubmit: true,
         maximumValue: max,
         minimumValue: min
      };

      var suffixText = $(e).attr('suffixText');
      if (suffixText) {
         option.suffixText = suffixText;
      }

      var decimalPlaces = $(e).attr('decimalPlaces');
      if (decimalPlaces) {
         option.decimalPlaces = decimalPlaces;
      }


      var anElement = new AutoNumeric(e, option);
      anElements[e.getAttribute('id')] = anElement;
   }
}


function initTextEditor() {

   var lsTxt = $(".text-editor");

   for (var i = 0; i < lsTxt.length; i++) {
      var e = lsTxt.eq(i);
      var placeholder = e.attr("placeholder");
      if (!placeholder) {
         placeholder = "Select " + ToTitleCase(e.attr('id'));
      }
      if (e.hasClass("input-validation-error")) {
         e.parent().addClass("input-validation-error");
      }

      var isDisabled = $(e).closest('fieldset').attr("disabled");
      if (isDisabled) {
         e.summernote({
            toolbar: [],
            placeholder: placeholder,
            tabsize: 2,
            height: 80,
         });
         e.summernote('disable');
      } else {
         e.summernote({
            toolbar: [
               ['style', ['bold', 'italic', 'underline', 'strikethrough']],
               ['para', ['ol', 'ul']],

            ],
            placeholder: placeholder,
            tabsize: 2,
            height: 80,
            callbacks: {
               onChange: function () {
                  var containerDiv = $(this).closest(".mb-3");
                  containerDiv.removeClass("input-validation-error")
                  containerDiv.find(".field-validation-error").hide();
               },
               onImageUpload: function (data) {
                  data.pop();
               }
            }
         });
      }


   }

   $(".text-editor")
}

function ToTitleCase(text) {
   const result = text.replace(/([A-Z])/g, " $1");
   const finalResult = result.charAt(0).toUpperCase() + result.slice(1);

   return finalResult;
}

function initSelect2() {

   var lsSelect2 = $(".init-select2");

   for (var i = 0; i < lsSelect2.length; i++) {
      var e = lsSelect2.eq(i);
      var placeholder = e.attr("placeholder");
      if (!placeholder) {

         if (e.attr('id')) {
            placeholder = "Select " + ToTitleCase(e.attr('id'));
         }

      }
      if (e.hasClass("input-validation-error")) {
         e.parent().addClass("input-validation-error");
      }





      var select2Option = {
         placeholder: placeholder,
         allowClear: true,
         width: "100%"
      };


      var tag = e.attr("tags");
      if (tag) {
         select2Option.tags = true;
         select2Option.templateSelection = function (selection) {
            if (selection.selected == undefined) {
               return selection.text;
            }
            if (selection.selected) {
               return selection.text;
            }
            else {
               return $.parseHTML('<div class="d-flex align-items-center"> <div>' + selection.text + '</div><div class="ms-auto select2-onthefly-item">New</div></div>');
            }
         };

         select2Option.createTag = function (params) {
            var term = $.trim(params.term);

            if (term === '') {
               return null;
            }

            return {
               id: term,
               text: term,
               newTag: true // add additional parameters
            }
         };

         select2Option.templateResult = function (item) {
            if (!item) {
               return;
            }

            if (item.newTag) {
               return $.parseHTML('<div class="d-flex align-items-center"> <div>' + item.text + '</div><div class="ms-auto select2-onthefly-item me-0">New</div></div>');
            }

            return item.text;
         };
      }

      var parentModal = e.closest('.modal');
      if (parentModal.length > 0) {
         select2Option.dropdownParent = parentModal;
      }


      e.select2(select2Option).on('select2:unselecting', function () {
         $(this).data('unselecting', true);
      }).on('select2:opening', function (e) {
         if ($(this).data('unselecting')) {
            $(this).removeData('unselecting');
            e.preventDefault();
         }
      }).on("change", function (e) {
         $(e.target).closest('.mb-3').removeClass('input-validation-error');
         $(e.target).closest('.mb-3').find(".input-validation-error").removeClass('input-validation-error');
         $(e.target).closest('.mb-3').find(".field-validation-error").hide()
      });




   }

}

function initMultiselect() {
   var lsSelect2 = $(".init-multiselect");

   for (var i = 0; i < lsSelect2.length; i++) {
      var e = lsSelect2.eq(i);

      var placeholder = e.attr("placeholder");
      if (!placeholder) {
         if (e.attr('id')) {
            placeholder = "Select " + ToTitleCase(e.attr('id')).trim();
         }
      }
      let option = {
         placeholder: placeholder,
         closeOnSelect: false,
         dropdownCssClass: 'multicheckbox',
         allowClear: true,
         width: "100%"
      };

      var maxSelectionLength = e.attr("maximumSelectionLength");
      if (maxSelectionLength) {

         option.maximumSelectionLength = maxSelectionLength;
      }

      e.select2(option)
         .on('select2:unselecting', function () {
            $(this).data('unselecting', true);
         }).on('select2:opening', function (e) {
            if ($(this).data('unselecting')) {
               $(this).removeData('unselecting');
               e.preventDefault();
            }
         }).on('select2:opening select2:closing', function (event) {

         }).on("change", function (e) {
            $(e.target).closest('.mb-3').removeClass('input-validation-error');
            $(e.target).closest('.mb-3').find(".input-validation-error").removeClass('input-validation-error');
            $(e.target).closest('.mb-3').find(".field-validation-error").hide()
         });


   }



}

var listDatePicker = [];
function initDatePicker(elementId) {
   var listMonthPicker = $(".init-datepicker");


   function syncDateToHiddenEl(event) {
      let rawDate = $(event.currentTarget).datepicker('getDate');


      var btnClear = $(event.currentTarget).next();
      if (btnClear.hasClass('btn-input-clear')) {
         if (rawDate) {
            btnClear.show();
         } else {
            btnClear.hide();
         }
      }


      let formatedDate = moment(rawDate).format('YYYY-MM-DD');
      var originalId = event.currentTarget.getAttribute('original-id')
      if (rawDate) {
         $("#" + originalId).val(formatedDate);
      } else { $("#" + originalId).val(''); }

   }



   function initOneDatePicker(elementId) {
      let el = $("#" + elementId);
      el.removeClass("init-datepicker");
      el.addClass("datepicker");
      var originalValue = $(el).val();
      var originalElement = '<input type="hidden" id="@id" value="@value" name="@id"  />'
      originalElement = originalElement
         .replaceAll("@id", elementId)
         .replaceAll("@value", originalValue);

      $(el).parent().append(originalElement);
      $(el).attr('id', elementId + "_datepicker");
      $(el).attr('original-id', elementId);

      var option = {
         autoclose: true,
         orientation: "bottom auto",
         templates: {
            leftArrow: '<i class="fa-solid fa-angle-left"></i>',
            rightArrow: '<i class="fa-solid fa-angle-right"></i>'
         },
         maxViewMode: 2,
         format: 'dd-M-yyyy',
         weekStart: 1

      };


      $(el).datepicker(option).on('changeDate', function (e) {
         syncDateToHiddenEl(e);
         // clear warning if any
         $(this).removeClass('input-validation-error');
         $(this).parent().parent().find(".field-validation-error").hide();

         $(this).closest('input-validation-error').removeClass('input-validation-error');
      });

      if (originalValue) {
         $(el).datepicker('setDate', new Date(originalValue));
      }
   }

   // define one el
   if (!!elementId == true) {
      initOneDatePicker(elementId);
      return;
   }

   for (var i = 0; i < listMonthPicker.length; i++) {
      var el = listMonthPicker[i];
      var elementId = $(el).attr('id');
      initOneDatePicker(elementId);
   }
}



function initMonthPicker() {
   var listMonthPicker = $(".monthpicker");


   function syncDateToHiddenEl(event) {
      let rawDate = $(event.currentTarget).datepicker('getDate');
      let formatedDate = moment(rawDate).format('YYYY-MM-DD');
      var originalId = event.currentTarget.getAttribute('original-id')
      if (rawDate) {
         $("#" + originalId).val(formatedDate);
      } else { $("#" + originalId).val(''); }

   }
   for (var i = 0; i < listMonthPicker.length; i++) {
      var el = listMonthPicker[i];
      var elementId = $(el).attr('id');

      var originalValue = $(el).val();
      var originalElement = '<input type="hidden" id="@id" value="@value" name="@id"  />'
      originalElement = originalElement
         .replaceAll("@id", elementId)
         .replaceAll("@value", originalValue);

      $(el).parent().append(originalElement);
      $(el).attr('id', elementId + "_datepicker");
      $(el).attr('original-id', elementId);


      var option = {
         autoclose: true,
         orientation: "bottom auto",
         templates: {
            leftArrow: '<i class="fa-solid fa-angle-left"></i>',
            rightArrow: '<i class="fa-solid fa-angle-right"></i>'
         },
         minViewMode: 1,
         maxViewMode: 2,
         format: 'M yyyy'

      };

      var minDate = $(el).attr("minDate");
      if (minDate) {
         option.startDate = new Date(minDate);
      }

      var maxDate = $(el).attr("maxDate");
      if (maxDate) {
         option.endDate = new Date(maxDate);
      }

      $(el).datepicker(option).on('changeDate', function (e) {
         syncDateToHiddenEl(e);
      });

      $(el).datepicker('setDate', new Date(originalValue));

      //$(el).change(function (event) {
      //    syncDateToHiddenEl(e);
      //});
   }
}


function initYearPicker() {
   var listMonthPicker = $(".yearpicker");

   function showHideClearBtn(e) {
      var rawDate = $(e).val();
      var btnClear = $(e).next();
      if (btnClear.hasClass('btn-input-clear')) {
         if (rawDate) {
            btnClear.show();
         } else {
            btnClear.hide();
         }
      }
   }

   for (var i = 0; i < listMonthPicker.length; i++) {
      var el = listMonthPicker[i];
      showHideClearBtn(el);
      $(el).datepicker({
         autoclose: true,
         orientation: "bottom auto",
         templates: {
            leftArrow: '<i class="fa-solid fa-angle-left"></i>',
            rightArrow: '<i class="fa-solid fa-angle-right"></i>'
         },
         minViewMode: 2,
         maxViewMode: 2,
         format: 'yyyy'
      }).on('changeDate', function (e) {
         showHideClearBtn(e.currentTarget);

         // clear warning if any
         $(this).removeClass('input-validation-error');
         $(this).parent().parent().find(".field-validation-error").hide();

         $(this).closest('input-validation-error').removeClass('input-validation-error');
      });
   }
}


/**
 * Format bytes as human-readable text.
 * 
 * @param bytes Number of bytes.
 * @param dp Number of decimal places to display.
 * 
 * @return Formatted string.
 */

function humanFileSize(bytes, dp = 1) {
   const thresh = 1024;

   if (Math.abs(bytes) < thresh) {
      return bytes + ' B';
   }

   const units = ['kB', 'MB', 'GB', 'TB', 'PB', 'EB', 'ZB', 'YB']
   let u = -1;
   const r = 10 ** dp;

   do {
      bytes /= thresh;
      ++u;
   } while (Math.round(Math.abs(bytes) * r) / r >= thresh && u < units.length - 1);


   return bytes.toFixed(dp) + ' ' + units[u];
}

function initClearValidationError() {

   // text input
   $('input.input-validation-error').on('keyup change', function () {
      $(this).removeClass('input-validation-error');
      $(this).parent().find(".field-validation-error").hide();
      $(".field-validation-error").parent().parent().find('label').removeClass("is-error");
   });

   // text area
   $('textarea.input-validation-error').on('keyup change', function () {
      $(this).removeClass('input-validation-error');
      $(this).parent().find(".field-validation-error").hide();
      $(".field-validation-error").parent().parent().find('label').removeClass("is-error");
   });

   // select2
   $("select.input-validation-error").on('change', function () {
      var parentEl = $(this).parent().closest('.input-validation-error');
      parentEl.removeClass('input-validation-error');
      parentEl.find(".field-validation-error").hide();
      $(".field-validation-error").parent().parent().find('label').removeClass("is-error");
   });

}

function clearDate(e) {
   $(e).prev().datepicker('setDate', '');
}

function showLoadingOverlay() {
   var loadingHtml = '<div class="loading-overlay"><div class=card><div class="card-body fw-bold"><span aria-hidden=true class="me-2 spinner-border spinner-border-sm" role=status></span>Loading...</div></div></div>';
   $("body").append(loadingHtml);
}

function hideLoadingOverlay() {
   $(".loading-overlay").remove();
}

function setupTextAreaCounter(elementSelector) {
   $(elementSelector + '_counter').text($(elementSelector).val().length);
   $(elementSelector).on('keyup', function () {
      $(elementSelector + '_counter').text(this.value.length);
   });
}


// show error message berkaitan dengan roles / error dari backend
function hasValidResponse(response) {

   // response html page
   var err = $(response).find("#pageErrorMessage").text();
   if (err) {
      showToastError(err);
      return false;
   }

   // response json
   if (response.hasOwnProperty('IsSuccess')) {
      if (response.IsSuccess == false) {
         showToastError(response.ErrorMessage);
         return false;
      }
      return true;
   }
   return true;
}


function handleHttpError(data) {
   // internal server error
   if (data.status == 500) {
      var errMsg = $(data.responseText).find('h2').text().trim();
      if (!errMsg) {
         errMsg = $(data.responseText).find('.header-page-small').text().trim();
      }
      showToastError(errMsg);
   } else {
      if (data.readyState == 0) {
         showToastError('Network Error. Please check your internet connection.');
      } else {
         showToastError('Something went wrong. Please try again later.');
      }

   }
}



// clear modal form ketika mau di show dengan tampilan loading spinner
function clearForm(id) {
   var loadingOverlay = '<div class="modal-content"><div class="modal-body text-center"><div class="spinner-border my-5"></div></div></div>';
   $(id + " .append-here").html(loadingOverlay);
}


// set modal loading untuk form / delete
let submitBtnTextTemp = '';
function setModalLoading(isShow) {
   if (isShow) {

      $(".modal.show button").prop("disabled", true);
      submitBtnTextTemp = $(".modal.show button[type='submit']").eq(0).text();

      if (submitBtnTextTemp) {
         $(".modal.show button[type='submit']").html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>');
      } else {
         submitBtnTextTemp = $(".modal.show button.btn-will-loading").text();
         $(".modal.show button.btn-will-loading").html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>');
      }


   } else {

      $(".modal.show button").prop("disabled", false);
      $(".modal.show button[type='submit']").html(submitBtnTextTemp);
      $(".modal.show button.btn-will-loading").html(submitBtnTextTemp);
   }
}


function uploadFile(e,projectPhase,documentType) {

   var parentElement = $(e).closest(".attachment-section");
   var parentRenderElement = $(e).closest(".renderAttachment");

   const formData = new FormData();

   // dikumpulin di ui untuk di submit sekaligus
   var attachmentList = parentElement.find(".attachment-row");
   for (var i = 0; i < attachmentList.length; i++) {
      var attachmentItem = attachmentList.eq(i);

      var indexArray = "existingList[" + i + "].";
      formData.append(indexArray + "FilePath", attachmentItem.attr("FilePath"));
      formData.append(indexArray + "FileUrl", attachmentItem.attr("FileUrl"));
      formData.append(indexArray + "FileName", attachmentItem.attr("FileName"));
      formData.append(indexArray + "FileDescription", attachmentItem.attr("FileDescription"));
      formData.append(indexArray + "CreatedBy", attachmentItem.attr("CreatedBy"));
      formData.append(indexArray + "FileSize", attachmentItem.attr("FileSize"));
      formData.append(indexArray + "ProjectPhase", attachmentItem.attr("ProjectPhase"));
      formData.append(indexArray + "DocumentType", attachmentItem.attr("DocumentType"));
   }
   var inputFile = parentElement.find('input[type="file"]');
   var inputDescription = parentElement.find('input[type="text"]');

   var selectedFile = inputFile.get(0).files[0];
   formData.append("file", selectedFile);
   formData.append("description", inputDescription.val());
   formData.append("projectPhase", projectPhase);
   formData.append("documentType", documentType);

   var fileSize = selectedFile.size;
   var maxFileSize = 5 * 1028 * 1028;

   if (fileSize > maxFileSize) {
      parentElement.find(".field-validation-error").text("File must be lower than " + humanFileSize(maxFileSize) + ". Your file : " + humanFileSize(fileSize));
      parentElement.find(".field-validation-error").show();
      return;
   }

   var buttonEl = $(e);
   buttonEl.attr("disabled",true);
   buttonEl.find("i").remove();
   buttonEl.prepend('<span class="me-2 spinner-border spinner-border-sm"></span>');
   var progressEl = $("<div class='progress-bar'></div>")
   buttonEl.append(progressEl);
   $.ajax({
      url: baseHostUrl + '/Attachment/UploadFile',
      type: 'POST',
      data: formData,
      processData: false,
      contentType: false,
      async: true,
      cache: false,
      enctype: "multipart/form-data",
      success: function (response) {
         $(e).val('');
         parentRenderElement.html(response);
         buttonEl.removeAttr("disabled");
         progressEl.remove();
      },
      xhr: function () {
         var myXhr = $.ajaxSettings.xhr();
         if (myXhr.upload) {
            myXhr.upload.addEventListener('progress', function (event) {

               var percent = 0;
               var position = event.loaded || event.position;
               var total = event.total;
               if (event.lengthComputable) {
                  percent = Math.ceil(position / total * 100);
               }

               var percentText = percent + "%";
               progressEl.css("width", percentText);

            }, false);
         }
         return myXhr;
      },
   });
}


function onFileChoose(e) {
   var parentElement = $(e).closest('.attachment-section');
   var btnAdd = parentElement.find(".btn-add-attachment");
   var inputFile = parentElement.find("input[type='file']");
   if (inputFile.val()) {
      var fileInput = inputFile.get(0).files[0];
      if (!validateFile(fileInput)) {
         inputFile.val('');
         return;
      }
      btnAdd.removeAttr('disabled');
   } else {
      btnAdd.attr('disabled', 'disabled');
   }
}



function validateFile(fileInput) {
   var _validFileExtensions = [".pdf", ".xls", ".xslx", ".docx", ".ppt", ".pptx", ".jpg", "png", "bpm"];
   var sFileName = fileInput.name;
   if (sFileName.length > 0) {
      var blnValid = false;
      for (var j = 0; j < _validFileExtensions.length; j++) {
         var sCurExtension = _validFileExtensions[j];
         if (sFileName.substr(sFileName.length - sCurExtension.length, sCurExtension.length).toLowerCase() == sCurExtension.toLowerCase()) {
            blnValid = true;
            break;
         }
      }

      if (!blnValid) {
         alert("Sorry, " + sFileName + " is invalid.\nAllowed extensions : " + _validFileExtensions.join(", "));
         return false;
      }
   }

   var fileSize = fileInput.size;
   var maxFileSize = 5 * 1028 * 1028;
   if (fileSize > maxFileSize) {
      alert("File size must be lower than " + humanFileSize(maxFileSize) + "\nYour file " + sFileName + "(" + humanFileSize(fileSize) + ")");
      return false;
   }

   return true;
}


function removeAttachment(e) {
   function reorderIndexAttr(index, property) {
      var inputFieldID = item.find("input[propertyName='" + property + "']");
      inputFieldID.removeAttr('name');
      inputFieldID.attr('name', "Attachments[" + index + "]." + property);
   }

   var item = $(e).closest(".attachment-row");
   var parent = item.closest("table")
   item.remove();

   // RE ORDER INDEX OF INPUT HIDDEN 
   var listItem = $(parent).find(".attachment-row");
   for (var i = 0; i < listItem.length; i++) {
      var item = $(listItem[i]);
      reorderIndexAttr(i, 'FilePath');
      reorderIndexAttr(i, 'FileName');
      reorderIndexAttr(i, 'FileUrl');
      reorderIndexAttr(i, 'CreatedBy');
      reorderIndexAttr(i, 'FileSize');
   }

   // add placeholder no attachment
   if (listItem.length == 0) {
      $(parent).closest('.card').append('<div class="text-center py-5 text-muted">No Attachments Added</div>');
   }
}


function scrollToInputErrorInModal() {
   var position = $('.input-validation-error').position();
   $(".modal.show").scrollTop(position.top - 40);
}
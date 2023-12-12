function initAddNewCa() {

}

function initSelect2Department() {
   var e = $('#ResponsibleManager');
   var parentModal = e.closest('.modal');
    if (e.hasClass("input-validation-error")) {
        e.parent().addClass("input-validation-error");
    }
   e.select2({
      ajax: {
         url: baseController + '/GetManagerList',
         dataType: 'json',
         delay: 100,
         data: function (params) {
            return {
               q: params.term, // search term
               page: params.page
            };
         },
         processResults: function (data, params) {
            // parse the results into the format expected by Select2
            // since we are using custom formatting functions we do not need to
            // alter the remote JSON data, except to indicate that infinite
            // scrolling can be used
            params.page = params.page || 1;

            return {
               results: data.Items,
               pagination: {
                  more: data.IsMoreItem
               }
            };
         },


      },
      allowClear: true,
      placeholder: 'Search Responsible Department (Manager)',
      dropdownParent: parentModal,
      width: "100%",
      templateResult: function (item) {

         if (item.loading) {
            return item.text;
         }
         var $container = $(
            "<div class='select2-result-employee'>" +
            "<div class='select2-result-employee-name'></div>" +
            "<div class='select2-result-employee-position'></div>" +
            "</div>"
         );

         $container.find(".select2-result-employee-name").text(item.EmpName);
         $container.find(".select2-result-employee-position").text(item.DepartmentDesc);

         return $container;
      },

      templateSelection: function (item) {
         if (item.EmpName == null) {
            if (item.text != null) {
               return item.text;
            }
            return "Search Responsible Department (Manager)";
         }
         if (item.DepartmentDesc) {
            return item.DepartmentDesc + " - " + item.EmpName;
         }
         return item.EmpName;
      }
   });
}

function clearPic() {
   $('#Pic1').val(null).trigger('change');
   $('#Pic2').val(null).trigger('change');
}

function initSelect2Pic() {
   var ePic1 = $('#Pic1');
   var ePic2 = $('#Pic2');
   var parentModal = ePic1.closest('.modal');

   if (ePic1.hasClass("input-validation-error")) {
      ePic1.parent().addClass("input-validation-error");
   }

   if (ePic2.hasClass("input-validation-error")) {
      ePic2.parent().addClass("input-validation-error");
   }

   var select2Option = {
      ajax: {
         url: baseController + '/GetEmployeeListByParentEmpID',
         dataType: 'json',
         delay: 100,
         data: function (params) {
            return {
               empID: $('#ResponsibleManager').val(),
               q: params.term, // search term
               page: params.page
            };
         },
         processResults: function (data, params) {
            // parse the results into the format expected by Select2
            // since we are using custom formatting functions we do not need to
            // alter the remote JSON data, except to indicate that infinite
            // scrolling can be used
            params.page = params.page || 1;

            return {
               results: data.Items,
               pagination: {
                  more: data.IsMoreItem
               }
            };
         },


      },
      allowClear: true,
      placeholder: 'Search Employee for PIC',
      dropdownParent: parentModal,
      width: "100%",
      templateResult: function (item) {

         if (item.loading) {
            return item.text;
         }
         var $container = $(
            "<div class='select2-result-employee'>" +
            "<div class='select2-result-employee-name'></div>" +
            "<div class='select2-result-employee-position'></div>" +
            "</div>"
         );

         $container.find(".select2-result-employee-name").text(item.EmpName);
         $container.find(".select2-result-employee-position").text(item.PositionTitle);

         return $container;
      },
      templateSelection: function (item) {
         if (item.EmpName == null) {
            if (item.text != null) {
               return item.text;
            }
            return "Search Employee for PIC";
         }
         return item.EmpName;
      }
   };
   ePic1.select2(select2Option);
   ePic2.select2(select2Option);
}







function initCaForm() {
   initSelect2();
  
   initDatePicker();
   setupTextAreaCounter('#Recomendation');
   initSelect2Department();
   initSelect2Pic();
   initClearValidationError();

   var refId = $("#AdditionalData").val()
   $("#caRefId").val(refId);
   
   // SUBMIT CA FORM CREATE / UPDATE
   $("#CaModal form").eq(0).submit(function (e) {

      e.preventDefault(); // avoid to execute the actual submit of the form.

      var form = $(this);
      var actionUrl = form.attr('action');
      var formState = $("#CaModal form").attr("form-state");

      setModalLoading(true);

      var formSerialize = form.serialize();


      var caListForm = $("<form></form>");
      var cloneForm = $($("#sectionCaList")).clone(true);
      caListForm.append(cloneForm);
      var caListSerialize = caListForm.serialize();
      formSerialize = formSerialize + '&' + caListSerialize;
      $.ajax({
         type: "POST",
         url: actionUrl,
         data: formSerialize,
         success: function (partialView) {
            setModalLoading(false);

            var isSuccess = partialView.indexOf('sectionCaList') > -1;
            if (isSuccess == false) {
               if (!hasValidResponse(partialView)) {
                  $("#CaModal").modal('hide');
                  return;
               }

               $("#CaModal .append-here").html($(partialView));
               initCaForm();
               scrollToInputError();
            } else {
               $(".append-calist-here").html($(partialView));
               $("#CaModal").modal('hide');
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
function showCreateCaForm() {
   clearForm("#CaModal");
   $("#CaModal").modal('show');
   $.get(baseController + "/CreateCa", function (partialView) {
      if (!hasValidResponse(partialView)) {
         setTimeout(function () {
            $("#CaModal").modal('hide');
         }, 500);
         return;
      }
      $("#CaModal .append-here").html($(partialView));
      initCaForm();


   }).fail(function (response) {
      handleHttpError(response);
      setTimeout(function () {
         $("#CaModal").modal('hide');
      }, 500);
   });
}

// show modal sambil nunggu form dari server
function showCaForm(isEdit, e) {
   clearForm("#CaModal");
   $("#CaModal").modal('show');

   var tdEl = $(e).closest("td").clone(true);
   var formVirtual = $("<form></form>");
   formVirtual.append(tdEl);

   // remove array index biar bisa kebaca sebagai individual object
   var inputList = formVirtual.find("input");

   for (var i = 0; i < inputList.length; i++) {
      var input = inputList.eq(i)
      var rawFieldName = $(input).attr('name');
      var firstDot = rawFieldName.indexOf('.');
      var cleanFieldName = rawFieldName.substring(firstDot + 1);
      $(input).attr('name', cleanFieldName);
   }


   var formSerialize = formVirtual.serialize();

   var url = baseController + "/ShowEditFormCa";
   if (!isEdit) {

      url = baseController + "/ViewCa";
   }

   $.ajax({
      type: "POST",
      url: url,
      data: formSerialize,
      success: function (partialView) {
         $("#CaModal .append-here").html($(partialView));
         initCaForm();
      },
      error: function (data) {
         setModalLoading(false);
         handleHttpError(data);
      }
   });
}

function deleteCaRow(index) {
   var caListForm = $("<form></form>");
   var cloneForm = $($("#sectionCaList")).clone(true);
   caListForm.append(cloneForm);
   var caListSerialize = caListForm.serialize();
   var formSerialize = caListSerialize + "&index=" + index;
   var actionUrl = baseController + "/DeleteCa";
   $.ajax({
      type: "POST",
      url: actionUrl,
      data: formSerialize,
      success: function (partialView) {
         $(".append-calist-here").html($(partialView));
      },
      error: function (data) {
         handleHttpError(data);
      }
   });
}
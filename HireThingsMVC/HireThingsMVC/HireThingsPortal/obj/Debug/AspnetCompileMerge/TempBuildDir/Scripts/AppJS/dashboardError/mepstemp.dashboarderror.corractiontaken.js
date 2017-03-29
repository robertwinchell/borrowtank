﻿/*** 
* Used for defining the MEPSTEMP  UI
* @module UI
* @namespace MEPSTEMP.temp
*/

MEPSTEMP.namespace("UI.DashboardError.CorrActionTaken");
MEPSTEMP.UI.DashboardError.CorrActionTaken = (function () {
	"use strict";
	var _isInitialized = false;

	var $ddlSearch, $txtSearch,  $DivDashboardErrorGrid, $btnDashboardErrorSearch;

	function initialiseControls() {

		if (_isInitialized == false) {
			_isInitialized = true;
			CacheEl();
			BindEvents();
		}
	}
	//Element caching done in here
	function CacheEl() {
		$ddlSearch = $('#ddlSearch');
		$txtSearch = $('#txtSearch');
		$DivDashboardErrorGrid = $('#DivDashboardErrorGrid');
		$btnDashboardErrorSearch = $('#btnDashboardErrorSearch');
	

	}
	//Event binding done in here
	function BindEvents() {
	    
	    MEPSTEMP.Global.ApplyMenuSelection('CorrActionTaken');
	    BindGridEvents();
	    $txtSearch.keypress(function (event) {

	        if (event.keyCode == 13) {
	            event.preventDefault();
	            SearchDashboardError();
	        }
	    });
		//Search button event
		$btnDashboardErrorSearch.click(function () {
		    SearchDashboardError();
		});
		$('#ddlSystem').change(function () {

		    MEPSTEMP.Global.BindDropDownOnClick("GET", 'Dropdown/PopulateOrganizationDropDownList?SystemId=' + $(this).val(), 0, 0, function (result) {
		        $('#DivOrganization').html(result);

		    });
		});
	}

	function BindGridEvents() {
	    //MEPSTEMP.Global.BindDeletionModal(MEPSTEMP.UI.DashboardError.CorrActionInProgress.resetGrid);

		$('#ddlPageSize').find('option[text=' + $('#hdnPageSize').val() + ']').attr('selected', true);
		var data = {};		
		data.SearchField = $ddlSearch.val();
		data.SearchText = $txtSearch.val();
		data.SystemId = $('#ddlSystem').val();
		data.OrganizationId = $('#ddlOrganization').val();

	    //pagination of the grid
		MEPSTEMP.Global.LoadAJAXPagination(
            //placeholder div name where pagination html is to be injected
            '#paginationDiv',
            //total number of records- obtained at first call
            $('#hdnTotalRecords'),

            // page size ddl control - in global the change event etc is registered as well as setting the ddl to 
            $('#ddlPageSize'),
            //hidden value of page size control to maintain state
            $('#hdnPageSize'),
            //current page number 
            $('#hdnCurrentPage'),
            //pagination window start page number - default is 1 -carefull : if this is set more than total pages then unwanted behaviour may appear
            '',
           //pagination window end page number - default is 3 -carefull : if this is set more than total pages then unwanted behaviour may appear
            '',
            // data object to be passed along - i.e. search criteria data
            data,
            //call to controller action which will return partial view containing the grid
            'DashboardErrorSearch/FilterCorrActionTakenSearch',
             // success call back
            function (result) {
                $DivDashboardErrorGrid.html(result);
                //Bind grid events again as new html is generated
                BindGridEvents();
            },
            //failure call back
            function (xhr, ajaxOptions, thrownError) {
                console.log('an error occurred in making ajax call');
            });


		var url = 'Dashboard/DashCorrectiveActionTakenPartial';
		$('.toggleActionTakenModal').each(function () {
		    $(this).unbind('click');
		    $(this).click(function () {
		        if ($(this).data('dasherrorid') > -1) {

		            var data = {};
		            data.DashAlertId = $(this).data('dasherrorid');
		            data.errorDate = $(this).data('errordate');
		            var table = $(this).data('table');
		            
		                MEPSTEMP.Global.MakeSimpleAjaxCall("GET", url, data, function (result) {
		                    
		                    $('#hdnCorrActionTakenPartial').html(result);
		                    $('#myModal').modal('toggle');
                            
		                    $.validator.unobtrusive.parse('#DashCorrActionTakenForm');
		                    $('.datetimepicker').datetimepicker({
		                        minDate: data.errorDate,
		                        maxDate: new Date(),
		                        useCurrent: false
		                    });

		                    BindCorrectiveActionTakenPartailView(table);

		                }, function (xhr, ajaxOptions, thrownError) {
		                    //debugger;
		                    console.log('an error occurred in making ajax call');
		                });
		            //else if (table == 1)
		            //    MEPSTEMP.Global.MakeSimpleAjaxCall("GET", 'Dashboard/DashCorrectiveActionBeingTakenPartial', data, function (result) {
		            //        $('#hdnCorrActionTakenPartial').html(result);
		            //        //    $.validator.unobtrusive.parse('#DashCorrActionTakenForm');
		            //        $('.datetimepicker').datetimepicker({
		            //            minDate: data.errorDate,
		            //            maxDate: new Date()
		            //        });

		            //        BindCorrectiveActionInProgressPartailView();
		            //    }, function (xhr, ajaxOptions, thrownError) {
		            //        //debugger;
		            //        console.log('an error occurred in making ajax call');
		            //    });
		        }
		    });
		});
		var url2 = 'Dashboard/DashboardErrorSuppressPartial';
		$('.toggleSuppressModal').each(function () {
		    $(this).unbind('click');
		    $(this).click(function () {
		        if ($(this).data('dasherrorid') > -1) {
		            var data = {};
		            data.DashErrorId = $(this).data('dasherrorid');
		            var table = $(this).data('table');
		            MEPSTEMP.Global.MakeSimpleAjaxCall("GET", url2, data, function (result) {
		                $('#hdnDashboardErrorPartial').html(result);
		                $.validator.unobtrusive.parse('#DashboardErrorForm');
		                BindSuppressPartialView(table);

		            }, function (xhr, ajaxOptions, thrownError) {
		                //debugger;
		                console.log('an error occurred in making ajax call');
		            });
		        }
		    });

		});
	}
	function GenerateValidationMessage(name, eType) {

	    var elIdent = '';

	    if (eType == undefined) {
	        if ($.trim(name.toLowerCase()) == 'ddlcorractionlist') {
	            elIdent = 'Corrective Action List is ';
	        }
	        else if ($.trim(name.toLowerCase()) == 'correctiveactiondate') {
	            elIdent = 'Corrective Action Date is ';
	        }
	        else if ($.trim(name.toLowerCase()) == 'correctiveactionnotes') {
	            elIdent = 'Notes are ';
	        }
	        else if ($.trim(name.toLowerCase()) == 'suppressreason') {
	            elIdent = 'Reason is ';
	        }
	        else if ($.trim(name.toLowerCase()) == 'ddlsuppressinterval') {
	            elIdent = 'Duration is ';
	        }
	        else if ($.trim(name.toLowerCase()) == 'customsuppressinterval') {
	            elIdent = 'Duration is ';
	        }
	        else if ($.trim(name.toLowerCase()) == 'corractioninprogressnotes') {
	            elIdent = 'Notes are ';
	        }

	    }
	    else if ($.trim(eType.toLowerCase()) == 'customnumeric') {
	        elIdent = 'Duration must be ';
	    }


	    if (eType == undefined) {
	        return '<span data-valmsg-replace="true" data-valmsg-for="' + name + '" class="text-danger field-validation-error"><span for="" class="">' + elIdent + 'required.</span></span>';
	    }
	    else if ($.trim(eType.toLowerCase()) == 'customnumeric') {

	        return '<span data-valmsg-replace="true" data-valmsg-for="' + name + '" class="text-danger field-validation-error"><span for="" class="">' + elIdent + 'numeric.</span></span>';
	    }


	}
	
	function BindCorrectiveActionTakenPartailView(table) {
	    
	    $('#btnCorrActTakenSave').click(function (e) {
	        e.preventDefault();	        
	        $('#myModal').find('.requiredEl').each(function () {
	            $(this).parent().children('.text-danger').remove();
	            $(this).parent().parent().children('.text-danger').remove();
	            if ($(this).hasClass('requiredDdl')) {
	                if ($(this).val() <= 0) {
	                    $(this).parent().append(GenerateValidationMessage($(this).attr('id')));
	                }
	            } else {
	                if ($(this).val() == null || $(this).val() == undefined || $(this).val() == '') {
	                    if ($(this).attr('id') == 'CorrectiveActionDate')
	                        $(this).parent().parent().append(GenerateValidationMessage($(this).attr('id')));
	                    else
	                        $(this).parent().append(GenerateValidationMessage($(this).attr('id')));
	                }
	            }
	        });
	        var errorCount = 0;
	        $('#myModal').find('.text-danger').each(function () {
	            errorCount += 1;
	        });
	        if (errorCount == 0) {
	            var model = {};
	            model.CorrectiveActionListId = $('#ddlCorrActionList').val();
	            model.CorrectiveActionDate = $('#CorrectiveActionDate').val();
	            model.CorrectiveActionNotes = $('#CorrectiveActionNotes').val();
	            model.DashboardErrorId = $('#DashboardErrorId').val();

	            var url = 'Dashboard/DashCorrectiveActionTakenPartial';
	            $('#btnCorrActTakenSave').prop('disabled', true);	            
	            MEPSTEMP.Global.MakeSimpleAjaxCall("POST", url, model, function (result) {
	                setTimeout(function () {
	                    $('body').removeClass('modal-open');
	                    $('#myModal').modal('toggle');

	                    
	                    result=result.substring(result.indexOf('<thead>'), result.indexOf('</tbody>'))+'</tbody>';
	                    $('#tblDashErrorGrid').html(result);
	                    SearchDashboardError();

	                }, 0);
	            }, function (xhr, ajaxOptions, thrownError) {
	                //debugger;
	                console.log('an error occurred in making ajax call');
	            });
	        }

	    });
	}
	function BindSuppressPartialView(table) {
	    var arrSuppressInterval = [];
	    $('#myModalSuppress').modal('toggle');
	    if ($('#SuppressInterval').val() == 0)
	        $('#SuppressInterval').val(-1);
	    $('#SuppressStatus').bootstrapSwitch({ labelText: 'Suppress', onText: 'YES', offText: 'NO' });
	    $('#ddlSuppressInterval').on('change', function (e) {

	        if ($(this).val() == 0) {
	            $(this).parent().removeClass('col-lg-6').removeClass('col-lg-7').addClass('col-lg-3');
	            $('#customSuppressInterval').parent().show();
	            $('#customSuppressInterval').addClass('requiredEl');
	            MEPSTEMP.Global.KeyLocking('.hasKeyLocking', 'data-keylocking');
	        } else {
	            if ($(this).parent().hasClass('col-lg-3'))
	                $(this).parent().removeClass('col-lg-3').addClass('col-lg-7');
	            $('#customSuppressInterval').parent().hide().children('.text-danger').remove();
	            $('#customSuppressInterval').removeClass('requiredEl');
	        }
	    });
	    $('#customSuppressInterval').on('focusout', function () {
	        if (isNaN($('#customSuppressInterval').val())) {
	            $(this).parent().children().remove('.text-danger');
	            $(this).parent().append(GenerateValidationMessage('customSuppressInterval', 'customnumeric'));
	        }
	    });
	    $('#ddlSuppressInterval option').each(function () {
	        arrSuppressInterval.push($(this).val());
	    });
	    if ($('#SuppressInterval').val() == 0 || $.inArray($('#SuppressInterval').val(), arrSuppressInterval) == -1) { $('#ddlSuppressInterval').val(0).trigger('change'); $('#customSuppressInterval').val($('#SuppressInterval').val()); } else { $('#ddlSuppressInterval').val($('#SuppressInterval').val()); }
	    $('#btnSuppressPartialSave').click(function (e) {
	        e.preventDefault();
	       
	        $(this).closest('form').find('.requiredEl').each(function () {
	            $(this).parent().children('.text-danger').remove();
	            $(this).parent().parent().children('.text-danger').remove();
	            if ($(this).hasClass('requiredDdl')) {
	                if ($(this).val() < 0) {
	                    $(this).parent().append(GenerateValidationMessage($(this).attr('id')));
	                }
	            } else {
	                if ($(this).val() == null || $(this).val() == undefined || $(this).val() == '') {
	                    $(this).parent().append(GenerateValidationMessage($(this).attr('id')));
	                }
	            }
	        });
	        var errorCount = 0;
	        $('#myModalSuppress').find('.text-danger').each(function () {	        
	            errorCount += 1;
	        });	        
	        if (errorCount == 0) {
	            var model = {};
	            if ($('#ddlSuppressInterval').val() == 0)
	                model.SuppressInterval = $('#customSuppressInterval').val();
	            else
	                model.SuppressInterval = $('#ddlSuppressInterval').val();
	            if ($('#SuppressStatus').hasClass('hidden'))
	                model.SuppressStatus = true;
	            else
	                model.SuppressStatus = $('#SuppressStatus').is(':checked')
	            model.DashBoardErrorId = $('#DashBoardErrorId').val();
	            model.SuppressReason = $('#SuppressReason').val();

	            if (table == 1)
	                model.AlertStatusId = 1;
	            else
	                model.AlertStatusId = 3;
	            //model.DashboardErrorId = $('#DashboardErrorId').val();
	            var url = 'Dashboard/DashboardErrorSuppressPartial';
	            $('#btnSuppressPartialSave').prop('disabled', true);
	            MEPSTEMP.Global.MakeSimpleAjaxCall("POST", url, model, function (result) {

	                setTimeout(function () {
	                    $('body').removeClass('modal-open');
	                    $('#myModalSuppress').modal('hide');
	                    if (table == 1)
	                        $('#DivDashboardErrorGrid').html(result);
	                    else if (table == 2)
	                        $('#DivDashboardErrorGridCorrAction').html(result);
	                    BindGridEvents();
	                }, 0);

	            }, function (xhr, ajaxOptions, thrownError) {
	                //debugger;
	                console.log('an error occurred in making ajax call');
	            });
	        }


	    });

	}
	//Search ProbeType using the form field values
	function SearchDashboardError() {
	    var data = {};
	    data.SearchField = $ddlSearch.val();
	    data.SearchText = $txtSearch.val();
	    data.SystemId = $('#ddlSystem').val();
	    data.OrganizationId = $('#ddlOrganization').val();
	
	    var url = 'DashboardErrorSearch/FilterCorrActionTakenSearch';
		MEPSTEMP.Global.MakeAjaxCall("POST", url, data, function (result) {

		    $DivDashboardErrorGrid.html(result);
		    BindGridEvents();
		}, function (xhr, ajaxOptions, thrownError) {
			//debugger;
			console.log('an error occurred in making ajax call');
		});
	}
	//Clear Fields function
	function ClearFields() { }

	return {
		readyMain: function () {

		},
		initialisePage: function () {
		    
			initialiseControls();
		},
		resetPage: function () {

		},
		resetGrid: function () {
		    SearchDashboardError();
		}
	};
}());
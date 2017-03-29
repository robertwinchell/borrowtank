/*** 
* Used for defining the MEPSTEMP  UI
* @module UI
* @namespace MEPSTEMP.temp
*/

MEPSTEMP.namespace("UI.EmailGroup.Search");
MEPSTEMP.UI.EmailGroup.Search = (function () {
	"use strict";
	var _isInitialized = false;

	var $ddlSearch, $txtSearch, $ddlSystem, $ddlOrganization, $DivEmailGroupGrid, $btnEmailGroupSearch;

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
		$DivEmailGroupGrid = $('#DivEmailGroupGrid');
		$btnEmailGroupSearch = $('#btnEmailGroupSearch');
	}
	//Event binding done in here
	function BindEvents() {
	    
	    MEPSTEMP.Global.ApplyMenuSelection('EmailGroup');
	    BindGridEvents();
	    $txtSearch.keypress(function (event) {

	        if (event.keyCode == 13) {
	            event.preventDefault();
	            SearchEmailGroup();
	        }
	    });
		//Search button event
		$btnEmailGroupSearch.click(function () {
		    SearchEmailGroup();
		});

	}

	function BindGridEvents() {
	    MEPSTEMP.Global.BindDeletionModal(MEPSTEMP.UI.EmailGroup.Search.resetGrid);
	    
	    $('.btnEmailGroupEdit').each(function () {
	        
			$(this).click(function () {
			    $('#EmailGroupId').val($(this).data('emailgroupid'));
				$('#EmailGroupSearch').submit();
			});
		});

		$('#ddlPageSize').find('option[text=' + $('#hdnPageSize').val() + ']').attr('selected', true);
		var data = {};		
		data.SearchField = $ddlSearch.val();
		data.SearchText = $txtSearch.val();
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
            'EmailGroupSearch/FilterEmailGroupSearch',
             // success call back
            function (result) {
                $DivEmailGroupGrid.html(result);
                //Bind grid events again as new html is generated
                BindGridEvents();
            },
            //failure call back
            function (xhr, ajaxOptions, thrownError) {
                console.log('an error occurred in making ajax call');
            });
	}

	//Search ProbeType using the form field values
	function SearchEmailGroup() {
	    var data = {};
	    data.SearchField = $ddlSearch.val();
	    data.SearchText = $txtSearch.val();
	    var url = 'EmailGroupSearch/FilterEmailGroupSearch';
		MEPSTEMP.Global.MakeAjaxCall("POST", url, data, function (result) {

		    $DivEmailGroupGrid.html(result);
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
		    SearchEmailGroup();
		}
	};
}());
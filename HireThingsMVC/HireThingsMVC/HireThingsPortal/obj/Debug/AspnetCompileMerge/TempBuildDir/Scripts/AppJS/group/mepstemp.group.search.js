/*** 
* Used for defining the MEPSTEMP  UI
* @module UI
* @namespace MEPSTEMP.temp
*/

MEPSTEMP.namespace("UI.Group.Search");
MEPSTEMP.UI.Group.Search = (function () {
    "use strict";
    var _isInitialized = false;

    var $ddlSearch, $txtSearch, $ddlSystem, $ddlOrganization, $DivGroupGrid, $btnGroupSearch;

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
        $DivGroupGrid = $('#DivGroupGrid');
        $btnGroupSearch = $('#btnGroupSearch');
    }
    //Event binding done in here
    function BindEvents() {
        MEPSTEMP.Global.ApplyMenuSelection('Group');
        BindGridEvents();
        //Search button event
        $btnGroupSearch.click(function () {
            SearchGroup();
        });

        $txtSearch.keypress(function (event) {
           
            if (event.keyCode == 13) {
                event.preventDefault();
                SearchGroup();
            }
        });

    }

    function BindGridEvents() {
        MEPSTEMP.Global.BindDeletionModal(MEPSTEMP.UI.Group.Search.resetGrid);
        $('.btnGroupEdit').each(function () {
            $(this).click(function () {
                $('#hdnTempGroupId').val($(this).data('tempgroupid'));
                $('#GroupSearch').submit();
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
            'GroupSearch/FilterGroupSearch',
             // success call back
            function (result) {
                $DivGroupGrid.html(result);
                //Bind grid events again as new html is generated
                BindGridEvents();
            },
            //failure call back
            function (xhr, ajaxOptions, thrownError) {
                console.log('an error occurred in making ajax call');
            });
    }

    //Search ProbeType using the form field values
    function SearchGroup() {
        var data = {};
        data.SearchField = $ddlSearch.val();
        data.SearchText = $txtSearch.val();
        var url = 'GroupSearch/FilterGroupSearch';
        MEPSTEMP.Global.MakeAjaxCall("POST", url, data, function (result) {

            $DivGroupGrid.html(result);
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
            SearchGroup();
        }
    };
}());
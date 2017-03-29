/*** 
* Used for defining the HIRETHINGS  UI
* @module UI
* @namespace HIRETHINGS.temp
*/

HIRETHINGS.namespace("UI.WebApiObject.Search");
HIRETHINGS.UI.WebApiObject.Search = (function () {
    "use strict";
    var _isInitialized = false;

    var $ddlSearch, $txtSearch, $ddlDeviceGroup, $ddlSystem, $ddlGroup, $DivObjectGrid, $btnObjectSearch;

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
        $DivObjectGrid = $('#DivObjectGrid');
        $btnObjectSearch = $('#btnWAObjectSearch');
    }
    //Event binding done in here
    function BindEvents() {
        HIRETHINGS.Global.ApplyMenuSelection('webapiobject');
        BindGridEvents();
        $txtSearch.keypress(function (event) {
            if (event.keyCode == 13) {
                event.preventDefault();
                SearchObject();
            }
        });
        //Search button event
        $btnObjectSearch.click(function () {
            SearchObject();
        });

    }

    function BindGridEvents() {
        HIRETHINGS.Global.BindDeletionModal(HIRETHINGS.UI.WebApiObject.Search.resetGrid);
        $('.btnObjectEdit').each(function () {
            $(this).click(function () {
                $('#hdWAObjectId').val($(this).data('objectid'));
                $('#WAObjectSearch').submit();
            });
        });
        $('#ddlPageSize').find('option[text=' + $('#hdnPageSize').val() + ']').attr('selected', true);
        var data = {};
        data.SearchField = $ddlSearch.val();
        data.SearchText = $txtSearch.val();
        //pagination of the grid
        HIRETHINGS.Global.LoadAJAXPagination(
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
            'WebApiObjectSearch/FilterObjectSearch',
             // success call back
            function (result) {
                $DivObjectGrid.html(result);
                //Bind grid events again as new html is generated
                BindGridEvents();
            },
            //failure call back
            function (xhr, ajaxOptions, thrownError) {
                console.log('an error occurred in making ajax call');
            });
    }
    //Search Escalation using the form field values
    function SearchObject() {
        var data = {};
        data.SearchField = $ddlSearch.val();
        data.SearchText = $txtSearch.val();
        var url = 'WebApiObjectSearch/FilterObjectSearch';
        HIRETHINGS.Global.MakeAjaxCall("POST", url, data, function (result) {

            $DivObjectGrid.html(result);
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
            SearchObject();
        }
    };
}());
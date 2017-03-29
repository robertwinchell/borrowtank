/*** 
* Used for defining the HIRETHINGS  UI
* @module UI
* @namespace HIRETHINGS.temp
*/

HIRETHINGS.namespace("UI.Role.Search");
HIRETHINGS.UI.Role.Search = (function () {
    "use strict";
    var _isInitialized = false;

    var $ddlSearch, $txtSearch, $ddlDeviceGroup, $ddlSystem, $ddlGroup, $DivRoleGrid, $btnRoleSearch;

    function initialiseControls() {

        if (_isInitialized == false) {
            _isInitialized = true;
            CacheEl();
            BindEvents();
        }
    }
    //Element caching done in here
    function CacheEl() {
        $txtSearch = $('#txtSearch');
        $DivRoleGrid = $('#DivRoleGrid');
        $btnRoleSearch = $('#btnRoleSearch');
    }
    //Event binding done in here
    function BindEvents() {
        HIRETHINGS.Global.ApplyMenuSelection('Role');
        $txtSearch.keypress(function (event) {
            if (event.keyCode == 13) {
                event.preventDefault();
                SearchRole();
            }
        });
        BindGridEvents();
        //Search button event
        $btnRoleSearch.click(function () {
            SearchRole();
        });

    }

    function BindGridEvents() {
        HIRETHINGS.Global.BindDeletionModal(HIRETHINGS.UI.Role.Search.resetGrid);
        $('.btnRoleEdit').each(function () {
            $(this).click(function () {
                $('#hdRoleId').val($(this).data('roleid'));
                $('#RoleSearch').submit();
            });
        });
        $('#ddlPageSize').find('option[text=' + $('#hdnPageSize').val() + ']').attr('selected', true);
        var data = {};

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
            'RoleSearch/FilterRoleSearch',
             // success call back
            function (result) {
                $DivRoleGrid.html(result);
                //Bind grid events again as new html is generated
                BindGridEvents();
            },
            //failure call back
            function (xhr, ajaxOptions, thrownError) {
                console.log('an error occurred in making ajax call');
            });
    }
    //Search Escalation using the form field values
    function SearchRole() {

        var url = 'RoleSearch/FilterRoleSearch';
        HIRETHINGS.Global.MakeAjaxCall("POST", url, { SearchText: $txtSearch.val() }, function (result) {

            $DivRoleGrid.html(result);
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
            SearchRole();
        }
    };
}());
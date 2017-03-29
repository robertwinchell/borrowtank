/*** 
* Used for defining the HIRETHINGS  UI
* @module UI
* @namespace HIRETHINGS.temp
*/

HIRETHINGS.namespace("UI.User.Search");
HIRETHINGS.UI.User.Search = (function () {
    "use strict";
    var _isInitialized = false;
   
    var $ddlSearch, $txtSearch, $ddlSystem, $ddlOrganization, $DivUserGrid, $btnUserSearch;

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
            $ddlSystem = $('#ddlSystem');
            $ddlOrganization = $("#ddlOrganization");
            $DivUserGrid = $('#DivUserGrid');
            $btnUserSearch = $('#btnWAUserSearch');
        }
        //Event binding done in here
        function BindEvents() {
            HIRETHINGS.Global.ApplyMenuSelection('webapiuser');
            BindGridEvents();
            $txtSearch.keypress(function (event) {
                if (event.keyCode == 13) {
                    event.preventDefault();
                    SearchUser();
                }
            });
            //Search button event
            $btnUserSearch.click(function () {
                SearchUser();
            });

            $('#ddlSystem').change(function () {

                HIRETHINGS.Global.BindDropDownOnClick("GET", 'Dropdown/PopulateOrganizationDropDownList?SystemId=' + $(this).val(), 0, 0, function (result) {
                    $('#DivOrganization').html(result);

                });
            });
        }

        function BindGridEvents() {

            HIRETHINGS.Global.BindDeletionModal(HIRETHINGS.UI.User.Search.resetGrid);
            $('.btnWAUserEdit').each(function () {
                $(this).click(function () {
                    $('#hdnWAUserId').val($(this).data('userid'));
                    $('#UserSearch').submit();
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
                'WebApiUserSearch/FilterUserSearch',
                 // success call back
                function (result) {
                    $DivUserGrid.html(result);
                    //Bind grid events again as new html is generated
                    BindGridEvents();
                },
                //failure call back
                function (xhr, ajaxOptions, thrownError) {
                    console.log('an error occurred in making ajax call');
                });
        }
        //Search User using the form field values
        function SearchUser() {
            var data = {};
            data.SearchField = $ddlSearch.val();
            data.SearchText = $txtSearch.val();
            data.SystemId = $('#ddlSystem').val();
            data.OrganizationId = $('#ddlOrganization').val();
            var url = 'WebApiUserSearch/FilterUserSearch';
            HIRETHINGS.Global.MakeAjaxCall("POST", url, data, function (result) {                
                $DivUserGrid.html(result);
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
            SearchUser();
        }
    };
}());
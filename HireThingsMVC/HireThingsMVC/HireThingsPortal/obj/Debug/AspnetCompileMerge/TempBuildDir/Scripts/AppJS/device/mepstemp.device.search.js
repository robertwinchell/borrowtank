/*** 
* Used for defining the MEPSTEMP  UI
* @module UI
* @namespace MEPSTEMP.temp
*/

MEPSTEMP.namespace("UI.Device.Search");
MEPSTEMP.UI.Device.Search = (function () {
    "use strict";
    var _isInitialized = false;
   
    var $ddlSearch, $txtSearch, $ddlSystem, $ddlOrganization, $DivDeviceGrid, $btnDeviceSearch;

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
            $ddlOrganization = $('#ddlOrganization');
            $DivDeviceGrid = $('#DivDeviceGrid');
            $btnDeviceSearch = $('#btnDeviceSearch');
        }
        //Event binding done in here
        function BindEvents() {

            MEPSTEMP.Global.ApplyMenuSelection('Device');
            BindGridEvents();
         
            //Search button event
            $txtSearch.keypress(function (event) {

                if (event.keyCode == 13) {
                    event.preventDefault();
                    SearchDevice();
                }
            });

            $btnDeviceSearch.click(function () {
                SearchDevice();
            });

            $('#ddlSystem').change(function () {
              
                MEPSTEMP.Global.BindDropDownOnClick("GET", 'Dropdown/PopulateOrganizationDropDownList?SystemId=' + $(this).val() , 0, 0, function (result) {
                    $('#DivOrganization').html(result);                             
                 
                });
            });
           
           
        }

        function BindGridEvents() {

            MEPSTEMP.Global.BindDeletionModal(MEPSTEMP.UI.Device.Search.resetGrid);
            $('.btnDeviceEdit').each(function () {
                $(this).click(function () {
                    $('#hdnDeviceId').val($(this).data('deviceid'));
                    $('#DeviceSearch').submit();
                });
            });
            $('#ddlPageSize').find('option[text=' + $('#hdnPageSize').val() + ']').attr('selected', true);
            var data = {};
            data.SystemId = $('#ddlSystem').val();
            data.OrganizationId = $('#ddlOrganization').val();
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
                'DeviceSearch/FilterDeviceSearch',
                 // success call back
                function (result) {
                    $DivDeviceGrid.html(result);
                    //Bind grid events again as new html is generated
                    BindGridEvents();
                },
                //failure call back
                function (xhr, ajaxOptions, thrownError) {
                    console.log('an error occurred in making ajax call');
                });
        }
        //Search device using the form field values
        function SearchDevice() {
            var data = {};
            data.SystemId = $('#ddlSystem').val();
            data.OrganizationId = $('#ddlOrganization').val();
            data.SearchField = $ddlSearch.val();
            data.SearchText = $txtSearch.val();
            var url = 'DeviceSearch/FilterDeviceSearch';
            MEPSTEMP.Global.MakeAjaxCall("POST", url, data, function (result) {                
                $DivDeviceGrid.html(result);
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
            SearchDevice();
        }
    };
}());
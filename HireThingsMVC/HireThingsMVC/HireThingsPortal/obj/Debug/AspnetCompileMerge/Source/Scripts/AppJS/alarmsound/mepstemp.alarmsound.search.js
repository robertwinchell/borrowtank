/*** 
* Used for defining the MEPSTEMP  UI
* @module UI
* @namespace MEPSTEMP.temp
*/

MEPSTEMP.namespace("UI.AlarmSound.Search");
MEPSTEMP.UI.AlarmSound.Search = (function () {
    "use strict";
    var _isInitialized = false;

    var $ddlSearch, $txtSearch, $ddlDeviceGroup, $ddlSystem, $ddlGroup, $DivAlarmSoundGrid, $btnAlarmSoundSearch;

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
        $DivAlarmSoundGrid = $('#DivAlarmSoundGrid');
        $btnAlarmSoundSearch = $('#btnAlarmSoundSearch');
    }
    //Event binding done in here
    function BindEvents() {

        MEPSTEMP.Global.ApplyMenuSelection('AlarmSound');
        
        $txtSearch.keypress(function (event) {
            if (event.keyCode == 13) {
                event.preventDefault();
                SearchAlarmSound();
            }
        });
        BindGridEvents();
        //Search button event
        $btnAlarmSoundSearch.click(function () {
            SearchAlarmSound();
        });

    }

    function BindGridEvents() {
        MEPSTEMP.Global.BindDeletionModal(MEPSTEMP.UI.AlarmSound.Search.resetGrid);
        $('.btnAlarmSoundEdit').each(function () {
            $(this).click(function () {
                $('#hdnAlarmSoundId').val($(this).data('alarmsoundid'));
                $('#AlarmSoundSearch').submit();
            });
        });

        $('#ddlPageSize').find('option[text=' + $('#hdnPageSize').val() + ']').attr('selected', true);
            var data = {};
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
                'AlarmSoundSearch/FilterAlarmSoundSearch',
                 // success call back
                function (result) {                               
                    $DivAlarmSoundGrid.html(result);
                    //Bind grid events again as new html is generated
                    BindGridEvents();
                },
                //failure call back
                function (xhr, ajaxOptions, thrownError) {                
                console.log('an error occurred in making ajax call');
            });
    }
    //Search Escalation using the form field values
    function SearchAlarmSound() {
        var data = {};
        data.SearchText = $txtSearch.val();
        var url = 'AlarmSoundSearch/FilterAlarmSoundSearch';
        MEPSTEMP.Global.MakeAjaxCall("POST", url, data, function (result) {

            $DivAlarmSoundGrid.html(result);
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
            SearchAlarmSound();
        }
    };
}());
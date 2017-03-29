﻿/*** 
* Used for defining the HIRETHINGS  UI
* @module UI
* @namespace HIRETHINGS.temp
*/

HIRETHINGS.namespace("UI.Location.Search");
HIRETHINGS.UI.Location.Search = (function () {
    "use strict";
    var _isInitialized = false;
   
    var $ddlSearch, $txtSearch, $ddlLocation, $ddlCountry, $DivLocationGrid,$btnLocationSearch;

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
            $ddlCountry = $('#ddlCountry');
            $ddlLocation = $("#ddlLocation");
            $btnLocationSearch = $('#btnLocationSearch');
            $DivLocationGrid = $('#DivLocationGrid');
        }
        //Event binding done in here
        function BindEvents() {
            HIRETHINGS.Global.ApplyMenuSelection('Location');
            BindGridEvents();
            $txtSearch.keypress(function (event) {
                if (event.keyCode == 13) {
                    event.preventDefault();
                    SearchLocation();
                }
            });
            //Search button event
            $btnLocationSearch.click(function () {
                SearchLocation();
            });
           
                       
        }
        function BindGridEvents() {
            HIRETHINGS.Global.BindDeletionModal(HIRETHINGS.UI.Location.Search.resetGrid);
            $('.btnLocationEdit').each(function () {
                $(this).click(function () {
                    $('#hdnLocationId').val($(this).data('locationid'));
                    $('#LocationSearch').submit();
                });
            });
            $('#ddlPageSize').find('option[text=' + $('#hdnPageSize').val() + ']').attr('selected', true);
            var data = {};
            data.CountryId = $('#ddlCountry').val();
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
                'LocationSearch/FilterLocationSearch',                
                 // success call back
                function (result) {                               
                    $DivLocationGrid.html(result);
                    //Bind grid events again as new html is generated
                    BindGridEvents();
                },
                //failure call back
                function (xhr, ajaxOptions, thrownError) {                
                console.log('an error occurred in making ajax call');
            });
        }
        //Search Location using the form field values
        function SearchLocation() {
            var data= {};
            data.CountryId = $('#ddlCountry').val();
            data.SearchField= $ddlSearch.val();
            data.SearchText=$txtSearch.val();
            var url = 'LocationSearch/FilterLocationSearch';
            HIRETHINGS.Global.MakeAjaxCall("POST", url, data, function (result) {
                
                $DivLocationGrid.html(result);
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
            SearchLocation();
        }
    };
}());
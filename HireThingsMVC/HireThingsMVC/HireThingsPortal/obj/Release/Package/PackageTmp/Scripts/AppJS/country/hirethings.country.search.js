/*** 
* Used for defining the HIRETHINGS  UI
* @module UI
* @namespace HIRETHINGS.temp
*/

HIRETHINGS.namespace("UI.Country.Search");
HIRETHINGS.UI.Country.Search = (function () {
    "use strict";
    var _isInitialized = false;
   
    var $ddlSearch, $txtSearch, $DivCountryGrid, $btnCountrySearch;

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
           
            $DivCountryGrid = $('#DivCountryGrid');
            $btnCountrySearch = $('#btnCountrySearch');
        }
        //Event binding done in here
        function BindEvents() {
            HIRETHINGS.Global.ApplyMenuSelection('Country');

           
            BindGridEvents();
            $txtSearch.keypress(function (event) {
                if (event.keyCode == 13) {
                    event.preventDefault();
                    SearchCountry();
                }
            });
            //Search button event
            $btnCountrySearch.click(function () {
                SearchCountry();
            });

            
           
        }
        function BindGridEvents() {

            //sorting temporary here

            $('.sortButton').click(function () {
                var sortType = -1;
                if ($(this).hasClass('ascSort')) {
                    sortType = 0;                    
                } else {
                    sortType = 1;
                    //descending sorting wished
                }
                $('#hdnColumnName').val($(this).data('columnname'));
                $('#hdnSortType').val(sortType);
                //Call the search
                SearchCountry();
            });
            var selectedCoulmn = $('#hdnColumnName').val();
            var selectedSortType = $('#hdnSortType').val();      
            if (selectedCoulmn != '') {
                $('.sortButton').each(function () {
                    if ($(this).data('columnname') == selectedCoulmn) {
                        
                        if (selectedSortType == 0) {
                            //i.e. ascending                                                        
                            $(this).addClass('descSort');
                            $(this).children('.ascArrow').removeClass('hidden');

                        } else if (selectedSortType == 1) {
                            //i.e descending
                            $(this).addClass('ascSort');
                            $(this).children('.descArrow').removeClass('hidden');
                        }
                        
                    }
                });
            }

            // sorting functionality ends here


            HIRETHINGS.Global.BindDeletionModal(HIRETHINGS.UI.Country.Search.resetGrid);
            $('.btnCountryEdit').each(function () {
                $(this).click(function () {
                    $('#hdnCountryId').val($(this).data('countryid'));
                    $('#CountrySearch').submit();
                });
            });
            $('#ddlPageSize').find('option[text=' + $('#hdnPageSize').val() + ']').attr('selected', true);
            var data = {};
            
            
            data.SearchField = $ddlSearch.val();
            data.SearchText = $txtSearch.val();

            data.SortType = $('#hdnSortType').val();
            data.ColumnName = $('#hdnColumnName').val();
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
                'CountrySearch/FilterCountrySearch',
                 // success call back
                function (result) {
                    $DivCountryGrid.html(result);
                    //Bind grid events again as new html is generated
                    BindGridEvents();
                },
                //failure call back
                function (xhr, ajaxOptions, thrownError) {
                    console.log('an error occurred in making ajax call');
                });
        }
        //Search Country using the form field values
        function SearchCountry() {
          
            var url = 'CountrySearch/FilterCountrySearch';
            var data = {};
           
           
            data.SearchField = $ddlSearch.val();
            data.SearchText = $txtSearch.val();
            data.SortType = $('#hdnSortType').val();
            data.ColumnName = $('#hdnColumnName').val();
            data.CPage = $('#hdnCurrentPage').val();
            data.pSize = $('#hdnPageSize').val();
            HIRETHINGS.Global.MakeAjaxCall("POST", url, data, function (result) {
                
                $DivCountryGrid.html(result);
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
            SearchCountry();
        }
    };
}());
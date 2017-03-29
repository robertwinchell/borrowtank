/*** 
* Used for defining the HIRETHINGS  UI
* @module UI
* @namespace HIRETHINGS.temp
*/

HIRETHINGS.namespace("UI.WebAPIRoleObject");
HIRETHINGS.UI.WebAPIRoleObject = (function () {
    "use strict";
    var _isInitialized = false;
    //global variables for caching purposes
    var $ddlSearch, $DivObjectRoleGrid;

    function initialiseControls(type) {

        if (_isInitialized == false) {
            _isInitialized = true;
            CacheEl();
            BindEvents(type);
        }
    }

    //Element caching done in here
    function CacheEl() {
        $ddlSearch = $('#ddlSearch');
        $DivObjectRoleGrid = $("#DivObjectRoleGrid");
    }
    //Event binding done in here
    function BindEvents(type) {
        HIRETHINGS.Global.ApplyMenuSelection('webapiroleobject');

        if ($ddlSearch.val() > 0) {
            $("#btnRow").show();
        }

        $("#DivObjectRoleGrid table tbody").find("input[type='checkbox']").change(function (event) {
            var currentRow = $(this).closest('tr');

            //set flag of row change
            currentRow.find('input.ischanged').val('true');

            // if current checkbox is active checkbox
            if ($(this).hasClass('isactivechk')) {
                var currentRowcheckbox = currentRow.find('input[type="checkbox"]').not(".isactivechk").not(".dependent");

                if ($(this).is(":checked")) {
                    currentRowcheckbox.removeAttr("disabled");
                }
                else {
                    // remove check from checkbox's
                    currentRowcheckbox.each(function (index, element) {
                        if ($(element).is(":checked")) {
                            $(element).trigger("click");
                        }
                    });
                    currentRowcheckbox.attr("disabled", true);
                }
            }
            
        });

        $("#DivObjectRoleGrid").on("click", "#chkisactive", function (event) {
            if ($(this).is(":checked")) {
                $(".isactivechk").not(":checked").trigger("click").trigger("change");
            }
            else {
                $(".isactivechk:checked").trigger("click").trigger("change")    ;
            }
        });


        $("#DivObjectRoleGrid").on("click", "#chkisallowget", function (event) {  
            if ($(this).is(":checked")) {
                $(".isallowget").not(":checked").not(".dependent").trigger("click").trigger("change");
            }
            else {
                $(".isallowget:checked").trigger("click").trigger("change");
            }
        });

        $("#DivObjectRoleGrid").on("click", "#chkisallowpost", function (event) {  
            if ($(this).is(":checked")) {
                $(".isallowpost").not(":checked").trigger("click").trigger("change");
            }
            else {
                $(".isallowpost:checked").trigger("click").trigger("change");
            }
        });
        
        $("#DivObjectRoleGrid").on("click", "#chkisallowdelete", function (event) {  
            if ($(this).is(":checked")) {
                $(".isallowdelete").not(":checked").trigger("click").trigger("change");
            }
            else {
                $(".isallowdelete:checked").trigger("click").trigger("change");
            }
         });

        $ddlSearch.change(function () {
            SearchRoleObject();
        });

        $("#DivObjectRoleGrid").on("click", "#btnCancel", function (e) {
            $ddlSearch.val("-1");
            $ddlSearch.trigger("change");
        });

        //BindGridEvents();
    }
    
    //Search device using the form field values
    function SearchRoleObject() {
        

        var data = {};
        data.RoleId = $ddlSearch.val();        
        var url = 'WebAPIRoleObject/FilterRoleObject';
        HIRETHINGS.Global.MakeAjaxCall("POST", url, data, function (result) {
            $DivObjectRoleGrid.html(result);
            if ($("#DivObjectRoleGrid table tbody tr").length > 0) {
                $("#btnRow").show();
            }
            BindEvents();
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
        initialisePage: function (type) {
            initialiseControls(type);
        },
        resetPage: function () {

        }
    };
}());
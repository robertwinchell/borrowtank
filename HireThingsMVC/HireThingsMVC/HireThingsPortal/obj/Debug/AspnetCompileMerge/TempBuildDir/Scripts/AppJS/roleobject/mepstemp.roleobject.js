/*** 
* Used for defining the MEPSTEMP  UI
* @module UI
* @namespace MEPSTEMP.temp
*/

MEPSTEMP.namespace("UI.RoleObject");
MEPSTEMP.UI.RoleObject = (function () {
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
        MEPSTEMP.Global.ApplyMenuSelection('RoleObject');
        //disabled or enable all checkboxes on click event
        $("#DivObjectRoleGrid").on("click", "#chkisactive", function (event) {  //on click
            if (this.checked) { // check select status
                $('.isactivechk').each(function () {
                    this.checked = true;
                    var id = $(this).data("id");
                    var hidVal = $("#ischange_" + id);
                    hidVal.val(true);
                });
                $('.isactivechk').prop('checked', true);
                $('.isallowrite').removeAttr('disabled');
                $('.isallowdelete').removeAttr('disabled');
            }
            else {
                $('.isactivechk').each(function () {
                    this.checked = false;
                    var id = $(this).data("id");
                    var hidVal = $("#ischange_" + id);
                    hidVal.val(true);
                });
                $('.isallowrite').prop('disabled', 'disabled');
                $('.isallowdelete').prop('disabled', 'disabled');
                $('.isallowrite').prop('checked', false);
                $('.isallowdelete').prop('checked', false);
            }
        });


        $("#DivObjectRoleGrid").on("click", "#chkisallowwrite", function (event) {  //on click
            if (this.checked) { // check select status
                $('.isallowrite').each(function () {
                    var attr = $(this).attr('disabled');
                    
                    if (typeof attr !== typeof undefined || attr === false) {
                        
                        
                    } else {

                        $(this).prop('checked', true);
                        var id = $(this).data("id");
                        var hidVal = $("#ischange_" + id);
                        hidVal.val(true);

                     //   $(this).prop('checked', false);
                    }

                });
               // $(':checkbox[class=' + "isallowrite" + ']:not(:disabled)').prop('checked', true);
            }
            else {
                $('.isallowrite').each(function () {
                    var attr = $(this).attr('disabled');
                    if (typeof attr !== typeof undefined || attr === false) {
                       
                    } else {
                        $(this).prop('checked', false);
                        var id = $(this).data("id");
                        var hidVal = $("#ischange_" + id);
                        hidVal.val(true);
                       // $(this).prop('checked', false);
                    }

                });
                $(':checkbox[class=' + "isallowrite" + ']:not(:disabled)').prop('checked', false);
            }
        });

        $("#DivObjectRoleGrid").on("click", "#chkisallowdelete", function (event) {  //on click
            if (this.checked) { // check select status
                $('.isallowdelete').each(function () {
                    var attr = $(this).attr('disabled');

                    if (typeof attr !== typeof undefined || attr === false) {


                    } else {
                        $(this).prop('checked', true);
                        var id = $(this).data("id");
                        var hidVal = $("#ischange_" + id);
                        hidVal.val(true);
                        //   $(this).prop('checked', false);
                    }

                });
                // $(':checkbox[class=' + "isallowrite" + ']:not(:disabled)').prop('checked', true);
            }
            else {
                $('.isallowdelete').each(function () {
                    var attr = $(this).attr('disabled');
                    if (typeof attr !== typeof undefined || attr === false) {

                    } else {
                        $(this).prop('checked', false);
                        var id = $(this).data("id");
                        var hidVal = $("#ischange_" + id);
                        hidVal.val(true);
                        // $(this).prop('checked', false);
                    }

                });
            }
        });
        

        $("#DivObjectRoleGrid").on("change", ".isactivechk", function (event) {  //on click
            if (this.checked)                
            {                
                $(this).closest("tr").children().children('.dependant').removeAttr("disabled");
                var id = $(this).data("id");
                var hidVal = $("#ischange_" + id);
                hidVal.val(true);
                
            }
            else
            {                
                $(this).closest("tr").children().children('.dependant').prop('disabled', 'disabled');
                $(this).closest("tr").children().children('.dependant').prop('checked', false);
                var id = $(this).data("id");
                var hidVal = $("#ischange_" + id);
                hidVal.val(true);
            }
            
        });

        $("#DivObjectRoleGrid").on("change", ".isallowrite", function (event) {  //on click
            if (this.checked) {
                var id = $(this).data("id");
                var hidVal = $("#ischange_" + id);
                hidVal.val(true);

            }
            else {
                var id = $(this).data("id");
                var hidVal = $("#ischange_" + id);
                hidVal.val(true);
            }

        });

        $("#DivObjectRoleGrid").on("change", ".isallowdelete", function (event) {  //on click
            if (this.checked) {
                var id = $(this).data("id");
                var hidVal = $("#ischange_" + id);
                hidVal.val(true);

            }
            else {
                var id = $(this).data("id");
                var hidVal = $("#ischange_" + id);
                hidVal.val(true);
            }

        });

        $ddlSearch.change(function () {
            SearchRoleObject();
        });
        BindGridEvents();
    }
    
    //Search device using the form field values
    function SearchRoleObject() {

        var data = {};
        data.RoleId = $ddlSearch.val();

        
        var url = 'RoleObject/FilterRoleObject';

        MEPSTEMP.Global.MakeAjaxCall("POST", url, data, function (result) {

            $DivObjectRoleGrid.html(result);
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
        initialisePage: function (type) {
            initialiseControls(type);
        },
        resetPage: function () {

        }
    };
}());
/*** 
* Used for defining the MEPSTEMP  UI
* @module UI
* @namespace MEPSTEMP.temp
*/

MEPSTEMP.namespace("UI.Object");
MEPSTEMP.UI.Object = (function () {
    "use strict";
    var _isInitialized = false;
    //global variables for caching purposes
    var $ddlParentObject, $ddlObjectLevel, $DivParentObject;

    function initialiseControls(type) {

        if (_isInitialized == false) {
            _isInitialized = true;
            CacheEl();
            BindEvents(type);
            MEPSTEMP.Global.KeyLocking('.hasKeyLocking','data-keylocking');
        }
    }

    //Element caching done in here
    function CacheEl() {
        $ddlParentObject = $('#ddlParentObject');
        $ddlObjectLevel = $('#ddlObjectLevel');
        $DivParentObject = $("#DivParentObject");
    }
    //Event binding done in here
    function BindEvents(type) {
        MEPSTEMP.Global.ApplyMenuSelection('Object');
        MEPSTEMP.Global.ValidateDropdown('#ddlObjectLevel');
        $('#ddlObjectLevel').change(function () {          
            MEPSTEMP.Global.BindDropDownOnClick("GET", 'Dropdown/PopulateParentObjectDropDownList?ObjectLevel=' + $(this).val() + "&ParentObjectId=" + $('#ddlParentObject').data('saved'), 0, 0, function (result) {
                $DivParentObject.html(result);
              
                MEPSTEMP.Global.ValidateDropdownOnChange();
                if ($('#ddlParentObject').data('saved') > 0 && type == 1) {
                    $('#ddlParentObject').val($('#ddlParentObject').data('saved'));
                }               
            });
        });
        if (type == 1) {

            $('#ddlObjectLevel').val($('#ddlObjectLevel').data('saved'));
            $('#ddlObjectLevel').trigger('change');

        }


       
       
    }
    //Search device using the form field values
    
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
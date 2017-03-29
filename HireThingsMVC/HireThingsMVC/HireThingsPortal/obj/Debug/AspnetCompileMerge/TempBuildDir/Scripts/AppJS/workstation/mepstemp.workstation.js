/*** 
* Used for defining the MEPSTEMP  UI
* @module UI
* @namespace MEPSTEMP.temp
*/

MEPSTEMP.namespace("UI.Workstation");
MEPSTEMP.UI.Workstation = (function () {
    "use strict";
    var _isInitialized = false;
    //global variables for caching purposes

    var $DivOrganization;


    function initialiseControls(type) {

        if (_isInitialized == false) {
            _isInitialized = true;
            CacheEl();
            BindEvents(type);
        }
    }
    //Element caching done in here
    function CacheEl() {
       // $DivOrganization = $('#DivOrganization');
       
    }
    //Event binding done in here
    function BindEvents(type) {
        MEPSTEMP.Global.ApplyMenuSelection('Workstation');
        MEPSTEMP.Global.KeyLocking('.hasKeyLocking', 'data-keylocking');
        //  Functionality to handle ajax dropdown
        MEPSTEMP.Global.ValidateDropdown('#ddlSystem');
        $('#ddlSystem').focus();

        $('#ddlSystem').change(function () {
            MEPSTEMP.Global.BindDropDownOnClick("GET", 'Dropdown/PopulateOrganizationDropDownList?SystemId=' + $(this).val() + "&OrganizationId=" + $('#ddlOrganization').data('saved'), 0, 0, function (result) {
                
                $('#DivOrganization').html(result);
                MEPSTEMP.Global.ValidateDropdownOnChange();
                if ($('#ddlOrganization').data('saved') > 0 && type == 1) {
                    $('#ddlOrganization').val($('#ddlOrganization').data('saved'));
                }                
            });
        });
        if (type == 1) {

            $('#ddlSystem').val($('#ddlSystem').data('saved'));
            $('#ddlSystem').trigger('change');

        }




    }
 
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
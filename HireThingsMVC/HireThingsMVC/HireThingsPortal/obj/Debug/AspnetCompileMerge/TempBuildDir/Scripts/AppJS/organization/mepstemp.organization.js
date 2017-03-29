/*** 
* Used for defining the MEPSTEMP  UI
* @module UI
* @namespace MEPSTEMP.temp
*/

MEPSTEMP.namespace("UI.Organization");
MEPSTEMP.UI.Organization = (function () {
    "use strict";
    var _isInitialized = false;
    //global variables for caching purposes

    var $DivParentOrganization;


    function initialiseControls(type) {
       if (_isInitialized == false) {
            _isInitialized = true;
            CacheEl();
            BindEvents(type);
        }
    }
    //Element caching done in here
    function CacheEl() {
        $DivParentOrganization = $('#DivParentOrganization');
       
    }
    //Event binding done in here
    function BindEvents(type) {

         $('#ddlOrganizationLevel').change(function () {
            MEPSTEMP.Global.BindDropDownOnClick("GET", 'Dropdown/PopulateParentObjectLevelDropDownList?OrganizationLevelId=' + $(this).val() + "&SystemId=" + $('#ddlSystem').val(),0,0, function (result) {
                $('#DivParentOrganization').html(result);

                if ($('#ddlParentObject option').length < 2) {
                        $('#ddlParentObject').prop("disabled", true);
                }
            });

         });

         $('#ddlSystem').change(function () {
             if ($('#ddlOrganizationLevel').val() > 0) {
                 MEPSTEMP.Global.BindDropDownOnClick("GET", 'Dropdown/PopulateParentObjectLevelDropDownList?OrganizationLevelId=' + $('#ddlOrganizationLevel').val() + "&SystemId=" + $('#ddlSystem').val(), 0, 0, function (result) {
                     $('#DivParentOrganization').html(result);

                     if ($('#ddlParentObject option').length < 2) {
                         $('#ddlParentObject').prop("disabled", true);
                     }
                 });
             }
         });

        if ($('#ddlParentObject').data('saved') > 0) {
            $('#ddlParentObject').val($('#ddlParentObject').data('saved'));
        }

        if ($('#ddlParentObject option').length < 2) {
            $('#ddlParentObject').prop("disabled", true);
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
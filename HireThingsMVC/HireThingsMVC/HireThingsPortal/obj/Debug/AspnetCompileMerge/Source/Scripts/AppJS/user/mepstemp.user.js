/*** 
* Used for defining the MEPSTEMP  UI
* @module UI
* @namespace MEPSTEMP.temp
*/

MEPSTEMP.namespace("UI.User");
MEPSTEMP.UI.User = (function () {
    "use strict";
    var _isInitialized = false;
   

    function initialiseControls(type) {

        if (_isInitialized == false) {
            _isInitialized = true;
            CacheEl();
            BindEvents(type);
        }
    }

    //Element caching done in here
    function CacheEl() {
       
    }
    //Event binding done in here
    function BindEvents(type) {
            
        $('#groupListbox').parent().parent().find('.ListingHeading').each(function () {
            var $html = $(this).html();
            $html = $html.replace('Email Groups', 'Group');
            $(this).html($html);
        });

        MEPSTEMP.Global.ApplyMenuSelection('User');
        MEPSTEMP.Global.ApplyMasking('#PhoneNo', 'usphone');

        $('.datetimepicker').datetimepicker({
            minDate: '1900-01-01',
            maxDate: new Date()
        });
        $('.datepicker').datetimepicker({
            minDate: '1900-01-01',
            maxDate:new Date(),
            viewMode: 'years',
            format:'MM/DD/YYYY'
        });
        MEPSTEMP.Global.KeyLocking('.hasKeyLocking', 'data-keylocking');
        MEPSTEMP.Global.ValidateDropdown('#ddlSystem');
        $('#ddlSystem').change(function () {
            MEPSTEMP.Global.BindDropDownOnClick("GET", 'Dropdown/PopulateOrganizationDropDownList?SystemId=' + $(this).val() + "&OrganizationId=" + $('#ddlOrganization').data('saved'), 0, 0, function (result) {
                $('#DivOrganization').html(result);
                MEPSTEMP.Global.ValidateDropdownOnChange();
                if ($('#ddlOrganization').data('saved') > 0 && type == 1) {
                    $('#ddlOrganization').val($('#ddlOrganization').data('saved'));
                }
                $('#ddlOrganization').trigger('change');
            });
            
        });
        MEPSTEMP.Global.ValidateDropdownOnChange();
        if (type == 1) {

            $('#ddlSystem').val($('#ddlSystem').data('saved'));
            $('#ddlSystem').trigger('change');

        }

        $('.firstTextBox').focus();
       
       
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
/*** 
* Used for defining the HIRETHINGS  UI
* @module UI
* @namespace HIRETHINGS.temp
*/

HIRETHINGS.namespace("UI.User");
HIRETHINGS.UI.User = (function () {
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
        
        



        HIRETHINGS.Global.ApplyMenuSelection('User');
        HIRETHINGS.Global.ApplyMasking('#PhoneNo', 'usphone');

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
        HIRETHINGS.Global.KeyLocking('.hasKeyLocking', 'data-keylocking');
        HIRETHINGS.Global.ValidateDropdown('#ddlCountry');
        $('#ddlCountry').change(function () {
            HIRETHINGS.Global.BindDropDownOnClick("GET", 'Dropdown/PopulateLocationDropDownList?CountryId=' + $(this).val() + "&LocationId=" + $('#ddlLocation').data('saved'), 0, 0, function (result) {
                $('#DivLocation').html(result);
                HIRETHINGS.Global.ValidateDropdownOnChange();
                if ($('#ddlLocation').data('saved') > 0 && type == 1) {
                    $('#ddlLocation').val($('#ddlLocation').data('saved'));
                }
                $('#ddlLocation').trigger('change');
            });
            
        });
        HIRETHINGS.Global.ValidateDropdownOnChange();
        if (type == 1) {
            $('#ddlCountry').val($('#ddlCountry').data('saved'));
            $('#ddlCountry').trigger('change');
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
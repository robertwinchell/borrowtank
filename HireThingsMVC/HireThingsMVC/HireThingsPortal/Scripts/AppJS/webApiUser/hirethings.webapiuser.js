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
        
        $('#groupListbox').parent().parent().find('.ListingHeading').each(function () {
            var $html = $(this).html();
            $html = $html.replace('Email Groups', 'Device Groups');
            $(this).html($html);
        });
        $('#groupListbox').closest('form').on('submit', function (e) {
            $(".listBoxSel").each(function () {
                $(this).find('option').each(function () {
                    $(this).prop("selected", "true");
                });
            });

            if ($('#groupListbox').val() == null) {
                e.preventDefault();
                $('#groupListbox').parent().find('.text-danger').remove();
                $('#groupListbox').parent().append('<span data-valmsg-replace="true" data-valmsg-for="groupListbox" class="text-danger field-validation-error"><span for="groupListbox" class="">Device Group(s) is required.</span></span>');
            }
            else {                
                $('#groupListbox').parent().find('.text-danger').remove();
            }
        });
        



        HIRETHINGS.Global.ApplyMenuSelection('webapiuser');
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
        HIRETHINGS.Global.ValidateDropdown('#ddlSystem');
        $('#ddlSystem').change(function () {
            HIRETHINGS.Global.BindDropDownOnClick("GET", 'Dropdown/PopulateOrganizationDropDownList?SystemId=' + $(this).val() + "&OrganizationId=" + $('#ddlOrganization').data('saved'), 0, 0, function (result) {
                $('#DivOrganization').html(result);
                HIRETHINGS.Global.ValidateDropdownOnChange();
                if ($('#ddlOrganization').data('saved') > 0 && type == 1) {
                    $('#ddlOrganization').val($('#ddlOrganization').data('saved'));
                }
                $('#ddlOrganization').trigger('change');
            });
            
        });
        HIRETHINGS.Global.ValidateDropdownOnChange();
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
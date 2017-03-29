/*** 
* Used for defining the MEPSTEMP  UI
* @module UI
* @namespace MEPSTEMP.temp
*/

MEPSTEMP.namespace("UI.Device");
MEPSTEMP.UI.Device = (function () {
    "use strict";
    var _isInitialized = false;
    //global variables for caching purposes

    var $listBoxAvail, $listBoxSel, $add, $remove, $deviceForm, $ddlOrganization, $ddlWorkStation, $ddlSystem, $DivOrganization, $DivWorkStation;


    function initialiseControls(type) {
       
        if (_isInitialized == false) {
            _isInitialized = true;
            CacheEl();
            BindEvents(type);

        }
    }
    //Element caching done in here
    function CacheEl() {

        $deviceForm = $('#deviceForm');
        $ddlSystem = $('#ddlSystem');
        $ddlOrganization = $('#ddlListItemId');
        $ddlWorkStation = $('#ddlWorkstation');
        $DivOrganization = $("#DivOrganization");
        $DivWorkStation = $("#DivWorkStation");
    }
    //Event binding done in here
    function BindEvents(type) {        
        MEPSTEMP.Global.ApplyMenuSelection('Device');
        $('#groupListbox').parent().parent().find('.ListingHeading').each(function(){
            var $html = $(this).html();
            $html=$html.replace('Email Groups', 'Device Groups');
            $(this).html($html);            
        });
        $('#probeListbox').parent().parent().find('.ListingHeading').each(function () {
            var $html = $(this).html();
            $html = $html.replace('Email Groups', 'Probes');
            $(this).html($html);
        });
        //Masking and keylocking - Starts here
        MEPSTEMP.Global.ApplyMasking('#MacAddress', 'macadd');
        MEPSTEMP.Global.ApplyMasking('#DeviceIP', 'ipadd');
        MEPSTEMP.Global.KeyLocking('.hasKeyLocking', 'data-keylocking');
        // Masking and keylocking - Ends here
        $('.datetimepicker').datetimepicker({
            minDate: '1900-01-01',
            maxDate: new Date(),
            useCurrent: false
        });
        
        MEPSTEMP.UI.Shared.initialisePage('#deviceForm', '.listboxDiv');
        $deviceForm.submit(function (e) {
            $(".listBoxSel").each(function () {
                $(this).find('option').each(function () {
                    $(this).prop("selected", "true");
                });
            });
          
            MEPSTEMP.Global.KeyLocking('.hasKeyLocking', 'data-keylocking');
            if ($('#groupListbox').val() == null) {
                e.preventDefault();
                $('#groupListbox').parent().find('.text-danger').remove();
                $('#groupListbox').parent().append('<span data-valmsg-replace="true" data-valmsg-for="deviceListbox" class="text-danger field-validation-error"><span for="groupListbox" class="">Device Group(s) is required.</span></span>');
            } else { $('#groupListbox').parent().find('.text-danger').remove(); }

            if ($('#probeListbox').val() == null) {
                e.preventDefault();
                $('#probeListbox').parent().find('.text-danger').remove();
                $('#probeListbox').parent().append('<span data-valmsg-replace="true" data-valmsg-for="probeListbox" class="text-danger field-validation-error"><span for="probeListbox" class="">Probe(s) is required.</span></span>');
            } else { $('#probeListbox').parent().find('.text-danger').remove(); }

        });
        //  Functionality to handle ajax dropdown
        MEPSTEMP.Global.ValidateDropdown('#ddlSystem');
        $('#ddlSystem').on('change', function () {
            $(this).parent().children('.text-danger').remove();
            MEPSTEMP.Global.BindDropDownOnClick("GET", 'Dropdown/PopulateOrganizationDropDownList?SystemId=' + $(this).val() + "&OrganizationId=" + $('#ddlOrganization').data('saved'), 0, 0, function (result) {
                $DivOrganization.html(result);
                $('#ddlOrganization').change(function () {
                    MEPSTEMP.Global.BindDropDownOnClick("GET", 'Dropdown/PopulateWorkStationDropDownList?OrganizationId=' + $(this).val() + "&WSId=" + $('#ddlWorkstation').data('saved'), 0, 0, function (result) {
                        $DivWorkStation.html(result);
                        if ($('#ddlWorkstation').data('saved') > 0 && type == 1) {
                            $('#ddlWorkstation').val($('#ddlWorkstation').data('saved'));
                        }
                        MEPSTEMP.Global.ValidateDropdownOnChange();
                    });
                });
                if ($('#ddlOrganization').data('saved') > 0) {
                    $('#ddlOrganization').val($('#ddlOrganization').data('saved'));
                }
                $('#ddlOrganization').trigger('change');
            });                        
         
        });
        $('#ddlSystem').val($('#ddlSystem').data('saved'));
        $('#ddlSystem').trigger('change');
        
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
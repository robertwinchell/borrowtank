/*** 
* Used for defining the HIRETHINGS  UI
* @module UI
* @namespace HIRETHINGS.temp
*/

HIRETHINGS.namespace("UI.Device");
HIRETHINGS.UI.Device = (function () {
    "use strict";
    var _isInitialized = false;
    //global variables for caching purposes

    var $listBoxAvail, $listBoxSel, $add, $remove, $deviceForm, $ddlOrganization, $ddlWorkStation, $ddlSystem, $DivOrganization, $DivWorkStation
        , $MacAddress, $DeviceSrNo;


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
        $MacAddress = $("#MacAddress");
        $DeviceSrNo = $("#DeviceSrNo");
        
    }
    //Event binding done in here
    function BindEvents(type) {        
        HIRETHINGS.Global.ApplyMenuSelection('Device');

        $MacAddress.blur(function (e) {
            var macAddressVal = $MacAddress.val();
            var pattern = /^([a-fA-F0-9]{2}[:][a-fA-F0-9]{2}[:][a-fA-F0-9]{2}[:][a-fA-F0-9]{2}[:][a-fA-F0-9]{2}[:][a-fA-F0-9]{2})$/;
            if (pattern.test(macAddressVal)) {
                $DeviceSrNo.val(macAddressVal);

            } else
            {
                $DeviceSrNo.val("");
            }

            
        });

        $('#groupListbox').parent().parent().find('.ListingHeading').each(function () {
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
        HIRETHINGS.Global.ApplyMasking('#MacAddress', 'macadd');
        HIRETHINGS.Global.ApplyMasking('#DeviceIP', 'ipadd');
        HIRETHINGS.Global.KeyLocking('.hasKeyLocking', 'data-keylocking');
        // Masking and keylocking - Ends here

        var maxDate = new Date();
        maxDate.setFullYear(maxDate.getFullYear() + 100);

        $('.datetimepicker').datetimepicker({
            minDate: '1900-01-01',
            maxDate: maxDate,
            useCurrent: false
        });
        
        HIRETHINGS.UI.Shared.initialisePage('#deviceForm', '.listboxDiv');
        $deviceForm.submit(function (e) {
            $(".listBoxSel").each(function () {
                $(this).find('option').each(function () {
                    $(this).prop("selected", "true");
                });
            });
          
            HIRETHINGS.Global.KeyLocking('.hasKeyLocking', 'data-keylocking');
            if ($('#groupListbox').val() == null) {
                e.preventDefault();
                $('#groupListbox').parent().find('.text-danger').remove();
                $('#groupListbox').parent().append('<span data-valmsg-replace="true" data-valmsg-for="deviceListbox" class="text-danger field-validation-error"><span for="groupListbox" class="">Device Group(s) is required.</span></span>');
            } else { $('#groupListbox').parent().find('.text-danger').remove(); }

            // since probe is no longer required
            //if ($('#probeListbox').val() == null) {
            //    e.preventDefault();
            //    $('#probeListbox').parent().find('.text-danger').remove();
            //    $('#probeListbox').parent().append('<span data-valmsg-replace="true" data-valmsg-for="probeListbox" class="text-danger field-validation-error"><span for="probeListbox" class="">Probe(s) is required.</span></span>');
            //} else { $('#probeListbox').parent().find('.text-danger').remove(); }

        });
        //  Functionality to handle ajax dropdown
        HIRETHINGS.Global.ValidateDropdown('#ddlSystem');
        $('#ddlSystem').on('change', function () {
            $(this).parent().children('.text-danger').remove();
            HIRETHINGS.Global.BindDropDownOnClick("GET", 'Dropdown/PopulateOrganizationDropDownList?SystemId=' + $(this).val() + "&OrganizationId=" + $('#ddlOrganization').data('saved'), 0, 0, function (result) {
                $DivOrganization.html(result);
                $('#ddlOrganization').change(function () {
                    HIRETHINGS.Global.BindDropDownOnClick("GET", 'Dropdown/PopulateWorkStationDropDownList?OrganizationId=' + $(this).val() + "&WSId=" + $('#ddlWorkstation').data('saved'), 0, 0, function (result) {
                        $DivWorkStation.html(result);
                        if ($('#ddlWorkstation').data('saved') > 0 && type == 1) {
                            $('#ddlWorkstation').val($('#ddlWorkstation').data('saved'));
                        }
                        HIRETHINGS.Global.ValidateDropdownOnChange();
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
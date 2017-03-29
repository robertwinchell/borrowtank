/*** 
* Used for defining the MEPSTEMP  UI
* @module UI
* @namespace MEPSTEMP.temp
*/

MEPSTEMP.namespace("UI.Group");
MEPSTEMP.UI.Group = (function () {
    "use strict";
    var _isInitialized = false;

    

    function initialiseControls() {

        if (_isInitialized == false) {
            _isInitialized = true;
         
            BindEvents();
        }
    }
    
    //Event binding done in here
    function BindEvents() {
        //scince it is partial view so we need to change label in here
        
        $('#escalationListbox').parent().parent().find('.ListingHeading').each(function () {
            var $html = $(this).html();
            $html = $html.replace('Email Groups', 'Escalations');
            $(this).html($html);
        });

        $('#deviceListbox').parent().parent().find('.ListingHeading').each(function () {
            var $html = $(this).html();
            $html = $html.replace('Email Groups', 'Devices');
            $(this).html($html);
        });

        $('#GroupForm').submit(function (e) {
            //mark the options seleced in the right listbox
            $(".listBoxSel").each(function () {
                $(this).find('option').each(function () {
                    $(this).prop("selected", "true");
                });
            });

            //device group listbox client end validation
            //if ($('#deviceListbox').val() == null) {
            //    e.preventDefault();
            //    $('#deviceListbox').parent().find('.text-danger').remove();
            //    $('#deviceListbox').parent().append('<span data-valmsg-replace="true" data-valmsg-for="deviceListbox" class="text-danger field-validation-error"><span for="deviceListbox" class="">Device(s) is required.</span></span>');
            //} else { $('#deviceListbox').parent().find('.text-danger').remove(); }
        });
        //menu selection
        MEPSTEMP.Global.ApplyMenuSelection('Group');
        //Listbox partial view-button and other functionality
        MEPSTEMP.UI.Shared.initialisePage('#GroupForm', '.listboxDiv');

        MEPSTEMP.Global.ValidateDropdown('#ddlSystem');
        MEPSTEMP.Global.ValidateDropdownOnChange();
        var sysDataSaved = $('#ddlSystem').data('saved');
        if (sysDataSaved <= 0)
            $('#ddlSystem').val(-1);
        else
            $('#ddlSystem').val(sysDataSaved);
    }

   
    //Clear Fields function
    function ClearFields() { }

    return {
        readyMain: function () {

        },
        initialisePage: function () {
            initialiseControls();
        },
        resetPage: function () {

        }
    };
}());
﻿/*** 
* Used for defining the MEPSTEMP  UI
* @module UI
* @namespace MEPSTEMP.temp
*/

MEPSTEMP.namespace("UI.EmailGroup");
MEPSTEMP.UI.EmailGroup = (function () {
    "use strict";
    var _isInitialized = false;
    //global variables for caching purposes

    var $listBoxAvail, $listBoxSel, $add, $remove, $EmailGroupForm, $ddlOrganization, $ddlWorkStation, $ddlSystem, $DivOrganization, $DivWorkStation;


    function initialiseControls(type) {

        if (_isInitialized == false) {
            _isInitialized = true;
            CacheEl();
            BindEvents(type);
        }
    }
    //Element caching done in here
    function CacheEl() {

        $EmailGroupForm = $('#EmailGroupForm');
    }

    //Event binding done in here
    function BindEvents(type) {
      

        MEPSTEMP.Global.ValidateDropdown('#ddlSystem');
        MEPSTEMP.Global.ValidateDropdownOnChange();
        var sysDataSaved = $('#ddlSystem').data('saved');
        if (sysDataSaved<=0)
            $('#ddlSystem').val(-1);
        else
            $('#ddlSystem').val(sysDataSaved);
        var emailRegEx = new RegExp(/^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$/i);
        var emailWithNameRegEx = new RegExp(/^([a-z]|\d|\s|[!#\$%&'\*\+\-\/=\.\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*(\<)((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))(\>)\.?$/i);

        $('.tagedInput').on('beforeItemAdd', function (event) {
            if (!emailRegEx.test(event.item) && !emailWithNameRegEx.test(event.item)) {               
                event.cancel = true;
            }
        });
        $('.tagedInput').on('itemAdded', function (event) {
            $('#' + event.delegateTarget.id).parent().find('.bootstrap-tagsinput').find('input').addClass('clearTagInput');           
        });
        function ClearAll() { };
        $(".tagedInput").tagsinput({
            tagClass: 'tag-styled',
            confirmKeys: [9,13, 59, 44],
            trimValue: true,
            inputSize: 15,
            typeahead: {
                source: function (querys) {
                    var dataT = {};
                    dataT.query = querys

                    //$.ajax({
                    //    url: '../EmailGroup/EmailTypeahead',
                    //    data: dataT,
                    //    dataType: 'json'
                    //});
                 
                    return $.get('../EmailGroup/EmailTypeahead', dataT);
                },                                             
                afterSelect: function () {
                    $('.clearTagInput').val('');
                }                
            }
        })
        
     
        $('#MailTo').closest('form').submit(function (e) {
            // var val = true;
            if ($('#MailTo').val() == '') {
                $('#MailTo').parent().children('.text-danger').remove();
                $('#MailTo').parent().append('<span class="text-danger">Please specify at least one recipient in the "To" field.</span>');
                e.preventDefault();
            }
        });

        $('.bootstrap-tagsinput').each(function () {
            var $this=$(this);
            $this.focusout(function () {
                if ($($this).find('.tag-styled').length > 0) {
                    $this.parent().find('.text-danger').remove();
                }
            });
            });
    
      

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
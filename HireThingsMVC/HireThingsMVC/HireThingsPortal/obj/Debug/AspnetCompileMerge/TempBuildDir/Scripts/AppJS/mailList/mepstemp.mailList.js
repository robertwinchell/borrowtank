/*** 
* Used for defining the MEPSTEMP  UI
* @module UI
* @namespace MEPSTEMP.temp
*/

MEPSTEMP.namespace("UI.MailList");
MEPSTEMP.UI.MailList = (function () {
    "use strict";
    var _isInitialized = false;
    //global variables for caching purposes

    var $listBoxAvail, $listBoxSel, $add, $remove, $MailListForm, $ddlOrganization, $ddlWorkStation, $ddlSystem, $DivOrganization, $DivWorkStation;


    function initialiseControls(type) {
       
        if (_isInitialized == false) {
            _isInitialized = true;
            CacheEl();
            BindEvents(type);
        }
    }
    //Element caching done in here
    function CacheEl() {
       
        $MailListForm = $('#MailListForm');
        $ddlSystem = $('#ddlSystem');
        $ddlOrganization = $('#ddlListItemId');
        $DivOrganization = $("#DivOrganization");
        
        
    }
    //Event binding done in here
    function BindEvents(type) {
        
        MEPSTEMP.Global.ApplyMenuSelection('MailList');
   
      
        $('.emailGroupList').chosen({ placeholder_text_multiple: '', width: '100%' });
        var emailRegEx = new RegExp(/^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$/i);
        var emailWithNameRegEx = new RegExp(/^([a-z]|\d|\s|[!#\$%&'\*\+\-\/=\.\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*(\<)((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))(\>)\.?$/i);
        $(".tagedInput").tagsinput({
            tagClass: 'tag-styled',
            confirmKeys: [13, 59, 44],
            trimValue: true,
            inputSize:15
          
        })
        $('.tagedInput').on('beforeItemAdd', function (event) {            
            if (!emailRegEx.test(event.item) && !emailWithNameRegEx.test(event.item)) {
                console.log('invalid item');
                event.cancel = true;
            } 
            // event.item: contains the item
            // event.cancel: set to true to prevent the item getting added
        });
        $('#emailListId').closest('form').submit(function (e) {

            var val = true;
            
            if ($('#emailListId').val() == '')
            {
                $('#emailListId').parent().children('.text-danger').remove();
                $('#emailListId').parent().append('<span class="text-danger">Please specify at least one recipient in the TO field.</span>');
                e.preventDefault();
            }
        });

            //$('.ddlWithOnClick').each(function (i, obj) {

            //    if ($(this).val() < 0) {
            //        val = false;
            //        $(this).parent().children('.text-danger').remove();
            //        $(this).parent().append(GenerateValidationMessage($(this).attr('id')));
            //    }
            //    else {
            //        $(this).parent().children('.text-danger').remove();
            //    }
            //});

            //if (!val) {
            //    e.preventDefault();
            //};
      //  });
        //  Functionality to handle ajax dropdown
        MEPSTEMP.Global.ValidateDropdown('#ddlSystem');
        $('#ddlSystem').change(function () {
            MEPSTEMP.Global.BindDropDownOnClick("GET", 'Dropdown/PopulateOrganizationDropDownList?SystemId=' + $(this).val() + "&OrganizationId=" + $('#ddlOrganization').data('saved'), 0, 0, function (result) {
                $DivOrganization.html(result);          
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
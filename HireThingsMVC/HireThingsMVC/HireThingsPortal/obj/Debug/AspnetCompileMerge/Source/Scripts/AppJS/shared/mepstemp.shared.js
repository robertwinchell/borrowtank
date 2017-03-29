/*** 
* Used for defining the MEPSTEMP  UI
* @module UI
* @namespace MEPSTEMP.temp
*/

MEPSTEMP.namespace("UI.Shared");
MEPSTEMP.UI.Shared = (function () {
    "use strict";
    var _isInitialized = false;
    //global variables for caching purposes

    var $listBoxAvail, $listBoxSel, $add, $remove, $Form;

    function initialiseControls(form, divarr) {

        if (_isInitialized == false) {
            _isInitialized = true;
           
            CacheEl(form);
            BindEvents(divarr);
        }
    }
        //Element caching done in here
        function CacheEl(form) {
           
            $Form = $(form);
         
        }
        //Event binding done in here
        function BindEvents(divarr) {
            console.log('BindEvents');
            $('.btnAddRight').each(function () {
                $(this).click(function () {
                    console.log('add right');
                    var listBoxSel = $(this).closest(divarr).find('.listBoxSel');
                    $(this).closest(divarr).find('.listBoxAvail').find('option:selected').each(function () {
                        $(this).remove().appendTo(listBoxSel);
                    });
                });
            });
            $('.btnRemoveRight').each(function () {
                $(this).click(function () {
                    var listBoxAvail = $(this).closest(divarr).find('.listBoxAvail');
                    $(this).closest(divarr).find('.listBoxSel').find('option:selected').each(function () {
                        $(this).remove().appendTo(listBoxAvail);
                    });
                });
            });

            $Form.submit(function (e) {

                $(".listBoxSel").each(function () {
                    $(this).find('option').each(function () {
                        $(this).prop("selected", "true");
                    });
                });

            });
        }
        //Clear Fields function
        function ClearFields() { }
    
    return {
        readyMain: function () {
           
        },
        initialisePage: function (form,divarr) {
            initialiseControls(form, divarr);
        },
        resetPage: function () {
          
        }
    };
}());
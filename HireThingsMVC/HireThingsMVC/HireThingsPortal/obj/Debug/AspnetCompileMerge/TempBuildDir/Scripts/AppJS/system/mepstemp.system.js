/*** 
* Used for defining the MEPSTEMP  UI
* @module UI
* @namespace MEPSTEMP.temp
*/

MEPSTEMP.namespace("UI.System");
MEPSTEMP.UI.System = (function () {
    "use strict";
    var _isInitialized = false;
    //global variables for caching purposes

    var $DivSystem;


    function initialiseControls(type) {

        if (_isInitialized == false) {
            _isInitialized = true;
            CacheEl();
            BindEvents(type);
        }
    }
    //Element caching done in here
    function CacheEl() {
        // $DivOrganization = $('#DivOrganization');

    }
    //Event binding done in here
    function BindEvents(type) {
        
        $("#txtSystem").keydown(function (event) {

            if (event.keyCode == 222 || event.keyCode == 39 || event.keyCode == 34) {
                event.preventDefault();

            }
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
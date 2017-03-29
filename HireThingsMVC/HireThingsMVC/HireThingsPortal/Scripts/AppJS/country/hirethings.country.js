/*** 
* Used for defining the HIRETHINGS  UI
* @module UI
* @namespace HIRETHINGS.temp
*/

HIRETHINGS.namespace("UI.Country");
HIRETHINGS.UI.Country = (function () {
    "use strict";
    var _isInitialized = false;
    //global variables for caching purposes

    var $DivCountry;


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
        
        $("#txtCountry").keydown(function (event) {

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
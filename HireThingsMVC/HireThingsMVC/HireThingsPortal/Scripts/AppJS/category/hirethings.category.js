/*** 
* Used for defining the HIRETHINGS  UI
* @module UI
* @namespace HIRETHINGS.temp
*/

HIRETHINGS.namespace("UI.Category");
HIRETHINGS.UI.Category = (function () {
    "use strict";
    var _isInitialized = false;
    //global variables for caching purposes
    // var $txtEntryDate, $txtExpiryDate;

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

        HIRETHINGS.Global.ApplyMenuSelection('Category');
        HIRETHINGS.Global.KeyLocking('.hasKeyLocking', 'data-keylocking');

       
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

        },
        CustomValidation: function () {
          
        }
    };
}());
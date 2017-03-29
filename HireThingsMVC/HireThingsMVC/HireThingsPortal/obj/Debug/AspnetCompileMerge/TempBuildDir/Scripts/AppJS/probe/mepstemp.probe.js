/*** 
* Used for defining the MEPSTEMP  UI
* @module UI
* @namespace MEPSTEMP.temp
*/

MEPSTEMP.namespace("UI.Probe");
MEPSTEMP.UI.Probe = (function () {
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
        //$txtEntryDate = $('#txtEntryDate');
        //$txtExpiryDate = $('#txtExpiryDate');
    }
    //Event binding done in here
    function BindEvents(type) {
       
       
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
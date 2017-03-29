/*** 
* Used for defining the HIRETHINGS  UI
* @module UI
* @namespace HIRETHINGS.temp
*/

HIRETHINGS.namespace("UI.Advertisement.Public");
HIRETHINGS.UI.Advertisement.Public = (function () {
    "use strict";
    var _isInitialized = false;
   

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

       // HIRETHINGS.Global.ApplyMenuSelection('PublicAdvertisement');
        HIRETHINGS.Global.KeyLocking('.hasKeyLocking', 'data-keylocking');
        HIRETHINGS.UI.HirePlana.initialisePage('.btnAddToHireplan', 'advertisementid');
        HIRETHINGS.Global.KeyLocking('.keylock', 'data-keylock');

        $('.btnAdvertiserDetail').click(function (e) {
            alert('Redirect the user to profile of advertiser id:  ' + $(this).data('advertiser'));
        });
    }
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
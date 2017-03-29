/*** 
* Used for defining the HIRETHINGS  UI
* @module UI
* @namespace HIRETHINGS.temp
*/

HIRETHINGS.namespace("UI.HirePlana");
HIRETHINGS.UI.HirePlana = (function () {
    "use strict";
    var _isInitialized = false;
    //global variables for caching purposes

    var $listBoxAvail, $listBoxSel, $add, $remove, $Form;

    function initialiseControls(element,dataAttr) {

        if (_isInitialized == false) {
            _isInitialized = true;
           
            CacheEl();
            BindEvents(element, dataAttr);
        }
    }
        //Element caching done in here
        function CacheEl() {
           
          
         
        }
        //Event binding done in here
        function BindEvents(element,dataAttr) {

            $(element).click(function () {

               alert('will add advertisement id # ' + $(this).data(dataAttr) + 'to hirePlana');
            });
          
        }
        //Clear Fields function
        function ClearFields() { }
    
    return {
        readyMain: function () {
           
        },
        initialisePage: function (element, dataAttr) {
            initialiseControls(element, dataAttr);
        },
        resetPage: function () {
          
        }
    };
}());
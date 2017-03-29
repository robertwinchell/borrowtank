/*** 
* Used for defining the MEPSTEMP temp UI
* @module UI
* @namespace MEPSTEMP.temp
*/

MEPSTEMP.namespace("Utilities");
MEPSTEMP.Utilities = (function () {
    "use strict";

    function globalAllowedKeys(type, e, control) {
        var arr = [];
        if (type == 'specialalphabet') {
            arr = [46, 8, 9, 27, 13, 144,57,48,190,109,188,189];
        } else if (type == 'numeric') {
            arr = [46, 8, 9, 27, 13, 144];
        } else if (type == 'decimal') {
            if ($(control).val().indexOf('.') > -1) {
                arr = [46, 8, 9, 27, 13, 190, 144];
            }
            else {
                arr = [46, 8, 9, 27, 13, 110, 190, 144];
            }
        } else {
            arr = [46, 32, 8, 9, 27, 13, 110, 190, 109, 107, 144];
        }
        
        if ($.inArray(e.keyCode, arr) !== -1 ||
            // Allow: Ctrl+A
                   (e.keyCode == 65 && e.ctrlKey === true) ||
            // Allow: Ctrl+C
                   (e.keyCode == 67 && e.ctrlKey === true) ||
            // Allow: Ctrl+X
                     (e.keyCode == 86 && e.ctrlKey === true) ||
            // Allow: Ctrl+V
                   (e.keyCode == 88 && e.ctrlKey === true) ||
            // // Allow: Ctrl+Shift+R
                   (e.keyCode == 82 && e.keyCode == 16 && e.ctrlKey === true) ||
            //Allow Shift Key
                    (e.keyCode==16)||

            // Allow: home, end, left, right,down
                   (e.keyCode >= 35 && e.keyCode <= 40) && !(e.keyCode == 190 && e.shiftKey)) {
            // let it happen, don't do anything
            if((e.keyCode === 190 && e.shiftKey))
                return false;
           

            return true;
        } else { return false; }

    }
    function NumericAllowedKeys(e) {
        // Ensure that it is a number and stop the keypress
        if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            return false;
        } else {
            return true;
        }
    }
    function SpecialAllowedCharacter(e) {
        if ($.inArray(e.keyCode, [17, 32, 38, 48, 49, 50, 52, 55, 57, 173, 186, 189, 191, 192, 219, 220, 221, 222, 111, 110, 109, 9]) !== -1) {
            return true;
        } else {
            return false;
        }
    }
    function AlphabetAllowedKeys(e) {
        if (e.keyCode >= 65 && e.keyCode <= 90 || e.keyCode==32) {
            return true;
        }
        else {
            return false;
        }
    }

    function NumericKeysWithDot(e) {
        if (e.keyCode != 190) {
            return false;
        }
        else {
            return true;
        }
    }
    
    return {
        getRegularExp: function (identifier, e, control) {
            switch ($.trim(identifier.toLowerCase())) {
                case 'special-alphanumeric':
                    if (globalAllowedKeys('specialalphabet', e) || (NumericAllowedKeys(e) || AlphabetAllowedKeys(e))) {
                    }
                    else {
                        e.preventDefault();
                    }
                    break;
                case 'alphanumeric':
                    if (globalAllowedKeys('numeric', e) || (NumericAllowedKeys(e) || AlphabetAllowedKeys(e))) {
                    }
                    else {
                        e.preventDefault();
                    }
                    break;
                case 'alphabet':
                    if (globalAllowedKeys('', e) || AlphabetAllowedKeys(e)) {
                    }
                    else {
                        e.preventDefault();
                    } break;
                case 'numeric':
                    if (globalAllowedKeys('numeric', e) || NumericAllowedKeys(e)) {                       
                    }
                    else {                       
                        e.preventDefault();
                    }
                    break;
                case 'decimal':                  
                    if (globalAllowedKeys('decimal', e, control) || NumericAllowedKeys(e)) {                    
                    }
                    else {                       
                        e.preventDefault();
                    }
                    break;
                case 'ipaddress':
                    if (globalAllowedKeys('ipaddress', e, control) || NumericAllowedKeys(e)) {
                    }
                    else {
                        e.preventDefault();
                    }
                    break;
                default:
                    return;


            }


        },
        maskType: function (identifier) {
            switch ($.trim(identifier.toLowerCase())) {
                case 'ipadd':
                    $.mask.definitions['~'] = '([0-9] )?';
                    return '~~9.~~9.~~9.~~9';
                    break;
                case 'macadd':
                    return 'AA:AA:AA:AA:AA:AA';
                    break;
                default:
                    return identifier;
            }
        },
        ipaddressOptions: function () {

            var options = {
                onKeyPress: function (cep, event, currentField, options) {
                        console.log('An key was pressed!:', cep, ' event: ', event,'currentField: ', currentField, ' options: ', options);
                    if (cep) {
                        var ipArray = cep.split(".");
                        var lastValue = ipArray[ipArray.length - 1];
                        if (lastValue != "" && parseInt(lastValue) > 255) {
                            ipArray[ipArray.length - 1] = '255';
                            var resultingValue = ipArray.join(".");
                            currentField.attr('value', resultingValue);
                        }
                    }
                }
            };
            return options;

        }

    };
}());
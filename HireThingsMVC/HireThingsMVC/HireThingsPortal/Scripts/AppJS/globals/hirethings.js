/*** 
* Used for defining the HIRETHINGS namespace
* @namespace HIRETHINGS
*/

var HIRETHINGS = HIRETHINGS || {};

HIRETHINGS.namespace = function (ns_string) {
    "use strict";
    var parts = ns_string.split('.'),
        parent = HIRETHINGS,
        i;

    // strip redundant leading global
    if (parts[0] === "HIRETHINGS") {
        parts = parts.slice(1);
    }

    for (i = 0; i < parts.length; i += 1) {
        // create a property if it doesn't exist
        if (typeof parent[parts[i]] === "undefined") {
            parent[parts[i]] = {};
        }
        parent = parent[parts[i]];
    }
    return parent;
};
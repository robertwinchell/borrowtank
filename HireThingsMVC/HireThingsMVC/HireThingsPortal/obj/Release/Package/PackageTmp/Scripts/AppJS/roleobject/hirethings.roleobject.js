/*** 
* Used for defining the HIRETHINGS  UI
* @module UI
* @namespace HIRETHINGS.temp
*/

HIRETHINGS.namespace("UI.RoleObject");
HIRETHINGS.UI.RoleObject = (function () {
    "use strict";
    var _isInitialized = false;
    //global variables for caching purposes
    var $ddlSearch, $DivObjectRoleGrid;

    function initialiseControls(type) {

        if (_isInitialized == false) {
            _isInitialized = true;
            CacheEl();
            BindEvents(type);
        }
    }

    //Element caching done in here
    function CacheEl() {
        $ddlSearch = $('#ddlSearch');
        $DivObjectRoleGrid = $("#DivObjectRoleGrid");
    }
    //Event binding done in here
    function BindEvents(type) {
        HIRETHINGS.Global.ApplyMenuSelection('RoleObject');
        if ($ddlSearch.val() > 0) {
            $("#btnRow").show();
        }

        $ddlSearch.change(function () {
            SearchRoleObject();
        });

        $("#DivObjectRoleGrid").on("click", "#btnCancel", function (e) {
            $ddlSearch.val("-1");
            $ddlSearch.trigger("change");
        });

        $("#DivObjectRoleGrid").on("click", "#btnSave", function (e) {
            var data = {};
            data.RoleId = $ddlSearch.val();
            data.RoleObjectList = [];

            $("tr[data-changed]").each(function () {
                var roleObject = {};
                roleObject.ObjectName = $(this).find("td").eq(0).text();
                roleObject.RoleObjectId = $(this).data("roleobjectid");
                roleObject.ObjectID = $(this).data("objectid");
                roleObject.IsActive = $(this).find("input.IsActive").is(":checked");
                roleObject.AllowWrite = ($(this).find("input.IsActive").is(":checked") && $(this).find("input.AllowWrite").is(":checked"));
                roleObject.AllowDelete = ($(this).find("input.IsActive").is(":checked") && $(this).find("input.AllowDelete").is(":checked"));

                data.RoleObjectList.push(roleObject);
            });

            if (data.RoleObjectList.length > 0) {
                var jsonDataString = JSON.stringify(data);

                var url = 'RoleObject/Index';
                HIRETHINGS.Global.MakeJSONAjaxCall("POST", url, jsonDataString, function (result) {
                    ShowAlert(result.IsSuccess);

                }, function (xhr, ajaxOptions, thrownError) {
                    //debugger;
                    console.log('an error occurred in making ajax call');
                });
            }
            else {
                console.log("data not changed");
            }
        });
        //BindGridEvents();
    }

    //Search device using the form field values
    function SearchRoleObject() {


        var data = {};
        data.RoleId = $ddlSearch.val();
        var url = 'RoleObject/FilterRoleObject';
        HIRETHINGS.Global.MakeAjaxCall("POST", url, data, function (result) {
            $DivObjectRoleGrid.html(result);
            $('.tree').treegrid();

            if ($("#DivObjectRoleGrid table tbody tr").length > 0) {
                $("#btnRow").show();
            }
            BindGridEvents();
        }, function (xhr, ajaxOptions, thrownError) {
            //debugger;
            console.log('an error occurred in making ajax call');
        });
    }


    function BindGridEvents() {

        $("#chkIsActive").change(function () {

            //Apply data change attribute
            $(".IsActive").not(".dependent").each(function () {
                if ($(this).closest("tr").data("changed") == null) {
                    $(this).closest("tr").attr("data-changed", true);
                }
            });

            if ($(this).is(":checked")) {
                $(".IsActive").not(".dependent").prop("checked", true);
                $(".AllowWrite").not(".dependent").removeAttr("disabled");
                $(".AllowDelete").not(".dependent").removeAttr("disabled");
            }
            else {
                $("#chkIsAllowWrite").prop("checked", false);
                $("#chkIsAllowDelete").prop("checked", false);
                $(".IsActive").not(".dependent").prop("checked", false);
                $(".AllowWrite").not(".dependent").prop("checked", false).attr("disabled", "disabled");
                $(".AllowDelete").not(".dependent").prop("checked", false).attr("disabled", "disabled");
            }
        });

        $("#chkIsAllowWrite").change(function () {
            if ($(this).is(":checked")) {
                $(".AllowWrite").not(".dependent").not(":disabled").prop("checked", true);
            }
            else {
                $(".AllowWrite").not(".dependent").not(":disabled").prop("checked", false);
            }
        });

        $("#chkIsAllowDelete").change(function () {
            if ($(this).is(":checked")) {
                $(".AllowDelete").not(".dependent").not(":disabled").prop("checked", true);
            }
            else {
                $(".AllowDelete").not(".dependent").not(":disabled").prop("checked", false);
            }
        });

        // IsActive Checkbox 
        $("tr.Level1-TableTree").find("input.IsActive").change(function (e) {
            var rowIndex = $(this).closest("tr").data("rowindex");

            //Apply data change attribute
            $(".chkIsActive-" + rowIndex).not(".dependent").each(function () {
                if ($(this).closest("tr").data("changed") == null) {
                    $(this).closest("tr").attr("data-changed", true);
                }
            });

            if ($(this).is(":checked")) {
                $(".chkIsActive-" + rowIndex).not(".dependent").prop("checked", true);
                $(".chkAllowWrite-" + rowIndex).not(".dependent").removeAttr("disabled");
                $(".chkAllowDelete-" + rowIndex).not(".dependent").removeAttr("disabled");
            }
            else {
                $(".chkIsActive-" + rowIndex).not(".dependent").prop("checked", false);
                $(".chkAllowWrite-" + rowIndex).not(".dependent").prop("checked", false).attr("disabled", "disabled");
                $(".chkAllowDelete-" + rowIndex).not(".dependent").prop("checked", false).attr("disabled", "disabled");
            }
        });

        $("tr.Level2-TableTree").find("input.IsActive").change(function (e) {
            var rowIndex = $(this).closest("tr").data("rowindex");

            //Apply data change attribute
            $(".chkIsActive-parent-" + rowIndex).each(function () {
                if ($(this).closest("tr").data("changed") == null) {
                    $(this).closest("tr").attr("data-changed", true);
                }
            });

            if ($(this).is(":checked")) {
                $(".chkIsActive-parent-" + rowIndex).prop("checked", true);
                $(".chkAllowWrite-parent-" + rowIndex).removeAttr("disabled");
                $(".chkAllowDelete-parent-" + rowIndex).removeAttr("disabled");

                var parentIndex = $(this).closest("tr").data("parentindex");

                //Apply data change attribute
                if ($(".chkIsActive-" + parentIndex).eq(0).closest("tr").data("changed") == null) {
                    $(".chkIsActive-" + parentIndex).eq(0).closest("tr").attr("data-changed", true);
                }

                // check parent checkbox
                $(".chkIsActive-" + parentIndex).eq(0).prop("checked", true);
                $(".chkAllowWrite-" + parentIndex).eq(0).removeAttr("disabled");
                $(".chkAllowDelete-" + parentIndex).eq(0).removeAttr("disabled");

            }
            else {
                $(".chkIsActive-parent-" + rowIndex).prop("checked", false);
                $(".chkAllowWrite-parent-" + rowIndex).prop("checked", false).attr("disabled", "disabled");
                $(".chkAllowDelete-parent-" + rowIndex).prop("checked", false).attr("disabled", "disabled");
            }
        });

        $("tr.Level3-TableTree").find("input.IsActive").change(function (e) {
            var currentRow = $(this).closest("tr");
            var rowIndex = currentRow.data("rowindex");
            var parentIndex = currentRow.data("parentindex");
            var parentIndex2 = currentRow.data("parentindex2");

            $(".chkIsActive-" + parentIndex).eq(0).prop("checked", true);
            $(".chkIsActive-parent-" + parentIndex2).eq(0).prop("checked", true);

            $(currentRow).find('input.AllowWrite').removeAttr("disabled");
            $(".chkAllowWrite-" + parentIndex).eq(0).removeAttr("disabled");
            $(".chkAllowWrite-parent-" + parentIndex2).eq(0).removeAttr("disabled");

            $(currentRow).find('input.AllowDelete').removeAttr("disabled");
            $(".chkAllowDelete-" + parentIndex).eq(0).removeAttr("disabled");
            $(".chkAllowDelete-parent-" + parentIndex2).eq(0).removeAttr("disabled");

            //Apply data change attribute
            if ($(currentRow).data("changed") == null) {
                $(currentRow).attr("data-changed", true);
            }
            if ($(".chkIsActive-" + parentIndex).eq(0).closest("tr").data("changed") == null) {
                $(".chkIsActive-" + parentIndex).eq(0).closest("tr").attr("data-changed", true);
            }
            if ($(".chkIsActive-parent-" + parentIndex2).eq(0).closest("tr").data("changed") == null) {
                $(".chkIsActive-parent-" + parentIndex2).eq(0).closest("tr").attr("data-changed", true);
            }


        });
        // End IsActive Checkbox 


        // AllowWrite Checkbox 
        $("tr.Level1-TableTree").find("input.AllowWrite").change(function (e) {
            var rowIndex = $(this).closest("tr").data("rowindex");

            //Apply data change attribute
            $(".chkAllowWrite-" + rowIndex).not(":disabled").each(function () {
                if ($(this).closest("tr").data("changed") == null) {
                    $(this).closest("tr").attr("data-changed", true);
                }
            });

            if ($(this).is(":checked")) {
                $(".chkAllowWrite-" + rowIndex).not(":disabled").prop("checked", true);
            }
            else {
                $(".chkAllowWrite-" + rowIndex).not(":disabled").prop("checked", false);
            }
        });

        $("tr.Level2-TableTree").find("input.AllowWrite").change(function (e) {
            var rowIndex = $(this).closest("tr").data("rowindex");

            //Apply data change attribute
            $(".chkAllowWrite-parent-" + rowIndex).not(":disabled").each(function () {
                if ($(this).closest("tr").data("changed") == null) {
                    $(this).closest("tr").attr("data-changed", true);
                }
            });

            if ($(this).is(":checked")) {
                $(".chkAllowWrite-parent-" + rowIndex).not(":disabled").prop("checked", true);

                //Apply data change attribute
                if ($(".chkAllowWrite-" + parentIndex).eq(0).closest("tr").data("changed") == null) {
                    $(".chkAllowWrite-" + parentIndex).eq(0).closest("tr").attr("data-changed", true);
                }

                // check parent checkbox
                var parentIndex = $(this).closest("tr").data("parentindex");
                $(".chkAllowWrite-" + parentIndex).eq(0).prop("checked", true);

            }
            else {
                $(".chkAllowWrite-parent-" + rowIndex).not(":disabled").prop("checked", false);
            }
        });

        $("tr.Level3-TableTree").find("input.AllowWrite").change(function (e) {
            var currentRow = $(this).closest("tr");
            var parentIndex = currentRow.data("parentindex");
            var parentIndex2 = currentRow.data("parentindex2");

            $(".chkAllowWrite-" + parentIndex).eq(0).prop("checked", true);
            $(".chkAllowWrite-parent-" + parentIndex2).eq(0).prop("checked", true);

            //Apply data change attribute
            if ($(currentRow).data("changed") == null) {
                $(currentRow).attr("data-changed", true);
            }
            if ($(".chkAllowWrite-" + parentIndex).eq(0).closest("tr").data("changed") == null) {
                $(".chkAllowWrite-" + parentIndex).eq(0).closest("tr").attr("data-changed", true);
            }
            if ($(".chkAllowWrite-parent-" + parentIndex2).eq(0).closest("tr").data("changed") == null) {
                $(".chkAllowWrite-parent-" + parentIndex2).eq(0).closest("tr").attr("data-changed", true);
            }

        });
        // End AllowWrite Checkbox


        // AllowDelete Checkbox
        $("tr.Level1-TableTree").find("input.AllowDelete").change(function (e) {
            var rowIndex = $(this).closest("tr").data("rowindex");
            if ($(this).is(":checked")) {
                $(".chkAllowDelete-" + rowIndex).not(":disabled").prop("checked", true);
            }
            else {
                $(".chkAllowDelete-" + rowIndex).not(":disabled").prop("checked", false);
            }
        });

        $("tr.Level2-TableTree").find("input.AllowDelete").change(function (e) {
            var rowIndex = $(this).closest("tr").data("rowindex");
            if ($(this).is(":checked")) {
                $(".chkAllowDelete-parent-" + rowIndex).not(":disabled").prop("checked", true);

                // check parent checkbox
                var parentIndex = $(this).closest("tr").data("parentindex");
                $(".chkAllowDelete-" + parentIndex).eq(0).prop("checked", true);

            }
            else {
                $(".chkAllowDelete-parent-" + rowIndex).not(":disabled").prop("checked", false);
            }
        });

        $("tr.Level3-TableTree").find("input.AllowDelete").change(function (e) {
            var currentRow = $(this).closest("tr");
            var parentIndex = currentRow.data("parentindex");
            var parentIndex2 = currentRow.data("parentindex2");

            $(".chkAllowDelete-" + parentIndex).eq(0).prop("checked", true);
            $(".chkAllowDelete-parent-" + parentIndex2).eq(0).prop("checked", true);

        });
        // End AllowDelete Checkbox

    }



    function ShowAlert(IsSuccess) {
        if (IsSuccess) {
            $('#errorDiv').html('<div class="row FadeOut mb-sm"><div class="col-lg-12"><div class="alert alert-success mb-sm"><p class="m-none text-semibold h6">Success : Record has been Updated Successfully.</p></div></div></div>');
        } else {
            $('#errorDiv').html('<div class="row FadeOut mb-sm"><div class="col-lg-12"><div class="alert alert-danger mb-sm"><p class="m-none text-semibold h6">Error: Some error occurred.</p></div></div></div>');
        }
        $(window).scrollTop(0);
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

        }
    };
}());
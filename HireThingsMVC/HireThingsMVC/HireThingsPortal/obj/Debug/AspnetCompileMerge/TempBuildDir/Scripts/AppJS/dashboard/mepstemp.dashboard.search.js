/*** 
* Used for defining the MEPSTEMP  UI
* @module UI
* @namespace MEPSTEMP.temp
*/

MEPSTEMP.namespace("UI.Dashboard.Search");
MEPSTEMP.UI.Dashboard.Search = (function () {
    "use strict";
    var _isInitialized = false;

    var $ddlSearch, $txtSearch, $ddlDeviceGroup, $ddlSystem, $ddlGroup, $DivDashboardErrorGrid, $DivTempReadingGrid;

    function initialiseControls() {

        if (_isInitialized == false) {
            _isInitialized = true;
            CacheEl();
            BindEvents();            
            DashboardErrorNotifications();
            DashboardErrorInProgressNotification();
            DashboardTempReadingNotification();

            
        }
    }

    function CheckandRemoveAlreadyExist(data,attr,parent) {
        var flag = 0;
        $(parent).children().find('a['+attr+'="' + data + '"]').each(function () {
            flag += 1;
            $(this).closest('tr').remove();
        });
        console.log(flag);
        return flag;
    }
    //Add notifications to the dashboardErrorGrid
    function DashboardErrorNotifications() {
        // Reference the auto-generated proxy for the hub.

        var notification = $.connection.dashboardErrorHub;
        
        //Client side method which will be invoked from the Global.asax.cs file.
        notification.client.addLatestNotification = function (data) {
            $('#tblDashErrorGrid').parent().find('.RecordNotFoundlLable').remove();

            //Check if error already exists
            if (!CheckandRemoveAlreadyExist(data.DashBoardErrorId, 'data-dasherrorid', '#tblDashErrorGrid')) {                       
                $('#dashErrorCount').html(parseInt($('#dashErrorCount').html()) + 1);             
            }
            var newHtml = getDashErrorRowHml(data);
            $('#tblDashErrorGrid tr:first').after(newHtml);
          //  $('#tblDashErrorGrid tr:nth(1)').effect("pulsate", { color: 'blue', times: 5 }, 4000);
            
            //$(newHtml).toggle("pulsate")
            BindGridEvents();

           
            
        }

        notification.client.sayhello = function (data) {
            console.log(data);
        }
        //// Start the connection.
        $.connection.hub.start().done(function () {            
        });
    }
 
    //Generate Row html for dashboardErrorGrid
    function getDashErrorRowHml(data) {
        var htmlCreated = '';
        //html For a single Row
        htmlCreated = '<tr class="newlycreated ';
        if (data.SuppressStatus == true) {
            htmlCreated += '  warning';
        }
        htmlCreated += '"><td>' + data.OrganizationName + '</td><td>' + data.DeviceName + '</td><td>' + data.CurrentTemp + ' <sup>o</sup>C</td><td>&nbsp;&nbsp;<i class="';
        if (data.ErrorCode == 5)
            htmlCreated += '  fa fa-long-arrow-up text-danger';
        else if (data.ErrorCode == 6)
            htmlCreated += '  fa fa-long-arrow-down text-danger';
        else
            htmlCreated += '  fa fa-ban text-danger';

        htmlCreated += '">&nbsp;&nbsp;</i>' + data.ErrorReason + '</td><td>' + data.ErrorDateTZ + '</td><td class="actions">' +
        '<a data-original-title="Action Taken" data-placement="top" data-errordate="' + data.ErrorDate + '" data-dasherrorid="' + data.DashBoardErrorId + '" data-table="1" class="toggleActionTakenModal Pointer">' +
        '<i class="fa fa-check-square-o" data-original-title="Action Taken" data-placement="top" data-toggle="tooltip"></i></a>' +
        '<a data-original-title="Action Taken" data-placement="top" data-dasherrorid="' + data.DashBoardErrorId + '" data-table="1" class="toggleSuppressModal Pointer';
        if (data.SuppressStatus == true)
            htmlCreated += ' text-danger';

        htmlCreated += '"><i class="fa ';
        if (data.SuppressStatus == true)
            htmlCreated += ' fa-eye';
        else
            htmlCreated += ' fa-eye-slash';

        htmlCreated += '" data-original-title="';
        if (data.SuppressStatus == true)
            htmlCreated += ' Un-Suppress';
        else
            htmlCreated += ' Suppress';
        htmlCreated += '" data-placement="top" data-toggle="tooltip"></i></a></td></tr>';

        return htmlCreated;
    }

    //Add notifications to the dashboardErrorInProgGrid
    function DashboardErrorInProgressNotification() {
        // Reference the auto-generated proxy for the hub.
       
        var notificationInProg = $.connection.dashboardErrorInProgHub;
        //Client side method which will be invoked from the Global.asax.cs file.
        notificationInProg.client.addLatestInProgressNotification = function (data) {
           
            //If corr action in progress grid is visible 
            if ($('#tblDashErrorInProgGrid').length) {

                if (!CheckandRemoveAlreadyExist(data.DashBoardErrorId, 'data-dasherrorid', '#tblDashErrorInProgGrid')) {
                    $('#dashErrorInProgCount').html(parseInt($('#dashErrorInProgCount').html()) + 1);
                }
               
                var newHtml = getDashErrorRowHml(data);
                $('#tblDashErrorInProgGrid tr:first').after(newHtml);
               // BindGridEvents();
               // $(newHtml).effect("pulsate", { color: '#4BADF5' }, 4000);
                //$(newHtml).toggle("pulsate")

            } else {
                
                // Generate Partial View
                
                //Refresh First grid
                MEPSTEMP.Global.MakeSimpleAjaxCall("POST", 'Dashboard/FilterDashBoardErrorCorrActionSearch', {}, function (result) {
                 
                        $('#DivDashboardErrorGridCorrAction').html(result);
                 
                 
                }, function (xhr, ajaxOptions, thrownError) {
                    //debugger;
                    console.log('an error occurred in making ajax call');
                });


               // $('#tblDashErrorInProgGrid tr:first').effect("pulsate", { color: '#4BADF5' }, 4000);

                $('#tblDashErrorInProgGrid tr:first').toggle("pulsate")
            }
            //Refresh First grid
            MEPSTEMP.Global.MakeSimpleAjaxCall("POST", 'Dashboard/FilterDashBoardSearch', {}, function (result) {              
                    $('#DivDashboardErrorGrid').html(result);
                    BindGridEvents();              
            }, function (xhr, ajaxOptions, thrownError) {
                //debugger;
                console.log('an error occurred in making ajax call');
            });


        }
        //// Start the connection.
        $.connection.hub.start().done(function () {
        });
    }

    //Generate Row html for dashboardErrorInProgressGrid
    function getDashErrorInProgRowHml(data) {
        var htmlCreated = '';
        //html For a single Row
        htmlCreated = '<tr class="newlycreated ';
        if (data.SuppressStatus == true) {
            htmlCreated += '  warning';
        }
        htmlCreated += '"><td>' + data.OrganizationName + '</td><td>' + data.DeviceName + '</td><td>' + data.CurrentTemp + ' <sup>o</sup>C</td><td>&nbsp;&nbsp;<i class="';
        if (data.ErrorCode == 5)
            htmlCreated += '  fa fa-long-arrow-up text-danger';
        else if (data.ErrorCode == 6)
            htmlCreated += '  fa fa-long-arrow-down text-danger';
        else
            htmlCreated += '  fa fa-ban text-danger';

        htmlCreated += '">&nbsp;&nbsp;</i>' + data.ErrorReason + '</td><td>' + data.ErrorDateTZ + '</td><td class="actions">' +
        '<a data-original-title="Action Taken" data-placement="top" data-errordate="' + data.ErrorDate + '" data-dasherrorid="' + data.DashBoardErrorId + '" data-table="2" class="toggleActionTakenModal Pointer">' +
        '<i class="fa fa-check-square-o" data-original-title="Action Taken" data-placement="top" data-toggle="tooltip"></i></a>' +
        '<a data-original-title="Action Taken" data-placement="top" data-dasherrorid="' + data.DashBoardErrorId + '" data-table="2" class="toggleSuppressModal Pointer';
        if (data.SuppressStatus == true)
            htmlCreated += ' text-danger';

        htmlCreated += '"><i class="fa ';
        if (data.SuppressStatus == true)
            htmlCreated += ' fa-eye';
        else
            htmlCreated += ' fa-eye-slash';

        htmlCreated += '" data-original-title="';
        if (data.SuppressStatus == true)
            htmlCreated += ' Un-Suppress';
        else
            htmlCreated += ' Suppress';
        htmlCreated += '" data-placement="top" data-toggle="tooltip"></i></a></td></tr>';

        return htmlCreated;
    }

    function DashboardTempReadingNotification() {
        // Reference the auto-generated proxy for the hub.
        var notification = $.connection.dashboardDeviceStatusHub;
        //Client side method which will be invoked from the Global.asax.cs file.
        notification.client.addLatestTempReading = function (data) {
            
            $('#tblDashTempReadingBody').parent().find('.RecordNotFoundlLable').remove();

            //Check if error already exists
            if (!CheckandRemoveAlreadyExist(data.DeviceName, 'data-devicename', '#tblDashTempReadingBody')) {
                $('#dashTempReadingCount').html(parseInt($('#dashTempReadingCount').html()) + 1);
            }

            var newHtml = getDashTempReadingRowHml(data);
            $('#tblDashTempReadingGrid tr:first').after(newHtml);



        }
        //// Start the connection.
        $.connection.hub.start().done(function () {
        });
        
    }

    //Generate Row html for dashboardErrorGrid
    function getDashTempReadingRowHml(data) {
        var htmlCreated = '';
        //html For a single Row
        htmlCreated = '<tr class="newlycreated"><td>' + data.OrganizationName + '</td><td>' + data.DeviceName + ' <a style="display:none" data-devicename="' + data.DeviceName + '"></a></td><td>' + data.ReadingDate + '</td><td>' + data.TempReading + ' <sup>o</sup>C</td></tr>';

        return htmlCreated;
    }
    //Element caching done in here
    function CacheEl() {
        $DivDashboardErrorGrid = $('#DivDashboardErrorGrid');
        $DivTempReadingGrid = $('#DivTempReadingGrid');
    }
    //Event binding done in here
    function BindEvents() {
       
        BindGridEvents();
    }
    function BindGridEvents(type) {
        
            var url = 'Dashboard/DashCorrectiveActionTakenPartial';
            $('.toggleActionTakenModal').each(function () {
                $(this).unbind('click');
                $(this).click(function () {
                    if ($(this).data('dasherrorid') > -1) {
                        
                        var data = {};
                        data.DashAlertId = $(this).data('dasherrorid');
                        data.errorDate = $(this).data('errordate');
                        var table = $(this).data('table');                        
                        if (table == 2)
                            MEPSTEMP.Global.MakeSimpleAjaxCall("GET", url, data, function (result) {                        
                            $('#hdnCorrActionTakenPartial').html(result);
                            $.validator.unobtrusive.parse('#DashCorrActionTakenForm');
                            $('.datetimepicker').datetimepicker({
                                minDate: data.errorDate,
                                maxDate: new Date(),
                                useCurrent:false
                            });
                           
                            BindCorrectiveActionTakenPartailView(table);
                        
                        }, function (xhr, ajaxOptions, thrownError) {
                            //debugger;
                                console.log('an error occurred in making ajax call');
                        });
                        else if(table==1)
                            MEPSTEMP.Global.MakeSimpleAjaxCall("GET", 'Dashboard/DashCorrectiveActionBeingTakenPartial', data, function (result) {
                                $('#hdnCorrActionTakenPartial').html(result);
                            //    $.validator.unobtrusive.parse('#DashCorrActionTakenForm');
                                $('.datetimepicker').datetimepicker({
                                    minDate: data.errorDate,
                                    maxDate: new Date()
                                });

                                BindCorrectiveActionInProgressPartailView();

                            }, function (xhr, ajaxOptions, thrownError) {
                                //debugger;
                                console.log('an error occurred in making ajax call');
                            });
                    }
                });
           
            });
            var url2 = 'Dashboard/DashboardErrorSuppressPartial';
            $('.toggleSuppressModal').each(function () {
                $(this).unbind('click');
                $(this).click(function () {
                    if ($(this).data('dasherrorid') > -1) {
                        var data = {};
                        data.DashErrorId = $(this).data('dasherrorid');
                        var table = $(this).data('table');
                        MEPSTEMP.Global.MakeSimpleAjaxCall("GET", url2, data, function (result) {
                            $('#hdnDashboardErrorPartial').html(result);
                            $.validator.unobtrusive.parse('#DashboardErrorForm');                           
                            BindSuppressPartialView(table);
                     
                        }, function (xhr, ajaxOptions, thrownError) {
                            //debugger;
                            console.log('an error occurred in making ajax call');
                        });
                    }
                });

            });
       
    }
    function GenerateValidationMessage(name,eType) {
    
        var elIdent = '';
       
        if(eType==undefined){
            if ($.trim(name.toLowerCase()) == 'ddlcorractionlist') {
                elIdent = 'Corrective Action List is ';
            }
            else if ($.trim(name.toLowerCase()) == 'correctiveactiondate') {
                elIdent = 'Corrective Action Date is ';
            }
            else if ($.trim(name.toLowerCase()) == 'correctiveactionnotes') {
                elIdent = 'Notes are ';
            }
            else if ($.trim(name.toLowerCase()) == 'suppressreason') {
                elIdent = 'Reason is ';
            }
            else if ($.trim(name.toLowerCase()) == 'ddlsuppressinterval') {
                elIdent = 'Duration is ';
            }
            else if ($.trim(name.toLowerCase()) == 'customsuppressinterval') {
                elIdent = 'Duration is ';
            }
            else if ($.trim(name.toLowerCase()) == 'corractioninprogressnotes') {
                elIdent = 'Notes are ';
            }
            
        }
        else if ($.trim(eType.toLowerCase()) == 'customnumeric') {
            elIdent = 'Duration must be ';
        }


        if (eType == undefined) {
            return '<span data-valmsg-replace="true" data-valmsg-for="' + name + '" class="text-danger field-validation-error"><span for="" class="">' + elIdent + 'required.</span></span>';
        }        
        else if ($.trim(eType.toLowerCase()) == 'customnumeric') {
            
            return '<span data-valmsg-replace="true" data-valmsg-for="' + name + '" class="text-danger field-validation-error"><span for="" class="">' + elIdent + 'numeric.</span></span>';
        }
        
        
    }
    function BindCorrectiveActionInProgressPartailView() {
        $('#corrActBeingTakenModal').modal('toggle');
        $('#btnCorrActionInProgressSave').click(function (e) {
            e.preventDefault();

            $(this).closest('form').find('.requiredEl').each(function () {
                $(this).parent().children('.text-danger').remove();
                $(this).parent().parent().children('.text-danger').remove();
                if ($(this).hasClass('requiredDdl')) {
                    if ($(this).val() <= 0) {
                        $(this).parent().append(GenerateValidationMessage($(this).attr('id')));
                    }
                } else {
                    if ($(this).val() == null || $(this).val() == undefined || $(this).val() == '') {                        
                            $(this).parent().append(GenerateValidationMessage($(this).attr('id')));
                    }
                }
            });
            var errorCount = 0;
            $(this).closest('form').find('.text-danger').each(function () {
                errorCount += 1;
            });
            if (errorCount == 0) {
                var model = {};                               
                model.CorrActionInProgressNotes = $('#CorrActionInProgressNotes').val();
                model.DashboardErrorId = $('#DashboardErrorId').val();
                var url = 'Dashboard/DashCorrectiveActionInProgressPartial';
                $('#btnCorrActionInProgressSave').prop('disabled', true);

                MEPSTEMP.Global.MakeSimpleAjaxCall("POST", url, model, function (result) {
                    setTimeout(function () {
                        $('body').removeClass('modal-open');
                        $('#corrActBeingTakenModal').modal('toggle');
                        
                            $('#DivDashboardErrorGrid').html(result);
                            MEPSTEMP.Global.MakeSimpleAjaxCall("POST", 'Dashboard/FilterDashBoardErrorCorrActionSearch', {}, function (result) {                               
                                setTimeout(function () {
                                    $('#DivDashboardErrorGridCorrAction').html(result);
                                    BindGridEvents();
                                }, 0);
                            }, function (xhr, ajaxOptions, thrownError) {
                                //debugger;
                                console.log('an error occurred in making ajax call');
                            });                        

                    }, 0);
                }, function (xhr, ajaxOptions, thrownError) {
                    //debugger;
                    console.log('an error occurred in making ajax call');
                });
            }

        });


    }
    function BindCorrectiveActionTakenPartailView(table) {        
        $('#myModal').modal('toggle');
        $('#btnCorrActTakenSave').click(function (e) {
            e.preventDefault();

            $(this).closest('form').find('.requiredEl').each(function () {
                $(this).parent().children('.text-danger').remove();
                $(this).parent().parent().children('.text-danger').remove();
                if ($(this).hasClass('requiredDdl')) {
                    if ($(this).val() <= 0) {
                        $(this).parent().append(GenerateValidationMessage($(this).attr('id')));
                    }
                } else {
                    if ($(this).val() == null || $(this).val() == undefined || $(this).val() == '') {
                        if ($(this).attr('id') == 'CorrectiveActionDate')
                            $(this).parent().parent().append(GenerateValidationMessage($(this).attr('id')));
                        else
                            $(this).parent().append(GenerateValidationMessage($(this).attr('id')));
                    }
                }                                
            });
            var errorCount = 0;
            $(this).closest('form').find('.text-danger').each(function(){
                errorCount+=1;
            });
            if (errorCount == 0) {
                var model = {};
                model.CorrectiveActionListId = $('#ddlCorrActionList').val();
                model.CorrectiveActionDate = $('#CorrectiveActionDate').val();
                model.CorrectiveActionNotes = $('#CorrectiveActionNotes').val();
                model.DashboardErrorId = $('#DashboardErrorId').val();

                var url = 'Dashboard/DashCorrectiveActionTakenPartial';
                $('#btnCorrActTakenSave').prop('disabled', true);
                MEPSTEMP.Global.MakeSimpleAjaxCall("POST", url, model, function (result) {
                    setTimeout(function () {
                        $('body').removeClass('modal-open');
                        $('#myModal').modal('toggle');                                               
                        $('#DivDashboardErrorGridCorrAction').html(result);
                        BindGridEvents();                        
                        
                    }, 0);                 
                }, function (xhr, ajaxOptions, thrownError) {
                    //debugger;
                    console.log('an error occurred in making ajax call');
                });
            }
           
        });
    }
    function BindSuppressPartialView(table) {
        var arrSuppressInterval = [];
        $('#myModalSuppress').modal('toggle');        
        if ($('#SuppressInterval').val() == 0)
            $('#SuppressInterval').val(-1);
        $('#SuppressStatus').bootstrapSwitch({ labelText: 'Suppress', onText: 'YES', offText: 'NO' });
        $('#ddlSuppressInterval').on('change', function (e) {
           
            if ($(this).val() == 0) {
                $(this).parent().removeClass('col-lg-6').removeClass('col-lg-7').addClass('col-lg-3');
                $('#customSuppressInterval').parent().show();
                $('#customSuppressInterval').addClass('requiredEl');
                MEPSTEMP.Global.KeyLocking('.hasKeyLocking', 'data-keylocking');
            } else {
                if ($(this).parent().hasClass('col-lg-3'))
                    $(this).parent().removeClass('col-lg-3').addClass('col-lg-7');
                $('#customSuppressInterval').parent().hide().children('.text-danger').remove();
                $('#customSuppressInterval').removeClass('requiredEl');
            }
        });
        $('#customSuppressInterval').on('focusout', function () {            
            if (isNaN($('#customSuppressInterval').val())) {
                $(this).parent().children().remove('.text-danger');
                $(this).parent().append(GenerateValidationMessage('customSuppressInterval', 'customnumeric'));
            }
        });
        $('#ddlSuppressInterval option').each(function () {
            arrSuppressInterval.push($(this).val());
        });
        if ($('#SuppressInterval').val() == 0 || $.inArray($('#SuppressInterval').val(), arrSuppressInterval) == -1) { $('#ddlSuppressInterval').val(0).trigger('change'); $('#customSuppressInterval').val($('#SuppressInterval').val()); } else { $('#ddlSuppressInterval').val($('#SuppressInterval').val()); }
        $('#btnSuppressPartialSave').click(function (e) {
            e.preventDefault();

            $(this).closest('form').find('.requiredEl').each(function () {
                $(this).parent().children('.text-danger').remove();
                $(this).parent().parent().children('.text-danger').remove();
                if ($(this).hasClass('requiredDdl')) {
                    if ($(this).val() < 0) {
                        $(this).parent().append(GenerateValidationMessage($(this).attr('id')));
                    }
                } else {
                    if ($(this).val() == null || $(this).val() == undefined || $(this).val() == '') {                       
                            $(this).parent().append(GenerateValidationMessage($(this).attr('id')));
                    }
                }
            });                       
            var errorCount = 0;
            $(this).closest('form').find('.text-danger').each(function () {
                errorCount += 1;
            });
            if (errorCount == 0) {
                var model = {};
                if ($('#ddlSuppressInterval').val() == 0)
                    model.SuppressInterval = $('#customSuppressInterval').val();
                else
                    model.SuppressInterval = $('#ddlSuppressInterval').val();
                if ($('#SuppressStatus').hasClass('hidden'))
                    model.SuppressStatus = true;
                else
                    model.SuppressStatus = $('#SuppressStatus').is(':checked')
                model.DashBoardErrorId = $('#DashBoardErrorId').val();
                model.SuppressReason = $('#SuppressReason').val();
               
                if (table == 1)
                    model.AlertStatusId = 1;
                else
                    model.AlertStatusId = 3;
                //model.DashboardErrorId = $('#DashboardErrorId').val();
                var url = 'Dashboard/DashboardErrorSuppressPartial';
                $('#btnSuppressPartialSave').prop('disabled', true);
                MEPSTEMP.Global.MakeSimpleAjaxCall("POST", url, model, function (result) {
                    
                    setTimeout(function () {
                        $('body').removeClass('modal-open');
                        $('#myModalSuppress').modal('hide');
                        if(table==1)
                            $('#DivDashboardErrorGrid').html(result);
                        else if (table == 2)
                            $('#DivDashboardErrorGridCorrAction').html(result);
                        BindGridEvents();
                    }, 0);
                    
                }, function (xhr, ajaxOptions, thrownError) {
                    //debugger;
                    console.log('an error occurred in making ajax call');
                });
            }


        });
        
    }
    //Clear Fields function
    function ClearFields() { }

    return {
        readyMain: function () {

        },
        initialisePage: function () {
            initialiseControls();
        },
        resetPage: function () {

        }
    };
}());
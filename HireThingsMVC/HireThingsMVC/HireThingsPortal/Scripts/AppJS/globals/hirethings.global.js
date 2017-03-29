/*** 
* Used for defining the HIRETHINGS temp UI
* @module UI
* @namespace HIRETHINGS.temp
*/

HIRETHINGS.namespace("Global");
HIRETHINGS.Global = (function () {
    "use strict";
    var counter = 0;
    //var webapiurlbase = window.ApplicationBasePath + "aapi/";
    var normalurlbase = window.ApplicationBasePath;
    // Since controller is returing viewmodel 
    $.ajaxSetup({
        //contentType: "application/json;charset=utf-8",
        //dataType: "json",
        cache: false
    });
    function GenerateValidationMessage(name) {

        var elIdent = '';
        if ($.trim(name.toLowerCase()) == 'ddllocation') {
            elIdent = 'Location is ';
        }
        else if ($.trim(name.toLowerCase()) == 'ddlcountry') {
            elIdent = 'Country is ';
        }
        else if ($.trim(name.toLowerCase()) == 'ddlparentobject') {
            elIdent = 'Parent Object is ';
        }
        else if ($.trim(name.toLowerCase()) == 'ddlobjectlevel') {
            elIdent = 'Object Level is ';
        }
        else if ($.trim(name.toLowerCase()) == 'ddlsystem') {
            elIdent = 'Organization is ';
        }
        else if ($.trim(name.toLowerCase()) == 'reason') {
            elIdent = 'Delete Reason is ';
        }
        else if ($.trim(name.toLowerCase()) == 'ddldevice') {
            elIdent = 'Device is ';
        }
        return '<span data-valmsg-replace="true" data-valmsg-for="' + name + '" class="text-danger field-validation-error"><span for="" class="">' + elIdent + 'required.</span></span>';
    }

    return {
        baseURL: normalurlbase
        , getParameterByName: function (name) {
            name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
            var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
                results = regex.exec(location.search);
            return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
        },
        MakeAjaxCall: function (httpmethod, URL, data, successCallback, failureCallback, aynch, showProgress) {


            if (typeof aynch == 'undefined')
                aynch = true;

            if (typeof showProgress == 'undefined')
                showProgress = false;

            //if (showProgress) {
            //    counter++;
            //    if (counter > 0) {
            //        $('#divProgressOverlay').show();
            //        $('#divProgressStatus').show();
            //        ShowSpinner();
            //    }
            //}

            var urltocall = normalurlbase + URL;
            var defObj = $.ajax({
                type: httpmethod, //"POST"
                url: urltocall,
                data: data,
                async: aynch,
                //beforeSend: function (jqXHR, settings) {

                //},
                success: function (resp) {

                    try {
                        if ($('#txtSearch').hasClass('hasError')) {

                            $('#txtSearch').parent().find('span').remove();
                            $('#txtSearch').removeClass('hasError');

                        }
                        if (successCallback)
                            successCallback(resp);
                    } catch (err) {
                        console.log('error occurred while making ajax call--' + err);
                    }
                },
                error: function (err, type, httpStatus) {
                    if (err.status == 417) {
                        var obj = {};
                        obj = $.parseJSON(err.responseText);
                        if (obj[0].key == 'SearchText') {
                            $('#txtSearch').addClass('hasError');
                            //  console.log($('#txtSearch').parent().find('.text-danger').addClass('foundu'));
                            // if ($('#txtSearch').parent().children().find('span')===undefined)
                            $('#txtSearch').parent().find('.text-danger').remove();
                            $('#txtSearch').parent().append('<span class="text-danger field-validation-error" data-valmsg-for="txtSearch" data-valmsg-replace="true"><span class="" for="">Special characters are not allowed.</span></span>')
                        }

                    }

                    else {
                        $('#errorDiv').html('<div class="row FadeOut mb-sm"><div class="col-lg-12"><div class="alert alert-danger mb-sm"><p class="m-none text-semibold h6">Error : Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum tristique, justo id maximus malesuada.</p></div></div></div>');
                    }

                    // In case of authentication error -redirect to login


                    //if (err.status == 406) {

                    //    var retPath = "";
                    //    if (window.location.pathname) {
                    //        retPath = "?ReturnURL=" + window.location.pathname;
                    //    }
                    //    alert('an authentication error occurred! This should make application redirect to login !');
                    //    //window.location.href = CFU.Resources.Views.LoginURL + retPath;

                    //    return;
                    //}
                    if (failureCallback)
                        failureCallback(err, type, httpStatus);
                    //else {
                    //    //alert(err.status + " - " + err.responseText + " - " + httpStatus);
                    //}

                    console.log('Error occurred in ajax call' + err.status + " - " + err.responseText + " - " + httpStatus);
                },
                complete: function () {

                    $('.Pointer').tooltip();
                    //if (showProgress) {
                    //    counter--;
                    //    if (counter <= 0) {
                    //        $('#divProgressOverlay').hide();
                    //        $('#divProgressStatus').hide();
                    //        HideSpinner();
                    //    }
                    //}
                }
            });

            return defObj;
        },
        MakeJSONAjaxCall: function (httpmethod, URL, data, successCallback, failureCallback, aynch, showProgress) {


            if (typeof aynch == 'undefined')
                aynch = true;

            if (typeof showProgress == 'undefined')
                showProgress = false;


            var urltocall = normalurlbase + URL;
            var defObj = $.ajax({
                type: httpmethod, //"POST"
                url: urltocall,
                data: data,
                async: aynch,
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                //beforeSend: function (jqXHR, settings) {

                //},
                success: function (resp) {

                    try {
                        if ($('#txtSearch').hasClass('hasError')) {

                            $('#txtSearch').parent().find('span').remove();
                            $('#txtSearch').removeClass('hasError');

                        }
                        if (successCallback)
                            successCallback(resp);
                    } catch (err) {
                        console.log('error occurred while making ajax call--' + err);
                    }
                },
                error: function (err, type, httpStatus) {
                    if (err.status == 417) {
                        var obj = {};
                        obj = $.parseJSON(err.responseText);
                        if (obj[0].key == 'SearchText') {
                            $('#txtSearch').addClass('hasError');
                            $('#txtSearch').parent().find('.text-danger').remove();
                            $('#txtSearch').parent().append('<span class="text-danger field-validation-error" data-valmsg-for="txtSearch" data-valmsg-replace="true"><span class="" for="">Special characters are not allowed.</span></span>')
                        }

                    }

                    else {
                        $('#errorDiv').html('<div class="row FadeOut mb-sm"><div class="col-lg-12"><div class="alert alert-danger mb-sm"><p class="m-none text-semibold h6">Error : Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum tristique, justo id maximus malesuada.</p></div></div></div>');
                    }

                    // In case of authentication error -redirect to login


                    //if (err.status == 406) {

                    //    var retPath = "";
                    //    if (window.location.pathname) {
                    //        retPath = "?ReturnURL=" + window.location.pathname;
                    //    }
                    //    alert('an authentication error occurred! This should make application redirect to login !');
                    //    //window.location.href = CFU.Resources.Views.LoginURL + retPath;

                    //    return;
                    //}
                    if (failureCallback)
                        failureCallback(err, type, httpStatus);


                    console.log('Error occurred in ajax call' + err.status + " - " + err.responseText + " - " + httpStatus);
                },
                complete: function () {
                    $('.Pointer').tooltip();
                }
            });

            return defObj;
        },
        MakeSimpleAjaxCall: function (httpmethod, URL, data, successCallback, failureCallback, aynch, showProgress) {


            if (typeof aynch == 'undefined')
                aynch = true;

            if (typeof showProgress == 'undefined')
                showProgress = false;

            //if (showProgress) {
            //    counter++;
            //    if (counter > 0) {
            //        $('#divProgressOverlay').show();
            //        $('#divProgressStatus').show();
            //        ShowSpinner();
            //    }
            //}

            var urltocall = normalurlbase + URL;
            var defObj = $.ajax({
                type: httpmethod, //"POST"
                url: urltocall,
                data: data,
                async: aynch,
                //beforeSend: function (jqXHR, settings) {

                //},
                success: function (resp) {
                    try {
                        if (successCallback)
                            successCallback(resp);
                    } catch (err) {
                        console.log('error occurred while making ajax call--' + err);
                    }
                },
                error: function (err, type, httpStatus) {
                    if (failureCallback)
                        failureCallback(err, type, httpStatus);


                    console.log('Error occurred in ajax call' + err.status + " - " + err.responseText + " - " + httpStatus);
                },
                complete: function () {
                    $('.Pointer').tooltip();

                }
            });

            return defObj;
        },
        LoadPagination: function (element, totalRows, pSize, cPage, pStart, pEnd) {


            var flagFirst = true, flagLast = false;
            var i;
            var totalPages;
            var s = 0;
            if (totalRows / pSize < 1) {
                s = 1;
                totalPages = 1;
            }
            else if (totalRows % pSize > 0) {
                s = 2;
                totalPages = parseInt(totalRows / pSize) + 1;
            }

            else
                totalPages = parseInt(totalRows / pSize);
            if (pStart === undefined) {
                pStart = 1;
            }
            if (pEnd === undefined) {
                if (totalPages > 3)
                    pEnd = 3;
                else
                    pEnd = totalPages;
            }
            if (cPage == undefined || cPage == '') {
                cPage = 1;
            }
            if (cPage == pEnd) {
                if (cPage > 2) {
                    pStart = parseInt(cPage) - 1;
                    pEnd = parseInt(pEnd) + 1;
                    if (pEnd > totalPages) {

                        pEnd = totalPages;
                    }
                }
            }

            else if (cPage > pEnd) {
                pStart = parseInt(cPage) - 1;
                pEnd = parseInt(cPage) + 1;
                if (pEnd > totalPages) {
                    pEnd = totalPages;
                }
            }
            if (cPage == pStart) {
                if (cPage > 3) {
                    pStart = cPage - 1;
                    pEnd -= 1;
                    if (pEnd > totalPages) {
                        pEnd = totalPages;
                    }
                }
            }
            if (pEnd == totalPages) { flagLast = true; } else { flagLast = false; }
            if (pStart == 1) { flagFirst = true; } else { flagFirst = false; }
            var htmlToAdd = '<li id="spagel-left-li"><a data-target="left" data-prev="" id="spagel-left" class="Pointer pagel"> <i class="fa fa-angle-left"></i> <i class="fa fa-angle-left"></i> </a></li><li id="pagel-left-li"><a data-target="left" data-prev="" id="pagel-left" class="Pointer pagel"> <i class="fa fa-angle-left"></i> </a></li>';
            var storeStart = pStart;
            for (pStart; pStart < pEnd + 1; pStart++) {

                htmlToAdd += '<li id="pagel-el-' + pStart + '"><a data-target="' + pStart + '"  class="Pointer pagel pagel-el">' + pStart + '</a></li>';
            }

            htmlToAdd += '<li id="pagel-right-li"><a data-target="right" data-next="" id="pagel-right" class="Pointer pagel">  <i class="fa fa-angle-right"></i></a></li><li id="pagel-right-li"><a data-target="right" data-next="" id="pagel-right" class="Pointer pagel">  <i class="fa fa-angle-right"></i><i class="fa fa-angle-right"></i></a></li>';

            //   htmlToAdd += '<div class="well pull-right"> showing page(s): ' + parseInt(storeStart) + ' to  ' + pEnd + ' of ' + totalPages + '</div>';
            $(element).html(htmlToAdd);

            //attach events for pagination
            if (flagFirst) {
                $('#pagel-left-li').attr('disabled', 'true')
                   .addClass('disabled');
            }
            else { $('#pagel-left-li').removeAttr('disabled').removeClass('disabled'); }
            if (flagLast) {
                $('#pagel-right-li').attr('disabled', 'true')
                                    .addClass('disabled');
            }
            else { $('#pagel-right-li').removeAttr('disabled').removeClass('disabled'); }

            $('.pagel').each(function () {

                var $this = $(this);
                $this.unbind('click');
                $this.click(function () {

                    if ($this.data('target') > 0) {

                        if (HIRETHINGS.Global.getParameterByName('p') != '') {
                            var loc = window.location.href.substring(0, window.location.href.indexOf('?'));
                            window.location.href = loc + '?p=' + $this.data('target');
                        }
                        else {
                            window.location.href = window.location.href + '?p=' + $this.data('target');
                        }
                    }
                    else {
                        var p = parseInt(HIRETHINGS.Global.getParameterByName('p'));

                        if ($this.data('target') == 'left') {
                            p = p - 1;


                            var loc = window.location.href.substring(0, window.location.href.indexOf('?'));
                            window.location.href = loc + '?p=' + p;
                        }
                        else if ($this.data('target') == 'right') {
                            //if (totalPages >= pEnd + 5) {
                            //    p = pEnd;
                            //}
                            if (p == 'undefined' || isNaN(p)) {
                                p = 2;
                            } else {
                                p = p + 1;
                            }

                            var loc = window.location.href.substring(0, window.location.href.indexOf('?'));

                            window.location.href = loc + '?p=' + p;
                        }
                    }

                });

            });
            $('.pagel-el').each(function () {
                //  $(this).closest('li').removeClass('disabled');
                $(this).closest('li').removeClass('active');
            });
            //$('#pagel-el-' + cPage).addClass('disabled');
            $('#pagel-el-' + cPage).addClass('active');
        },
        LoadAJAXPagination: function (element, totalRows, ddlPageSize, hdnPageSize, cPage, pStart, pEnd, data, URL, sucessCallback, failureCallback) {
            if (data === null) {
                data = {};
            }
            var pSize, $hdnCPage = cPage, $hdnTotalRecords = totalRows;
            cPage = cPage.val();
            totalRows = totalRows.val();
            var flagFirst = true, flagLast = false;
            var i;
            var totalPages;
            pSize = ddlPageSize.val();

            if (pSize === '' || pSize === undefined || !isNaN(pSize)) {
                if (hdnPageSize.val() === '') {
                    pSize = 10;
                    hdnPageSize.val('10');
                }
                else {
                    pSize = hdnPageSize.val();
                }
            }
            if (!isNaN(hdnPageSize.val())) {

                //console.log('hiddenpage size : ' + hdnPageSize.val());
                if (hdnPageSize.val() > 0) {

                    ddlPageSize.val(hdnPageSize.val());
                } else {

                    ddlPageSize.val(0);
                }
            }
            //set hdnPageSize to current page size
            hdnPageSize.val(pSize);
            // register the onchange event of $(ddlPageSize)
            ddlPageSize.change(function (e) {
                if ($(this).val() == hdnPageSize.val()) {
                    return false;
                }
                var currSelPSize = $(this).val();
                if (isNaN(currSelPSize)) {
                    currSelPSize = 0;
                }
                hdnPageSize.val(currSelPSize);
                pSize = currSelPSize;
                data.CPage = 1;
                data.pSize = pSize;
                HIRETHINGS.Global.MakeAjaxCall('POST', URL, data, function (result) {
                    if (sucessCallback) {
                        sucessCallback(result);
                        if (pSize > 0)
                            ddlPageSize.val(hdnPageSize.val());
                        else
                            ddlPageSize.val(0);
                    }
                }, function (xhr, ajaxOptions, thrownError) {
                    if (failureCallback)
                        failureCallback(xhr, ajaxOptions, thrownError);
                }, false, false);

            });
            if (totalRows / pSize < 1) {
                totalPages = 1;
            }
            else if (totalRows % pSize > 0) {
                totalPages = parseInt(totalRows / pSize) + 1;
            }
            else
                totalPages = parseInt(totalRows / pSize);

            if (pStart === undefined || pStart == '') {
                pStart = 1;
            }

            if (pEnd === undefined || pEnd == '') {
                if (totalPages > 3)
                    pEnd = 3;
                else
                    pEnd = totalPages;
            }

            if (cPage == undefined || cPage == '') {
                cPage = 1;
            }
            if (totalPages == 1) {
                pStart = 1;
                pEnd = 1;
            }

            else if (cPage == totalPages && cPage > 2) {
                pStart = totalPages - 2;
                pEnd = totalPages;
            }
            else if (cPage == pEnd) {
                if (cPage > 2) {
                    pStart = parseInt(cPage) - 1;
                    pEnd = parseInt(pEnd) + 1;
                    if (pEnd > totalPages) {

                        pEnd = totalPages;
                    }
                }
            }
            else if (cPage > pEnd) {
                pStart = parseInt(cPage) - 1;
                pEnd = parseInt(cPage) + 1;
                if (pEnd > totalPages) {
                    pEnd = totalPages;
                }
            }
            if (cPage == pStart) {
                if (cPage > 3) {
                    pStart = cPage - 1;
                    pEnd -= 1;
                    if (pEnd > totalPages) {
                        pEnd = totalPages;
                    }
                }
            }
            if (pEnd == totalPages) { flagLast = true; } else { flagLast = false; }
            if (pStart == 1) { flagFirst = true; } else { flagFirst = false; }
            var elementasId = element.replace('#', '');
            var htmlToAdd = '<li id="spagel-left-li' + elementasId + '"><a data-target="left" data-prev="" id="spagel-left' + elementasId + '" class="Pointer pagel"> <i class="fa fa-angle-left"></i> <i class="fa fa-angle-left"></i> </a></li><li id="pagel-left-li' + elementasId + '"><a data-target="left" data-prev="" id="pagel-left' + elementasId + '" class="Pointer pagel"> <i class="fa fa-angle-left"></i> </a></li>';
            var storeStart = pStart;
            for (pStart; pStart < pEnd + 1; pStart++) {

                htmlToAdd += '<li id="pagel-el-' + pStart + elementasId + '"><a data-target="' + pStart + '"  class="Pointer pagel pagel-el">' + pStart + '</a></li>';
            }
            htmlToAdd += '<li id="pagel-right-li' + elementasId + '"><a data-target="right" data-next="" id="pagel-right' + elementasId + '" class="Pointer pagel">  <i class="fa fa-angle-right"></i></a></li><li id="spagel-right-li' + elementasId + '"><a data-target="right" data-next="" id="spagel-right' + elementasId + '" class="Pointer pagel">  <i class="fa fa-angle-right"></i><i class="fa fa-angle-right"></i></a></li>';

            //   htmlToAdd += '<div class="well pull-right"> showing page(s): ' + parseInt(storeStart) + ' to  ' + pEnd + ' of ' + totalPages + '</div>';
            if (pSize > 0)
                $(element).html(htmlToAdd);

            //attach events for pagination
            if (flagFirst) {
                if (cPage == 1) {

                    $('#pagel-left-li' + elementasId).attr('disabled', 'true')
                   .addClass('disabled');
                    $('#spagel-left-li' + elementasId).attr('disabled', 'true')
                  .addClass('disabled');
                }
            }
            else {
                if (cPage > 1) {
                    $('#pagel-left-li' + elementasId).removeAttr('disabled')
                        .removeClass('disabled');
                    $('#spagel-left-li' + elementasId).removeAttr('disabled')
                        .removeClass('disabled');
                }

            }
            if (flagLast) {
                if (cPage == totalPages) {
                    $('#pagel-right-li' + elementasId).attr('disabled', 'true').addClass('disabled');
                    $('#spagel-right-li' + elementasId).attr('disabled', 'true').addClass('disabled');
                }

            }
            else {
                if (cPage < totalPages) {
                    $('#pagel-right-li' + elementasId).removeAttr('disabled').removeClass('disabled');
                    $('#spagel-right-li' + elementasId).removeAttr('disabled').removeClass('disabled');
                }
            }

            $('.pagel').each(function () {
                var $this = $(this);
                $this.unbind('click');
                if (!$this.parent('li').hasClass('disabled')) {
                    $this.click(function () {
                        var p = parseInt($hdnCPage.val());
                        if ($this.attr('id') == 'spagel-right' + element) {
                            //i.e. take it to last page

                            p = totalPages;
                            $hdnCPage.val(totalPages);
                        }
                        else if ($this.attr('id') == 'spagel-left' + element) {
                            // i.e. take it to first page
                            p = 1;
                            $hdnCPage.val(1);
                        }
                        else if ($this.data('target') > 0) {
                            p = parseInt($this.data('target'));
                            $hdnCPage.val($this.data('target'));
                        }
                        else {
                            if ($this.data('target') == 'left') {
                                p = p - 1;
                            }
                            else if ($this.data('target') == 'right') {
                                if (p == 'undefined' || isNaN(p)) {
                                    p = 2;
                                } else {
                                    p = p + 1;
                                }
                            }
                        }
                        if (p <= totalPages)
                            $hdnCPage.val(p);
                        if ($hdnCPage.val() <= totalPages && $hdnCPage.val() > 0) {
                            // Make ajax call here
                            data.CPage = $hdnCPage.val();
                            data.TPage = totalPages;
                            data.TRows = $hdnTotalRecords.val();
                            data.pSize = pSize;

                            HIRETHINGS.Global.MakeAjaxCall('POST', URL, data, function (result) {
                                if (sucessCallback)
                                    sucessCallback(result);
                                if (pSize > 0)
                                    ddlPageSize.val(hdnPageSize.val());
                                else
                                    ddlPageSize.val(0);

                            }, function (xhr, ajaxOptions, thrownError) {
                                if (failureCallback)
                                    failureCallback(xhr, ajaxOptions, thrownError);
                            }, false, false);
                        }
                    });
                }
            });
            $('.pagel-el').each(function () {
                if ($(this).closest('li').attr('id').indexOf(elementasId) > 0)
                    $(this).closest('li').removeClass('active');
            });
            $('#pagel-el-' + cPage + elementasId).addClass('active');
        },
        BindDropDownOnClick: function (type, url, dropdownFirst, dropdownSecond, successCallback) {
            HIRETHINGS.Global.MakeAjaxCall(type, url, {}, function (result) {
                if (successCallback)
                    successCallback(result);

                // Success call back from ajax
            }, function (xhr, ajaxOptions, thrownError) {
                console.log('an error occurred in making ajax call --  HIRETHINGS.Global.BindDropDownOnClick()');
            });
        },
        ValidateDropdown: function (el) {
            $(el).closest('form').submit(function (e) {

                var val = true;
                $('.ddlWithOnClick').each(function (i, obj) {

                    if ($(this).val() < 0) {
                        val = false;
                        $(this).parent().children('.text-danger').remove();
                        $(this).parent().append(GenerateValidationMessage($(this).attr('id')));
                    }
                    else {
                        $(this).parent().children('.text-danger').remove();
                    }
                });

                if (!val) {
                    e.preventDefault();
                };
            });
        },
        ValidateDropdownExcept: function (el, exceptel) {
            $(el).closest('form').submit(function (e) {

                var val = true;
                $('.ddlWithOnClick').each(function (i, obj) {
                    if ($(this).attr(id) !== exceptel)
                        if ($(this).val() < 0) {
                            val = false;
                            $(this).parent().children('.text-danger').remove();
                            $(this).parent().append(GenerateValidationMessage($(this).attr('id')));
                        }
                        else {
                            $(this).parent().children('.text-danger').remove();
                        }
                });

                if (!val) {
                    e.preventDefault();
                };
            });
        },
        ValidateDropdownOnCondition: function (el, depel, value) {
            $(el).closest('form').submit(function (e) {
                //console.log('submit');
                var val = true;

                $('.ddlWithOnClick').each(function (i, obj) {

                    var $this = $(el);
                    if ($this.val() < 0 && $(depel).val() != value) {
                        //console.log('shouldnt be here : ' + $(depel).val());
                        val = false;
                        $this.parent().children('.text-danger').remove();
                        $this.parent().append(GenerateValidationMessage($this.attr('id')));
                    }
                    else {
                        //console.log('remove 1');
                        $this.parent().children('.text-danger').remove();
                        //console.log('remove 2');
                    }
                });




                if (!val) {
                    //console.log('dont');
                    e.preventDefault();
                }
            });
        },
        ValidateDropdownOnChange: function () {

            $('.ddlWithOnClick').change(function () {
                if ($(this).val() < 0) {
                    console.log('val < 0');
                    $(this).parent().children('.text-danger').remove();
                    $(this).parent().append(GenerateValidationMessage($(this).attr('id')));
                }
                else {
                    console.log('remove');
                    $(this).parent().children('.text-danger').remove();
                }
            });
        },
        KeyLocking: function (className, dataAttr) {
            $(className).each(function () {
                $(this).keydown(function (e) {
                   
                    HIRETHINGS.Utilities.getRegularExp($(this).attr(dataAttr), e, this);
                });


            });
        },
        ApplyMasking: function (control, mask, options) {

            switch ($.trim(mask.toLowerCase())) {
                case 'ipadd':
                    $(control).mask('0ZZ.0ZZ.0ZZ.0ZZ', {
                        translation: {
                            'Z': {
                                pattern: /[0-9]/, optional: true
                            }
                        }
                    });
                    $(control).mask('099.099.099.099');
                    break;
                case 'macadd':
                    $(control).mask('EE:EE:EE:EE:EE:EE', {
                        translation: {
                            'E': {
                                pattern: /[0-9a-fA-F]/, optional: false
                            }
                        }
                    });
                    break;
                case 'usphone':
                    $(control).mask('(000) 000-0000');
                    break;
                default:
                    $(control).mask(mask, options);
                    break;

            }
        },

        ApplyMenuSelection: function (caption) {
            $('.menuanchor').each(function () {
                if ($.trim($(this).data('caption').toLowerCase()) === $.trim(caption.toLowerCase())) {
                    //shrink all expanded navs
                    $('.nav-expanded').removeClass('nav-expanded');
                    //expand current's parent nav
                    $(this).closest('.nav-parent').addClass('nav-expanded');
                    //expand level top nav
                    $(this).closest('.nav-parent').parent('ul').parent('li.nav-parent').addClass('nav-expanded');
                    //add selected class
                    $(this).addClass('ActiveMenu');
                    return true;
                }
            });
        },
        BindDeletionModal: function (functionToExecute) {
            // delete functionality done in here
            $('.btnItemDelete').unbind('click');
            $('.btnItemDelete').each(function () {


                $(this).click(function () {
                    var data = {};
                    data.ItemDeleteId = $(this).data('delitemid');
                    var url = 'ItemDelete/Index';
                    HIRETHINGS.Global.MakeAjaxCall("GET", url, data, function (result) {
                        $('.itemDeletionConfirmPlaceholder').html(result);
                        if ($('#confirm-delete').length > 0)
                            $('#confirm-delete').modal('toggle');

                        // delete button click functionality
                        $('#btnDeleteConfirm').click(function () {
                            $('#Reason').parent().children('.text-danger').remove();
                            if ($('#Reason').val() == '') {
                                $('#Reason').parent().append(GenerateValidationMessage('Reason'));
                                return false;
                            }

                            var objectName;
                            if (window.location.href.toLowerCase().indexOf('index') > 0)
                                objectName = window.location.href.substring(0, window.location.href.lastIndexOf('/'));
                            else {
                                var count = (window.location.href.toLowerCase().match(/\/\//g) || []).length;
                                //console.log('count ' + count);
                                if (count > 0) {
                                    //case of http://10.10.10.10/itms/escalationsearch
                                    //case of http://10.10.10.10/escalationsearch
                                    //console.log('case 1 ');
                                    if ((window.location.href.toLowerCase().match(/\//g) || []).length > 3) {
                                        objectName = window.location.href.substring(window.location.href.lastIndexOf('/') + 1, window.location.href.length);
                                    } else {
                                        objectName = window.location.href;
                                    }
                                } else {
                                    //case of 10.10.10.10/itms/escalationsearch
                                    //case of 10.10.10.10/escalationsearch
                                    //console.log('case 2 ');
                                    if ((window.location.href.toLowerCase().match(/\//g) || []).length > 2) {
                                        //console.log('case 2 - 1 ');
                                        objectName = window.location.href.substring(window.location.href.lastIndexOf('/') + 1, window.location.href.length);
                                    } else {
                                        // console.log('case 1 - 1 ');
                                        objectName = window.location.href;
                                    }
                                }
                                //objectName = window.location.href;

                            }
                            objectName = objectName.substring(objectName.lastIndexOf('/') + 1, objectName.length)

                            //console.log('objectName : ' + objectName);
                            var dataPost = {};
                            dataPost.Reason = $('#Reason').val();
                            dataPost.RecordId = $('#RecordId').val();
                            dataPost.ObjectName = objectName.toLowerCase().replace('search', '');
                            HIRETHINGS.Global.MakeAjaxCall("POST", 'ItemDelete/Index', dataPost, function (result) {

                                $('#confirm-delete').modal('hide');
                                if (result == 'True') {

                                    $('#errorDiv').html('<div class="row FadeOut mb-sm"><div class="col-lg-12"><div class="alert alert-warning mb-sm"><p class="m-none text-semibold h6">Success : Record has been Deleted Successfully.</p></div></div></div>');
                                    functionToExecute();

                                } else {
                                    $('#errorDiv').html('<div class="row FadeOut mb-sm"><div class="col-lg-12"><div class="alert alert-danger mb-sm"><p class="m-none text-semibold h6">Error : Record Deletion Failed.</p></div></div></div>');
                                }
                            }, function (xhr, ajaxOptions, thrownError) {
                                console.log('an error occurred in making ajax call');

                            });
                        });
                    }, function (xhr, ajaxOptions, thrownError) {
                        console.log('an error occurred in making ajax call');

                    });
                });
            });
        },

        //Tempfunction: function () { console.log('Yes');}

    };
}());
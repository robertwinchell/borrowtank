/*** 
* Used for defining the HIRETHINGS  UI
* @module UI
* @namespace HIRETHINGS.temp
*/

HIRETHINGS.namespace("UI.Advertisement");
HIRETHINGS.UI.Advertisement = (function () {
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

        HIRETHINGS.Global.ApplyMenuSelection('Advertisement');
        HIRETHINGS.Global.KeyLocking('.hasKeyLocking', 'data-keylocking');




        $('#ddlCategory').on('dblclick', function () {

            alert('only level 0 categories yet');
        });


        $('input[type=radio][name=chkIsMoreThanOne]').change(function () {
            if (this.value == '0') {
                $('#divIsVisible').addClass('hidden');
            }
            else if (this.value == '1') {
                $('#divIsVisible').removeClass('hidden');
            }
        });
        $('input[type=radio][name=chkIsVisibleToPublic]').change(function () {
            if (this.value == '0') {
                $('#divQuantity').addClass('hidden');

            }
            else if (this.value == '1') {
                $('#divQuantity').removeClass('hidden');
            }
        });

        $('input[type=radio][name=chkChargingOption]').change(function () {
            if (this.value == '1') {
                $('#hourlyChargeDiv').removeClass('hidden');
                $('.fixedRate').addClass('hidden');
            }
            else if (this.value == '2') {
                $('#hourlyChargeDiv').addClass('hidden');
                $('.fixedRate').removeClass('hidden');

            }
        });
        $('input[type=checkbox][name=hirePeriod]').change(function () {
            if ($(this).is(':checked')) {

                $(this).closest('tr').find('.hpvalue').removeAttr('disabled');

            } else {
                $(this).closest('tr').find('.hpvalue').attr('disabled', 'disabled').val('0.00');

                $(this).closest('tr').find('.text-danger').remove();
                if ($('#ddlDefaultDisplayRate option[val=' + $(this).data('hpid') + ']') != undefined)
                    $('#ddlDefaultDisplayRate option[val=' + $(this).data('hpid') + ']').remove();
            }
        });


        $('.hpvalue').focusout(function () {
          
            if ($.isNumeric($(this).val())) {
              
                if ($(this).val() != 0) {

                    var val = $(this).closest('tr').find('input[type="checkbox"]').data('hpid');
                    var text = $(this).closest('tr').find('label').html();
                    if ($("#ddlDefaultDisplayRate option[value='" + val + "']").length == 0)
                        $('#ddlDefaultDisplayRate')
                        .append($('<option>', { value: val })
                        .text(text));
                    //is valid
                    $(this).closest('td').children('.text-danger').remove();
                   

                } else {
                    //should be greater than 0
                    $(this).closest('td').children('.text-danger').remove();
                    $(this).closest('td').append('<span class="text-danger field-validation-error" data-valmsg-for="" data-valmsg-replace="true"><span for="" class="">Value must be greater than 0.</span></span>');
                }
            } else {
                //should be numeric
                $(this).closest('td').children('.text-danger').remove();
                $(this).closest('td').append('<span class="text-danger field-validation-error" data-valmsg-for="" data-valmsg-replace="true"><span for="" class="">Value must be a number.</span></span>');
            }
        });


        $('#AdvertisementForm').submit(function (e) {
            e.preventDefault();
            $(this).validate();

            if ($(this).valid()) {
                SaveAdvertisement();
            } else {
                $('.input-validation-error').first().focus();
                $('html, body').animate({
                    scrollTop: $('.input-validation-error').first()
                }, 1500);
            }
        });

        HIRETHINGS.Global.KeyLocking('.keylock', 'data-keylock');
    }
    function SaveAdvertisement() {
        var data = {};
        data.Name = $('#txtName').val();
        data.Description = $('#txtDescription').val();
        data.Email = $('#txtEmail').val();
        data.Web = $('#txtWeb').val();
        data.PhoneNumber = $('#txtPhoneNumber').val();
        data.Address = $('#txtAddress').val();
        data.IsMoreThanOne = $('input[type=radio][name=chkIsMoreThanOne]:checked').val();
        data.IsVisibleToPublic = $('input[type=radio][name=chkIsVisibleToPublic]:checked').val();
        data.Quantity = $('#txtQuantity').val();
        data.CategoryIds = $('#ddlCategory').val();
        data.ChargingTypeId = $('input[type=radio][name=chkChargingOption]:checked').val();
        var arrayOfTimePrices = [];
        $('.hirePeriod:checkbox:checked').each(function () {
            var timePrice = {};
            timePrice.Id = $(this).attr('data-hpid');
            timePrice.Price = $(this).closest('tr').find('.hpvalue').val();
            arrayOfTimePrices.push(timePrice);
        });

        data.TimePrices = arrayOfTimePrices;
        if ($('#ddlDefaultDisplayRate').val() != undefined)
            data.DefaultTimeChargingId = $('#ddlDefaultDisplayRate').val();
        else
            data.DefaultTimeChargingId = 0;

        data.MinimumCharges = $('#txtMinimumCharges').val();
        data.BondCharges = $('#txtBondCharges').val();
        data.DepositCharges = $('#txtDepositCharges').val();
        data.SpecialTerms = $('#txtSpecialTerms').val();
        data.FixedRateCharges = $('#txtFixedRateCharges').val();
        data.FixedRateLabel = $('#txtFixedRateLabel').val();
        if (data.FixedRateLabel == '')
            data.FixedRateLabel = 'NULL';
        if (data.Web == '')
            data.Web = 'NULL';
        var dataToPost = JSON.stringify(data);
       

        //checks to ensure correct data posting
        if ($('input[type=radio][name=chkChargingOption]:checked').val() == 1) {
            if (arrayOfTimePrices.length == 0) {
                $('#ddlDefaultDisplayRate').closest('td').children('.text-danger').remove();
                $('#ddlDefaultDisplayRate').closest('td').append('<span class="text-danger field-validation-error" data-valmsg-for="" data-valmsg-replace="true"><span for="" class="">Default display rate is required.</span></span>');

                return;
            } else {
                $('#ddlDefaultDisplayRate').closest('td').children('.text-danger').remove();
            }
        }

        var url = 'Advertisement/Index';
        HIRETHINGS.Global.MakeJSONAjaxCall("POST", url, dataToPost, function (result) {
            if (result.isSuccess === true) {
                // alert(result.Message);
                window.location.href='../Browse'
            } else {
                alert(result.Message);
            }
        }, function (xhr, ajaxOptions, thrownError) {
            console.log('an error occurred in making ajax call');
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
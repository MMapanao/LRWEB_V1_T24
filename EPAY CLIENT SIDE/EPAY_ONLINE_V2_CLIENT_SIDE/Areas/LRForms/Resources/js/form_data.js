var idleTime = 0;
var idleInterval = 0;
$(document).ready(function () {

    idleInterval = setInterval(timerIncrement, 60000) //1min
    //idleInterval = setInterval(timerIncrement, 1000)
    $(this).mousemove(function (e) {
        idleTime = 0;
    });
    $(this).keypress(function (e) {
        idleTime = 0;
    });

    $(document).ready(function () {
        $('[data-toggle="tooltip"]').tooltip();
    });
    $("#cboxaccept").prop("checked", false)
    $(document).on('change', '#loanPurpose', function () {
        if ($("#loanPurpose option:selected").val() == "Others") {
            $("#loanOthers").prop("hidden", false);
            $("#loanPurposeOthers").val("");
        } else {
            $("#loanOthers").prop("hidden", "true");
        }
    });

    $("#ExpiryDate").mim
    var form = $("#example-form");

    $.validator.addMethod('checknum', function (value, element) { return value == 'Required'; }, 'Required');
    $.validator.addMethod("lessThanEqual", function (value, element, int) {
        return this.optional(element) || parseInt(value) <= int;
    }, "Please enter a lesser value on Loan Amount.");

    $.validator.addMethod('lessThan', function (value, element, param) {
        return this.optional(element) || parseFloat(value) <= parseFloat($(param).val());
    }, 'Change loan amount or loan term.');

    jQuery.validator.addMethod("notEqual", function (value, element, param) {
        return this.optional(element) || value != param;
    }, "Enter loan amount");

    $.validator.addMethod("minyear", function (value, element, string) {
        return value.indexOf(string);
    }, 'Minimum 1 year');

    $.validator.addMethod("minage", function (value, element, string) {
        return value.indexOf(string);
    }, 'Minimum 18 years');

    form.validate({
        errorPlacement: function errorPlacement(error, element) { element.before(error); },
        rules: {
            cboxregular: {
                required: true
            },
            loanAmount: {
                lessThanEqual: 1000000
            },
            tenure: {
                minyear: "0 YRS"
            },
            loanPurpose: {
                required: true
            },
            age: {
                minage: "0 YRS",
                minage: "1 YRS",
                minage: "2 YRS",
                minage: "3 YRS",
                minage: "4 YRS",
                minage: "5 YRS",
                minage: "6 YRS",
                minage: "7 YRS",
                minage: "8 YRS",
                minage: "9 YRS",
                minage: "10 YRS",
                minage: "11 YRS",
                minage: "12 YRS",
                minage: "13 YRS",
                minage: "14 YRS",
                minage: "15 YRS",
                minage: "16 YRS",
                minage: "17 YRS"
            },


            monthlyAmortization: {
                lessThan: "#creditRatio",
                notEqual: "0",
            },
            fileupload1: {
                required: true
            },
            fileupload2: {
                required: true
            },
            fileupload3: {
                required: true
            },
            fileupload4: {
                required: true
            },
            fileupload5: {
                required: true
            },
            Present_Years: {
                checknum: {
                    depends: function (element) {
                        return $("#Present_Years").val() == '0' && $("#Present_Months").val() == '0';
                    }
                }
            },
            Present_Months: {
                checknum: {
                    depends: function (element) {
                        return $("#Present_Years").val() == '0' && $("#Present_Months").val() == '0';
                    }
                }
            },
            Permanent_Years: {
                checknum: {
                    depends: function (element) {
                        return $("#Permanent_Years").val() == '0' && $("#Permanent_Months").val() == '0';
                    }
                }
            },
            Permanent_Months: {
                checknum: {
                    depends: function (element) {
                        return $("#Permanent_Years").val() == '0' && $("#Permanent_Months").val() == '0';
                    }
                }
            },
        }
    });
    form.children("div").steps({
        headerTag: "h3",
        bodyTag: "section",
        transitionEffect: "slideLeft",
        onStepChanging: function (event, currentIndex, newIndex) {

            if (currentIndex > newIndex) {
                return true;
            }
            if (currentIndex == 3) {

                $(".wizard .actions a[href='#finish']").hide();
            }
            form.validate().settings.ignore = ":hidden";
            return form.valid();
            //return true;
        },
        onFinishing: function (event, currentIndex) {
            //form.validate().settings.ignore = ":disabled";
            return form.valid();
            //return true
        },
        onFinished: function (event, currentIndex) {
            //<button onclick="loadpreview()" type="button" class="btn btn-info btn-lg" data-toggle="modal" data-target="#myModal">Open Modal</button>

            $("#OTP").prop('hidden', false);
            //alert("Submitted!");
        }
        //,stepsOrientation: "vertical"
    });
    //$('#monthlyAmortization').change(function () {
    //    $('#monthlyAmortization').validate();
    //    $('#monthlyAmortization').valid()
    //});
    $("#birthDate").focusout(function () {
        //var dob = new Date($("#birthDate").val());
        //var diff_ms = Date.now() - dob.getTime();
        //var age_dt = new Date(diff_ms);
        //var age = Math.abs(age_dt.getUTCFullYear() - 1970);
        var age;
        age = diffDate(new Date($("#birthDate").val()), Date.now());
        $("#age").val(age.years + " YRS AND " + age.months + " MONTHS OLD");
    });
    $("#spouseBirthDate").focusout(function () {
        var sage;
        sage = diffDate(new Date($("#spouseBirthDate").val()), Date.now());
        $("#spouseAge").val(sage.years + " YRS AND " + sage.months + " MONTHS OLD");
    });
    $("#dateHired").focusout(function () {
        //var dt = new Date($("#dateHired").val());
        //var diff = (Date.now() - dt.getTime()) / 1000;
        //diff /= (60 * 60 * 24 * 7 * 4);
        //var tenure = Math.abs(Math.round(diff));
        //tenure += " Months";
        //$("#tenure").val(tenure);
        var ten;
        ten = diffDate(new Date($("#dateHired").val()), Date.now());
        $("#tenure").val(ten.years + " YRS AND " + ten.months + " MONTHS OLD");
    });
    $("#SSS").focusout(function () {
        if ($("#SSS").val() == "") {

            $("#gsisField").prop("hidden", false)
        } else {
            $("#gsisField").prop("hidden", "true")
        }
    });

    $("#GSIS").focusout(function () {
        if ($("#GSIS").val() == "") {
            $("#sssField").prop("hidden", false)
        } else {
            $("#sssField").prop("hidden", "true")
        }
    });

    $("#civilStatus").focusout(function () {
        if ($("#civilStatus").val() == "Married" || $("#civilStatus").val() == "Living with Partner") {
            $("#spouseInfo").prop("hidden", false)
        } else {
            $("#spouseInfo").prop("hidden", "true")
        }
    });

    $("#creditOption").change(function () {
        reset();
        if ($("#creditOption").val() == "csbNew") {
            $("#csbNew").prop("hidden", false)
        } else if ($("#creditOption").val() == "csbOld") {
            $("#csbOld").prop("hidden", false)
        } else if ($("#creditOption").val() == "payrollUBP") {
            $("#payrollUbp").prop("hidden", false)
        } else if ($("#creditOption").val() == "others") {
            $("#payrollOthers").prop("hidden", false)
        }
    });

    //loaddata
    $(document).on('click', '#btncancelapplication', function () {
        $.confirm({
            title: 'Cancel application',
            content: 'Are you sure?',
            buttons: {
                formSubmit: {
                    text: 'Yes',
                    btnClass: 'btn-orange',
                    action: function () {
                        var appID = $("label[id='appID']").html();
                        var CIFno = $("label[id='CIFno']").html();
                        console.log(appID)
                        console.log(CIFno)
                        $.ajax({
                            url: FolderName + "/LRForms/Fillup/CancelApplication",
                            contentType: false,
                            type: "POST",
                            contentType: 'application/json; charset=utf-8',
                            data: JSON.stringify({ appID: appID, CIFno: CIFno }),
                            async: true,
                            cache: false,
                            processData: false,
                            success: function (data) {
                                if (data == "Success") {
                                    setTimeout(function () {
                                        ;
                                        window.location.href = FolderName + "/LRForms/Message/InfoCancelled";
                                    }, 2000);
                                }

                            },
                            error: function (ex) {
                                $('.modal').modal('hide');
                            }
                        });
                    }
                },
                No: {
                    btnClass: 'btn-orange',
                    action: function () {
                        //close
                    }
                },
            },
            onContentReady: function () {
                // bind to events
                var jc = this;
                this.$content.find('form').on('submit', function (e) {
                    // if the user submits the form by pressing enter in the field.
                    e.preventDefault();
                    jc.$$formSubmit.trigger('click'); // reference the button and click it
                });
            }
        });
    });

    $(document).on('click', '#btnproceed', function () {
        loaddata();
        $('#loadingmodal').modal('show');
        setTimeout(function () {
            $("#frontpage").css("display", "none");
            $("#loanpage").css("display", "");
            $('.modal').modal('hide');
        }, 5000);
    });

    $(document).on("focusout", 'input', function () {
        var thisid = this.id;
        if (thisid != "Email_Address") {
            //$(this).val($(this).val().toUpperCase());
            const element = $(this);
            element.val($(this).val().toUpperCase());
            const textToReplace = element.val();
            const newText = textToReplace.replace("Ñ", "N");
            element.val(newText);
        }
    });

    $(document).on('click', '#txtpicture1', function () {
        $('#fileupload1').focus().trigger('click');
    });
    $(document).on('click', '#txtpicture2', function () {
        $('#fileupload2').focus().trigger('click');
    });
    $(document).on('click', '#txtpicture3', function () {
        $('#fileupload3').focus().trigger('click');
    });
    $(document).on('click', '#txtpicture4', function () {
        $('#fileupload4').focus().trigger('click');
    });
    $(document).on('click', '#txtpicture5', function () {
        $('#fileupload5').focus().trigger('click');
    });
    $(document).on('click', '#txtpicture6', function () {
        $('#fileupload6').focus().trigger('click');
    });

    $("#requestOTP").on('click', function (e) {
        e.preventDefault();
        form.validate();
        var res = form.valid()
        if (res == false) {
            $.alert("Complete all required fields");
        } else {
            if ($("#fileupload1").val() == "") {
                $.alert("Photo is Required");
            } else if ($("#fileupload2").val() == "") {
                $.alert("Payslip is Required");
            } else if ($("#fileupload3").val() == "") {
                $.alert("Certificate of Employment / Company ID is Required");
            } else if ($("#fileupload4").val() == "") {
                $.alert("ID picture (Front) is Required");
            } else if ($("#fileupload5").val() == "") {
                $.alert("ID picture (Back) is Required");
            } else if ($("#fileupload6").val() == "") {
                $.alert("Signature is Required");
            } else {
                $.ajax({
                    url: FolderName + "/LRForms/Fillup/requestOTP",
                    dataType: "json",
                    type: "GET",
                    contentType: 'application/json; charset=utf-8',
                    async: true,
                    processData: false,
                    cache: false,
                    success: function (data) {
                        alert(data.message);
                        var fiveMinutes = 60 * 5,
                            display = document.querySelector('#counter');
                        startTimer(fiveMinutes, display)
                        $("#timerdisplay").prop("hidden", false);
                        $("#requestOTP").prop("hidden", true);
                        $("#submitOTP").prop("hidden", false);
                    },
                    error: function (ex) {
                        $.alert("Internal Server Error");
                    }
                });
            }

        }

    })

    $("#submitOTP").on('click', function (e) {
        $("#loadingmodal").modal('show');
        e.preventDefault();
        let otp = $("input[id=OTP]").val();

        $.ajax({
            url: FolderName + "/LRForms/Fillup/checkOTP",
            dataType: "json",
            type: "POST",
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify({ otp: otp }),
            async: true,
            processData: false,
            cache: false,
            success: function (data) {
                //if (data.message == "valid") {
                if (data.message == "valid") {
                    const months = [
                        'January',
                        'February',
                        'March',
                        'April',
                        'May',
                        'June',
                        'July',
                        'August',
                        'September',
                        'October',
                        'November',
                        'December'
                    ]

                    d = new Date();
                    utc = d.getTime() + (d.getTimezoneOffset() * 60000);
                    nd = new Date(utc + (3600000 * 8));

                    const monthIndex = d.getMonth();
                    const monthName = months[monthIndex];

                    var dateGranted = monthName + " " + d.getDate() + ", " + d.getFullYear();
                    $("i[name='signature']").text("(Electronically Signed via OTP) OTP: " + otp + " OTP EMAIL :  " + data.email);
                    $("i[name='otp']").text("OTP VERIFIED ON: " + dateGranted + " " + nd.toLocaleTimeString('en-US', { hour: 'numeric', minute: 'numeric', hour12: true }));
                    $("input[name='dateGrantedDay']").val(nd.getDate());
                    $("input[name='dateGrantedMonthYear']").val(monthName + " " + nd.getFullYear());
                    clearInterval(mytimer);
                    alert("You have successfully signed the documents.");
                    let OTP = "(Electronically Signed via OTP) OTP: " + otp + " OTP EMAIL :  " + data.email + " OTP VERIFIED ON: " + dateGranted + " " + nd.toLocaleTimeString('en-US', { hour: 'numeric', minute: 'numeric', hour12: true })
                    $("span[name='dOTP']").html(OTP);
                    clearInterval(mytimer);
                    $("input[id='OTP']").prop("disabled", true);
                    $("#timerdisplay").prop("hidden", true);
                    $("#submitOTP").prop("hidden", true);
                    $("#submit").prop("hidden", false);
                    setTimeout(function () {
                        $("#loadingmodal").modal('hide');
                    }, 1000)
                    //alert(OTP)

                    ////clearInterval(mytimer);
                    //$("#timerdisplay").prop("hidden", true);
                    //// create all pdf



                } else if (data.message == "invalid") {
                    alert("OTP is invalid");
                    setTimeout(function () {
                        $("#loadingmodal").modal('hide');
                    }, 2000)
                }
            }
        })
    });

    $("#submit").on('click', function (e) {
        $("#loadingmodal").modal('show');
        e.preventDefault();
        loadpreview();
    });

});
function addCommas(elem) {
    var nStr = document.getElementById(elem.id).value;

    nStr += '';
    x = nStr.split('.');
    if (!x[0]) {
        x[0] = "0";

    }
    x1 = x[0];
    if (!x[1]) {
        x[1] = "00";
    }
    x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    }

    document.getElementById(elem.id).value = x1 + x2;
    return true;
}

function removeCommas(elem) {
    var nStr = document.getElementById(elem.id).value;
    nStr = nStr.replace(/,/g, '');
    document.getElementById(elem.id).value = nStr;

    $(elem).select();

    return true;
}
function reset() {
    $("#csbNew").prop("hidden", 'true');
    $("#csbOld").prop("hidden", 'true');
    $("#payrollUbp").prop("hidden", 'true');
    $("#payrollOthers").prop("hidden", 'true');
}

function ClearAllValidate() {
    $("input").css("border-color", "");
    $("small").css("display", "none");
    $("select").css("border-color", "");
}

function cboxaccept() {
    if ($("#cboxaccept").prop("checked") == true) {
        $("#btnproceed").prop('hidden', false)
    } else {
        $("#btnproceed").prop('hidden', true)
    }
}

$(document).on('click', '#btnsubmitdocs', function () {
    $.confirm({
        title: 'Submit application',
        content: 'Are you sure?',
        buttons: {
            formSubmit: {
                text: 'Yes',
                btnClass: 'btn-orange',
                action: function () {
                    var model_state = JMODEL_validateAll();
                    $("#myModal").modal('toggle');
                    $('#loadingmodal').modal('show');
                    //window.location.href = FolderName + "/LRForms/Message/FormApplicationDone";
                    $.ajax({
                        url: FolderName + "/LRForms/Fillup/SubmitEmployeeData",
                        //dataType: "json",
                        contentType: false,
                        type: "POST",
                        data: model_state,
                        async: true,
                        cache: false,
                        processData: false,
                        success: function (data) {

                            //  console.log(data);
                            //if (data != "Success") {
                            //    $('.modal').modal('hide');
                            //    window.location.href = FolderName + "/LRForms/Message/Submited";
                            //}
                            if (data == "Invalid Tokens") {
                                // window.location.href = FolderName + "/LRForms/Message/ReferenceInvalid";
                            }
                            if (data == "Success") {
                                setTimeout(function () {
                                    //$('.modal').modal('hide');
                                    //window.location.href = FolderName + "/LRForms/Message/FormApplicationDone?" + JModelReview();
                                    window.location.href = FolderName + "/LRForms/Message/FormApplicationDone";
                                }, 2000);
                            }

                        },
                        error: function (ex) {
                            // alert(ex);
                            $('.modal').modal('hide');
                        }
                    });
                }
            },
            No: {
                btnClass: 'btn-orange',
                action: function () {
                    //close
                }
            },
        },
        onContentReady: function () {
            // bind to events
            var jc = this;
            this.$content.find('form').on('submit', function (e) {
                // if the user submits the form by pressing enter in the field.
                e.preventDefault();
                jc.$$formSubmit.trigger('click'); // reference the button and click it
            });
        }
    });

});

$(document).on('click', '#btneditdocs', function () {
    $.confirm({
        title: 'Submit application',
        content: 'Are you sure?',
        buttons: {
            formSubmit: {
                text: 'Yes',
                btnClass: 'btn-orange',
                action: function () {
                    var model_state = JMODEL_validateAll();
                    $("#myModal").modal('toggle');
                    $('#loadingmodal').modal('show');
                    //window.location.href = FolderName + "/LRForms/Message/FormApplicationDone";
                    $.ajax({
                        url: FolderName + "/LRForms/Fillup/ChangeEmployeeData",
                        //dataType: "json",
                        contentType: false,
                        type: "POST",
                        data: model_state,
                        async: true,
                        cache: false,
                        processData: false,
                        success: function (data) {

                            //  console.log(data);
                            //if (data != "Success") {
                            //    $('.modal').modal('hide');
                            //    window.location.href = FolderName + "/LRForms/Message/Submited";
                            //}
                            if (data == "Invalid Tokens") {
                                // window.location.href = FolderName + "/LRForms/Message/ReferenceInvalid";
                            }
                            if (data == "Success") {
                                setTimeout(function () {
                                    //$('.modal').modal('hide');
                                    //window.location.href = FolderName + "/LRForms/Message/FormApplicationDone?" + JModelReview();
                                    window.location.href = FolderName + "/LRForms/Message/FormApplicationDone";
                                }, 2000);
                            }

                        },
                        error: function (ex) {
                            // alert(ex);
                            $('.modal').modal('hide');
                        }
                    });
                }
            },
            No: {
                btnClass: 'btn-orange',
                action: function () {
                    //close
                }
            },
        },
        onContentReady: function () {
            // bind to events
            var jc = this;
            this.$content.find('form').on('submit', function (e) {
                // if the user submits the form by pressing enter in the field.
                e.preventDefault();
                jc.$$formSubmit.trigger('click'); // reference the button and click it
            });
        }
    });

});



$(document).on('change', '#cboxpermanent', function () {

    if ($(this).prop('checked')) {
        $('#loadingmodal').modal('show');
        $("#Permanent_City").empty();
        $("#Permanent_Barangay").empty();
        $("#Permanent_Address").val($("#Present_Address").val());
        $("#Permanent_Province").val($("#Present_Province").val());
        $("#Permanent_Years").val($("#Present_Years").val());
        $("#Permanent_Months").val($("#Present_Months").val());
        $("#Permanent_Ownership").val($("#Present_Ownership").val());
        $("#Permanent_Telephone").val($("#Present_Telephone").val());
        $("#Permanent_Country").val($("#Present_Country").val());
        var $cityoptions = $("#Present_City > option").clone();
        var $brgyyoptions = $("#Present_Barangay > option").clone();
        $("#Permanent_City").append($cityoptions);
        $("#Permanent_Barangay").append($brgyyoptions);

        $("#Permanent_Address").attr("readonly", true);
        $("#Permanent_Barangay").attr("readonly", true);
        $("#Permanent_Ownership").attr("readonly", true);
        $("#Permanent_Years").attr("readonly", true);
        $("#Permanent_Months").attr("readonly", true);
        $("#Permanent_Telephone").attr("readonly", true);
        $("#Permanent_City").attr("readonly", true);
        $("#Permanent_Province").attr("readonly", true);
        $("#Permanent_Zipcode").attr("readonly", true);
        $("#Permanent_Country").attr("readonly", true);
        $("#Permanent_Zipcode").val($("#Present_Zipcode").val());

        setTimeout(function () {
            $("#Permanent_City").val($("#Present_City").val());
            $("#Permanent_Barangay").val($("#Present_Barangay").val());
            $('#loadingmodal').modal('hide');
        }, 1000);


    } else {
        $("#Permanent_Address").attr("readonly", false);
        $("#Permanent_Barangay").attr("readonly", false);
        $("#Permanent_Ownership").attr("readonly", false);
        $("#Permanent_Years").attr("readonly", false);
        $("#Permanent_Months").attr("readonly", false);
        $("#Permanent_Telephone").attr("readonly", false);
        $("#Permanent_City").attr("readonly", false);
        $("#Permanent_State").attr("readonly", false);
        $("#Permanent_Zipcode").attr("readonly", false);
        $("#Permanent_Country").attr("readonly", false);
    }
});

$(document).on('change', '#Present_City', function () {
    $('#Present_Barangay').attr('DISABLED', true);
    loadpresentbrgy();
    setTimeout(function () {
        $('#Present_Barangay').attr('DISABLED', false);

    }, 1000);
});

$(document).on('change', '#Permanent_City', function () {
    $('#Permanent_Barangay').attr('DISABLED', true);
    loadpermabrgy();
    setTimeout(function () {
        $('#Permanent_Barangay').attr('DISABLED', false);

    }, 1000);
});

$(document).on('change', '#Employer_City', function () {
    $('#Employer_Barangay').attr('DISABLED', true);
    loademployerbrgy();
    setTimeout(function () {
        $('#Employer_Barangay').attr('DISABLED', false);

    }, 1000);
});

$(document).on('change', '#Present_Province', function () {
    $('#Present_City').attr('DISABLED', true);
    loadpresentcity();
    setTimeout(function () {
        $('#Present_City').attr('DISABLED', false);

    }, 1000);
});

$(document).on('change', '#Permanent_Province', function () {
    $('#Permanent_City').attr('DISABLED', true);
    loadpermacity();
    setTimeout(function () {
        $('#Permanent_City').attr('DISABLED', false);

    }, 1000);
});

$(document).on('change', '#Employer_Province', function () {
    $('#Employer_City').attr('DISABLED', true);
    loademployercity();
    setTimeout(function () {
        $('#Employer_City').attr('DISABLED', false);

    }, 1000);
});

$(document).on('change', '#fileupload1', function () {
    readURL(this, "#txtpicture1");
});

$(document).on('change', '#fileupload2', function () {
    readURL(this, "#txtpicture2");
});
$(document).on('change', '#fileupload3', function () {
    readURL(this, "#txtpicture3");
});
$(document).on('change', '#fileupload4', function () {
    readURL(this, "#txtpicture4");
});

$(document).on('change', '#fileupload5', function () {
    readURL(this, "#txtpicture5");
});

$(document).on('change', '#fileupload6', function () {
    readURL(this, "#txtpicture6");
});

function readURL(input, pictureid) {

    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $(pictureid).attr('src', e.target.result);
        }

        reader.readAsDataURL(input.files[0]);
    }
}

function loaddata() {
    $('#loadingmodal').modal('show');
    $.ajax({
        url: FolderName + "/LRForms/Fillup/getLoanPurpose",
        dataType: "json",
        type: "GET",
        contentType: 'application/json; charset=utf-8',
        async: true,
        processData: false,
        cache: false,
        success: function (data) {
            var elm = $("#loanPurpose");
            elm.empty();
            elm.append(`<option disabled selected>--Please select--</option>`)
            var res = jQuery.parseJSON(data);

            $.each(res, function (key, value) {
                if (res[key].value != 'Small Business Financing') {
                    elm.append(`<option value="${res[key].value}"> ${res[key].display}</option>`)
                }

            });
        },
        error: function (ex) {
            alert("Internal Server Error");
        }
    });

    $.ajax({
        url: FolderName + "/LRForms/Fillup/getCivilStatus",
        dataType: "json",
        type: "GET",
        contentType: 'application/json; charset=utf-8',
        async: true,
        processData: false,
        cache: false,
        success: function (data) {
            var elm = $("#civilStatus");
            elm.empty();
            elm.append(`<option disabled selected>--Please select--</option>`)
            var res = jQuery.parseJSON(data);
            $.each(res, function (key, value) {
                elm.append(`<option value="${res[key].value}">${res[key].display}</option>`)
            });
        },
        error: function (ex) {
            alert("Internal Server Error");
        }
    });

    $.ajax({
        url: FolderName + "/LRForms/Fillup/getNatureOfWork",
        dataType: "json",
        type: "GET",
        contentType: 'application/json; charset=utf-8',
        async: true,
        processData: false,
        cache: false,
        success: function (data) {
            var elm = $("#natureOfWork");
            var elm2 = $("#spouseNatureofwork");
            elm.empty();
            elm.append(`<option disabled selected>--Please select--</option>`)
            elm2.empty();
            elm2.append(`<option disabled selected>--Please select--</option>`)
            var res = jQuery.parseJSON(data);
            $.each(res, function (key, value) {
                elm.append(`<option value="${res[key].value}"> ${res[key].display}</option>`)
                elm2.append(`<option value="${res[key].value}"> ${res[key].display}</option>`)
            });
        },
        error: function (ex) {
            alert("Internal Server Error");
        }
    });

    //$.ajax({
    //    url: FolderName + "/LRForms/Fillup/getOwnership",
    //    dataType: "json",
    //    type: "GET",
    //    contentType: 'application/json; charset=utf-8',
    //    async: true,
    //    processData: false,
    //    cache: false,
    //    success: function (data) {
    //        var elm = $("#Permanent_Ownership");
    //        var elm2 = $("#Present_Ownership");
    //        elm.empty();
    //        elm2.empty();
    //        var res = jQuery.parseJSON(data);
    //        $.each(res, function (key, value) {
    //            elm.append(`<option value="${res[key].value}"> ${res[key].display}</option>`)
    //            elm2.append(`<option value="${res[key].value}"> ${res[key].display}</option>`)
    //        });
    //    },
    //    error: function (ex) {
    //        alert("Internal Server Error");
    //    }
    //});

    $.ajax({
        url: FolderName + "/LRForms/Fillup/getProvince",
        dataType: "json",
        type: "GET",
        contentType: 'application/json; charset=utf-8',
        async: true,
        processData: false,
        cache: false,
        success: function (data) {
            var elm = $("#Permanent_Province");
            var elm2 = $("#Present_Province");
            var elm3 = $("#Employer_Province");
            elm.empty();
            elm2.empty();
            elm3.empty();
            elm.append(`<option disabled selected>--Please select--</option>`)
            elm2.append(`<option disabled selected>--Please select--</option>`)
            elm3.append(`<option disabled selected>--Please select--</option>`)
            var res = jQuery.parseJSON(data);
            $.each(res, function (key, value) {
                elm.append(`<option value="${res[key].MappingId}"> ${res[key].Title}</option>`)
                elm2.append(`<option value="${res[key].MappingId}"> ${res[key].Title}</option>`)
                elm3.append(`<option value="${res[key].MappingId}"> ${res[key].Title}</option>`)
            });
            //loadpermacity();
            //loadpresentcity();
            //loademployercity();
        },
        error: function (ex) {
            alert("Internal Server Error");
        }
    });

    $.ajax({
        url: FolderName + "/LRForms/Fillup/getIssueAuth",
        dataType: "json",
        type: "GET",
        contentType: 'application/json; charset=utf-8',
        async: true,
        processData: false,
        cache: false,
        success: function (data) {
            var elm = $("#issueAuth");
            elm.empty();
            elm.append(`<option disabled selected>--Please select--</option>`)
            var res = jQuery.parseJSON(data);
            $.each(res, function (key, value) {
                elm.append(`<option value="${res[key].value}"> ${res[key].display}</option>`)
            });
        },
        error: function (ex) {
            alert("Internal Server Error");
        }
    });

    $.ajax({
        url: FolderName + "/LRForms/Fillup/getDocumentName",
        dataType: "json",
        type: "GET",
        contentType: 'application/json; charset=utf-8',
        async: true,
        processData: false,
        cache: false,
        success: function (data) {
            var elm = $("#documentName");
            elm.empty();
            elm.append(`<option disabled selected>--Please select--</option>`)
            var res = jQuery.parseJSON(data);
            $.each(res, function (key, value) {
                elm.append(`<option value="${res[key].value}"> ${res[key].display}</option>`)
            });
        },
        error: function (ex) {
            alert("Internal Server Error");
        }
    });

}

function loadpermacity() {
    $('#loadingmodal').modal('show');
    var elm = $("#Permanent_City")
    var ID = $("#Permanent_Province").val();
    $.ajax({
        url: FolderName + "/LRForms/Fillup/getCity",
        dataType: "json",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        async: true,
        processData: false,
        data: JSON.stringify({ provinceId: ID }),
        cache: false,
        success: function (data) {
            elm.empty();
            elm.append(`<option disabled selected>--Please select--</option>`)
            var res = jQuery.parseJSON(data);
            $.each(res, function (key, value) {
                elm.append(`<option value="${res[key].MappingId}"> ${res[key].Title}</option>`)
            });
            //loadpermabrgy();
            setTimeout(function () {
                $("#loadingmodal").modal('hide');
            }, 1000);
        },

        error: function (ex) {
            alert("Internal Server Error");
        }
    });
}

function loadpermabrgy() {
    //$('#loadingmodal').modal('show');
    var elm = $("#Permanent_Barangay")
    var ID = $("#Permanent_City").val();
    $('#loadingmodal').modal('show');
    $.ajax({
        url: FolderName + "/LRForms/Fillup/getBarangay",
        dataType: "json",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        async: true,
        processData: false,
        data: JSON.stringify({ cityId: ID }),
        cache: false,
        success: function (data) {
            elm.empty();
            elm.append(`<option disabled selected>--Please select--</option>`)
            var res = jQuery.parseJSON(data);
            $.each(res, function (key, value) {
                elm.append(`<option value="${res[key].SysId}"> ${res[key].Title}</option>`)
            });
            setTimeout(function () {
                $("#loadingmodal").modal('hide');
            }, 1000);
        },
        error: function (ex) {
            alert("Internal Server Error");
        }
    });
}

function loadpresentcity() {
    $('#loadingmodal').modal('show');
    var elm = $("#Present_City")
    var ID = $("#Present_Province").val();
    $.ajax({
        url: FolderName + "/LRForms/Fillup/getCity",
        dataType: "json",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        async: true,
        processData: false,
        data: JSON.stringify({ provinceId: ID }),
        cache: false,
        success: function (data) {
            elm.empty();
            elm.append(`<option disabled selected>--Please select--</option>`)
            var res = jQuery.parseJSON(data);
            $.each(res, function (key, value) {
                elm.append(`<option value="${res[key].MappingId}"> ${res[key].Title}</option>`)
            });
            //loadpresentbrgy();
            setTimeout(function () {
                $("#loadingmodal").modal('hide');
            }, 1000);;
        },
        error: function (ex) {
            alert("Internal Server Error");
        }
    });
}

function loadpresentbrgy() {
    $('#loadingmodal').modal('show');
    var elm = $("#Present_Barangay")
    var ID = $("#Present_City").val();

    $.ajax({
        url: FolderName + "/LRForms/Fillup/getBarangay",
        dataType: "json",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        async: true,
        processData: false,
        data: JSON.stringify({ cityId: ID }),
        cache: false,
        success: function (data) {
            elm.empty();
            elm.append(`<option disabled selected>--Please select--</option>`)
            var res = jQuery.parseJSON(data);
            $.each(res, function (key, value) {
                elm.append(`<option value="${res[key].SysId}"> ${res[key].Title}</option>`)
            });
            setTimeout(function () {
                $("#loadingmodal").modal('hide');
            }, 1000);
        },
        error: function (ex) {
            alert("Internal Server Error");
        }
    });
}

function loademployercity() {
    $('#loadingmodal').modal('show');
    var elm = $("#Employer_City")
    var ID = $("#Employer_Province").val();
    $.ajax({
        url: FolderName + "/LRForms/Fillup/getCity",
        dataType: "json",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        async: true,
        processData: false,
        data: JSON.stringify({ provinceId: ID }),
        cache: false,
        success: function (data) {
            elm.empty();
            elm.append(`<option disabled selected>--Please select--</option>`)
            var res = jQuery.parseJSON(data);
            $.each(res, function (key, value) {
                elm.append(`<option value="${res[key].MappingId}"> ${res[key].Title}</option>`)
            });
            setTimeout(function () {
                $("#loadingmodal").modal('hide');
            }, 1000);
            //loademployerbrgy();
        },
        error: function (ex) {
            alert("Internal Server Error");
        }
    });
}

function loademployerbrgy() {
    $('#loadingmodal').modal('show');
    var elm = $("#Employer_Barangay")
    var ID = $("#Employer_City").val();

    $.ajax({
        url: FolderName + "/LRForms/Fillup/getBarangay",
        dataType: "json",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        async: true,
        processData: false,
        data: JSON.stringify({ cityId: ID }),
        cache: false,
        success: function (data) {
            elm.empty();
            elm.append(`<option disabled selected>--Please select--</option>`)
            var res = jQuery.parseJSON(data);
            $.each(res, function (key, value) {
                elm.append(`<option value="${res[key].SysId}"> ${res[key].Title}</option>`)
            });
            setTimeout(function () {
                $("#loadingmodal").modal('hide');
            }, 1000);
        },
        error: function (ex) {
            alert("Internal Server Error");
        }
    });
}

function loadpreview() {
    $('#loadingmodal').modal('show');

    var today = new Date();
    var dd = String(today.getDate()).padStart(2, '0');
    var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
    var yyyy = today.getFullYear();

    today = mm + '/' + dd + '/' + yyyy;

    var loanamount = $("#loanAmount").val();
    var otherLoans = $("#otherLoans").val();
    var netPay = $("#netPay").val();
    var netincome = $("#netIncome").val();
    var loanterm = $("#loanTerms option:selected").text().toUpperCase() || "N / A";
    var loanpurpose = $("#loanPurpose option:selected").text().toUpperCase() || "N / A";
    var loanothers = $("#loanPurposeOthers").val() || "N / A";
    var fullclientname = $("#salutation option:selected").text() + " " + $("#firstName").val() + " " + $("#middleName").val() + " " + $("#lastName").val() + " " + $("#suffix").val();
    var birthplace = $("#birthPlace").val();
    var birthdate = $("#birthDate").val();
    var age = $("#age").val();
    var gender = $("#gender option:selected").text().toUpperCase();
    var religion = $("#religion").val();
    var citizenship = $("#citizenship option:selected").text();
    var civilstatus = $("#civilStatus option:selected").text().toUpperCase();
    var tin = $("#TIN").val() || "N / A";
    var mothersMaidenName = $("#mothersMaidenName").val() || "N / A";
    var sss = $("#SSS").val() || "N / A";
    var gsis = $("#GSIS").val() || "N / A";
    console.log(mothersMaidenName)
    $("#dOtherLoans").html(otherLoans);
    $("#dNetPay").html(netPay);
    $("#dLoanAmount").html(loanamount);
    $("#dNetIncome").html(netincome);
    $("#dLoanTerm").html(loanterm);
    $("#dLoanPurpose").html(loanpurpose);
    $("#dLoanPurposeOthers").html(loanothers);
    $("#dName").html(fullclientname);
    $("#dBirthPlace").html(birthplace);
    $("#dBirthDate").html(birthdate);
    $("#dAge").html(age);
    $("#dGender").html(gender);
    $("#dReligion").html(religion);
    $("#dCitizenship").html(citizenship);
    $("#dCivilStatus").html(civilstatus);
    $("#dTIN").html(tin);
    $("#dSSS").html(sss);
    $("#dGSIS").html(gsis);
    if (civilstatus == "MARRIED" || civilstatus == "LIVING WITH PARTNER") {
        var sfullname = $("#spouseSalutation option:selected").text().toUpperCase() + " "
            + $("#spouseFirstname").val() + " "
            + $("#spouseMiddlename").val() + " "
            + $("#spouseLastname").val() + " "
            + $("#spouseSuffix").val() || "N / A";
        var sgender = $("#spouseGender option:selected").text().toUpperCase() || "N / A";
        var sage = $("#spouseAge").val() || "N / A";
        var sbirthdate = $("#spouseBirthDate").val() || "N / A";
        var dependents = $("#dependents").val() || "N / A";
        var employer = $("#spouseEmployer").val() || "N / A";
        var sMonthlyIncome = $("#spouseMonthlyIncome").val() || "N / A";
        var sNumber = $("#spouseNumber").val() || "N / A";
        var sOccupation = $("#spouseOccupation option:selected").text().toUpperCase() || "N / A";
    } else {
        var sfullname = "N / A";
        var sgender = "N / A";
        var sage = "N / A";
        var sbirthdate = "N / A";
        var dependents = "N / A";
        var employer = "N / A";
        var sMonthlyIncome = "N / A";
        var sNumber = "N / A";
        var sOccupation = "N / A";
    }

    $("#dsName").html(sfullname);
    $("#dsGender").html(sgender);
    $("#dsAge").html(sage);
    $("#dsBirthDate").html(sbirthdate);
    $("#dsDependents").html(dependents);
    $("#dsEmployer").html(employer);
    $("#dsMonthlyIncome").html(sMonthlyIncome);
    $("#dsNumber").html(sNumber);
    $("#dsOccupation").html(sOccupation);

    var presentaddress = $("#Present_Address").val() + " "
        + $("#Present_Barangay option:selected").text().toUpperCase() + " "
        + $("#Present_City option:selected").text().toUpperCase() + ", "
        + $("#Present_Province option:selected").text().toUpperCase() + " "
        + $("#Present_Country").val() + " "
        + $("#Present_Zipcode").val();
    var presentownership = $("#Present_Ownership option:selected").text().toUpperCase()
    var presentlos = $("#Present_Years").val() + " YEARS and " + $("#Present_Months").val() + " MONTHS.";
    var presenttel = $("#Present_Telephone").val();

    $("#dPresentAddress").html(presentaddress);
    $("#dPresentOwnership").html(presentownership);
    $("#dPresentLengthOfStay").html(presentlos);
    $("#dPresentTelephone").html(presenttel);

    var permanentaddress = $("#Permanent_Address").val() + " "
        + $("#Permanent_Barangay option:selected").text().toUpperCase() + " "
        + $("#Permanent_City option:selected").text().toUpperCase() + ", "
        + $("#Permanent_Province option:selected").text().toUpperCase() + " "
        + $("#Permanent_Country").val() + " "
        + $("#Permanent_Zipcode").val();
    var permanentownership = $("#Permanent_Ownership option:selected").text().toUpperCase()
    var permanentlos = $("#Permanent_Years").val() + " YEARS and " + $("#Permanent_Months").val() + " MONTHS.";
    var permanenttel = $("#Permanent_Telephone").val();

    $("#dPermanentAddress").html(permanentaddress);
    $("#dPermanentOwnership").html(permanentownership);
    $("#dPermanentLengthOfStay").html(permanentlos);
    $("#dPermanentTelephone").html(permanenttel);

    var datehired = $("#dateHired").val();
    var tenure = $("#tenure").val();
    var employeenumber = $("#employeeNumber").val();
    //var position = $("#position").val();
    var rank = $("#rank").val();
    var department = $("#department").val();
    var natureofwork = $("#natureOfWork option:selected").text().toUpperCase();
    var employeraddress = $("#Employer_Address").val() + " "
        + $("#Employer_Barangay option:selected").text().toUpperCase() + " "
        + $("#Employer_City option:selected").text().toUpperCase() + ", "
        + $("#Employer_Province option:selected").text().toUpperCase() + " "
        + $("#Employer_Country").val() + " "
        + $("#Employer_Zipcode").val();
    var employertelephone = $("#Employer_Telephone").val();
    var occupation = $("#occupation option:selected").text().toUpperCase();
    var monthlyallowance = $("#monthlyAllowance").val() || "N / A";
    var dSourceOFIncomeOthers = $("#sourceOfIncomeOthers").val() || "N / A";
    var dMonthlyIncomeOthers = $("#monthlyIncomeOthers").val() || "N / A";
    var monthlyBasicSalary = $("#monthlyBasicSalary").val();
    $("#dDateHired").html(datehired);
    $("#dTenure").html(tenure);
    $("#dEmployeeNumber").html(employeenumber);
    //$("#dPosition").html(position);
    $("#dRank").html(rank);
    $("#dMonthlyBasicSalary").html(monthlyBasicSalary);
    $("#dDepartment").html(department);
    $("#dNatureOfWork").html(natureofwork);
    $("#dEmployerAddress").html(employeraddress);
    $("#deTelephone").html(employertelephone);
    $("#dMonthlyAllowance").html(monthlyallowance);
    $("#dOccupation").html(occupation);
    $("#dSourceOFIncomeOthers").html(dSourceOFIncomeOthers);
    $("#dMonthlyIncomeOthers").html(dMonthlyIncomeOthers);
    var creditOption = $("#creditOption").val();
    var credOption = $("#creditOption option:selected").text().toUpperCase();
    var bankName = "N / A";
    var nameToDisplay = "N / A";
    var accountNumber = "N / A";
    if (creditOption == "csbNew") {
        nameToDisplay = $("#nameToDisplay").val();
    } else if (creditOption == "csbOld") {
        //console.log($("#oldCSBAccount"))
        accountNumber = $("#oldCSBAccount").val();
        //console.log(accountNumber);
    } else if (creditOption == "payrollUBP") {
        accountNumber = $("#payrollUnionAccount").val();
    } else if (creditOption == "others") {
        bankName = $("#payrollOtherBankName").val();
        accountNumber = $("#payrollOtherAccount").val();
    }
    $("#dCreditOption").html(credOption);
    $("#dBankName").html(bankName);
    $("#dNameToDisplay").html(nameToDisplay);
    $("#dAccountNumber").html(accountNumber);

    var firstname = $("#firstName").val();
    var middlename = $("#middleName").val();
    var lastname = $("#lastName").val();


    //$("span[name='dBranchCode']").html('')
    $("span[name='dDate']").html(today)
    $("span[name='dIDType']").html($("#documentName").val())
    $("span[name='dName']").html(fullclientname)
    $("span[name='dPresentAddress']").html(presentaddress)
    $("span[name='dZipcodePresent']").html($("#Present_Zipcode").val())
    $("span[name='dPermanentAddress']").html(permanentaddress)
    $("span[name='dZipcodePermanent']").html($("#Permanent_Zipcode").val())
    $("span[name='dBirthDate']").html(birthdate)
    $("span[name='dBirthplace']").html(birthplace)
    $("span[name='dCitizenship']").html(citizenship)
    $("span[name='dGender']").html(gender)
    $("span[name='dIDTypeCode']").html('')
    $("span[name='dSSS']").html(sss)
    $("span[name='dGSIS']").html(gsis)
    $("span[name='dTIN']").html(tin)
    $("span[name='dSourceOFIncomeOthers']").html(dSourceOFIncomeOthers)
    //$("span[name='dEmail']").html('')
    $("span[name='dEmployerName']").html($("#employerName").val())
    $("span[name='dEmployerNatureOfWork']").html(natureofwork)
    $("span[name='dEmployeeOccupation']").html(occupation)
    $("span[name='dEmployerAddress']").html(employeraddress)
    $("span[name='dEmployerTelephone']").html(employertelephone)
    $("span[name='dClassification']").html($("#classification").val())
    $("span[name='dPresentTelephone']").html(presenttel)
    $("span[name='dCivilStatus']").html(civilstatus)
    //$("span[name='dMobileNumber']").html('')
    $("span[name='dsName']").html(sfullname)
    $("span[name='dsBirthDate']").html(sbirthdate)
    $("span[name='dsNumber']").html(sNumber)
    $("span[name='dsEmployer']").html(employer)
    $("span[name='dMothersMaidenName']").html(mothersMaidenName)
    $("span[name='dsMotherMaidenName']").html(mothersMaidenName)
    $("span[name='dsEmployerAddress']").html('N / A')
    $("span[name='dsNatureofWork']").html('N / A')
    $("span[name='dEmployerZipcode']").html($("#Employer_Zipcode").val())

    $("span[name='dFirstName']").html($("#firstName").val())
    $("span[name='dMiddleName']").html($("#middleName").val())
    $("span[name='dLastName']").html($("#lastName").val())
    if (sss == 'N / A') {
        $("span[name='dSSSGSIS']").html(gsis)
    } else {
        $("span[name='dSSSGSIS']").html(sss)
    }

    if ($("span[name='dCIFNo']").text() == "TEST_CIF_NO" || $("span[name='dCIFNo']").text() == "") {
        //setTimeout(function () {
        //    $('#loadingmodal').modal('hide');
        //    $("#myModal").modal('show');
        //}, 1000);
        $.ajax({
            url: FolderName + "/LRForms/Fillup/getCIF",
            dataType: "json",
            type: "POST",
            contentType: 'application/json; charset=utf-8',
            async: true,
            processData: false,
            data: JSON.stringify({ birthdate: birthdate, lastname: lastname, firstname: firstname, middlename: middlename }),
            cache: false,
            success: function (data) {
                var res = JSON.parse(data)
                if (res[0].mnemonic != null) {
                    $("span[name='dCIFNo']").html(res[0].mnemonic)
                    setTimeout(function () {
                        $('#loadingmodal').modal('hide');
                        $("#myModal").modal('show');
                    }, 1000);
                } else {
                    alert(data)
                }

            },
            error: function (ex) {
                alert("Internal Server Error");
            }
        });
    } else {
        setTimeout(function () {
            $('#loadingmodal').modal('hide');
            $("#myModal").modal('show');
        }, 1000);
    }
}

function diffDate(startDate, endDate) {
    var b = moment(startDate),
        a = moment(endDate),
        intervals = ['years', 'months'],
        out = {};
    for (var i = 0; i < intervals.length; i++) {
        var diff = a.diff(b, intervals[i]);
        b.add(diff, intervals[i]);
        out[intervals[i]] = diff;
    }
    return out;
}

function display(obj) {
    var str = '';
    for (key in obj) {
        str = str + obj[key] + ' ' + key + ' '
    }
    console.log(str);
}

function startTimer(duration, display) {
    var timer = duration, minutes, seconds;
    mytimer = setInterval(function () {
        minutes = parseInt(timer / 60, 10);
        seconds = parseInt(timer % 60, 10);

        minutes = minutes < 10 ? "0" + minutes : minutes;
        seconds = seconds < 10 ? "0" + seconds : seconds;

        display.textContent = minutes + ":" + seconds;

        if (--timer < 0) {
            timer = duration;
        }
        if (timer == 0) {
            alert("Please request another OTP");
            $("#requestOTP").prop("hidden", false);
            $("#submitOTP").prop("hidden", true);
            clearInterval(mytimer);
            $("#timerdisplay").prop("hidden", true);
        }
    }, 1000);
}


function timerIncrement() {
    idleTime = idleTime + 1;
    console.log(idleTime + " min")
    if (idleTime > 15) {
        idleTime = 0;
        $.confirm({
            title: 'Logout?',
            content: 'Due to nactivity, you will be automatically logged out in 60 seconds.',
            autoClose: 'logoutUser|60000',
            buttons: {
                logoutUser: {
                    text: 'logout myself',
                    action: function () {
                        clearInterval(idleInterval);
                        window.location = FolderName + "/lrforms/Message/Timeout";
                    }
                },
                cancel: function () {
                }
            }
        });
    }
}
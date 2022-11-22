$(document).ready(function () {

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
    $.validator.addMethod("lessThanEqual", function (value, element, param) {
        var target = $(param);

        if (this.settings.onfocusout && target.not(".validate-lessThanEqual-blur").length) {
            target.addClass("validate-lessThanEqual-blur").on("blur.validate-lessThanEqual", function () {
                $(element).valid();
            });
        }

        return value <= target.val();
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
            tenure: {
                minyear: "0 YRS"
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
            }
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
            form.validate().settings.ignore = ":hidden";
            return form.valid();
            //return true;
        },
        onFinishing: function (event, currentIndex) {
            //form.validate().settings.ignore = ":disabled";
            return form.valid();
        },
        onFinished: function (event, currentIndex) {
            //<button onclick="loadpreview()" type="button" class="btn btn-info btn-lg" data-toggle="modal" data-target="#myModal">Open Modal</button>

            loadpreview();
            //alert("Submitted!");
        }
        //,stepsOrientation: "vertical"
    });

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
    $(document).on('click', '#btnproceed', function () {
        loaddata();
        $('#loadingmodal').modal('show');
        setTimeout(function () {
            $("#frontpage").css("display", "none");
            $("#loanpage").css("display", "");
            $('.modal').modal('hide');
        }, 3100);
    });



    $(document).on("focusout", 'input', function () {
        var thisid = this.id;
        if (thisid != "Email_Address") {
            $(this).val($(this).val().toUpperCase());
        }
    });
});

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



$(document).on('change', '#cboxpermanent', function () {

    if ($(this).prop('checked')) {

        $("#Permanent_Address").val($("#Present_Address").val());
        $("#Permanent_Province").val($("#Present_Province").val());
        $("#Permanent_Years").val($("#Present_Years").val());
        $("#Permanent_Months").val($("#Present_Months").val());
        $("#Permanent_Ownership").val($("#Present_Ownership").val());
        $("#Permanent_Telephone").val($("#Present_Telephone").val());
        $("#Permanent_Country").val($("#Present_Country").val());
        loadpermacity();

        $("#Permanent_Address").attr("disabled", true);
        $("#Permanent_Barangay").attr("disabled", true);
        $("#Permanent_Ownership").attr("disabled", true);
        $("#Permanent_Years").attr("disabled", true);
        $("#Permanent_Months").attr("disabled", true);
        $("#Permanent_Telephone").attr("disabled", true);
        $("#Permanent_City").attr("disabled", true);
        $("#Permanent_Province").attr("disabled", true);
        $("#Permanent_Zipcode").attr("disabled", true);
        $("#Permanent_Country").attr("disabled", true);
        setTimeout(function () {
            $("#Permanent_City").val($("#Present_City").val());
            loadpermabrgy();
        }, 1000);
        setTimeout(function () {
            $("#Permanent_Barangay").val($("#Present_Barangay").val());
            $("#Permanent_Zipcode").val($("#Present_Zipcode").val());
        }, 2000);
    } else {
        $("#Permanent_Address").attr("disabled", false);
        $("#Permanent_Barangay").attr("disabled", false);
        $("#Permanent_Ownership").attr("disabled", false);
        $("#Permanent_Years").attr("disabled", false);
        $("#Permanent_Months").attr("disabled", false);
        $("#Permanent_Telephone").attr("disabled", false);
        $("#Permanent_City").attr("disabled", false);
        $("#Permanent_State").attr("disabled", false);
        $("#Permanent_Zipcode").attr("disabled", false);
        $("#Permanent_Country").attr("disabled", false);
    }
});

$(document).on('change', '#Present_City', function () {
    $('#Present_Barangay').attr('DISABLED', true);
    setTimeout(function () {
        $('#Present_Barangay').attr('DISABLED', false);
        loadpresentbrgy();
    }, 100);
});

$(document).on('change', '#Permanent_City', function () {
    $('#Permanent_Barangay').attr('DISABLED', true);
    setTimeout(function () {
        $('#Permanent_Barangay').attr('DISABLED', false);
        loadpermabrgy();
    }, 100);
});

$(document).on('change', '#Employer_City', function () {
    $('#Employer_Barangay').attr('DISABLED', true);
    setTimeout(function () {
        $('#Employer_Barangay').attr('DISABLED', false);
        loademployerbrgy();
    }, 100);
});

$(document).on('change', '#Present_Province', function () {
    $('#Present_City').attr('DISABLED', true);
    setTimeout(function () {
        $('#Present_City').attr('DISABLED', false);
        loadpresentcity();
    }, 100);
});

$(document).on('change', '#Permanent_Province', function () {
    $('#Permanent_City').attr('DISABLED', true);
    setTimeout(function () {
        $('#Permanent_City').attr('DISABLED', false);
        loadpermacity();
    }, 100);
});

$(document).on('change', '#Employer_Province', function () {
    $('#Employer_City').attr('DISABLED', true);
    setTimeout(function () {
        $('#Employer_City').attr('DISABLED', false);
        loademployercity();
    }, 100);
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
            elm2.empty();
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
            var res = jQuery.parseJSON(data);
            $.each(res, function (key, value) {
                elm.append(`<option value="${res[key].MappingId}"> ${res[key].Title}</option>`)
                elm2.append(`<option value="${res[key].MappingId}"> ${res[key].Title}</option>`)
                elm3.append(`<option value="${res[key].MappingId}"> ${res[key].Title}</option>`)
            });
            loadpermacity();
            loadpresentcity();
            loademployercity();
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
            var res = jQuery.parseJSON(data);
            $.each(res, function (key, value) {
                elm.append(`<option value="${res[key].MappingId}"> ${res[key].Title}</option>`)
            });
            loadpermabrgy();
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
            var res = jQuery.parseJSON(data);
            $.each(res, function (key, value) {
                elm.append(`<option value="${res[key].SysId}"> ${res[key].Title}</option>`)
            });
            $('#loadingmodal').modal('hide');
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
            var res = jQuery.parseJSON(data);
            $.each(res, function (key, value) {
                elm.append(`<option value="${res[key].MappingId}"> ${res[key].Title}</option>`)
            });
            loadpresentbrgy();
        },
        error: function (ex) {
            alert("Internal Server Error");
        }
    });
}

function loadpresentbrgy() {
    //$('#loadingmodal').modal('show');
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
            var res = jQuery.parseJSON(data);
            $.each(res, function (key, value) {
                elm.append(`<option value="${res[key].SysId}"> ${res[key].Title}</option>`)
            });
            $('#loadingmodal').modal('hide');
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
            var res = jQuery.parseJSON(data);
            $.each(res, function (key, value) {
                elm.append(`<option value="${res[key].MappingId}"> ${res[key].Title}</option>`)
            });
            loademployerbrgy();
        },
        error: function (ex) {
            alert("Internal Server Error");
        }
    });
}

function loademployerbrgy() {
    //$('#loadingmodal').modal('show');
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
            var res = jQuery.parseJSON(data);
            $.each(res, function (key, value) {
                elm.append(`<option value="${res[key].SysId}"> ${res[key].Title}</option>`)
            });
            $('#loadingmodal').modal('hide');
        },
        error: function (ex) {
            alert("Internal Server Error");
        }
    });
}

function loadpreview() {
    //$('#loadingmodal').modal('show');




    var today = new Date();
    var dd = String(today.getDate()).padStart(2, '0');
    var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
    var yyyy = today.getFullYear();

    today = mm + '/' + dd + '/' + yyyy;

    var loanamount = $("#loanAmount").val();
    var netincome = $("#netIncome").val();
    var loanterm = $("#loanTerms option:selected").text();
    var loanpurpose = $("#loanPurpose option:selected").text();
    var loanothers = $("#loanPurposeOthers").val();
    var fullclientname = $("#salutation option:selected").text() + " " + $("#firstName").val() + " " + $("#middleName").val() + " " + $("#lastName").val() + " " + $("#suffix").val();
    var birthplace = $("#birthPlace").val();
    var birthdate = $("#birthDate").val();
    var age = $("#age").val();
    var gender = $("#gender option:selected").text();
    var religion = $("#religion").val();
    var citizenship = $("#citizenship option:selected").text();
    var civilstatus = $("#civilStatus option:selected").text();
    var tin = $("#TIN").val();
    var sss = $("#SSS").val();
    var gsis = $("#GSIS").val();

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
    if (civilstatus == "Married" || civilstatus == "Living with Partner") {
        var sfullname = $("#spouseSalutation option:selected").text() + " "
            + $("#spouseFirstname").val() + " "
            + $("#spouseMiddlename").val() + " "
            + $("#spouseLastname").val() + " "
            + $("#spouseSuffix").val();
        var sgender = $("#spouseGender option:selected").text();
        var sage = $("#spouseAge").val();
        var sbirthdate = $("#spouseBirthDate").val();
        var dependents = $("#dependents").val();
        var employer = $("#spouseEmployer").val();
        var sMonthlyIncome = $("#spouseMonthlyIncome").val();
        var sNumber = $("#spouseNumber").val();
        var sOccupation = $("#spouseOccupation option:selected").text();
    } else {
        var sfullname = "";
        var sgender = "";
        var sage = "";
        var sbirthdate = "";
        var dependents = "";
        var employer = "";
        var sMonthlyIncome = "";
        var sNumber = "";
        var sOccupation = "";
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
        + $("#Present_Barangay option:selected").text() + " "
        + $("#Present_City option:selected").text() + ", "
        + $("#Present_Province option:selected").text() + " "
        + $("#Present_Country").val() + " "
        + $("#Present_Zipcode").val();
    var presentownership = $("#Present_Ownership option:selected").text()
    var presentlos = $("#Present_Years").val() + "YEARS " + $("#Present_Months").val() + "MONTHS.";
    var presenttel = $("#Present_Telephone").val();

    $("#dPresentAddress").html(presentaddress);
    $("#dPresentOwnership").html(presentownership);
    $("#dPresentLengthOfStay").html(presentlos);
    $("#dPresentTelephone").html(presenttel);

    var permanentaddress = $("#Permanent_Address").val() + " "
        + $("#Permanent_Barangay option:selected").text() + " "
        + $("#Permanent_City option:selected").text() + ", "
        + $("#Permanent_Province option:selected").text() + " "
        + $("#Permanent_Country").val() + " "
        + $("#Permanent_Zipcode").val();
    var permanentownership = $("#Permanent_Ownership option:selected").text()
    var permanentlos = $("#Permanent_Years").val() + "YEARS " + $("#Permanent_Months").val() + "MONTHS.";
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
    var natureofwork = $("#natureOfWork option:selected").text();
    var employeraddress = $("#Employer_Address").val() + " "
        + $("#Employer_Barangay option:selected").text() + " "
        + $("#Employer_City option:selected").text() + ", "
        + $("#Employer_Province option:selected").text() + " "
        + $("#Employer_Country").val() + " "
        + $("#Employer_Zipcode").val();
    var employertelephone = $("#Employer_Telephone").val();
    var occupation = $("#occupation option:selected").text();
    var monthlyallowance = $("#monthlyAllowance").val();
    var dSourceOFIncomeOthers = $("#sourceOfIncomeOthers").val();
    var dMonthlyIncomeOthers = $("#monthlyIncomeOthers").val();

    $("#dDateHired").html(datehired);
    $("#dTenure").html(tenure);
    $("#dEmployeeNumber").html(employeenumber);
    //$("#dPosition").html(position);
    $("#dRank").html(rank);
    $("#dDepartment").html(department);
    $("#dNatureOfWork").html(natureofwork);
    $("#dEmployerAddress").html(employeraddress);
    $("#deTelephone").html(employertelephone);
    $("#dMonthlyAllowance").html(monthlyallowance);
    $("#dOccupation").html(occupation);
    $("#dSourceOFIncomeOthers").html(dSourceOFIncomeOthers);
    $("#dMonthlyIncomeOthers").html(dMonthlyIncomeOthers);
    var creditOption = $("#creditOption").val();
    var credOption = $("#creditOption option:selected").text();
    var bankName = "";
    var nameToDisplay = "";
    var accountNumber = "";
    if (creditOption == "csbNew") {
        nameToDisplay = $("#nameToDisplay").val();
    } else if (creditOption == "csbOld") {
        console.log($("#oldCSBAccount"))
        accountNumber = $("#oldCSBAccount").val();
        console.log(accountNumber);
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
            console.log(data)
            $("span[name='dCIFNo']").html(data)
        },
        error: function (ex) {
            alert("Internal Server Error");
        }
    });

    $("span[name='dBranchCode']").html('')
    $("span[name='dDate']").html(today)
    $("span[name='dIDType']").html('')
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
    $("span[name='dsEmployer']").html('')
    $("span[name='dMotherMaidenName']").html('')
    $("span[name='dsMotherMaidenName']").html('')
    $("span[name='dsEmployerAddress']").html('')
    $("span[name='dsNatureofWork']").html('')
    $("span[name='dEmployerZipcode']").html($("#Employer_Zipcode").val())

    $("span[name='dFirstName']").html($("#firstName").val())
    $("span[name='dMiddleName']").html($("#middleName").val())
    $("span[name='dLastName']").html($("#lastName").val())
    $('#loadingmodal').modal('hide');
    $("#myModal").modal('show');
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
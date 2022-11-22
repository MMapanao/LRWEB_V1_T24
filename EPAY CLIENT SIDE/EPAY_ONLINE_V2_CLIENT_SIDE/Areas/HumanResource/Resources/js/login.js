
function awhdkawudjaw() {
    let unm = $("#txtemail").val();
    let ups = $("#txtpassword").val();

    if (unm == "") {
        alert('Username/Email is Required!');
    } else if (ups == "") {
        alert('Password is Required!');
    } else {
        var form = $('#__AjaxAntiForgeryForm');
        var aaff = $('input[name="__RequestVerificationToken"]', form).val();

        var jsonX = {
            unm: unm, ups: ups 
        };
        $.ajax({
            url: FolderName + "/HumanResource/Login/awkfawidjawkdvnkl",
            type: "POST",
            data:{ __RequestVerificationToken: aaff, jsonX:jsonX },
            success: function (data) {

                if (data == "1") {
                    otpView();
                }else if(data == "SC1"){
                    window.location = FolderName + "/HumanResource/Manage/Index?content=EnrollEmployee";
                }else {
                    alert(data);
                }
            }
        });
    }
}
//$(document).on('click', '#btnresend', function () {
    
//});

function ResentOTPEmail() {
    let unm = $("#txtemail").val();
    let ups = $("#txtpassword").val();
    
    if (unm == "") {
        alert('Username/Email is Required!');
    } else if (ups == "") {
        alert('Password is Required!');
    } else {
        var form = $('#__AjaxAntiForgeryForm');
        var aaff = $('input[name="__RequestVerificationToken"]', form).val();

        var jsonX = {
            unm: unm, ups: ups
        };
        $.ajax({
            url: FolderName + "/HumanResource/Login/OTPResendEmail",
            type: "POST",
            data: { __RequestVerificationToken: aaff, jsonX: jsonX },
            success: function (data) {
                if (data == "1") {

                    alert("Email Successfully Sent");
                } else {
                    alert(data);
                }
            }
        });
    }
}

var htmlotpval =$("#htmlotp").html();
function otpView() {
    $.confirm({
        title: '',
        columnClass:"small",
        content: htmlotpval,
        type: 'dark',
        typeAnimated: true,
        buttons: {
            tryAgain: {
                text: 'Submit',
                btnClass: 'btn-orange',
                action: function () {


                    let unm = $("#txtemail").val();
                    let ups = $("#txtpassword").val();

                    if (unm == "") {
                        alert('Username/Email is Required!');
                    } else if (ups == "") {
                        alert('Password is Required!');
                    } else {
                        var form = $('#__AjaxAntiForgeryForm');
                        var aaff = $('input[name="__RequestVerificationToken"]', form).val();
                        var otpcode = $("#txtotp").val();
                        var cb30daysischeck = $('#txt30days:checked').length > 0;
                        var jsonX = {
                            unm: unm, ups: ups, code: otpcode, otp: cb30daysischeck
                        };
                        $.ajax({
                            url: FolderName + "/HumanResource/Login/zxcValidate",
                            //dataType: "json",
                            type: "POST",
                            // contentType: 'application/json; charset=utf-8',
                            data: { __RequestVerificationToken: aaff, jsonX: jsonX },
                            //async: true,
                            //processData: false,
                            // cache: false,
                            success: function (data) {
                                if (data == "1") {
                                    window.location = FolderName + "/HumanResource/Manage/Index?content=EnrollEmployee";
                                } else { 
                                    alert(data);
                                }
                            }
                        });
                    }
                    return false;
                }
            },
            Resend: {
                text: '',
                btnClass: 'btn-orange',
                action: function () {
                    ResentOTPEmail();
                    return false;
                }
            },
            Cancel: {
                btnClass: 'btn-orange',
                action: function () {}
            }
        }
    });
    $("#htmlotp").html("");
}
var otpfocus = 0;
$(document).on('input', 'input[name="otp"]', function () {
 
    $("#" + (otpfocus + 1)).focus();

});

$(document).on('focus', 'input[name="otp"]', function () {
    otpfocus = parseInt(this.id);

});
$(document).on('click', '#btnlogin', function () {
    awhdkawudjaw();
});

$(document).on('keypress', 'input[name="txtinput"]', function (e) {
    var sds = e.which;
    if (sds == 13) {
        awhdkawudjaw();
    }

    
});


$(document).on('input', 'input', function () {
    var thisval = $(this).val();
    $(this).val(thisval.replace("'", ""));
});




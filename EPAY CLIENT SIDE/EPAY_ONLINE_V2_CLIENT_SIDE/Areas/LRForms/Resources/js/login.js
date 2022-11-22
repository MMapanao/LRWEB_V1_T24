function awhdkawudjaw() {
    let unm = $("#txtemail").val();
    let ups = $("#txtpassword").val();

    if (unm == "") {
        alert('Username/Email is Required!');
    } else if (ups == "") {
        alert('Password is Required!');
    } else {
        $.ajax({
            url: FolderName + "/HumanResource/Login/awkfawidjawkdvnkl",
            dataType: "json",
            type: "POST",
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify({ unm: unm, ups: ups }),
            async: true,
            processData: false,
            cache: false,
            success: function (data) {

                if (data == "Login Successful!") {
                    //msgbox("Login", data, "dark");
                    window.location = FolderName + "/HumanResource/Manage/Index?content=EnrollEmployee";
                } else {
                   // msgbox("Login", data, "red");
                }
            }
        });
    }
}

$(document).on('click', '#btnlogin', function () {
    awhdkawudjaw();
});

$(document).on('keypress', 'input[name="txtinput"]', function (e) {
    var sds = e.which;
    if (sds == 13) {
        awhdkawudjaw();
    }
});

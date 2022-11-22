$(document).ready(function () {
    $("#loadingmodal").show();
    cleanNewClient();
    loadsol();
    loaddropdownprovince();
    
    $("#province").change(function () {
        loaddropdowncity();
    });

    $("#city").change(function () {
        loaddropdownbrgy();
    });

    $("#groupCode").change(function () {
        loadgroupcodeinfo();
    });



    function loaddropdownprovince() {
        var province = $("#province")
        $.ajax({
            url: FolderName + "/Admin/EnrollClients/getProvince",
            dataType: "json",
            type: "GET",
            contentType: 'application/json; charset=utf-8',
            async: true,
            processData: false,
            cache: false,
            success: function (data) {
                province.empty();
                province.append(`<option disabled selected value="">--Please select--</option>`)
                var res = jQuery.parseJSON(data);
                $.each(res, function (key, value) {
                    province.append(`<option value="${res[key].MappingId}"> ${res[key].Title}</option>`)
                });
                //$("#city").empty();
                //loaddropdowncity()
                $("#loadingmodal").hide();
            },
            error: function (ex) {
                alert("Internal Server Error");
            }
        });
    }

    function loaddropdowncity() {
        $("#loadingmodal").show();
        var city = $("#city")
        var barangay = $("#barangay")
        var provinceId = $("#province").val();
        barangay.empty();
        barangay.append(`<option disabled selected value="">--Please select--</option>`)


        $.ajax({
            url: FolderName + "/Admin/EnrollClients/getCity",
            dataType: "json",
            type: "POST",
            contentType: 'application/json; charset=utf-8',
            async: true,
            processData: false,
            data: JSON.stringify({ provinceId: provinceId }),
            cache: false,
            success: function (data) {
                city.empty();
                city.append(`<option disabled selected value="">--Please select--</option>`)
                var res = jQuery.parseJSON(data);
                $.each(res, function (key, value) {
                    city.append(`<option value="${res[key].MappingId}"> ${res[key].Title}</option>`)
                });
                //$("#barangay").empty();
                //loaddropdownbrgy();
                setTimeout(function () {
                    $("#loadingmodal").hide();
                }, 1000);
            },
            error: function (ex) {
                alert("Internal Server Error");
            }
        });
    }

    function loaddropdownbrgy() {
        $("#loadingmodal").show();
        var barangay = $("#barangay")
        var cityId = $("#city").val();

        $.ajax({
            url: FolderName + "/Admin/EnrollClients/getBarangay",
            dataType: "json",
            type: "POST",
            contentType: 'application/json; charset=utf-8',
            async: true,
            processData: false,
            data: JSON.stringify({ cityId: cityId }),
            cache: false,
            success: function (data) {
                barangay.empty();
                barangay.append(`<option disabled selected value="">--Please select--</option>`)
                var res = jQuery.parseJSON(data);
                $.each(res, function (key, value) {
                    barangay.append(`<option value="${res[key].SysId}"> ${res[key].Title}</option>`)
                });
                setTimeout(function () {
                    $("#loadingmodal").hide();
                }, 1000);
            },
            error: function (ex) {
                alert("Internal Server Error");
            }
        });
    }

    function loadsol() {
        var csb = $("#csbBranch")
        $.ajax({
            url: FolderName + "/Admin/EnrollClients/getSol",
            dataType: "json",
            type: "GET",
            contentType: 'application/json; charset=utf-8',
            async: true,
            processData: false,
            cache: false,
            success: function (data) {
                csb.empty();
                csb.append(`<option disabled selected value="">--Please select--</option>`)
                var res = jQuery.parseJSON(data);
                $.each(res, function (key, value) {
                    csb.append(`<option value="${res[key].sol_id}-${res[key].finacle_sol_id}"> ${res[key].branch_name}</option>`)
                });

            },
            error: function (ex) {
                alert("Internal Server Error");
            }
        });
    }

    //function loadgroupcode() {
    //    var groupcode = $("#groupCode")
    //    $.ajax({
    //        url: FolderName + "/Admin/EnrollClients/getCompany",
    //        dataType: "json",
    //        type: "GET",
    //        contentType: 'application/json; charset=utf-8',
    //        async: true,
    //        processData: false,
    //        cache: false,
    //        success: function (data) {
    //            $("#groupCode").empty();
    //            groupcode.append(`<option value="">Select Group Code</option>`)
    //            var res = jQuery.parseJSON(data);
    //            $.each(res, function (key, value) {
    //                groupcode.append(`<option value="${res[key].sys_id}"> ${res[key].group_code}</option>`)
    //            });
    //        },
    //        error: function (ex) {
    //            alert("Internal Server Error");
    //        }
    //    });
    //}

    function loadgroupcodeinfo() {
        var agencyCode = $("#groupCode option:selected").val();
        $.ajax({
            url: FolderName + "/Admin/EnrollClients/getCompanyDetails",
            dataType: "json",
            type: "POST",
            contentType: 'application/json; charset=utf-8',
            async: true,
            processData: false,
            data: JSON.stringify({ agencyCode: agencyCode }),
            cache: false,
            success: function (data) {
                if (data.length != 0) {
                    $("#clientName").val(data[0].agency_name);
                    $("#schemeCode").val(data[0].scheme_code);
                } else {
                    $("#clientName").val("");
                    $("#schemeCode").val("");
                }
                
            },
            error: function (ex) {
                alert("Internal Server Error");
            }
        });
    }
    $("#loadingmodal").hide();
});


function cleanNewClient() {
    $('small[name="txtmessage"]').css('visibility', 'hidden');

}

//$(document).on('change', '#GroupCode', function () {
 
//    $.ajax({
//        url: FolderName + "/Admin/EnrollClients/GetRMCode",
//        dataType: "json",
//        type: "POST",
//        contentType: 'application/json; charset=utf-8',
//        async: true,
//        processData: false,
//        data: JSON.stringify({ grpcode: $("#GroupCode").val() }),
//        cache: false,
//        success: function (data) {
//            $("#RMCode").html(data);
    
//        },
//        error: function (ex) {
//            alert(ex);
//        }
//    });

//});

//$(document).on('change', '#ZipCode', function () {
//    var selval = $("#ZipCode").val();
//    var getloc = "zipcode" + selval;
//    var cityloc = $("#" + getloc).attr('name');
//    $("#CityProvince").val(cityloc.substr(0, cityloc.indexOf(",")));
//    $("#DistrictBarangay").val(cityloc.split(",").pop());
//});



$(document).on('input', 'input[name="newclient"]', function () {
    var thisid = this.id;
    if (thisid != "emailAddress") {
        $(this).val($(this).val().replace(/[^\w\s]/gi, ''));
    }
});

$(document).on('focusout', 'input[name="newclient"]', function () {
    var thisid = this.id;
    if (thisid != "emailAddress") {
        $(this).val($(this).val().toUpperCase());
    }
});

$(document).on('click', '#btncheckgroupcode', function () {
    ValidateGroupCode();
});
$(document).on('focusout', '#groupCode', function () {
    ValidateGroupCode();
});
function ValidateGroupCode() {
    $.ajax({
        url: FolderName + "/Admin/EnrollClients/ValidateGroupCode",
        dataType: "json",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify({ model_state: $("#groupCode option:selected").html() }),
        async: true,
        cache: false,
        success: function (data) {
            if (data != "") {
                ClearAllValidate();
                $(data).each(function (index, item) {
                    if (item.error_message != null) {
                        $("#" + item.content_id).css('border-color', '#D32F2F');
                        $("#" + item.content_id + "_message").css('visibility', 'visible');
                        $("#" + item.content_id + "_message").removeClass("text-success").addClass("text-danger");
                        $("#" + item.content_id + "_message").text(item.error_message);
                    }
                });
            } else {
                ClearAllValidate();
                $("#GroupCode").css("border-color", '#28a745');
                $("#GroupCode_message").removeClass("text-danger").addClass("text-success");
                $("#GroupCode_message").text("VALID GROUP CODE");
                $("#GroupCode_message").css('visibility', 'visible');
            }
        },
        error: function (ex) {
            msgbox("Error", ex, "red");

        }
    });
}
//$(document).on('click', '#btnsubmit', function () {
    //  $('input[name="newclient"]').css("border-color", "#dd2c00");
$("#regForm").submit(function (e) {
    e.preventDefault();
    $("#loadingmodal").show();
    $.confirm({
        title: '<span class="glyphicon glyphicon-question-sign" aria-hidden="true"></span> CREATE NEW GROUP CODE',
        content: "Are you sure you want to submit new group code?",
        type: "green",
        typeAnimated: true,
        buttons: {
            tryAgmnain: {
                text: 'Yes',
                btnClass: "btn-orange",
                action: function () {
                    var model_state = JModel_EnrollClients();
                    $.ajax({
                        url: FolderName + "/Admin/EnrollClients/InsertNewClient",
                        dataType: "json",
                        type: "POST",
                        data: model_state,
                        async: true, 
                        cache: false,
                        success: function (data) {
                            setTimeout(function () {
                                $("#loadingmodal").hide();
                            }, 1000)
                            MessageResults(data);
                        },
                        error: function (ex) {

                            msgbox("Error", ex, "red");

                        }
                    });

                }
            },
            No: {
                text: 'No',
                btnClass: "btn-orange",
                action: function () {

                }
            }
        }
    });
});



function MessageResults(params) {
    if (params == "") {

        swal({
            title: 'Saved Group Code Data',
            text: "Successfully Submitted Group Code.",
            type: 'success',
            confirmButtonColor: '#4f2683',
            confirmButtonText: 'Okay'
        }).then((result) => {
            if (result.value) {
                window.location = FolderName + "/Admin/EnrollClients/Index";
            }
        })
        setTimeout(
        window.location = FolderName + "/Admin/EnrollClients/Index?content=EnrollClient"
        , 3500);
    } else {
        ClearAllValidate(); 
        $(params).each(function (index, item) {
            if (item.error_message != null) {
                $("#" + item.content_id).css('border-color', '#D32F2F');
                $("#" + item.content_id + "_message").css('visibility', 'visible');
                $("#" + item.content_id + "_message").text(item.error_message);
            }
        });
    }
}

function ClearAllValidate() {
    $("input").css("border-color", "");
    $("small").css("visibility", "hidden");
    $("select").css("border-color", "");
}
 
function JModel_EnrollClients() {
    //var cardtype = "";
    //var schemcode_array = [];
    ////cardtype = $("#cardtype").val() + "|" + $("#txtcardtypeSB151").val() + "|" + $("#txtcardtypeSB157").val();
 
 
    //$('input[name="cboxschemecode"]').each(function () {
    //    if ($(this).prop('checked') == true) {
    //        schemcode_array.push($(this).val());
    //    }
    //});

    //var CardDeliverySol_array = [];
    //$('td[name="carddelsollist"]').each(function () {
    //    CardDeliverySol_array.push($(this).text());
    //});
    var modelstate = {
        ID: "N/A",
        groupCode: $("#groupCode option:selected").html(),
        agencyCode: $("#groupCode option:selected").val(),
        creditRatio: $("#creditRatio").val(),
        schemeCode: $("#schemeCode").val(),
        clientName: $("#clientName").val(),
        csbBranch: $("#csbBranch option:selected").html(),
        sol_id: $("#csbBranch").val().split("-")[0],
        finacle_sol_id: $("#csbBranch").val().split("-")[1],
        AED: $("#AED").val(),
        addressLine: $("#addressLine").val(),
        barangayId: $("#barangay").val(),
        cityId: $("#city").val(),
        provinceId: $("#province").val(),
        barangay: $("#barangay option:selected").html(),
        city: $("#city option:selected").html(),
        province: $("#province option:selected").html(),
        mobileNumber: $("#mobileNumber").val(),
        officeNumber: $("#officeNumber").val(),
        emailAddress: $("#emailAddress").val(),
        emailFormat: $("#emailFormat").val(),
        classification: $("#classification").val(),
        structure: $("#structure").val(),
        accountNumber: $("#accountNumber").val(),
        customerID: $("#customerID").val(),
        status: "PENDING"
    };

 
    return modelstate;
}
 
function clearSideButton() {
    $('a[name="btnside"]').removeClass('btnsidebar-active');
}

$(document).on('click', '#btnlogout', function () {
    $.confirm({
        title: "LOG OUT",
        content: "Are you sure you want to log out?",
        type: "blue",
        typeAnimated: true,
        buttons: {
            tryAgain: {
                text: 'LOG OUT',
                btnClass: "btn-orange",
                action: function () {
                    $.ajax({
                        url: FolderName + "/Admin/SuperAdmin/LOGOUT",
                        dataType: "json",
                        type: "POST",
                        contentType: 'application/json; charset=utf-8',
                        async: true,
                        cache: false,
                        success: function (data) {
                            $.alert({
                                title: "",
                                content: 'Log out successful.',
                                onClose: function () {
                                    window.location = FolderName + "/Admin/Login";
                                },
                            })       
                        },
                        error: function (ex) {
                            console.log(ex)

                        }
                    });
                }
            },
            cancel: {
                text: 'Cancel',
                btnClass: "btn-orange",
                action: function () {

                }
            }
        }
    });
});



$(document).ready(function () {
    if (getUrlParameter('content').toString() != "") {
        $("a[name='btnside']").css('background-color','');
        $("#" + getUrlParameter("content")).css('background-color', "#FAA634");
        $("#" + getUrlParameter("content")).css('color', "#FFF");
    }
});
function getUrlParameter(name) {
    name = name.replace(/[\[]/, '\\[').replace(/[\]]/, '\\]');
    var regex = new RegExp('[\\?&]' + name + '=([^&#]*)');
    var results = regex.exec(location.search);
    return results === null ? '' : decodeURIComponent(results[1].replace(/\+/g, ' '));
};
 
function msgbox(title, msg, type) {
    $.confirm({
        title: title,
        content: msg,
        type: type,
        typeAnimated: true,
        buttons: {
            tryAgain: {
                text: 'Close',
                btnClass: "btn-orange",
                action: function () {

                }
            }
        }
    });
}
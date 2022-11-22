 
function clearSideButton() {
    $('a[name="btnside"]').removeClass('btnsidebar-active');
}

$(document).on('click', '#btnlogout', function () {
    $.confirm({
        title: "Confirmation",
        content: "Do you really want to logout?",
        type: "blue",
        typeAnimated: true,
        buttons: {
            tryAgain: {
                text: 'Log out',
                btnClass: "btn-" + "red",
                action: function () {
         
                    window.location = FolderName + "/Admin/Login/Index";
                }
            },
            cancel: {
                text: 'Cancel',
                btnClass: "btn-" + "blue",
                action: function () {

                }
            }
        }
    });
});



$(document).ready(function () {
    if (getUrlParameter('content').toString() != "") {
        $("a[name='btnside']").css('background-color','');
        $("#" + getUrlParameter("content")).css('background-color', "#2e2e2e");
    }
});
function getUrlParameter(name) {
    name = name.replace(/[\[]/, '\\[').replace(/[\]]/, '\\]');
    var regex = new RegExp('[\\?&]' + name + '=([^&#]*)');
    var results = regex.exec(location.search);
    return results === null ? '' : decodeURIComponent(results[1].replace(/\+/g, ' '));
};
 
//function msgbox(title, msg, type) {
//    $.confirm({
//        title: title,
//        content: msg,
//        type: type,
//        typeAnimated: true,
//        buttons: {
//            tryAgain: {
//                text: 'Close',
//                btnClass: "btn-" + type,
//                action: function () {

//                }
//            }
//        }
//    });
//}
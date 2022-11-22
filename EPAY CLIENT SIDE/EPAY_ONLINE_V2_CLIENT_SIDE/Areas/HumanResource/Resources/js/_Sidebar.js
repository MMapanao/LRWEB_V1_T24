 
function clearSideButton() {
    $('a[name="btnside"]').removeClass('btnsidebar-active');
}

$(document).on('click', '#btnlogout', function () {
    var x = confirm("Are you sure you want to log out?")
    if (x == true) {
        $(location).attr('href', FolderName + "/HumanResource/Login/Index");
    }
    //$.confirm({
    //    title: "Log out",
    //    content: "Are you sure?",
    //    type: "blue",
    //    typeAnimated: true,
    //    buttons: {
    //        tryAgain: {
    //            text: 'Yes',
    //            btnClass: "btn-" + "orange",
    //            action: function () {
    //                $(location).attr('href', FolderName + "/HumanResource/Login/Index");
    //            }
    //        },
    //        cancel: {
    //            text: 'No',
    //            btnClass: "btn-" + "orange",
    //            action: function () {

    //            }
    //        }
    //    }
    //});
});



$(document).ready(function () {
    if (getUrlParameter('content').toString() != "") {
        $("a[name='btnside']").css('background-color', '').css('color','#4f2683');
        $("#" + getUrlParameter("content")).css('background-color', "#faa634").css('color', '#fff');
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
$(document).on('click', 'a[name="btndelete"]', function () {
    var thisid = this.id;
    $.confirm({
        title: '<i class="fa fa-trash"></i> Delete',
        content: "Are you sure?",
        type: "red",
        typeAnimated: true,
        buttons: {
            tryAgmnain: {
                text: 'Yes',
                btnClass: "btn-red",
                action: function () {
                    $("#loadingmodal").show();
                    $.ajax({
                        url: FolderName + "/Admin/EnrollClients/DeleteClient",
                        dataType: "json",
                        type: "POST",
                         contentType: 'application/json; charset=utf-8',
                        data: JSON.stringify({ thisid: thisid }),
                        async: true,
                        cache: false,
                        processData: false,
                        success: function (data) {
                            GetData($("#txtsearch").val());

                            swal({
                                title: 'Delete',
                                text: "Successfully Delete Client",
                                type: 'Success',
                                showCancelButton: true,
                                confirmButtonColor: '#3085d6',
                                cancelButtonColor: '#d33',
                                confirmButtonText: 'Yes, delete it!'
                            }).then((result) => {
                                if (result.value) {
                                     
                                }
                            })

                        },
                        error: function (ex) {
                            msgbox("Error", ex, "red");

                        }
                    });

                }
            },
            No: {
                text: 'No',
                btnClass: "btn-gray",
                action: function () {

                }
            }
        }
    });
});

$(document).on('input', '#txtsearch', function () {
    GetData($(this).val());
});

var count_row = 0;
function GetData(txtsearch) {
    $("#loadingmodal").show();
    $.ajax({
        url: FolderName + "/Admin/EnrollClients/SearchClient",
        dataType: "json",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify({ txtsearch: txtsearch }),
        async: true,
        cache: false,
        processData: false,
        success: function (data) {
    
            var sbj_list = "";
            $.each(data, function (idx, obj) {
                var xxxs = "";
                if (obj.status == "PENDING") {
                    xxxs = 'style="color:#ff6a00"';
                } else if (obj.status == "RETURNED") {
                    xxxs = 'style="color:#607D8B"';
                } else if (obj.status == "VERIFIED") {
                    xxxs = 'style="color:#0094ff"';
                }
                var ddd = "";
                if (obj.RMCode != null) {
                    ddd = obj.RMCode;
                }
                sbj_list = sbj_list +
                    '<tr class="" name="rowclients" >' +
                        '<td>' + obj.AccountNo + '</td>' +
                        '<td>' + obj.GroupCode + '</td>' +
                        '<td>' + obj.CompanyName + '</td>' +
                        '<td>' + obj.CustomerID + '</td>' +
                      //  '<td>' + ddd + '</td>' +
                        '<td>' + obj.branches + '</td>' +
                        '<td><b ' + xxxs + '>' + obj.status + '</b></td>' +
                        '<td style="text-align:center;">'+
                                    '<a href="' + FolderName + '/Admin/Branches/Index?content=EnrollClient&GroupCode=' + obj.GroupCode + '"><i class="fa fa-table"></i> Branches</a> &nbsp;' +
                                    '<a href="' + FolderName + '/Admin/EnrollClients/UpdateGroupcode?GroupCode=' + obj.GroupCode + '"><i class="fa fa-pencil"></i> Edit</a> &nbsp;' +
                                    //'<a href="#" id="'+ obj.IntRecord + '" name="btndelete"><i class="fa fa-trash"></i> Delete</a>'+
                       '</td>'+
                    '</tr>';
            });
 
            $("#tblclientid").html(sbj_list);
            setTimeout(function () {
                $("#loadingmodal").hide();
            }, 1000)
        }
    });
}
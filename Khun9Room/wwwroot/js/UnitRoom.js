var dataTable;

$(document).ready(function () {
    loadDataTable();
});


function loadDataTable() {
    dataTable = $('#tblDataUnitList').DataTable({
        "ajax": {
            "url": "/User/Room/GetUnitList"
        },
        "columns": [
            { "data": "unitNo", "width": "15%" },
            { "data": "unitPrefix", "width": "15%" },
            {
                "data": "unitNumberId",
                "render": function (data) {
                    return `
                            <div class="text-center">
                                <a href="/user/room/create/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                    <i class="fas fa-edit"></i> 
                                </a>
                                <a onclick=Delete("/User/Room/DeleteUnit/${data}") class="btn btn-danger text-white" style="cursor:pointer">
                                    <i class="fas fa-trash-alt"></i> 
                                </a>
                            </div>
                           `;
                }, "width": "20%"
            }
        ]
    });
}




function Delete(url) {
    swal({
        title: "ยืนยันการลบ",
        text: "หากลบเเล้วข้อมูลจะทำการกู้คืนไม่ได้",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}
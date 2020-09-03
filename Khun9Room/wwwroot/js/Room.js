var dataTable;

$(document).ready(function () {
    loadDataTable();
});


function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/User/Room/GetAll"
        },
        "columns": [
            { "data": "unitNumber", "width": "15%" },
            { "data": "tenant.fullName", "width": "15%" },
            { "data": "paymentStatus", "width": "15%" },
            { "data": "nextPayDate", "width": "15%" },
            {
                "data": "roomNumber",
                "render": function (data) {
                    return `
                            <div class="text-center">
                                <a href="/user/room/create/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                    <i class="fas fa-edit"></i> 
                                </a>
                                <a onclick=Delete("/User/Room/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer">
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
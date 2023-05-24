var dataTable;



$(document).ready(function () {


    loadDataTable();

})



function loadDataTable() {

    dataTable = $("#tblData").DataTable({

        "ajax": {

            "url": "/Admin/Product/getall"

        },

        "columns": [

            { "data": "title", "width": "25%" },

            { "data": "isbn", "width": "15%" },

            { "data": "price", "width": "10%" },

            { "data": "author", "width": "15%" },

            { "data": "category.name", "width": "10%" },

            {
                "data": "id",
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                            <a href="/Admin/Product/Upsert?id=${data}" class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i> Edit</a>
                            <a onClick=Delete('/Admin/Product/Delete/${data}') class="btn btn-danger mx-2"> <i class="bi bi-trash-fill"></i> Delete</a>
					    </div>
                        `
                },
                "width": "25%"
            }
        ]
    });
}

//sweet Alert2 (2) 
function Delete(url) {
    Swal.fire({
        title: 'คุณแน่ใจหรือไม่?',
        text: "หลังจากลบข้อมูลคุณจะเรียกไม่ได้!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#d33',
        cancelButtonColor: '#3A3B3C',
        confirmButtonText: 'ใช่ , ลบมันเลย!',
        cancelButtonText: 'ยกเลิกการลบข้อมูล'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload();
                        Swal.fire({
                            title: 'ลบข้อมูลสำเร็จ',
                            text: "ข้อมูลได้ถูกลบแล้ว!",
                            icon: 'success',
                            confirmButtonColor: '#3A3B3C',
                            confirmButtonText: 'รับทราบ'

                        })
                        //toastr.success(data.message);
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            })
        }
    })
}
var dataTable;



$(document).ready(function () {


    loadDataTable();

})



function loadDataTable() {

    dataTable = $("#tblData").DataTable({

        "ajax": {

            "url": "/Admin/company/getall"

        },

        "columns": [

            { "data": "name", "width": "20%" },

            { "data": "streetAddress", "width": "10%" },

            { "data": "city", "width": "10%" },

            { "data": "state", "width": "15%" },

            { "data": "postalCode", "width": "15%" },

            { "data": "phoneNumber", "width": "10%" },

            {
                "data": "id",
                "render": function (data) {
                    return `<div class=" d-flex btn-group" role="group">
                            <a href="/Admin/company/Upsert?id=${data}" class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i>แก้ไข</a>
                            <a onClick=Delete('/Admin/company/Delete/${data}') class="btn btn-danger mx-2"><i class="bi bi-trash-fill"></i>ลบข้อมูล</a>
					    </div>
                        `
                }
                ,"width": "30%"
            }
        ]
    });
}

//sweet Alert2 (2) 
function Delete(url) {
    Swal.fire({
        title: 'คุณแน่ใจหรือไม่?',
        text: "หลังจากลบข้อมูลคุณจะเรียกกลับคืนมาไม่ได้!",
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
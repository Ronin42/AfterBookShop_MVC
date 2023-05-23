$(document).ready(function () {
    loadDataTable();
});

function loadDataTable()
{
    dataTable = $('#tblData').DataTable(
    {
        'ajax' : url:'/admin/product/getall'
    },
        "columns": [
        { data: '' },
        { data: '' },
        { data: '' },
        { data: '' },
        { data: '' },
        { data: '' } )
}

$('#myTable').DataTable({
    ajax: '/api/myData'
});

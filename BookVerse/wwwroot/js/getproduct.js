var table;
function DeleteProduct(url) {


    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {

            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (response) {
                    debugger
                    if (response.success) {
                        table.ajax.reload();
                        toastr.success(response.message);
                    }
                }

            })

            
        }
    })
}


function LoadProduct()
{

    table = $('#ProductTable').DataTable({
        "processing": true,
        "serverSide": true,
        "ajax": {
            "url": '/Admin/Product/GetProductData',
            "datatype": 'json',
            "type":'GET',
            "dataSrc": '',
        },
        lengthMenu: [[5, 10, 25, -1], [5, 10, 25, "All"]],



        "columns": [
            {data: 'title',"width":"15%"},
            { data: 'author',"width":"15%"},
            { data: 'isbn',"width":"10%"},
            { data: 'listPrice',"width":"15%"},
            { data: 'category.name', "width": "15%" },
            {
                data: 'id',
                render: function (data) {
                    return `<div class="w-75 btn-group" role="group">
                            <a asp-controller="Product" asp-action="Upsert" href="/Admin/Product/Upsert?id=${data}" class="btn btn-info mx-2">
                            <i class="bi bi-pencil-square"></i>Edit
                            </a>
                            <a asp-controller="Product" asp-action="Delete" onClick=DeleteProduct('/Admin/Product/DeleteProductAPI/${data}') class="btn btn-danger mx-2">
                            <i class="bi bi-trash3"></i>Delete
                            </a>
                            </div>`
                },
                "width": "20%"
            }
        ]





});

}




$(document).ready(() => {
    //debugger
    LoadProduct();
})
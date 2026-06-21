$(function () {
    loadDataTable();
});

let dataTable;

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        ajax: {
            url: '/admin/product/getall',
            type: 'GET',
            datatype: 'json'
        },
        columns: [
            { data: 'title', width: '25%' },
            { data: 'isbn', width: '15%' },
            { data: 'listPrice', width: '10%' },
            { data: 'author', width: '25%' },
            { data: 'category.name', width: '15%' }
        ]
    });
}
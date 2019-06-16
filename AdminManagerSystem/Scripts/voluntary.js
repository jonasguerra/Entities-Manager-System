$(document).ready( function () {
    update_table_voluntary()
});

function update_table_voluntary(){

    $('[data-toggle="tooltip"]').tooltip('update')
    
    if ($.fn.dataTable.isDataTable("#voluntary table")){
        $('#voluntary table').DataTable().destroy()
    }

    $('#voluntary table').DataTable({
        "lengthChange": false,
        "info": false,
        responsive: true,
        aaSorting: [[1, 'asc']],
        "pageLength": 5,
        "columnDefs": [
            { "targets": [0,2], "orderable": false }
        ],
        language: {
            emptyTable: "Registro vazio",
            infoEmpty: "Exibindo 0 a 0 de 0 registros",
            infoFiltered: "(filtrado de _MAX_ registros)",
            lengthMenu: "Exibir _MENU_ registros",
            loadingRecords: "Carregando...",
            processing: "Processando...",
            search: "_INPUT_",
            searchPlaceholder: "Pesquisar...",
            zeroRecords: "Nenhum registro encontrado",
            paginate: {
                first: "Primeira",
                last: "Última",
                next: "Próxima",
                previous: "Anterior"
            }
        },
        "fnDrawCallback": function(oSettings) {
            if (oSettings._iDisplayLength >= oSettings.fnRecordsDisplay()) {
                $(oSettings.nTableWrapper).find('.dataTables_paginate').hide();
            }
        }
    });

    $('#voluntary table:first').DataTable().on( 'order.dt search.dt', function () {
        $('#voluntary table:first').DataTable().column(0, {search:'applied', order:'applied'}).nodes().each( function (cell, i) {
            cell.innerHTML = i+1;
        } );
    } ).draw();

    $('#voluntary table:last').DataTable().on( 'order.dt search.dt', function () {
        $('#voluntary table:last').DataTable().column(0, {search:'applied', order:'applied'}).nodes().each( function (cell, i) {
            cell.innerHTML = i+1;
        } );
    } ).draw();
}



$('#voluntary').on('click', '.approve', function () {
    let id = $(this).closest('tr').attr('data-voluntary-id');
    $.ajax({
        url: $('#approve_voluntary_url').val(),
        method: 'POST',
        data: {
            'id': id,
        },
        success: function (data) {
            if (data.status == 'success') {
                new PNotify({
                    title: data.message_title,
                    text: '',
                    delay: 1000,
                    type: 'success',
                });
                
                
                //Este if deve ser executado antes de alterar a tabela
                if ($.fn.dataTable.isDataTable("#voluntary table")){
                    $('#voluntary table').DataTable().destroy()
                }

                let name = $('tr[data-voluntary-id="' + id + '"] .name').text();
                $('tr[data-voluntary-id="' + id + '"]').remove();
                append_voluntary_approved(id, name);

                update_table_voluntary()


            } else if (data.status == 'error') {
                new PNotify({
                    title: data.message_title,
                    text: '',
                    delay: 1000,
                    type: 'error',
                });
            }
        },
        error: function (e) {
            console.error('ERROR AJAX:', e)
        },
    })
})

$('#voluntary').on('click', '.show_more', function () {
    let id = $(this).closest('tr').attr('data-voluntary-id')
    $.ajax({
        url: $('#show_more_voluntary_url').val(),
        method: 'POST',
        data: {
            'id': id,
        },
        success: function (data) {
            if (data.status == 'success') {
                

            } else if (data.status == 'error') {
                new PNotify({
                    title: data.message_title,
                    text: '',
                    delay: 1000,
                    type: 'error',
                });
            }
        },
        error: function (e) {
            console.error('ERROR AJAX:', e)
        },
    })
})

$('#voluntary').on('click', '.trash', function () {
    let id = $(this).closest('tr').attr('data-voluntary-id');
    $.ajax({
        url: $('#trash_voluntary_url').val(),
        method: 'POST',
        data: {
            'id': id,
        },
        success: function (data) {
            if (data.status == 'success') {
                new PNotify({
                    title: data.message_title,
                    text: '',
                    delay: 1000,
                    type: 'success',
                });

                //Este if deve ser executado antes de alterar a tabela
                if ($.fn.dataTable.isDataTable("#voluntary table")){
                    $('#voluntary table').DataTable().destroy()
                }

                $('tr[data-voluntary-id="' + id + '"]').remove();

                update_table_voluntary();
    
            } else if (data.status == 'error') {
                new PNotify({
                    title: data.message_title,
                    text: '',
                    delay: 1000,
                    type: 'error',
                });
            }
        },
        error: function (e) {
            console.error('ERROR AJAX:', e)
        },
    })
})


function append_voluntary_approved(id, name) {
    
    $('#tbody_voluntary_approved').append('\
                                       <tr data-voluntary-id="'+ id +'">\n' +
                                '            <td></td>\n' +
                                '            <td class="name">'+ name +'</td>\n' +
                                '            <td class="actions">\n' +
                                '                <a href="javascript:;" class="show_more" data-placement="top" data-toggle="tooltip" data-original-title="Ver mais">\n' +
                                '                    <i class="os-icon os-icon-search"></i>\n' +
                                '                </a>\n' +
                                '                <a href="javascript:;" class="trash" data-placement="top" data-toggle="tooltip" data-original-title="Excluir">\n' +
                                '                    <i class="os-icon os-icon-ui-15"></i>\n' +
                                '                </a>\n' +
                                '            </td>\n' +
                                '        </tr>');


}
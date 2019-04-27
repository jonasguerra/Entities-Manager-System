$(document).ready( function () {
    update_table_affinity()
} );


function update_table_affinity(){

    if ($.fn.dataTable.isDataTable("#affinity table")){
        $('#affinity table').DataTable().destroy()
    }

    $('#affinity table').DataTable({
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

    $('#affinity table:first').DataTable().on( 'order.dt search.dt', function () {
        $('#affinity table:first').DataTable().column(0, {search:'applied', order:'applied'}).nodes().each( function (cell, i) {
            cell.innerHTML = i+1;
        } );
    } ).draw();

    $('#affinity table:last').DataTable().on( 'order.dt search.dt', function () {
        $('#affinity table:last').DataTable().column(0, {search:'applied', order:'applied'}).nodes().each( function (cell, i) {
            cell.innerHTML = i+1;
        } );
    } ).draw();
}


$('#affinity').on('click', '.approve', function () {
    let id = $(this).closest('tr').attr('data-affinity-id')
    $.ajax({
        url: $('#approve_affinity_url').val(),
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

                if ($.fn.dataTable.isDataTable("#affinity table")){
                    $('#affinity table').DataTable().destroy()
                }

                let name = $('tr[data-affinity-id="' + id + '"] .name').text();
                $('tr[data-affinity-id="' + id + '"]').remove();
                append_affinity_approved(id, name);

                update_table_affinity()
                
                
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


// Esta função deve ser chamada no evento de submit do formulário da affinidade
$('#affinity').on('click', '.edit', function () {
    
    let id = $(this).closest('tr').attr('data-affinity-id')
    // let name = "AQUI RECEBE O NOVO NOME DA AFINIDADE"
    
    $.ajax({
        url: $('#edit_affinity_url').val(),
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

                //Altera os dados na tablela
                // $('tr[data-affinity-id="' + id + '"] .name').text(name)
                update_table_affinity()
                
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

$('#affinity').on('click', '.trash', function () {
    let id = $(this).closest('tr').attr('data-affinity-id')
    $.ajax({
        url: $('#trash_affinity_url').val(),
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

                if ($.fn.dataTable.isDataTable("#affinity table")){
                    $('#affinity table').DataTable().destroy()
                }
                $('tr[data-affinity-id="' + id + '"]').remove();

                update_table_affinity()
                
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


function append_affinity_approved(id, name) {
    
    $('#tbody_affinity_approved').prepend('\
                                       <tr data-affinity-id="'+ id +'">\n' +
                                '            <td></td>\n' +
                                '            <td class="name">'+ name +'</td>\n' +
                                '            <td class="actions">\n' +
                                '                <a href="javascript:;" class="edit"data-placement="top" data-toggle="tooltip" data-original-title="Editar">\n' +
                                '                    <i class="os-icon os-icon-pencil-1"></i>\n' +
                                '                </a>\n' +
                                '                <a href="javascript:;" class="trash" data-placement="top" data-toggle="tooltip" data-original-title="Excluir">\n' +
                                '                    <i class="os-icon os-icon-ui-15"></i>\n' +
                                '                </a>\n' +
                                '            </td>\n' +
                                '        </tr>');
}
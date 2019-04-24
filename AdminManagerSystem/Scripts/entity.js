$(document).ready( function () {
    update_table
} );


function update_table(){
    $('#entity table').DataTable({
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
}


$('#entity').on('click', '.approve', function () {
    let id = $(this).closest('tr').attr('data-entity-id')
    $.ajax({
        url: $('#approve_entity_url').val(),
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

                let name = $('tr[data-entity-id="' + id + '"] .name').text();
                $('tr[data-entity-id="' + id + '"]').remove();
                append_entity_approved(id, name);

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

$('#entity').on('click', '.show_more', function () {
    let id = $(this).closest('tr').attr('data-entity-id')
    $.ajax({
        url: $('#show_more_entity_url').val(),
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

$('#entity').on('click', '.trash', function () {
    let id = $(this).closest('tr').attr('data-entity-id')
    $.ajax({
        url: $('#trash_entity_url').val(),
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

                $('tr[data-entity-id="' + id + '"]').remove();

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


function append_entity_approved(id, name) {
    $('#tbody_entity_approved').prepend('\
                                       <tr data-entity-id="'+ id +'">\n' +
        '            <th scope="row">5</th>\n' +
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


    $('[data-toggle="tooltip"]').tooltip('update')
    update_table()
}
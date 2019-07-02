$(document).ready( function () {
    update_table_entity()
} );

function update_table_entity(){

    $('[data-toggle="tooltip"]').tooltip('update')
    
    if ($.fn.dataTable.isDataTable("#entity table")){
        $('#entity table').DataTable().destroy()
    }

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

    $('#entity table:first').DataTable().on( 'order.dt search.dt', function () {
        $('#entity table:first').DataTable().column(0, {search:'applied', order:'applied'}).nodes().each( function (cell, i) {
            cell.innerHTML = i+1;
        } );
    } ).draw();

    $('#entity table:last').DataTable().on( 'order.dt search.dt', function () {
        $('#entity table:last').DataTable().column(0, {search:'applied', order:'applied'}).nodes().each( function (cell, i) {
            cell.innerHTML = i+1;
        } );
    } ).draw();
}


$('#entity').on('click', '.approve', function () {
    let id = $(this).closest('tr').attr('data-entity-id');
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

                if ($.fn.dataTable.isDataTable("#entity table")){
                    $('#entity table').DataTable().destroy()
                }

                let name = $('tr[data-entity-id="' + id + '"] .name').text();
                $('tr[data-entity-id="' + id + '"]').remove();
                append_entity_approved(id, name);

                update_table_entity()

                
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

                let entity = data.entity;

                $('.show-entity-modal .name').text(entity.EntityName);
                $('.show-entity-modal .initials').text(entity.EntityInitials);
                $('.show-entity-modal .email').text(entity.Email);
                $('.show-entity-modal .phone').text(entity.EntityPhone);
                $('.show-entity-modal .socialnetwork').text(entity.EntitySocialNetwork);
                $('.show-entity-modal .responsable').text(entity.EntityResponsableName);
                $('.show-entity-modal .site').text(entity.EntityWebSite).attr({'href':entity.EntityWebSite, "target":"_blank"});
                $('.show-entity-modal .description').text(entity.EntityDescription);
                
                $('.show-entity-modal .cep').text(entity.EntityAddress.CEP);
                $('.show-entity-modal .avenue').text(entity.EntityAddress.Avenue);
                $('.show-entity-modal .number').text(entity.EntityAddress.Number);
                $('.show-entity-modal .neighborhood').text(entity.EntityAddress.Neighborhood);
                $('.show-entity-modal .city').text(entity.EntityAddress.City + ' - ' + entity.EntityAddress.State);

                let affinities ='';
                entity.EntityAffinity.forEach(function (e) {
                    affinities = affinities + e.Name + '; ';
                })

                $('.show-entity-modal .affinities').text(affinities);

                $('.show-entity-modal').modal('show')
                
                
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

                if ($.fn.dataTable.isDataTable("#entity table")){
                    $('#entity table').DataTable().destroy()
                }
                
                $('tr[data-entity-id="' + id + '"]').remove();

                
                update_table_entity();
                
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

    $('#tbody_entity_approved').append('<tr data-entity-id="'+ id +'">\n' +
                            '            <td></td>\n' +
                            '            <td class="name">'+ name +'</td>\n' +
                            '            <td class="actions text-right">\n' +
                            '                <a href="javascript:;" class="show_more" data-placement="top" data-toggle="tooltip" data-original-title="Ver mais">\n' +
                            '                    <i class="os-icon os-icon-search"></i>\n' +
                            '                </a>\n' +
                            '                <a href="javascript:;" class="trash" data-placement="top" data-toggle="tooltip" data-original-title="Excluir">\n' +
                            '                    <i class="os-icon os-icon-ui-15"></i>\n' +
                            '                </a>\n' +
                            '             </td>\n' +
                            '           </tr>')
    
}
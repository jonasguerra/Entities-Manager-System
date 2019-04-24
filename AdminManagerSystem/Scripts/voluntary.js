
$('#voluntary').on('click', '.approve', function () {
    let id = $(this).closest('tr').attr('data-voluntary-id')
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

                let name = $('tr[data-voluntary-id="' + id + '"] .name').text();
                $('tr[data-voluntary-id="' + id + '"]').remove();
                append_voluntary_approved(id, name);


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
    let id = $(this).closest('tr').attr('data-voluntary-id')
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

                $('tr[data-voluntary-id="' + id + '"]').remove();

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
    $('#tbody_voluntary_approved').prepend('\
                                       <tr data-voluntary-id="'+ id +'">\n' +
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
}
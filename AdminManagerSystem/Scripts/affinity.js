
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

$('#affinity').on('click', '.edit', function () {
    let id = $(this).closest('tr').attr('data-affinity-id')
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

                $('tr[data-affinity-id="' + id + '"]').remove();
    
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
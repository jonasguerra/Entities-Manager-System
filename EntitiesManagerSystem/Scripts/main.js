$(document).ready(function () {
    
    $('input#date_range').daterangepicker({
        timePicker: true,
        timePickerIncrement: 30,
        locale: {
            format: 'MM/DD/YYYY h:mm A',
            closeText: 'Fechar',
            prevText: '&#x3c;Anterior',
            nextText: 'Pr&oacute;ximo&#x3e;',
            currentText: 'Hoje',
            monthNames: ['Janeiro','Fevereiro','Mar&ccedil;o','Abril','Maio','Junho',
                'Julho','Agosto','Setembro','Outubro','Novembro','Dezembro'],
            monthNamesShort: ['Jan','Fev','Mar','Abr','Mai','Jun',
                'Jul','Ago','Set','Out','Nov','Dez'],
            dayNames: ['Domingo','Segunda-feira','Ter&ccedil;a-feira','Quarta-feira','Quinta-feira','Sexta-feira','Sabado'],
            dayNamesShort: ['Dom','Seg','Ter','Qua','Qui','Sex','Sab'],
            dayNamesMin: ['Dom','Seg','Ter','Qua','Qui','Sex','Sab'],
            weekHeader: 'Sm',
            firstDay: 0,
            isRTL: false,
            showMonthAfterYear: false,
            yearSuffix: ''
        },
    });

    $("select.select2").select2({
        "language": "pt-BR"
    });
    
    $("#cep").autocompleteAddress();

    if ($('#ckeditor1').length) {
        // CKEDITOR.replace('ckeditor1');
    }

    if ($('#formValidate').length) {
        $('#formValidate').validator();
      }
    
    addMask();
})


function addMask() {

    $(".mask_phone").keyup(function () {
        var valor = $(this).val().length;
        if (valor > 14) {
            $('.mask_phone').mask('(00) 00000-0000');
        } else {
            $('.mask_phone').mask('(00) 0000-00000');
        }
    });

    $('.mask_cpf').mask('999.999.999-99');

    $(".mask_cnpj").mask("99.999.999/9999-99");

    $(".mask_cep").mask("99999-999");
}

$('input[name="collect_donation"]').on('change', function(){
    if($(this).is(':checked')){
        $('#collect_donation_address').show('slow')
    }else{
        $('#collect_donation_address').hide('slow')
    }
})
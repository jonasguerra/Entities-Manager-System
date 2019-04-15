$(document).ready(function () {
    if ($('input#date_range').length) {
        $('input#date_range').daterangepicker({
            timePicker: true,
            timePickerIncrement: 30,
            "locale": {
                "format": "DD/MM/YYYY",
                "separator": " - ",
                "applyLabel": "Aplicar",
                "cancelLabel": "Cancelar",
                "fromLabel": "De",
                "toLabel": "Até",
                "customRangeLabel": "Custom",
                "daysOfWeek": [
                    "Dom",
                    "Seg",
                    "Ter",
                    "Qua",
                    "Qui",
                    "Sex",
                    "Sáb"
                ],
                "monthNames": [
                    "Janeiro",
                    "Fevereiro",
                    "Março",
                    "Abril",
                    "Maio",
                    "Junho",
                    "Julho",
                    "Agosto",
                    "Setembro",
                    "Outubro",
                    "Novembro",
                    "Dezembro"
                ],
                "firstDay": 0
            },
        });
    }


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
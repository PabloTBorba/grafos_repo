$(function() {
    $('#lblMenorCaminho').hide();
    $('#grafoPontoA').val('');
    $('#grafoPontoB').val('');

    $('#btnPontosGrafo').click(function() {
        var tab = $('.nav-tabs .active > a');
        var dados = JSON.stringify({
            VerticeA: $('#grafoPontoA').val(),
            VerticeB: $('#grafoPontoB').val(),
            TabId: tab.attr('href')
        });

        if ($('#grafoPontoA').val().length < 1 || $('#grafoPontoB').val().length < 1) {
            alert('Por favor, informe todos os vértices');
        }
        else {
            $.ajax({
                url: '/Home/GetMenorCaminho',
                type: 'POST',
                contentType: 'application/json',
                data: dados,
                headers: {}
            }).done(function(response) {
                $('#lblMenorCaminho').html(response);
                $('#lblMenorCaminho').show();
            });
        }
    });

    $('a[data-toggle="tab"]').on('click', function(e) {
        $('#lblMenorCaminho').hide();
        $('#grafoPontoA').val('');
        $('#grafoPontoB').val('');
    });
});
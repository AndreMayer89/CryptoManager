function chamadaAjaxGet(url, parametros, callbackSucesso, callbackErro, async, exibirCarregando) {
    chamadaAjax('GET', url, parametros, callbackSucesso, callbackErro, async, exibirCarregando);
}

function chamadaAjaxPost(url, parametros, callbackSucesso, callbackErro, async, exibirCarregando) {
    chamadaAjax('POST', url, parametros, callbackSucesso, callbackErro, async, exibirCarregando);
}

function chamadaAjax(tipo, url, parametros, callbackSucesso, callbackErro, async, exibirCarregando) {
    $.ajax({
        type: tipo,
        url: url,
        data: parametros,
        cache: false,
        async: async == null || async,
        beforeSend: function () {
            if (exibirCarregando) {
                bloquearPagina();
            }
        },
        complete: function () {
            if (exibirCarregando) {
                desbloquearPagina();
            }
        },
        success: function (args) {
            processarSucessoChamadaAjax(args, callbackSucesso, callbackErro);
        },
        error: function () {
            if (exibirCarregando) {
                desbloquearPagina();
            }
            notificacaoErro('Ocorreu um erro interno.');
        }
    });
}

function processarSucessoChamadaAjax(args, callbackSucesso, callbackErro) {
    if (args.sucesso != undefined && args.sucesso != null && !args.sucesso) {
        if (args.mensagem != undefined && args.mensagem != null) {
            notificacaoErro(args.mensagem);
            if (callbackErro != null) {
                callbackErro();
            }
        }
        else {
            notificacaoErro('Ocorreu um erro interno.');
        }
    }
    else {
        callbackSucesso(args);
    }
}

function bloquearPagina() {
    $.blockUI({

    });
}

function desbloquearPagina() {
    $.unblockUI();
}

function notificacaoErro(texto) {
    notificacaoTempoDefinido(texto, 'error');
}

function notificacaoSucesso(texto) {
    notificacaoTempoDefinido(texto, 'success');
}

function notificacaoAlerta(texto) {
    notificacaoTempoDefinido(texto, 'warning');
}

function notificacaoTempoDefinido(texto, tipo) {
    window.noty({ text: texto, type: tipo, layout: 'topCenter', timeout: 4 * 1000 });
}

function transformarEmTabelaOrdenavel(tabela) {
    $(tabela).DataTable({
        "bJQueryUI": false,
        "oLanguage": {
            "sProcessing": "Processando...",
            "sLengthMenu": "Mostrar _MENU_ registros",
            "sZeroRecords": "Não foram encontrados resultados",
            "sInfo": "Mostrando de _START_ até _END_ de _TOTAL_ registros",
            "sInfoEmpty": "Mostrando de 0 até 0 de 0 registros",
            "sInfoFiltered": "",
            "sInfoPostFix": "",
            "sSearch": "Buscar:",
            "sUrl": "",
            "oPaginate": {
                "sFirst": "Primeiro",
                "sPrevious": "Anterior",
                "sNext": "Seguinte",
                "sLast": "Último"
            }
        },
        "bLengthChange": false,
        "bFilter": false, searching: false, paging: false
    });
}
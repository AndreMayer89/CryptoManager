var listaCryptosUltimaPesquisa;
var chaveSessao;

function montarPagina() {
    if ($('.total-investido').val() != null && $('.total-investido').val().length > 0) {
        chamadaAjaxGet(urlObterCorpo, {
            totalInvestido: parseFloat($('.total-investido').val()),
            entradaPolo: obterEntradaPolo(),
            entradaBittrex: obterEntradaBittrex(),
            entradaBitfinex: obterEntradaBitfinex(),
            entradaKraken: obterEntradaKraken(),
            listaBalancoColdWalletString: obterListaMoedasColdWallet()
        },
        function (ret) {
            mostrarCarteira(ret);
        }, function (err) {
        }, true, true);
    }
    else {
        notificacaoErro('Preencha o Investimento (R$)');
    }
}

function mostrarCarteira(retorno) {
    $('.corpo-pagina').empty();
    $('.corpo-pagina').html(retorno.html);
    $('a.tab-resumo').click();
    transformarEmTabelaOrdenavel($('#tab-resumo table, #tab-quantitativo table, #tab-grafico table'));
    listaCryptosUltimaPesquisa = retorno.listaCryptos;
    refazerDashboard();

    if (!retorno.logou) {
        chaveSessao = retorno.chaveSessao;
        perguntarSobreSenha();
    }
}

function perguntarSobreSenha() {
    window.noty({
        text: 'Você deseja salvar sua entrada? </br> Cadastre uma senha <input id="senha" type="password"/>',
        layout: 'topCenter',
        buttons:
            [
                {
                    addClass: 'btn btn-success', text: 'Sign up', onClick: function ($noty) {
                        window.location.href = '/Home/DownloadWallet?chaveSessao=' + chaveSessao + '&senha=' + $('#senha').val();
                        $noty.close();
                    }
                },
                {
                    addClass: 'btn btn-danger', text: 'Cancel', onClick: function ($noty) {
                        $noty.close();
                    }
                }
            ]
    }).show();
}

function obterEntradaPolo() {
    return obterEntradaApiEspecifica($('.key-poloniex').val(), $('.secret-poloniex').val());
}

function obterEntradaBittrex() {
    return obterEntradaApiEspecifica($('.key-bittrex').val(), $('.secret-bittrex').val());
}

function obterEntradaBitfinex() {
    return obterEntradaApiEspecifica($('.key-bitfinex').val(), $('.secret-bitfinex').val());
}

function obterEntradaKraken() {
    return obterEntradaApiEspecifica($('.key-kraken').val(), $('.secret-kraken').val());
}

function obterEntradaApiEspecifica(key, secret) {
    if (key.length > 0 && secret.length > 0) {
        return JSON.stringify({ ApiKey: key, ApiSecret: secret });
    }
    return null;
}
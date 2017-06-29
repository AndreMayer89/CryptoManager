function adicionarLinhaBalancoColdAPartirDoInput() {
    var siglaMoedaBalanco = $('.input-balanco-moeda').val();
    var qtdBalanco = $('.input-balanco-qtd').val();
    var exchangeBalanco = $('.input-balanco-exchange').val();

    adicionarLinhaBalancoCold(siglaMoedaBalanco, qtdBalanco, exchangeBalanco);

    $('.input-balanco-moeda').val('');
    $('.input-balanco-qtd').val('');
    $('.input-balanco-exchange').val('');
}

function adicionarLinhaBalancoCold(siglaMoedaBalanco, qtdBalanco, exchangeBalanco) {
    var linha = ''
        + '<tr class="linha-crypto linha-input-balanco-cold">'
        + '<td class="col-moeda">' + siglaMoedaBalanco + '</td>'
        + '<td class="col-qtd">' + qtdBalanco + '</td>'
        + '<td class="col-exchange">' + exchangeBalanco + '</td>'
        + '<td><button class="btn btn-danger botao-remover-linha-balanco-cold" type="button">Remover</button></td>'
        + '</tr>';
    $('.tabela-input-balanco-cold tbody').append(linha);
}

function obterListaMoedasColdWallet() {
    var lista = [];
    $.each($('.linha-input-balanco-cold'), function () {
        lista.push({
            SiglaMoeda: $(this).find('.sigla-moeda').val(),
            QuantidadeMoeda: $(this).find('.qtd-moeda').val(),
            ExchangeCotacao: $(this).find('.exchange-cotacao').val()
        });
    });
    return JSON.stringify(lista);
}

function removerLinhaInputBalancoCold(botao) {
    botao.parents('.linha-input-balanco-cold').remove();
}
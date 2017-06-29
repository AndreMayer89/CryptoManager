function adicionarLinhaCompraColdAPartirDoInput() {
    var siglaMoedaComprada = $('.input-moeda-comprada').val();
    var qtdMoedaComprada = $('.input-qtd-moeda-comprada').val();
    var siglaMoedaPagamento = $('.input-moeda-pagamento').val();
    var qtdMoedaPagamento = $('.input-qtd-moeda-pagamento').val();
    
    adicionarLinhaCompraCold(siglaMoedaComprada, qtdMoedaComprada, siglaMoedaPagamento, qtdMoedaPagamento);

    $('.input-moeda-comprada').val('');
    $('.input-qtd-moeda-comprada').val('');
    $('.input-moeda-pagamento').val('');
    $('.input-qtd-moeda-pagamento').val('');
}

function adicionarLinhaCompraCold(siglaMoedaComprada, qtdMoedaComprada, siglaMoedaPagamento, qtdMoedaPagamento) {
    var linha = ''
        + '<tr class="linha-crypto linha-input-compra-cold">'
        + '<td class="col-moeda-comprada">' + siglaMoedaComprada + '</td>'
        + '<td class="col-qtd-moeda-comprada">' + qtdMoedaComprada + '</td>'
        + '<td class="col-moeda-pagamento">' + siglaMoedaPagamento + '</td>'
        + '<td class="col-qtd-moeda-pagamento">' + qtdMoedaPagamento + '</td>'
        + '<td><button class="btn btn-danger botao-remover-linha-compra-cold" type="button">Remover</button></td>'
        + '</tr>';
    $('.tabela-input-compra-cold tbody').append(linha);
}

function obterListaComprasMoedaCold() {
    var lista = [];
    $.each($('.linha-input-compra-cold'), function () {
        lista.push({
            SiglaMoedaComprada: $(this).find('.col-moeda-comprada').text(),
            QuantidadeMoedaComprada: $(this).find('.col-qtd-moeda-comprada').text(),
            SiglaMoedaUtilizadaNaCompra: $(this).find('.col-moeda-pagamento').text(),
            QuantidadeMoedaUtilizadaNaCompra: $(this).find('.col-qtd-moeda-pagamento').text()
        });
    });
    return JSON.stringify(lista);
}

function removerLinhaInputCompraCold(botao) {
    botao.parents('.linha-input-compra-cold').remove();
}
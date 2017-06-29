var percentColors = [
    { pct: -50.0, color: { r: 0xff, g: 0x00, b: 0 } },
    { pct: 0, color: { r: 0xff, g: 0xff, b: 0xff } },
    { pct: 100.0, color: { r: 0x00, g: 0xff, b: 0 } }];

function getColorForPercentage(pct) {
    for (var i = 1; i < percentColors.length - 1; i++) {
        if (pct < percentColors[i].pct) {
            break;
        }
    }
    var lower = percentColors[i - 1];
    var upper = percentColors[i];
    var range = upper.pct - lower.pct;
    var rangePct = (pct - lower.pct) / range;
    var pctLower = 1 - rangePct;
    var pctUpper = rangePct;
    var color = {
        r: Math.floor(lower.color.r * pctLower + upper.color.r * pctUpper),
        g: Math.floor(lower.color.g * pctLower + upper.color.g * pctUpper),
        b: Math.floor(lower.color.b * pctLower + upper.color.b * pctUpper)
    };
    return 'rgb(' + [color.r, color.g, color.b].join(',') + ')';
}

function desenharGrafico(listaCryptos) {
    var arrayInfo = [];
    arrayInfo.push(['Crypto', 'Porcentagem']);
    for (var crypto = 0; crypto < listaCryptos.length; crypto++) {
        if ($('.checkbox-crypto-resumo[data-crypto=' + listaCryptos[crypto].SiglaCrypto + ']').is(':checked')) {
            arrayInfo.push([listaCryptos[crypto].NomeCrypto, listaCryptos[crypto].ValorTotalBrlRealDouble]);
        }
    }
    var data = google.visualization.arrayToDataTable(arrayInfo);
    var options = {
        title: 'Cryptos', height: 600, width: 600
    };
    var chart = new google.visualization.PieChart(document.getElementById('piechart'));
    chart.draw(data, options);
}

function refazerDashboard() {
    desenharGrafico(listaCryptosUltimaPesquisa);
}
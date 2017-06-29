using CryptoManager.Entidades;
using CryptoManager.Entidades.Especificas;
using CryptoManager.Repositorio.Especificas;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace CryptoManager.Business
{
    public class ConsultaExchangesBusiness
    {
        public ConsultaExchangesEntidade ConsultarExchanges(PoloniexEntradaApiEntidade entradaPoloniex,
            BittrexEntradaApiEntidade entradaBittrex, KrakenEntradaApiEntidade entradaKraken,
            BitfinexEntradaApiEntidade entradaBitfinex, List<MoedaEmCarteiraEntidade> listaBalancoColdWallet,
            List<CompraMoedaEmColdEntidade> listaComprasColdWallet)
        {
            ConsultaExchangesEntidade retorno = new ConsultaExchangesEntidade();
            retorno.ListaCotacoes = new List<CotacaoMoedaEntidade>();
            retorno.ListaQuantidades = new List<CryptoQuantidadeEntidade>();
            retorno.ListaResultadosOperacoesExchanges = new List<ResultadoOperacaoEntidade>();
            var listaCompra = new List<OrdemMoedaEntidade>();
            var listaBalanco = new List<BalancoMoedaEntidade>();
            long nonce = DateTime.Now.Ticks;
            var resultadoConsulta = new List<ResultadoConsultaExchangeEntidade>();

            if (entradaPoloniex != null)
                resultadoConsulta.Add(ConsultarExchange(entradaPoloniex,
                    new ApiPoloniex(entradaPoloniex.ApiKey, entradaPoloniex.ApiSecret, nonce)));

            if (entradaBittrex != null)
                resultadoConsulta.Add(ConsultarExchange(entradaBittrex,
                    new ApiBittrex(entradaBittrex.ApiKey, entradaBittrex.ApiSecret, nonce)));

            if (entradaKraken != null)
                resultadoConsulta.Add(ConsultarExchange(entradaKraken,
                    new ApiKraken(entradaKraken.ApiKey, entradaKraken.ApiSecret, nonce)));

            if (entradaBitfinex != null)
                resultadoConsulta.Add(ConsultarExchange(entradaBitfinex,
                    new ApiBitfinex(entradaBitfinex.ApiKey, entradaBitfinex.ApiSecret, nonce)));

            listaBalanco.AddRange(resultadoConsulta.SelectMany(r => r.Balanco));
            listaCompra.AddRange(resultadoConsulta.SelectMany(r => r.LivroOrdens));
            retorno.ListaCotacoes.AddRange(resultadoConsulta.SelectMany(r => r.Cotacao));
            retorno.ListaResultadosOperacoesExchanges.AddRange(resultadoConsulta.Select(r => r.ResultadoOperacoes));
            foreach (var balanco in listaBalanco)
            {
                if (!retorno.ListaQuantidades.Any(q => q != null && q.Tipo != null && q.Tipo.Sigla == balanco.Moeda.Sigla))
                {
                    retorno.ListaQuantidades.Add(new CryptoQuantidadeEntidade(balanco.Moeda));
                }
                retorno.ListaQuantidades.First(q => q != null && q.Tipo != null && q.Tipo.Sigla == balanco.Moeda.Sigla).ListaBalancos.Add(balanco);
            }
            foreach (var compra in listaCompra)
            {
                TipoCrypto moeda = compra.Moeda;
                if (!retorno.ListaQuantidades.Any(q => q != null && q.Tipo != null && q.Tipo.Sigla == moeda.Sigla))
                {
                    retorno.ListaQuantidades.Add(new CryptoQuantidadeEntidade(moeda));
                }
                retorno.ListaQuantidades.First(q => q != null && q.Tipo != null && q.Tipo.Sigla == moeda.Sigla).ListaCompras.Add(compra);
            }
            if (listaBalancoColdWallet != null)
            {
                foreach (var moedaColdWallet in listaBalancoColdWallet)
                {
                    TipoCrypto moeda = TipoCrypto.ObterPorSigla(moedaColdWallet.SiglaMoeda);
                    if (!retorno.ListaQuantidades.Any(q => q != null && q.Tipo != null && q.Tipo.Sigla == moeda.Sigla))
                    {
                        retorno.ListaQuantidades.Add(new CryptoQuantidadeEntidade(moeda));
                    }
                    retorno.ListaQuantidades.First(q => q != null && q.Tipo != null && q.Tipo.Sigla == moeda.Sigla).ListaBalancos.Add(
                        new BalancoMoedaEntidade()
                        {
                            Exchange = TipoExchange.ObterPorNome(moedaColdWallet.ExchangeCotacao),
                            Moeda = moeda,
                            Quantidade = moedaColdWallet.QuantidadeMoeda,
                            ColdWallet = true
                        });
                }
            }
            var listaComprasColdWalletConsolidada = ObterListaConsolidada(listaComprasColdWallet);
            foreach (var moedaColdWallet in listaComprasColdWalletConsolidada)
            {
                var qtdMoedaPagamento = retorno.ListaQuantidades.FirstOrDefault(q => q.Tipo.Sigla == moedaColdWallet.SiglaMoedaUtilizadaNaCompra);
                var valorUnidadeBtcMoedaPagamento = qtdMoedaPagamento.Quantidade > 0
                    ? (ObterValorTotalEmBtc(retorno.ListaCotacoes, qtdMoedaPagamento) / qtdMoedaPagamento.Quantidade) : 0;

                qtdMoedaPagamento.ListaCompras.Add(new OrdemMoedaEntidade()
                {
                    Exchange = TipoExchange.ColdWallet,
                    Moeda = qtdMoedaPagamento.Tipo,
                    QuantidadeInvestida = 0,
                    QuantidadeMoeda = moedaColdWallet.QuantidadeMoedaUtilizadaNaCompra
                });

                var qtdMoedaComprada = retorno.ListaQuantidades.FirstOrDefault(q => q.Tipo.Sigla == moedaColdWallet.SiglaMoedaComprada);
                if (qtdMoedaComprada == null)
                {
                    retorno.ListaQuantidades.Add(new CryptoQuantidadeEntidade(TipoCrypto.ObterPorSigla(moedaColdWallet.SiglaMoedaComprada)));
                    qtdMoedaComprada = retorno.ListaQuantidades.FirstOrDefault(q => q.Tipo.Sigla == moedaColdWallet.SiglaMoedaComprada);
                }
                qtdMoedaComprada.ListaCompras.Add(new OrdemMoedaEntidade()
                {
                    Exchange = TipoExchange.ColdWallet,
                    Moeda = qtdMoedaComprada.Tipo,
                    QuantidadeInvestida = moedaColdWallet.QuantidadeMoedaUtilizadaNaCompra * valorUnidadeBtcMoedaPagamento,
                    QuantidadeMoeda = moedaColdWallet.QuantidadeMoedaComprada
                });
            }
            foreach (var qtd in retorno.ListaQuantidades.Where(q => q.ListaCompras.Any(c => c.Exchange.Id == TipoExchange.ColdWallet.Id && c.QuantidadeInvestida == 0)))
            {
                var valorInvestidoTotalBtc = qtd.QuantidadeBtcInvestida;
                var qtdUtilizadoEmComprasCold = qtd.ListaCompras.Where(c => c.Exchange == TipoExchange.ColdWallet && c.QuantidadeInvestida == 0).Sum(c => c.QuantidadeMoeda);
                var qtdInvestidaAbatimento = valorInvestidoTotalBtc * (1 - (qtd.Quantidade / (qtd.Quantidade + qtdUtilizadoEmComprasCold)));
                foreach (var compra in qtd.ListaCompras.Where(c => c.Exchange == TipoExchange.ColdWallet && c.QuantidadeInvestida == 0))
                {
                    compra.QuantidadeInvestida = -(compra.QuantidadeMoeda * qtdInvestidaAbatimento / qtdUtilizadoEmComprasCold);
                }
            }
            return retorno;
        }

        private List<CompraMoedaEmColdEntidade> ObterListaConsolidada(List<CompraMoedaEmColdEntidade> listaComprasColdWallet)
        {
            var listaComprasColdWalletConsolidada = new List<CompraMoedaEmColdEntidade>();
            if (listaComprasColdWallet != null)
            {
                foreach (var moedaColdWallet in listaComprasColdWallet)
                {
                    if (!listaComprasColdWalletConsolidada.Any(m => m.SiglaMoedaComprada == moedaColdWallet.SiglaMoedaComprada
                        && m.SiglaMoedaUtilizadaNaCompra == moedaColdWallet.SiglaMoedaUtilizadaNaCompra))
                    {
                        listaComprasColdWalletConsolidada.Add(new CompraMoedaEmColdEntidade()
                        {
                            SiglaMoedaComprada = moedaColdWallet.SiglaMoedaComprada,
                            SiglaMoedaUtilizadaNaCompra = moedaColdWallet.SiglaMoedaUtilizadaNaCompra
                        });
                    }
                    listaComprasColdWalletConsolidada.First(m => m.SiglaMoedaComprada == moedaColdWallet.SiglaMoedaComprada
                        && m.SiglaMoedaUtilizadaNaCompra == moedaColdWallet.SiglaMoedaUtilizadaNaCompra)
                        .QuantidadeMoedaComprada += moedaColdWallet.QuantidadeMoedaComprada;
                    listaComprasColdWalletConsolidada.First(m => m.SiglaMoedaComprada == moedaColdWallet.SiglaMoedaComprada
                        && m.SiglaMoedaUtilizadaNaCompra == moedaColdWallet.SiglaMoedaUtilizadaNaCompra)
                        .QuantidadeMoedaUtilizadaNaCompra += moedaColdWallet.QuantidadeMoedaUtilizadaNaCompra;
                }
            }
            return listaComprasColdWalletConsolidada;
        }

        public static double ObterValorTotalEmBtc(List<CotacaoMoedaEntidade> listaCotacoes, CryptoQuantidadeEntidade moeda)
        {
            if (moeda.Quantidade > 0)
            {
                var cotacoesDaMoeda = listaCotacoes.Where(c => c.Tipo.Sigla == moeda.Tipo.Sigla).ToList();
                double valorAcumulado = 0;
                cotacoesDaMoeda.ForEach(c => valorAcumulado +=
                    (c.ValorUnidadeEmBitcoin * moeda.ListaBalancos.FirstOrDefault(b => b.Exchange.Id == c.Exchange.Id)?.Quantidade ?? 0));
                return valorAcumulado;
            }
            return 0;
        }

        private ResultadoConsultaExchangeEntidade ConsultarExchange(BaseEntradaApiEntidade entradaApi, IApiExchange apiExchange)
        {
            var resultado = new ResultadoConsultaExchangeEntidade();
            resultado.Exchange = apiExchange.ObterTipo();
            resultado.LivroOrdens = new List<OrdemMoedaEntidade>();
            resultado.Balanco = new List<BalancoMoedaEntidade>();
            resultado.Cotacao = new List<CotacaoMoedaEntidade>();
            resultado.ResultadoOperacoes = new ResultadoOperacaoEntidade() { Exchange = resultado.Exchange };
            if (entradaApi != null)
            {
                resultado.ResultadoOperacoes.ListaTipoOperacao.Add(Executar(TipoOperacaoExchange.Cotacao, () => { resultado.Cotacao.AddRange(apiExchange.Cotar()); }));
                resultado.ResultadoOperacoes.ListaTipoOperacao.Add(Executar(TipoOperacaoExchange.Balanco, () => { resultado.Balanco.AddRange(apiExchange.ListarBalancoConta()); }));
                resultado.ResultadoOperacoes.ListaTipoOperacao.Add(Executar(TipoOperacaoExchange.LivroOrdens, () => { resultado.LivroOrdens.AddRange(apiExchange.ListarHistoricoOrdem()); }));
            }
            return resultado;
        }

        private static ResultadoOperacaoEntidade.ResultadoOperacaoEspecificaEntidade Executar(TipoOperacaoExchange tipoOperacao, Action action)
        {
            var resultado = new ResultadoOperacaoEntidade.ResultadoOperacaoEspecificaEntidade() { Operacao = tipoOperacao };
            Stopwatch stopwatch = Stopwatch.StartNew();
            try
            {
                action();
            }
            catch (Exception e)
            {
                resultado.MensagemErro = e.Message;
            }
            stopwatch.Stop();
            resultado.Milisegundos = stopwatch.Elapsed.TotalMilliseconds;
            return resultado;
        }
    }
}

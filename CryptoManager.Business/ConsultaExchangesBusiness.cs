﻿using CryptoManager.Entidades;
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
            BitfinexEntradaApiEntidade entradaBitfinex, List<MoedaEmCarteiraEntidade> listaBalancoColdWallet)
        {
            ConsultaExchangesEntidade retorno = new ConsultaExchangesEntidade();
            retorno.ListaCotacoes = new List<CotacaoMoedaEntidade>();
            retorno.ListaQuantidades = new List<CryptoQuantidadeEntidade>();
            retorno.ListaResultadosOperacoesExchanges = new List<ResultadoOperacaoEntidade>();
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
                            Exchange = TipoExchange.Obter(Convert.ToInt32(moedaColdWallet.ExchangeCotacao)),
                            Moeda = moeda,
                            Quantidade = moedaColdWallet.QuantidadeMoeda,
                            ColdWallet = true
                        });
                }
            }
            return retorno;
        }

        public static double ObterValorTotalEmBtc(IEnumerable<CotacaoMoedaEntidade> listaCotacoes, CryptoQuantidadeEntidade moeda)
        {
            if (moeda.Quantidade > 0)
            {
                var cotacoesDaMoeda = listaCotacoes.Where(c => c.Tipo.Sigla == moeda.Tipo.Sigla).ToList();
                double valorAcumulado = 0;
                foreach (var balanco in moeda.ListaBalancos)
                {
                    var cotacaoUtilizada = cotacoesDaMoeda.FirstOrDefault(c => c.Exchange.Id == balanco.Exchange.Id);
                    if (cotacaoUtilizada != null)
                    {
                        valorAcumulado += balanco.Quantidade * cotacaoUtilizada.ValorUnidadeEmBitcoin;
                    }
                }
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

        public object Cotar(int? idExchange)
        {
            //TODO [André] - Utilizar o parâmetro
            return new ApiBitfinex(null, null, 0).Cotar().ToList();
        }
    }
}

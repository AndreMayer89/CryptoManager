using CryptoManager.Entidades;
using CryptoManager.Entidades.Especificas;
using CryptoManager.Util.Cache;
using CryptoManager.Util.Enum;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;

namespace CryptoManager.Repositorio.Especificas
{
    public class ApiPoloniex : ApiBase<PoloniexEntidade>, IApiExchange
    {
        private string ApiKey { get; set; }
        private string ApiSecret { get; set; }
        private long Nonce { get; set; }

        public ApiPoloniex(string apiKey, string apiSecret, long nonce)
        {
            ApiKey = apiKey != null ? apiKey.Trim() : string.Empty;
            ApiSecret = apiSecret != null ? apiSecret.Trim() : string.Empty;
            Nonce = nonce;
        }

        public TipoExchange ObterTipo()
        {
            return TipoExchange.Poloniex;
        }

        private static Dictionary<string, object> ObterDicionarioParametrosCotacao()
        {
            Dictionary<string, object> dicionario = new Dictionary<string, object>();
            dicionario.Add("command", "returnTicker");
            return dicionario;
        }

        public IEnumerable<CotacaoMoedaEntidade> Cotar()
        {
            PoloniexEntidade retorno = Cotar("https://poloniex.com/public", ObterDicionarioParametrosCotacao());
            List<CotacaoMoedaEntidade> lista = new List<CotacaoMoedaEntidade>();
            lista.Add(CriarRegistroCotacao(TipoCrypto.Ethereum, retorno.BTC_ETH.last));
            lista.Add(CriarRegistroCotacao(TipoCrypto.GameCredits, retorno.BTC_GAME.last));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Monero, retorno.BTC_XMR.last));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Maid, retorno.BTC_MAID.last));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Decred, retorno.BTC_DCR.last));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Digibyte, retorno.BTC_DGB.last));
            lista.Add(CriarRegistroCotacao(TipoCrypto.EthereumClassic, retorno.BTC_ETC.last));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Factom, retorno.BTC_FCT.last));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Golem, retorno.BTC_GNT.last));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Lisk, retorno.BTC_LSK.last));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Ripple, retorno.BTC_XRP.last));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Siacoin, retorno.BTC_SC.last));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Stellar, retorno.BTC_STR.last));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Stratis, retorno.BTC_STRAT.last));
            lista.Add(CriarRegistroCotacao(TipoCrypto.BitcoinCash, retorno.BTC_BCH.last));
            lista.Add(CriarRegistroCotacao(TipoCrypto.ZeroX, retorno.BTC_ZRX.last));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Gas, retorno.BTC_GAS.last));
            lista.Add(new CotacaoMoedaEntidade()
            {
                Exchange = ObterTipo(),
                Tipo = TipoCrypto.Bitcoin,
                ValorUnidadeEmBitcoin = 1
            });
            return lista;
        }

        public IEnumerable<OrdemMoedaEntidade> ListarHistoricoOrdem()
        {
            var balancos = ObterHistoricoPoloniex();
            List<OrdemMoedaEntidade> lista = new List<OrdemMoedaEntidade>();
            lista.AddRange(CriarRegistroOrdem(GetPropertyName(() => balancos.BTC_AMP), balancos.BTC_AMP));
            lista.AddRange(CriarRegistroOrdem(GetPropertyName(() => balancos.BTC_ARDR), balancos.BTC_ARDR));
            lista.AddRange(CriarRegistroOrdem(GetPropertyName(() => balancos.BTC_BCH), balancos.BTC_BCH));
            lista.AddRange(CriarRegistroOrdem(GetPropertyName(() => balancos.BTC_BCN), balancos.BTC_BCN));
            lista.AddRange(CriarRegistroOrdem(GetPropertyName(() => balancos.BTC_BCY), balancos.BTC_BCY));
            lista.AddRange(CriarRegistroOrdem(GetPropertyName(() => balancos.BTC_BELA), balancos.BTC_BELA));
            lista.AddRange(CriarRegistroOrdem(GetPropertyName(() => balancos.BTC_BLK), balancos.BTC_BLK));
            lista.AddRange(CriarRegistroOrdem(GetPropertyName(() => balancos.BTC_BTC), balancos.BTC_BTC));
            lista.AddRange(CriarRegistroOrdem(GetPropertyName(() => balancos.BTC_BTCD), balancos.BTC_BTCD));
            lista.AddRange(CriarRegistroOrdem(GetPropertyName(() => balancos.BTC_BTM), balancos.BTC_BTM));
            lista.AddRange(CriarRegistroOrdem(GetPropertyName(() => balancos.BTC_BTS), balancos.BTC_BTS));
            lista.AddRange(CriarRegistroOrdem(GetPropertyName(() => balancos.BTC_BURST), balancos.BTC_BURST));
            lista.AddRange(CriarRegistroOrdem(GetPropertyName(() => balancos.BTC_CLAM), balancos.BTC_CLAM));
            lista.AddRange(CriarRegistroOrdem(GetPropertyName(() => balancos.BTC_DASH), balancos.BTC_DASH));
            lista.AddRange(CriarRegistroOrdem(GetPropertyName(() => balancos.BTC_DCR), balancos.BTC_DCR));
            lista.AddRange(CriarRegistroOrdem(GetPropertyName(() => balancos.BTC_DGB), balancos.BTC_DGB));
            lista.AddRange(CriarRegistroOrdem(GetPropertyName(() => balancos.BTC_DOGE), balancos.BTC_DOGE));
            lista.AddRange(CriarRegistroOrdem(GetPropertyName(() => balancos.BTC_EMC2), balancos.BTC_EMC2));
            lista.AddRange(CriarRegistroOrdem(GetPropertyName(() => balancos.BTC_ETC), balancos.BTC_ETC));
            lista.AddRange(CriarRegistroOrdem(GetPropertyName(() => balancos.BTC_ETH), balancos.BTC_ETH));
            lista.AddRange(CriarRegistroOrdem(GetPropertyName(() => balancos.BTC_EXP), balancos.BTC_EXP));
            lista.AddRange(CriarRegistroOrdem(GetPropertyName(() => balancos.BTC_FCT), balancos.BTC_FCT));
            lista.AddRange(CriarRegistroOrdem(GetPropertyName(() => balancos.BTC_FLDC), balancos.BTC_FLDC));
            lista.AddRange(CriarRegistroOrdem(GetPropertyName(() => balancos.BTC_FLO), balancos.BTC_FLO));
            lista.AddRange(CriarRegistroOrdem(GetPropertyName(() => balancos.BTC_GAME), balancos.BTC_GAME));
            lista.AddRange(CriarRegistroOrdem(GetPropertyName(() => balancos.BTC_GNO), balancos.BTC_GNO));
            lista.AddRange(CriarRegistroOrdem(GetPropertyName(() => balancos.BTC_GNT), balancos.BTC_GNT));
            lista.AddRange(CriarRegistroOrdem(GetPropertyName(() => balancos.BTC_GRC), balancos.BTC_GRC));
            lista.AddRange(CriarRegistroOrdem(GetPropertyName(() => balancos.BTC_HUC), balancos.BTC_HUC));
            lista.AddRange(CriarRegistroOrdem(GetPropertyName(() => balancos.BTC_LBC), balancos.BTC_LBC));
            lista.AddRange(CriarRegistroOrdem(GetPropertyName(() => balancos.BTC_LSK), balancos.BTC_LSK));
            lista.AddRange(CriarRegistroOrdem(GetPropertyName(() => balancos.BTC_LTC), balancos.BTC_LTC));
            lista.AddRange(CriarRegistroOrdem(GetPropertyName(() => balancos.BTC_MAID), balancos.BTC_MAID));
            lista.AddRange(CriarRegistroOrdem(GetPropertyName(() => balancos.BTC_NAUT), balancos.BTC_NAUT));
            lista.AddRange(CriarRegistroOrdem(GetPropertyName(() => balancos.BTC_NAV), balancos.BTC_NAV));
            lista.AddRange(CriarRegistroOrdem(GetPropertyName(() => balancos.BTC_NEOS), balancos.BTC_NEOS));
            lista.AddRange(CriarRegistroOrdem(GetPropertyName(() => balancos.BTC_NMC), balancos.BTC_NMC));
            lista.AddRange(CriarRegistroOrdem(GetPropertyName(() => balancos.BTC_NOTE), balancos.BTC_NOTE));
            lista.AddRange(CriarRegistroOrdem(GetPropertyName(() => balancos.BTC_NXC), balancos.BTC_NXC));
            lista.AddRange(CriarRegistroOrdem(GetPropertyName(() => balancos.BTC_NXT), balancos.BTC_NXT));
            lista.AddRange(CriarRegistroOrdem(GetPropertyName(() => balancos.BTC_OMNI), balancos.BTC_OMNI));
            lista.AddRange(CriarRegistroOrdem(GetPropertyName(() => balancos.BTC_PASC), balancos.BTC_PASC));
            lista.AddRange(CriarRegistroOrdem(GetPropertyName(() => balancos.BTC_PINK), balancos.BTC_PINK));
            lista.AddRange(CriarRegistroOrdem(GetPropertyName(() => balancos.BTC_POT), balancos.BTC_POT));
            lista.AddRange(CriarRegistroOrdem(GetPropertyName(() => balancos.BTC_PPC), balancos.BTC_PPC));
            lista.AddRange(CriarRegistroOrdem(GetPropertyName(() => balancos.BTC_RADS), balancos.BTC_RADS));
            lista.AddRange(CriarRegistroOrdem(GetPropertyName(() => balancos.BTC_REP), balancos.BTC_REP));
            lista.AddRange(CriarRegistroOrdem(GetPropertyName(() => balancos.BTC_RIC), balancos.BTC_RIC));
            lista.AddRange(CriarRegistroOrdem(GetPropertyName(() => balancos.BTC_SBD), balancos.BTC_SBD));
            lista.AddRange(CriarRegistroOrdem(GetPropertyName(() => balancos.BTC_SC), balancos.BTC_SC));
            lista.AddRange(CriarRegistroOrdem(GetPropertyName(() => balancos.BTC_SJCX), balancos.BTC_SJCX));
            lista.AddRange(CriarRegistroOrdem(GetPropertyName(() => balancos.BTC_STEEM), balancos.BTC_STEEM));
            lista.AddRange(CriarRegistroOrdem(GetPropertyName(() => balancos.BTC_STR), balancos.BTC_STR));
            lista.AddRange(CriarRegistroOrdem(GetPropertyName(() => balancos.BTC_STRAT), balancos.BTC_STRAT));
            lista.AddRange(CriarRegistroOrdem(GetPropertyName(() => balancos.BTC_SYS), balancos.BTC_SYS));
            lista.AddRange(CriarRegistroOrdem(GetPropertyName(() => balancos.BTC_USDT), balancos.BTC_USDT));
            lista.AddRange(CriarRegistroOrdem(GetPropertyName(() => balancos.BTC_VIA), balancos.BTC_VIA));
            lista.AddRange(CriarRegistroOrdem(GetPropertyName(() => balancos.BTC_VRC), balancos.BTC_VRC));
            lista.AddRange(CriarRegistroOrdem(GetPropertyName(() => balancos.BTC_VTC), balancos.BTC_VTC));
            lista.AddRange(CriarRegistroOrdem(GetPropertyName(() => balancos.BTC_XBC), balancos.BTC_XBC));
            lista.AddRange(CriarRegistroOrdem(GetPropertyName(() => balancos.BTC_XCP), balancos.BTC_XCP));
            lista.AddRange(CriarRegistroOrdem(GetPropertyName(() => balancos.BTC_XEM), balancos.BTC_XEM));
            lista.AddRange(CriarRegistroOrdem(GetPropertyName(() => balancos.BTC_XMR), balancos.BTC_XMR));
            lista.AddRange(CriarRegistroOrdem(GetPropertyName(() => balancos.BTC_XPM), balancos.BTC_XPM));
            lista.AddRange(CriarRegistroOrdem(GetPropertyName(() => balancos.BTC_XRP), balancos.BTC_XRP));
            lista.AddRange(CriarRegistroOrdem(GetPropertyName(() => balancos.BTC_XVC), balancos.BTC_XVC));
            lista.AddRange(CriarRegistroOrdem(GetPropertyName(() => balancos.BTC_ZEC), balancos.BTC_ZEC));
            lista.AddRange(CriarRegistroOrdem(GetPropertyName(() => balancos.BTC_ZRX), balancos.BTC_ZRX));
            return lista;
        }

        public IEnumerable<BalancoMoedaEntidade> ListarBalancoConta()
        {
            List<BalancoMoedaEntidade> lista = new List<BalancoMoedaEntidade>();
            var balancos = ObterBalancoConta();
            lista.Add(CriarRegistroRetornoBalanco(GetPropertyName(() => balancos.AMP), balancos.AMP));
            lista.Add(CriarRegistroRetornoBalanco(GetPropertyName(() => balancos.ARDR), balancos.ARDR));
            lista.Add(CriarRegistroRetornoBalanco(GetPropertyName(() => balancos.BCH), balancos.BCH));
            lista.Add(CriarRegistroRetornoBalanco(GetPropertyName(() => balancos.BCN), balancos.BCN));
            lista.Add(CriarRegistroRetornoBalanco(GetPropertyName(() => balancos.BCY), balancos.BCY));
            lista.Add(CriarRegistroRetornoBalanco(GetPropertyName(() => balancos.BELA), balancos.BELA));
            lista.Add(CriarRegistroRetornoBalanco(GetPropertyName(() => balancos.BLK), balancos.BLK));
            lista.Add(CriarRegistroRetornoBalanco(GetPropertyName(() => balancos.BTC), balancos.BTC));
            lista.Add(CriarRegistroRetornoBalanco(GetPropertyName(() => balancos.BTCD), balancos.BTCD));
            lista.Add(CriarRegistroRetornoBalanco(GetPropertyName(() => balancos.BTM), balancos.BTM));
            lista.Add(CriarRegistroRetornoBalanco(GetPropertyName(() => balancos.BTS), balancos.BTS));
            lista.Add(CriarRegistroRetornoBalanco(GetPropertyName(() => balancos.BURST), balancos.BURST));
            lista.Add(CriarRegistroRetornoBalanco(GetPropertyName(() => balancos.CLAM), balancos.CLAM));
            lista.Add(CriarRegistroRetornoBalanco(GetPropertyName(() => balancos.DASH), balancos.DASH));
            lista.Add(CriarRegistroRetornoBalanco(GetPropertyName(() => balancos.DCR), balancos.DCR));
            lista.Add(CriarRegistroRetornoBalanco(GetPropertyName(() => balancos.DGB), balancos.DGB));
            lista.Add(CriarRegistroRetornoBalanco(GetPropertyName(() => balancos.DOGE), balancos.DOGE));
            lista.Add(CriarRegistroRetornoBalanco(GetPropertyName(() => balancos.EMC2), balancos.EMC2));
            lista.Add(CriarRegistroRetornoBalanco(GetPropertyName(() => balancos.ETC), balancos.ETC));
            lista.Add(CriarRegistroRetornoBalanco(GetPropertyName(() => balancos.ETH), balancos.ETH));
            lista.Add(CriarRegistroRetornoBalanco(GetPropertyName(() => balancos.EXP), balancos.EXP));
            lista.Add(CriarRegistroRetornoBalanco(GetPropertyName(() => balancos.FCT), balancos.FCT));
            lista.Add(CriarRegistroRetornoBalanco(GetPropertyName(() => balancos.FLDC), balancos.FLDC));
            lista.Add(CriarRegistroRetornoBalanco(GetPropertyName(() => balancos.FLO), balancos.FLO));
            lista.Add(CriarRegistroRetornoBalanco(GetPropertyName(() => balancos.GAME), balancos.GAME));
            lista.Add(CriarRegistroRetornoBalanco(GetPropertyName(() => balancos.GAS), balancos.GAS));
            lista.Add(CriarRegistroRetornoBalanco(GetPropertyName(() => balancos.GNO), balancos.GNO));
            lista.Add(CriarRegistroRetornoBalanco(GetPropertyName(() => balancos.GNT), balancos.GNT));
            lista.Add(CriarRegistroRetornoBalanco(GetPropertyName(() => balancos.GRC), balancos.GRC));
            lista.Add(CriarRegistroRetornoBalanco(GetPropertyName(() => balancos.HUC), balancos.HUC));
            lista.Add(CriarRegistroRetornoBalanco(GetPropertyName(() => balancos.LBC), balancos.LBC));
            lista.Add(CriarRegistroRetornoBalanco(GetPropertyName(() => balancos.LSK), balancos.LSK));
            lista.Add(CriarRegistroRetornoBalanco(GetPropertyName(() => balancos.LTC), balancos.LTC));
            lista.Add(CriarRegistroRetornoBalanco(GetPropertyName(() => balancos.MAID), balancos.MAID));
            lista.Add(CriarRegistroRetornoBalanco(GetPropertyName(() => balancos.NAUT), balancos.NAUT));
            lista.Add(CriarRegistroRetornoBalanco(GetPropertyName(() => balancos.NAV), balancos.NAV));
            lista.Add(CriarRegistroRetornoBalanco(GetPropertyName(() => balancos.NEOS), balancos.NEOS));
            lista.Add(CriarRegistroRetornoBalanco(GetPropertyName(() => balancos.NMC), balancos.NMC));
            lista.Add(CriarRegistroRetornoBalanco(GetPropertyName(() => balancos.NOTE), balancos.NOTE));
            lista.Add(CriarRegistroRetornoBalanco(GetPropertyName(() => balancos.NXC), balancos.NXC));
            lista.Add(CriarRegistroRetornoBalanco(GetPropertyName(() => balancos.NXT), balancos.NXT));
            lista.Add(CriarRegistroRetornoBalanco(GetPropertyName(() => balancos.OMNI), balancos.OMNI));
            lista.Add(CriarRegistroRetornoBalanco(GetPropertyName(() => balancos.PASC), balancos.PASC));
            lista.Add(CriarRegistroRetornoBalanco(GetPropertyName(() => balancos.PINK), balancos.PINK));
            lista.Add(CriarRegistroRetornoBalanco(GetPropertyName(() => balancos.POT), balancos.POT));
            lista.Add(CriarRegistroRetornoBalanco(GetPropertyName(() => balancos.PPC), balancos.PPC));
            lista.Add(CriarRegistroRetornoBalanco(GetPropertyName(() => balancos.RADS), balancos.RADS));
            lista.Add(CriarRegistroRetornoBalanco(GetPropertyName(() => balancos.REP), balancos.REP));
            lista.Add(CriarRegistroRetornoBalanco(GetPropertyName(() => balancos.RIC), balancos.RIC));
            lista.Add(CriarRegistroRetornoBalanco(GetPropertyName(() => balancos.SBD), balancos.SBD));
            lista.Add(CriarRegistroRetornoBalanco(GetPropertyName(() => balancos.SC), balancos.SC));
            lista.Add(CriarRegistroRetornoBalanco(GetPropertyName(() => balancos.SJCX), balancos.SJCX));
            lista.Add(CriarRegistroRetornoBalanco(GetPropertyName(() => balancos.STEEM), balancos.STEEM));
            lista.Add(CriarRegistroRetornoBalanco(GetPropertyName(() => balancos.STR), balancos.STR));
            lista.Add(CriarRegistroRetornoBalanco(GetPropertyName(() => balancos.STRAT), balancos.STRAT));
            lista.Add(CriarRegistroRetornoBalanco(GetPropertyName(() => balancos.SYS), balancos.SYS));
            lista.Add(CriarRegistroRetornoBalanco(GetPropertyName(() => balancos.USDT), balancos.USDT));
            lista.Add(CriarRegistroRetornoBalanco(GetPropertyName(() => balancos.VIA), balancos.VIA));
            lista.Add(CriarRegistroRetornoBalanco(GetPropertyName(() => balancos.VRC), balancos.VRC));
            lista.Add(CriarRegistroRetornoBalanco(GetPropertyName(() => balancos.VTC), balancos.VTC));
            lista.Add(CriarRegistroRetornoBalanco(GetPropertyName(() => balancos.XBC), balancos.XBC));
            lista.Add(CriarRegistroRetornoBalanco(GetPropertyName(() => balancos.XCP), balancos.XCP));
            lista.Add(CriarRegistroRetornoBalanco(GetPropertyName(() => balancos.XEM), balancos.XEM));
            lista.Add(CriarRegistroRetornoBalanco(GetPropertyName(() => balancos.XMR), balancos.XMR));
            lista.Add(CriarRegistroRetornoBalanco(GetPropertyName(() => balancos.XPM), balancos.XPM));
            lista.Add(CriarRegistroRetornoBalanco(GetPropertyName(() => balancos.XRP), balancos.XRP));
            lista.Add(CriarRegistroRetornoBalanco(GetPropertyName(() => balancos.XVC), balancos.XVC));
            lista.Add(CriarRegistroRetornoBalanco(GetPropertyName(() => balancos.ZEC), balancos.ZEC));
            lista.Add(CriarRegistroRetornoBalanco(GetPropertyName(() => balancos.ZRX), balancos.ZRX));
            lista = lista.Where(r => r != null && r.Quantidade > 0).ToList();
            return lista;
        }

        #region Util

        private PoloniexQuantidadeEntidade ObterBalancoConta()
        {
            if (CmCache.Obter().ContemItem(ApiKey + "Balanco"))
            {
                return CmCache.Obter().ObterItem<PoloniexQuantidadeEntidade>(ApiKey + "Balanco");
            }
            string resultadoConsulta = ExecutarConsultaAutenticada("returnCompleteBalances");
            var retorno = JsonConvert.DeserializeObject<PoloniexQuantidadeEntidade>(resultadoConsulta);
            CmCache.Obter().AdicionarItem(ApiKey + "Balanco", retorno, CmCache.TEMPO_CACHE_5_MINUTOS);
            return retorno;
        }

        private HistoricoPoloniex ObterHistoricoPoloniex()
        {
            if (CmCache.Obter().ContemItem(ApiKey + "Historico"))
            {
                return CmCache.Obter().ObterItem<HistoricoPoloniex>(ApiKey + "Historico");
            }
            string resultadoConsulta = ExecutarConsultaAutenticada("returnTradeHistory");
            var retorno = JsonConvert.DeserializeObject<HistoricoPoloniex>(resultadoConsulta);
            CmCache.Obter().AdicionarItem(ApiKey + "Historico", retorno, CmCache.TEMPO_CACHE_30_MINUTOS);
            return retorno;
        }

        private string ExecutarConsultaAutenticada(string complementoUrl)
        {
            string url = "https://poloniex.com/tradingApi";
            int unixTimestampInicio = (int)(new DateTime(2017, 01, 01).Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            int unixTimestampFinal = (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            string parametros = $"command={complementoUrl}&nonce={Nonce}&currencyPair=all&start={unixTimestampInicio}&end={unixTimestampFinal}";
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                client.Headers["Key"] = ApiKey;
                client.Headers["Sign"] = ObterHMAC512(parametros, ApiSecret);
                string ret = client.UploadString(url, "POST", parametros);
                Nonce = Nonce + 1;
                return ret;
            }
        }

        private IEnumerable<OrdemMoedaEntidade> CriarRegistroOrdem(string nome, HistoricoOrdemPoloniex[] historicos)
        {
            List<OrdemMoedaEntidade> lista = new List<OrdemMoedaEntidade>();
            if (historicos == null)
            {
                return lista;
            }
            TipoCrypto tipoCrypto = TipoCrypto.ObterPorSigla(nome.Split('_')[1]);
            foreach (var ordem in historicos)
            {
                string tipoOrdem = ordem.TipoOrdem;
                if (tipoOrdem == TipoOrdem.Compra)
                {
                    if (!lista.Any())
                    {
                        lista.Add(new OrdemMoedaEntidade()
                        {
                            Moeda = tipoCrypto,
                            Exchange = TipoExchange.Poloniex,
                            QuantidadeInvestida = 0,
                            QuantidadeMoeda = 0
                        });
                    }
                    lista.FirstOrDefault().QuantidadeInvestida += (ordem.total);
                    lista.FirstOrDefault().QuantidadeMoeda += (ordem.amount);
                }
            }
            return lista;
        }

        private CotacaoMoedaEntidade CriarRegistroCotacao(TipoCrypto tipo, double valorUnidade)
        {
            return new CotacaoMoedaEntidade()
            {
                Exchange = ObterTipo(),
                Tipo = tipo,
                ValorUnidadeEmBitcoin = valorUnidade
            };
        }

        private BalancoMoedaEntidade CriarRegistroRetornoBalanco(string nome, PoloniexQuantidadeEntidade.PoloniexQuantidadeEntidadeMoeda valor)
        {
            if (valor != null)
            {
                double valorDisponivel = Convert.ToDouble(valor.available, CultureInfo.CreateSpecificCulture("en-US"));
                double valorEmOrdens = Convert.ToDouble(valor.onOrders, CultureInfo.CreateSpecificCulture("en-US"));
                double quantidadeUnidadesAtual = valorEmOrdens + valorDisponivel;
                return new BalancoMoedaEntidade()
                {
                    Exchange = TipoExchange.Poloniex,
                    Moeda = TipoCrypto.ObterPorSigla(nome),
                    Quantidade = quantidadeUnidadesAtual
                };
            }
            return null;
        }

        public class HistoricoPoloniex
        {
            public HistoricoOrdemPoloniex[] BTC_AMP { get; set; }
            public HistoricoOrdemPoloniex[] BTC_ARDR { get; set; }
            public HistoricoOrdemPoloniex[] BTC_BCH { get; set; }
            public HistoricoOrdemPoloniex[] BTC_BCN { get; set; }
            public HistoricoOrdemPoloniex[] BTC_BCY { get; set; }
            public HistoricoOrdemPoloniex[] BTC_BELA { get; set; }
            public HistoricoOrdemPoloniex[] BTC_BLK { get; set; }
            public HistoricoOrdemPoloniex[] BTC_BTC { get; set; }
            public HistoricoOrdemPoloniex[] BTC_BTCD { get; set; }
            public HistoricoOrdemPoloniex[] BTC_BTM { get; set; }
            public HistoricoOrdemPoloniex[] BTC_BTS { get; set; }
            public HistoricoOrdemPoloniex[] BTC_BURST { get; set; }
            public HistoricoOrdemPoloniex[] BTC_CLAM { get; set; }
            public HistoricoOrdemPoloniex[] BTC_DASH { get; set; }
            public HistoricoOrdemPoloniex[] BTC_DCR { get; set; }
            public HistoricoOrdemPoloniex[] BTC_DGB { get; set; }
            public HistoricoOrdemPoloniex[] BTC_DOGE { get; set; }
            public HistoricoOrdemPoloniex[] BTC_EMC2 { get; set; }
            public HistoricoOrdemPoloniex[] BTC_ETC { get; set; }
            public HistoricoOrdemPoloniex[] BTC_ETH { get; set; }
            public HistoricoOrdemPoloniex[] BTC_EXP { get; set; }
            public HistoricoOrdemPoloniex[] BTC_FCT { get; set; }
            public HistoricoOrdemPoloniex[] BTC_FLDC { get; set; }
            public HistoricoOrdemPoloniex[] BTC_FLO { get; set; }
            public HistoricoOrdemPoloniex[] BTC_GAME { get; set; }
            public HistoricoOrdemPoloniex[] BTC_GNO { get; set; }
            public HistoricoOrdemPoloniex[] BTC_GNT { get; set; }
            public HistoricoOrdemPoloniex[] BTC_GRC { get; set; }
            public HistoricoOrdemPoloniex[] BTC_HUC { get; set; }
            public HistoricoOrdemPoloniex[] BTC_LBC { get; set; }
            public HistoricoOrdemPoloniex[] BTC_LSK { get; set; }
            public HistoricoOrdemPoloniex[] BTC_LTC { get; set; }
            public HistoricoOrdemPoloniex[] BTC_MAID { get; set; }
            public HistoricoOrdemPoloniex[] BTC_NAUT { get; set; }
            public HistoricoOrdemPoloniex[] BTC_NAV { get; set; }
            public HistoricoOrdemPoloniex[] BTC_NEOS { get; set; }
            public HistoricoOrdemPoloniex[] BTC_NMC { get; set; }
            public HistoricoOrdemPoloniex[] BTC_NOTE { get; set; }
            public HistoricoOrdemPoloniex[] BTC_NXC { get; set; }
            public HistoricoOrdemPoloniex[] BTC_NXT { get; set; }
            public HistoricoOrdemPoloniex[] BTC_OMNI { get; set; }
            public HistoricoOrdemPoloniex[] BTC_PASC { get; set; }
            public HistoricoOrdemPoloniex[] BTC_PINK { get; set; }
            public HistoricoOrdemPoloniex[] BTC_POT { get; set; }
            public HistoricoOrdemPoloniex[] BTC_PPC { get; set; }
            public HistoricoOrdemPoloniex[] BTC_RADS { get; set; }
            public HistoricoOrdemPoloniex[] BTC_REP { get; set; }
            public HistoricoOrdemPoloniex[] BTC_RIC { get; set; }
            public HistoricoOrdemPoloniex[] BTC_SBD { get; set; }
            public HistoricoOrdemPoloniex[] BTC_SC { get; set; }
            public HistoricoOrdemPoloniex[] BTC_SJCX { get; set; }
            public HistoricoOrdemPoloniex[] BTC_STEEM { get; set; }
            public HistoricoOrdemPoloniex[] BTC_STR { get; set; }
            public HistoricoOrdemPoloniex[] BTC_STRAT { get; set; }
            public HistoricoOrdemPoloniex[] BTC_SYS { get; set; }
            public HistoricoOrdemPoloniex[] BTC_USDT { get; set; }
            public HistoricoOrdemPoloniex[] BTC_VIA { get; set; }
            public HistoricoOrdemPoloniex[] BTC_VRC { get; set; }
            public HistoricoOrdemPoloniex[] BTC_VTC { get; set; }
            public HistoricoOrdemPoloniex[] BTC_XBC { get; set; }
            public HistoricoOrdemPoloniex[] BTC_XCP { get; set; }
            public HistoricoOrdemPoloniex[] BTC_XEM { get; set; }
            public HistoricoOrdemPoloniex[] BTC_XMR { get; set; }
            public HistoricoOrdemPoloniex[] BTC_XPM { get; set; }
            public HistoricoOrdemPoloniex[] BTC_XRP { get; set; }
            public HistoricoOrdemPoloniex[] BTC_XVC { get; set; }
            public HistoricoOrdemPoloniex[] BTC_ZEC { get; set; }
            public HistoricoOrdemPoloniex[] BTC_ZRX { get; set; }
        }

        public class HistoricoOrdemPoloniex
        {
            public string globalTradeID { get; set; }
            public string tradeID { get; set; }
            public string date { get; set; }
            public double rate { get; set; }
            public double amount { get; set; }
            public double total { get; set; }
            public double fee { get; set; }
            public string type { get; set; }
            
            public string TipoOrdem
            {
                get
                {
                    if (type == "sell")
                    {
                        return Util.Enum.TipoOrdem.Venda;
                    }
                    else if (type == "buy")
                    {
                        return Util.Enum.TipoOrdem.Compra;
                    }
                    return null;
                }
            }
        }

        #endregion
    }
}

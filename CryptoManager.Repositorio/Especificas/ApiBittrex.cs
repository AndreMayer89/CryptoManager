using CryptoManager.Entidades;
using CryptoManager.Entidades.Especificas;
using CryptoManager.Util;
using CryptoManager.Util.Cache;
using CryptoManager.Util.Enum;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace CryptoManager.Repositorio.Especificas
{
    public class ApiBittrex : ApiBase<BittrexEntidade>, IApiExchange
    {
        private string ApiKey { get; set; }
        private string ApiSecret { get; set; }
        private long Nonce { get; set; }

        public ApiBittrex(string apiKey, string apiSecret, long nonce)
        {
            ApiKey = apiKey != null ? apiKey.Trim() : string.Empty;
            ApiSecret = apiSecret != null ? apiSecret.Trim() : string.Empty;
            Nonce = nonce;
        }

        public TipoExchange ObterTipo()
        {
            return TipoExchange.Bittrex;
        }

        public IEnumerable<CotacaoMoedaEntidade> Cotar()
        {
            BittrexEntidade retorno = Cotar("https://bittrex.com/api/v1.1/public/getmarketsummaries");
            List<CotacaoMoedaEntidade> lista = new List<CotacaoMoedaEntidade>();
            lista.Add(CriarRegistroCotacao(TipoCrypto.BasicAttentionToken, Convert.ToDouble(retorno.result.FirstOrDefault(r => r.MarketName == "BTC-BAT").Last)));
            lista.Add(CriarRegistroCotacao(TipoCrypto.WeTrust, Convert.ToDouble(retorno.result.FirstOrDefault(r => r.MarketName == "BTC-TRST").Last)));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Byteball, Convert.ToDouble(retorno.result.FirstOrDefault(r => r.MarketName == "BTC-GBYTE").Last)));
            lista.Add(CriarRegistroCotacao(TipoCrypto.FirstBlood, Convert.ToDouble(retorno.result.FirstOrDefault(r => r.MarketName == "BTC-1ST").Last)));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Golem, Convert.ToDouble(retorno.result.FirstOrDefault(r => r.MarketName == "BTC-GNT").Last)));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Ethereum, Convert.ToDouble(retorno.result.FirstOrDefault(r => r.MarketName == "BTC-ETH").Last)));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Stratis, Convert.ToDouble(retorno.result.FirstOrDefault(r => r.MarketName == "BTC-STRAT").Last)));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Verge, Convert.ToDouble(retorno.result.FirstOrDefault(r => r.MarketName == "BTC-XVG").Last)));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Digibyte, Convert.ToDouble(retorno.result.FirstOrDefault(r => r.MarketName == "BTC-DGB").Last)));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Siacoin, Convert.ToDouble(retorno.result.FirstOrDefault(r => r.MarketName == "BTC-SC").Last)));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Ardor, Convert.ToDouble(retorno.result.FirstOrDefault(r => r.MarketName == "BTC-ARDR").Last)));
            lista.Add(CriarRegistroCotacao(TipoCrypto.NXT, Convert.ToDouble(retorno.result.FirstOrDefault(r => r.MarketName == "BTC-NXT").Last)));
            lista.Add(CriarRegistroCotacao(TipoCrypto.FoldingCoin, Convert.ToDouble(retorno.result.FirstOrDefault(r => r.MarketName == "BTC-FLDC").Last)));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Gnosis, Convert.ToDouble(retorno.result.FirstOrDefault(r => r.MarketName == "BTC-GNO").Last)));
            lista.Add(CriarRegistroCotacao(TipoCrypto.EdgeLess, Convert.ToDouble(retorno.result.FirstOrDefault(r => r.MarketName == "BTC-EDG").Last)));
            lista.Add(CriarRegistroCotacao(TipoCrypto.iExec, Convert.ToDouble(retorno.result.FirstOrDefault(r => r.MarketName == "BTC-RLC").Last)));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Factom, Convert.ToDouble(retorno.result.FirstOrDefault(r => r.MarketName == "BTC-FCT").Last)));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Augur, Convert.ToDouble(retorno.result.FirstOrDefault(r => r.MarketName == "BTC-REP").Last)));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Lumen, Convert.ToDouble(retorno.result.FirstOrDefault(r => r.MarketName == "BTC-XLM").Last)));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Antshares, Convert.ToDouble(retorno.result.FirstOrDefault(r => r.MarketName == "BTC-ANS").Last)));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Litecoin, Convert.ToDouble(retorno.result.FirstOrDefault(r => r.MarketName == "BTC-LTC").Last)));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Ripple, Convert.ToDouble(retorno.result.FirstOrDefault(r => r.MarketName == "BTC-XRP").Last)));
            lista.Add(CriarRegistroCotacao(TipoCrypto.EthereumClassic, Convert.ToDouble(retorno.result.FirstOrDefault(r => r.MarketName == "BTC-ETC").Last)));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Komodo, Convert.ToDouble(retorno.result.FirstOrDefault(r => r.MarketName == "BTC-KMD").Last)));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Dash, Convert.ToDouble(retorno.result.FirstOrDefault(r => r.MarketName == "BTC-DASH").Last)));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Decred, Convert.ToDouble(retorno.result.FirstOrDefault(r => r.MarketName == "BTC-DCR").Last)));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Lisk, Convert.ToDouble(retorno.result.FirstOrDefault(r => r.MarketName == "BTC-LSK").Last)));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Bitshares, Convert.ToDouble(retorno.result.FirstOrDefault(r => r.MarketName == "BTC-BTS").Last)));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Zcash, Convert.ToDouble(retorno.result.FirstOrDefault(r => r.MarketName == "BTC-ZEC").Last)));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Monero, Convert.ToDouble(retorno.result.FirstOrDefault(r => r.MarketName == "BTC-XMR").Last)));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Dogecoin, Convert.ToDouble(retorno.result.FirstOrDefault(r => r.MarketName == "BTC-DOGE").Last)));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Waves, Convert.ToDouble(retorno.result.FirstOrDefault(r => r.MarketName == "BTC-WAVES").Last)));
            lista.Add(CriarRegistroCotacao(TipoCrypto.QuantumResistantLedger, Convert.ToDouble(retorno.result.FirstOrDefault(r => r.MarketName == "BTC-QRL").Last)));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Voxels, Convert.ToDouble(retorno.result.FirstOrDefault(r => r.MarketName == "BTC-VOX").Last)));
            lista.Add(CriarRegistroCotacao(TipoCrypto.NEM, Convert.ToDouble(retorno.result.FirstOrDefault(r => r.MarketName == "BTC-XEM").Last)));
            lista.Add(CriarRegistroCotacao(TipoCrypto.ReddCoin, Convert.ToDouble(retorno.result.FirstOrDefault(r => r.MarketName == "BTC-RDD").Last)));
            lista.Add(CriarRegistroCotacao(TipoCrypto.LBRYCredits, Convert.ToDouble(retorno.result.FirstOrDefault(r => r.MarketName == "BTC-LBC").Last)));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Ubiq, Convert.ToDouble(retorno.result.FirstOrDefault(r => r.MarketName == "BTC-UBQ").Last)));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Aragon, Convert.ToDouble(retorno.result.FirstOrDefault(r => r.MarketName == "BTC-ANT").Last)));
            lista.Add(CriarRegistroCotacao(TipoCrypto.GameCredits, Convert.ToDouble(retorno.result.FirstOrDefault(r => r.MarketName == "BTC-GAME").Last)));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Wings, Convert.ToDouble(retorno.result.FirstOrDefault(r => r.MarketName == "BTC-WINGS").Last)));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Humaniq, Convert.ToDouble(retorno.result.FirstOrDefault(r => r.MarketName == "BTC-HMQ").Last)));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Counterparty, Convert.ToDouble(retorno.result.FirstOrDefault(r => r.MarketName == "BTC-XCP").Last)));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Steem, Convert.ToDouble(retorno.result.FirstOrDefault(r => r.MarketName == "BTC-STEEM").Last)));
            lista.Add(CriarRegistroCotacao(TipoCrypto.ZCoin, Convert.ToDouble(retorno.result.FirstOrDefault(r => r.MarketName == "BTC-XZC").Last)));
            lista.Add(CriarRegistroCotacao(TipoCrypto.SwarmCity, Convert.ToDouble(retorno.result.FirstOrDefault(r => r.MarketName == "BTC-SWT").Last)));
            lista.Add(CriarRegistroCotacao(TipoCrypto.PIVX, Convert.ToDouble(retorno.result.FirstOrDefault(r => r.MarketName == "BTC-PIVX").Last)));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Influxcoin, Convert.ToDouble(retorno.result.FirstOrDefault(r => r.MarketName == "BTC-INFX").Last)));
            lista.Add(CriarRegistroCotacao(TipoCrypto.BitcoinDark, Convert.ToDouble(retorno.result.FirstOrDefault(r => r.MarketName == "BTC-BTCD").Last)));
            lista.Add(CriarRegistroCotacao(TipoCrypto.ZClassic, Convert.ToDouble(retorno.result.FirstOrDefault(r => r.MarketName == "BTC-ZCL").Last)));
            lista.Add(CriarRegistroCotacao(TipoCrypto.VeriCoin, Convert.ToDouble(retorno.result.FirstOrDefault(r => r.MarketName == "BTC-VRC").Last)));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Ark, Convert.ToDouble(retorno.result.FirstOrDefault(r => r.MarketName == "BTC-ARK").Last)));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Peercoin, Convert.ToDouble(retorno.result.FirstOrDefault(r => r.MarketName == "BTC-PPC").Last)));
            lista.Add(CriarRegistroCotacao(TipoCrypto.DigitalNote, Convert.ToDouble(retorno.result.FirstOrDefault(r => r.MarketName == "BTC-XDN").Last)));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Credibit, Convert.ToDouble(retorno.result.FirstOrDefault(r => r.MarketName == "BTC-CRB").Last)));
            lista.Add(CriarRegistroCotacao(TipoCrypto.ZenCash, Convert.ToDouble(retorno.result.FirstOrDefault(r => r.MarketName == "BTC-ZEN").Last)));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Syscoin, Convert.ToDouble(retorno.result.FirstOrDefault(r => r.MarketName == "BTC-SYS").Last)));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Viacoin, Convert.ToDouble(retorno.result.FirstOrDefault(r => r.MarketName == "BTC-VIA").Last)));
            lista.Add(CriarRegistroCotacao(TipoCrypto.SingularDTV, Convert.ToDouble(retorno.result.FirstOrDefault(r => r.MarketName == "BTC-SNGLS").Last)));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Chronobank, Convert.ToDouble(retorno.result.FirstOrDefault(r => r.MarketName == "BTC-TIME").Last)));
            lista.Add(CriarRegistroCotacao(TipoCrypto.TokenCard, Convert.ToDouble(retorno.result.FirstOrDefault(r => r.MarketName == "BTC-TKN").Last)));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Bancor, Convert.ToDouble(retorno.result.FirstOrDefault(r => r.MarketName == "BTC-BNT").Last)));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Status, Convert.ToDouble(retorno.result.FirstOrDefault(r => r.MarketName == "BTC-SNT").Last)));

            lista.Add(CriarRegistroCotacao(TipoCrypto.Bitcoin, 1));
            return lista;
        }

        public IEnumerable<BalancoMoedaEntidade> ListarBalancoConta()
        {
            List<BalancoMoedaEntidade> lista = new List<BalancoMoedaEntidade>();
            var balanco = ObterBalancoConta();
            if (balanco == null || balanco.result == null)
            {
                throw new CryptoManagerException("Erro na consulta de balanço");
            }
            lista.AddRange(balanco.result.Where(r => r.Balance > 0).Select(r => CriarRegistroRetornoBalanco(r.Currency, r.Balance)));
            lista = lista.Where(r => r.Quantidade > 0).ToList();
            return lista;
        }

        public IEnumerable<OrdemMoedaEntidade> ListarHistoricoOrdem()
        {
            var historico = ObterHistoricoBittrex();
            List<OrdemMoedaEntidade> retorno = new List<OrdemMoedaEntidade>();
            historico.result = historico.result.Where(r => r.Exchange.StartsWith("BTC-")).ToArray();
            foreach (var ordem in historico.result)
            {
                string siglaCrypto = ordem.Exchange.Split('-')[1];
                string tipoOrdem = ObterTipoOrdem(ordem);
                if (!retorno.Any(q => q.Moeda != null && q.Moeda.Sigla == siglaCrypto))
                {
                    retorno.Add(new OrdemMoedaEntidade()
                    {
                        Moeda = TipoCrypto.ObterPorSigla(siglaCrypto),
                        Exchange = TipoExchange.Bittrex
                    });
                }
                if (tipoOrdem == TipoOrdem.Compra)
                {
                    retorno.FirstOrDefault(q => q.Moeda != null && q.Moeda.Sigla == siglaCrypto).QuantidadeInvestida += (ordem.Price);
                    retorno.FirstOrDefault(q => q.Moeda != null && q.Moeda.Sigla == siglaCrypto).QuantidadeMoeda += (ordem.Quantity - ordem.QuantityRemaining);
                }
            }
            return retorno;
        }

        #region Util

        private HistoricoBittrex ObterHistoricoBittrex()
        {
            if (CmCache.Obter().ContemItem(ApiKey + "Historico"))
            {
                return CmCache.Obter().ObterItem<HistoricoBittrex>(ApiKey + "Historico");
            }
            string resultadoConsulta = ExecutarConsultaAutenticada("getorderhistory");
            var retorno = JsonConvert.DeserializeObject<HistoricoBittrex>(resultadoConsulta);
            CmCache.Obter().AdicionarItem(ApiKey + "Historico", retorno, CmCache.TEMPO_CACHE_30_MINUTOS);
            return retorno;
        }

        private BittrexQuantidadeEntidade ObterBalancoConta()
        {
            if (CmCache.Obter().ContemItem(ApiKey + "Balanco"))
            {
                return CmCache.Obter().ObterItem<BittrexQuantidadeEntidade>(ApiKey + "Balanco");
            }
            string resultadoConsulta = ExecutarConsultaAutenticada("getbalances");
            var retorno = JsonConvert.DeserializeObject<BittrexQuantidadeEntidade>(resultadoConsulta);
            CmCache.Obter().AdicionarItem(ApiKey + "Balanco", retorno, CmCache.TEMPO_CACHE_5_MINUTOS);
            return retorno;
        }

        private string ExecutarConsultaAutenticada(string complementoUrl)
        {
            string urlBase = $"https://bittrex.com/api/v1.1/account/{complementoUrl}?";
            string parametros = $"apikey={ApiKey}&nonce={Nonce}";
            string url = urlBase + parametros;
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                client.Headers["apisign"] = ObterHMAC512(url, ApiSecret);
                var retorno = client.DownloadString(url);
                Nonce = Nonce + 1;
                return retorno;
            }
        }

        private CotacaoMoedaEntidade CriarRegistroCotacao(TipoCrypto tipo, double valorUnidade)
        {
            return new CotacaoMoedaEntidade()
            {
                Exchange = TipoExchange.Bittrex,
                Tipo = tipo,
                ValorUnidadeEmBitcoin = valorUnidade
            };
        }

        private BalancoMoedaEntidade CriarRegistroRetornoBalanco(string currency, double balance)
        {
            return new BalancoMoedaEntidade()
            {
                Exchange = TipoExchange.Bittrex,
                Moeda = TipoCrypto.ObterPorSigla(currency),
                Quantidade = balance
            };
        }

        private static string ObterTipoOrdem(HistoricoOrdemBittrex ordem)
        {
            if (ordem.OrderType == "LIMIT_SELL")
            {
                return TipoOrdem.Venda;
            }
            else if (ordem.OrderType == "LIMIT_BUY")
            {
                return TipoOrdem.Compra;
            }
            return null;
        }

        public class HistoricoBittrex
        {
            public HistoricoOrdemBittrex[] result { get; set; }
        }

        public class HistoricoOrdemBittrex
        {
            public string OrderUuid { get; set; }
            public string Exchange { get; set; }
            public string TimeStamp { get; set; }
            public string OrderType { get; set; }

            public double Limit { get; set; }
            public double Quantity { get; set; }
            public double QuantityRemaining { get; set; }
            public double Commission { get; set; }
            public double Price { get; set; }
            public double? PricePerUnit { get; set; }
        }

        #endregion
    }
}

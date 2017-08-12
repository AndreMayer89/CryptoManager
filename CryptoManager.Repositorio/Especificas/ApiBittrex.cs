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
            lista.Add(CriarRegistroCotacao(TipoCrypto.BasicAttentionToken, retorno, "BTC-BAT"));
            lista.Add(CriarRegistroCotacao(TipoCrypto.WeTrust, retorno, "BTC-TRST"));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Byteball, retorno, "BTC-GBYTE"));
            lista.Add(CriarRegistroCotacao(TipoCrypto.FirstBlood, retorno, "BTC-1ST"));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Golem, retorno, "BTC-GNT"));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Ethereum, retorno, "BTC-ETH"));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Stratis, retorno, "BTC-STRAT"));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Verge, retorno, "BTC-XVG"));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Digibyte, retorno, "BTC-DGB"));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Siacoin, retorno, "BTC-SC"));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Ardor, retorno, "BTC-ARDR"));
            lista.Add(CriarRegistroCotacao(TipoCrypto.NXT, retorno, "BTC-NXT"));
            lista.Add(CriarRegistroCotacao(TipoCrypto.FoldingCoin, retorno, "BTC-FLDC"));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Gnosis, retorno, "BTC-GNO"));
            lista.Add(CriarRegistroCotacao(TipoCrypto.EdgeLess, retorno, "BTC-EDG"));
            lista.Add(CriarRegistroCotacao(TipoCrypto.iExec, retorno, "BTC-RLC"));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Factom, retorno, "BTC-FCT"));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Augur, retorno, "BTC-REP"));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Lumen, retorno, "BTC-XLM"));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Litecoin, retorno, "BTC-LTC"));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Ripple, retorno, "BTC-XRP"));
            lista.Add(CriarRegistroCotacao(TipoCrypto.EthereumClassic, retorno, "BTC-ETC"));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Komodo, retorno, "BTC-KMD"));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Dash, retorno, "BTC-DASH"));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Decred, retorno, "BTC-DCR"));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Lisk, retorno, "BTC-LSK"));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Bitshares, retorno, "BTC-BTS"));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Zcash, retorno, "BTC-ZEC"));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Monero, retorno, "BTC-XMR"));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Dogecoin, retorno, "BTC-DOGE"));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Waves, retorno, "BTC-WAVES"));
            lista.Add(CriarRegistroCotacao(TipoCrypto.QuantumResistantLedger, retorno, "BTC-QRL"));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Voxels, retorno, "BTC-VOX"));
            lista.Add(CriarRegistroCotacao(TipoCrypto.NEM, retorno, "BTC-XEM"));
            lista.Add(CriarRegistroCotacao(TipoCrypto.ReddCoin, retorno, "BTC-RDD"));
            lista.Add(CriarRegistroCotacao(TipoCrypto.LBRYCredits, retorno, "BTC-LBC"));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Ubiq, retorno, "BTC-UBQ"));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Aragon, retorno, "BTC-ANT"));
            lista.Add(CriarRegistroCotacao(TipoCrypto.GameCredits, retorno, "BTC-GAME"));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Wings, retorno, "BTC-WINGS"));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Humaniq, retorno, "BTC-HMQ"));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Counterparty, retorno, "BTC-XCP"));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Steem, retorno, "BTC-STEEM"));
            lista.Add(CriarRegistroCotacao(TipoCrypto.ZCoin, retorno, "BTC-XZC"));
            lista.Add(CriarRegistroCotacao(TipoCrypto.SwarmCity, retorno, "BTC-SWT"));
            lista.Add(CriarRegistroCotacao(TipoCrypto.PIVX, retorno, "BTC-PIVX"));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Influxcoin, retorno, "BTC-INFX"));
            lista.Add(CriarRegistroCotacao(TipoCrypto.BitcoinDark, retorno, "BTC-BTCD"));
            lista.Add(CriarRegistroCotacao(TipoCrypto.ZClassic, retorno, "BTC-ZCL"));
            lista.Add(CriarRegistroCotacao(TipoCrypto.VeriCoin, retorno, "BTC-VRC"));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Ark, retorno, "BTC-ARK"));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Peercoin, retorno, "BTC-PPC"));
            lista.Add(CriarRegistroCotacao(TipoCrypto.DigitalNote, retorno, "BTC-XDN"));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Credibit, retorno, "BTC-CRB"));
            lista.Add(CriarRegistroCotacao(TipoCrypto.ZenCash, retorno, "BTC-ZEN"));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Syscoin, retorno, "BTC-SYS"));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Viacoin, retorno, "BTC-VIA"));
            lista.Add(CriarRegistroCotacao(TipoCrypto.SingularDTV, retorno, "BTC-SNGLS"));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Chronobank, retorno, "BTC-TIME"));
            lista.Add(CriarRegistroCotacao(TipoCrypto.TokenCard, retorno, "BTC-TKN"));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Bancor, retorno, "BTC-BNT"));
            lista.Add(CriarRegistroCotacao(TipoCrypto.Status, retorno, "BTC-SNT"));
            lista.Add(CriarRegistroCotacao(TipoCrypto.BitcoinCash, retorno, "BTC-BCC"));

            lista.Add(new CotacaoMoedaEntidade()
            {
                Exchange = TipoExchange.Bittrex,
                Tipo = TipoCrypto.Bitcoin,
                ValorUnidadeEmBitcoin = 1
            });
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

        private CotacaoMoedaEntidade CriarRegistroCotacao(TipoCrypto tipo, BittrexEntidade cotacoes, string codigoPar)
        {
            try
            {
                double valorUnidade = Convert.ToDouble(cotacoes.result.FirstOrDefault(r => r.MarketName == codigoPar).Last);
                return new CotacaoMoedaEntidade()
                {
                    Exchange = TipoExchange.Bittrex,
                    Tipo = tipo,
                    ValorUnidadeEmBitcoin = valorUnidade
                };
            }
            catch (Exception)
            {
                return null;
            }
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

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
using System.Text;

namespace CryptoManager.Repositorio.Especificas
{
    public class ApiBitfinex : ApiBase<object[][]>, IApiExchange
    {
        private string ApiKey { get; set; }
        private string ApiSecret { get; set; }
        private long Nonce { get; set; }

        public ApiBitfinex(string apiKey, string apiSecret, long nonce)
        {
            ApiKey = apiKey != null ? apiKey.Trim() : string.Empty;
            ApiSecret = apiSecret != null ? apiSecret.Trim() : string.Empty;
            Nonce = nonce;
        }

        public TipoExchange ObterTipo()
        {
            return TipoExchange.Bitfinex;
        }

        public IEnumerable<CotacaoMoedaEntidade> Cotar()
        {
            object[][] retorno = Cotar("https://api.bitfinex.com/v2/tickers?symbols=tETHBTC,tIOTBTC,tLTCBTC,tETCBTC,tZECBTC,tXMRBTC,tDSHBTC,tBCCBTC,tXRPBTC,tBCHBTC,tOMGBTC,tEOSBTC,tSANBTC,tNEOBTC,tBTGBTC,tBTCUSD");
            List<CotacaoMoedaEntidade> lista = new List<CotacaoMoedaEntidade>();
            lista.Add(CriarRegistroRetorno(TipoCrypto.BitcoinCash, retorno, "tBCHBTC"));
            lista.Add(CriarRegistroRetorno(TipoCrypto.Dash, retorno, "tDSHBTC"));
            lista.Add(CriarRegistroRetorno(TipoCrypto.OmiseGo, retorno, "tEOSBTC"));
            lista.Add(CriarRegistroRetorno(TipoCrypto.EthereumClassic, retorno, "tETCBTC"));
            lista.Add(CriarRegistroRetorno(TipoCrypto.Ethereum, retorno, "tETHBTC"));
            lista.Add(CriarRegistroRetorno(TipoCrypto.IOTA, retorno, "tIOTBTC"));
            lista.Add(CriarRegistroRetorno(TipoCrypto.Litecoin, retorno, "tLTCBTC"));
            lista.Add(CriarRegistroRetorno(TipoCrypto.Zcash, retorno, "tZECBTC"));
            lista.Add(CriarRegistroRetorno(TipoCrypto.Monero, retorno, "tXMRBTC"));
            lista.Add(CriarRegistroRetorno(TipoCrypto.Ripple, retorno, "tXRPBTC"));
            lista.Add(CriarRegistroRetorno(TipoCrypto.OmiseGo, retorno, "tOMGBTC"));
            lista.Add(CriarRegistroRetorno(TipoCrypto.Santiment, retorno, "tSANBTC"));
            lista.Add(CriarRegistroRetorno(TipoCrypto.NEO, retorno, "tNEOBTC"));
            lista.Add(CriarRegistroRetorno(TipoCrypto.BitcoinGold, retorno, "tBTGBTC"));

            double valorUnidadeDolar = Convert.ToDouble(retorno.FirstOrDefault(r => r[0].ToString().Equals("tBTCUSD"))[7]);
            lista.Add(new CotacaoMoedaEntidade() { Exchange = TipoExchange.Bitfinex, Tipo = TipoCrypto.Dolar, ValorUnidadeEmBitcoin = 1 / valorUnidadeDolar });
            lista.Add(new CotacaoMoedaEntidade() { Exchange = ObterTipo(), Tipo = TipoCrypto.Bitcoin, ValorUnidadeEmBitcoin = 1 });
            return lista;
        }

        public IEnumerable<BalancoMoedaEntidade> ListarBalancoConta()
        {
            var balanco = ObterBalancoConta();
            List<BalancoMoedaEntidade> lista = new List<BalancoMoedaEntidade>();
            foreach (var crypto in balanco)
            {
                if (crypto.amount > 0)
                {
                    if (lista.FirstOrDefault(c => c.Moeda.Sigla == crypto.currency.ToUpperInvariant()) != null)
                    {
                        lista.FirstOrDefault(c => c.Moeda.Sigla == crypto.currency.ToUpperInvariant()).Quantidade += crypto.amount;
                    }
                    else
                    {
                        lista.Add(CriarRegistroRetornoBalanco(crypto.currency.ToUpperInvariant(), crypto.amount));
                    }
                }
            }
            return lista;
        }

        public IEnumerable<OrdemMoedaEntidade> ListarHistoricoOrdem()
        {
            List<OrdemMoedaEntidade> lista = new List<OrdemMoedaEntidade>();
            List<HistoricoOrdemBitfinex> historico = new List<HistoricoOrdemBitfinex>();
            historico.AddRange(ObterHistoricoOrdem("ETHBTC", "ETH"));
            historico.AddRange(ObterHistoricoOrdem("IOTBTC", "IOT"));
            foreach (var ordem in historico)
            {
                if (!lista.Any(q => q.Moeda != null && q.Moeda.Sigla == ordem.SiglaCrypto))
                {
                    lista.Add(new OrdemMoedaEntidade()
                    {
                        Moeda = TipoCrypto.ObterPorSigla(ordem.SiglaCrypto),
                        Exchange = TipoExchange.Bitfinex
                    });
                }
                if (ordem.TipoOrdem == TipoOrdem.Compra)
                {
                    double precoPorUnidade = Convert.ToDouble(ordem.price, CultureInfo.CreateSpecificCulture("en-US"));
                    double unidades = Convert.ToDouble(ordem.amount, CultureInfo.CreateSpecificCulture("en-US"));
                    lista.FirstOrDefault(q => q.Moeda != null && q.Moeda.Sigla == ordem.SiglaCrypto)
                        .QuantidadeInvestida += (unidades * precoPorUnidade);
                    lista.FirstOrDefault(q => q.Moeda != null && q.Moeda.Sigla == ordem.SiglaCrypto)
                        .QuantidadeMoeda += unidades;
                }
            }
            return lista;
        }

        #region Util

        private BitfinexQuantidadeEntidade[] ObterBalancoConta()
        {
            if (CmCache.Obter().ContemItem(ApiKey + "Balanco"))
            {
                return CmCache.Obter().ObterItem<BitfinexQuantidadeEntidade[]>(ApiKey + "Balanco");
            }
            string resultadoConsulta = ExecutarConsultaAutenticada("/v1/balances", new BitfinexPostBase());
            var retorno = JsonConvert.DeserializeObject<BitfinexQuantidadeEntidade[]>(resultadoConsulta);
            CmCache.Obter().AdicionarItem(ApiKey + "Balanco", retorno, CmCache.TEMPO_CACHE_5_MINUTOS);
            return retorno;
        }

        private HistoricoOrdemBitfinex[] ObterHistoricoOrdem(string par, string siglaCrypto)
        {
            if (CmCache.Obter().ContemItem(ApiKey + "Historico" + par))
            {
                return CmCache.Obter().ObterItem<HistoricoOrdemBitfinex[]>(ApiKey + "Historico" + par);
            }
            string resultadoConsulta = ExecutarConsultaAutenticada("/v1/mytrades", new BitfinexPostHistorico
            {
                Symbol = par,
                Timestamp = (new DateTime(2017, 01, 01).Subtract(new DateTime(1970, 1, 1))).TotalSeconds.ToString()
            });
            var retorno = JsonConvert.DeserializeObject<HistoricoOrdemBitfinex[]>(resultadoConsulta);
            foreach (var historico in retorno)
            {
                historico.SiglaCrypto = siglaCrypto;
            }
            CmCache.Obter().AdicionarItem(ApiKey + "Historico" + par, retorno, CmCache.TEMPO_CACHE_30_MINUTOS);
            return retorno;
        }

        private string ExecutarConsultaAutenticada(string complementoUrl, BitfinexPostBase dadosPost)
        {
            string urlBase = "https://api.bitfinex.com";
            string urlCompleta = urlBase + complementoUrl;
            dadosPost.Request = complementoUrl;
            dadosPost.Nonce = Nonce.ToString();
            var jsonObj = JsonConvert.SerializeObject(dadosPost);
            var payload = Convert.ToBase64String(Encoding.UTF8.GetBytes(jsonObj));
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                client.Headers["X-BFX-APIKEY"] = ApiKey;
                client.Headers["X-BFX-PAYLOAD"] = payload;
                client.Headers["X-BFX-SIGNATURE"] = ObterHMAC384(payload, ApiSecret);
                string retorno = client.UploadString(urlCompleta, "POST", jsonObj);
                Nonce = Nonce + 1;
                return retorno;
            }
        }

        private BalancoMoedaEntidade CriarRegistroRetornoBalanco(string sigla, double valor)
        {
            return new BalancoMoedaEntidade()
            {
                Exchange = TipoExchange.Bitfinex,
                Moeda = TipoCrypto.ObterPorSigla(sigla),
                Quantidade = valor
            };
        }

        private CotacaoMoedaEntidade CriarRegistroRetorno(TipoCrypto tipo, object[][] retorno, string siglaPar)
        {
            double valorUnidade = Convert.ToDouble(retorno.FirstOrDefault(r => r[0].ToString().Equals(siglaPar))[7]);
            return new CotacaoMoedaEntidade() { Exchange = TipoExchange.Bitfinex, Tipo = tipo, ValorUnidadeEmBitcoin = valorUnidade };
        }

        public class HistoricoOrdemBitfinex
        {
            public string price { get; set; }
            public string amount { get; set; }
            public string type { get; set; }

            public string TipoOrdem
            {
                get
                {
                    if (type == "Sell")
                    {
                        return Util.Enum.TipoOrdem.Venda;
                    }
                    else if (type == "Buy")
                    {
                        return Util.Enum.TipoOrdem.Compra;
                    }
                    return null;
                }
            }

            public string SiglaCrypto { get; internal set; }
        }

        public class BitfinexPostBase
        {
            [JsonProperty("request")]
            public string Request { get; set; }

            [JsonProperty("nonce")]
            public string Nonce { get; set; }
        }

        public class BitfinexPostHistorico : BitfinexPostBase
        {
            [JsonProperty("symbol")]
            public string Symbol { get; set; }

            [JsonProperty("timestamp")]
            public string Timestamp { get; set; }
        }

        #endregion
    }
}

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
using System.Security.Cryptography;
using System.Text;

namespace CryptoManager.Repositorio.Especificas
{
    public class ApiKraken : ApiBase<KrakenEntidade>, IApiExchange
    {
        private string ApiKey { get; set; }
        private string ApiSecret { get; set; }
        private long Nonce { get; set; }

        public ApiKraken(string apiKey, string apiSecret, long nonce)
        {
            ApiKey = apiKey != null ? apiKey.Trim() : string.Empty;
            ApiSecret = apiSecret != null ? apiSecret.Trim() : string.Empty;
            Nonce = nonce;
        }

        public TipoExchange ObterTipo()
        {
            return TipoExchange.Kraken;
        }

        public IEnumerable<CotacaoMoedaEntidade> Cotar()
        {
            KrakenEntidade retorno = Cotar("https://api.kraken.com/0/public/Ticker?pair=ICNXBT,ETHXBT");
            List<CotacaoMoedaEntidade> lista = new List<CotacaoMoedaEntidade>();
            lista.Add(CriarRegistroRetorno(TipoCrypto.Ethereum, retorno.result.XETHXXBT.c[0]));
            lista.Add(CriarRegistroRetorno(TipoCrypto.Iconomi, retorno.result.XICNXXBT.c[0]));
            lista.Add(CriarRegistroRetorno(TipoCrypto.Bitcoin, 1));
            return lista;
        }

        public IEnumerable<BalancoMoedaEntidade> ListarBalancoConta()
        {
            var balanco = ObterBalancoConta();
            List<BalancoMoedaEntidade> lista = new List<BalancoMoedaEntidade>();
            lista.AddRange(balanco.result.Where(r => r.Balance > 0).Select(r => CriarRegistroRetornoBalanco(r.Currency, r.Balance)));
            lista = lista.Where(r => r.Quantidade > 0).ToList();
            return lista;
        }

        public IEnumerable<OrdemMoedaEntidade> ListarHistoricoOrdem()
        {
            var historico = ObterHistoricoOrdem();
            List<OrdemMoedaEntidade> retorno = new List<OrdemMoedaEntidade>();
            foreach (var ordem in historico)
            {
                string siglaCrypto = ObterSigla(ordem.pair);
                string tipoOrdem = ObterTipoOrdem(ordem);
                if (!retorno.Any(q => q.Moeda != null && q.Moeda.Sigla == siglaCrypto))
                {
                    retorno.Add(new OrdemMoedaEntidade()
                    {
                        Moeda = TipoCrypto.ObterPorSigla(siglaCrypto),
                        Exchange = TipoExchange.Kraken
                    });
                }
                if (tipoOrdem == TipoOrdem.Compra)
                {
                    retorno.FirstOrDefault(q => q.Moeda != null && q.Moeda.Sigla == siglaCrypto)
                        .QuantidadeInvestida += Convert.ToDouble(ordem.cost, CultureInfo.CreateSpecificCulture("en-US"));
                    retorno.FirstOrDefault(q => q.Moeda != null && q.Moeda.Sigla == siglaCrypto)
                        .QuantidadeMoeda += Convert.ToDouble(ordem.vol, CultureInfo.CreateSpecificCulture("en-US"));
                }
            }
            return retorno;
        }

        #region Util

        private KrakenQuantidadeEntidade ObterBalancoConta()
        {
            if (CmCache.Obter().ContemItem(ApiKey + "Balanco"))
            {
                return CmCache.Obter().ObterItem<KrakenQuantidadeEntidade>(ApiKey + "Balanco");
            }
            string complementoUrl = "/0/private/Balance";
            string resultadoConsulta = ExecutarConsultaAutenticada(complementoUrl);
            var retorno = JsonConvert.DeserializeObject<KrakenQuantidadeEntidade>(resultadoConsulta);
            CmCache.Obter().AdicionarItem(ApiKey + "Balanco", retorno, CmCache.TEMPO_CACHE_5_MINUTOS);
            return retorno;
        }

        private KrakenHistoricoOrdem[] ObterHistoricoOrdem()
        {
            if (CmCache.Obter().ContemItem(ApiKey + "Historico"))
            {
                return CmCache.Obter().ObterItem<KrakenHistoricoOrdem[]>(ApiKey + "Historico");
            }
            string complementoUrl = "/0/private/TradesHistory";
            string resultadoConsulta = ExecutarConsultaAutenticada(complementoUrl);
            var retorno = JsonConvert.DeserializeObject<KrakenHistoricoOrdemCompleto>(resultadoConsulta).result.trades;
            CmCache.Obter().AdicionarItem(ApiKey + "Historico", retorno, CmCache.TEMPO_CACHE_30_MINUTOS);
            return retorno;
        }

        private string ObterSigla(string pair)
        {
            if (pair == "XICNXXBT")
            {
                return "ICN";
            }
            else if (pair == "XETHXXBT")
            {
                return "ETH";
            }
            return string.Empty;
        }

        private string ObterTipoOrdem(KrakenHistoricoOrdem ordem)
        {
            if (ordem.type == "sell")
            {
                return TipoOrdem.Venda;
            }
            else if (ordem.type == "buy")
            {
                return TipoOrdem.Compra;
            }
            return null;
        }

        private string ExecutarConsultaAutenticada(string complementoUrl)
        {
            string urlBase = "https://api.kraken.com";
            string urlCompleta = urlBase + complementoUrl;
            var content = "nonce=" + Nonce;
            var np = Nonce + Convert.ToChar(0) + content;
            var pathBytes = Encoding.UTF8.GetBytes(complementoUrl);
            var hash256Bytes = sha256_hash(np);
            var z = new byte[pathBytes.Count() + hash256Bytes.Count()];
            pathBytes.CopyTo(z, 0);
            hash256Bytes.CopyTo(z, pathBytes.Count());
            var signature = Convert.ToBase64String(getHash(Convert.FromBase64String(ApiSecret), z));
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                client.Headers["API-Key"] = ApiKey;
                client.Headers["API-Sign"] = signature;
                string resultado = client.UploadString(urlCompleta, "POST", content);
                Nonce = Nonce + 1;
                return resultado;
            }
        }

        private CotacaoMoedaEntidade CriarRegistroRetorno(TipoCrypto tipo, double valorUnidade)
        {
            return new CotacaoMoedaEntidade() { Exchange = TipoExchange.Kraken, Tipo = tipo, ValorUnidadeEmBitcoin = valorUnidade };
        }

        private BalancoMoedaEntidade CriarRegistroRetornoBalanco(string currency, double balance)
        {
            return new BalancoMoedaEntidade()
            {
                Exchange = TipoExchange.Kraken,
                Moeda = TipoCrypto.ObterPorSigla(currency),
                Quantidade = balance
            };
        }

        private byte[] getHash(byte[] keyByte, byte[] messageBytes)
        {
            using (var hmacsha512 = new HMACSHA512(keyByte))
            {
                return hmacsha512.ComputeHash(messageBytes);
            }
        }

        private byte[] sha256_hash(string value)
        {
            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(value));
                return result;
            }
        }

        #endregion
    }
}

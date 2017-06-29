using CryptoManager.Util;
using CryptoManager.Util.Cache;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace CryptoManager.Repositorio
{
    public class ApiBase<TCotacoes>
    {
        private readonly int timeout = 15000;

        private string GetAddressToGet(string url, Dictionary<string, object> parametros)
        {
            return parametros == null || parametros.Count == 0 ? url :
                string.Format("{0}?{1}", url, 
                string.Join("&", parametros.Select(c => string.Format("{0}={1}",
                HttpUtility.UrlEncode(c.Key), c.Value))));
        }

        internal TCotacoes Cotar(string url)
        {
            return Cotar(url, new Dictionary<string, object>());
        }

        internal TCotacoes Cotar(string url, Dictionary<string, object> parametros)
        {
            try
            {
                return CmCache.Obter().ExecutarFuncaoBusca<TCotacoes>(
                    new Func<string, Dictionary<string, object>, TCotacoes>(CotarSemCache),
                    CmCache.TEMPO_CACHE_5_MINUTOS, url, parametros);
            }
            catch (Exception exception)
            {
                string informacoesAdicionais = "";
                try
                {
                    informacoesAdicionais = string.Format("Tentativa de GET na rota: {0}", GetAddressToGet(url, parametros));
                }
                catch
                {
                    informacoesAdicionais = "ERRO ao obter informações adicionais.";
                }
                throw new CryptoManagerException(string.Format("Erro na cotação. {0}", informacoesAdicionais), exception);
            }
        }

        private TCotacoes CotarSemCache(string url, Dictionary<string, object> parametros)
        {
            using (var client = new ApiWebClient(timeout))
            {
                client.UseDefaultCredentials = true;
                client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                var resposta = client.DownloadString(GetAddressToGet(url, parametros));
                return JsonConvert.DeserializeObject<TCotacoes>(resposta);
            }
        }

        private class ApiWebClient : WebClient
        {
            private int TimeoutApi { get; set; }

            public ApiWebClient() : this(30000) { }

            public ApiWebClient(int pTimeout)
            {
                TimeoutApi = pTimeout;
            }

            protected override WebRequest GetWebRequest(Uri pAddress)
            {
                var request = base.GetWebRequest(pAddress);
                if (request != null)
                    request.Timeout = TimeoutApi;
                return request;
            }
        }

        protected string GetPropertyName<TValue>(Expression<Func<TValue>> propertyId)
        {
            return ((MemberExpression)propertyId.Body).Member.Name;
        }

        protected static string ObterHMAC512(string message, string apiSecret)
        {
            var hmac = new HMACSHA512(Encoding.ASCII.GetBytes(apiSecret));
            var messagebyte = Encoding.ASCII.GetBytes(message);
            var hashmessage = hmac.ComputeHash(messagebyte);
            var sign = BitConverter.ToString(hashmessage).Replace("-", "").ToLower();
            return sign;
        }

        protected static string ObterHMAC384(string message, string apiSecret)
        {
            var hmac = new HMACSHA384(Encoding.ASCII.GetBytes(apiSecret));
            byte[] hashmessage = hmac.ComputeHash(Encoding.ASCII.GetBytes(message));
            var sign = BitConverter.ToString(hashmessage).Replace("-", "").ToLower();
            return sign;
        }
    }
}

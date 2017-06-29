using CryptoManager.Entidades.CustomDeserializer;
using Newtonsoft.Json;

namespace CryptoManager.Entidades.Especificas
{
    [JsonConverter(typeof(ApiKrakenHistoricoOrdemSerializer))]
    public class KrakenHistoricoOrdemCompleto
    {
        public KrakenHistoricoOrdemResult result { get; set; }
    }
}

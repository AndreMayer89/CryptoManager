using CryptoManager.Entidades.Especificas;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace CryptoManager.Entidades.CustomDeserializer
{
    public class ApiKrakenHistoricoOrdemSerializer : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
           return typeof(KrakenHistoricoOrdemCompleto).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jsonObject = JObject.Load(reader);
            return new KrakenHistoricoOrdemCompleto
            {
                result = new KrakenHistoricoOrdemResult()
                {
                    trades = jsonObject.Children().Last().Children().First().Children().First().Children().Last().Children().Select(o => CriarRegistro(o)).ToArray()
                }
            };
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        private KrakenHistoricoOrdem CriarRegistro(JToken jObj)
        {
            return JsonConvert.DeserializeObject<KrakenHistoricoOrdem>((jObj.First()).ToString());
        }
    }
}

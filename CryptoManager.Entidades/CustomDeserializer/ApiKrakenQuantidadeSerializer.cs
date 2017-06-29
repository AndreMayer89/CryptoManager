using CryptoManager.Entidades.Especificas;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using static CryptoManager.Entidades.Especificas.KrakenQuantidadeEntidade;

namespace CryptoManager.Entidades.CustomDeserializer
{
    public class ApiKrakenQuantidadeSerializer : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(KrakenQuantidadeEntidade).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jsonObject = JObject.Load(reader);
            return new KrakenQuantidadeEntidade
            {
                result = jsonObject.Children().Last().Children().Last().Select(o => new KrakenQuantidadeEntidadePorMoeda
                {
                    Currency = ((JProperty)o).Name.Substring(1),
                    Balance = Convert.ToDouble(((JValue)((JProperty)o).Value).Value.ToString().Replace('.', ','))
                }).ToList()
            };
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}

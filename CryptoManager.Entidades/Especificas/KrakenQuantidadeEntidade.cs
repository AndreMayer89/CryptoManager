using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoManager.Entidades.CustomDeserializer;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CryptoManager.Entidades.Especificas
{
    [JsonConverter(typeof(ApiKrakenQuantidadeSerializer))]
    public class KrakenQuantidadeEntidade
    {
        public List<KrakenQuantidadeEntidadePorMoeda> result { get; set; }

        public class KrakenQuantidadeEntidadePorMoeda
        {
            public string Currency { get; set; }
            public double Balance { get; set; }
        }
    }
}

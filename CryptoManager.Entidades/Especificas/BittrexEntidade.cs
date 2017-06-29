using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoManager.Entidades.Especificas
{
    public class BittrexEntidade
    {
        public List<BittrexMoedaEntidade> result { get; set; }

        public class BittrexMoedaEntidade
        {
            public string MarketName { get; set; }
            public double? Last { get; set; }
        }
    }
}

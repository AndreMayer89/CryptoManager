using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoManager.Entidades.Especificas
{
    public class KrakenEntidade
    {
        public KrakenResultEntidade result { get; set; }

        public class KrakenResultEntidade
        {
            public KrakenMoedaEntidade XETHXXBT { get; set; }
            public KrakenMoedaEntidade XICNXXBT { get; set; }
        }

        public class KrakenMoedaEntidade
        {
            public double[] c { get; set; }
        }
    }
}

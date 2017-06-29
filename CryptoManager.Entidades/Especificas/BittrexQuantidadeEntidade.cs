using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoManager.Entidades.Especificas
{
    public class BittrexQuantidadeEntidade
    {
        public List<BittrexQuantidadeEntidadePorMoeda> result { get; set; }

        public class BittrexQuantidadeEntidadePorMoeda
        {
            public string Currency { get; set; }
            public double Balance { get; set; }
        }
    }
}

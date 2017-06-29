using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoManager.Entidades.Especificas
{
    public class DolarRealEntidade
    {
        public DolarRealEntidadeData data { get; set; }
        public class DolarRealEntidadeData
        {
            public string resultSimple { get; set; }
        }
    }
}

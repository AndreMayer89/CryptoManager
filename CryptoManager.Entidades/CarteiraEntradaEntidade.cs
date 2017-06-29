using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoManager.Entidades.Especificas;

namespace CryptoManager.Entidades
{
    public class CarteiraEntradaEntidade
    {
        public double Investido { get; set; }
        public PoloniexEntradaApiEntidade Poloniex { get; set; }
        public BittrexEntradaApiEntidade Bittrex { get; set; }
        public KrakenEntradaApiEntidade Kraken { get; set; }
        public BitfinexEntradaApiEntidade Bitfinex { get; set; }
        public List<MoedaEmCarteiraEntidade> BalancoCold { get; set; }
        public List<CompraMoedaEmColdEntidade> ComprasCold { get; set; }
    }
}

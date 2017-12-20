using System.Collections.Generic;
using System.Linq;

namespace CryptoManager.Models
{
    public class GridListaCryptosModel
    {
        public List<CryptoModel> Lista { get; set; }
        private double ValorBrlBtcDouble { get; set; }
        private double ValorBrlBtcRealDouble { get; set; }
        private double ValorUsdBtcDouble { get; set; }
        public string ValorTotalBtc { get { return ValorTotalBtcDouble.ToString("N8"); } }
        public double ValorTotalBtcDouble { get { return Lista.Sum(c => c.ValorTotalBtcDouble); } }
        public string ValorTotalBrlReal { get { return Lista.Sum(c => c.ValorTotalBtcDouble * ValorBrlBtcRealDouble).ToString("N2"); } }
        public string ValorTotalBrl { get { return Lista.Sum(c => c.ValorTotalBtcDouble * ValorBrlBtcDouble).ToString("N2"); } }
        public string ValorTotalUsd { get { return Lista.Sum(c => c.ValorTotalBtcDouble * ValorUsdBtcDouble).ToString("N2"); } }

        public List<ResultadoOperacaoModel> ResultadosOperacao { get; internal set; }

        public GridListaCryptosModel(double valorBrlBtcDouble, double valorBrlBtcRealDouble, double valorUsdBtcDouble)
        {
            ValorBrlBtcDouble = valorBrlBtcDouble;
            ValorBrlBtcRealDouble = valorBrlBtcRealDouble;
            ValorUsdBtcDouble = valorUsdBtcDouble;
        }
    }
}
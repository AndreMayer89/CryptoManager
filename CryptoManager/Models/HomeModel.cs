using CryptoManager.Entidades;
using System.Linq;

namespace CryptoManager.Models
{
    public class HomeModel
    {
        public GridListaCryptosModel GridListaCryptos { get; set; }
        public CotacoesBtcModel CotacoesBtc { get; set; }

        public double ValorBrlBtcDouble { get { return CotacoesBtc.BtcBrlFoxbit; } }
        public double ValorUsdBtcDouble { get { return CotacoesBtc.BtcUsd; } }
        public double ValorBrlBtcRealDouble { get { return CotacoesBtc.UsdBrl * ValorUsdBtcDouble; } }

        public string ValorBrlBtc { get { return ValorBrlBtcDouble.ToString("N2"); } }
        public string ValorUsdBtc { get { return ValorUsdBtcDouble.ToString("N2"); } }
        public string ValorBrlBtcReal { get { return ValorBrlBtcRealDouble.ToString("N2"); } }

        public double ValorTotalInvestido { get; set; }

        public string ValorizacaoTotalReal
        {
            get
            {
                return TotalValorizacaoBrl.ToString("N2") + " (" + PorcentagemValorizacaoTotalReal + ")";
            }
        }

        public double TotalAtual { get { return GridListaCryptos.Lista.Sum(c => c.ValorTotalBtcDouble * ValorBrlBtcRealDouble); } }
        public double TotalValorizacaoBrl { get { return TotalAtual - ValorTotalInvestido; } }

        public string ValorizacaoTotalRealFoxbit
        {
            get
            {
                return TotalValorizacaoBrlFoxbit.ToString("N2") + " (" + PorcentagemValorizacaoTotalRealFoxbit + ")";
            }
        }

        public double TotalAtualFoxbit { get { return GridListaCryptos.Lista.Sum(c => c.ValorTotalBtcDouble * ValorBrlBtcDouble); } }
        public double TotalValorizacaoBrlFoxbit { get { return TotalAtualFoxbit - ValorTotalInvestido; } }

        public string PorcentagemValorizacaoTotalReal { get { return (100 * TotalValorizacaoBrl / ValorTotalInvestido).ToString("N2") + "%"; } }
        public string PorcentagemValorizacaoTotalRealFoxbit { get { return (100 * TotalValorizacaoBrlFoxbit / ValorTotalInvestido).ToString("N2") + "%"; } }

        public SecaoInputExchangeModel Bitfinex { get; set; }
        public SecaoInputExchangeModel Kraken { get; set; }
        public SecaoInputExchangeModel Poloniex { get; set; }
        public SecaoInputExchangeModel Bittrex { get; set; }
        
        public GridBalancoColdModel GridBalancoCold { get; set; }

        public HomeModel()
        {
            Bitfinex = new SecaoInputExchangeModel() { Titulo = TipoExchange.Bitfinex.Nome, SufixoInputs = TipoExchange.Bitfinex.Nome.ToLowerInvariant() };
            Bittrex = new SecaoInputExchangeModel() { Titulo = TipoExchange.Bittrex.Nome, SufixoInputs = TipoExchange.Bittrex.Nome.ToLowerInvariant() };
            Kraken = new SecaoInputExchangeModel() { Titulo = TipoExchange.Kraken.Nome, SufixoInputs = TipoExchange.Kraken.Nome.ToLowerInvariant() };
            Poloniex = new SecaoInputExchangeModel() { Titulo = TipoExchange.Poloniex.Nome, SufixoInputs = TipoExchange.Poloniex.Nome.ToLowerInvariant() };
            GridBalancoCold = new GridBalancoColdModel();
        }
    }
}
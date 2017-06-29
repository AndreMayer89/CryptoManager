namespace CryptoManager.Models
{
    public class CryptoModel
    {
        public string NomeCrypto { get; set; }
        public string SiglaCrypto { get; set; }
        public double QuantidadeCryptoDouble { get; set; }
        public string ValorCryptoBtc { get; set; }
        public double ValorTotalBtcDouble { get; set; }
        public double QuantidadeBtcInvestida { get; set; }
        public double ValorTotalBrlDouble { get; set; }
        public string PorcentagemValorizacaoEmRelacaoAoBtc { get; set; }
        public string PorcentagemValorizacaoEmRelacaoAoBtcDouble { get; set; }
        public double ValorTotalDolaresDouble { get; internal set; }
        public double ValorTotalBrlRealDouble { get; internal set; }
        public double PercentualRelativoDouble { get; internal set; }


        public string QuantidadeCrypto { get { return QuantidadeCryptoDouble.ToString("N8"); } }
        public string ValorTotalBtc { get { return ValorTotalBtcDouble.ToString("N8"); } }
        public string ValorTotalReais { get { return ValorTotalBrlDouble.ToString("N2"); } }
        public string ValorTotalDolares { get { return ValorTotalDolaresDouble.ToString("N2"); } }
        public string ValorTotalBrlReal { get { return ValorTotalBrlRealDouble.ToString("N2"); } }
        public string PercentualRelativo { get { return PercentualRelativoDouble.ToString("N2"); } }
        public string PrecoMedioCompraEmBtc
        {
            get
            {
                double precoMedio = QuantidadeBtcInvestida > 0 ? (QuantidadeBtcInvestida / QuantidadeCryptoDouble) : 0;
                return precoMedio.ToString("N8");
            }
        }
    }
}
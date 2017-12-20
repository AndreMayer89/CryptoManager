namespace CryptoManager.Models
{
    public class CryptoModel
    {
        public string NomeCrypto { get; set; }
        public string SiglaCrypto { get; set; }
        public double QuantidadeCryptoDouble { get; set; }
        public string ValorCryptoBtc { get; set; }
        public double ValorTotalBtcDouble { get; set; }
        public double ValorTotalBrlDouble { get; set; }
        public double ValorTotalUsdDouble { get; internal set; }
        public double ValorTotalBrlRealDouble { get; internal set; }
        public double PercentualRelativoDouble { get; internal set; }


        public string QuantidadeCrypto { get { return QuantidadeCryptoDouble.ToString("N8"); } }
        public string ValorTotalBtc { get { return ValorTotalBtcDouble.ToString("N8"); } }
        public string ValorTotalBrl { get { return ValorTotalBrlDouble.ToString("N2"); } }
        public string ValorTotalUsd { get { return ValorTotalUsdDouble.ToString("N2"); } }
        public string ValorTotalBrlReal { get { return ValorTotalBrlRealDouble.ToString("N2"); } }
        public string PercentualRelativo { get { return PercentualRelativoDouble.ToString("N2"); } }
    }
}
using System.Globalization;

namespace CryptoManager.Entidades
{
    public class MoedaEmCarteiraEntidade
    {
        public string SiglaMoeda { get; set; }
        public double QuantidadeMoeda { get; set; }
        public string QuantidadeMoedaString { get { return QuantidadeMoeda.ToString("N2", CultureInfo.CreateSpecificCulture("en-US")); } }
        public string ExchangeCotacao { get; set; }
    }
}

using System.Globalization;

namespace CryptoManager.Entidades
{
    public class CompraMoedaEmColdEntidade
    {
        public string SiglaMoedaComprada { get; set; }
        public double QuantidadeMoedaComprada { get; set; }
        public string QuantidadeMoedaCompradaString { get { return QuantidadeMoedaComprada.ToString("N2", CultureInfo.CreateSpecificCulture("en-US")); } }
        public string SiglaMoedaUtilizadaNaCompra { get; set; }
        public double QuantidadeMoedaUtilizadaNaCompra { get; set; }
        public string QuantidadeMoedaUtilizadaNaCompraString { get { return QuantidadeMoedaUtilizadaNaCompra.ToString("N2", CultureInfo.CreateSpecificCulture("en-US")); } }
    }
}

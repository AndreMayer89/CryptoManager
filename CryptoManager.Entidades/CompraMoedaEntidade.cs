namespace CryptoManager.Entidades
{
    public class OrdemMoedaEntidade
    {
        public TipoExchange Exchange { get; set; }
        public TipoCrypto Moeda { get; set; }
        public double QuantidadeInvestida { get; set; }
        public double QuantidadeMoeda { get; set; }
    }
}

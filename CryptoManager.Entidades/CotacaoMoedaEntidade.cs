namespace CryptoManager.Entidades
{
    public class CotacaoMoedaEntidade
    {
        public TipoCrypto Tipo { get; set; }
        public TipoExchange Exchange { get; set; }
        public double ValorUnidadeEmBitcoin { get; set; }
    }
}

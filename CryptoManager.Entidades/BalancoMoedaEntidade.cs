namespace CryptoManager.Entidades
{
    public class BalancoMoedaEntidade
    {
        public TipoCrypto Moeda { get; set; }
        public TipoExchange Exchange { get; set; }
        public double Quantidade { get; set; }
        public bool ColdWallet { get; set; }
    }
}

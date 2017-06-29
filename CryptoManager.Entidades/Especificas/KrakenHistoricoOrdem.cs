namespace CryptoManager.Entidades.Especificas
{
    public class KrakenHistoricoOrdem
    {
        public string pair { get; set; }
        public string type { get; set; } //buy/sell
        public string price { get; set; } //preço por unidade
        public string cost { get; set; } //qtd btc investida
        public string fee { get; set; }
        public string vol { get; set; } //qtd da moeda
    }
}

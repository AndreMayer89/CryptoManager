using CryptoManager.Entidades;

namespace CryptoManager.Models
{
    public class ResultadoOperacaoModel
    {
        public TipoExchange Exchange { get; set; }
        public TipoOperacaoExchange TipoOperacao { get; set; }
        public string MensagemErro { get; set; }
        public double TempoExecucao { get; set; }
    }
}
using System.Collections.Generic;

namespace CryptoManager.Entidades
{
    public class ResultadoConsultaExchangeEntidade
    {
        public List<CotacaoMoedaEntidade> Cotacao { get; set; }
        public List<BalancoMoedaEntidade> Balanco { get; set; }
        public List<OrdemMoedaEntidade> LivroOrdens { get; set; }
        public ResultadoOperacaoEntidade ResultadoOperacoes { get; set; }
        public TipoExchange Exchange { get; set; }
    }
}

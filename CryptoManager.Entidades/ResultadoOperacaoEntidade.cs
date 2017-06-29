using System.Collections.Generic;

namespace CryptoManager.Entidades
{
    public class ResultadoOperacaoEntidade
    {
        public TipoExchange Exchange { get; set; }
        public List<ResultadoOperacaoEspecificaEntidade> ListaTipoOperacao { get; set; }

        public class ResultadoOperacaoEspecificaEntidade
        {
            public TipoOperacaoExchange Operacao { get; set; }
            public double Milisegundos { get; set; }
            public string MensagemErro { get; set; }
        }

        public ResultadoOperacaoEntidade()
        {
            ListaTipoOperacao = new List<ResultadoOperacaoEspecificaEntidade>();
        }
    }
}

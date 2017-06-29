using System.Collections.Generic;

namespace CryptoManager.Entidades
{
    public class ConsultaExchangesEntidade
    {
        public List<CryptoQuantidadeEntidade> ListaQuantidades { get; set; }
        public List<CotacaoMoedaEntidade> ListaCotacoes { get; set; }
        public List<ErroConsultaApiEntidade> ListaErros { get; set; }
        public List<ResultadoOperacaoEntidade> ListaResultadosOperacoesExchanges { get; set; }
    }
}

using CryptoManager.Entidades;
using System.Collections.Generic;

namespace CryptoManager.Repositorio.Especificas
{
    public interface IApiExchange
    {
        IEnumerable<CotacaoMoedaEntidade> Cotar();
        IEnumerable<BalancoMoedaEntidade> ListarBalancoConta();
        IEnumerable<OrdemMoedaEntidade> ListarHistoricoOrdem();
        TipoExchange ObterTipo();
    }
}

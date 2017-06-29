using CryptoManager.Entidades;
using CryptoManager.Repositorio.Especificas;
using System.Collections.Generic;
using System.Linq;

namespace CryptoManager.Business
{
    public static class CotacoesBusiness
    {
        public static CotacoesBtcEntidade ObterCotacoesBtc()
        {
            CotacoesBtcEntidade retorno = new CotacoesBtcEntidade();
            retorno.BtcBrlFoxbit = new ApiBlinktrade().Cotar().FirstOrDefault().ValorUnidadeEmBitcoin;
            retorno.UsdBrl = new ApiDolarReal().Cotar().FirstOrDefault().ValorUnidadeEmBitcoin;
            retorno.BtcUsd = new ApiDolarBitcoin().Cotar().FirstOrDefault().ValorUnidadeEmBitcoin;
            return retorno;
        }
    }
}

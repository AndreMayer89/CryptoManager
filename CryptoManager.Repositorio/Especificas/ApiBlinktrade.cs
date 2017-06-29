using CryptoManager.Entidades;
using CryptoManager.Entidades.Especificas;
using CryptoManager.Util.Cache;
using System;
using System.Collections.Generic;

namespace CryptoManager.Repositorio.Especificas
{
    public class ApiBlinktrade : ApiBase<BlinkTradeEntidade>
    {
        public CotacaoMoedaEntidade CriarRegistroRetorno(TipoCrypto tipo, double valorUnidade)
        {
            return new CotacaoMoedaEntidade() { Exchange = TipoExchange.Blinktrade, Tipo = tipo, ValorUnidadeEmBitcoin = valorUnidade };
        }

        public List<CotacaoMoedaEntidade> Cotar()
        {
            List<CotacaoMoedaEntidade> lista = new List<CotacaoMoedaEntidade>();
            try
            {
                lista.AddRange(CmCache.Obter().ExecutarFuncaoBusca<List<CotacaoMoedaEntidade>>(
                    new Func<List<CotacaoMoedaEntidade>>(ObterCotacoesSemCache), CmCache.TEMPO_CACHE_10_MINUTOS));
            }
            catch
            {
                lista.Add(CriarRegistroRetorno(TipoCrypto.Real, 9500));
            }
            return lista;
        }

        private List<CotacaoMoedaEntidade> ObterCotacoesSemCache()
        {
            List<CotacaoMoedaEntidade> lista = new List<CotacaoMoedaEntidade>();
            var retorno = Cotar("https://api.blinktrade.com/api/v1/BRL/ticker?apply_formatting=true");
            lista.Add(CriarRegistroRetorno(TipoCrypto.Real, retorno.last));
            return lista;
        }
    }
}

using CryptoManager.Entidades;
using CryptoManager.Entidades.Especificas;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace CryptoManager.Repositorio.Especificas
{
    public class ApiDolarBitcoin : ApiBase<DolarBitcoinEntidade>
    {
        public CotacaoMoedaEntidade CriarRegistroRetorno(TipoCrypto tipo, double valorUnidade)
        {
            return new CotacaoMoedaEntidade() { Exchange = TipoExchange.DolarReal, Tipo = tipo, ValorUnidadeEmBitcoin = valorUnidade };
        }

        public List<CotacaoMoedaEntidade> Cotar()
        {
            DolarBitcoinEntidade retorno = Cotar("https://api.bitfinex.com/v1/pubticker/btcusd");
            List<CotacaoMoedaEntidade> lista = new List<CotacaoMoedaEntidade>();
            lista.Add(CriarRegistroRetorno(TipoCrypto.Real, Convert.ToDouble(retorno.last_price, CultureInfo.CreateSpecificCulture("en-US"))));
            return lista;
        }
    }
}

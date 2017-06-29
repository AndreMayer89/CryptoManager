using CryptoManager.Entidades;
using CryptoManager.Entidades.Especificas;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace CryptoManager.Repositorio.Especificas
{
    public class ApiDolarReal : ApiBase<DolarRealEntidade>
    {
        public CotacaoMoedaEntidade CriarRegistroRetorno(TipoCrypto tipo, double valorUnidade)
        {
            return new CotacaoMoedaEntidade() { Exchange = TipoExchange.DolarReal, Tipo = tipo, ValorUnidadeEmBitcoin = valorUnidade };
        }

        public List<CotacaoMoedaEntidade> Cotar()
        {
            List<CotacaoMoedaEntidade> lista = new List<CotacaoMoedaEntidade>();
            try
            {
                DolarRealEntidade retorno = Cotar("https://api.vitortec.com/currency/converter/v1.2/?from=USD&to=BRL&value=1");
                lista.Add(CriarRegistroRetorno(TipoCrypto.Real, Convert.ToDouble(retorno.data.resultSimple, CultureInfo.CreateSpecificCulture("en-US"))));
            }
            catch
            {
                lista.Add(CriarRegistroRetorno(TipoCrypto.Real, 3.3));
            }
            return lista;
        }
    }
}

using System.Collections.Generic;
using System.Linq;

namespace CryptoManager.Entidades
{
    public class CryptoQuantidadeEntidade
    {
        public TipoCrypto Tipo { get; private set; }
        public List<OrdemMoedaEntidade> ListaCompras { get; private set; }
        public List<BalancoMoedaEntidade> ListaBalancos { get; private set; }

        public CryptoQuantidadeEntidade(TipoCrypto tipo)
        {
            Tipo = tipo;
            ListaBalancos = new List<BalancoMoedaEntidade>();
            ListaCompras = new List<OrdemMoedaEntidade>();
        }

        public double Quantidade
        {
            get
            {
                return ListaBalancos.Sum(b => b.Quantidade);
            }
        }
    }
}

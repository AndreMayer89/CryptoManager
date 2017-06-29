using System.Collections.Generic;

namespace CryptoManager.Entidades
{
    public class TipoOperacaoExchange
    {
        public static readonly TipoOperacaoExchange Cotacao = new TipoOperacaoExchange(1, "Cotação");
        public static readonly TipoOperacaoExchange Balanco = new TipoOperacaoExchange(2, "Balanço");
        public static readonly TipoOperacaoExchange LivroOrdens = new TipoOperacaoExchange(3, "Livro Ordens");

        public int Id { get; private set; }
        public string Nome { get; private set; }

        private TipoOperacaoExchange(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        public static List<TipoOperacaoExchange> ListarParaResumoOperacao()
        {
            List<TipoOperacaoExchange> lista = new List<TipoOperacaoExchange>();
            lista.Add(Cotacao);
            lista.Add(Balanco);
            lista.Add(LivroOrdens);
            return lista;
        }
    }
}

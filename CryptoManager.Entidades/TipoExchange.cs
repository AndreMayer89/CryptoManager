using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoManager.Entidades
{
    public class TipoExchange
    {
        public static readonly TipoExchange DolarReal = new TipoExchange(-2, "DolarReal");
        public static readonly TipoExchange ColdWallet = new TipoExchange(-1, "ColdWallet");
        public static readonly TipoExchange Blinktrade = new TipoExchange(0, "Blinktrade");

        public static readonly TipoExchange Poloniex = new TipoExchange(1, "Poloniex");
        public static readonly TipoExchange Kraken = new TipoExchange(2, "Kraken");
        public static readonly TipoExchange Bitfinex = new TipoExchange(3, "Bitfinex");
        public static readonly TipoExchange Bittrex = new TipoExchange(4, "Bittrex");

        public int Id { get; private set; }
        public string Nome { get; private set; }

        private TipoExchange(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        private static List<TipoExchange> ListarTodos()
        {
            List<TipoExchange> lista = new List<TipoExchange>();
            lista.Add(Poloniex);
            lista.Add(Kraken);
            lista.Add(Bitfinex);
            lista.Add(Blinktrade);
            lista.Add(Bittrex);
            return lista;
        }

        public static List<TipoExchange> ListarTodosParaInputTela()
        {
            return ListarTodos().Where(e => e.Id > 0).ToList();
        }

        public static TipoExchange Obter(int id)
        {
            return ListarTodos().FirstOrDefault(t => t.Id == id);
        }

        public static TipoExchange ObterPorNome(string nome)
        {
            return ListarTodos().FirstOrDefault(t => string.Equals(t.Nome, nome, StringComparison.InvariantCultureIgnoreCase));
        }

        public static List<TipoExchange> ListarParaResumoOperacao()
        {
            List<TipoExchange> lista = new List<TipoExchange>();
            lista.Add(Poloniex);
            lista.Add(Kraken);
            lista.Add(Bitfinex);
            lista.Add(Bittrex);
            return lista;
        }
    }
}

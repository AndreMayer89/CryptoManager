using CryptoManager.Entidades;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CryptoManager.Models
{
    public class GridBalancoColdModel
    {
        public List<SelectListItem> ListaMoedas { get; set; }
        public List<SelectListItem> ListaExchange { get; set; }
        public List<MoedaEmCarteiraEntidade> ListaBalancos { get; set; }

        public GridBalancoColdModel()
        {
            ListaMoedas = new List<SelectListItem>();
            ListaMoedas.Add(new SelectListItem() { Text = string.Empty, Value = string.Empty });
            ListaMoedas.AddRange(TipoCrypto.ListarTodasParaInputTela().Select(c => new SelectListItem()
            {
                Text = c.Sigla + " - " + c.Nome,
                Value = c.Sigla
            }).OrderBy(c => c.Text));

            ListaExchange = new List<SelectListItem>();
            ListaExchange.Add(new SelectListItem() { Text = string.Empty, Value = string.Empty });
            ListaExchange = TipoExchange.ListarTodosParaInputTela().Select(e => new SelectListItem()
            {
                Text = e.Nome,
                Value = e.Id.ToString()
            }).OrderBy(c => c.Text).ToList();

            ListaBalancos = new List<MoedaEmCarteiraEntidade>();
        }
    }
}
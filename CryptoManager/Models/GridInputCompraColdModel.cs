using CryptoManager.Entidades;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CryptoManager.Models
{
    public class GridInputCompraColdModel
    {
        public List<SelectListItem> ListaMoedas { get; set; }
        public List<CompraMoedaEmColdEntidade> ListaCompras { get; internal set; }

        public GridInputCompraColdModel()
        {
            ListaMoedas = new List<SelectListItem>();
            ListaMoedas.Add(new SelectListItem() { Text = string.Empty, Value = string.Empty });
            ListaMoedas.AddRange(TipoCrypto.ListarTodasParaInputTela().Select(c => new SelectListItem()
            {
                Text = c.Sigla + " - " + c.Nome,
                Value = c.Sigla
            }).OrderBy(c => c.Text));
            
            ListaCompras = new List<CompraMoedaEmColdEntidade>();
        }
    }
}
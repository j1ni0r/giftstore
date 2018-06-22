using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace giftstore.Models
{
    public class Item
    {
        public int ItemId { get; set; }
        public int CategoriaId { get; set; }
        public int FabricanteId { get; set; }
        public string Titulo { get; set; }
        public decimal Preco { get; set; }
        public string ItemArtUrl { get; set; }
        public Categoria Categoria { get; set; }
        public Fabricante Fabricante { get; set; }
    }
}
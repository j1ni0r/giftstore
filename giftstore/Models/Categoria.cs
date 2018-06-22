using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace giftstore.Models
{
    public partial class Categoria
    {
        public int CategoriaId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public List<Item> Itens { get; set; }

    }
}
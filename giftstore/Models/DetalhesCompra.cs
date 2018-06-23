using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace giftstore.Models
{
    public class DetalhesCompra
    {
        public int DetalhesCompraId { get; set; }
        public int CompraId { get; set; }
        public int ItemId { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public virtual Item Item { get; set; }
        public virtual Compra Compra { get; set; }
    }
}
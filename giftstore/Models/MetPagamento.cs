using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace giftstore.Models
{
    public class MetPagamento
    {
        public int MetPagamentoId { get; set; }
        public string Tipo { get; set; }
        public string Descricao { get; set; }
        public List<Compra> Compras { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace giftstore.Models
{
    public class Compra
    {
        public int CompraId { get; set; }
        public string NomeUser { get; set; }
        public string PrimeiroNome { get; set; }
        public string UltimoNome { get; set; }
        public string Endereco { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public int MetPagamentoId { get; set; }
        public virtual MetPagamento MetPagamento { get; set; }
        public System.DateTime DataCompra { get; set; }
        public List<DetalhesCompra> DetalhesCompras { get; set; }
        public decimal Total { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace giftstore.Models
{
    public class Compra
    {
        [ScaffoldColumn(false)]
        public int CompraId { get; set; }
        [ScaffoldColumn(false)]
        public System.DateTime DataCompra { get; set; }
        [ScaffoldColumn(false)]
        public string NomeUser { get; set; }
        [Required(ErrorMessage = "Primeiro nome é necessário")]
        [DisplayName("Primeiro Nome")]
        [StringLength(160)]
        public string PrimeiroNome { get; set; }
        [Required(ErrorMessage = "Último nome é necessário")]
        [DisplayName("Último Nome")]
        [StringLength(160)]
        public string UltimoNome { get; set; }
        [Required(ErrorMessage = "Endereço é necessário")]
        [StringLength(70)]
        public string Endereco { get; set; }
        [Required(ErrorMessage = "Endereço de e-mail é necessário")]
        [DisplayName("Endereço Email")]

        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",
            ErrorMessage = "Email não é válido.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Telefone é necessário")]
        [StringLength(24)]
        public string Telefone { get; set; }
        [DisplayName("Método de Pagamento")]
        public int MetPagamentoId { get; set; }
        public virtual MetPagamento MetPagamento { get; set; }
        public List<DetalhesCompra> DetalhesCompras { get; set; }
        [ScaffoldColumn(false)]
        public decimal Total { get; set; }
    }
}
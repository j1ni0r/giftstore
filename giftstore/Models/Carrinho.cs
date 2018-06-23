using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace giftstore.Models
{
    public class carrinho
    {
        [Key]
        public int RegistroId { get; set; }
        public string CarrinhoId { get; set; }
        public int ItemId { get; set; }
        public int Cont { get; set; }
        public System.DateTime DataCriacao { get; set; }
        public virtual Item Item { get; set; }

    }
}
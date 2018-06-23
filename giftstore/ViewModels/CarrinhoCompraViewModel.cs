using giftstore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace giftstore.ViewModels
{
    public class CarrinhoCompraViewModel
    {
        public List<carrinho> CarrinhoItens { get; set; }
        public decimal CarrinhoTotal { get; set; }



    }
}
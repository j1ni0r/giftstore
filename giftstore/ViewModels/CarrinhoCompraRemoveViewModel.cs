using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace giftstore.ViewModels
{
    public class CarrinhoCompraRemoveViewModel
    {
        public string Mensagem { get; set; }
        public decimal CarrinhoTotal { get; set; }
        public int CarrinhoCont { get; set; }
        public int ItemCont { get; set; }
        public int DeleteId { get; set; }
    }
}
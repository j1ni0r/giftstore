using giftstore.Models;
using giftstore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace giftstore.Controllers
{
    public class CarrinhoCompraController : Controller
    {
        GiftStoreEntities storeDB = new GiftStoreEntities();

        public ActionResult Index()
        {
            var carrinho = CarrinhoCompra.GetCarrinho(this.HttpContext);


            var viewModel = new CarrinhoCompraViewModel
            {
                CarrinhoItens = carrinho.GetCarrinhoItens(),
                CarrinhoTotal = carrinho.GetTotal()
            };

            return View(viewModel);
        }

        public ActionResult AddToCart(int id)
        {

            var addedItem = storeDB.Itens
                .Single(item => item.ItemId == id);


            var carrinho = CarrinhoCompra.GetCarrinho(this.HttpContext);

            carrinho.AddNoCarrinho(addedItem);


            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult RemoveDoCarrinho(int id)
        {

            var carrinho = CarrinhoCompra.GetCarrinho(this.HttpContext);

            string nomeItem = storeDB.Carrinhos
                .Single(item => item.RegistroId == id).Item.Titulo;

            int itemCount = carrinho.RemoveDoCarrinho(id);

            var results = new CarrinhoCompraRemoveViewModel
            {
                Mensagem = Server.HtmlEncode(nomeItem) +
                    " foi removido do seu carrinho.",
                CarrinhoTotal = carrinho.GetTotal(),
                CarrinhoCont = carrinho.GetCount(),
                ItemCont = itemCount,
                DeleteId = id
            };
            return Json(results);
        }
        [ChildActionOnly]
        public ActionResult CarrinhoResumo()
        {
            var carrinho = CarrinhoCompra.GetCarrinho(this.HttpContext);

            ViewData["CarrinhoCont"] = carrinho.GetCount();
            return PartialView("CarrinhoResumo");
        }
    }
}
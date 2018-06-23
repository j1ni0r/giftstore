using giftstore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace giftstore.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        GiftStoreEntities storeDB = new GiftStoreEntities();
        const string PromoCode = "50";

        public ActionResult AddressAndPayment()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddressAndPayment(FormCollection values)
        {
            var compra = new Compra();
            TryUpdateModel(compra);

            try
            {
                if (string.Equals(values["PromoCode"], PromoCode,
                    StringComparison.OrdinalIgnoreCase) == false)
                {
                    return View(compra);
                }
                else
                {
                    compra.NomeUser = User.Identity.Name;
                    compra.DataCompra = DateTime.Now;


                    storeDB.Compras.Add(compra);
                    storeDB.SaveChanges();

                    var carrinho = CarrinhoCompra.GetCarrinho(this.HttpContext);
                    carrinho.CriarCompra(compra);

                    return RedirectToAction("Completo",
                        new { id = compra.CompraId });
                }
            }
            catch
            {

                return View(compra);
            }
        }

        public ActionResult Complete(int id)
        {

            bool isValid = storeDB.Compras.Any(
                o => o.CompraId == id &&
                o.NomeUser == User.Identity.Name);

            if (isValid)
            {
                return View(id);
            }
            else
            {
                return View("Error");
            }
        }
    }
}
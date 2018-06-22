using giftstore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace giftstore.Controllers
{
    public class StoreController : Controller
    {
        GiftStoreEntities storeDB = new GiftStoreEntities();
        // GET: Store
        public ActionResult Index()
        {
            var categorias = storeDB.Categorias.ToList();
            {
                return View(categorias);
            };
 
        }
        public ActionResult Navega(string categoria)
        {
            var categoryModel = storeDB.Categorias.Include("Itens").Single(c=>c.Nome==categoria);
           return View(categoryModel);
        }
        public ActionResult Detalhes(int id)
        {
            var Item = storeDB.Itens.Find(id);
                        return View(Item);

        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using giftstore.Models;

namespace giftstore.Controllers
{
    [Authorize]
    public class EditaComprasController : Controller
    {
        private GiftStoreEntities storeDB = new GiftStoreEntities();

        // GET: EditaCompras
        public ActionResult Index()
        {
            var compras = storeDB.Compras.Include(c => c.MetPagamento);
            return View(compras.ToList());
        }

        // GET: EditaCompras/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compra compra = storeDB.Compras.Find(id);
            if (compra == null)
            {
                return HttpNotFound();
            }
            return View(compra);
        }

        // GET: EditaCompras/Create
        public ActionResult Create()
        {
            ViewBag.MetPagamentoId = new SelectList(storeDB.MetPagamentos, "MetPagamentoId", "Tipo");
            return View();
        }

        // POST: EditaCompras/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CompraId,DataCompra,NomeUser,PrimeiroNome,UltimoNome,Endereco,Email,Telefone,MetPagamentoId,Total")] Compra compra)
        {

             /*  if (ModelState.IsValid)
              {
                  db.Compras.Add(compra);
                  db.SaveChanges();
                  return RedirectToAction("Index");
              } */

            if (ModelState.IsValid)
            {
                compra.NomeUser = User.Identity.Name;
                compra.DataCompra = DateTime.Now;

                storeDB.Compras.Add(compra);
                storeDB.SaveChanges();

                var carrinho = CarrinhoCompra.GetCarrinho(this.HttpContext);
                carrinho.CriarCompra(compra);

                return RedirectToAction("VendaRealizada",
                    new { id = compra.CompraId });


            }




            ViewBag.MetPagamentoId = new SelectList(storeDB.MetPagamentos, "MetPagamentoId", "Tipo", compra.MetPagamentoId);
            return View(compra);
        }

        // GET: EditaCompras/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compra compra = storeDB.Compras.Find(id);
            if (compra == null)
            {
                return HttpNotFound();
            }
            ViewBag.MetPagamentoId = new SelectList(storeDB.MetPagamentos, "MetPagamentoId", "Tipo", compra.MetPagamentoId);
            return View(compra);
        }

        // POST: EditaCompras/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CompraId,DataCompra,NomeUser,PrimeiroNome,UltimoNome,Endereco,Email,Telefone,MetPagamentoId,Total")] Compra compra)
        {
            if (ModelState.IsValid)
            {
                storeDB.Entry(compra).State = EntityState.Modified;
                storeDB.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MetPagamentoId = new SelectList(storeDB.MetPagamentos, "MetPagamentoId", "Tipo", compra.MetPagamentoId);
            return View(compra);
        }

        // GET: EditaCompras/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compra compra = storeDB.Compras.Find(id);
            if (compra == null)
            {
                return HttpNotFound();
            }
            return View(compra);
        }

        // POST: EditaCompras/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Compra compra = storeDB.Compras.Find(id);
            storeDB.Compras.Remove(compra);
            storeDB.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                storeDB.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult VendaRealizada(int id)
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

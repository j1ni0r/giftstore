using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace giftstore.Models
{
    public class CarrinhoCompra
    {
        GiftStoreEntities storeDB = new GiftStoreEntities();
        string CarrinhoCompraId { get; set; }
        public const string CarrinhoSessaoKey = "CarrinhoId";

        public static CarrinhoCompra GetCarrinho(HttpContextBase context)
        {
            var carrinho = new CarrinhoCompra();
            carrinho.CarrinhoCompraId = carrinho.GetCarrinhoId(context);
            return carrinho;
        }
        // Metodo Helper para simplificar as chamadas de carrinho de compras

        public static CarrinhoCompra GetCarrinho(Controller controller)
        {
            return GetCarrinho(controller.HttpContext);
        }

        public void AddNoCarrinho (Item item)
        {

            var carrinhoItem = storeDB.Carrinhos.SingleOrDefault(
                c => c.CarrinhoId == CarrinhoCompraId
                && c.ItemId == item.ItemId);

            if (carrinhoItem == null)
            {

                carrinhoItem = new Carrinho
                {
                    ItemId = item.ItemId,
                    CarrinhoId = CarrinhoCompraId,
                    Cont = 1,
                    DataCriacao = DateTime.Now
                };
                storeDB.Carrinhos.Add(carrinhoItem);
            }
            else
            {

                carrinhoItem.Cont++;
            }

            storeDB.SaveChanges();
        }

        public int RemoveDoCarrinho(int id)
        {

            var carrinhoItem = storeDB.Carrinhos.Single(
                carrinho => carrinho.CarrinhoId == CarrinhoCompraId
                && carrinho.RegistroId == id);

            int itemCount = 0;

            if (carrinhoItem != null)
            {
                if (carrinhoItem.Cont > 1)
                {
                    carrinhoItem.Cont--;
                    itemCount = carrinhoItem.Cont;
                }
                else
                {
                    storeDB.Carrinhos.Remove(carrinhoItem);
                }

                storeDB.SaveChanges();
            }
            return itemCount;
        }

        public void CarrinhoVazio()
        {
            var carrinhoItens = storeDB.Carrinhos.Where(
                carrinho => carrinho.CarrinhoId == CarrinhoCompraId);
            foreach (var carrinhoItem in carrinhoItens)
            {
                storeDB.Carrinhos.Remove(carrinhoItem);
            }
            storeDB.SaveChanges();
        }

        public List<Carrinho> GetCarrinhoItens()
        {
            return storeDB.Carrinhos.Where(
                carrinho => carrinho.CarrinhoId == CarrinhoCompraId).ToList();
        }

        public int GetCount()
        {

            int? count = (from carrinhoItens in storeDB.Carrinhos
                          where carrinhoItens.CarrinhoId == CarrinhoCompraId
                          select (int?)carrinhoItens.Cont).Sum();

            return count ?? 0;
        }

        public decimal GetTotal()
        {
            // Multiplica o preco do produto pelo contador do produto 
            // para obter o preco para cada um dos produtos no carrinho
            // soma todos os precos dos produtos para ter o preco do total do carrinho

            decimal? total = (from carrinhoItens in storeDB.Carrinhos
                              where carrinhoItens.CarrinhoId == CarrinhoCompraId
                              select (int?)carrinhoItens.Cont *
                              carrinhoItens.Item.Preco).Sum();

            return total ?? decimal.Zero;
        }

        public int CriarCompra(Compra compra)
        {
            decimal compraTotal = 0;

            var carrinhoItens = GetCarrinhoItens();

            foreach (var item in carrinhoItens)
            {
                var detalheCompra = new DetalhesCompra
                {
                    ItemId = item.ItemId,
                    CompraId = compra.CompraId,
                    PrecoUnitario = item.Item.Preco,
                    Quantidade = item.Cont
                };

                compraTotal += (item.Cont * item.Item.Preco);

                storeDB.DetalhesCompras.Add(detalheCompra);

            }

            compra.Total = compraTotal;


            storeDB.SaveChanges();

            CarrinhoVazio();

            return compra.CompraId;
        }

        public string GetCarrinhoId(HttpContextBase context)
        {
            if (context.Session[CarrinhoSessaoKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[CarrinhoSessaoKey] =
                        context.User.Identity.Name;
                }
                else
                {

                    Guid tempCartId = Guid.NewGuid();

                    context.Session[CarrinhoSessaoKey] = tempCartId.ToString();
                }
            }
            return context.Session[CarrinhoSessaoKey].ToString();
        }
        public void MigrateCart(string Email)
        {
            var carrinhoCompra = storeDB.Carrinhos.Where(
                c => c.CarrinhoId == CarrinhoCompraId);

            foreach (Carrinho item in carrinhoCompra)
            {
                item.CarrinhoId = Email;
            }
            storeDB.SaveChanges();
        }

    }
}
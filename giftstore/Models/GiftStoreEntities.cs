using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace giftstore.Models
{
    public class GiftStoreEntities: DbContext
    {
        public DbSet<Item> Itens { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Fabricante> Fabricantes { get; set; }
        public DbSet<carrinho> Carrinhos  { get; set; }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<DetalhesCompra> DetalhesCompras { get; set; }
        public DbSet<MetPagamento> MetPagamentos { get; set; }
        //public System.Data.Entity.DbSet<giftstore.Models.MetPagamento> MetPagamentoes { get; set; }
    }
}
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


    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace giftstore.Models
{
    [Bind(Exclude = "ItemId")]
    public class Item
    {
        [ScaffoldColumn(false)]
        public int ItemId { get; set; }
        [DisplayName("Categoria")]
        public int CategoriaId { get; set; }
        [DisplayName("Fabricante")]
        public int FabricanteId { get; set; }
        [Required(ErrorMessage = "Um titulo de produto é requerido")]
        [StringLength(160)]
        public string Titulo { get; set; }
        [Required(ErrorMessage = "Um valor é requerido")]
        [Range(0.1, 10000, ErrorMessage = "Preço precisa ser entre 0.1 e 10000")]
        public decimal Preco { get; set; }
        [DisplayName("Url de imagem do item")]
        [StringLength(1024)]
        public string ItemArtUrl { get; set; }
        public virtual Categoria Categoria { get; set; }
        public virtual Fabricante Fabricante { get; set; }
    }
}
using LiquorShop.API.Models.DTO;
using LiquorShop_DataLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiquorShop.API.Areas.Admin.Models.ViewModel
{
    public class ProductVM
    {
        //public IEnumerable<Product_Categories> productCategories { get; set; }
        //public ProductDTO productDto { get; set; }
        [Key]
        public int ProId { get; set; }
        public string ProductName { get; set; }
        public int? SKU { get; set; }
        public string Size { get; set; }
        public decimal? Price { get; set; }
        public int CatFKId { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public string ImageName { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }
        public IEnumerable<String> Images { get; set; }

        public ProductVM()
        {

        }
        public ProductVM(ProductDTO prod)
        {
            ProId = prod.ProId;
            ProductName = prod.ProductName;
            SKU = prod.SKU;
            Size = prod.Size;
            Price = prod.Price;
            Slug = prod.Slug;
            Description = prod.Description;
            ImageName = prod.ImageName;
            CatFKId = prod.CatFKId;
        }
        
    }
}
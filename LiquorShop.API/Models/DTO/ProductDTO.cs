using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiquorShop.API.Models.DTO
{
    public class ProductDTO
    {
        public int ProId { get; set; }
        public string ProductName { get; set; }
        public int? SKU { get; set; }
        public string Size { get; set; }
        public decimal? Price { get; set; }
        public int CatFKId { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public string ImageName { get; set; }

        //public virtual Product_Categories Product_Categories { get; set; }
    }
}
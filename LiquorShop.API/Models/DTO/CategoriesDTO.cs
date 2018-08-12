using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiquorShop.API.Models.DTO
{
    public class CategoriesDTO
    {
        
        public int CatgId { get; set; }
        public string CategoryName { get; set; }
        public string Slug { get; set; }
        public int? Sorting { get; set; }
    }
}
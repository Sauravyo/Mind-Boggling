using LiquorShop.API.Models.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LiquorShop.API.Areas.Admin.Models.ViewModel
{
    public class CategoriesVM
    {
        [Key]
        public int CatgId { get; set; }
        public string CategoryName { get; set; }
        public string Slug { get; set; }
        public int? Sorting { get; set; }
        public CategoriesVM()
        {

        }

        public CategoriesVM(CategoriesDTO catDto)
        {
            CatgId = catDto.CatgId;
            CategoryName = catDto.CategoryName;
            Slug = catDto.Slug;
            Sorting = catDto.Sorting;

        }
    }
}
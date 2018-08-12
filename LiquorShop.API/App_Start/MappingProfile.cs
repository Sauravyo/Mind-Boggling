using AutoMapper;
using LiquorShop.API.Areas.Admin.Models.ViewModel;
using LiquorShop.API.Models.DTO;
using LiquorShop_DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiquorShop.API.App_Start
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Product_Name, ProductDTO>();
            CreateMap<ProductDTO, Product_Name>();
            CreateMap<ProductDTO, ProductVM>();
            CreateMap<ProductVM, ProductDTO>();
            CreateMap<Page, PageDTO>();
            CreateMap<PageDTO, Page>();
            CreateMap<tblSidebar, SidebarDTO>();
            CreateMap<SidebarDTO, tblSidebar>();
            CreateMap<Product_Categories, CategoriesDTO>();
            CreateMap<CategoriesDTO, Product_Categories>();

        }
    }
}
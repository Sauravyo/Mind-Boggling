using AutoMapper;
using LiquorShop.API.Models.DTO;
using LiquorShop_AppLayer.Services;
using LiquorShop_DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LiquorShop.API.Controllers
{
    public class ProductController : ApiController
    {
        private IProductService productservice = new ProductService();

        //Returns list of products to the client
        public IEnumerable<ProductDTO> Get()
        {
            
            return productservice.GetProductName()
                .Select(Mapper.Map<Product_Name, ProductDTO>);
        }

        //Returns single product to the client
        
        public ProductDTO Get(int id)
        {
            return Mapper.Map<Product_Name, ProductDTO>
                (productservice.GetProductName(id)) ;
        }

    }
}

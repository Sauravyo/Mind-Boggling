using LiquorShop_DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LiquorShop_AppLayer.Services
{
    public interface IProductService
    {
        //Service Contract for returning list of product
        List<Product_Name> GetProductName();

        //Service Contract for returning one product
        Product_Name GetProductName(int id);
        
        //Service Contract for adding new product
        bool AddProduct(Product_Name product, HttpPostedFileBase file);

        List<Product_Categories> GetCategories();

        string AddCategory(string category);

        bool ReorderCategory(int[] idz);

        List<Product_Name> Products(int? page, int? catId);

        Product_Name EditProduct(int id);

       bool EditProductPost(Product_Name prod);
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using LiquorShop_DataLayer;

namespace LiquorShop_AppLayer.Services
{
    public class ProductService : IProductService
    {
        //Api Service for adding new product
        public bool AddProduct(Product_Name product, HttpPostedFileBase file)
        {
            using (Liquor_ShopEntities _dbContext = new Liquor_ShopEntities())
            {
                if (_dbContext.Product_Name.Any(p => p.ProductName == product.ProductName))
                {
                    return false;
                }
                product.Slug = product.ProductName.Replace(" ", "-").ToLower();
                _dbContext.Product_Name.Add(product);
                _dbContext.SaveChanges();

            }
            return true;
        }



        //Api service returning for list of product
        public List<Product_Name> GetProductName()
        {
            using (Liquor_ShopEntities _dbContext = new Liquor_ShopEntities())
            {
                return _dbContext.Product_Name.ToList();
            }
        }

        //Api Service for returning a single product
        public Product_Name GetProductName(int id)
        {
            using (Liquor_ShopEntities _dbContext = new Liquor_ShopEntities())
            {
                return _dbContext.Product_Name.Where(x => x.ProId == id).FirstOrDefault();
            }
        }

        public Product_Name GetProductName(string prName)
        {
            using (Liquor_ShopEntities _dbContext = new Liquor_ShopEntities())
            {
                return _dbContext.Product_Name.Where(x => x.ProductName == prName).FirstOrDefault();
            }
        }

        public List<Product_Categories> GetCategories()
        {
            using (Liquor_ShopEntities _dbContext = new Liquor_ShopEntities())
            {
                return _dbContext.Product_Categories.ToList();
            }
        }

        public string AddCategory(string category)
        {
            string id;
            using (Liquor_ShopEntities _dbContext = new Liquor_ShopEntities())
            {
                if (_dbContext.Product_Categories.Any(c => c.CategoryName == category))
                    return "titletaken";

                Product_Categories cat = new Product_Categories
                {
                    CategoryName = category,
                    Slug = category.Replace(" ", "-").ToLower(),
                    Sorting = 100,
                };
                _dbContext.Product_Categories.Add(cat);
                _dbContext.SaveChanges();
                id = cat.CatgId.ToString();
            }
            return id;
        }

        public bool ReorderCategory(int[] idz)
        {
            using (Liquor_ShopEntities _dbContext = new Liquor_ShopEntities())
            {
                int count = 1;
                Product_Categories catg;
                foreach (var ctId in idz)
                {
                    catg = _dbContext.Product_Categories.Find(ctId);
                    catg.Sorting = count;
                    _dbContext.SaveChanges();
                    count++;
                }
            }

            return true;
        }

        public List<Product_Name> Products(int? page, int? catId)
        {
            using (Liquor_ShopEntities _dbContext = new Liquor_ShopEntities())
            {
                return _dbContext.Product_Name.ToArray()
                    .Where(c => catId == null || catId == 0 || c.CatFKId == catId)
                    .ToList();
            }
        }

        public Product_Name EditProduct(int id)
        {
            using (Liquor_ShopEntities _dbContext = new Liquor_ShopEntities())
            {
                Product_Name product = _dbContext.Product_Name.Find(id);
                return product;
            }
        }

        public bool EditProductPost(Product_Name prod)
        {
            int id = prod.ProId;
            using (Liquor_ShopEntities _dbContext = new Liquor_ShopEntities())
            {
                Product_Name product = _dbContext.Product_Name.Find(id);
                product.ProductName = prod.ProductName;
                product.Description = prod.Description;
                product.Price = prod.Price;
                product.Size = prod.Size;
                product.Slug = prod.ProductName.Replace(" ", "-").ToLower();
                product.SKU = prod.SKU;
                product.CatFKId = prod.CatFKId;
                product.ImageName = prod.ImageName;

                _dbContext.SaveChanges();
            }
            return true;
        }

    }


}
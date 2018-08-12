using AutoMapper;
using LiquorShop.API.Areas.Admin.Models.ViewModel;
using LiquorShop.API.Models.DTO;
using LiquorShop_AppLayer.Services;
using LiquorShop_DataLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace LiquorShop.API.Areas.Admin.Controllers
{
    public class ShopController : Controller
    {
        private IProductService productservice = new ProductService();
        //private IAdminService adminService = new AdminService();

        [HttpGet]
        public ActionResult Category()
        {
            List<CategoriesDTO> categories = productservice.GetCategories()
                .Select(Mapper.Map<Product_Categories, CategoriesDTO>).ToList();
            List<CategoriesVM> model = categories.OrderBy(c => c.Sorting)
                .Select(c => new CategoriesVM(c)).ToList();

            return View(model);
        }

        [HttpPost]
        public String AddCategory(string catgName)
        {
            string id = productservice.AddCategory(catgName);

            return id;
        }

        #region Reordercategory

        [HttpPost]
        public void ReorderCategory(int[] idz)
        {
            var isSuccess = productservice.ReorderCategory(idz);
            if (isSuccess != true)
                TempData["Message"] = "Sorting has been done";
        }

        #endregion

        #region DeleteCategory
        [HttpDelete]
        public String DeleteCategory(int id)
        {
            Liquor_ShopEntities _dbContext = new Liquor_ShopEntities();
            //bool result = false;
            var catgry = _dbContext.Product_Categories.Find(id);
            if (catgry != null)
            {
                _dbContext.Product_Categories.Remove(catgry);
                _dbContext.SaveChanges();
                //result = true;
            }
            return "success";
        }

        #endregion

        #region Renamecategory

        [HttpPost]
        public String RenameCategory(string catgName, int id)
        {
            using (Liquor_ShopEntities _dbContext = new Liquor_ShopEntities())
            {
                if (_dbContext.Product_Categories.Any(c => c.CategoryName == catgName))
                    return "titletaken";

                Product_Categories catg = _dbContext.Product_Categories.Find(id);
                catg.CategoryName = catgName;
                catg.Slug = catgName.Replace(" ", "-").ToLower();
                _dbContext.SaveChanges();

            };
            return "ok";
        }

        #endregion

        //Admin part of adding prouct
        [HttpGet]
        public ActionResult AddProduct()
        {
            ProductVM model = new ProductVM();
            using (Liquor_ShopEntities _dbContext = new Liquor_ShopEntities())
            {
                model.Categories = new SelectList(_dbContext.Product_Categories.ToList(), "CatgId", "CategoryName");
            }

            return View(model);
        }

        //Admin part of adding prouct
        [HttpPost]
        public ActionResult AddProduct(ProductVM model, HttpPostedFileBase file)
        {
            Liquor_ShopEntities _dbContext = new Liquor_ShopEntities();
            ProductDTO dto = Mapper.Map<ProductVM, ProductDTO>(model);
            Product_Name prod = Mapper.Map<ProductDTO, Product_Name>(dto);
            var isSave = productservice.AddProduct(prod, file);

            if (isSave == true)
            {
                int id = prod.ProId;
                Product_Name pro = _dbContext.Product_Name.Find(id);
                AddImage(pro, file);
                _dbContext.SaveChanges();
                ViewBag.Message = "Success";

            }
            return RedirectToAction("Products");
        }

        [NonAction]
        public void AddImage(Product_Name prd, HttpPostedFileBase file)
        {
            string imagename = Path.GetFileNameWithoutExtension(file.FileName);
            string extension = Path.GetExtension(file.FileName);
            var originalDir = new DirectoryInfo(String.Format("{0}Images", Server.MapPath(@"\")));
            var pathstring = Path.Combine(originalDir.ToString(), prd.ProductName);
            imagename = imagename + prd.ProId.ToString() + extension;


            if (!Directory.Exists(pathstring))
                Directory.CreateDirectory(pathstring);


            //string path = Path.Combine(Server.MapPath("~/Content/Images/"), imagename);
            var path = string.Format("{0}\\{1}", pathstring, imagename);
            prd.ImageName = imagename;
            file.SaveAs(path);
        }

        [HttpGet]
        public ActionResult Products(int? page, int? catId)
        {
            Liquor_ShopEntities _dbContext = new Liquor_ShopEntities();
            var pageNumber = page ?? 1;
            List<ProductDTO> prod = productservice.Products(page, catId).Select(Mapper.Map<Product_Name, ProductDTO>).ToList();
            List<ProductVM> productvm = prod.Select(v => new ProductVM(v)).ToList();

            ViewBag.Categories = new SelectList(_dbContext.Product_Categories.ToList(), "CatgId", "CategoryName");
            ViewBag.SelectedCat = catId.ToString();
            var onePageOfProducts = productvm.ToPagedList(pageNumber, 3);
            ViewBag.OnePageOfProducts = onePageOfProducts;
            return View(productvm);
        }

        [HttpGet]
        public ActionResult EditProduct(int id)
        {
            Liquor_ShopEntities _dbContext = new Liquor_ShopEntities();
            ProductDTO productdto = Mapper.Map<Product_Name, ProductDTO>(productservice.EditProduct(id));
            ProductVM model = Mapper.Map<ProductDTO, ProductVM>(productdto);
            model.Categories = new SelectList(_dbContext.Product_Categories.ToList(), "CatgId", "CategoryName");
            model.Images = Directory.EnumerateFiles(Server.MapPath("~/Images/" + model.ProductName)).Select(i => Path.GetFileName(i));
            return View(model);
        }

        [HttpPost]
        public ActionResult EditProduct(ProductVM model, HttpPostedFileBase file)
        {
            //int id = model.ProId;
            //using (Liquor_ShopEntities _dbContext = new Liquor_ShopEntities())
            //{
            //    model.Categories = new SelectList(_dbContext.Product_Categories.ToList(), "CatgId", "CategoryName");
            //}
            //model.Images = Directory.EnumerateFiles(Server.MapPath("~/Content/Images")).Select(i => Path.GetFileName(i));
            bool isSave = false;
            Liquor_ShopEntities _dbContext = new Liquor_ShopEntities();

            ProductDTO productdto = Mapper.Map<ProductVM, ProductDTO>(model);
            Product_Name prod = Mapper.Map<ProductDTO, Product_Name>(productdto);
            isSave = productservice.EditProductPost(prod);

            if (isSave == true)
                TempData["Message"] = "You have edited the product";

            if (file != null)
            {
                int id = prod.ProId;
                Product_Name pro = _dbContext.Product_Name.Find(id);
                string path = Server.MapPath("/Images/" + model.ImageName);
                FileInfo filez = new FileInfo(path);
                filez.Delete();
                AddImage(pro, file);
                _dbContext.SaveChanges();
            }
            return RedirectToAction("EditProduct");
        }

        [HttpDelete]
        public String DeleteProduct(int id)
        {
            Liquor_ShopEntities _dbContext = new Liquor_ShopEntities();
            //bool result = false;
            var product = _dbContext.Product_Name.Find(id);
            if (product != null)
            {
                string path = Server.MapPath("/Images/" + product.ImageName);
                FileInfo filez = new FileInfo(path);
                filez.Delete();
                _dbContext.Product_Name.Remove(product);
                _dbContext.SaveChanges();
                //result = true;
            }
            return "success";
        }

        [HttpPost]
        public void SaveGalleryImages(string proName)
        {
            foreach (string image in Request.Files)
            {
                HttpPostedFileBase file = Request.Files[image];

                if(file!=null && file.ContentLength>0)
                {
                    var originalDir = new DirectoryInfo(String.Format("{0}Images", Server.MapPath(@"\")));
                    var pathstring = Path.Combine(originalDir.ToString(), proName);

                    var path = string.Format("{0}\\{1}", pathstring, file.FileName);
                    file.SaveAs(path);
                }
            }
        }


        [HttpPost]
        public string DeleteImage(string proName, string imageName)
        {
            string imagePath = Request.MapPath("~/Images/" + proName + "/" + imageName);

            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);

            return "success";
        }
    }
}
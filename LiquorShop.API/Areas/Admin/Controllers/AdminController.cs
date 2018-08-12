using AutoMapper;
using LiquorShop.API.Areas.Admin.Models.ViewModel;
using LiquorShop.API.Models.DTO;
using LiquorShop_AppLayer.Services;
using LiquorShop_DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiquorShop.API.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {

        private IProductService productservice = new ProductService();
        private IAdminService adminService = new AdminService();


        //// GET: Admin/Admin
        [HttpGet]
        public ActionResult Index()
        {
            List<PageDTO> PageList;
            List<PageVM> Pagevm;
            PageList = adminService.GetPages().Select(Mapper.Map<Page, PageDTO>).ToList();
            Pagevm = PageList.OrderBy(x => x.Sorting).Select(x => new PageVM(x)).ToList();
            return View(Pagevm);
        }


        #region AddNewPage
        [HttpGet]
        public ActionResult AddPage()
        {
            //var viewModel = new PageVM();
            return View();
        }

        [HttpPost]
        public ActionResult AddPage(PageVM pageVm)
        {
            var page = Mapper.Map<PageDTO, Page>(pageVm.Pagedto);
            var isSave = adminService.AddPage(page);
            if (isSave != true)
                TempData["Message"] = "Title or the slug is not unique";
            else
                TempData["Message"] = "The page has been added successfully";
            return RedirectToAction("AddPage");
        }
        #endregion

        #region EditExistingPage
        [HttpGet]
        public ActionResult EditPage(int id)
        {
            Liquor_ShopEntities _dbContext = new Liquor_ShopEntities();
            var page = _dbContext.Pages.Find(id);
            var pagedto = Mapper.Map<Page, PageDTO>(page);
            PageVM model = new PageVM(pagedto);

            return View(model);
        }

        [HttpPost]
        public ActionResult EditPage(PageVM pageVm)
        {
            var page = Mapper.Map<PageDTO, Page>(pageVm.Pagedto);
            var isSave = adminService.EditPage(page);
            if (isSave != true)
                TempData["Message"] = "Title or the slug already exists";
            else
                TempData["Message"] = "The page has been edited successfully";


            return RedirectToAction("EditPage");
        }
        #endregion

        #region PageDetail
        [HttpGet]
        public ActionResult PageDetail(int id)
        {
            Liquor_ShopEntities _dbContext = new Liquor_ShopEntities();
            var page = _dbContext.Pages.Find(id);
            var pagedto = Mapper.Map<Page, PageDTO>(page);
            PageVM model = new PageVM(pagedto);

            return View(model);
        }
        #endregion

        #region Deletepage
        [HttpPost]
        public String DeletePage(int id)
        {
            Liquor_ShopEntities _dbContext = new Liquor_ShopEntities();
            //bool result = false;
            var page = _dbContext.Pages.Find(id);
            if (page != null)
            {
                _dbContext.Pages.Remove(page);
                _dbContext.SaveChanges();
                //result = true;
            }
            return "success";
        }

        #endregion

        #region Reorderpages

        [HttpPost]
        public void ReorderPages( int[] idz)
        {
            var isSuccess=adminService.ReorderPage(idz);
            if (isSuccess != true)
                TempData["Message"] = "Sorting has been done";
        }

        #endregion

        #region EditSidebar

        [HttpGet]
        public ActionResult EditSidebar()
        {
            SidebarVM model;
            using (Liquor_ShopEntities _dbContext = new Liquor_ShopEntities())
            {
                tblSidebar sidebar = _dbContext.tblSidebars.Find(1);
                var sidedto = Mapper.Map<tblSidebar, SidebarDTO>(sidebar);
                model = new SidebarVM(sidedto);
            }

            return View(model);
        }


        [HttpPost]
        public ActionResult EditSidebar(SidebarVM model)
        {
            var sidebar = Mapper.Map<SidebarDTO, tblSidebar>(model.SidebarDto);
            var sbar = adminService.EditSidebar(sidebar);
           
             model = new SidebarVM(Mapper.Map<tblSidebar, SidebarDTO>(sbar));
            return View(model);
        }
        #endregion
    }
}
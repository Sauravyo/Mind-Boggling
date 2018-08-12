using LiquorShop_DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiquorShop_AppLayer.Services
{
    public class AdminService : IAdminService
    {
        public List<Page> GetPages()
        {
            using (Liquor_ShopEntities _dbContext = new Liquor_ShopEntities())
            {
                return _dbContext.Pages.ToList();
            }
        }

        public bool AddPage(Page model)
        {
            using (Liquor_ShopEntities _dbContext = new Liquor_ShopEntities())
            {
                string slug;
                Page page = new Page
                {
                    PageTitle = model.PageTitle
                };
                //check slug and set
                if (string.IsNullOrWhiteSpace(model.Slug))
                {
                    slug = model.PageTitle.Replace(" ", "-").ToLower();
                }
                else
                {
                    slug = model.Slug.Replace(" ", "-").ToLower();
                }
                //Set title and slug unique
                if (_dbContext.Pages.Any(p => p.PageTitle == model.PageTitle || _dbContext.Pages.Any(s => s.Slug == model.Slug)))
                {

                    return false;

                }
                page.Slug = slug;
                page.Body = model.Body;
                page.HasSidebar = model.HasSidebar;
                page.Sorting = 100;
                _dbContext.Pages.Add(page);
                _dbContext.SaveChanges();

                return true;
            }

        }


        public bool EditPage(Page model)
        {
            using (Liquor_ShopEntities _dbContext = new Liquor_ShopEntities())
            {
                int id = model.PageId;
                string slug = "home";

                Page page = _dbContext.Pages.Find(id);
                page.PageTitle = model.PageTitle;

                //check slug and set
                if (model.Slug != "home")
                {
                    if (string.IsNullOrWhiteSpace(model.Slug))
                    {
                        slug = model.PageTitle.Replace(" ", "-").ToLower();
                    }
                    else
                    {
                        slug = model.Slug.Replace(" ", "-").ToLower();
                    }
                }
                //Set title and slug unique
                if (_dbContext.Pages.Where(p => p.PageId != id).Any(p => p.PageTitle == model.PageTitle)
                    || _dbContext.Pages.Where(s => s.PageId != id).Any(s => s.Slug == slug))
                {
                    return false;
                }

                page.Slug = slug;
                page.Body = model.Body;
                page.HasSidebar = model.HasSidebar;
                _dbContext.SaveChanges();
                return true;
            }
        }


        public bool ReorderPage(int[] ids)
        {
            using (Liquor_ShopEntities _dbContext = new Liquor_ShopEntities())
            {
                int count = 1;
                Page pg;
                foreach (var pgId in ids)
                {
                    pg = _dbContext.Pages.Find(pgId);
                    pg.Sorting = count;
                    _dbContext.SaveChanges();
                    count++;
                }
            }

            return true;
        }

        public tblSidebar EditSidebar(tblSidebar bar)
        {
            tblSidebar sidebar = new tblSidebar();
            using (Liquor_ShopEntities _dbContext = new Liquor_ShopEntities())
            {
                sidebar = _dbContext.tblSidebars.Find(1);
                sidebar.Body = bar.Body;
                _dbContext.SaveChanges();
            }
            return sidebar;
        }
    }
}
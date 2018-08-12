using LiquorShop.API.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiquorShop.API.Areas.Admin.Models.ViewModel
{
    public class SidebarVM
    {
        public SidebarDTO SidebarDto { get; set; }
        public SidebarVM()
        {

        }

        public SidebarVM(SidebarDTO Sidebar)
        {
            SidebarDto = Sidebar;
        }
    }
}
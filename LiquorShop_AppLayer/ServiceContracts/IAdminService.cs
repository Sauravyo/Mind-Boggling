using LiquorShop_DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiquorShop_AppLayer.Services
{
    public interface IAdminService
    {
        bool AddPage(Page page);

        List<Page> GetPages();

        bool EditPage(Page page);

        bool ReorderPage(int[] ids);

        tblSidebar EditSidebar(tblSidebar sidebar);
    }
}

using LiquorShop.API.Models.DTO;
using LiquorShop_DataLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LiquorShop.API.Areas.Admin.Models.ViewModel
{
    [Table("Pages")]
    public class PageVM
    {
        public PageDTO Pagedto { get; set; }
        public PageVM()
        {

        }

        public PageVM(PageDTO Pg)
        {
            Pagedto = Pg;
        }
        
    }
}
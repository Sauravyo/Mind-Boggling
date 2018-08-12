using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiquorShop.API.Models.DTO
{
    
    public class PageDTO
    {
        public int PageId { get; set; }
        public string PageTitle { get; set; }
        public string Slug { get; set; }
        [AllowHtml]
        public string Body { get; set; }
        public int? Sorting { get; set; }
        public bool? HasSidebar { get; set; }
    }
}
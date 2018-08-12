using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiquorShop.API.Models.DTO
{
    public class SidebarDTO
    {
        [Key]
        public int Id { get; set; }
        [AllowHtml]
        public string Body { get; set; }
    }
}
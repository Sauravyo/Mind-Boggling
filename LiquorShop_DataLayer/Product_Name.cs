//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LiquorShop_DataLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product_Name
    {
        public int ProId { get; set; }
        public string ProductName { get; set; }
        public Nullable<int> SKU { get; set; }
        public string Size { get; set; }
        public Nullable<decimal> Price { get; set; }
        public int CatFKId { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public string ImageName { get; set; }
    
        public virtual Product_Categories Product_Categories { get; set; }
    }
}
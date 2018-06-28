using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Workshop1.Models
{
    public class OrderDetail
    {
        public int? OrderID { get; set; }
        public List<ProductDorpDown> ProductID { get; set; }
        public decimal? UnitPrice { get; set; }
        public int? Qty { get; set; }
        public double? Discount { get; set; }
    }
}
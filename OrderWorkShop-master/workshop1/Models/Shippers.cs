using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Workshop1.Models
{
    public class Shippers
    {
        /// <summary>
        /// 發貨人編號
        /// </summary>
        ///
        public int ShipperID { get; set; }
        /// <summary>
        /// 出貨公司
        /// </summary>
        /// 
        public string CompanyName { get; set; }
        /// <summary>
        /// 電話
        /// </summary>
        /// 
        public string Phone { get; set; }
    }
}
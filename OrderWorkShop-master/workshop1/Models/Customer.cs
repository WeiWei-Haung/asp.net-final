﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Workshop1.Models
{
    public class Customer
    {
        /// <summary>
        /// 客戶編號
        /// </summary>
        ///
        public int CustomerID { get; set; }
        /// <summary>
        /// 客戶名稱
        /// </summary>
        /// 
        public string CompanyName { get; set; }
        /// <summary>
        /// 聯繫人姓名
        /// </summary>
        /// 
        public string ContactName { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        /// 
        public string Address { get; set; }
    }
}
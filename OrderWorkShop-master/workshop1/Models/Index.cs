using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace Workshop1.Models
{
    public class Index
    {
        /// <summary>
        /// 訂單編號
        /// </summary>
        ///
        [DisplayName("訂單編號")]
        public int? OrderID { get; set; }
        /// <summary>
        /// 公司名稱
        /// </summary>
        /// 
        [DisplayName("客戶名稱")]
        public string CompanyName { get; set; }
        /// <summary>
        /// 員工姓名
        /// </summary>
        /// 
        [DisplayName("員工姓名")]
        public int? EmployeeId { get; set; }
        /// <summary>
        /// 出貨公司
        /// </summary>
        /// 
        [DisplayName("出貨公司")]
        public int? ShippersId { get; set; }
        /// <summary>
        /// 訂單日期
        /// </summary>
        /// 
        [DisplayName("訂單日期")]
        public DateTime? OrderDate { get; set; }
        /// <summary>
        /// 需要日期
        /// </summary>
        ///
        [DisplayName("需要日期")]
        public DateTime? RequiredDate { get; set; }
        /// <summary>
        /// 發貨日期
        /// </summary>
        /// 
        [DisplayName("發貨日期")]
        public DateTime? ShipperDate { get; set; }
    }
}
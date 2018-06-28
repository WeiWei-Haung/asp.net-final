using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Workshop1.Models
{
    public class Employees
    {
        /// <summary>
        /// 員工編號
        /// </summary>
        ///
        public int EmployeeId { get; set; }
        /// <summary>
        /// 員工姓
        /// </summary>
        /// 
        public string EmployeeFirstName { get; set; }
        /// <summary>
        /// 員工名
        /// </summary>
        /// 
        public string EmployeeLastName { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Workshop1.Dao;


namespace Workshop1.Models.Services
{
    public class CustomerService
    {
        /// <summary>
        /// customer下拉式選單
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetCustomerList()
        {
            CustomerDao customerDao = new CustomerDao();
            return customerDao.GetCustomerList();
        }
    }
}
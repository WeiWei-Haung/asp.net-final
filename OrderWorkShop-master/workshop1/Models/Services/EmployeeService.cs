﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Workshop1.Dao;

namespace Workshop1.Models.Services
{
    /// <summary>
    /// new employee 假資料
    /// </summary>
    public class EmployeeService
    {
        /// <summary>
        /// employee下拉式選單
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetEmpName()
        {
            EmployeeDao employeeDao = new EmployeeDao();
            return employeeDao.GetEmpName();
        }
    }
}
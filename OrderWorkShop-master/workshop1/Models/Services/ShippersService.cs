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
    public class ShippersService
    {
        /// <summary>
        /// shipper 下拉式選單
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetShipper()
        {
            ShipperDao shipperDao = new ShipperDao();
            return shipperDao.GetShipper();
        }
    }
}
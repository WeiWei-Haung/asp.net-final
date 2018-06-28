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
    public class OrderService
    {
        /// <summary>
        /// 查詢order條件資料
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public List<Index> GetOrderCondition(Index arg)
        {
            OrderDao orderDao = new OrderDao();
            return orderDao.GetOrderCondition(arg);
        }
        /// <summary>
        /// 新增一筆order資料
        /// </summary>
        /// <param name="order"></param>
        public string InsertOrder(Order order)
        {
            OrderDao orderDao = new OrderDao();
            return orderDao.InsertOrder(order);
        }
        /// <summary>
        /// 刪除一筆order資料
        /// </summary>
        /// <param name="orderid"></param>
        public string Del(string orderid)
        {
            OrderDao orderDao = new OrderDao();
            return orderDao.Del(orderid);
        }
        /// <summary>
        /// update一筆order資料
        /// </summary>
        /// <param name="order"></param>
        public string Update(Order order)
        {
            OrderDao orderDao = new OrderDao();
            string mess = orderDao.Update(order);
            return mess;
        }
        public List<SelectListItem> GetOrderDetailList()
        {
            OrderDao orderDao = new OrderDao();
            return orderDao.GetOrderDetailList();
        }
        public string GetPrice(string arg)
        {
            OrderDao orderDao = new OrderDao();
            return orderDao.GetPrice(arg);
        }
        public Order GetOrders(int orderid)
        {
            OrderDao orderDao = new OrderDao();
            return orderDao.GetOrders(orderid);
        }
    }
}
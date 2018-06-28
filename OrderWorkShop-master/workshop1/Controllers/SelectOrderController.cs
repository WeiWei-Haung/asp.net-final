using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Workshop1.Models;
using Workshop1.Models.Services;
using System.Data;

namespace Workshop1.Controllers
{
    public class SelectOrderController : Controller
    {
        // GET: SelectOrder
        /// <summary>
        /// 查詢首頁
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            //準備employee下拉選單
            EmployeeService employeeservice = new EmployeeService();
            ViewBag.employeelist = employeeservice.GetEmpName();
            //準備shipper下拉選單
            ShippersService shippersservice = new ShippersService();
            ViewBag.shipperslist = shippersservice.GetShipper();
            return View();
        }
        /// <summary>
        /// action 查詢
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SelectList(Index arg)
        {
            //篩選查詢條件
            OrderService orderService = new OrderService();
            List<Index> orderList = orderService.GetOrderCondition(arg);
            return this.Json(orderList);
        }
        /// <summary>
        /// 新增頁
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult InsertOrder()
        {
            //準備employee下拉選單
            EmployeeService employeeservice = new EmployeeService();
            ViewBag.employeelist = employeeservice.GetEmpName();
            //準備shipper下拉選單
            ShippersService shippersservice = new ShippersService();
            ViewBag.shipperslist = shippersservice.GetShipper();
            //準備customer下拉選單
            CustomerService customerservice = new CustomerService();
            ViewBag.customerlist = customerservice.GetCustomerList();
            return View();
        }
        /// <summary>
        /// action 新增
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult InsertOrder(Order arg)
        {
            OrderService orderService = new OrderService();
            string insertmessage = orderService.InsertOrder(arg);
            return this.Json(insertmessage);
        }
        [HttpPost]
        public JsonResult Del(string orderid)
        {
            //刪除動作
            OrderService orderService = new OrderService();
            string result = orderService.Del(orderid);
            return this.Json(result);
        }
        /// <summary>
        /// 更新資料頁
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Update(int orderid)
        {
            //準備orderService
            OrderService orderService = new OrderService();
            //產品下拉選單
            List<SelectListItem> productlist = orderService.GetOrderDetailList();
            ViewBag.product = productlist;
            //準備employee下拉選單
            EmployeeService employeeservice = new EmployeeService();
            List<SelectListItem> employeeitems = employeeservice.GetEmpName();            
            ViewBag.employeelist = employeeitems;
            //準備shipper下拉選單
            ShippersService shippersservice = new ShippersService();
            List<SelectListItem> shippersitems = shippersservice.GetShipper();            
            ViewBag.shipperslist = shippersitems;
            //準備customer下拉選單
            CustomerService customerservice = new CustomerService();
            List<SelectListItem> customeritems = customerservice.GetCustomerList();            
            ViewBag.customerlist = customeritems;
            //把order資料丟去view
            Order order = orderService.GetOrders(orderid);
            return View(order);
        }
        /// <summary>
        /// action update
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Update(Order arg)
        {
            OrderService orderService = new OrderService();
            string result = orderService.Update(arg);
            return this.Json(result);
        }
        [HttpPost]
        public JsonResult Create()
        {
            //準備商品下拉選單
            OrderService orderService = new OrderService();
            List<SelectListItem> result = orderService.GetOrderDetailList();
            return this.Json(result);
        }
        [HttpPost]
        public JsonResult Price(string arg)
        {
            OrderService orderService = new OrderService();
            string price = orderService.GetPrice(arg);
            return this.Json(price);
        }
    }
}
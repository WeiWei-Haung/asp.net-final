using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Workshop1.Dao
{
    public class ShipperDao
    {
        /// <summary>
        /// shipper 下拉式選單
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetShipper()
        {
            ConnDao dbService = new ConnDao();
            string conn = dbService.GetConnStr();
            SqlConnection sqlConnection = new SqlConnection(conn);
            string sql = "select Sales.Shippers.ShipperID,Sales.Shippers.CompanyName from Sales.Shippers";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, sqlConnection);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            DataTable dataTable = ds.Tables[0];
            List<SelectListItem> shipperslist = new List<SelectListItem>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                shipperslist.Add(new SelectListItem()
                {
                    Text = dataTable.Rows[i][1].ToString(),
                    Value = dataTable.Rows[i][0].ToString()
                });
            }
            return shipperslist;
        }
    }
}
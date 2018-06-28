using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Workshop1.Dao
{
    public class EmployeeDao
    {
        /// <summary>
        /// employee下拉式選單
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetEmpName()
        {
            ConnDao dbService = new ConnDao();
            string conn = dbService.GetConnStr();
            SqlConnection sqlConnection = new SqlConnection(conn);
            string sql = "select hr.Employees.EmployeeID,hr.Employees.FirstName+hr.Employees.LastName from hr.Employees";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, sqlConnection);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            DataTable dataTable = ds.Tables[0];
            List<SelectListItem> employeelist = new List<SelectListItem>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                employeelist.Add(new SelectListItem()
                {
                    Text = dataTable.Rows[i][1].ToString(),
                    Value = dataTable.Rows[i][0].ToString()
                });
            }
            return employeelist;
        }
    }
}
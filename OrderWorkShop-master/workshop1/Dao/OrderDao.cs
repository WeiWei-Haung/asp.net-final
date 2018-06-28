using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using Workshop1.Models;

namespace Workshop1.Dao
{
    public class OrderDao
    {
        /// <summary>
        /// 查詢order條件資料
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public List<Index> GetOrderCondition(Index arg)
        {
            ConnDao dbService = new ConnDao();
            string conn = dbService.GetConnStr();
            SqlConnection sqlConnection = new SqlConnection(conn);
            string sql = @"select 
                        OrderID,
                        CompanyName,
                        OrderDate,
                        ShippedDate 
                        from 
                        Sales.Orders join Sales.Customers on Sales.Orders.CustomerID = Sales.Customers.CustomerID 
                        where";
            if (arg.OrderID.HasValue)
            {
                sql += " OrderID = @OrderID";
            }
            else
            {
                sql += " OrderID like @OrderID";
            }
            if (!string.IsNullOrWhiteSpace(arg.CompanyName))
            {
                sql += " and CompanyName like @CompanyName";
            }
            if (arg.EmployeeId.HasValue)
            {
                sql += " and EmployeeID = @EmployeeID";
            }
            if (arg.ShippersId.HasValue)
            {
                sql += " and ShipperID = @ShipperID";
            }
            if (arg.ShipperDate.HasValue)
            {
                sql += " and ShippedDate = @ShippedDate";
            }
            if (arg.OrderDate.HasValue)
            {
                sql += " and OrderDate = @OrderDate";
            }
            if (arg.RequiredDate.HasValue)
            {
                sql += " and RequiredDate = @RequiredDate";
            }
            SqlCommand command = new SqlCommand(sql, sqlConnection);
            if (arg.OrderID.HasValue)
            {
                command.Parameters.Add(new SqlParameter("@OrderID", arg.OrderID));
            }
            else
            {
                command.Parameters.Add(new SqlParameter("@OrderID", "%%"));
            }
            if (!string.IsNullOrWhiteSpace(arg.CompanyName))
            {
                command.Parameters.Add(new SqlParameter("@CompanyName", "%" + arg.CompanyName + "%"));
            }
            if (arg.EmployeeId.HasValue)
            {
                command.Parameters.Add(new SqlParameter("@EmployeeID", arg.EmployeeId));
            }
            if (arg.ShippersId.HasValue)
            {
                command.Parameters.Add(new SqlParameter("@ShipperID", arg.ShippersId));
            }
            if (arg.OrderDate.HasValue)
            {
                command.Parameters.Add(new SqlParameter("@OrderDate", arg.OrderDate));
            }
            if (arg.ShipperDate.HasValue)
            {
                command.Parameters.Add(new SqlParameter("@ShippedDate", arg.ShipperDate));
            }
            if (arg.RequiredDate.HasValue)
            {
                command.Parameters.Add(new SqlParameter("@RequiredDate", arg.RequiredDate));
            }
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);

            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            List<Index> orders = new List<Index>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                orders.Add(new Index
                {
                    OrderID = Convert.ToInt32(row["OrderID"]),
                    CompanyName = row["CompanyName"].ToString(),
                    OrderDate = Convert.ToDateTime(row["OrderDate"]),
                    ShipperDate = (!string.IsNullOrWhiteSpace(row["ShippedDate"].ToString())) ? new DateTime?(Convert.ToDateTime(row["ShippedDate"])) : null
                });
            }
            return orders;
        }
        /// <summary>
        /// 新增一筆order資料
        /// </summary>
        /// <param name="order"></param>
        public string InsertOrder(Order order)
        {
            string mess = "";
            ConnDao dbService = new ConnDao();
            string conn = dbService.GetConnStr();
            SqlConnection sqlConnection = new SqlConnection(conn);
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    string insertsql = @"INSERT INTO 
                                        [Sales].[Orders](
                                        [CustomerID],
                                        [EmployeeID],
                                        [OrderDate],
                                        [RequiredDate],
                                        [ShippedDate],
                                        [ShipperID],
                                        [Freight],
                                        [ShipName],
                                        [ShipAddress],
                                        [ShipCity],
                                        [ShipRegion],
                                        [ShipPostalCode],
                                        [ShipCountry]) 
                                        VALUES(
                                        @CustomerID,
                                        @EmployeeID,
                                        @OrderDate,
                                        @RequiredDate,
                                        @ShippedDate,
                                        @ShipperID,
                                        @Freight,
                                        @ShipName,
                                        @ShipAddress,
                                        @ShipCity,
                                        @ShipRegion,
                                        @ShipPostalCode,
                                        @ShipCountry) 
                                        select SCOPE_IDENTITY()";
                    SqlCommand command = new SqlCommand(insertsql, sqlConnection);
                    command.Parameters.Add(new SqlParameter("@CustomerID", order.CustomerID));
                    command.Parameters.Add(new SqlParameter("@EmployeeID", order.EmployeeID));
                    command.Parameters.Add(new SqlParameter("@OrderDate", order.OrderDate));
                    command.Parameters.Add(new SqlParameter("@RequiredDate", order.RequiredDate));
                    if (order.ShipperDate.HasValue)
                    {
                        command.Parameters.Add(new SqlParameter("@ShippedDate", order.ShipperDate));
                    }
                    else
                    {
                        command.Parameters.Add(new SqlParameter("@ShippedDate", DBNull.Value));
                    }
                    command.Parameters.Add(new SqlParameter("@ShipperID", order.ShipperID));
                    command.Parameters.Add(new SqlParameter("@Freight", order.Freight));
                    command.Parameters.Add(new SqlParameter("@ShipName", order.ShipName));
                    command.Parameters.Add(new SqlParameter("@ShipAddress", order.ShipAddress));
                    command.Parameters.Add(new SqlParameter("@ShipCity", order.ShipCity));
                    if (!string.IsNullOrWhiteSpace(order.ShipRegion))
                    {
                        command.Parameters.Add(new SqlParameter("@ShipRegion", order.ShipRegion));
                    }
                    else
                    {
                        command.Parameters.Add(new SqlParameter("@ShipRegion", DBNull.Value));
                    }
                    if (!string.IsNullOrWhiteSpace(order.ShipPostalCode))
                    {
                        command.Parameters.Add(new SqlParameter("@ShipPostalCode", order.ShipPostalCode));
                    }
                    else
                    {
                        command.Parameters.Add(new SqlParameter("@ShipPostalCode", DBNull.Value));
                    }
                    command.Parameters.Add(new SqlParameter("@ShipCountry", order.ShipCountry));
                    sqlConnection.Open();
                    int newid = Convert.ToInt32(command.ExecuteScalar());
                    if (order.OrderDetails != null)
                    {
                        string sql2 = @"INSERT INTO 
                                [Sales].[OrderDetails](
                                [OrderID],
                                [ProductID],
                                [UnitPrice],
                                [Qty],
                                [Discount]) 
                                VALUES(
                                @OrderID,
                                @ProductID,
                                @UnitPrice,
                                @Qty,
                                @Discount) 
                                select SCOPE_IDENTITY()";
                        SqlCommand command2 = new SqlCommand(sql2, sqlConnection);
                        for (int i = 0; i < order.OrderDetails.Count; i++)
                        {
                            command2.Parameters.Add(new SqlParameter("@OrderID", newid));
                            command2.Parameters.Add(new SqlParameter("@ProductID", order.OrderDetails[i].ProductID[0].Value));
                            command2.Parameters.Add(new SqlParameter("@UnitPrice", order.OrderDetails[i].UnitPrice));
                            command2.Parameters.Add(new SqlParameter("@Qty", order.OrderDetails[i].Qty));
                            command2.Parameters.Add(new SqlParameter("@Discount", "0"));
                            command2.ExecuteScalar();
                            command2.Parameters.Clear();
                        }
                    }
                    mess = "新增失敗";
                    scope.Complete();
                    mess = newid.ToString();
                }

            }
            catch (Exception ex)
            {

            }
            finally
            {
                sqlConnection.Close();
            }
            return mess;
        }
        /// <summary>
        /// 刪除一筆order資料
        /// </summary>
        /// <param name="orderid"></param>
        public string Del(string orderid)
        {
            string message = "";
            ConnDao dbService = new ConnDao();
            string connstr = dbService.GetConnStr();
            SqlConnection sqlConnection = new SqlConnection(connstr);
            string sql = @"DELETE 
                        FROM [Sales].[OrderDetails] 
                        WHERE OrderID = @OrderID 
                        DELETE 
                        FROM [Sales].[Orders] 
                        WHERE OrderID = @OrderID";
            SqlCommand command = new SqlCommand(sql, sqlConnection);
            command.Parameters.Add(new SqlParameter("@OrderID", orderid));
            sqlConnection.Open();
            SqlTransaction transaction = sqlConnection.BeginTransaction();
            command.Transaction = transaction;
            try
            {
                command.ExecuteNonQuery();
                transaction.Commit();
                message = "刪除成功";
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                message = ex.Message;
            }
            finally
            {
                sqlConnection.Close();
            }
            return message;
        }
        /// <summary>
        /// update一筆order資料
        /// </summary>
        /// <param name="order"></param>
        public string Update(Order order)
        {
            ConnDao dbService = new ConnDao();
            string conn = dbService.GetConnStr();
            SqlConnection connection = new SqlConnection(conn);
            string mess = "";
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    string sql = @"UPDATE [Sales].[Orders]
                                    SET
                                    [CustomerID] = @CustomerID
                                    ,[EmployeeID] = @EmployeeID
                                    ,[OrderDate] = @OrderDate
                                    ,[RequiredDate] = @RequiredDate
                                    ,[ShippedDate] = @ShippedDate
                                    ,[ShipperID] = @ShipperID
                                    ,[Freight] = @Freight
                                    ,[ShipName] = @ShipName
                                    ,[ShipAddress] = @ShipAddress
                                    ,[ShipCity] = @ShipCity
                                    ,[ShipRegion] = @ShipRegion
                                    ,[ShipPostalCode] = @ShipPostalCode
                                    ,[ShipCountry] = @ShipCountry
                                    WHERE 
                                    OrderID = @OrderID 
                                    DELETE
                                    FROM [Sales].[OrderDetails]
                                    WHERE OrderID = @OrderID";
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.Add(new SqlParameter("@OrderID", order.OrderID));
                    command.Parameters.Add(new SqlParameter("@CustomerID", order.CustomerID));
                    command.Parameters.Add(new SqlParameter("@EmployeeID", order.EmployeeID));
                    command.Parameters.Add(new SqlParameter("@OrderDate", order.OrderDate));
                    command.Parameters.Add(new SqlParameter("@RequiredDate", order.RequiredDate));
                    if (order.ShipperDate.HasValue)
                    {
                        command.Parameters.Add(new SqlParameter("@ShippedDate", order.ShipperDate));
                    }
                    else
                    {
                        command.Parameters.Add(new SqlParameter("@ShippedDate", DBNull.Value));
                    }
                    command.Parameters.Add(new SqlParameter("@ShipperID", order.ShipperID));
                    command.Parameters.Add(new SqlParameter("@Freight", order.Freight));
                    command.Parameters.Add(new SqlParameter("@ShipName", order.ShipName));
                    command.Parameters.Add(new SqlParameter("@ShipAddress", order.ShipAddress));
                    command.Parameters.Add(new SqlParameter("@ShipCity", order.ShipCity));
                    if (!string.IsNullOrWhiteSpace(order.ShipRegion))
                    {
                        command.Parameters.Add(new SqlParameter("@ShipRegion", order.ShipRegion));
                    }
                    else
                    {
                        command.Parameters.Add(new SqlParameter("@ShipRegion", DBNull.Value));
                    }
                    if (!string.IsNullOrWhiteSpace(order.ShipPostalCode))
                    {
                        command.Parameters.Add(new SqlParameter("@ShipPostalCode", order.ShipPostalCode));
                    }
                    else
                    {
                        command.Parameters.Add(new SqlParameter("@ShipPostalCode", DBNull.Value));
                    }
                    command.Parameters.Add(new SqlParameter("@ShipCountry", order.ShipCountry));
                    connection.Open();
                    command.ExecuteNonQuery();
                    string sql2 = @"INSERT INTO 
                                [Sales].[OrderDetails]
                                (
                                [OrderID],
                                [ProductID],
                                [UnitPrice],
                                [Qty],
                                [Discount]
                                ) 
                                VALUES(@OrderID,
                                @ProductID,
                                @UnitPrice,
                                @Qty,
                                @Discount) 
                                select SCOPE_IDENTITY()";
                    SqlCommand command2 = new SqlCommand(sql2, connection);
                    for (int i = 0; i < order.OrderDetails.Count; i++)
                    {
                        command2.Parameters.Add(new SqlParameter("@OrderID", order.OrderID));
                        command2.Parameters.Add(new SqlParameter("@ProductID", order.OrderDetails[i].ProductID[0].Value));
                        command2.Parameters.Add(new SqlParameter("@UnitPrice", order.OrderDetails[i].UnitPrice));
                        command2.Parameters.Add(new SqlParameter("@Qty", order.OrderDetails[i].Qty));
                        command2.Parameters.Add(new SqlParameter("@Discount", "0"));
                        command2.ExecuteScalar();
                        command2.Parameters.Clear();
                    }
                    mess = "修改失敗";
                    scope.Complete();
                    mess = order.OrderID.ToString();
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                connection.Close();
            }
            return mess;
        }
        public List<SelectListItem> GetOrderDetailList()
        {
            ConnDao dbService = new ConnDao();
            string conn = dbService.GetConnStr();
            SqlConnection sqlConnection = new SqlConnection(conn);
            string sql = @"SELECT 
                        [ProductID],
                        [ProductName]  
                        FROM [Production].[Products]";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, sqlConnection);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            DataTable dataTable = ds.Tables[0];
            List<SelectListItem> orderdetaillist = new List<SelectListItem>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                orderdetaillist.Add(new SelectListItem()
                {
                    Text = dataTable.Rows[i][1].ToString(),
                    Value = dataTable.Rows[i][0].ToString()
                });
            }
            return orderdetaillist;
        }
        public string GetPrice(string arg)
        {
            ConnDao dbService = new ConnDao();
            string conn = dbService.GetConnStr();
            SqlConnection sqlConnection = new SqlConnection(conn);
            string sql = @"SELECT 
                        [UnitPrice]  
                        FROM [Production].[Products] 
                        WHERE [ProductID] = @ProductID";
            SqlCommand command = new SqlCommand(sql, sqlConnection);
            command.Parameters.Add(new SqlParameter("@ProductID", arg));
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            DataTable dataTable = ds.Tables[0];
            return Convert.ToDouble(dataTable.Rows[0]["UnitPrice"]).ToString();
        }
        public Order GetOrders(int orderid)
        {
            ConnDao dbService = new ConnDao();
            string conn = dbService.GetConnStr();
            SqlConnection sqlConnection = new SqlConnection(conn);
            string sql = @"SELECT 
                        a.[OrderID],
                        [CustomerID],
                        [EmployeeID],
                        [OrderDate],
                        [RequiredDate],
                        [ShippedDate],
                        [ShipperID],
                        [Freight],
                        [ShipName],
                        [ShipAddress],
                        [ShipCity],
                        [ShipRegion],
                        [ShipPostalCode],
                        [ShipCountry],
                        b.[ProductID],
                        b.[UnitPrice],
                        [Qty],
						[ProductName]
                        FROM [Sales].[Orders] as a join[Sales].[OrderDetails] as b on a.OrderID = b.OrderID join [Production].[Products] as c on b.ProductID = c.ProductID
                        Where a.OrderID = @OrderID";
            SqlCommand command = new SqlCommand(sql, sqlConnection);
            command.Parameters.Add(new SqlParameter("@OrderID", orderid));
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            List<OrderDetail> details = new List<OrderDetail>();
            List<ProductDorpDown> productDorpDowns = new List<ProductDorpDown>();
            Order orders = new Order();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                details.Add(new OrderDetail
                {
                    ProductID = new List<ProductDorpDown>()
                    {
                        new ProductDorpDown
                        {
                            Value = Convert.ToInt32(row["ProductID"]),
                            Text = row["ProductName"].ToString()
                        }
                    },
                    UnitPrice = Convert.ToDecimal(row["UnitPrice"]),
                    Qty = Convert.ToInt32(row["Qty"])
                });
                orders.OrderID = Convert.ToInt32(row["OrderID"]);
                orders.CustomerID = Convert.ToInt32(row["CustomerID"]);
                orders.EmployeeID = Convert.ToInt32(row["EmployeeID"]);
                orders.OrderDate = Convert.ToDateTime(row["OrderDate"]);
                orders.RequiredDate = Convert.ToDateTime(row["RequiredDate"]);
                orders.ShipperDate = (!string.IsNullOrWhiteSpace(row["ShippedDate"].ToString())) ? new DateTime?(Convert.ToDateTime(row["ShippedDate"])) : null;
                orders.ShipperID = Convert.ToInt32(row["ShipperID"]);
                orders.Freight = Convert.ToDecimal(row["Freight"]);
                orders.ShipName = row["ShipName"].ToString();
                orders.ShipAddress = row["ShipAddress"].ToString();
                orders.ShipCity = row["ShipCity"].ToString();
                orders.ShipRegion = row["ShipRegion"].ToString();
                orders.ShipPostalCode = row["ShipPostalCode"].ToString();
                orders.ShipCountry = row["ShipCountry"].ToString();
                orders.OrderDetails = details;
            }
            return orders;
        }
    }
}
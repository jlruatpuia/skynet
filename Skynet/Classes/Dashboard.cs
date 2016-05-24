using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skynet.Classes
{
    class Dashboard
    {
        OleDbConnection cm = new OleDbConnection(Utils.ConnString);

        public Server2Client SalesByCategory(int Month)
        {
            Server2Client sc = new Server2Client();
            OleDbCommand cmd = new OleDbCommand("SELECT Category.CategoryName, Sum(SaleDetail.Quantity) AS SumOfQuantity FROM Sale INNER JOIN((Category INNER JOIN Product ON Category.ID = Product.CategoryID) INNER JOIN SaleDetail ON Product.ID = SaleDetail.ProductID) ON Sale.InvoiceNo = SaleDetail.InvoiceNo WHERE(((Month([SaleDate])) = " + Month + ")) GROUP BY Category.CategoryName", cm);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            sc.dataTable = ds.Tables[0];
            return sc;
        }

        //public Server2Client SalesByCategory(int Month)
        //{
        //    Server2Client sc = new Server2Client();
        //    OleDbCommand cmd = new OleDbCommand("SELECT Day([SaleDate]) AS SalesDate, Count(Category.CategoryName) AS CountOfCategoryName, Category.CategoryName FROM Sale INNER JOIN((Category INNER JOIN Product ON Category.ID = Product.CategoryID) INNER JOIN SaleDetail ON Product.ID = SaleDetail.ProductID) ON Sale.InvoiceNo = SaleDetail.InvoiceNo WHERE(((Month([SaleDate])) = 5)) GROUP BY Day([SaleDate]), Category.CategoryName", cm);
        //    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
        //    DataSet ds = new DataSet();
        //    da.Fill(ds);
        //    sc.dataTable = ds.Tables[0];
        //    return sc;
        //}
    }
}

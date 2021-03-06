﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skynet.Classes
{
    
    class Sales
    {
        OleDbConnection cm = new OleDbConnection(Utils.ConnString);

        public string InvoiceNo()
        {
            string inv = null;
            OleDbCommand cmd = new OleDbCommand("SELECT TOP 1 InvoiceNo FROM Sale ORDER BY InvoiceNo DESC", cm);
            //OleDbCommand cmd = new OleDbCommand("SELECT MAX(InvoiceNo) FROM Sale", cm);
            try
            {
                cm.Open();
                int i = Convert.ToInt32(cmd.ExecuteScalar());
                inv = (i + 1).ToString("0000000");
            }
            catch (Exception ex) { inv = ex.Message; }
            finally { cm.Close(); }
            return inv;
        }

        public Server2Client AddSale(Sale sls)
        {
            Server2Client sc = new Server2Client();
            OleDbCommand cmd = new OleDbCommand("INSERT INTO Sale (InvoiceNo, SaleDate, CustomerID, Amount, Discount, Payment, Balance) VALUES (@INV, @SDT, @CID, @AMT, @DSC, @PMT, @BAL)", cm);
            cmd.Parameters.AddWithValue("@INV", sls.InvoiceNo);
            cmd.Parameters.AddWithValue("@SDT", sls.SaleDate);
            cmd.Parameters.AddWithValue("@CID", sls.CustomerID);
            cmd.Parameters.AddWithValue("@AMT", sls.Amount);
            cmd.Parameters.AddWithValue("@DSC", sls.Discount);
            cmd.Parameters.AddWithValue("@PMT", sls.Payment);
            cmd.Parameters.AddWithValue("@BAL", sls.Balance);
            try
            {
                cm.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { sc.Message = ex.Message; }
            finally { cm.Close(); }
            return sc;
        }

        public Server2Client AddSaleDetails(SaleDetail s)
        {
            Server2Client sc = new Server2Client();
            OleDbCommand cmd = new OleDbCommand("INSERT INTO SaleDetail (InvoiceNo, ProductID, Quantity, BuyingValue, SellingValue, Amount) VALUES (@INV, @PID, @QTY, @BVL, @SVL, @TAM)", cm);
            cmd.Parameters.AddWithValue("@INV", s.InvoiceNo);
            cmd.Parameters.AddWithValue("@PID", s.ProductID);
            cmd.Parameters.AddWithValue("@QTY", s.Quantity);
            cmd.Parameters.AddWithValue("@BVL", s.BuyingValue);
            cmd.Parameters.AddWithValue("@SVL", s.SellingValue);
            cmd.Parameters.AddWithValue("@TAM", s.Amount);
            try
            {
                cm.Open();
                sc.Count = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                sc.Message = ex.Message;
            }
            finally { cm.Close(); }
            return sc;
        }

        public Server2Client getSoldProducts(DateTime dt)
        {
            string df = dt.Date.Month.ToString("00") + "/" + dt.Date.Day.ToString("00") + "/" + dt.Date.Year.ToString();
            //string dt = dtTo.Date.Month.ToString("00") + "/" + dtTo.Date.Day.ToString("00") + "/" + dtTo.Date.Year.ToString();
            Server2Client sc = new Server2Client();
            OleDbCommand cmd = new OleDbCommand("SELECT Sale.SaleDate, Product.ProductName, IIF(Product.BarCode IS NOT NULL, 'S/N: ' + Product.BarCode, ' ') AS BarCode, Sum(SaleDetail.SellingValue) AS SumOfSellingValue, Sum(SaleDetail.Quantity) AS SumOfQuantity, Sum(SaleDetail.Amount) AS SumOfAmount FROM Sale INNER JOIN(Product INNER JOIN SaleDetail ON Product.ID = SaleDetail.ProductID) ON Sale.InvoiceNo = SaleDetail.InvoiceNo WHERE Sale.SaleDate = #" + df + "# GROUP BY Sale.SaleDate, Product.ProductName, Product.BarCode", cm);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            sc.Count = ds.Tables[0].Rows.Count;
            sc.dataTable = ds.Tables[0];
            return sc;
        }

        public Server2Client getSoldProducts(DateTime dtFR, DateTime dtTO)
        {
            string df = dtFR.Date.Month.ToString("00") + "/" + dtFR.Date.Day.ToString("00") + "/" + dtFR.Date.Year.ToString();
            string dt = dtTO.Date.Month.ToString("00") + "/" + dtTO.Date.Day.ToString("00") + "/" + dtTO.Date.Year.ToString();
            Server2Client sc = new Server2Client();
            OleDbCommand cmd = new OleDbCommand("SELECT Sale.SaleDate, Product.ProductName, IIF(Product.BarCode IS NOT NULL, 'S/N: ' + Product.BarCode, ' ') AS BarCode, Sum(SaleDetail.SellingValue) AS SumOfSellingValue, Sum(SaleDetail.Quantity) AS SumOfQuantity, Sum(SaleDetail.Amount) AS SumOfAmount FROM Sale INNER JOIN(Product INNER JOIN SaleDetail ON Product.ID = SaleDetail.ProductID) ON Sale.InvoiceNo = SaleDetail.InvoiceNo WHERE Sale.SaleDate BETWEEN #" + dtFR + "# AND #" + dtTO + "# GROUP BY Sale.SaleDate, Product.ProductName, Product.BarCode", cm);
            //OleDbCommand cmd = new OleDbCommand();
            //cmd.Connection = cm;
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.CommandText = "SoldProducts";
            //cmd.Parameters.AddWithValue("FromDate", dtFR);
            //cmd.Parameters.AddWithValue("ToDate", dtTO);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            sc.Count = ds.Tables[0].Rows.Count;
            sc.dataTable = ds.Tables[0];
            return sc;
        }

        public Server2Client getSoldProducts(string InvoiceNo)
        {
            Server2Client sc = new Server2Client();
            OleDbCommand cmd = new OleDbCommand("SELECT Customer.CustomerName, Customer.Address, Customer.Phone, Sale.InvoiceNo, Sale.SaleDate, Product.ProductName, Product.BarCode, SaleDetail.Quantity, SaleDetail.SellingValue, Sale.Amount, Sale.Discount, Sale.Payment FROM(Customer INNER JOIN Sale ON Customer.ID = Sale.CustomerID) INNER JOIN(Product INNER JOIN SaleDetail ON Product.ID = SaleDetail.ProductID) ON Sale.InvoiceNo = SaleDetail.InvoiceNo WHERE Sale.InvoiceNo='" + InvoiceNo + "'", cm);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            sc.Count = ds.Tables[0].Rows.Count;
            sc.dataTable = ds.Tables[0];
            return sc;
        }

        public Server2Client getProfitLoss(DateTime dt)
        {
            Server2Client sc = new Server2Client();
            OleDbCommand cmd = new OleDbCommand("SELECT Sale.SaleDate, SUM(Product.SellingValue*SaleDetail.Quantity) AS TotalSellingValue, SUM(Product.BuyingValue*SaleDetail.Quantity) AS TotalBuyingValue, TotalSellingValue -  TotalBuyingValue AS Profit FROM Sale INNER JOIN(Product INNER JOIN SaleDetail ON Product.ID = SaleDetail.ProductID) ON Sale.InvoiceNo = SaleDetail.InvoiceNo WHERE Sale.SaleDate=#" + dt + "# GROUP BY Sale.SaleDate", cm);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            sc.Count = ds.Tables[0].Rows.Count;
            sc.dataTable = ds.Tables[0];
            return sc;
        }

        public Server2Client getProfitLoss(DateTime dtFR, DateTime dtTO)
        {
            Server2Client sc = new Server2Client();
            OleDbCommand cmd = new OleDbCommand("SELECT Sale.SaleDate, SUM(Product.SellingValue*SaleDetail.Quantity) AS TotalSellingValue, SUM(Product.BuyingValue*SaleDetail.Quantity) AS TotalBuyingValue, TotalSellingValue -  TotalBuyingValue AS Profit FROM Sale INNER JOIN(Product INNER JOIN SaleDetail ON Product.ID = SaleDetail.ProductID) ON Sale.InvoiceNo = SaleDetail.InvoiceNo WHERE Sale.SaleDate BETWEEN #" + dtFR + "# AND #" + dtTO + "# GROUP BY Sale.SaleDate", cm);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            sc.Count = ds.Tables[0].Rows.Count;
            sc.dataTable = ds.Tables[0];
            return sc;
        }

        public Server2Client SalesToCustomer(int CustomerID)
        {
            Server2Client sc = new Server2Client();
            OleDbCommand cmd = new OleDbCommand("SELECT Customer.CustomerName, Customer.Address, Customer.Phone, Sale.SaleDate, Product.ProductName, Product.BarCode, SaleDetail.SellingValue, Sum(SaleDetail.Quantity) AS TotalQuantity, [SaleDetail].[SellingValue]*[SaleDetail].[Quantity] AS Amount FROM Product INNER JOIN((Customer INNER JOIN Sale ON Customer.ID = Sale.CustomerID) INNER JOIN SaleDetail ON Sale.InvoiceNo = SaleDetail.InvoiceNo) ON Product.ID = SaleDetail.ProductID WHERE Customer.ID=" + CustomerID + " GROUP BY Customer.CustomerName, Customer.Address, Customer.Phone, Sale.SaleDate, Product.ProductName, Product.BarCode, SaleDetail.SellingValue, [SaleDetail].[SellingValue]*[SaleDetail].[Quantity]", cm);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            sc.Count = ds.Tables[0].Rows.Count;
            sc.dataTable = ds.Tables[0];
            return sc;
        }

        public Server2Client SalesToCustomer(int CID, DateTime dtF, DateTime dtT)
        {
            string dtf = dtF.Date.Month.ToString("00") + "/" + dtF.Date.Day.ToString("00") + "/" + dtF.Date.Year.ToString();
            string dtt = dtT.Date.Month.ToString("00") + "/" + dtT.Date.Day.ToString("00") + "/" + dtT.Date.Year.ToString();
            Server2Client sc = new Server2Client();
            //OleDbCommand cmd = new OleDbCommand("SELECT Customer.CustomerName, Customer.Address, Customer.Phone, Sale.SaleDate, Product.ProductName, Product.BarCode, SaleDetail.SellingValue, Sum(SaleDetail.Quantity) AS TotalQuantity, [SaleDetail].[SellingValue]*[SaleDetail].[Quantity] AS Amount FROM Product INNER JOIN((Customer INNER JOIN Sale ON Customer.ID = Sale.CustomerID) INNER JOIN SaleDetail ON Sale.InvoiceNo = SaleDetail.InvoiceNo) ON Product.ID = SaleDetail.ProductID WHERE Customer.ID=" + CID + " AND Sale.SaleDate BETWEEN #" + dtF + "# AND #" + dtT + "# GROUP BY Customer.CustomerName, Customer.Address, Customer.Phone, Sale.SaleDate, Product.ProductName, Product.BarCode, SaleDetail.SellingValue, [SaleDetail].[SellingValue]*[SaleDetail].[Quantity]", cm);
            OleDbCommand cmd = new OleDbCommand("SELECT Customer.CustomerName, Customer.Address, Sale.SaleDate, Product.ProductName, SaleDetail.Quantity, [Sale.Amount]-[Discount] AS Amount, Sale.Payment, Sale.Balance FROM Product INNER JOIN((Customer INNER JOIN Sale ON Customer.ID = Sale.CustomerID) INNER JOIN SaleDetail ON Sale.InvoiceNo = SaleDetail.InvoiceNo) ON Product.ID = SaleDetail.ProductID WHERE Customer.ID=" + CID + " AND Sale.SaleDate BETWEEN #" + dtf + "# AND #" + dtt + "#", cm);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            sc.Count = ds.Tables[0].Rows.Count;
            sc.dataTable = ds.Tables[0];
            return sc;
        }

        //public Server2Client GetSoldByInvoiceNo(string InvoiceNo)
        //{
        //    Server2Client sc;
        //    Customer cus = new Customer();
        //    OleDbCommand cmd;
        //    cmd = new OleDbCommand("SELECT CustomerName, Address, Phone FROM Customer INNER JOIN Sale ON Customer.ID = Sale.CustomerID WHERE InvoiceNo='" + InvoiceNo + "'", cm);
        //    try
        //    {
        //        cm.Open();
        //        OleDbDataReader rd = cmd.ExecuteReader();
        //        rd.Read();
        //        cus.CustomerName = rd[0].ToString();
        //        cus.Address = rd[1].ToString();
        //        cus.Phone = rd[2].ToString();
        //    }
        //    catch
        //    {
        //        cus.CustomerName = "";
        //        cus.Address = "";
        //        cus.Phone = "";
        //    }
        //    finally { cm.Close(); }

        //    //cmd = new OleDbCommand("");

        //    cmd = new OleDbCommand("SELECT Sale.InvoiceNo, Sale.SaleDate, Product.ProductName, Product.BarCode, SaleDetail.Quantity, SaleDetail.SellingValue, Sale.Amount, Sale.Discount, Sale.Payment FROM Sale INNER JOIN(Product INNER JOIN SaleDetail ON Product.ID = SaleDetail.ProductID) ON Sale.InvoiceNo = SaleDetail.InvoiceNo WHERE Sale.InvoiceNo='" + InvoiceNo + "'", cm);
        //    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
        //    DataSet ds = new DataSet();
        //    da.Fill(ds);

        //}
    }
}

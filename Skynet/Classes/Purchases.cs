﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skynet.Classes
{
    class Purchases
    {
        OleDbConnection cm = new OleDbConnection(Utils.ConnString);
        public string InvoiceNo()
        {
            string inv = null;
            OleDbCommand cmd = new OleDbCommand("SELECT TOP 1 InvoiceNo FROM Purchase ORDER BY InvoiceNo DESC", cm);
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

        public Server2Client addPurchase(Purchase p)
        {
            Server2Client sc = new Server2Client();
            OleDbCommand cmd = new OleDbCommand("INSERT INTO Purchase (InvoiceNo, PurchaseDate, SupplierID, Amount, Payment, Balance) VALUES (@INV, @PDT, @SID, @TAM, @TPM, @TBL)", cm);
            cmd.Parameters.AddWithValue("@INV", p.InvoiceNo);
            cmd.Parameters.AddWithValue("@PDT", p.PurchaseDate);
            cmd.Parameters.AddWithValue("@SID", p.SupplierID);
            cmd.Parameters.AddWithValue("@TAM", p.Amount);
            cmd.Parameters.AddWithValue("@TPM", p.Payment);
            cmd.Parameters.AddWithValue("@TBL", p.Balance);
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

        public Server2Client addPurchaseDetails(PurchaseDetail p)
        {
            Server2Client sc = new Server2Client();
            OleDbCommand cmd = new OleDbCommand("INSERT INTO PurchaseDetail (InvoiceNo, ProductCode, Quantity, BuyingValue, SellingValue, Amount) VALUES (@INV, @PID, @QTY, @BVL, @SVL, @TAM)", cm);
            cmd.Parameters.AddWithValue("@INV", p.InvoiceNo);
            cmd.Parameters.AddWithValue("@PID", p.ProductCode);
            cmd.Parameters.AddWithValue("@QTY", p.Quantity);
            cmd.Parameters.AddWithValue("@BVL", p.BuyingValue);
            cmd.Parameters.AddWithValue("@SVL", p.SellingValue);
            cmd.Parameters.AddWithValue("@TAM", p.Amount);
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

        public Server2Client getPurchasedProducts(DateTime dt)
        {
            Server2Client sc = new Server2Client();
            OleDbCommand cmd = new OleDbCommand("SELECT Purchase.PurchaseDate, Product.ProductName, IIF(Product.BarCode IS NOT NULL, 'S/N: ' + Product.BarCode, ' ') AS BarCode, Sum(PurchaseDetail.BuyingValue) AS SumOfBuyingValue, Sum(PurchaseDetail.Quantity) AS SumOfQuantity, Sum(PurchaseDetail.Amount) AS SumOfAmount FROM Purchase INNER JOIN(Product INNER JOIN PurchaseDetail ON Product.ProductCode = PurchaseDetail.ProductCode) ON Purchase.InvoiceNo = PurchaseDetail.InvoiceNo WHERE Purchase.PurchaseDate = #" + dt + "# GROUP BY Purchase.PurchaseDate, Product.ProductName, Product.BarCode", cm);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            sc.Count = ds.Tables[0].Rows.Count;
            sc.dataTable = ds.Tables[0];
            return sc;
        }

        public Server2Client getPurchasedProducts(DateTime dtFr, DateTime dtTo)
        {
            Server2Client sc = new Server2Client();
            OleDbCommand cmd = new OleDbCommand("SELECT Purchase.PurchaseDate, Product.ProductName, IIF(Product.BarCode IS NOT NULL, 'S/N: ' + Product.BarCode, ' ') AS BarCode, Sum(PurchaseDetail.BuyingValue) AS SumOfBuyingValue, Sum(PurchaseDetail.Quantity) AS SumOfQuantity, Sum(PurchaseDetail.TotalAmount) AS SumOfAmount FROM Purchase INNER JOIN(Product INNER JOIN PurchaseDetail ON Product.ProductCode = PurchaseDetail.ProductCode) ON Purchase.InvoiceNo = PurchaseDetail.InvoiceNo WHERE Purchase.PurchaseDate BETWEEN #" + dtFr + "# AND #" + dtTo + "# GROUP BY Purchase.PurchaseDate, Product.ProductName, Product.BarCode", cm);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            sc.Count = ds.Tables[0].Rows.Count;
            sc.dataTable = ds.Tables[0];
            return sc;
        }

        public Server2Client getPurchasedProducts(string InvoiceNo)
        {
            Server2Client sc = new Server2Client();
            OleDbCommand cmd = new OleDbCommand("SELECT Purchase.PurchaseDate, Product.ProductName, IIF(Product.BarCode IS NOT NULL, 'S/N: ' + Product.BarCode, ' ') AS BarCode, Sum(PurchaseDetail.BuyingValue) AS SumOfBuyingValue, Sum(PurchaseDetail.Quantity) AS SumOfQuantity, Sum(PurchaseDetail.TotalAmount) AS SumOfAmount FROM Purchase INNER JOIN(Product INNER JOIN PurchaseDetail ON Product.ProductCode = PurchaseDetail.ProductCode) ON Purchase.InvoiceNo = PurchaseDetail.InvoiceNo WHERE Purchase.InvoiceNo = '" + InvoiceNo + "' GROUP BY Purchase.PurchaseDate, Product.ProductName, Product.BarCode", cm);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            sc.Count = ds.Tables[0].Rows.Count;
            sc.dataTable = ds.Tables[0];
            return sc;
        }

        public Server2Client PurchaseFromSupplier(int SupplierID)
        {
            Server2Client sc = new Server2Client();
            OleDbCommand cmd = new OleDbCommand("SELECT Supplier.SupplierName, Supplier.Address, Supplier.Phone, Purchase.PurchaseDate, Product.ProductName, Product.BarCode, PurchaseDetail.SellingValue, Sum(PurchaseDetail.Quantity) AS TotalQuantity, [PurchaseDetail].[SellingValue]*[PurchaseDetail].[Quantity] AS Amount FROM Product INNER JOIN((Supplier INNER JOIN Purchase ON Supplier.ID = Purchase.SupplierID) INNER JOIN PurchaseDetail ON Purchase.InvoiceNo = PurchaseDetail.InvoiceNo) ON Product.ProductCode = PurchaseDetail.ProductCode WHERE Supplier.ID=" + SupplierID + " GROUP BY Supplier.SupplierName, Supplier.Address, Supplier.Phone, Purchase.PurchaseDate, Product.ProductName, Product.BarCode, PurchaseDetail.SellingValue, [PurchaseDetail].[SellingValue]*[PurchaseDetail].[Quantity]", cm);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            sc.Count = ds.Tables[0].Rows.Count;
            sc.dataTable = ds.Tables[0];
            return sc;
        }

        public Server2Client PurchaseFromSupplier(int SupplierID, DateTime dtFr, DateTime dtTo)
        {
            Server2Client sc = new Server2Client();
            OleDbCommand cmd = new OleDbCommand("SELECT Supplier.SupplierName, Supplier.Address, Supplier.Phone, Purchase.PurchaseDate, Product.ProductName, Product.BarCode, PurchaseDetail.SellingValue, Sum(PurchaseDetail.Quantity) AS TotalQuantity, [PurchaseDetail].[SellingValue]*[PurchaseDetail].[Quantity] AS Amount FROM Product INNER JOIN((Supplier INNER JOIN Purchase ON Supplier.ID = Purchase.SupplierID) INNER JOIN PurchaseDetail ON Purchase.InvoiceNo = PurchaseDetail.InvoiceNo) ON Product.ProductCode = PurchaseDetail.ProductCode WHERE Supplier.ID=" + SupplierID + " AND Purchase.PurchaseDate BETWEEN #" + dtFr + "# AND #" + dtTo + "# GROUP BY Supplier.SupplierName, Supplier.Address, Supplier.Phone, Purchase.PurchaseDate, Product.ProductName, Product.BarCode, PurchaseDetail.SellingValue, [PurchaseDetail].[SellingValue]*[PurchaseDetail].[Quantity]", cm);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            sc.Count = ds.Tables[0].Rows.Count;
            sc.dataTable = ds.Tables[0];
            return sc;
        }
    }
}

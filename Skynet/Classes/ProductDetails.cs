using System;
using System.Data;
using System.Data.OleDb;

namespace Skynet.Classes
{
    class ProductDetails
    {
        OleDbConnection cm = new OleDbConnection(Utils.ConnString);

        //public Server2Client GetProductDetailAll()
        //{
        //    Server2Client sc = new Server2Client();
        //    OleDbCommand cmd = new OleDbCommand("SELECT ID, ProductName, BuyingValue, SellingValue, Quantity FROM ProductDetail INNER JOIN Product ON ProductDetail.ProductID=Product.ID", cm);
        //    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
        //    DataSet ds = new DataSet();
        //    da.Fill(ds);
        //    sc.Count = ds.Tables[0].Rows.Count;
        //    sc.dataTable = ds.Tables[0];
        //    return sc;
        //}

        //public Server2Client GetProductDetailTable()
        //{
        //    Server2Client sc = new Server2Client();
        //    OleDbCommand cmd = new OleDbCommand("SELECT ID, ProductID, BuyingValue, SellingValue, Quantity FROM ProductDetail", cm);
        //    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
        //    DataSet ds = new DataSet();
        //    da.Fill(ds);
        //    sc.Count = ds.Tables[0].Rows.Count;
        //    sc.dataTable = ds.Tables[0];
        //    return sc;
        //}

        //public Server2Client GetDetailsByProduct(int ProductID)
        //{
        //    Server2Client sc = new Server2Client();
        //    OleDbCommand cmd = new OleDbCommand("SELECT ID, ProductID, BuyingValue, SellingValue, Quantity FROM ProductDetail WHERE ProductID=" + ProductID, cm);
        //    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
        //    DataSet ds = new DataSet();
        //    da.Fill(ds);
        //    sc.Count = ds.Tables[0].Rows.Count;
        //    sc.dataTable = ds.Tables[0];
        //    return sc;
        //}

        //public Server2Client GetDetailsByID(int ID)
        //{
        //    Server2Client sc = new Server2Client();
        //    OleDbCommand cmd = new OleDbCommand("SELECT ID, ProductID, BuyingValue, SellingValue, Quantity FROM ProductDetail WHERE ID=" + ID, cm);
        //    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
        //    DataSet ds = new DataSet();
        //    da.Fill(ds);
        //    sc.Count = ds.Tables[0].Rows.Count;
        //    sc.dataTable = ds.Tables[0];
        //    return sc;
        //}

        //public Server2Client AddProductDetail(ProductDetail pdt)
        //{
        //    Server2Client sc = new Server2Client();
        //    OleDbCommand cmd = new OleDbCommand("INSERT INTO ProductDetail (ProductID, BuyingValue, SellingValue, Quantity) VALUES (@PID, @BVL, @SVL, @QTY)", cm);
        //    cmd.Parameters.AddWithValue("@PID", pdt.ProductID);
        //    cmd.Parameters.AddWithValue("@BVL", pdt.BuyingValue);
        //    cmd.Parameters.AddWithValue("@SVL", pdt.SellingValue);
        //    cmd.Parameters.AddWithValue("@QTY", pdt.Quantity);
        //    try
        //    {
        //        cm.Open();
        //        cmd.ExecuteNonQuery();
        //    }
        //    catch(Exception ex) { sc.Message = ex.Message; }
        //    finally { cm.Close(); }
        //    return sc;
        //}

        //public Server2Client UpdateProductDetail(ProductDetail pdt)
        //{
        //    Server2Client sc = new Server2Client();
        //    OleDbCommand cmd = new OleDbCommand("UPDATE ProductDetail SET ProductID=@PID, BuyingValue=@BVL, SellingValue=@SVL, Quantity=@QTY WHERE ID=" + pdt.ProductDetailID, cm);
        //    cmd.Parameters.AddWithValue("@PID", pdt.ProductID);
        //    cmd.Parameters.AddWithValue("@BVL", pdt.BuyingValue);
        //    cmd.Parameters.AddWithValue("@SVL", pdt.SellingValue);
        //    cmd.Parameters.AddWithValue("@QTY", pdt.Quantity);
        //    try
        //    {
        //        cm.Open();
        //        cmd.ExecuteNonQuery();
        //    }
        //    catch (Exception ex) { sc.Message = ex.Message; }
        //    finally { cm.Close(); }
        //    return sc;
        //}

        //public Server2Client DeleteProductDetail(ProductDetail pdt)
        //{
        //    Server2Client sc = new Server2Client();
        //    OleDbCommand cmd = new OleDbCommand("DELETE FROM ProductDetail WHERE ID=" + pdt.ProductDetailID, cm);
        //    try
        //    {
        //        cm.Open();
        //        cmd.ExecuteNonQuery();
        //    }
        //    catch (Exception ex) { sc.Message = ex.Message; }
        //    finally { cm.Close(); }
        //    return sc;
        //}

        //public Server2Client IncreaseQuantity(int ProductDetailID, int Value)
        //{
        //    Server2Client sc = new Server2Client();
        //    OleDbCommand cmd = new OleDbCommand("UPDATE ProductDetail SET Quantity = Quantity + " + Value + " WHERE ID=" + ProductDetailID, cm);
        //    try
        //    {
        //        cm.Open();
        //        cmd.ExecuteNonQuery();
        //    }
        //    catch (Exception ex) { sc.Message = ex.Message; }
        //    finally { cm.Close(); }
        //    return sc;
        //}

        //public Server2Client DecreaseQuantity(int ProductDetailID, int Value)
        //{
        //    Server2Client sc = new Server2Client();
        //    OleDbCommand cmd = new OleDbCommand("UPDATE ProductDetail SET Quantity = Quantity - " + Value + " WHERE ID=" + ProductDetailID, cm);
        //    try
        //    {
        //        cm.Open();
        //        cmd.ExecuteNonQuery();
        //    }
        //    catch (Exception ex) { sc.Message = ex.Message; }
        //    finally { cm.Close(); }
        //    return sc;
        //}

        public Server2Client ProductListByCategorySimplified()
        {
            Server2Client sc = new Server2Client();
            OleDbCommand cmd = new OleDbCommand("SELECT Category.CategoryName, Product.ProductName, Product.BuyingValue, Product.SellingValue, Sum(Product.Quantity) AS SumOfQuantity FROM Category INNER JOIN Product ON Category.ID = Product.CategoryID WHERE Product.Quantity > 0 GROUP BY Category.CategoryName, Product.ProductName, Product.BuyingValue, Product.SellingValue", cm);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            sc.dataTable = ds.Tables[0];

            return sc;
        }

        public Server2Client ProductListByCategoryExtended()
        {
            Server2Client sc = new Server2Client();
            OleDbCommand cmd = new OleDbCommand("SELECT Category.CategoryName, Product.ProductName, Product.BuyingValue, Product.SellingValue, Product.Quantity, Product.BarCode FROM Category INNER JOIN Product ON Category.ID = Product.CategoryID WHERE Product.Quantity > 0 GROUP BY Category.CategoryName, Product.ProductName, Product.BuyingValue, Product.SellingValue, Product.BarCode, Product.Quantity", cm);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            sc.dataTable = ds.Tables[0];

            return sc;
        }

        public Server2Client ProductListBySupplierSimple()
        {
            Server2Client sc = new Server2Client();
            OleDbCommand cmd = new OleDbCommand("SELECT Supplier.SupplierName, Product.ProductName, Product.BuyingValue, Product.SellingValue, Sum(Product.Quantity) AS SumOfQuantity FROM Supplier LEFT JOIN Product ON Supplier.ID = Product.SupplierID GROUP BY Supplier.SupplierName, Product.ProductName, Product.BuyingValue, Product.SellingValue", cm);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            sc.dataTable = ds.Tables[0];

            return sc;
        }

        public Server2Client ProductListBySupplierExtended()
        {
            Server2Client sc = new Server2Client();
            OleDbCommand cmd = new OleDbCommand("SELECT Supplier.SupplierName, Product.ProductName, Product.BuyingValue, Product.SellingValue, Quantity, Product.BarCode FROM Supplier LEFT JOIN Product ON Supplier.ID = Product.SupplierID GROUP BY Supplier.SupplierName, Product.ProductName, Product.BuyingValue, Product.SellingValue, Product.BarCode, Quantity", cm);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            sc.dataTable = ds.Tables[0];

            return sc;
        }
    }
}

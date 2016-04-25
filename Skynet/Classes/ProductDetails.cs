using System;
using System.Data;
using System.Data.OleDb;

namespace Skynet.Classes
{
    class ProductDetails
    {
        OleDbConnection cm = new OleDbConnection(Utils.ConnString);

        public Server2Client GetProductDetailAll()
        {
            Server2Client sc = new Server2Client();
            OleDbCommand cmd = new OleDbCommand("SELECT ID, ProductName, BuyingValue, SellingValue, Quantity FROM ProductDetail INNER JOIN Product ON ProductDetail.ProductID=Product.ID", cm);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            sc.Count = ds.Tables[0].Rows.Count;
            sc.dataTable = ds.Tables[0];
            return sc;
        }

        public Server2Client GetProductDetailTable()
        {
            Server2Client sc = new Server2Client();
            OleDbCommand cmd = new OleDbCommand("SELECT ID, ProductID, BuyingValue, SellingValue, Quantity FROM ProductDetail", cm);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            sc.Count = ds.Tables[0].Rows.Count;
            sc.dataTable = ds.Tables[0];
            return sc;
        }

        public Server2Client GetDetailsByProduct(int ProductID)
        {
            Server2Client sc = new Server2Client();
            OleDbCommand cmd = new OleDbCommand("SELECT ID, ProductID, BuyingValue, SellingValue, Quantity FROM ProductDetail WHERE ProductID=" + ProductID, cm);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            sc.Count = ds.Tables[0].Rows.Count;
            sc.dataTable = ds.Tables[0];
            return sc;
        }

        public Server2Client GetDetailsByID(int ID)
        {
            Server2Client sc = new Server2Client();
            OleDbCommand cmd = new OleDbCommand("SELECT ID, ProductID, BuyingValue, SellingValue, Quantity FROM ProductDetail WHERE ID=" + ID, cm);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            sc.Count = ds.Tables[0].Rows.Count;
            sc.dataTable = ds.Tables[0];
            return sc;
        }

        public Server2Client AddProductDetail(ProductDetail pdt)
        {
            Server2Client sc = new Server2Client();
            OleDbCommand cmd = new OleDbCommand("INSERT INTO ProductDetail (ProductID, BuyingValue, SellingValue, Quantity) VALUES (@PID, @BVL, @SVL, @QTY)", cm);
            cmd.Parameters.AddWithValue("@PID", pdt.ProductID);
            cmd.Parameters.AddWithValue("@BVL", pdt.BuyingValue);
            cmd.Parameters.AddWithValue("@SVL", pdt.SellingValue);
            cmd.Parameters.AddWithValue("@QTY", pdt.Quantity);
            try
            {
                cm.Open();
                cmd.ExecuteNonQuery();
            }
            catch(Exception ex) { sc.Message = ex.Message; }
            finally { cm.Close(); }
            return sc;
        }

        public Server2Client UpdateProductDetail(ProductDetail pdt)
        {
            Server2Client sc = new Server2Client();
            OleDbCommand cmd = new OleDbCommand("UPDATE ProductDetail SET ProductID=@PID, BuyingValue=@BVL, SellingValue=@SVL, Quantity=@QTY WHERE ID=" + pdt.ProductDetailID, cm);
            cmd.Parameters.AddWithValue("@PID", pdt.ProductID);
            cmd.Parameters.AddWithValue("@BVL", pdt.BuyingValue);
            cmd.Parameters.AddWithValue("@SVL", pdt.SellingValue);
            cmd.Parameters.AddWithValue("@QTY", pdt.Quantity);
            try
            {
                cm.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { sc.Message = ex.Message; }
            finally { cm.Close(); }
            return sc;
        }

        public Server2Client DeleteProductDetail(ProductDetail pdt)
        {
            Server2Client sc = new Server2Client();
            OleDbCommand cmd = new OleDbCommand("DELETE FROM ProductDetail WHERE ID=" + pdt.ProductDetailID, cm);
            try
            {
                cm.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { sc.Message = ex.Message; }
            finally { cm.Close(); }
            return sc;
        }

        public Server2Client IncreaseQuantity(int ProductDetailID, int Value)
        {
            Server2Client sc = new Server2Client();
            OleDbCommand cmd = new OleDbCommand("UPDATE ProductDetail SET Quantity = Quantity + " + Value + " WHERE ID=" + ProductDetailID, cm);
            try
            {
                cm.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { sc.Message = ex.Message; }
            finally { cm.Close(); }
            return sc;
        }

        public Server2Client DecreaseQuantity(int ProductDetailID, int Value)
        {
            Server2Client sc = new Server2Client();
            OleDbCommand cmd = new OleDbCommand("UPDATE ProductDetail SET Quantity = Quantity - " + Value + " WHERE ID=" + ProductDetailID, cm);
            try
            {
                cm.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { sc.Message = ex.Message; }
            finally { cm.Close(); }
            return sc;
        }
    }
}

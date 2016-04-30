using System;
using System.Data;
using System.Data.OleDb;

namespace Skynet.Classes
{
    class Products
    {
        OleDbConnection cm = new OleDbConnection(Utils.ConnString);

        public Server2Client GetProducts()
        {
            Server2Client sc = new Server2Client();
            OleDbCommand cmd = new OleDbCommand("SELECT ID, ProductName, BuyingValue, SellingValue, SUM(Quantity) AS Quantity FROM Product GROUP BY ID, ProductName, BuyingValue, SellingValue", cm);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            sc.Count = ds.Tables[0].Rows.Count;
            sc.dataTable = ds.Tables[0];
            return sc;
        }

        public Server2Client GetProductFull()
        {
            Server2Client sc = new Server2Client();
            OleDbCommand cmd = new OleDbCommand("SELECT Product.ID, Category.CategoryName, Product.ProductName, ProductDetail.BuyingValue, ProductDetail.SellingValue, ProductDetail.Quantity, Product.BarCode, Product.ReorderLevel FROM(Category INNER JOIN Product ON Category.ID = Product.CategoryID) INNER JOIN ProductDetail ON Product.ID = ProductDetail.ProductID", cm);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            sc.Count = ds.Tables[0].Rows.Count;
            sc.dataTable = ds.Tables[0];
            return sc;
        }

        public Server2Client GetProductsTable()
        {
            Server2Client sc = new Server2Client();
            OleDbCommand cmd = new OleDbCommand("SELECT ID, CategoryID, ProductName, BarCode FROM Product", cm);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            sc.Count = ds.Tables[0].Rows.Count;
            sc.dataTable = ds.Tables[0];
            return sc;
        }

        public Server2Client GetProductValues()
        {
            Server2Client sc = new Server2Client();
            OleDbCommand cmd = new OleDbCommand("SELECT CategoryName, ProductName, BuyingValue, SellingValue, SUM(Quantity) AS TotalQuantity FROM Category INNER JOIN Product ON Category.ID = Product.CategoryID GROUP BY CategoryName, ProductName, BuyingValue, SellingValue", cm);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            sc.Count = ds.Tables[0].Rows.Count;
            sc.dataTable = ds.Tables[0];
            return sc;
        }

        public Server2Client GetProductByCategory(int CategoryID)
        {
            Server2Client sc = new Server2Client();
            OleDbCommand cmd = new OleDbCommand("SELECT ID, ProductName, BuyingValue, SellingValue, SUM(Quantity) AS Quantity FROM Product WHERE CategoryID=" + CategoryID + " GROUP BY ID, ProductName, BuyingValue, SellingValue", cm);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            sc.Count = ds.Tables[0].Rows.Count;
            sc.dataTable = ds.Tables[0];
            return sc;
        }

        public Product GetProductByID(int ProductID)
        {
            Product prd = new Product();
            OleDbCommand cmd = new OleDbCommand("SELECT ID, CategoryID, ProductName, BuyingValue, SellingValue, Quantity, BarCode FROM Product WHERE ID=" + ProductID, cm);
            try
            {
                cm.Open();
                OleDbDataReader rd = cmd.ExecuteReader();
                rd.Read();
                prd.ProductID = Convert.ToInt32(rd[0]);
                prd.CategoryID = Convert.ToInt32(rd[1]);
                prd.ProductName = rd[2].ToString();
                prd.BuyingValue = Convert.ToDouble(rd[3]);
                prd.SellingValue = Convert.ToDouble(rd[4]);
                prd.Quantity = Convert.ToInt32(rd[5]);
                prd.BarCode = rd[6].ToString();
            }
            catch (Exception ex) { throw ex; }
            finally { cm.Close(); }
            return prd;
        }

        public Product GetProductByBarCode(string BarCode)
        {
            Product prd = new Product();
            prd.Message = null;
            OleDbCommand cmd = new OleDbCommand("SELECT TOP 1 ID, CategoryID, ProductName, BuyingValue, SellingValue, BarCode FROM Product WHERE BarCode='" + BarCode + "' ORDER BY ID", cm);
            try
            {
                cm.Open();
                OleDbDataReader rd = cmd.ExecuteReader();
                rd.Read();
                prd.ProductID = Convert.ToInt32(rd[0]);
                prd.CategoryID = Convert.ToInt32(rd[1]);
                prd.ProductName = rd[2].ToString();
                prd.BuyingValue = Convert.ToDouble(rd[3]);
                prd.SellingValue = Convert.ToDouble(rd[4]);
                //prd.Quantity = Convert.ToInt32(rd[5]);
                prd.BarCode = rd[5].ToString();
            }
            catch(Exception ex) { prd.Message = ex.Message; }
            finally { cm.Close(); }
            return prd;
        }

        public Server2Client AddProduct(Product prd)
        {
            Server2Client sc = new Server2Client();
            sc.Message = null;
            OleDbCommand cmd = new OleDbCommand("INSERT INTO Product (CategoryID, ProductName, BuyingValue, SellingValue, Quantity, BarCode) VALUES (@CID, @PNM, @BVL, @SVL, @QTY, @BCD)", cm);
            cmd.Parameters.AddWithValue("@CID", prd.CategoryID);
            cmd.Parameters.AddWithValue("@PNM", prd.ProductName);
            cmd.Parameters.AddWithValue("@BVL", prd.BuyingValue);
            cmd.Parameters.AddWithValue("@SVL", prd.SellingValue);
            cmd.Parameters.AddWithValue("@BVL", prd.Quantity);
            cmd.Parameters.AddWithValue("@BCD", prd.BarCode);
            try
            {
                cm.Open();
                cmd.ExecuteNonQuery();
            }
            catch(Exception ex) { sc.Message = ex.Message; }
            finally { cm.Close(); }
            return sc;
        }

        public Server2Client updateProduct(Product prd)
        {
            Server2Client sc = new Server2Client();
            OleDbCommand cmd = new OleDbCommand("UPDATE Product SET CategoryID=@CID, ProductName=@PNM, BuyingValue=@BVL, SellingValue=@SVL, Quantity=@QTY, BarCode=@BCD WHERE ID=" + prd.ProductID, cm);
            cmd.Parameters.AddWithValue("@CID", prd.CategoryID);
            cmd.Parameters.AddWithValue("@PNM", prd.ProductName);
            cmd.Parameters.AddWithValue("@BVL", prd.BuyingValue);
            cmd.Parameters.AddWithValue("@SVL", prd.SellingValue);
            cmd.Parameters.AddWithValue("@BVL", prd.Quantity);
            cmd.Parameters.AddWithValue("@BCD", prd.BarCode);
            try
            {
                cm.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { sc.Message = ex.Message; }
            finally { cm.Close(); }
            return sc;
        }

        public Server2Client deleteProduct(int ID)
        {
            Server2Client sc = new Server2Client();
            OleDbCommand cmd = new OleDbCommand("DELETE FROM Product WHERE ID=" + ID, cm);
            try
            {
                cm.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { sc.Message = ex.Message; }
            finally { cm.Close(); }
            return sc;
        }

        public int GetLastInsertedID()
        {
            int i = 0;
            OleDbCommand cmd = new OleDbCommand("SELECT TOP 1 ID FROM Product ORDER BY ID DESC", cm);
            try
            {
                cm.Open();
                i = (int)cmd.ExecuteScalar();
            }
            catch { i = -1; }
            finally { cm.Close(); }
            return i;
        }

        public Server2Client updateQuantity(int ID, int Value, string type)
        {
            Server2Client sc = new Server2Client();
            OleDbCommand cmd = new OleDbCommand("UPDATE Product SET Quantity=Quantity" + type + "" + Value + " WHERE ID=" + ID, cm);

            try
            {
                cm.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                sc.Message = ex.Message;
            }
            finally { cm.Close(); }
            return sc;
        }
    }
}

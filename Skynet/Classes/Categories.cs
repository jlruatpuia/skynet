using System;
using System.Data;
using System.Data.OleDb;

namespace Skynet.Classes
{
    class Categories
    {
        OleDbConnection cm = new OleDbConnection(Utils.ConnString);

        public Server2Client GetCategories()
        {
            Server2Client sc = new Server2Client();
            OleDbCommand cmd = new OleDbCommand("SELECT ID, CategoryName FROM Category ORDER BY CategoryName", cm);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            sc.Count = ds.Tables[0].Rows.Count;
            sc.dataSet = ds;
            sc.dataTable = ds.Tables[0];
            return sc;
        }

        public Category GetCategory(int CategoryID)
        {
            Category cat = new Category();
            OleDbCommand cmd = new OleDbCommand("SELECT ID, CategoryName FROM Category WHERE ID=" + CategoryID, cm);
            try
            {
                cm.Open();
                OleDbDataReader rd = cmd.ExecuteReader();
                rd.Read();
                cat.CategoryID = Convert.ToInt32(rd[0]);
                cat.CategoryName = rd[1].ToString();
            }
            catch { throw; }
            finally { cm.Close(); }
            return cat;
        }

        public Server2Client AddCategory(Category cat)
        {
            Server2Client sc = new Server2Client();
            sc.Message = null;
            OleDbCommand cmd = new OleDbCommand("INSERT INTO Category (CategoryName) VALUES (@CNM)", cm);
            cmd.Parameters.AddWithValue("@CNM", cat.CategoryName);
            try
            {
                cm.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { sc.Message = ex.Message; }
            finally { cm.Close(); }
            return sc;
        }

        public Server2Client UpdateCategory(Category cat)
        {
            Server2Client sc = new Server2Client();
            sc.Message = null;
            OleDbCommand cmd = new OleDbCommand("UPDATE Category SET CategoryName=@CNM WHERE ID=" + cat.CategoryID, cm);
            cmd.Parameters.AddWithValue("@CNM", cat.CategoryName);
            try
            {
                cm.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { sc.Message = ex.Message; }
            finally { cm.Close(); }
            return sc;
        }
        public Server2Client DeleteCategory(Category cat)
        {
            Server2Client sc = new Server2Client();
            sc.Message = null;
            OleDbCommand cmd = new OleDbCommand("DELETE FROM Category WHERE ID=" + cat.CategoryID, cm);
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

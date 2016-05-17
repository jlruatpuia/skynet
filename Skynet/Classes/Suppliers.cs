using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skynet.Classes
{
    class Suppliers
    {
        OleDbConnection cm = new OleDbConnection(Utils.ConnString);

        Server2Client sc;
        Supplier sup;
        public Server2Client getSuppliers()
        {
            sc = new Server2Client();
            OleDbCommand cmd = new OleDbCommand("SELECT ID, SupplierName, Address, Phone, Email, Balance FROM Supplier", cm);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            sc.Count = ds.Tables[0].Rows.Count;
            sc.dataTable = ds.Tables[0];
            return sc;
        }
        public Server2Client getMaxID()
        {
            sc = new Server2Client();
            OleDbCommand cmd = new OleDbCommand("SELECT MAX(ID) FROM Supplier", cm);
            try
            {
                cm.Open();
                sc.Count = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                sc.Message = ex.Message;
            }
            finally { cm.Close(); }
            return sc;
        }

        public Supplier getSupplier(int ID)
        {
            sup = new Supplier();
            OleDbCommand cmd = new OleDbCommand("SELECT ID, SupplierName, Address, Phone, Email FROM Supplier WHERE ID=" + ID, cm);
            try
            {
                cm.Open();
                OleDbDataReader rd = cmd.ExecuteReader();
                rd.Read();
                sup.SupplierName = rd[1].ToString();
                sup.Address = rd[2].ToString();
                sup.Phone = rd[3].ToString();
                sup.Email = rd[4].ToString();
                //sup.Balance = Convert.ToDouble(rd[5]);
            }
            catch
            {
                ;
            }
            finally { cm.Close(); }
            return sup;
        }

        public Server2Client addSupplier(Supplier sup)
        {
            sc = new Server2Client();
            OleDbCommand cmd = new OleDbCommand("INSERT INTO Supplier (SupplierName, Address, Phone, Email, Balance) VALUES (@SNM, @ADR, @PHN, @EML, @BAL)", cm);
            cmd.Parameters.AddWithValue("@SNM", sup.SupplierName);
            cmd.Parameters.AddWithValue("@ADR", sup.Address);
            cmd.Parameters.AddWithValue("@PHN", sup.Phone);
            cmd.Parameters.AddWithValue("@EML", sup.Email);
            cmd.Parameters.AddWithValue("@BAL", sup.Balance);
            try
            {
                cm.Open();
                cmd.ExecuteNonQuery();
                //sc.Value = cmd.las
            }
            catch (Exception ex)
            {
                sc.Message = ex.Message;
            }
            finally { cm.Close(); }
            return sc;
        }

        public Server2Client updateSupplier(Supplier sup)
        {
            sc = new Server2Client();
            OleDbCommand cmd = new OleDbCommand("UPDATE Supplier SET SupplierName=@SNM, Address=@ADR, Phone=@PHN, Email=@EML WHERE ID=" + sup.SupplierID, cm);
            cmd.Parameters.AddWithValue("@SNM", sup.SupplierName);
            cmd.Parameters.AddWithValue("@ADR", sup.Address);
            cmd.Parameters.AddWithValue("@PHN", sup.Phone);
            cmd.Parameters.AddWithValue("@EML", sup.Email);
            //cmd.Parameters.AddWithValue("@BAL", sup.Balance);
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

        public Server2Client deleteSupplier(int ID)
        {
            sc = new Server2Client();
            OleDbCommand cmd = new OleDbCommand("DELETE FROM Supplier WHERE ID=" + ID, cm);
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

        public Server2Client Supplier_Account()
        {
            sc = new Server2Client();
            OleDbCommand cmd;
            OleDbDataAdapter da;
            DataSet ds = new DataSet();
            cmd = new OleDbCommand("SELECT ID, SupplierName, Address, Phone, Email FROM Supplier", cm);
            da = new OleDbDataAdapter(cmd);
            da.Fill(ds, "Supplier");

            cmd = new OleDbCommand("SELECT ID, SupplierID, TransDate, Description, Debit, Credit, Balance FROM SupplierAccount", cm);
            da = new OleDbDataAdapter(cmd);
            da.Fill(ds, "SupplierAccount");
            DataColumn pk = ds.Tables[0].Columns[0];
            DataColumn fk = ds.Tables[1].Columns[1];
            sc.Message = "pk_fk";
            ds.Relations.Add(sc.Message, pk, fk);
            sc.Count = ds.Tables[0].Rows.Count;
            sc.dataSet = ds;
            return sc;
        }
    }
}

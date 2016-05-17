using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skynet.Classes
{
    class SupplierAccounts
    {
        OleDbConnection cm = new OleDbConnection(Utils.ConnString);

        Server2Client sc;

        public Server2Client addTrans(SupplierAccount ca)
        {
            sc = new Server2Client();
            OleDbCommand cmd = new OleDbCommand("INSERT INTO SupplierAccount (SupplierID, TransDate, Description, Debit, Credit, Balance) VALUES (@CID, @TDT, @DSC, @CDR, @CCR, @BAL)", cm);
            cmd.Parameters.AddWithValue("@CID", ca.SupplierID);
            cmd.Parameters.AddWithValue("@TDT", ca.TransDate);
            cmd.Parameters.AddWithValue("@DSC", ca.Description);
            cmd.Parameters.AddWithValue("@CDR", ca.Debit);
            cmd.Parameters.AddWithValue("@CCR", ca.Credit);
            cmd.Parameters.AddWithValue("@BAL", ca.Balance);
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

        public Server2Client getSupplierBalance(int SupplierID)
        {
            sc = new Server2Client();
            OleDbCommand cmd = new OleDbCommand("SELECT SUM(Debit) - SUM(Credit) FROM SupplierAccount WHERE SupplierID=" + SupplierID, cm);
            try
            {
                cm.Open();
                sc.Value = Convert.ToDouble(cmd.ExecuteScalar());
            }
            catch
            {
                sc.Message = "0";
            }
            finally { cm.Close(); }
            return sc;
        }

        public Server2Client getSupplierOpeningBalance(int SupplierID)
        {
            sc = new Server2Client();
            OleDbCommand cmd = new OleDbCommand("SELECT Balance FROM SupplierAccount WHERE Description='Opening Balance' AND SupplierID=" + SupplierID, cm);
            try
            {
                cm.Open();
                sc.Value = Convert.ToDouble(cmd.ExecuteScalar());
            }
            catch
            {
                sc.Message = "0";
            }
            finally { cm.Close(); }
            return sc;
        }

        public Server2Client getTransactionDetails(int SupplierID)
        {
            sc = new Server2Client();
            OleDbCommand cmd = new OleDbCommand("SELECT Supplier.SupplierName, Supplier.Address, Supplier.Phone, Supplier.Email, SupplierAccount.TransDate, SupplierAccount.Description, iif([SupplierAccount.Debit] = 0, Null, [SupplierAccount.Debit]) AS Debit, iif([SupplierAccount.Credit] = 0, Null, [SupplierAccount.Credit]) AS Credit, SupplierAccount.Balance FROM Supplier INNER JOIN SupplierAccount ON Supplier.ID = SupplierAccount.SupplierID WHERE SupplierID=" + SupplierID, cm);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            sc.dataTable = ds.Tables[0];
            sc.Count = ds.Tables[0].Rows.Count;
            return sc;
        }
    }
}

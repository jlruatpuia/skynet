using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skynet.Classes
{
    class CustomerAccounts
    {
        OleDbConnection cm = new OleDbConnection(Utils.ConnString);

        Server2Client sc;

        public Server2Client addTrans(CustomerAccount ca)
        {
            sc = new Server2Client();
            OleDbCommand cmd = new OleDbCommand("INSERT INTO CustomerAccount (CustomerID, TransDate, Description, Debit, Credit, Balance) VALUES (@CID, @TDT, @DSC, @CDR, @CCR, @BAL)", cm);
            cmd.Parameters.AddWithValue("@CID", ca.CustomerID);
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

        public Server2Client getCustomerBalance(int CustomerID)
        {
            Server2Client sc = new Server2Client();
            OleDbCommand cmd = new OleDbCommand("SELECT SUM(Debit) - SUM(Credit) FROM CustomerAccount WHERE CustomerID=" + CustomerID, cm);
            try
            {
                cm.Open();
                sc.Value = Convert.ToDouble(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                sc.Message = ex.Message;
            }
            finally { cm.Close(); }
            return sc;
        }

        public Server2Client getTransactionDetails(int CustomerID)
        {
            sc = new Server2Client();
            OleDbCommand cmd = new OleDbCommand("SELECT Customer.CustomerName, Customer.Address, Customer.Phone, Customer.Email, CustomerAccount.TransDate, CustomerAccount.Description, iif([CustomerAccount.Debit] = 0, Null, [CustomerAccount.Debit]) AS Debit, iif([CustomerAccount.Credit] = 0, Null, [CustomerAccount.Credit]) AS Credit, CustomerAccount.Balance FROM Customer INNER JOIN CustomerAccount ON Customer.ID = CustomerAccount.CustomerID WHERE CustomerID=" + CustomerID, cm);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            sc.dataTable = ds.Tables[0];
            sc.Count = ds.Tables[0].Rows.Count;
            return sc;
        }
    }
}

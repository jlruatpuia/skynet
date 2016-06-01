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

        //public Server2Client getTransactionDetails(int SupplierID)
        //{
        //    sc = new Server2Client();
        //    OleDbCommand cmd = new OleDbCommand("SELECT Supplier.SupplierName, Supplier.Address, Supplier.Phone, Supplier.Email, SupplierAccount.TransDate, SupplierAccount.Description, iif([SupplierAccount.Debit] = 0, Null, [SupplierAccount.Debit]) AS Debit, iif([SupplierAccount.Credit] = 0, Null, [SupplierAccount.Credit]) AS Credit, SupplierAccount.Balance FROM Supplier INNER JOIN SupplierAccount ON Supplier.ID = SupplierAccount.SupplierID WHERE SupplierID=" + SupplierID, cm);
        //    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
        //    DataSet ds = new DataSet();
        //    da.Fill(ds);
        //    sc.dataTable = ds.Tables[0];
        //    sc.Count = ds.Tables[0].Rows.Count;
        //    return sc;
        //}

        public Server2Client getSupplierBalance(int SupplierID, DateTime dtFr, DateTime dtTo)
        {
            Server2Client sc = new Server2Client();
            string df = dtFr.Date.Month.ToString("00") + "/" + dtFr.Date.Day.ToString("00") + "/" + dtFr.Date.Year.ToString();
            string dt = dtTo.Date.Month.ToString("00") + "/" + dtTo.Date.Day.ToString("00") + "/" + dtTo.Date.Year.ToString();
            OleDbCommand cmd;
            cmd = new OleDbCommand("SELECT Sum(Debit)-Sum(Credit) AS OpeningBalance FROM SupplierAccount WHERE SupplierID=" + SupplierID + " AND TransDate < #" + df + "#", cm);
            double OpeningBalance = 0;
            try
            {
                cm.Open();
                OpeningBalance = Convert.ToDouble(cmd.ExecuteScalar());
            }
            catch
            {
                OpeningBalance = 0;
            }
            finally
            {
                cm.Close();
            }

            cmd = new OleDbCommand("SELECT SUM(Debit) - SUM(Credit) FROM SupplierAccount WHERE SupplierID=" + SupplierID + " AND TransDate BETWEEN #" + df + "# AND #" + dt + "#", cm);
            try
            {
                cm.Open();
                sc.Value = Convert.ToDouble(cmd.ExecuteScalar());
                sc.Value = sc.Value + OpeningBalance;
            }
            catch (Exception ex)
            {
                sc.Message = ex.Message;
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

        public Server2Client AccountStatement(int SupplierID, DateTime dtFr, DateTime dtTo)
        {
            sc = new Server2Client();

            DataTable dt = new DataTable();
            dt.Columns.Add("TransDate", typeof(DateTime));
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("Debit", typeof(double));
            dt.Columns.Add("Credit", typeof(double));
            dt.Columns.Add("Balance", typeof(double));
            string dtf = dtFr.Date.Month.ToString("00") + "/" + dtFr.Date.Day.ToString("00") + "/" + dtFr.Date.Year.ToString();
            string dtt = dtTo.Date.Month.ToString("00") + "/" + dtTo.Date.Day.ToString("00") + "/" + dtTo.Date.Year.ToString();
            OleDbCommand cmd = new OleDbCommand("SELECT Sum(Debit)-Sum(Credit) AS OpeningBalance FROM SupplierAccount WHERE SupplierID=" + SupplierID + " AND TransDate < #" + dtf + "#", cm);
            double OpeningBalance = 0;
            try
            {
                cm.Open();
                OpeningBalance = Convert.ToDouble(cmd.ExecuteScalar());
            }
            catch
            {
                OpeningBalance = 0;
            }
            finally
            {
                cm.Close();
            }

            dt.Rows.Add(dtFr, "Opening Balance", OpeningBalance, 0, OpeningBalance);

            cmd = new OleDbCommand("SELECT TransDate, Description, IIf(Debit=0,'',Debit) AS Dr, IIf(Credit=0,'',Credit) AS Cr, Balance FROM SupplierAccount WHERE SupplierID=" + SupplierID + " AND SupplierAccount.TransDate BETWEEN #" + dtf + "# AND #" + dtt + "#", cm);

            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            {
                DateTime dd = DateTime.Parse(ds.Tables[0].Rows[i].ItemArray[0].ToString());
                string desc = ds.Tables[0].Rows[i].ItemArray[1].ToString();
                double debit, credit, balance;
                try
                {
                    debit = Convert.ToDouble(ds.Tables[0].Rows[i].ItemArray[2]);
                }
                catch { debit = 0; }
                try
                {
                    credit = Convert.ToDouble(ds.Tables[0].Rows[i].ItemArray[3]);
                }
                catch { credit = 0; }
                try
                {
                    balance = Convert.ToDouble(ds.Tables[0].Rows[i].ItemArray[4]);
                }
                catch { balance = 0; }
                dt.Rows.Add(dd, desc, debit, credit, balance);
            }

            sc.dataTable = dt;
            sc.Count = dt.Rows.Count;
            return sc;
        }
    }
}

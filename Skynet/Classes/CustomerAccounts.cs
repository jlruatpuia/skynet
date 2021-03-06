﻿using DevExpress.XtraEditors;
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

        public Server2Client getCustomerBalance(int CustomerID, DateTime dtFr, DateTime dtTo)
        {
            Server2Client sc = new Server2Client();
            string df = dtFr.Date.Month.ToString("00") + "/" + dtFr.Date.Day.ToString("00") + "/" + dtFr.Date.Year.ToString();
            string dt = dtTo.Date.Month.ToString("00") + "/" + dtTo.Date.Day.ToString("00") + "/" + dtTo.Date.Year.ToString();
            OleDbCommand cmd;
            cmd = new OleDbCommand("SELECT Sum(Debit)-Sum(Credit) AS OpeningBalance FROM CustomerAccount WHERE CustomerID=" + CustomerID + " AND TransDate < #" + df + "#", cm);
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

            cmd = new OleDbCommand("SELECT SUM(Debit) - SUM(Credit) FROM CustomerAccount WHERE CustomerID=" + CustomerID + " AND TransDate BETWEEN #" + df + "# AND #" + dt + "#", cm);
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

        public Server2Client AccountStatement(int CustomerID, DateTime dtFr, DateTime dtTo)
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
            OleDbCommand cmd = new OleDbCommand("SELECT Sum(Debit)-Sum(Credit) AS OpeningBalance FROM CustomerAccount WHERE CustomerID=" + CustomerID + " AND TransDate < #" + dtf + "#", cm);
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

            cmd = new OleDbCommand("SELECT TransDate, Description, IIf(Debit=0,'',Debit) AS Dr, IIf(Credit=0,'',Credit) AS Cr, Balance FROM CustomerAccount WHERE CustomerID=" + CustomerID + " AND CustomerAccount.TransDate BETWEEN #" + dtf + "# AND #" + dtt + "#", cm);

            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            for(int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
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

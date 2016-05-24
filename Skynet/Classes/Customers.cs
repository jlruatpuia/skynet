using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skynet.Classes
{
    class Customers
    {
        OleDbConnection cm = new OleDbConnection(Utils.ConnString);
        public Server2Client getCustomers()
        {
            Server2Client sc = new Server2Client();
            OleDbCommand cmd = new OleDbCommand("SELECT ID, CustomerName, Address, Phone, Email FROM Customer", cm);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            sc.Count = ds.Tables[0].Rows.Count;
            sc.dataTable = ds.Tables[0];
            return sc;
        }

        Server2Client sc;
        Customer cus;

        public Server2Client getCustomersFull()
        {
            sc = new Server2Client();
            OleDbCommand cmd = new OleDbCommand("SELECT Customer.ID, Customer.CustomerName, Customer.Address, Customer.Phone, Customer.Email, SUM(CustomerAccount.Debit) - SUM(CustomerAccount.Credit) AS Balance FROM Customer LEFT OUTER JOIN CustomerAccount ON Customer.ID=CustomerAccount.CustomerID GROUP BY Customer.ID, Customer.CustomerName, Customer.Address, Customer.Phone, Customer.Email", cm);
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
            OleDbCommand cmd = new OleDbCommand("SELECT MAX(ID) FROM Customer", cm);
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

        public Customer getCustomer(int ID)
        {
            cus = new Customer();
            OleDbCommand cmd = new OleDbCommand("SELECT ID, CustomerName, Address, Phone, Email FROM Customer WHERE ID=" + ID, cm);
            try
            {
                cm.Open();
                OleDbDataReader rd = cmd.ExecuteReader();
                rd.Read();
                cus.CustomerID = ID;
                cus.CustomerName = rd[1].ToString();
                cus.Address = rd[2].ToString();
                cus.Phone = rd[3].ToString();
                cus.Email = rd[4].ToString();
                //cus.Balance = Convert.ToDouble(rd[5]);
            }
            catch
            {
                ;
            }
            finally { cm.Close(); }
            return cus;
        }

        public Server2Client addCustomer(Customer cus)
        {
            sc = new Server2Client();
            OleDbCommand cmd = new OleDbCommand("INSERT INTO Customer (CustomerName, Address, Phone, Email) VALUES (@CNM, @ADR, @PHN, @EML)", cm);
            cmd.Parameters.AddWithValue("@CNM", cus.CustomerName);
            cmd.Parameters.AddWithValue("@ADR", cus.Address);
            cmd.Parameters.AddWithValue("@PHN", cus.Phone);
            cmd.Parameters.AddWithValue("@EML", cus.Email);
            //cmd.Parameters.AddWithValue("@BAL", cus.Balance);
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

        public Server2Client updateCustomer(Customer cus)
        {
            sc = new Server2Client();
            OleDbCommand cmd = new OleDbCommand("UPDATE Customer SET CustomerName=@CNM, Address=@ADR, Phone=@PHN, Email=@EML WHERE ID=" + cus.CustomerID, cm);
            cmd.Parameters.AddWithValue("@CNM", cus.CustomerName);
            cmd.Parameters.AddWithValue("@ADR", cus.Address);
            cmd.Parameters.AddWithValue("@PHN", cus.Phone);
            cmd.Parameters.AddWithValue("@EML", cus.Email);
            //cmd.Parameters.AddWithValue("@BAL", cus.Balance);
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

        public Server2Client deleteCustomer(int ID)
        {
            sc = new Server2Client();
            OleDbCommand cmd = new OleDbCommand("DELETE FROM Customer WHERE ID=" + ID, cm);
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

        public Server2Client Customer_Account()
        {
            sc = new Server2Client();
            OleDbCommand cmd;
            OleDbDataAdapter da;
            DataSet ds = new DataSet();
            cmd = new OleDbCommand("SELECT ID, CustomerName, Address, Phone, Email FROM Customer", cm);
            da = new OleDbDataAdapter(cmd);
            da.Fill(ds, "Customer");

            cmd = new OleDbCommand("SELECT ID, CustomerID, TransDate, Description, Debit, Credit, Balance FROM CustomerAccount", cm);
            da = new OleDbDataAdapter(cmd);
            da.Fill(ds, "CustomerAccount");
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

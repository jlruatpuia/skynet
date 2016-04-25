using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skynet.Classes
{
    
    class Sales
    {
        OleDbConnection cm = new OleDbConnection(Utils.ConnString);

        public string InvoiceNo()
        {
            string inv = null;
            OleDbCommand cmd = new OleDbCommand("SELECT TOP 1 InvoiceNo FROM Sale ORDER BY InvoiceNo DESC", cm);
            try
            {
                cm.Open();
                int i = Convert.ToInt32(cmd.ExecuteScalar());
                inv = (i + 1).ToString("0000000");
            }
            catch (Exception ex) { inv = ex.Message; }
            finally { cm.Close(); }
            return inv;
        }

        public Server2Client AddSale(Sale sls)
        {
            Server2Client sc = new Server2Client();
            OleDbCommand cmd = new OleDbCommand("INSERT INTO Sale (InvoiceNo, SaleDate, CustomerID, Amount, Discount, Payment, Balance) VALUES (@INV, @SDT, @CID, @AMT, @DSC, @PMT, @BAL)", cm);
            cmd.Parameters.AddWithValue("@INV", sls.InvoiceNo);
            cmd.Parameters.AddWithValue("@SDT", sls.SaleDate);
            cmd.Parameters.AddWithValue("@CID", sls.CustomerID);
            cmd.Parameters.AddWithValue("@AMT", sls.Amount);
            cmd.Parameters.AddWithValue("@DSC", sls.Discount);
            cmd.Parameters.AddWithValue("@PMT", sls.Payment);
            cmd.Parameters.AddWithValue("@BAL", sls.Balance);
            try
            {
                cm.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { sc.Message = ex.Message; }
            finally { cm.Close(); }
            return sc;
        }

        public Server2Client AddSaleDetails(SaleDetail s)
        {
            Server2Client sc = new Server2Client();
            OleDbCommand cmd = new OleDbCommand("INSERT INTO SaleDetail (InvoiceNo, ProductID, Quantity, BuyingValue, SellingValue, Amount) VALUES (@INV, @PID, @QTY, @BVL, @SVL, @TAM)", cm);
            cmd.Parameters.AddWithValue("@INV", s.InvoiceNo);
            cmd.Parameters.AddWithValue("@PID", s.ProductID);
            cmd.Parameters.AddWithValue("@QTY", s.Quantity);
            cmd.Parameters.AddWithValue("@BVL", s.BuyingValue);
            cmd.Parameters.AddWithValue("@SVL", s.SellingValue);
            cmd.Parameters.AddWithValue("@TAM", s.Amount);
            try
            {
                cm.Open();
                sc.Count = cmd.ExecuteNonQuery();
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

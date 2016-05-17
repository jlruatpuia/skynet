using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skynet.Classes
{
    class Purchases
    {
        OleDbConnection cm = new OleDbConnection(Utils.ConnString);
        public string InvoiceNo()
        {
            string inv = null;
            OleDbCommand cmd = new OleDbCommand("SELECT TOP 1 InvoiceNo FROM Purchase ORDER BY InvoiceNo DESC", cm);
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

        public Server2Client addPurchase(Purchase p)
        {
            Server2Client sc = new Server2Client();
            OleDbCommand cmd = new OleDbCommand("INSERT INTO Purchase (InvoiceNo, PurchaseDate, SupplierID, TotalAmount, TotalPayment, TotalBalance) VALUES (@INV, @PDT, @SID, @TAM, @TPM, @TBL)", cm);
            cmd.Parameters.AddWithValue("@INV", p.InvoiceNo);
            cmd.Parameters.AddWithValue("@PDT", p.PurchaseDate);
            cmd.Parameters.AddWithValue("@SID", p.SupplierID);
            cmd.Parameters.AddWithValue("@TAM", p.Amount);
            cmd.Parameters.AddWithValue("@TPM", p.Payment);
            cmd.Parameters.AddWithValue("@TBL", p.Balance);
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

        public Server2Client addPurchaseDetails(PurchaseDetail p)
        {
            Server2Client sc = new Server2Client();
            OleDbCommand cmd = new OleDbCommand("INSERT INTO PurchaseDetail (InvoiceNo, ProductID, Quantity, BuyingValue, SellingValue, TotalAmount) VALUES (@INV, @PID, @QTY, @BVL, @SVL, @TAM)", cm);
            cmd.Parameters.AddWithValue("@INV", p.InvoiceNo);
            cmd.Parameters.AddWithValue("@PID", p.ProductID);
            cmd.Parameters.AddWithValue("@QTY", p.Quantity);
            cmd.Parameters.AddWithValue("@BVL", p.BuyingValue);
            cmd.Parameters.AddWithValue("@SVL", p.SellingValue);
            cmd.Parameters.AddWithValue("@TAM", p.Amount);
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

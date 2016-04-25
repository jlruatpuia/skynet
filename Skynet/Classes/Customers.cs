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
    }
}

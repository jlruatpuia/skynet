using System;
using System.Data;
using System.Data.OleDb;

namespace Skynet.Classes
{
    class Users
    {
        OleDbConnection cm = new OleDbConnection(Utils.UsersCS);

        public Server2Client GetUsers()
        {
            Server2Client sc = new Server2Client();
            OleDbCommand cmd = new OleDbCommand("SELECT ID, [Username], '****' AS [Password], IIF(AccountType=0, 'admin', 'sales') AS [AcType], [Active] FROM [User]", cm);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            sc.Count = ds.Tables[0].Rows.Count;
            sc.dataSet = ds;
            sc.dataTable = ds.Tables[0];
            return sc;
        }

        public User GetUser(int ID)
        {
            User u = new User();
            OleDbCommand cmd = new OleDbCommand("SELECT ID, Username, Password, AccountType, Active FROM [User] WHERE ID=" + ID, cm);
            try
            {
                cm.Open();
                OleDbDataReader rd = cmd.ExecuteReader();
                u.ID = ID;
                u.Username = rd[1].ToString();
                u.Password = rd[2].ToString();
                u.AccountType = Convert.ToInt32(rd[3]);
                u.Active = Convert.ToBoolean(rd[4]);
            }
            catch(Exception ex)
            {
                u.ID = ID;
                u.Username = "Error " + ex.Message;
                u.Password = "Error " + ex.Message;
                u.AccountType = 0;
                u.Active = false;
            }
            finally { cm.Close(); }
            return u;
        }

        public Server2Client AddUser(User u)
        {
            Server2Client sc = new Server2Client();
            string password = Utils.Encrypt(u.Password);
            OleDbCommand cmd = new OleDbCommand("INSERT INTO [User]([Username], [Password], [AccountType], [Active]) VALUES (@UNM, @PWD, @ACT, @ACX)", cm);
            cmd.Parameters.AddWithValue("@UNM", u.Username);
            cmd.Parameters.AddWithValue("@PWD", password);
            cmd.Parameters.AddWithValue("@ACT", u.AccountType);
            cmd.Parameters.AddWithValue("@ACX", u.Active);
            try
            {
                cm.Open();
                cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                sc.Message = ex.Message;
            }
            finally { cm.Close(); }
            return sc;
        }

        public Server2Client UpdateUser(User u)
        {
            Server2Client sc = new Server2Client();
            string password = Utils.Encrypt(u.Password);
            OleDbCommand cmd = new OleDbCommand("UPDATE [User] SET Username=@UNM, Password=@PWD, AccountType=@ACT, Active=@ACX WHERE ID=" + u.ID, cm);
            cmd.Parameters.AddWithValue("@UNM", u.Username);
            cmd.Parameters.AddWithValue("@PWD", password);
            cmd.Parameters.AddWithValue("@ACT", u.AccountType);
            cmd.Parameters.AddWithValue("@ACX", u.Active);
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

        public Server2Client ChangePassword(int UID, string NewPassword)
        {
            Server2Client sc = new Server2Client();
            string newPassword = Utils.Encrypt(NewPassword);
            OleDbCommand cmd = new OleDbCommand("UPDATE [User] SET Password=@PWD WHERE ID=" + UID, cm);
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

        public Server2Client DeactivateUser(int UID)
        {
            Server2Client sc = new Server2Client();
            OleDbCommand cmd = new OleDbCommand("UPDATE [User] SET Active=FALSE WHERE ID=" + UID, cm);
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

        public Server2Client DeleteUser(int UID)
        {
            Server2Client sc = new Server2Client();
            OleDbCommand cmd = new OleDbCommand("DELETE FROM [User] WHERE ID=" + UID, cm);
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
    }
}

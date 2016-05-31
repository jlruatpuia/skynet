using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Skynet.Classes
{
    class Settings
    {
        
        public static void GeometryFromString(string thisWindowGeometry, Form formIn)
        {
            if (string.IsNullOrEmpty(thisWindowGeometry) == true)
            {
                return;
            }
            string[] numbers = thisWindowGeometry.Split('|');
            string windowString = numbers[4];
            if (windowString == "Normal")
            {
                Point windowPoint = new Point(int.Parse(numbers[0]),
                    int.Parse(numbers[1]));
                Size windowSize = new Size(int.Parse(numbers[2]),
                    int.Parse(numbers[3]));

                bool locOkay = GeometryIsBizarreLocation(windowPoint, windowSize);
                bool sizeOkay = GeometryIsBizarreSize(windowSize);

                if (locOkay == true && sizeOkay == true)
                {
                    formIn.Location = windowPoint;
                    formIn.Size = windowSize;
                    formIn.StartPosition = FormStartPosition.Manual;
                    formIn.WindowState = FormWindowState.Normal;
                }
                else if (sizeOkay == true)
                {
                    formIn.Size = windowSize;
                }
            }
            else if (windowString == "Maximized")
            {
                formIn.Location = new Point(100, 100);
                formIn.StartPosition = FormStartPosition.Manual;
                formIn.WindowState = FormWindowState.Maximized;
            }
        }

        private static bool GeometryIsBizarreLocation(Point loc, Size size)
        {
            bool locOkay;
            if (loc.X < 0 || loc.Y < 0)
            {
                locOkay = false;
            }
            else if (loc.X + size.Width > Screen.PrimaryScreen.WorkingArea.Width)
            {
                locOkay = false;
            }
            else if (loc.Y + size.Height > Screen.PrimaryScreen.WorkingArea.Height)
            {
                locOkay = false;
            }
            else
            {
                locOkay = true;
            }
            return locOkay;
        }

        private static bool GeometryIsBizarreSize(Size size)
        {
            return (size.Height <= Screen.PrimaryScreen.WorkingArea.Height &&
                size.Width <= Screen.PrimaryScreen.WorkingArea.Width);
        }

        public static string GeometryToString(Form mainForm)
        {
            return mainForm.Location.X.ToString() + "|" +
                mainForm.Location.Y.ToString() + "|" +
                mainForm.Size.Width.ToString() + "|" +
                mainForm.Size.Height.ToString() + "|" +
                mainForm.WindowState.ToString();
        }

        public static string GetFinancialYear(DateTime dt)
        {
            string finyear = "";

            int m = dt.Month;
            int y = dt.Year;
            if (m > 3)
            {
                finyear = y.ToString().Substring(2, 2) + "-" + Convert.ToString((y + 1)).Substring(2, 2);
            }
            else
            {
                finyear = Convert.ToString((y - 1)).Substring(2, 2) + "-" + y.ToString().Substring(2, 2);
            }
            return finyear;
        }

        public static string GetInvoiceNo(DateTime dt, string Table)
        {
            //string inv_no = "";
            int inv_no;
            string inv_yr = "";
            OleDbConnection cm = new OleDbConnection(Utils.ConnString);
            OleDbCommand cmd = new OleDbCommand("SELECT TOP 1 MID(InvoiceNo, 11, 5) FROM " + Table + " ORDER BY InvoiceNo DESC", cm);
            try
            {
                cm.Open();
                OleDbDataReader rd = cmd.ExecuteReader();
                rd.Read();
                inv_yr = rd[0].ToString();
                inv_yr = inv_yr.Substring(0, 5);
            }
            catch
            {
                inv_yr = GetFinancialYear(dt);
            }
            finally { cm.Close(); }

            string yr = inv_yr == GetFinancialYear(dt) ? inv_yr : GetFinancialYear(dt);

            cmd = new OleDbCommand("SELECT TOP 1 MID(InvoiceNo, 17) FROM " + Table + " WHERE MID(InvoiceNo, 11, 5)='" + yr +"' ORDER BY InvoiceNo DESC", cm);
            try
            {
                cm.Open();
                OleDbDataReader rd = cmd.ExecuteReader();
                rd.Read();
                string i = rd[0].ToString();
                inv_no = Convert.ToInt32(rd[0]); //.ToString();
            }
            catch
            {
                inv_no = 0;
            }
            finally { cm.Close(); }

            string ShortName = Properties.Settings.Default.ShortName;

            return ShortName + "/RI/" + yr + "/" + (inv_no + 1).ToString("0000");
        }
    }
}

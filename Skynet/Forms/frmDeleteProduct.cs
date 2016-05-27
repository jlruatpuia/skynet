using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Skynet.Classes;

namespace Skynet.Forms
{
    public partial class frmDeleteProduct : DevExpress.XtraEditors.XtraForm
    {
        Server2Client sc = new Server2Client();
        Categories cat = new Categories();
        Products prd = new Products();

        public frmDeleteProduct()
        {
            InitializeComponent();

            sc = new Server2Client();
            cat = new Categories();

            sc = cat.GetCategories();
            lueCAT.Properties.DataSource = sc.dataTable;
            lueCAT.Properties.DisplayMember = "CategoryName";
            lueCAT.Properties.ValueMember = "ID";
        }

        private void lueCAT_EditValueChanged(object sender, EventArgs e)
        {
            int CID = Convert.ToInt32(lueCAT.EditValue);

            sc = new Server2Client();
            prd = new Products();

            sc = prd.GetProductByCategory(CID);
            grd.DataSource = sc.dataTable;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string j = "";
            for(int i = 0; i <= grv.SelectedRowsCount; i++)
            {
                int h = Convert.ToInt32(grv.GetSelectedRows());
            }
        }
    }
}
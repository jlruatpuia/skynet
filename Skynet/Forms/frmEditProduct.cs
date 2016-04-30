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
    public partial class frmEditProduct : XtraForm
    {
        void InitCategory()
        {
            Server2Client sc = new Server2Client();
            Categories cat = new Categories();
            sc = cat.GetCategories();

            lueCAT.Properties.DataSource = sc.dataTable;
            lueCAT.Properties.DisplayMember = "CategoryName";
            lueCAT.Properties.ValueMember = "ID";

            lueCAT2.Properties.DataSource = sc.dataTable;
            lueCAT2.Properties.DisplayMember = "CategoryName";
            lueCAT2.Properties.ValueMember = "ID";
        }

        void InitProducts(int CategoryID)
        {
            Server2Client sc = new Server2Client();
            Products prd = new Products();
            sc = prd.GetProductByCategory(CategoryID);

            luePRD.Properties.DataSource = sc.dataTable;
            luePRD.Properties.DisplayMember = "ProductName";
            luePRD.Properties.ValueMember = "ID";
        }
        public frmEditProduct()
        {
            InitializeComponent();

            InitCategory();
        }

        private void lueCAT_EditValueChanged(object sender, EventArgs e)
        {
            if(lueCAT.EditValue != null)
            {
                int cid = Convert.ToInt32(lueCAT.EditValue);

                InitProducts(cid);
            }
        }

        private void luePRD_EditValueChanged(object sender, EventArgs e)
        {
            if(luePRD.EditValue != null)
            {
                int pid = Convert.ToInt32(luePRD.EditValue);

                Product p = new Product();
                Products prd = new Products();

                p = prd.GetProductByID(pid);

                lueCAT2.EditValue = lueCAT.EditValue;

                txtPNM.Text = p.ProductName;
                txtBVL.EditValue = p.BuyingValue;
                txtSVL.EditValue = p.SellingValue;
                txtQTY.EditValue = p.Quantity;
                txtBCD.EditValue = p.BarCode;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!dxVP.Validate())
                return;

            Product p = new Product();
            p.ProductID = Convert.ToInt32(luePRD.EditValue);
            p.CategoryID = Convert.ToInt32(lueCAT2.EditValue);
            p.ProductName = txtPNM.EditValue.ToString();
            p.BuyingValue = Convert.ToDouble(txtBVL.EditValue);
            p.SellingValue = Convert.ToDouble(txtSVL.EditValue);
            p.Quantity = Convert.ToInt32(txtQTY.EditValue);
            p.BarCode = txtBCD.Text;

            Server2Client sc = new Server2Client();
            Products prd = new Products();
            sc = prd.updateProduct(p);
            if (sc.Message != null)
                XtraMessageBox.Show(sc.Message);
            else
                DialogResult = DialogResult.OK;
        }
    }
}
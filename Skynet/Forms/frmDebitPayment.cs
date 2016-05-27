using DevExpress.XtraEditors;
using Skynet.Classes;
using System;

namespace Skynet
{
    public partial class frmDebitPayment : XtraForm
    {
        Server2Client sc;
        SupplierAccounts sa;
        Suppliers s;

        public frmDebitPayment()
        {
            InitializeComponent();
            txtPDT.DateTime = DateTime.Now.Date;
            sc = new Server2Client();
            s = new Suppliers();
            sc = s.getSuppliersFull();

            lueSNM.Properties.DataSource = sc.dataTable;
            lueSNM.Properties.DisplayMember = "SupplierName";
            lueSNM.Properties.ValueMember = "ID";

            txtCBAL.Text = "0";
            txtAMNT.Text = "0";
            txtNBAL.Text = "0";
        }

        public frmDebitPayment(int SupplierID)
        {
            InitializeComponent();
            txtPDT.DateTime = DateTime.Now.Date;
            sc = new Server2Client();
            s = new Suppliers();
            sa = new SupplierAccounts();
            sc = s.getSuppliers();

            lueSNM.Properties.DataSource = sc.dataTable;
            lueSNM.Properties.DisplayMember = "SupplierName";
            lueSNM.Properties.ValueMember = "ID";

            lueSNM.EditValue = SupplierID;
            sc = sa.getSupplierBalance(SupplierID);

            txtCBAL.Text = sc.Value.ToString();

            txtAMNT.Text = "0";
            txtNBAL.Text = "0";
        }

        private void lueSNM_EditValueChanged(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(lueSNM.EditValue);

            sc = new Server2Client();
            sa = new SupplierAccounts();
            sc = sa.getSupplierBalance(id);

            if (sc.Message != null)
            {
                txtCBAL.Text = sc.Message;
                txtCBAL.Enabled = false;
            }
            else
            {
                txtCBAL.Text = sc.Value.ToString();
            }
        }

        private void txtAMNT_EditValueChanged(object sender, EventArgs e)
        {
            double bal = Convert.ToInt32(txtCBAL.Text);
            double val = txtAMNT.Text == "" ? 0 : Convert.ToDouble(txtAMNT.Text);
            txtNBAL.Text = (bal - val).ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dxValidationProvider1.Validate())
            {
                sc = new Server2Client();
                SupplierAccount ss = new SupplierAccount();

                string Remarks = txtRMKS.Text == "" ? "Debit Payment" : txtRMKS.Text;
                ss.SupplierID = Convert.ToInt32(lueSNM.EditValue);
                ss.TransDate = txtPDT.DateTime;
                ss.Description = Remarks;
                ss.Debit = 0;
                ss.Credit = Convert.ToDouble(txtAMNT.Text);
                ss.Balance = Convert.ToDouble(txtNBAL.Text);

                sa = new SupplierAccounts();
                sc = sa.addTrans(ss);

                if (sc.Message == null)
                {
                    XtraMessageBox.Show("Payment done successfully!");
                    lueSNM.EditValue = null;
                    txtCBAL.Text = "0";
                    txtAMNT.Text = "0";
                    txtNBAL.Text = "0";
                    txtRMKS.Text = "";
                }
                else
                    XtraMessageBox.Show(sc.Message);
            }
        }
    }
}
using DevExpress.XtraEditors;
using Skynet.Classes;
using System;

namespace Skynet
{
    public partial class frmCreditPayment : DevExpress.XtraEditors.XtraForm
    {
        Server2Client sc;
        CustomerAccounts ca;
        Customers cus;
        public frmCreditPayment()
        {
            InitializeComponent();

            txtPDT.DateTime = DateTime.Now.Date;
            sc = new Server2Client();
            cus = new Customers();
            sc = cus.getCustomers();

            lueCNM.Properties.DataSource = sc.dataTable;
            lueCNM.Properties.DisplayMember = "CustomerName";
            lueCNM.Properties.ValueMember = "ID";

            txtCBAL.Text = "0";
            txtAMNT.Text = "0";
            txtNBAL.Text = "0";
        }

        public frmCreditPayment(int CustomerID)
        {
            InitializeComponent();

            txtPDT.DateTime = DateTime.Now.Date;
            sc = new Server2Client();
            cus = new Customers();
            ca = new CustomerAccounts();
            sc = cus.getCustomers();

            lueCNM.Properties.DataSource = sc.dataTable;
            lueCNM.Properties.DisplayMember = "CustomerName";
            lueCNM.Properties.ValueMember = "ID";

            lueCNM.EditValue = CustomerID;
            sc = ca.getCustomerBalance(CustomerID);

            txtCBAL.Text = sc.Value.ToString();
            
            txtAMNT.Text = "0";
            txtNBAL.Text = "0";
        }

        private void lueCNM_EditValueChanged(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(lueCNM.EditValue);

            sc = new Server2Client();
            ca = new CustomerAccounts();
            sc = ca.getCustomerBalance(id);

            if(sc.Message != null)
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
                CustomerAccount c = new CustomerAccount();

                string Remarks = txtRMKS.Text == "" ? "Credit Payment" : txtRMKS.Text;
                c.CustomerID = Convert.ToInt32(lueCNM.EditValue);
                c.TransDate = txtPDT.DateTime;
                c.Description = Remarks;
                c.Debit = 0;
                c.Credit = Convert.ToDouble(txtAMNT.Text);
                c.Balance = Convert.ToDouble(txtNBAL.Text);

                ca = new CustomerAccounts();
                sc = ca.addTrans(c);

                if (sc.Message == null)
                {
                    XtraMessageBox.Show("Payment done successfully!");
                    lueCNM.EditValue = null;
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
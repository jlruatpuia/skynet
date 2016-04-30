using Skynet.Classes;
using System;
using System.Windows.Forms;

namespace Skynet
{
    public partial class frmCustomer : DevExpress.XtraEditors.XtraForm
    {
        Server2Client sc;
        Customers cust;
        Customer cus;
        Timer tmr = new Timer();
        int counter = 0;
        public int _id { get; set; }

        public frmCustomer()
        {
            InitializeComponent();
            lbMSG.Text = string.Empty;
            tmr.Interval = 1000;
            tmr.Tick += new System.EventHandler(this.tmr_tick);
        }

        public frmCustomer(string something)
        {
            InitializeComponent();
            btnSave.Text = "&Add";
        }

        public frmCustomer(int ID)
        {
            InitializeComponent();
            lbMSG.Text = string.Empty;
            tmr.Interval = 1000;
            tmr.Tick += new System.EventHandler(this.tmr_tick);

            sc = new Server2Client();
            cus = new Customer();
            cust = new Customers();

            cus = cust.getCustomer(ID);
            _id = ID;
            txtCNM.Text = cus.CustomerName;
            txtADR.Text = cus.Address;
            txtPHN.Text = cus.Phone;
            txtEML.Text = cus.Email;
            //txtBAL.Text = cus.Balance.ToString();

            btnSave.Text = "&Update";
        }

        void reset()
        {
            txtCNM.Text = "";
            txtADR.Text = "";
            txtPHN.Text = "";
            txtEML.Text = "";
            //txtBAL.Text = "0";
            txtCNM.Focus();
        }
        private void tmr_tick(object sender, EventArgs e)
        {
            counter++;
            if(counter == 2)
            {
                lbMSG.Text = string.Empty;
                
                tmr.Stop();
                counter = 0;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (btnSave.Text == "&Update")
            {
                if (dxVP.Validate())
                {
                    sc = new Server2Client();
                    cus = new Customer();
                    cust = new Customers();
                    cus.CustomerID = _id;
                    cus.CustomerName = txtCNM.Text;
                    cus.Address = txtADR.Text;
                    cus.Phone = txtPHN.Text;
                    cus.Email = txtEML.Text;
                    //cus.Balance = Convert.ToInt32(txtBAL.Text);
                    sc = cust.updateCustomer(cus);
                    if (sc.Message == null)
                    {
                        lbMSG.Text = "Customer details updated!";
                        Close();
                    }
                    else
                        lbMSG.Text = sc.Message;
                }
            }
            else if(btnSave.Text == "&Save")
            {
                if (dxVP.Validate())
                {
                    sc = new Server2Client();
                    cus = new Customer();
                    cust = new Customers();
                    cus.CustomerName = txtCNM.Text;
                    cus.Address = txtADR.Text;
                    cus.Phone = txtPHN.Text;
                    cus.Email = txtEML.Text;
                    //cus.Balance = Convert.ToInt32(txtBAL.Text);
                    sc = cust.addCustomer(cus);
                    if (sc.Message == null)
                    {
                        lbMSG.Text = "New Customer added!";
                        reset();
                    }
                    else
                        lbMSG.Text = sc.Message;
                    tmr.Enabled = true;
                    tmr.Start();
                }
            }
            else
            {
                if (dxVP.Validate())
                {
                    sc = new Server2Client();
                    cus = new Customer();
                    cust = new Customers();
                    cus.CustomerName = txtCNM.Text;
                    cus.Address = txtADR.Text;
                    cus.Phone = txtPHN.Text;
                    cus.Email = txtEML.Text;
                    //cus.Balance = Convert.ToInt32(txtBAL.Text);
                    sc = cust.addCustomer(cus);
                    if (sc.Message == null)
                    {
                        sc = new Server2Client();
                        cust = new Customers();
                        sc = cust.getMaxID();
                        _id = sc.Count;
                        Close();
                    }
                    else
                        lbMSG.Text = sc.Message;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void frmCustomer_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
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
    public partial class frmSuppliers : XtraForm
    {
        Server2Client sc;
        Suppliers sup = new Suppliers();
        Supplier s = new Supplier();
        Timer tmr = new Timer();
        int counter = 0;
        public int _id { get; set; }

        public frmSuppliers()
        {
            InitializeComponent();
            lbMSG.Text = string.Empty;
            tmr.Interval = 1000;
            tmr.Tick += new EventHandler(this.tmr_tick);
        }

        public frmSuppliers(string Something)
        {
            InitializeComponent();
            btnOK.Text = "&Add";
        }

        public frmSuppliers(int id)
        {
            InitializeComponent();

            lbMSG.Text = string.Empty;
            tmr.Interval = 1000;
            tmr.Tick += new EventHandler(this.tmr_tick);

            sc = new Server2Client();
            s = new Supplier();
            sup = new Suppliers();

            s = sup.getSupplier(id);
            _id = id;
            txtCNM.Text = s.SupplierName;
            txtADR.Text = s.Address;
            txtPHN.Text = s.Phone;
            txtEML.Text = s.Email;
            //txtBAL.Text = cus.Balance.ToString();

            btnOK.Text = "&Update";
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
            if (counter == 2)
            {
                lbMSG.Text = string.Empty;

                tmr.Stop();
                counter = 0;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (btnOK.Text == "&Update")
            {
                if (dxvp.Validate())
                {
                    sc = new Server2Client();
                    s = new Supplier();
                    sup = new Suppliers();
                    s.SupplierID = _id;
                    s.SupplierName = txtCNM.Text;
                    s.Address = txtADR.Text;
                    s.Phone = txtPHN.Text;
                    s.Email = txtEML.Text;
                    //cus.Balance = Convert.ToInt32(txtBAL.Text);
                    sc = sup.updateSupplier(s);
                    if (sc.Message == null)
                    {
                        lbMSG.Text = "Supplier details updated!";
                        Close();
                    }
                    else
                        lbMSG.Text = sc.Message;
                }
            }
            else if (btnOK.Text == "&Save")
            {
                if (dxvp.Validate())
                {
                    sc = new Server2Client();
                    s = new Supplier();
                    sup = new Suppliers();
                    s.SupplierName = txtCNM.Text;
                    s.Address = txtADR.Text;
                    s.Phone = txtPHN.Text;
                    s.Email = txtEML.Text;
                    //cus.Balance = Convert.ToInt32(txtBAL.Text);
                    sc = sup.addSupplier(s);
                    if (sc.Message == null)
                    {
                        lbMSG.Text = "New Supplier added!";
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
                if (dxvp.Validate())
                {
                    sc = new Server2Client();
                    s = new Supplier();
                    sup = new Suppliers();
                    s.SupplierName = txtCNM.Text;
                    s.Address = txtADR.Text;
                    s.Phone = txtPHN.Text;
                    s.Email = txtEML.Text;
                    //cus.Balance = Convert.ToInt32(txtBAL.Text);
                    sc = sup.addSupplier(s);
                    if (sc.Message == null)
                    {
                        sc = new Server2Client();
                        sup = new Suppliers();
                        sc = sup.getMaxID();
                        _id = sc.Count;
                        Close();
                    }
                    else
                        lbMSG.Text = sc.Message;
                }
            }
        }

        private void btnOKN_Click(object sender, EventArgs e)
        {
            if (!dxvp.Validate()) return;
            //dt.Rows.Add(UName, Address, Phone, Email);
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void frmSupCus_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
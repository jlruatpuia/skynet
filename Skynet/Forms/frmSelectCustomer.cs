using Skynet.Classes;
using System;
using System.Windows.Forms;

namespace Skynet
{
    public partial class frmSelectCustomer : DevExpress.XtraEditors.XtraForm
    {
        public int CustomerID { get; set; }
        public bool DateSelected { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

        public frmSelectCustomer()
        {
            InitializeComponent();

            Server2Client sc = new Server2Client();
            Customers cus = new Customers();
            sc = cus.getCustomers();
            lueCNM.Properties.DataSource = sc.dataTable;
            lueCNM.Properties.DisplayMember = "CustomerName";
            lueCNM.Properties.ValueMember = "ID";

            checkEdit1_CheckedChanged(null, null);
            dtFr.DateTime = DateTime.Now.Date;
            dtTo.DateTime = DateTime.Now.Date;
        }

        public frmSelectCustomer(int ID)
        {
            InitializeComponent();
            Server2Client sc = new Server2Client();
            Customers cus = new Customers();
            sc = cus.getCustomers();
            lueCNM.Properties.DataSource = sc.dataTable;
            lueCNM.Properties.DisplayMember = "CustomerName";
            lueCNM.Properties.ValueMember = "ID";
            checkEdit1_CheckedChanged(null, null);
            chkSelect.Enabled = false;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (dxValidationProvider1.Validate())
            {
                CustomerID = Convert.ToInt32(lueCNM.EditValue);
                if (DateSelected)
                {
                    DateFrom = dtFr.DateTime;
                    DateTo = dtTo.DateTime;
                }
                DialogResult = DialogResult.OK;
            }
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSelect.Checked)
            {
                dtFr.Enabled = true;
                dtTo.Enabled = true;
                DateSelected = true;
            }
            else
            {
                dtFr.Enabled = false;
                dtTo.Enabled = false;
                DateSelected = false;
            }
        }
    }
}
using Skynet.Classes;
using System;
using System.Windows.Forms;

namespace Skynet
{
    public partial class frmSelectCustomer : DevExpress.XtraEditors.XtraForm
    {
        public int CustomerID { get; set; }

        public frmSelectCustomer()
        {
            InitializeComponent();

            Server2Client sc = new Server2Client();
            Customers cus = new Customers();
            sc = cus.getCustomers();
            lueCNM.Properties.DataSource = sc.dataTable;
            lueCNM.Properties.DisplayMember = "CustomerName";
            lueCNM.Properties.ValueMember = "ID";
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (dxValidationProvider1.Validate())
            {
                CustomerID = Convert.ToInt32(lueCNM.EditValue);
                DialogResult = DialogResult.OK;
            }
        }
    }
}
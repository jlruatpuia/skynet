using DevExpress.XtraEditors;
using Skynet.Classes;
using System;
using System.Windows.Forms;

namespace Skynet
{
    public partial class frmSelectSupplier : XtraForm
    {
        public int SupplierID { get; set; }

        public frmSelectSupplier()
        {
            InitializeComponent();

            Server2Client sc = new Server2Client();

            Suppliers sup = new Suppliers();
            sc = sup.getSuppliers();
            lueSNM.Properties.DataSource = sc.dataTable;
            lueSNM.Properties.DisplayMember = "SupplierName";
            lueSNM.Properties.ValueMember = "ID";
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (dxValidationProvider1.Validate())
            {
                SupplierID = Convert.ToInt32(lueSNM.EditValue);
                DialogResult = DialogResult.OK;
            }
        }
    }
}
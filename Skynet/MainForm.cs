using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Skynet.Forms;

namespace Skynet
{
    public partial class MainForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void bbNewProduct_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmNewProduct frm = new frmNewProduct();
            frm.ShowDialog();
        }

        private void bbExtProduct_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmExtProduct frm = new frmExtProduct();
            frm.ShowDialog();
        }

        private void bbSellProduct_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmSellProduct frm = new frmSellProduct();
            frm.ShowDialog();
        }
    }
}

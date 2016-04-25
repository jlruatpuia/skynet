using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Skynet.Classes;
using System;
using System.Data;
using System.Windows.Forms;

namespace Skynet.Forms
{
    public partial class frmNewProduct : XtraForm
    {
        DataTable dt = new DataTable();
        
        void InitDataTable()
        {
            //dt.Columns.Add("ProductID", typeof(int));
            dt.Columns.Add("CategoryID", typeof(int));
            dt.Columns.Add("ProductName", typeof(string));
            dt.Columns.Add("BuyingValue", typeof(double));
            dt.Columns.Add("SellingValue", typeof(double));
            dt.Columns.Add("Quantity", typeof(int));
            dt.Columns.Add("BarCode", typeof(string));
        }

        void InitCategory()
        {
            Server2Client sc = new Server2Client();
            Categories cat = new Categories();
            sc = cat.GetCategories();

            lueCAT.Properties.DataSource = sc.dataTable;
            lueCAT.Properties.DisplayMember = "CategoryName";
            lueCAT.Properties.ValueMember = "ID";
        }

        public frmNewProduct()
        {
            InitializeComponent();

            InitDataTable();
            InitCategory();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!dxVP.Validate())
                return;

            int CAT = Convert.ToInt32(lueCAT.EditValue);
            string PNM = txtPNM.Text;
            double BVL = Convert.ToDouble(txtBVL.EditValue);
            double SVL = Convert.ToDouble(txtSVL.EditValue);
            int QTY = Convert.ToInt32(txtQTY.EditValue);
            string BCD = txtBCD.Text;

            DataRow row = dt.NewRow();
            row["CategoryID"] = CAT;
            row["ProductName"] = PNM;
            row["BuyingValue"] = BVL;
            row["SellingValue"] = SVL;
            row["Quantity"] = QTY;
            row["BarCode"] = BCD;
            dt.Rows.Add(row);

            grd.DataSource = dt;
            grd.Refresh();
        }

        private void txtBCD_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                btnAdd_Click(null, null);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(grv.RowCount <= 0)
            {
                XtraMessageBox.Show("Please add some product first!");
                return;
            }

            Server2Client sc = new Server2Client();
            Products prd = new Products();
            

            for(int i = 0; i<= grv.RowCount - 1; i++)
            {
                Product p = new Product();
                p.CategoryID = Convert.ToInt32(grv.GetRowCellValue(i, colCAT));
                p.ProductName = grv.GetRowCellValue(i, colPNM).ToString();
                p.BuyingValue = Convert.ToDouble(grv.GetRowCellValue(i, colBVL));
                p.SellingValue = Convert.ToDouble(grv.GetRowCellValue(i, colSVL));
                p.Quantity = Convert.ToInt32(grv.GetRowCellValue(i, colQTY));
                p.BarCode = grv.GetRowCellValue(i, colBCD).ToString();

                sc = prd.AddProduct(p);
                if(sc.Message != null)
                {
                    XtraMessageBox.Show(sc.Message);
                    return;
                }
            }

            XtraMessageBox.Show("Product(s) added successfully!");

            grd.DataSource = null;
            grd.Refresh();
            lueCAT.EditValue = null;
            txtPNM.EditValue = null;
            txtBVL.EditValue = 0;
            txtSVL.EditValue = 0;
            txtQTY.EditValue = 1;
            txtBCD.EditValue = null;
            lueCAT.Focus();
        }

        private void grv_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator)
            {
                string rowno = null;
                if (e.RowHandle == -1)
                    rowno = "";
                else
                    rowno = (e.RowHandle + 1).ToString();
                e.Info.DisplayText = rowno;
            }
        }

        private void grv_Click(object sender, EventArgs e)
        {
            GridHitInfo hi = grv.CalcHitInfo(grd.PointToClient(MousePosition));
            if (hi.InRowCell && hi.Column == colEDT)
            {
                int CID = Convert.ToInt32(grv.GetRowCellValue(hi.RowHandle, colCAT));
                string PNM = grv.GetRowCellValue(hi.RowHandle, colPNM).ToString();
                double BVL = Convert.ToDouble(grv.GetRowCellValue(hi.RowHandle, colBVL));
                double SVL = Convert.ToDouble(grv.GetRowCellValue(hi.RowHandle, colSVL));
                int QTY = Convert.ToInt32(grv.GetRowCellValue(hi.RowHandle, colQTY));
                string BCD = grv.GetRowCellValue(hi.RowHandle, colBCD).ToString();

                frmRowEdit f = new frmRowEdit(hi.RowHandle, CID, PNM, BVL, SVL, QTY, BCD);

                if (f.ShowDialog() == DialogResult.OK)
                {
                    grv.SetRowCellValue(hi.RowHandle, colCAT, f.categoryID);
                    grv.SetRowCellValue(hi.RowHandle, colPNM, f.productName);
                    grv.SetRowCellValue(hi.RowHandle, colBVL, f.buyingValue);
                    grv.SetRowCellValue(hi.RowHandle, colSVL, f.sellingValue);
                    grv.SetRowCellValue(hi.RowHandle, colQTY, f.quantity);
                    grv.SetRowCellValue(hi.RowHandle, colBCD, f.barCode);
                    grv.UpdateCurrentRow();
                    grv.RefreshData();
                }
                else if (DialogResult == DialogResult.Yes)
                {
                    grv.DeleteRow(hi.RowHandle);
                    grv.UpdateCurrentRow();
                    grv.RefreshData();
                }
            }
        }
    }
}
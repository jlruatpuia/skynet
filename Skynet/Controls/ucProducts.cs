﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Skynet.Forms;
using Skynet.Classes;

namespace Skynet.Controls
{
    public partial class ucProducts : XtraUserControl
    {
        DataTable dt = new DataTable();

        void InitDataTable()
        {
            dt.Columns.Add("CategoryName", typeof(string));
            dt.Columns.Add("ProductName", typeof(string));
            dt.Columns.Add("BuyingValue", typeof(double));
            dt.Columns.Add("SellingValue", typeof(double));
            dt.Columns.Add("TotalQuantity", typeof(int));
            dt.Columns.Add("TotalBuyingValue", typeof(double));
            dt.Columns.Add("TotalSellingValue", typeof(double));
        }
        public ucProducts()
        {
            InitializeComponent();

            InitDataTable();
            Server2Client sc = new Server2Client();
            Products prd = new Products();

            sc = prd.GetProductValues();

            DataTable d = new DataTable();
            d = sc.dataTable;

            for(int i =0; i <= d.Rows.Count - 1; i++)
            {
                //CategoryName, ProductName, BuyingValue, SellingValue, TotalQuantity
                DataRow r = dt.NewRow();
                r["CategoryName"] = d.Rows[i].ItemArray[0].ToString();
                r["ProductName"] = d.Rows[i].ItemArray[1].ToString();
                r["BuyingValue"] = Convert.ToDouble(d.Rows[i].ItemArray[2]);
                r["SellingValue"] = Convert.ToDouble(d.Rows[i].ItemArray[3]);
                r["TotalQuantity"] = Convert.ToInt32(d.Rows[i].ItemArray[4]);
                r["TotalBuyingValue"] = Convert.ToDouble(d.Rows[i].ItemArray[2]) * Convert.ToInt32(d.Rows[i].ItemArray[4]); 
                r["TotalSellingValue"] = Convert.ToDouble(d.Rows[i].ItemArray[3]) * Convert.ToInt32(d.Rows[i].ItemArray[4]);

                dt.Rows.Add(r);
            }

            grd.DataSource = dt;
            grd.Refresh();
        }

        private void bEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmEditProduct frm = new frmEditProduct();
            frm.ShowDialog();
            //if(frm.ShowDialog() == DialogResult.OK)
            //{
            //    //Do something
            //}
        }

        private void bFind_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (bFind.Down)
                grv.OptionsFind.AlwaysVisible = true;
            else
                grv.OptionsFind.AlwaysVisible = false;
        }

        private void bPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
        }

        private void bPDF_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog() { Filter = "PDF File (*.pdf)|*.pdf" };
            if(sfd.ShowDialog() == DialogResult.OK)
            {
                grv.ExportToPdf(sfd.FileName);
            }
        }

        private void bXLS_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel File (*.xls)|*.xls" };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                grv.ExportToXls(sfd.FileName);
            }
        }

        private void grv_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (!grv.IsGroupRow(e.RowHandle))
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
        }

        private void bExpand_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (bExpand.Checked)
            {
                grv.ExpandAllGroups();
                bExpand.Caption = "Collapse";
                bExpand.Glyph = DevExpress.Images.ImageResourceCache.Default.GetImage("office2013/actions/squeeze_16x16.png");
                bExpand.LargeGlyph = DevExpress.Images.ImageResourceCache.Default.GetImage("office2013/actions/squeeze_32x32.png");
                //bExpand.Glyph = DevExpress.Images.ImageResourceCache.ImagesAssembly.
            }
            else
            {
                grv.CollapseAllGroups();
                bExpand.Caption = "Expand";
                bExpand.Glyph = DevExpress.Images.ImageResourceCache.Default.GetImage("office2013/actions/stretch_16x16.png");
                bExpand.LargeGlyph = DevExpress.Images.ImageResourceCache.Default.GetImage("office2013/actions/stretch_32x32.png");
            }
        }
    }
}
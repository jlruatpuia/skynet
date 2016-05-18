using System;
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
using DevExpress.XtraReports.UI;

namespace Skynet.Controls
{
    public partial class ucReports : DevExpress.XtraEditors.XtraUserControl
    {
        public ucReports()
        {
            InitializeComponent();

            if (dv.Document == null)
                rpPreview.Visible = false;
        }

        private void bSoldProducts_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmSelect frm = new frmSelect();
            if(frm.ShowDialog() == DialogResult.OK)
            {
                Server2Client sc;
                if(frm.RetVal == 0)
                {
                    sc = new Server2Client();
                    Sales s = new Sales();
                    sc = s.getSoldProducts(frm.DateOn);
                    rptSoldProducts rpt = new rptSoldProducts() { DataSource = sc.dataTable };


                    rpt.lbTTL.Text = "Sold Products On " + frm.DateOn.ToShortDateString();
                    
                    XRSummary stt = new XRSummary();
                    XRSummary gtt = new XRSummary();

                    GroupField grp = new GroupField("SaleDate");
                    rpt.GroupHeader1.GroupFields.Add(grp);

                    rpt.lbSDT.DataBindings.Add("Text", null, "SaleDate", "{0:dd-MM-yyyy}");
                    rpt.lbPNM.DataBindings.Add("Text", null, "ProductName");
                    rpt.lbPID.DataBindings.Add("Text", null, "BarCode");
                    rpt.lbSVL.DataBindings.Add("Text", null, "SumOfSellingValue", "{0:C2}");
                    rpt.lbQTY.DataBindings.Add("Text", null, "SumOfQuantity");
                    rpt.lbAMT.DataBindings.Add("Text", null, "SumOfAmount", "{0:C2}");
                    rpt.lbSTT.DataBindings.Add("Text", null, "SumOfAmount", "{0:C2}");
                    rpt.lbGTT.DataBindings.Add("Text", null, "SumOfAmount", "{0:C2}");

                    stt.FormatString = "{0:C2}";
                    gtt.FormatString = "{0:C2}";

                    stt.Running = SummaryRunning.Group;
                    gtt.Running = SummaryRunning.Report;

                    rpt.lbSTT.Summary = stt;
                    rpt.lbGTT.Summary = gtt;

                    dv.PrintingSystem = rpt.PrintingSystem;
                    rpt.CreateDocument(true);
                }
                else if(frm.RetVal == 1)
                {
                    sc = new Server2Client();
                    Sales s = new Sales();
                    sc = s.getSoldProducts(frm.DateFrom, frm.DateTo);
                    rptSoldProducts rpt = new rptSoldProducts() { DataSource = sc.dataTable };


                    rpt.lbTTL.Text = "Sold Products Between " + frm.DateFrom.ToShortDateString() + " and " + frm.DateTo.ToShortDateString();

                    XRSummary stt = new XRSummary();
                    XRSummary gtt = new XRSummary();

                    GroupField grp = new GroupField("SaleDate");
                    rpt.GroupHeader1.GroupFields.Add(grp);

                    rpt.lbSDT.DataBindings.Add("Text", null, "SaleDate", "{0:dd-MM-yyyy}");
                    rpt.lbPNM.DataBindings.Add("Text", null, "ProductName");
                    rpt.lbPID.DataBindings.Add("Text", null, "BarCode");
                    rpt.lbSVL.DataBindings.Add("Text", null, "SumOfSellingValue", "{0:C2}");
                    rpt.lbQTY.DataBindings.Add("Text", null, "SumOfQuantity");
                    rpt.lbAMT.DataBindings.Add("Text", null, "SumOfAmount", "{0:C2}");
                    rpt.lbSTT.DataBindings.Add("Text", null, "SumOfAmount", "{0:C2}");
                    rpt.lbGTT.DataBindings.Add("Text", null, "SumOfAmount", "{0:C2}");

                    stt.FormatString = "{0:C2}";
                    gtt.FormatString = "{0:C2}";

                    stt.Running = SummaryRunning.Group;
                    gtt.Running = SummaryRunning.Report;

                    rpt.lbSTT.Summary = stt;
                    rpt.lbGTT.Summary = gtt;

                    dv.PrintingSystem = rpt.PrintingSystem;
                    rpt.CreateDocument(true);
                }
            }
        }
    }
}

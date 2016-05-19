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

        private void bCreditPayment_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmSelectCustomer frm = new frmSelectCustomer();
            if(frm.ShowDialog() == DialogResult.OK)
            {
                Server2Client sc = new Server2Client();
                CustomerAccounts ca = new CustomerAccounts();

                sc = ca.getTransactionDetails(frm.CustomerID);

                rptCreditPayment rpt = new rptCreditPayment() { DataSource = sc.dataTable };

                rpt.lbCNM.DataBindings.Add("Text", null, "CustomerName");
                rpt.lbADR.DataBindings.Add("Text", null, "Address");
                rpt.lbPHN.DataBindings.Add("Text", null, "Phone");
                rpt.lbEML.DataBindings.Add("Text", null, "Email");

                rpt.lbTDT.DataBindings.Add("Text", null, "TransDate", "{0:dd-MM-yyyy}");
                rpt.lbRMK.DataBindings.Add("Text", null, "Description");
                rpt.lbTDR.DataBindings.Add("Text", null, "Debit", "{0:c}");
                rpt.lbTCR.DataBindings.Add("Text", null, "Credit", "{0:c}");
                rpt.lbBAL.DataBindings.Add("Text", null, "Balance", "{0:c}");

                dv.PrintingSystem = rpt.PrintingSystem;
                rpt.CreateDocument(true);
            }
        }

        private void dv_DocumentChanged(object sender, EventArgs e)
        {
            if(dv.Document != null)
            {
                rpPreview.Visible = true;
                ribbonControl1.SelectedPage = rpPreview;
            }
        }
    }
}

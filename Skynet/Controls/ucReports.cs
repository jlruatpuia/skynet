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
using Skynet.Reports;

namespace Skynet.Controls
{
    public partial class ucReports : XtraUserControl
    {
        public ucReports()
        {
            InitializeComponent();

            if (dv.Document == null)
                rpPreview.Visible = false;

            Server2Client sc = new Server2Client();
            Customers cus = new Customers();
            sc = cus.getCustomers();
            lueCUS.Properties.DataSource = sc.dataTable;
            lueCUS.Properties.DisplayMember = "CustomerName";
            lueCUS.Properties.ValueMember = "ID";
        }

        private void bProductList_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void bCustomersList_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Server2Client sc = new Server2Client();
            Customers cus = new Customers();
            sc = cus.getCustomersFull();

            rptCustomers rpt = new rptCustomers() { DataSource = sc.dataTable };
            
            XRSummary tbal = new XRSummary();

            rpt.lbCNM.DataBindings.Add("Text", null, "CustomerName");
            rpt.lbADR.DataBindings.Add("Text", null, "Address");
            rpt.lbPHN.DataBindings.Add("Text", null, "Phone");
            rpt.lbEML.DataBindings.Add("Text", null, "Email");
            rpt.lbBAL.DataBindings.Add("Text", null, "Balance", "{0:C2}");
            rpt.lbTBAL.DataBindings.Add("Text", null, "Balance", "{0:C2}");

            tbal.FormatString = "{0:C2}";
            tbal.Running = SummaryRunning.Report;
            rpt.lbTBAL.Summary = tbal;

            dv.PrintingSystem = rpt.PrintingSystem;
            rpt.CreateDocument(true);
        }

        private void bSuppliersList_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Server2Client sc = new Server2Client();
            Suppliers sup = new Suppliers();
            sc = sup.getSuppliersFull();

            rptSuppliers rpt = new rptSuppliers() { DataSource = sc.dataTable };

            XRSummary tbal = new XRSummary();

            rpt.lbCNM.DataBindings.Add("Text", null, "SupplierName");
            rpt.lbADR.DataBindings.Add("Text", null, "Address");
            rpt.lbPHN.DataBindings.Add("Text", null, "Phone");
            rpt.lbEML.DataBindings.Add("Text", null, "Email");
            rpt.lbBAL.DataBindings.Add("Text", null, "Balance", "{0:C2}");
            rpt.lbTBAL.DataBindings.Add("Text", null, "Balance", "{0:C2}");

            tbal.FormatString = "{0:C2}";
            tbal.Running = SummaryRunning.Report;
            rpt.lbTBAL.Summary = tbal;

            dv.PrintingSystem = rpt.PrintingSystem;
            rpt.CreateDocument(true);
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
                else
                {
                    sc = new Server2Client();
                    Sales ss = new Sales();

                    sc = ss.getSoldProducts(frm.InvoiceNo);
                    XRSummary total = new XRSummary();

                    rptCashMemo rpt = new rptCashMemo() { DataSource = sc.dataTable };
                    rpt.lblCNM.DataBindings.Add("Text", null, "CustomerName");
                    rpt.lblADR.DataBindings.Add("Text", null, "Address");
                    rpt.lblPHN.DataBindings.Add("Text", null, "Phone");

                    rpt.lblINV.DataBindings.Add("Text", null, "InvoiceNo");
                    rpt.lblSDT.DataBindings.Add("Text", null, "SaleDate");

                    rpt.lblPNM.DataBindings.Add("Text", null, "ProductName");
                    rpt.lbSNO.DataBindings.Add("Text", null, "BarCode");
                    rpt.lblQTY.DataBindings.Add("Text", null, "Quantity");
                    rpt.lblPRC.DataBindings.Add("Text", null, "SellingValue", "{0:c}");
                    rpt.lblAMT.DataBindings.Add("Text", null, "Amount", "{0:c}");
                    rpt.lblTTL.DataBindings.Add("Text", null, "Amount", "{0:c}");

                    total.FormatString = "{0:C2}";
                    total.Running = SummaryRunning.Report;
                    rpt.lblTTL.Summary = total;
                    //rpt.lblTTL.Text = s.Amount.ToString("c2");
                    int amt = 0;
                    for(int i = 0; i <= sc.dataTable.Rows.Count - 1; i++)
                    {
                        amt += Convert.ToInt32(sc.dataTable.Rows[i].ItemArray[9]);
                    }
                    rpt.lblAMW.Text = "Rupees " + Utils.NumbersToWords(Convert.ToInt32(amt)) + " only";
                    dv.PrintingSystem = rpt.PrintingSystem;
                    rpt.CreateDocument(true);
                }
            }
        }
        
        private void bPurchasedProducts_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            /*
            frmSelect frm = new frmSelect();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                Server2Client sc;
                if (frm.RetVal == 0)
                {
                    sc = new Server2Client();
                    Purchases p = new Purchases();
                    //sc = 
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
                else if (frm.RetVal == 1)
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
                else
                {
                    sc = new Server2Client();
                    Sales ss = new Sales();

                    sc = ss.getSoldProducts(frm.InvoiceNo);
                    XRSummary total = new XRSummary();

                    rptCashMemo rpt = new rptCashMemo() { DataSource = sc.dataTable };
                    rpt.lblCNM.DataBindings.Add("Text", null, "CustomerName");
                    rpt.lblADR.DataBindings.Add("Text", null, "Address");
                    rpt.lblPHN.DataBindings.Add("Text", null, "Phone");

                    rpt.lblINV.DataBindings.Add("Text", null, "InvoiceNo");
                    rpt.lblSDT.DataBindings.Add("Text", null, "SaleDate");

                    rpt.lblPNM.DataBindings.Add("Text", null, "ProductName");
                    rpt.lbSNO.DataBindings.Add("Text", null, "BarCode");
                    rpt.lblQTY.DataBindings.Add("Text", null, "Quantity");
                    rpt.lblPRC.DataBindings.Add("Text", null, "SellingValue", "{0:c}");
                    rpt.lblAMT.DataBindings.Add("Text", null, "Amount", "{0:c}");
                    rpt.lblTTL.DataBindings.Add("Text", null, "Amount", "{0:c}");

                    total.FormatString = "{0:C2}";
                    total.Running = SummaryRunning.Report;
                    rpt.lblTTL.Summary = total;
                    //rpt.lblTTL.Text = s.Amount.ToString("c2");
                    int amt = 0;
                    for (int i = 0; i <= sc.dataTable.Rows.Count - 1; i++)
                    {
                        amt += Convert.ToInt32(sc.dataTable.Rows[i].ItemArray[9]);
                    }
                    rpt.lblAMW.Text = "Rupees " + Utils.NumbersToWords(Convert.ToInt32(amt)) + " only";
                    dv.PrintingSystem = rpt.PrintingSystem;
                    rpt.CreateDocument(true);
                }
            }
            */
        }
        private void bProfitLoss_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmSelect frm = new frmSelect(false);
            if(frm.ShowDialog() == DialogResult.OK)
            {
                Server2Client sc = new Server2Client();
                string dt = null;
                if (frm.RetVal == 0)
                {
                    Sales s = new Sales();
                    sc = s.getProfitLoss(frm.DateOn);
                    dt = "Date " + frm.DateOn.ToShortDateString();
                }
                else
                {
                    Sales s = new Sales();
                    sc = s.getProfitLoss(frm.DateFrom, frm.DateTo);
                    dt = "Between " + frm.DateFrom.ToShortDateString() + " and " + frm.DateTo.ToShortDateString();
                }

                rptPL rpt = new rptPL() { DataSource = sc.dataTable };

                XRSummary gsv = new XRSummary();
                XRSummary gbv = new XRSummary();
                XRSummary gpf = new XRSummary();

                rpt.lbDT.Text = dt;
                rpt.lbSDT.DataBindings.Add("Text", null, "SaleDate", "{0:dd-MM-yyyy}");
                rpt.lbSVL.DataBindings.Add("Text", null, "TotalSellingValue", "{0:C2}");
                rpt.lbBVL.DataBindings.Add("Text", null, "TotalBuyingValue", "{0:C2}");
                rpt.lbPFT.DataBindings.Add("Text", null, "Profit", "{0:C2}");
                rpt.lbGSV.DataBindings.Add("Text", null, "TotalSellingValue", "{0:C2}");
                rpt.lbGBV.DataBindings.Add("Text", null, "TotalBuyingValue", "{0:C2}");
                rpt.lbGPF.DataBindings.Add("Text", null, "Profit", "{0:C2}");

                gsv.FormatString = "{0:C2}";
                gbv.FormatString = "{0:C2}";
                gpf.FormatString = "{0:C2}";


                gsv.Running = SummaryRunning.Report;
                gbv.Running = SummaryRunning.Report;
                gpf.Running = SummaryRunning.Report;

                rpt.lbGSV.Summary = gsv;
                rpt.lbGBV.Summary = gbv;
                rpt.lbGPF.Summary = gpf;

                dv.PrintingSystem = rpt.PrintingSystem;
                rpt.CreateDocument(true);
            }
        }

        private void bCreditPayment_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmSelectCustomer frm = new frmSelectCustomer();
            if(frm.ShowDialog() == DialogResult.OK)
            {
                Server2Client sc = new Server2Client();
                CustomerAccounts ca = new CustomerAccounts();
                Customers cc = new Customers();
                Customer c = new Customer();

                c = cc.getCustomer(frm.CustomerID);
                rptCreditPayment rpt = new rptCreditPayment();

                rpt.lbCNM.Text = c.CustomerName;
                rpt.lbADR.Text = c.Address;
                rpt.lbPHN.Text = c.Phone;
                rpt.lbEML.Text = c.Email;

                if (!frm.DateSelected)
                {
                    sc = ca.getTransactionDetails(frm.CustomerID);
                    rpt.DataSource = sc.dataTable;
                }
                else
                {
                    sc = ca.AccountStatement(frm.CustomerID, frm.DateFrom, frm.DateTo);
                    rpt.DataSource = sc.dataTable;
                }
                rpt.lbTDT.DataBindings.Add("Text", null, "TransDate", "{0:dd-MM-yyyy}");
                rpt.lbRMK.DataBindings.Add("Text", null, "Description");
                rpt.lbTDR.DataBindings.Add("Text", null, "Debit", "{0:c}");
                rpt.lbTCR.DataBindings.Add("Text", null, "Credit", "{0:c}");
                rpt.lbBAL.DataBindings.Add("Text", null, "Balance", "{0:c}");
                dv.PrintingSystem = rpt.PrintingSystem;
                rpt.CreateDocument(true);
            }
        }
        private void bDebitPayment_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmSelectSupplier frm = new frmSelectSupplier();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                Server2Client sc = new Server2Client();
                SupplierAccounts sa = new SupplierAccounts();

                sc = sa.getTransactionDetails(frm.SupplierID);

                rptDebitPayment rpt = new rptDebitPayment() { DataSource = sc.dataTable };

                rpt.lbSNM.DataBindings.Add("Text", null, "SupplierName");
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

        private void bPCATS_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Server2Client sc = new Server2Client();
            ProductDetails pd = new ProductDetails();
            sc = pd.ProductListByCategorySimplified();

            rptProductByCat rpt = new rptProductByCat() { DataSource = sc.dataTable };

            GroupField grp = new GroupField("CategoryName");

            rpt.groupHeader.GroupFields.Add(grp);

            rpt.lbCAT.DataBindings.Add("Text", null, "CategoryName");
            rpt.lbPNM.DataBindings.Add("Text", null, "ProductName");
            rpt.lbBVL.DataBindings.Add("Text", null, "BuyingValue", "{0:C2}");
            rpt.lbSVL.DataBindings.Add("Text", null, "SellingValue", "{0:C2}");
            rpt.lbQTY.DataBindings.Add("Text", null, "SumOfQuantity");
            rpt.lbBCD.Text = "";
            //rpt.lbSBVL.DataBindings.Add("Text", null, "BuyingValue", "{0:C2}");
            //rpt.lbSSVL.DataBindings.Add("Text", null, "SellingValue", "{0:C2}");
            //rpt.lbSQTY.DataBindings.Add("Text", null, "Quantity");
            //rpt.lbGBVL.DataBindings.Add("Text", null, "BuyingValue", "{0:C2}");
            //rpt.lbGSVL.DataBindings.Add("Text", null, "SellingValue", "{0:C2}");
            //rpt.lbGQTY.DataBindings.Add("Text", null, "Quantity");

            //sbvl.FormatString = "{0:C2}";
            //ssvl.FormatString = "{0:C2}";
            //gbvl.FormatString = "{0:C2}";
            //gsvl.FormatString = "{0:C2}";

            //sbvl.Running = SummaryRunning.Group;
            //ssvl.Running = SummaryRunning.Group;
            //sqty.Running = SummaryRunning.Group;

            //gbvl.Running = SummaryRunning.Report;
            //gsvl.Running = SummaryRunning.Report;
            //gqty.Running = SummaryRunning.Report;

            //rpt.lbSBVL.Summary = sbvl;
            //rpt.lbSSVL.Summary = ssvl;
            //rpt.lbSQTY.Summary = sqty;

            //rpt.lbGBVL.Summary = gbvl;
            //rpt.lbGSVL.Summary = gsvl;
            //rpt.lbGQTY.Summary = gqty;

            dv.PrintingSystem = rpt.PrintingSystem;
            rpt.CreateDocument(true);
        }

        private void bPCATX_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Server2Client sc = new Server2Client();
            ProductDetails pd = new ProductDetails();
            sc = pd.ProductListByCategoryExtended();

            rptProductByCat rpt = new rptProductByCat() { DataSource = sc.dataTable };

            GroupField grp = new GroupField("CategoryName");

            rpt.groupHeader.GroupFields.Add(grp);

            rpt.lbCAT.DataBindings.Add("Text", null, "CategoryName");
            rpt.lbPNM.DataBindings.Add("Text", null, "ProductName");
            rpt.lbBVL.DataBindings.Add("Text", null, "BuyingValue", "{0:C2}");
            rpt.lbSVL.DataBindings.Add("Text", null, "SellingValue", "{0:C2}");
            rpt.lbQTY.DataBindings.Add("Text", null, "Quantity");
            rpt.lbBCD.DataBindings.Add("Text", null, "BarCode");
            dv.PrintingSystem = rpt.PrintingSystem;
            rpt.CreateDocument(true);
        }

        private void bPSUPS_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Server2Client sc = new Server2Client();
            ProductDetails pd = new ProductDetails();
            sc = pd.ProductListBySupplierSimple();

            rptProductBySup rpt = new rptProductBySup() { DataSource = sc.dataTable };

            GroupField grp = new GroupField("SupplierName");

            rpt.groupHeader.GroupFields.Add(grp);

            rpt.lbSUP.DataBindings.Add("Text", null, "SupplierName");
            rpt.lbPNM.DataBindings.Add("Text", null, "ProductName");
            rpt.lbBVL.DataBindings.Add("Text", null, "BuyingValue", "{0:C2}");
            rpt.lbSVL.DataBindings.Add("Text", null, "SellingValue", "{0:C2}");
            rpt.lbQTY.DataBindings.Add("Text", null, "SumOfQuantity");
            rpt.lbBCD.Text = "";
            dv.PrintingSystem = rpt.PrintingSystem;
            rpt.CreateDocument(true);
        }

        private void bPSUPX_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Server2Client sc = new Server2Client();
            ProductDetails pd = new ProductDetails();
            sc = pd.ProductListBySupplierExtended();

            rptProductBySup rpt = new rptProductBySup() { DataSource = sc.dataTable };

            GroupField grp = new GroupField("SupplierName");

            rpt.groupHeader.GroupFields.Add(grp);

            rpt.lbSUP.DataBindings.Add("Text", null, "SupplierName");
            rpt.lbPNM.DataBindings.Add("Text", null, "ProductName");
            rpt.lbBVL.DataBindings.Add("Text", null, "BuyingValue", "{0:C2}");
            rpt.lbSVL.DataBindings.Add("Text", null, "SellingValue", "{0:C2}");
            rpt.lbQTY.DataBindings.Add("Text", null, "Quantity");
            rpt.lbBCD.DataBindings.Add("Text", null, "BarCode");
            dv.PrintingSystem = rpt.PrintingSystem;
            rpt.CreateDocument(true);
        }

        private void bInvoiceNo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmSelect frm = new frmSelect(false);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                Server2Client sc = new Server2Client();
                string dt = null;
                if (frm.RetVal == 0)
                {
                    Sales s = new Sales();
                    sc = s.getProfitLoss(frm.DateOn);
                    dt = "Date " + frm.DateOn.ToShortDateString();
                }
                else
                {
                    Sales s = new Sales();
                    sc = s.getProfitLoss(frm.DateFrom, frm.DateTo);
                    dt = "Between " + frm.DateFrom.ToShortDateString() + " and " + frm.DateTo.ToShortDateString();
                }

                rptPL rpt = new rptPL() { DataSource = sc.dataTable };

                XRSummary gsv = new XRSummary();
                XRSummary gbv = new XRSummary();
                XRSummary gpf = new XRSummary();

                rpt.lbDT.Text = dt;
                rpt.lbSDT.DataBindings.Add("Text", null, "SaleDate", "{0:dd-MM-yyyy}");
                rpt.lbSVL.DataBindings.Add("Text", null, "TotalSellingValue", "{0:C2}");
                rpt.lbBVL.DataBindings.Add("Text", null, "TotalBuyingValue", "{0:C2}");
                rpt.lbPFT.DataBindings.Add("Text", null, "Profit", "{0:C2}");
                rpt.lbGSV.DataBindings.Add("Text", null, "TotalSellingValue", "{0:C2}");
                rpt.lbGBV.DataBindings.Add("Text", null, "TotalBuyingValue", "{0:C2}");
                rpt.lbGPF.DataBindings.Add("Text", null, "Profit", "{0:C2}");

                gsv.FormatString = "{0:C2}";
                gbv.FormatString = "{0:C2}";
                gpf.FormatString = "{0:C2}";


                gsv.Running = SummaryRunning.Report;
                gbv.Running = SummaryRunning.Report;
                gpf.Running = SummaryRunning.Report;

                rpt.lbGSV.Summary = gsv;
                rpt.lbGBV.Summary = gbv;
                rpt.lbGPF.Summary = gpf;

                dv.PrintingSystem = rpt.PrintingSystem;
                rpt.CreateDocument(true);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            int CID = Convert.ToInt32(lueCUS.EditValue);

            Server2Client sc = new Server2Client();
            Sales ss = new Sales();

            sc = ss.SalesToCustomer(CID);

            rptCustomerSales rpt = new rptCustomerSales() { DataSource = sc.dataTable };

            GroupField grp = new GroupField("SaleDate");
            rpt.GroupHeader1.GroupFields.Add(grp);

            rpt.lbCNM.DataBindings.Add("Text", null, "CustomerName");
            rpt.lbADR.DataBindings.Add("Text", null, "Address");
            rpt.lbPHN.DataBindings.Add("Text", null, "Phone");
            rpt.lbSDT.DataBindings.Add("Text", null, "SaleDate", "{0:dd-MM-yyyy}");
            rpt.lbPNM.DataBindings.Add("Text", null, "ProductName");
            rpt.lbSNO.DataBindings.Add("Text", null, "BarCode");
            rpt.lbSVL.DataBindings.Add("Text", null, "SellingValue", "{0:C2}");
            rpt.lbQTY.DataBindings.Add("Text", null, "TotalQuantity");
            rpt.lbAMT.DataBindings.Add("Text", null, "Amount", "{0:C2}");

            dv.PrintingSystem = rpt.PrintingSystem;
            rpt.CreateDocument(true);

            popCustomer.HidePopup();
        }
    }
}

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for rptCustomers
/// </summary>
public class rptCustomers : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private ReportHeaderBand ReportHeader;
    private PageHeaderBand PageHeader;
    private XRLabel xrLabel3;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell4;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTableCell2;
    private XRTableCell xrTableCell5;
    private XRTableCell xrTableCell3;
    private XRTableCell xrTableCell6;
    private XRTable xrTable3;
    private XRTableRow xrTableRow3;
    private XRTableCell xrTableCell8;
    private XRTableCell xrTableCell16;
    public XRTableCell lbCNM;
    private XRTableCell xrTableCell10;
    public XRTableCell lbADR;
    public XRTableCell lbPHN;
    private XRTableCell xrTableCell14;
    public XRTableCell lbEML;
    public XRTableCell lbBAL;
    private XRTableCell xrTableCell7;
    private ReportFooterBand ReportFooter;
    private XRLine xrLine1;
    private XRTable xrTable2;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCell9;
    public XRTableCell lbTBAL;
    private XRTableCell xrTableCell12;
    public XRLabel lbShopName;
    public XRLabel lbAddress;
    public XRLabel lbPhone;
    private XRLine xrLine2;

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public rptCustomers()
    {
        InitializeComponent();
        //
        // TODO: Add constructor logic here
        //
    }

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
            this.lbCNM = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
            this.lbADR = new DevExpress.XtraReports.UI.XRTableCell();
            this.lbPHN = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
            this.lbEML = new DevExpress.XtraReports.UI.XRTableCell();
            this.lbBAL = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrLine1 = new DevExpress.XtraReports.UI.XRLine();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
            this.lbTBAL = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLine2 = new DevExpress.XtraReports.UI.XRLine();
            this.lbShopName = new DevExpress.XtraReports.UI.XRLabel();
            this.lbAddress = new DevExpress.XtraReports.UI.XRLabel();
            this.lbPhone = new DevExpress.XtraReports.UI.XRLabel();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable3});
            this.Detail.HeightF = 25F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrTable3
            // 
            this.xrTable3.Font = new System.Drawing.Font("Tahoma", 10F);
            this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(1.525879E-05F, 0F);
            this.xrTable3.Name = "xrTable3";
            this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
            this.xrTable3.SizeF = new System.Drawing.SizeF(1019F, 25F);
            this.xrTable3.StylePriority.UseFont = false;
            this.xrTable3.StylePriority.UseTextAlignment = false;
            this.xrTable3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell8,
            this.xrTableCell16,
            this.lbCNM,
            this.xrTableCell10,
            this.lbADR,
            this.lbPHN,
            this.xrTableCell14,
            this.lbEML,
            this.lbBAL,
            this.xrTableCell7});
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.Weight = 1D;
            // 
            // xrTableCell8
            // 
            this.xrTableCell8.Name = "xrTableCell8";
            xrSummary1.Func = DevExpress.XtraReports.UI.SummaryFunc.RecordNumber;
            xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrTableCell8.Summary = xrSummary1;
            this.xrTableCell8.Text = "Sl. No";
            this.xrTableCell8.Weight = 0.294617201784096D;
            // 
            // xrTableCell16
            // 
            this.xrTableCell16.Name = "xrTableCell16";
            this.xrTableCell16.Weight = 0.0620237007225998D;
            // 
            // lbCNM
            // 
            this.lbCNM.Name = "lbCNM";
            this.lbCNM.StylePriority.UseTextAlignment = false;
            this.lbCNM.Text = "Product Name";
            this.lbCNM.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.lbCNM.Weight = 0.954626076330247D;
            // 
            // xrTableCell10
            // 
            this.xrTableCell10.Name = "xrTableCell10";
            this.xrTableCell10.StylePriority.UseTextAlignment = false;
            this.xrTableCell10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell10.Weight = 0.0477105530174857D;
            // 
            // lbADR
            // 
            this.lbADR.Name = "lbADR";
            this.lbADR.StylePriority.UseTextAlignment = false;
            this.lbADR.Text = "Buying Value";
            this.lbADR.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.lbADR.Weight = 1.26080168271543D;
            // 
            // lbPHN
            // 
            this.lbPHN.Name = "lbPHN";
            this.lbPHN.StylePriority.UseTextAlignment = false;
            this.lbPHN.Text = "Selling Value";
            this.lbPHN.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.lbPHN.Weight = 0.565105073381995D;
            // 
            // xrTableCell14
            // 
            this.xrTableCell14.Name = "xrTableCell14";
            this.xrTableCell14.Weight = 0.021629909490207D;
            // 
            // lbEML
            // 
            this.lbEML.Name = "lbEML";
            this.lbEML.StylePriority.UseTextAlignment = false;
            this.lbEML.Text = "Quantity";
            this.lbEML.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.lbEML.Weight = 1.08743545655195D;
            // 
            // lbBAL
            // 
            this.lbBAL.Name = "lbBAL";
            this.lbBAL.StylePriority.UseTextAlignment = false;
            this.lbBAL.Text = "lbBAL";
            this.lbBAL.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.lbBAL.Weight = 0.520044935721261D;
            // 
            // xrTableCell7
            // 
            this.xrTableCell7.Name = "xrTableCell7";
            this.xrTableCell7.Weight = 0.0477107460027849D;
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 75F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 75F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lbShopName,
            this.lbAddress,
            this.lbPhone,
            this.xrLine2,
            this.xrLabel3});
            this.ReportHeader.HeightF = 100.5F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // xrLabel3
            // 
            this.xrLabel3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 75.41666F);
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(1019F, 23F);
            this.xrLabel3.StylePriority.UseFont = false;
            this.xrLabel3.StylePriority.UseTextAlignment = false;
            this.xrLabel3.Text = "Customer List";
            this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
            this.PageHeader.HeightF = 25F;
            this.PageHeader.Name = "PageHeader";
            // 
            // xrTable1
            // 
            this.xrTable1.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTable1.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(1019F, 25F);
            this.xrTable1.StylePriority.UseBorders = false;
            this.xrTable1.StylePriority.UseFont = false;
            this.xrTable1.StylePriority.UseTextAlignment = false;
            this.xrTable1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell4,
            this.xrTableCell1,
            this.xrTableCell2,
            this.xrTableCell5,
            this.xrTableCell3,
            this.xrTableCell6});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.Text = "Sl. No";
            this.xrTableCell4.Weight = 0.181798677557696D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.Text = "Customer Name";
            this.xrTableCell1.Weight = 0.627341359413603D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.Text = "Address";
            this.xrTableCell2.Weight = 0.807440176656951D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.Text = "Phone No";
            this.xrTableCell5.Weight = 0.348707856009203D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.Text = "Email";
            this.xrTableCell3.Weight = 0.684367988119648D;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.Text = "Credit Balance";
            this.xrTableCell6.Weight = 0.350343452448799D;
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLine1,
            this.xrTable2});
            this.ReportFooter.HeightF = 100F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // xrLine1
            // 
            this.xrLine1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLine1.Name = "xrLine1";
            this.xrLine1.SizeF = new System.Drawing.SizeF(1019F, 2.083333F);
            // 
            // xrTable2
            // 
            this.xrTable2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 2.083333F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTable2.SizeF = new System.Drawing.SizeF(1019F, 25F);
            this.xrTable2.StylePriority.UseFont = false;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell9,
            this.lbTBAL,
            this.xrTableCell12});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 1D;
            // 
            // xrTableCell9
            // 
            this.xrTableCell9.Name = "xrTableCell9";
            this.xrTableCell9.StylePriority.UseTextAlignment = false;
            this.xrTableCell9.Text = "TOTAL CREDIT BALANCE:";
            this.xrTableCell9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell9.Weight = 4.37660461543545D;
            // 
            // lbTBAL
            // 
            this.lbTBAL.Name = "lbTBAL";
            this.lbTBAL.StylePriority.UseTextAlignment = false;
            this.lbTBAL.Text = "lbTBAL";
            this.lbTBAL.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.lbTBAL.Weight = 0.530055145840313D;
            // 
            // xrTableCell12
            // 
            this.xrTableCell12.Name = "xrTableCell12";
            this.xrTableCell12.Weight = 0.0486288137535046D;
            // 
            // xrLine2
            // 
            this.xrLine2.LocationFloat = new DevExpress.Utils.PointFloat(2.083333F, 98.41666F);
            this.xrLine2.Name = "xrLine2";
            this.xrLine2.SizeF = new System.Drawing.SizeF(1016.917F, 2.083336F);
            // 
            // lbShopName
            // 
            this.lbShopName.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.lbShopName.LocationFloat = new DevExpress.Utils.PointFloat(2.083333F, 0F);
            this.lbShopName.Name = "lbShopName";
            this.lbShopName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lbShopName.SizeF = new System.Drawing.SizeF(1016.917F, 23F);
            this.lbShopName.StylePriority.UseFont = false;
            this.lbShopName.StylePriority.UseTextAlignment = false;
            this.lbShopName.Text = "SR HOME CONCEPT";
            this.lbShopName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lbAddress
            // 
            this.lbAddress.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.lbAddress.LocationFloat = new DevExpress.Utils.PointFloat(2.083333F, 23F);
            this.lbAddress.Name = "lbAddress";
            this.lbAddress.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lbAddress.SizeF = new System.Drawing.SizeF(1016.917F, 23F);
            this.lbAddress.StylePriority.UseFont = false;
            this.lbAddress.StylePriority.UseTextAlignment = false;
            this.lbAddress.Text = "Bazar Veng, Lunglei";
            this.lbAddress.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lbPhone
            // 
            this.lbPhone.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.lbPhone.LocationFloat = new DevExpress.Utils.PointFloat(2.083333F, 46F);
            this.lbPhone.Name = "lbPhone";
            this.lbPhone.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lbPhone.SizeF = new System.Drawing.SizeF(1016.917F, 23F);
            this.lbPhone.StylePriority.UseFont = false;
            this.lbPhone.StylePriority.UseTextAlignment = false;
            this.lbPhone.Text = "lbPhone";
            this.lbPhone.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // rptCustomers
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.PageHeader,
            this.ReportFooter});
            this.DisplayName = "Customers List";
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(75, 75, 75, 75);
            this.PageHeight = 827;
            this.PageWidth = 1169;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Version = "15.1";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion
}

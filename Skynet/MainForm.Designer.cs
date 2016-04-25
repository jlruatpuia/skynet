namespace Skynet
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.skinRibbonGalleryBarItem1 = new DevExpress.XtraBars.SkinRibbonGalleryBarItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.bbNewProduct = new DevExpress.XtraBars.BarButtonItem();
            this.bbExtProduct = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup5 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.rpgSales = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.rpgProduct = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.splt = new DevExpress.XtraEditors.SplitContainerControl();
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.navBarGroup1 = new DevExpress.XtraNavBar.NavBarGroup();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.bbSellProduct = new DevExpress.XtraBars.BarButtonItem();
            this.bbPurchaseProduct = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splt)).BeginInit();
            this.splt.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.skinRibbonGalleryBarItem1,
            this.barButtonItem1,
            this.bbNewProduct,
            this.bbExtProduct,
            this.barButtonItem2,
            this.bbSellProduct,
            this.bbPurchaseProduct});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.MaxItemId = 8;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbonControl1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2010;
            this.ribbonControl1.Size = new System.Drawing.Size(758, 143);
            // 
            // skinRibbonGalleryBarItem1
            // 
            this.skinRibbonGalleryBarItem1.Caption = "skinRibbonGalleryBarItem1";
            this.skinRibbonGalleryBarItem1.Id = 1;
            this.skinRibbonGalleryBarItem1.Name = "skinRibbonGalleryBarItem1";
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "barButtonItem1";
            this.barButtonItem1.Id = 2;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // bbNewProduct
            // 
            this.bbNewProduct.Caption = "Add New";
            this.bbNewProduct.Glyph = ((System.Drawing.Image)(resources.GetObject("bbNewProduct.Glyph")));
            this.bbNewProduct.Id = 3;
            this.bbNewProduct.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("bbNewProduct.LargeGlyph")));
            this.bbNewProduct.Name = "bbNewProduct";
            this.bbNewProduct.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbNewProduct_ItemClick);
            // 
            // bbExtProduct
            // 
            this.bbExtProduct.Caption = "Add Existing";
            this.bbExtProduct.Glyph = ((System.Drawing.Image)(resources.GetObject("bbExtProduct.Glyph")));
            this.bbExtProduct.Id = 4;
            this.bbExtProduct.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("bbExtProduct.LargeGlyph")));
            this.bbExtProduct.Name = "bbExtProduct";
            this.bbExtProduct.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbExtProduct_ItemClick);
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup5,
            this.rpgSales,
            this.rpgProduct,
            this.ribbonPageGroup2,
            this.ribbonPageGroup1});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "Main";
            // 
            // ribbonPageGroup5
            // 
            this.ribbonPageGroup5.ItemLinks.Add(this.barButtonItem2);
            this.ribbonPageGroup5.Name = "ribbonPageGroup5";
            this.ribbonPageGroup5.ShowCaptionButton = false;
            this.ribbonPageGroup5.Text = "Home";
            // 
            // rpgSales
            // 
            this.rpgSales.ItemLinks.Add(this.bbSellProduct);
            this.rpgSales.ItemLinks.Add(this.bbPurchaseProduct);
            this.rpgSales.Name = "rpgSales";
            this.rpgSales.ShowCaptionButton = false;
            this.rpgSales.Text = "Sales";
            // 
            // rpgProduct
            // 
            this.rpgProduct.ItemLinks.Add(this.bbNewProduct);
            this.rpgProduct.ItemLinks.Add(this.bbExtProduct);
            this.rpgProduct.Name = "rpgProduct";
            this.rpgProduct.ShowCaptionButton = false;
            this.rpgProduct.Text = "Products";
            // 
            // ribbonPageGroup2
            // 
            this.ribbonPageGroup2.ItemLinks.Add(this.skinRibbonGalleryBarItem1);
            this.ribbonPageGroup2.Name = "ribbonPageGroup2";
            this.ribbonPageGroup2.ShowCaptionButton = false;
            this.ribbonPageGroup2.Text = "Skins";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.ShowCaptionButton = false;
            this.ribbonPageGroup1.Text = "Settings";
            // 
            // splt
            // 
            this.splt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splt.Location = new System.Drawing.Point(0, 143);
            this.splt.Name = "splt";
            this.splt.Panel1.Controls.Add(this.navBarControl1);
            this.splt.Panel1.Text = "Panel1";
            this.splt.Panel2.Text = "Panel2";
            this.splt.Size = new System.Drawing.Size(758, 372);
            this.splt.SplitterPosition = 234;
            this.splt.TabIndex = 1;
            this.splt.Text = "splitContainerControl1";
            // 
            // navBarControl1
            // 
            this.navBarControl1.ActiveGroup = this.navBarGroup1;
            this.navBarControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.navBarGroup1});
            this.navBarControl1.Location = new System.Drawing.Point(0, 0);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 234;
            this.navBarControl1.PaintStyleKind = DevExpress.XtraNavBar.NavBarViewKind.NavigationPane;
            this.navBarControl1.Size = new System.Drawing.Size(234, 372);
            this.navBarControl1.TabIndex = 0;
            this.navBarControl1.Text = "navBarControl1";
            // 
            // navBarGroup1
            // 
            this.navBarGroup1.Caption = "navBarGroup1";
            this.navBarGroup1.Expanded = true;
            this.navBarGroup1.Name = "navBarGroup1";
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "barButtonItem2";
            this.barButtonItem2.Id = 5;
            this.barButtonItem2.Name = "barButtonItem2";
            // 
            // bbSellProduct
            // 
            this.bbSellProduct.Caption = "&Sell";
            this.bbSellProduct.Glyph = ((System.Drawing.Image)(resources.GetObject("bbSellProduct.Glyph")));
            this.bbSellProduct.Id = 6;
            this.bbSellProduct.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("bbSellProduct.LargeGlyph")));
            this.bbSellProduct.Name = "bbSellProduct";
            this.bbSellProduct.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbSellProduct_ItemClick);
            // 
            // bbPurchaseProduct
            // 
            this.bbPurchaseProduct.Caption = "Purchase";
            this.bbPurchaseProduct.Glyph = ((System.Drawing.Image)(resources.GetObject("bbPurchaseProduct.Glyph")));
            this.bbPurchaseProduct.Id = 7;
            this.bbPurchaseProduct.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("bbPurchaseProduct.LargeGlyph")));
            this.bbPurchaseProduct.Name = "bbPurchaseProduct";
            // 
            // MainForm
            // 
            this.AllowFormGlass = DevExpress.Utils.DefaultBoolean.False;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(758, 515);
            this.Controls.Add(this.splt);
            this.Controls.Add(this.ribbonControl1);
            this.Name = "MainForm";
            this.Ribbon = this.ribbonControl1;
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splt)).EndInit();
            this.splt.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraEditors.SplitContainerControl splt;
        private DevExpress.XtraBars.SkinRibbonGalleryBarItem skinRibbonGalleryBarItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup5;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup rpgSales;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup rpgProduct;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup1;
        private DevExpress.XtraBars.BarButtonItem bbNewProduct;
        private DevExpress.XtraBars.BarButtonItem bbExtProduct;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraBars.BarButtonItem bbSellProduct;
        private DevExpress.XtraBars.BarButtonItem bbPurchaseProduct;
    }
}


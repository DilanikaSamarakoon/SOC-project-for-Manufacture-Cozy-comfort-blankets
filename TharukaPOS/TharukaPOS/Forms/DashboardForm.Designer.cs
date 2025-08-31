namespace TharukaPOS.Forms
{
    partial class DashboardForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DashboardForm));
            this.panelMenu = new System.Windows.Forms.Panel();
            this.iconButtonSuppliers = new FontAwesome.Sharp.IconButton();
            this.iconButtonDealers = new FontAwesome.Sharp.IconButton();
            this.iconButtonInvoices = new FontAwesome.Sharp.IconButton();
            this.iconButtonReports = new FontAwesome.Sharp.IconButton();
            this.iconButtonProducts = new FontAwesome.Sharp.IconButton();
            this.iconButtonStock = new FontAwesome.Sharp.IconButton();
            this.iconButtonSales = new FontAwesome.Sharp.IconButton();
            this.iconButtonSettings = new FontAwesome.Sharp.IconButton();
            this.iconButtonLogout = new FontAwesome.Sharp.IconButton();
            this.iconButtonDashboard = new FontAwesome.Sharp.IconButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblUserRole = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnMenu = new FontAwesome.Sharp.IconButton();
            this.panelTitleBar = new System.Windows.Forms.Panel();
            this.btnMinimize = new FontAwesome.Sharp.IconButton();
            this.btnMaximize = new FontAwesome.Sharp.IconButton();
            this.btnClose = new FontAwesome.Sharp.IconButton();
            this.panelDesktop = new System.Windows.Forms.Panel();
            this.panelMenu.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelTitleBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMenu
            // 
            this.panelMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(28)))), ((int)(((byte)(63)))));
            this.panelMenu.Controls.Add(this.iconButtonSuppliers);
            this.panelMenu.Controls.Add(this.iconButtonDealers);
            this.panelMenu.Controls.Add(this.iconButtonInvoices);
            this.panelMenu.Controls.Add(this.iconButtonReports);
            this.panelMenu.Controls.Add(this.iconButtonProducts);
            this.panelMenu.Controls.Add(this.iconButtonStock);
            this.panelMenu.Controls.Add(this.iconButtonSales);
            this.panelMenu.Controls.Add(this.iconButtonSettings);
            this.panelMenu.Controls.Add(this.iconButtonLogout);
            this.panelMenu.Controls.Add(this.iconButtonDashboard);
            this.panelMenu.Controls.Add(this.panel1);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(224, 853);
            this.panelMenu.TabIndex = 0;
            // 
            // iconButtonSuppliers
            // 
            this.iconButtonSuppliers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(86)))));
            this.iconButtonSuppliers.Dock = System.Windows.Forms.DockStyle.Top;
            this.iconButtonSuppliers.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(83)))), ((int)(((byte)(255)))));
            this.iconButtonSuppliers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButtonSuppliers.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iconButtonSuppliers.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.iconButtonSuppliers.IconChar = FontAwesome.Sharp.IconChar.ArrowsToCircle;
            this.iconButtonSuppliers.IconColor = System.Drawing.Color.WhiteSmoke;
            this.iconButtonSuppliers.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButtonSuppliers.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButtonSuppliers.Location = new System.Drawing.Point(0, 633);
            this.iconButtonSuppliers.Name = "iconButtonSuppliers";
            this.iconButtonSuppliers.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.iconButtonSuppliers.Size = new System.Drawing.Size(224, 65);
            this.iconButtonSuppliers.TabIndex = 10;
            this.iconButtonSuppliers.Tag = "Suppliers";
            this.iconButtonSuppliers.Text = "    Suppliers";
            this.iconButtonSuppliers.UseVisualStyleBackColor = false;
            this.iconButtonSuppliers.Click += new System.EventHandler(this.iconButtonSuppliers_Click);
            // 
            // iconButtonDealers
            // 
            this.iconButtonDealers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(86)))));
            this.iconButtonDealers.Dock = System.Windows.Forms.DockStyle.Top;
            this.iconButtonDealers.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(83)))), ((int)(((byte)(255)))));
            this.iconButtonDealers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButtonDealers.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iconButtonDealers.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.iconButtonDealers.IconChar = FontAwesome.Sharp.IconChar.ArrowsDownToPeople;
            this.iconButtonDealers.IconColor = System.Drawing.Color.WhiteSmoke;
            this.iconButtonDealers.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButtonDealers.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButtonDealers.Location = new System.Drawing.Point(0, 568);
            this.iconButtonDealers.Name = "iconButtonDealers";
            this.iconButtonDealers.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.iconButtonDealers.Size = new System.Drawing.Size(224, 65);
            this.iconButtonDealers.TabIndex = 9;
            this.iconButtonDealers.Tag = "Dealers";
            this.iconButtonDealers.Text = "    Dealers";
            this.iconButtonDealers.UseVisualStyleBackColor = false;
            this.iconButtonDealers.Click += new System.EventHandler(this.iconButtonDealers_Click);
            // 
            // iconButtonInvoices
            // 
            this.iconButtonInvoices.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(86)))));
            this.iconButtonInvoices.Dock = System.Windows.Forms.DockStyle.Top;
            this.iconButtonInvoices.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(83)))), ((int)(((byte)(255)))));
            this.iconButtonInvoices.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButtonInvoices.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iconButtonInvoices.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.iconButtonInvoices.IconChar = FontAwesome.Sharp.IconChar.Receipt;
            this.iconButtonInvoices.IconColor = System.Drawing.Color.WhiteSmoke;
            this.iconButtonInvoices.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButtonInvoices.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButtonInvoices.Location = new System.Drawing.Point(0, 503);
            this.iconButtonInvoices.Name = "iconButtonInvoices";
            this.iconButtonInvoices.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.iconButtonInvoices.Size = new System.Drawing.Size(224, 65);
            this.iconButtonInvoices.TabIndex = 4;
            this.iconButtonInvoices.Tag = "Invoices";
            this.iconButtonInvoices.Text = "    Invoices";
            this.iconButtonInvoices.UseVisualStyleBackColor = false;
            this.iconButtonInvoices.Click += new System.EventHandler(this.iconButtonInvoices_Click);
            // 
            // iconButtonReports
            // 
            this.iconButtonReports.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(86)))));
            this.iconButtonReports.Dock = System.Windows.Forms.DockStyle.Top;
            this.iconButtonReports.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(83)))), ((int)(((byte)(255)))));
            this.iconButtonReports.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButtonReports.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iconButtonReports.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.iconButtonReports.IconChar = FontAwesome.Sharp.IconChar.ChartLine;
            this.iconButtonReports.IconColor = System.Drawing.Color.WhiteSmoke;
            this.iconButtonReports.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButtonReports.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButtonReports.Location = new System.Drawing.Point(0, 438);
            this.iconButtonReports.Name = "iconButtonReports";
            this.iconButtonReports.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.iconButtonReports.Size = new System.Drawing.Size(224, 65);
            this.iconButtonReports.TabIndex = 3;
            this.iconButtonReports.Tag = "Reports";
            this.iconButtonReports.Text = "    Reports";
            this.iconButtonReports.UseVisualStyleBackColor = false;
            this.iconButtonReports.Click += new System.EventHandler(this.iconButtonReports_Click);
            // 
            // iconButtonProducts
            // 
            this.iconButtonProducts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(86)))));
            this.iconButtonProducts.Dock = System.Windows.Forms.DockStyle.Top;
            this.iconButtonProducts.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(83)))), ((int)(((byte)(255)))));
            this.iconButtonProducts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButtonProducts.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iconButtonProducts.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.iconButtonProducts.IconChar = FontAwesome.Sharp.IconChar.BoxesPacking;
            this.iconButtonProducts.IconColor = System.Drawing.Color.WhiteSmoke;
            this.iconButtonProducts.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButtonProducts.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButtonProducts.Location = new System.Drawing.Point(0, 373);
            this.iconButtonProducts.Name = "iconButtonProducts";
            this.iconButtonProducts.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.iconButtonProducts.Size = new System.Drawing.Size(224, 65);
            this.iconButtonProducts.TabIndex = 2;
            this.iconButtonProducts.Tag = "Products";
            this.iconButtonProducts.Text = "    Products";
            this.iconButtonProducts.UseVisualStyleBackColor = false;
            this.iconButtonProducts.Click += new System.EventHandler(this.iconButton4_Click);
            // 
            // iconButtonStock
            // 
            this.iconButtonStock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(86)))));
            this.iconButtonStock.Dock = System.Windows.Forms.DockStyle.Top;
            this.iconButtonStock.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(83)))), ((int)(((byte)(255)))));
            this.iconButtonStock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButtonStock.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iconButtonStock.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.iconButtonStock.IconChar = FontAwesome.Sharp.IconChar.BoxesStacked;
            this.iconButtonStock.IconColor = System.Drawing.Color.WhiteSmoke;
            this.iconButtonStock.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButtonStock.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButtonStock.Location = new System.Drawing.Point(0, 308);
            this.iconButtonStock.Name = "iconButtonStock";
            this.iconButtonStock.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.iconButtonStock.Size = new System.Drawing.Size(224, 65);
            this.iconButtonStock.TabIndex = 1;
            this.iconButtonStock.Tag = "Stock";
            this.iconButtonStock.Text = "    Stock";
            this.iconButtonStock.UseVisualStyleBackColor = false;
            this.iconButtonStock.Click += new System.EventHandler(this.iconButtonStock_Click);
            // 
            // iconButtonSales
            // 
            this.iconButtonSales.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(86)))));
            this.iconButtonSales.Dock = System.Windows.Forms.DockStyle.Top;
            this.iconButtonSales.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(83)))), ((int)(((byte)(255)))));
            this.iconButtonSales.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButtonSales.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iconButtonSales.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.iconButtonSales.IconChar = FontAwesome.Sharp.IconChar.SackDollar;
            this.iconButtonSales.IconColor = System.Drawing.Color.WhiteSmoke;
            this.iconButtonSales.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButtonSales.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButtonSales.Location = new System.Drawing.Point(0, 243);
            this.iconButtonSales.Name = "iconButtonSales";
            this.iconButtonSales.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.iconButtonSales.Size = new System.Drawing.Size(224, 65);
            this.iconButtonSales.TabIndex = 8;
            this.iconButtonSales.Tag = "Sales";
            this.iconButtonSales.Text = "    Sales";
            this.iconButtonSales.UseVisualStyleBackColor = false;
            this.iconButtonSales.UseWaitCursor = true;
            this.iconButtonSales.Click += new System.EventHandler(this.iconButtonSales_Click);
            // 
            // iconButtonSettings
            // 
            this.iconButtonSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(86)))));
            this.iconButtonSettings.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.iconButtonSettings.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(83)))), ((int)(((byte)(255)))));
            this.iconButtonSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButtonSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iconButtonSettings.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.iconButtonSettings.IconChar = FontAwesome.Sharp.IconChar.Gears;
            this.iconButtonSettings.IconColor = System.Drawing.Color.WhiteSmoke;
            this.iconButtonSettings.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButtonSettings.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButtonSettings.Location = new System.Drawing.Point(0, 723);
            this.iconButtonSettings.Name = "iconButtonSettings";
            this.iconButtonSettings.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.iconButtonSettings.Size = new System.Drawing.Size(224, 65);
            this.iconButtonSettings.TabIndex = 7;
            this.iconButtonSettings.Tag = "Settings";
            this.iconButtonSettings.Text = "    Settings";
            this.iconButtonSettings.UseVisualStyleBackColor = false;
            this.iconButtonSettings.Click += new System.EventHandler(this.iconButtonSettings_Click);
            // 
            // iconButtonLogout
            // 
            this.iconButtonLogout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(86)))));
            this.iconButtonLogout.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.iconButtonLogout.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(83)))), ((int)(((byte)(255)))));
            this.iconButtonLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButtonLogout.Flip = FontAwesome.Sharp.FlipOrientation.Horizontal;
            this.iconButtonLogout.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iconButtonLogout.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.iconButtonLogout.IconChar = FontAwesome.Sharp.IconChar.SignOut;
            this.iconButtonLogout.IconColor = System.Drawing.Color.WhiteSmoke;
            this.iconButtonLogout.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButtonLogout.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButtonLogout.Location = new System.Drawing.Point(0, 788);
            this.iconButtonLogout.Name = "iconButtonLogout";
            this.iconButtonLogout.Padding = new System.Windows.Forms.Padding(10, 0, 0, 10);
            this.iconButtonLogout.Size = new System.Drawing.Size(224, 65);
            this.iconButtonLogout.TabIndex = 6;
            this.iconButtonLogout.Tag = "Log Out";
            this.iconButtonLogout.Text = "    Log Out";
            this.iconButtonLogout.UseVisualStyleBackColor = false;
            this.iconButtonLogout.Click += new System.EventHandler(this.iconButtonLogout_Click);
            // 
            // iconButtonDashboard
            // 
            this.iconButtonDashboard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(86)))));
            this.iconButtonDashboard.Dock = System.Windows.Forms.DockStyle.Top;
            this.iconButtonDashboard.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(83)))), ((int)(((byte)(255)))));
            this.iconButtonDashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButtonDashboard.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iconButtonDashboard.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.iconButtonDashboard.IconChar = FontAwesome.Sharp.IconChar.DesktopAlt;
            this.iconButtonDashboard.IconColor = System.Drawing.Color.WhiteSmoke;
            this.iconButtonDashboard.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButtonDashboard.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButtonDashboard.Location = new System.Drawing.Point(0, 178);
            this.iconButtonDashboard.Name = "iconButtonDashboard";
            this.iconButtonDashboard.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.iconButtonDashboard.Size = new System.Drawing.Size(224, 65);
            this.iconButtonDashboard.TabIndex = 0;
            this.iconButtonDashboard.Tag = "Dashboard";
            this.iconButtonDashboard.Text = "    Dashboard";
            this.iconButtonDashboard.UseVisualStyleBackColor = false;
            this.iconButtonDashboard.Click += new System.EventHandler(this.iconButtonDashboard_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblUserRole);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.btnMenu);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(224, 178);
            this.panel1.TabIndex = 0;
            // 
            // lblUserRole
            // 
            this.lblUserRole.AutoSize = true;
            this.lblUserRole.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserRole.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblUserRole.Location = new System.Drawing.Point(23, 143);
            this.lblUserRole.Name = "lblUserRole";
            this.lblUserRole.Size = new System.Drawing.Size(93, 20);
            this.lblUserRole.TabIndex = 0;
            this.lblUserRole.Text = "User Role";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(28)))), ((int)(((byte)(63)))));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(17, 38);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(99, 102);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // btnMenu
            // 
            this.btnMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(86)))));
            this.btnMenu.IconChar = FontAwesome.Sharp.IconChar.BarsStaggered;
            this.btnMenu.IconColor = System.Drawing.Color.Black;
            this.btnMenu.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnMenu.IconSize = 30;
            this.btnMenu.Location = new System.Drawing.Point(162, 3);
            this.btnMenu.Name = "btnMenu";
            this.btnMenu.Size = new System.Drawing.Size(44, 40);
            this.btnMenu.TabIndex = 0;
            this.btnMenu.UseVisualStyleBackColor = false;
            this.btnMenu.Click += new System.EventHandler(this.btnMenu_Click);
            // 
            // panelTitleBar
            // 
            this.panelTitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(28)))), ((int)(((byte)(63)))));
            this.panelTitleBar.Controls.Add(this.btnMinimize);
            this.panelTitleBar.Controls.Add(this.btnMaximize);
            this.panelTitleBar.Controls.Add(this.btnClose);
            this.panelTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitleBar.Location = new System.Drawing.Point(224, 0);
            this.panelTitleBar.Name = "panelTitleBar";
            this.panelTitleBar.Size = new System.Drawing.Size(1058, 38);
            this.panelTitleBar.TabIndex = 1;
            this.panelTitleBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelTitleBar_MouseDown_1);
            // 
            // btnMinimize
            // 
            this.btnMinimize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMinimize.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(28)))), ((int)(((byte)(63)))));
            this.btnMinimize.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.btnMinimize.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.btnMinimize.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnMinimize.IconChar = FontAwesome.Sharp.IconChar.Minus;
            this.btnMinimize.IconColor = System.Drawing.Color.WhiteSmoke;
            this.btnMinimize.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnMinimize.IconSize = 20;
            this.btnMinimize.Location = new System.Drawing.Point(930, 3);
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.Padding = new System.Windows.Forms.Padding(2, 10, 0, 0);
            this.btnMinimize.Size = new System.Drawing.Size(41, 32);
            this.btnMinimize.TabIndex = 3;
            this.btnMinimize.UseVisualStyleBackColor = false;
            this.btnMinimize.Click += new System.EventHandler(this.btnMinimize_Click);
            // 
            // btnMaximize
            // 
            this.btnMaximize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMaximize.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.btnMaximize.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.btnMaximize.IconChar = FontAwesome.Sharp.IconChar.Maximize;
            this.btnMaximize.IconColor = System.Drawing.Color.WhiteSmoke;
            this.btnMaximize.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnMaximize.IconSize = 20;
            this.btnMaximize.Location = new System.Drawing.Point(972, 3);
            this.btnMaximize.Name = "btnMaximize";
            this.btnMaximize.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.btnMaximize.Size = new System.Drawing.Size(41, 32);
            this.btnMaximize.TabIndex = 2;
            this.btnMaximize.UseVisualStyleBackColor = false;
            this.btnMaximize.Click += new System.EventHandler(this.btnMaximize_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.btnClose.ForeColor = System.Drawing.Color.Black;
            this.btnClose.IconChar = FontAwesome.Sharp.IconChar.X;
            this.btnClose.IconColor = System.Drawing.Color.WhiteSmoke;
            this.btnClose.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnClose.IconSize = 20;
            this.btnClose.Location = new System.Drawing.Point(1014, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Padding = new System.Windows.Forms.Padding(2, 3, 0, 0);
            this.btnClose.Size = new System.Drawing.Size(41, 32);
            this.btnClose.TabIndex = 1;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panelDesktop
            // 
            this.panelDesktop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(28)))), ((int)(((byte)(63)))));
            this.panelDesktop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDesktop.Location = new System.Drawing.Point(224, 38);
            this.panelDesktop.Name = "panelDesktop";
            this.panelDesktop.Size = new System.Drawing.Size(1058, 815);
            this.panelDesktop.TabIndex = 2;
            // 
            // DashboardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1282, 853);
            this.Controls.Add(this.panelDesktop);
            this.Controls.Add(this.panelTitleBar);
            this.Controls.Add(this.panelMenu);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1300, 900);
            this.Name = "DashboardForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DashboardForm";
            this.Resize += new System.EventHandler(this.DashboardForm_Resize);
            this.panelMenu.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelTitleBar.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Panel panelTitleBar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelDesktop;
        private FontAwesome.Sharp.IconButton btnMenu;
        private FontAwesome.Sharp.IconButton iconButtonDashboard;
        private FontAwesome.Sharp.IconButton iconButtonLogout;
        private FontAwesome.Sharp.IconButton iconButtonInvoices;
        private FontAwesome.Sharp.IconButton iconButtonReports;
        private FontAwesome.Sharp.IconButton iconButtonProducts;
        private FontAwesome.Sharp.IconButton iconButtonStock;
        private FontAwesome.Sharp.IconButton btnClose;
        private FontAwesome.Sharp.IconButton btnMinimize;
        private FontAwesome.Sharp.IconButton btnMaximize;
        private System.Windows.Forms.PictureBox pictureBox1;
        private FontAwesome.Sharp.IconButton iconButtonSettings;
        private FontAwesome.Sharp.IconButton iconButtonSales;
        private FontAwesome.Sharp.IconButton iconButtonDealers;
        private FontAwesome.Sharp.IconButton iconButtonSuppliers;
        private System.Windows.Forms.Label lblUserRole;
    }
}
namespace Manufacuter.de
{
    partial class Stock_Management
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
            this.grourtd = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtProductionCap = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.dgvStock = new System.Windows.Forms.DataGridView();
            this.btnDecreaseStock = new System.Windows.Forms.Button();
            this.btnIncreaseStock = new System.Windows.Forms.Button();
            this.numStockChange = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbStockBlanket = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.btnBlanketModels = new System.Windows.Forms.Button();
            this.btnProductionOrders = new System.Windows.Forms.Button();
            this.grourtd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStockChange)).BeginInit();
            this.SuspendLayout();
            // 
            // grourtd
            // 
            this.grourtd.BackColor = System.Drawing.Color.White;
            this.grourtd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.grourtd.Controls.Add(this.label3);
            this.grourtd.Controls.Add(this.txtProductionCap);
            this.grourtd.Controls.Add(this.btnAdd);
            this.grourtd.Controls.Add(this.dgvStock);
            this.grourtd.Controls.Add(this.btnDecreaseStock);
            this.grourtd.Controls.Add(this.btnIncreaseStock);
            this.grourtd.Controls.Add(this.numStockChange);
            this.grourtd.Controls.Add(this.label2);
            this.grourtd.Controls.Add(this.cmbStockBlanket);
            this.grourtd.Controls.Add(this.label1);
            this.grourtd.Location = new System.Drawing.Point(12, 25);
            this.grourtd.Name = "grourtd";
            this.grourtd.Size = new System.Drawing.Size(776, 297);
            this.grourtd.TabIndex = 0;
            this.grourtd.TabStop = false;
            this.grourtd.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 123);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(127, 16);
            this.label3.TabIndex = 10;
            this.label3.Text = "Production Capacity";
            // 
            // txtProductionCap
            // 
            this.txtProductionCap.Location = new System.Drawing.Point(142, 120);
            this.txtProductionCap.Multiline = true;
            this.txtProductionCap.Name = "txtProductionCap";
            this.txtProductionCap.Size = new System.Drawing.Size(121, 22);
            this.txtProductionCap.TabIndex = 9;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(102, 158);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(90, 44);
            this.btnAdd.TabIndex = 7;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // dgvStock
            // 
            this.dgvStock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStock.Location = new System.Drawing.Point(289, 21);
            this.dgvStock.Name = "dgvStock";
            this.dgvStock.RowHeadersWidth = 51;
            this.dgvStock.RowTemplate.Height = 24;
            this.dgvStock.Size = new System.Drawing.Size(464, 225);
            this.dgvStock.TabIndex = 6;
            // 
            // btnDecreaseStock
            // 
            this.btnDecreaseStock.Location = new System.Drawing.Point(142, 217);
            this.btnDecreaseStock.Name = "btnDecreaseStock";
            this.btnDecreaseStock.Size = new System.Drawing.Size(123, 43);
            this.btnDecreaseStock.TabIndex = 5;
            this.btnDecreaseStock.Text = "DecreaseStock";
            this.btnDecreaseStock.UseVisualStyleBackColor = true;
            this.btnDecreaseStock.Click += new System.EventHandler(this.btnDecreaseStock_Click);
            // 
            // btnIncreaseStock
            // 
            this.btnIncreaseStock.Location = new System.Drawing.Point(6, 218);
            this.btnIncreaseStock.Name = "btnIncreaseStock";
            this.btnIncreaseStock.Size = new System.Drawing.Size(125, 42);
            this.btnIncreaseStock.TabIndex = 4;
            this.btnIncreaseStock.Text = "IncreaseStock";
            this.btnIncreaseStock.UseVisualStyleBackColor = true;
            this.btnIncreaseStock.Click += new System.EventHandler(this.btnIncreaseStock_Click);
            // 
            // numStockChange
            // 
            this.numStockChange.DecimalPlaces = 2;
            this.numStockChange.Location = new System.Drawing.Point(143, 82);
            this.numStockChange.Name = "numStockChange";
            this.numStockChange.Size = new System.Drawing.Size(120, 22);
            this.numStockChange.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Quantity Change:";
            // 
            // cmbStockBlanket
            // 
            this.cmbStockBlanket.FormattingEnabled = true;
            this.cmbStockBlanket.Location = new System.Drawing.Point(142, 44);
            this.cmbStockBlanket.Name = "cmbStockBlanket";
            this.cmbStockBlanket.Size = new System.Drawing.Size(121, 24);
            this.cmbStockBlanket.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select Blanket:";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.DisabledLinkColor = System.Drawing.Color.White;
            this.linkLabel1.Location = new System.Drawing.Point(46, 325);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(75, 16);
            this.linkLabel1.TabIndex = 1;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Quick Links";
            this.linkLabel1.VisitedLinkColor = System.Drawing.Color.Transparent;
            // 
            // btnBlanketModels
            // 
            this.btnBlanketModels.Location = new System.Drawing.Point(49, 364);
            this.btnBlanketModels.Name = "btnBlanketModels";
            this.btnBlanketModels.Size = new System.Drawing.Size(105, 54);
            this.btnBlanketModels.TabIndex = 2;
            this.btnBlanketModels.Text = "Blanket Models";
            this.btnBlanketModels.UseVisualStyleBackColor = true;
            this.btnBlanketModels.Click += new System.EventHandler(this.btnBlanketModels_Click);
            // 
            // btnProductionOrders
            // 
            this.btnProductionOrders.Location = new System.Drawing.Point(194, 364);
            this.btnProductionOrders.Name = "btnProductionOrders";
            this.btnProductionOrders.Size = new System.Drawing.Size(114, 54);
            this.btnProductionOrders.TabIndex = 3;
            this.btnProductionOrders.Text = "Production Orders";
            this.btnProductionOrders.UseVisualStyleBackColor = true;
            this.btnProductionOrders.Click += new System.EventHandler(this.btnProductionOrders_Click_1);
            // 
            // Stock_Management
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PaleVioletRed;
            this.BackgroundImage = global::Manufacuter.de.Properties.Resources.successful_employees;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnProductionOrders);
            this.Controls.Add(this.btnBlanketModels);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.grourtd);
            this.DoubleBuffered = true;
            this.Name = "Stock_Management";
            this.Text = "Stock_Management";
            this.Load += new System.EventHandler(this.Stock_Management_Load);
            this.grourtd.ResumeLayout(false);
            this.grourtd.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStockChange)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grourtd;
        private System.Windows.Forms.ComboBox cmbStockBlanket;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvStock;
        private System.Windows.Forms.Button btnDecreaseStock;
        private System.Windows.Forms.Button btnIncreaseStock;
        private System.Windows.Forms.NumericUpDown numStockChange;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Button btnBlanketModels;
        private System.Windows.Forms.Button btnProductionOrders;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox txtProductionCap;
        private System.Windows.Forms.Label label3;
    }
}
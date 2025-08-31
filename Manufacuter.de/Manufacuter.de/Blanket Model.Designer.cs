namespace Manufacuter.de
{
    partial class BlanketModelsForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.blanketsDataGridView = new System.Windows.Forms.DataGridView();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.priceNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.lblPrice = new System.Windows.Forms.Label();
            this.txtMaterial = new System.Windows.Forms.TextBox();
            this.materialTextBox = new System.Windows.Forms.Label();
            this.txtModelName = new System.Windows.Forms.TextBox();
            this.modelNameTextBox = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.btnStockManagement = new System.Windows.Forms.Button();
            this.btnProductionOrders = new System.Windows.Forms.Button();
            this.btnManageProducts = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.blanketsDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.priceNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnDelete);
            this.groupBox1.Controls.Add(this.blanketsDataGridView);
            this.groupBox1.Controls.Add(this.btnClear);
            this.groupBox1.Controls.Add(this.btnUpdate);
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Controls.Add(this.priceNumericUpDown);
            this.groupBox1.Controls.Add(this.lblPrice);
            this.groupBox1.Controls.Add(this.txtMaterial);
            this.groupBox1.Controls.Add(this.materialTextBox);
            this.groupBox1.Controls.Add(this.txtModelName);
            this.groupBox1.Controls.Add(this.modelNameTextBox);
            this.groupBox1.Location = new System.Drawing.Point(27, 23);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(765, 283);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Add/Edit Blanket Model";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 0;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(144, 237);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(86, 29);
            this.btnDelete.TabIndex = 10;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // blanketsDataGridView
            // 
            this.blanketsDataGridView.AllowUserToAddRows = false;
            this.blanketsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.blanketsDataGridView.Location = new System.Drawing.Point(371, 21);
            this.blanketsDataGridView.Name = "blanketsDataGridView";
            this.blanketsDataGridView.RowHeadersWidth = 51;
            this.blanketsDataGridView.RowTemplate.Height = 24;
            this.blanketsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.blanketsDataGridView.Size = new System.Drawing.Size(388, 234);
            this.blanketsDataGridView.TabIndex = 9;
            this.blanketsDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.blanketsDataGridView_CellContentClick);
            this.blanketsDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.blanketsDataGridView_CellContentClick);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(265, 192);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(100, 23);
            this.btnClear.TabIndex = 8;
            this.btnClear.Text = "Clear Fields";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(144, 192);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(86, 23);
            this.btnUpdate.TabIndex = 7;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.UpdateButton_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(14, 192);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 6;
            this.btnAdd.Text = "Add New";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // priceNumericUpDown
            // 
            this.priceNumericUpDown.DecimalPlaces = 2;
            this.priceNumericUpDown.Location = new System.Drawing.Point(135, 134);
            this.priceNumericUpDown.Name = "priceNumericUpDown";
            this.priceNumericUpDown.Size = new System.Drawing.Size(165, 22);
            this.priceNumericUpDown.TabIndex = 5;
            // 
            // lblPrice
            // 
            this.lblPrice.AutoSize = true;
            this.lblPrice.Location = new System.Drawing.Point(34, 134);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(41, 16);
            this.lblPrice.TabIndex = 4;
            this.lblPrice.Text = "Price:";
            // 
            // txtMaterial
            // 
            this.txtMaterial.Location = new System.Drawing.Point(135, 88);
            this.txtMaterial.Name = "txtMaterial";
            this.txtMaterial.Size = new System.Drawing.Size(165, 22);
            this.txtMaterial.TabIndex = 3;
            // 
            // materialTextBox
            // 
            this.materialTextBox.AutoSize = true;
            this.materialTextBox.Location = new System.Drawing.Point(31, 91);
            this.materialTextBox.Name = "materialTextBox";
            this.materialTextBox.Size = new System.Drawing.Size(58, 16);
            this.materialTextBox.TabIndex = 2;
            this.materialTextBox.Text = "Material:";
            // 
            // txtModelName
            // 
            this.txtModelName.Location = new System.Drawing.Point(135, 46);
            this.txtModelName.Name = "txtModelName";
            this.txtModelName.Size = new System.Drawing.Size(165, 22);
            this.txtModelName.TabIndex = 1;
            // 
            // modelNameTextBox
            // 
            this.modelNameTextBox.AutoSize = true;
            this.modelNameTextBox.Location = new System.Drawing.Point(19, 46);
            this.modelNameTextBox.Name = "modelNameTextBox";
            this.modelNameTextBox.Size = new System.Drawing.Size(88, 16);
            this.modelNameTextBox.TabIndex = 0;
            this.modelNameTextBox.Text = "Model Name:";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(49, 332);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(71, 16);
            this.linkLabel1.TabIndex = 1;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Quick links";
            // 
            // btnStockManagement
            // 
            this.btnStockManagement.Location = new System.Drawing.Point(27, 363);
            this.btnStockManagement.Name = "btnStockManagement";
            this.btnStockManagement.Size = new System.Drawing.Size(176, 65);
            this.btnStockManagement.TabIndex = 3;
            this.btnStockManagement.Text = "Stock Management";
            this.btnStockManagement.UseVisualStyleBackColor = true;
            this.btnStockManagement.Click += new System.EventHandler(this.btClickStockManagement);
            // 
            // btnProductionOrders
            // 
            this.btnProductionOrders.Location = new System.Drawing.Point(243, 363);
            this.btnProductionOrders.Name = "btnProductionOrders";
            this.btnProductionOrders.Size = new System.Drawing.Size(138, 65);
            this.btnProductionOrders.TabIndex = 4;
            this.btnProductionOrders.Text = "Production Orders";
            this.btnProductionOrders.UseVisualStyleBackColor = true;
            this.btnProductionOrders.Click += new System.EventHandler(this.btClickProductionOrders);
            // 
            // btnManageProducts
            // 
            this.btnManageProducts.Location = new System.Drawing.Point(444, 363);
            this.btnManageProducts.Name = "btnManageProducts";
            this.btnManageProducts.Size = new System.Drawing.Size(123, 65);
            this.btnManageProducts.TabIndex = 5;
            this.btnManageProducts.Text = "Manage Products";
            this.btnManageProducts.UseVisualStyleBackColor = true;
            this.btnManageProducts.Click += new System.EventHandler(this.btClickManageProducts);
            // 
            // BlanketModelsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::Manufacuter.de.Properties.Resources.successful_employees;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnManageProducts);
            this.Controls.Add(this.btnProductionOrders);
            this.Controls.Add(this.btnStockManagement);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.Name = "BlanketModelsForm";
            this.Text = "Blanket Models";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.blanketsDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.priceNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown priceNumericUpDown;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.TextBox txtMaterial;
        private System.Windows.Forms.Label materialTextBox;
        private System.Windows.Forms.TextBox txtModelName;
        private System.Windows.Forms.Label modelNameTextBox;
        private System.Windows.Forms.DataGridView blanketsDataGridView;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Button btnStockManagement;
        private System.Windows.Forms.Button btnProductionOrders;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnManageProducts;
    }
}
namespace Manufacuter.de
{
    // FIX: Added ": Form" to ensure both partial class files match
    partial class Production_Orders
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
            this.btnRefreshOrders = new System.Windows.Forms.Button();
            this.dgvOrders = new System.Windows.Forms.DataGridView();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.btnBlanketModels = new System.Windows.Forms.Button();
            this.btnStockmanagement = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBlanketId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.numQuantity = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.btnCreateOrder = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity)).BeginInit();
            this.SuspendLayout();
            // 
            // btnRefreshOrders
            // 
            this.btnRefreshOrders.Location = new System.Drawing.Point(434, 12);
            this.btnRefreshOrders.Name = "btnRefreshOrders";
            this.btnRefreshOrders.Size = new System.Drawing.Size(75, 23);
            this.btnRefreshOrders.TabIndex = 0;
            this.btnRefreshOrders.Text = "Refresh";
            this.btnRefreshOrders.UseVisualStyleBackColor = true;
            this.btnRefreshOrders.Click += new System.EventHandler(this.btnRefreshOrders_Click);
            // 
            // dgvOrders
            // 
            this.dgvOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrders.Location = new System.Drawing.Point(421, 65);
            this.dgvOrders.Name = "dgvOrders";
            this.dgvOrders.RowHeadersWidth = 51;
            this.dgvOrders.RowTemplate.Height = 24;
            this.dgvOrders.Size = new System.Drawing.Size(367, 287);
            this.dgvOrders.TabIndex = 1;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(63, 314);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(75, 16);
            this.linkLabel1.TabIndex = 2;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Quick Links";
            // 
            // btnBlanketModels
            // 
            this.btnBlanketModels.Location = new System.Drawing.Point(42, 359);
            this.btnBlanketModels.Name = "btnBlanketModels";
            this.btnBlanketModels.Size = new System.Drawing.Size(123, 47);
            this.btnBlanketModels.TabIndex = 3;
            this.btnBlanketModels.Text = "Blanket Models";
            this.btnBlanketModels.UseVisualStyleBackColor = true;
            this.btnBlanketModels.Click += new System.EventHandler(this.btClickBlanketModels);
            // 
            // btnStockmanagement
            // 
            this.btnStockmanagement.Location = new System.Drawing.Point(195, 362);
            this.btnStockmanagement.Name = "btnStockmanagement";
            this.btnStockmanagement.Size = new System.Drawing.Size(126, 44);
            this.btnStockmanagement.TabIndex = 4;
            this.btnStockmanagement.Text = "Stock management";
            this.btnStockmanagement.UseVisualStyleBackColor = true;
            this.btnStockmanagement.Click += new System.EventHandler(this.btClickStockmanagement);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "Blanket ID:";
            // 
            // txtBlanketId
            // 
            this.txtBlanketId.Location = new System.Drawing.Point(129, 47);
            this.txtBlanketId.Name = "txtBlanketId";
            this.txtBlanketId.Size = new System.Drawing.Size(100, 22);
            this.txtBlanketId.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(45, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "Quantity:";
            // 
            // numQuantity
            // 
            this.numQuantity.Location = new System.Drawing.Point(129, 97);
            this.numQuantity.Name = "numQuantity";
            this.numQuantity.Size = new System.Drawing.Size(120, 22);
            this.numQuantity.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(48, 149);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 16);
            this.label3.TabIndex = 9;
            this.label3.Text = "Status:";
            // 
            // txtStatus
            // 
            this.txtStatus.Location = new System.Drawing.Point(129, 149);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(100, 22);
            this.txtStatus.TabIndex = 10;
            // 
            // btnCreateOrder
            // 
            this.btnCreateOrder.Location = new System.Drawing.Point(79, 187);
            this.btnCreateOrder.Name = "btnCreateOrder";
            this.btnCreateOrder.Size = new System.Drawing.Size(170, 50);
            this.btnCreateOrder.TabIndex = 11;
            this.btnCreateOrder.Text = "Create Production Order";
            this.btnCreateOrder.UseVisualStyleBackColor = true;
            this.btnCreateOrder.Click += new System.EventHandler(this.btnCreateOrder_Click);
            // 
            // Production_Orders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Manufacuter.de.Properties.Resources.successful_employees;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnCreateOrder);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numQuantity);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtBlanketId);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnStockmanagement);
            this.Controls.Add(this.btnBlanketModels);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.dgvOrders);
            this.Controls.Add(this.btnRefreshOrders);
            this.Name = "Production_Orders";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Production_Orders";
            this.Load += new System.EventHandler(this.Production_Orders_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRefreshOrders;
        private System.Windows.Forms.DataGridView dgvOrders;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Button btnBlanketModels;
        private System.Windows.Forms.Button btnStockmanagement;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBlanketId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numQuantity;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.Button btnCreateOrder;
    }
}

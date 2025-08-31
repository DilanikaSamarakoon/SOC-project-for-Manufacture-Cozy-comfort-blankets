using System.Windows.Forms;

namespace DistributorDashboard
{
    partial class Manufacturer_Interaction
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private GroupBox grpCheckStock;
        private Label lblCheckStockBlanketId;
        private TextBox txtCheckStockBlanketId;
        private Button btnCheckManufacturer;
        private DataGridView dgvStockCheck;

        private GroupBox grpPlaceOrder;
        private Label lblPlaceOrderBlanketId;
        private TextBox txtPlaceOrderBlanketId;
        private Label lblQuantity;
        private NumericUpDown numQuantity;
        private Button btnPlaceOrder;

        private Button btnMyInventory;
        private Button btnSellerOrders;

        private void InitializeComponent()
        {
            this.grpCheckStock = new System.Windows.Forms.GroupBox();
            this.dgvStockCheck = new System.Windows.Forms.DataGridView();
            this.btnCheckManufacturer = new System.Windows.Forms.Button();
            this.txtCheckStockBlanketId = new System.Windows.Forms.TextBox();
            this.lblCheckStockBlanketId = new System.Windows.Forms.Label();
            this.grpPlaceOrder = new System.Windows.Forms.GroupBox();
            this.btnPlaceOrder = new System.Windows.Forms.Button();
            this.numQuantity = new System.Windows.Forms.NumericUpDown();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.txtPlaceOrderBlanketId = new System.Windows.Forms.TextBox();
            this.lblPlaceOrderBlanketId = new System.Windows.Forms.Label();
            this.btnMyInventory = new System.Windows.Forms.Button();
            this.btnSellerOrders = new System.Windows.Forms.Button();
            this.grpCheckStock.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStockCheck)).BeginInit();
            this.grpPlaceOrder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity)).BeginInit();
            this.SuspendLayout();
            // 
            // grpCheckStock
            // 
            this.grpCheckStock.Controls.Add(this.dgvStockCheck);
            this.grpCheckStock.Controls.Add(this.btnCheckManufacturer);
            this.grpCheckStock.Controls.Add(this.txtCheckStockBlanketId);
            this.grpCheckStock.Controls.Add(this.lblCheckStockBlanketId);
            this.grpCheckStock.Location = new System.Drawing.Point(12, 12);
            this.grpCheckStock.Name = "grpCheckStock";
            this.grpCheckStock.Size = new System.Drawing.Size(250, 280);
            this.grpCheckStock.TabIndex = 0;
            this.grpCheckStock.TabStop = false;
            this.grpCheckStock.Text = "Check Orders";
            // 
            // dgvStockCheck
            // 
            this.dgvStockCheck.BackgroundColor = System.Drawing.SystemColors.ControlDark;
            this.dgvStockCheck.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStockCheck.Location = new System.Drawing.Point(9, 90);
            this.dgvStockCheck.Name = "dgvStockCheck";
            this.dgvStockCheck.RowHeadersWidth = 51;
            this.dgvStockCheck.RowTemplate.Height = 24;
            this.dgvStockCheck.Size = new System.Drawing.Size(226, 180);
            this.dgvStockCheck.TabIndex = 3;
            // 
            // btnCheckManufacturer
            // 
            this.btnCheckManufacturer.Location = new System.Drawing.Point(85, 55);
            this.btnCheckManufacturer.Name = "btnCheckManufacturer";
            this.btnCheckManufacturer.Size = new System.Drawing.Size(150, 28);
            this.btnCheckManufacturer.TabIndex = 2;
            this.btnCheckManufacturer.Text = "Check Order\r\n";
            this.btnCheckManufacturer.UseVisualStyleBackColor = true;
            this.btnCheckManufacturer.Click += new System.EventHandler(this.btnCheckManufacturer_Click);
            // 
            // txtCheckStockBlanketId
            // 
            this.txtCheckStockBlanketId.Location = new System.Drawing.Point(85, 27);
            this.txtCheckStockBlanketId.Name = "txtCheckStockBlanketId";
            this.txtCheckStockBlanketId.Size = new System.Drawing.Size(150, 22);
            this.txtCheckStockBlanketId.TabIndex = 1;
            // 
            // lblCheckStockBlanketId
            // 
            this.lblCheckStockBlanketId.AutoSize = true;
            this.lblCheckStockBlanketId.Location = new System.Drawing.Point(6, 30);
            this.lblCheckStockBlanketId.Name = "lblCheckStockBlanketId";
            this.lblCheckStockBlanketId.Size = new System.Drawing.Size(71, 16);
            this.lblCheckStockBlanketId.TabIndex = 0;
            this.lblCheckStockBlanketId.Text = "Blanket ID:";
            // 
            // grpPlaceOrder
            // 
            this.grpPlaceOrder.Controls.Add(this.btnPlaceOrder);
            this.grpPlaceOrder.Controls.Add(this.numQuantity);
            this.grpPlaceOrder.Controls.Add(this.lblQuantity);
            this.grpPlaceOrder.Controls.Add(this.txtPlaceOrderBlanketId);
            this.grpPlaceOrder.Controls.Add(this.lblPlaceOrderBlanketId);
            this.grpPlaceOrder.Location = new System.Drawing.Point(280, 12);
            this.grpPlaceOrder.Name = "grpPlaceOrder";
            this.grpPlaceOrder.Size = new System.Drawing.Size(250, 150);
            this.grpPlaceOrder.TabIndex = 1;
            this.grpPlaceOrder.TabStop = false;
            this.grpPlaceOrder.Text = "Place order ";
            // 
            // btnPlaceOrder
            // 
            this.btnPlaceOrder.Location = new System.Drawing.Point(85, 90);
            this.btnPlaceOrder.Name = "btnPlaceOrder";
            this.btnPlaceOrder.Size = new System.Drawing.Size(150, 28);
            this.btnPlaceOrder.TabIndex = 4;
            this.btnPlaceOrder.Text = "Place Order";
            this.btnPlaceOrder.UseVisualStyleBackColor = true;
            this.btnPlaceOrder.Click += new System.EventHandler(this.btnPlaceOrder_Click);
            // 
            // numQuantity
            // 
            this.numQuantity.Location = new System.Drawing.Point(85, 56);
            this.numQuantity.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numQuantity.Name = "numQuantity";
            this.numQuantity.Size = new System.Drawing.Size(150, 22);
            this.numQuantity.TabIndex = 3;
            // 
            // lblQuantity
            // 
            this.lblQuantity.AutoSize = true;
            this.lblQuantity.Location = new System.Drawing.Point(6, 58);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(58, 16);
            this.lblQuantity.TabIndex = 2;
            this.lblQuantity.Text = "Quantity:";
            // 
            // txtPlaceOrderBlanketId
            // 
            this.txtPlaceOrderBlanketId.Location = new System.Drawing.Point(85, 27);
            this.txtPlaceOrderBlanketId.Name = "txtPlaceOrderBlanketId";
            this.txtPlaceOrderBlanketId.Size = new System.Drawing.Size(150, 22);
            this.txtPlaceOrderBlanketId.TabIndex = 1;
            // 
            // lblPlaceOrderBlanketId
            // 
            this.lblPlaceOrderBlanketId.AutoSize = true;
            this.lblPlaceOrderBlanketId.Location = new System.Drawing.Point(6, 30);
            this.lblPlaceOrderBlanketId.Name = "lblPlaceOrderBlanketId";
            this.lblPlaceOrderBlanketId.Size = new System.Drawing.Size(71, 16);
            this.lblPlaceOrderBlanketId.TabIndex = 0;
            this.lblPlaceOrderBlanketId.Text = "Blanket ID:";
            // 
            // btnMyInventory
            // 
            this.btnMyInventory.Location = new System.Drawing.Point(280, 190);
            this.btnMyInventory.Name = "btnMyInventory";
            this.btnMyInventory.Size = new System.Drawing.Size(120, 30);
            this.btnMyInventory.TabIndex = 2;
            this.btnMyInventory.Text = "My Inventory";
            this.btnMyInventory.UseVisualStyleBackColor = true;
            this.btnMyInventory.Click += new System.EventHandler(this.btnMyInventory_Click);
            // 
            // btnSellerOrders
            // 
            this.btnSellerOrders.Location = new System.Drawing.Point(280, 230);
            this.btnSellerOrders.Name = "btnSellerOrders";
            this.btnSellerOrders.Size = new System.Drawing.Size(120, 30);
            this.btnSellerOrders.TabIndex = 3;
            this.btnSellerOrders.Text = "Seller Orders";
            this.btnSellerOrders.UseVisualStyleBackColor = true;
            this.btnSellerOrders.Click += new System.EventHandler(this.btnSellerOrders_Click);
            // 
            // Manufacturer_Interaction
            // 
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::Distributor.de.Properties.Resources.dis;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(544, 304);
            this.Controls.Add(this.btnSellerOrders);
            this.Controls.Add(this.btnMyInventory);
            this.Controls.Add(this.grpPlaceOrder);
            this.Controls.Add(this.grpCheckStock);
            this.Name = "Manufacturer_Interaction";
            this.Text = "Manufacturer Interaction";
            this.Load += new System.EventHandler(this.Manufacturer_Interaction_Load);
            this.grpCheckStock.ResumeLayout(false);
            this.grpCheckStock.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStockCheck)).EndInit();
            this.grpPlaceOrder.ResumeLayout(false);
            this.grpPlaceOrder.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
    }
}

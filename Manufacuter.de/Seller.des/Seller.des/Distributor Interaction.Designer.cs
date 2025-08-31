using System.Windows.Forms;

namespace SellerDashboard
{
    partial class Distributor_Interaction
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

        // All your controls are declared as fields here, making them accessible
        // to the Distributor_Interaction.cs logic file.
        private GroupBox grpCheckStock;
        private Label lblCheckStockBlanketId;
        private TextBox txtCheckStockBlanketId;
        private Button btnCheckStock;
        private DataGridView dgvCheckStockResult;

        private GroupBox grpPlaceOrder;
        private Label lblPlaceOrderBlanketId;
        private TextBox txtPlaceOrderBlanketId;
        private Label lblQuantity;
        private NumericUpDown numQuantity;
        private Button btnPlaceOrder;

        private GroupBox grpOrdersHistory;
        private DataGridView dgvOrdersHistory;

        private void InitializeComponent()
        {
            this.grpCheckStock = new System.Windows.Forms.GroupBox();
            this.dgvCheckStockResult = new System.Windows.Forms.DataGridView();
            this.btnCheckStock = new System.Windows.Forms.Button();
            this.txtCheckStockBlanketId = new System.Windows.Forms.TextBox();
            this.lblCheckStockBlanketId = new System.Windows.Forms.Label();
            this.grpPlaceOrder = new System.Windows.Forms.GroupBox();
            this.btnPlaceOrder = new System.Windows.Forms.Button();
            this.numQuantity = new System.Windows.Forms.NumericUpDown();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.txtPlaceOrderBlanketId = new System.Windows.Forms.TextBox();
            this.lblPlaceOrderBlanketId = new System.Windows.Forms.Label();
            this.grpOrdersHistory = new System.Windows.Forms.GroupBox();
            this.dgvOrdersHistory = new System.Windows.Forms.DataGridView();
            this.btnNewOrderForm = new System.Windows.Forms.Button();
            this.btnMyStock = new System.Windows.Forms.Button();
            this.grpCheckStock.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCheckStockResult)).BeginInit();
            this.grpPlaceOrder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity)).BeginInit();
            this.grpOrdersHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrdersHistory)).BeginInit();
            this.SuspendLayout();
            // 
            // grpCheckStock
            // 
            this.grpCheckStock.Controls.Add(this.dgvCheckStockResult);
            this.grpCheckStock.Controls.Add(this.btnCheckStock);
            this.grpCheckStock.Controls.Add(this.txtCheckStockBlanketId);
            this.grpCheckStock.Controls.Add(this.lblCheckStockBlanketId);
            this.grpCheckStock.Location = new System.Drawing.Point(12, 12);
            this.grpCheckStock.Name = "grpCheckStock";
            this.grpCheckStock.Size = new System.Drawing.Size(250, 200);
            this.grpCheckStock.TabIndex = 3;
            this.grpCheckStock.TabStop = false;
            this.grpCheckStock.Text = "Check Distributor Stock";
            // 
            // dgvCheckStockResult
            // 
            this.dgvCheckStockResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCheckStockResult.Location = new System.Drawing.Point(9, 89);
            this.dgvCheckStockResult.Name = "dgvCheckStockResult";
            this.dgvCheckStockResult.RowHeadersWidth = 51;
            this.dgvCheckStockResult.Size = new System.Drawing.Size(235, 100);
            this.dgvCheckStockResult.TabIndex = 0;
            // 
            // btnCheckStock
            // 
            this.btnCheckStock.Location = new System.Drawing.Point(124, 55);
            this.btnCheckStock.Name = "btnCheckStock";
            this.btnCheckStock.Size = new System.Drawing.Size(120, 28);
            this.btnCheckStock.TabIndex = 1;
            this.btnCheckStock.Text = "Check Stock";
            this.btnCheckStock.Click += new System.EventHandler(this.BtnCheckStock_Click);
            // 
            // txtCheckStockBlanketId
            // 
            this.txtCheckStockBlanketId.Location = new System.Drawing.Point(98, 27);
            this.txtCheckStockBlanketId.Name = "txtCheckStockBlanketId";
            this.txtCheckStockBlanketId.Size = new System.Drawing.Size(146, 22);
            this.txtCheckStockBlanketId.TabIndex = 2;
            // 
            // lblCheckStockBlanketId
            // 
            this.lblCheckStockBlanketId.AutoSize = true;
            this.lblCheckStockBlanketId.Location = new System.Drawing.Point(6, 30);
            this.lblCheckStockBlanketId.Name = "lblCheckStockBlanketId";
            this.lblCheckStockBlanketId.Size = new System.Drawing.Size(71, 16);
            this.lblCheckStockBlanketId.TabIndex = 3;
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
            this.grpPlaceOrder.Size = new System.Drawing.Size(260, 142);
            this.grpPlaceOrder.TabIndex = 2;
            this.grpPlaceOrder.TabStop = false;
            this.grpPlaceOrder.Text = "Place Order With Distributor";
            // 
            // btnPlaceOrder
            // 
            this.btnPlaceOrder.Location = new System.Drawing.Point(134, 89);
            this.btnPlaceOrder.Name = "btnPlaceOrder";
            this.btnPlaceOrder.Size = new System.Drawing.Size(120, 28);
            this.btnPlaceOrder.TabIndex = 0;
            this.btnPlaceOrder.Text = "Place Order";
            this.btnPlaceOrder.Click += new System.EventHandler(this.BtnPlaceOrder_Click);
            // 
            // numQuantity
            // 
            this.numQuantity.Location = new System.Drawing.Point(108, 56);
            this.numQuantity.Name = "numQuantity";
            this.numQuantity.Size = new System.Drawing.Size(146, 22);
            this.numQuantity.TabIndex = 1;
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
            this.txtPlaceOrderBlanketId.Location = new System.Drawing.Point(108, 27);
            this.txtPlaceOrderBlanketId.Name = "txtPlaceOrderBlanketId";
            this.txtPlaceOrderBlanketId.Size = new System.Drawing.Size(146, 22);
            this.txtPlaceOrderBlanketId.TabIndex = 3;
            // 
            // lblPlaceOrderBlanketId
            // 
            this.lblPlaceOrderBlanketId.AutoSize = true;
            this.lblPlaceOrderBlanketId.Location = new System.Drawing.Point(6, 30);
            this.lblPlaceOrderBlanketId.Name = "lblPlaceOrderBlanketId";
            this.lblPlaceOrderBlanketId.Size = new System.Drawing.Size(71, 16);
            this.lblPlaceOrderBlanketId.TabIndex = 4;
            this.lblPlaceOrderBlanketId.Text = "Blanket ID:";
            // 
            // grpOrdersHistory
            // 
            this.grpOrdersHistory.Controls.Add(this.dgvOrdersHistory);
            this.grpOrdersHistory.Location = new System.Drawing.Point(12, 220);
            this.grpOrdersHistory.Name = "grpOrdersHistory";
            this.grpOrdersHistory.Size = new System.Drawing.Size(354, 200);
            this.grpOrdersHistory.TabIndex = 1;
            this.grpOrdersHistory.TabStop = false;
            this.grpOrdersHistory.Text = "My Orders History";
            // 
            // dgvOrdersHistory
            // 
            this.dgvOrdersHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrdersHistory.Location = new System.Drawing.Point(9, 21);
            this.dgvOrdersHistory.Name = "dgvOrdersHistory";
            this.dgvOrdersHistory.RowHeadersWidth = 51;
            this.dgvOrdersHistory.Size = new System.Drawing.Size(303, 179);
            this.dgvOrdersHistory.TabIndex = 0;
            // 
            // btnNewOrderForm
            // 
            this.btnNewOrderForm.Location = new System.Drawing.Point(388, 309);
            this.btnNewOrderForm.Name = "btnNewOrderForm";
            this.btnNewOrderForm.Size = new System.Drawing.Size(120, 32);
            this.btnNewOrderForm.TabIndex = 4;
            this.btnNewOrderForm.Text = "New Order Form";
            this.btnNewOrderForm.UseVisualStyleBackColor = true;
            this.btnNewOrderForm.Click += new System.EventHandler(this.btClickbtnNewOrderForm);
            // 
            // btnMyStock
            // 
            this.btnMyStock.Location = new System.Drawing.Point(388, 256);
            this.btnMyStock.Name = "btnMyStock";
            this.btnMyStock.Size = new System.Drawing.Size(120, 28);
            this.btnMyStock.TabIndex = 0;
            this.btnMyStock.Text = "My Stock";
            this.btnMyStock.Click += new System.EventHandler(this.BtnMyStock_Click);
            // 
            // Distributor_Interaction
            // 
            this.BackgroundImage = global::Seller.des.Properties.Resources.see1;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(554, 432);
            this.Controls.Add(this.btnNewOrderForm);
            this.Controls.Add(this.btnMyStock);
            this.Controls.Add(this.grpOrdersHistory);
            this.Controls.Add(this.grpPlaceOrder);
            this.Controls.Add(this.grpCheckStock);
            this.Name = "Distributor_Interaction";
            this.Text = "Distributor Interaction";
            this.grpCheckStock.ResumeLayout(false);
            this.grpCheckStock.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCheckStockResult)).EndInit();
            this.grpPlaceOrder.ResumeLayout(false);
            this.grpPlaceOrder.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity)).EndInit();
            this.grpOrdersHistory.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrdersHistory)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private Button btnNewOrderForm;
        private Button btnMyStock;
    }
}

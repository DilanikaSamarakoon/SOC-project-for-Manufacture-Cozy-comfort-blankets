using System.Windows.Forms;

namespace DistributorDashboard // Corrected namespace
{
    partial class Seller_Orders
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

        // Controls are declared here as class fields
        private DataGridView dataGridViewSellerOrders;
        private Button btnRefreshOrders;

        private void InitializeComponent()
        {
            this.dataGridViewSellerOrders = new System.Windows.Forms.DataGridView();
            this.btnRefreshOrders = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSellerOrders)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewSellerOrders
            // 
            this.dataGridViewSellerOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSellerOrders.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridViewSellerOrders.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewSellerOrders.Name = "dataGridViewSellerOrders";
            this.dataGridViewSellerOrders.ReadOnly = true;
            this.dataGridViewSellerOrders.RowHeadersWidth = 51;
            this.dataGridViewSellerOrders.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewSellerOrders.Size = new System.Drawing.Size(500, 300);
            this.dataGridViewSellerOrders.TabIndex = 0;
            // 
            // btnRefreshOrders
            // 
            this.btnRefreshOrders.Location = new System.Drawing.Point(12, 310);
            this.btnRefreshOrders.Name = "btnRefreshOrders";
            this.btnRefreshOrders.Size = new System.Drawing.Size(75, 23);
            this.btnRefreshOrders.TabIndex = 1;
            this.btnRefreshOrders.Text = "Refresh";
            this.btnRefreshOrders.UseVisualStyleBackColor = true;
            this.btnRefreshOrders.Click += new System.EventHandler(this.BtnRefreshOrders_Click);
            // 
            // Seller_Orders
            // 
            this.ClientSize = new System.Drawing.Size(500, 350);
            this.Controls.Add(this.dataGridViewSellerOrders);
            this.Controls.Add(this.btnRefreshOrders);
            this.Name = "Seller_Orders";
            this.Text = "Seller Orders";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSellerOrders)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion
    }
}

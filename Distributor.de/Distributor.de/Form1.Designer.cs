namespace DistributorDashboard // Use the namespace of your new project
{
    partial class Form1
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
            this.dgvMyInventory = new System.Windows.Forms.DataGridView();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnManufacturerInteraction = new System.Windows.Forms.Button();
            this.btnSellerOrders = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMyInventory)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvMyInventory
            // 
            this.dgvMyInventory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMyInventory.Location = new System.Drawing.Point(12, 58);
            this.dgvMyInventory.Name = "dgvMyInventory";
            this.dgvMyInventory.RowHeadersWidth = 51;
            this.dgvMyInventory.Size = new System.Drawing.Size(400, 380);
            this.dgvMyInventory.TabIndex = 0;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(12, 12);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(120, 40);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // btnManufacturerInteraction
            // 
            this.btnManufacturerInteraction.Location = new System.Drawing.Point(430, 12);
            this.btnManufacturerInteraction.Name = "btnManufacturerInteraction";
            this.btnManufacturerInteraction.Size = new System.Drawing.Size(150, 40);
            this.btnManufacturerInteraction.TabIndex = 2;
            this.btnManufacturerInteraction.Text = "Manufacturer Interaction";
            this.btnManufacturerInteraction.UseVisualStyleBackColor = true;
            this.btnManufacturerInteraction.Click += new System.EventHandler(this.BtnManufacturerInteraction_Click);
            // 
            // btnSellerOrders
            // 
            this.btnSellerOrders.Location = new System.Drawing.Point(590, 12);
            this.btnSellerOrders.Name = "btnSellerOrders";
            this.btnSellerOrders.Size = new System.Drawing.Size(150, 40);
            this.btnSellerOrders.TabIndex = 3;
            this.btnSellerOrders.Text = "Seller Orders";
            this.btnSellerOrders.UseVisualStyleBackColor = true;
            this.btnSellerOrders.Click += new System.EventHandler(this.BtnSellerOrders_Click);
            // 
            // Form1
            // 
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::Distributor.de.Properties.Resources.dis;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.btnSellerOrders);
            this.Controls.Add(this.btnManufacturerInteraction);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.dgvMyInventory);
            this.Name = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dgvMyInventory)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvMyInventory;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnManufacturerInteraction;
        private System.Windows.Forms.Button btnSellerOrders;
    }
}

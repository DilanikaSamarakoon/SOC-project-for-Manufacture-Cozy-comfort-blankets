using System.Windows.Forms;

namespace Seller.des
{
    partial class MyStock
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
            this.btnRefreshMyStock = new System.Windows.Forms.Button();
            this.dgvMyStock = new System.Windows.Forms.DataGridView();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.btnDistributorInteraction = new System.Windows.Forms.Button();
            this.btnNewOrderForm = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMyStock)).BeginInit();
            this.SuspendLayout();
            // 
            // btnRefreshMyStock
            // 
            this.btnRefreshMyStock.Location = new System.Drawing.Point(81, 27);
            this.btnRefreshMyStock.Name = "btnRefreshMyStock";
            this.btnRefreshMyStock.Size = new System.Drawing.Size(137, 36);
            this.btnRefreshMyStock.TabIndex = 0;
            this.btnRefreshMyStock.Text = "Refresh My Stock";
            this.btnRefreshMyStock.UseVisualStyleBackColor = true;
            this.btnRefreshMyStock.Click += new System.EventHandler(this.BtnRefreshMyStock_Click);
            // 
            // dgvMyStock
            // 
            this.dgvMyStock.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMyStock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMyStock.Location = new System.Drawing.Point(53, 82);
            this.dgvMyStock.Name = "dgvMyStock";
            this.dgvMyStock.RowHeadersWidth = 51;
            this.dgvMyStock.RowTemplate.Height = 24;
            this.dgvMyStock.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMyStock.Size = new System.Drawing.Size(699, 189);
            this.dgvMyStock.TabIndex = 1;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(81, 302);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(75, 16);
            this.linkLabel1.TabIndex = 2;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Quick Links";
            // 
            // btnDistributorInteraction
            // 
            this.btnDistributorInteraction.Location = new System.Drawing.Point(84, 337);
            this.btnDistributorInteraction.Name = "btnDistributorInteraction";
            this.btnDistributorInteraction.Size = new System.Drawing.Size(134, 56);
            this.btnDistributorInteraction.TabIndex = 3;
            this.btnDistributorInteraction.Text = "Distributor Interaction";
            this.btnDistributorInteraction.UseVisualStyleBackColor = true;
            this.btnDistributorInteraction.Click += new System.EventHandler(this.BtnDistributorInteraction_Click);
            // 
            // btnNewOrderForm
            // 
            this.btnNewOrderForm.Location = new System.Drawing.Point(262, 337);
            this.btnNewOrderForm.Name = "btnNewOrderForm";
            this.btnNewOrderForm.Size = new System.Drawing.Size(88, 56);
            this.btnNewOrderForm.TabIndex = 4;
            this.btnNewOrderForm.Text = "New order Form";
            this.btnNewOrderForm.UseVisualStyleBackColor = true;
            this.btnNewOrderForm.Click += new System.EventHandler(this.btClickNewOrderForm);
            // 
            // MyStock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Seller.des.Properties.Resources.see1;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnNewOrderForm);
            this.Controls.Add(this.btnDistributorInteraction);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.dgvMyStock);
            this.Controls.Add(this.btnRefreshMyStock);
            this.Name = "MyStock";
            this.Text = "My Stock";
            ((System.ComponentModel.ISupportInitialize)(this.dgvMyStock)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRefreshMyStock;
        private System.Windows.Forms.DataGridView dgvMyStock;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Button btnDistributorInteraction;
        private Button btnNewOrderForm;
    }
    namespace Seller.des
    {
        partial class Form1
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

            private void InitializeComponent()
            {
                this.btnRefreshMyStock = new System.Windows.Forms.Button();
                this.dgvMyStock = new System.Windows.Forms.DataGridView();
                this.btnDistributorInteraction = new System.Windows.Forms.Button();
                ((System.ComponentModel.ISupportInitialize)(this.dgvMyStock)).BeginInit();
                this.SuspendLayout();
                // 
                // btnRefreshMyStock
                // 
                this.btnRefreshMyStock.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.btnRefreshMyStock.Location = new System.Drawing.Point(12, 12);
                this.btnRefreshMyStock.Name = "btnRefreshMyStock";
                this.btnRefreshMyStock.Size = new System.Drawing.Size(140, 30);
                this.btnRefreshMyStock.TabIndex = 0;
                this.btnRefreshMyStock.Text = "Refresh My Stock";
                this.btnRefreshMyStock.UseVisualStyleBackColor = true;
                this.btnRefreshMyStock.Click += new System.EventHandler(this.btnRefreshMyStock_Click);
                // 
                // dgvMyStock
                // 
                this.dgvMyStock.AllowUserToAddRows = false;
                this.dgvMyStock.AllowUserToDeleteRows = false;
                this.dgvMyStock.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
                this.dgvMyStock.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
                this.dgvMyStock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                this.dgvMyStock.Location = new System.Drawing.Point(12, 48);
                this.dgvMyStock.Name = "dgvMyStock";
                this.dgvMyStock.ReadOnly = true;
                this.dgvMyStock.Size = new System.Drawing.Size(560, 273);
                this.dgvMyStock.TabIndex = 1;
                // 
                // btnDistributorInteraction
                // 
                this.btnDistributorInteraction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
                this.btnDistributorInteraction.Location = new System.Drawing.Point(12, 327);
                this.btnDistributorInteraction.Name = "btnDistributorInteraction";
                this.btnDistributorInteraction.Size = new System.Drawing.Size(140, 23);
                this.btnDistributorInteraction.TabIndex = 3;
                this.btnDistributorInteraction.Text = "Distributor Interaction";
                this.btnDistributorInteraction.UseVisualStyleBackColor = true;
                this.btnDistributorInteraction.Click += new System.EventHandler(this.btnDistributorInteraction_Click);
                // 
                // Form1
                // 
                this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
                this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                this.ClientSize = new System.Drawing.Size(584, 362);
                this.Controls.Add(this.btnDistributorInteraction);
                this.Controls.Add(this.dgvMyStock);
                this.Controls.Add(this.btnRefreshMyStock);
                this.MinimumSize = new System.Drawing.Size(400, 300);
                this.Name = "Form1";
                this.Text = "Seller Dashboard";
                ((System.ComponentModel.ISupportInitialize)(this.dgvMyStock)).EndInit();
                this.ResumeLayout(false);
            }

            #endregion

            private System.Windows.Forms.Button btnRefreshMyStock;
            private System.Windows.Forms.DataGridView dgvMyStock;
            private System.Windows.Forms.Button btnDistributorInteraction;
        }
    }
}

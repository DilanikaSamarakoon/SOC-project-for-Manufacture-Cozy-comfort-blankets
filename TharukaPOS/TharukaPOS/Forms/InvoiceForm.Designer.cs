namespace TharukaPOS.Forms
{
    partial class InvoiceForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InvoiceForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnDealerSearch = new System.Windows.Forms.Button();
            this.btnDateOk = new System.Windows.Forms.Button();
            this.txtDealerSearch = new System.Windows.Forms.TextBox();
            this.dtpInvoiceDate = new System.Windows.Forms.DateTimePicker();
            this.dgvDealerList = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblSelectedDealerName = new System.Windows.Forms.Label();
            this.dgvSalesDetails = new System.Windows.Forms.DataGridView();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.btnDateChange = new System.Windows.Forms.Button();
            this.btnDealerOk = new System.Windows.Forms.Button();
            this.btnDealerChange = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDealerList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSalesDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label1.Location = new System.Drawing.Point(17, 12);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 38);
            this.label1.TabIndex = 0;
            this.label1.Text = "Invoices";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label2.Location = new System.Drawing.Point(19, 152);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(280, 29);
            this.label2.TabIndex = 1;
            this.label2.Text = "Search by Dealer\'s name";
            // 
            // btnDealerSearch
            // 
            this.btnDealerSearch.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(83)))), ((int)(((byte)(255)))));
            this.btnDealerSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(83)))), ((int)(((byte)(255)))));
            this.btnDealerSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDealerSearch.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnDealerSearch.Location = new System.Drawing.Point(495, 184);
            this.btnDealerSearch.Margin = new System.Windows.Forms.Padding(4);
            this.btnDealerSearch.Name = "btnDealerSearch";
            this.btnDealerSearch.Size = new System.Drawing.Size(103, 28);
            this.btnDealerSearch.TabIndex = 2;
            this.btnDealerSearch.Text = "Search";
            this.btnDealerSearch.UseVisualStyleBackColor = true;
            this.btnDealerSearch.Click += new System.EventHandler(this.btnDealerSearch_Click);
            // 
            // btnDateOk
            // 
            this.btnDateOk.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(83)))), ((int)(((byte)(255)))));
            this.btnDateOk.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(83)))), ((int)(((byte)(255)))));
            this.btnDateOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDateOk.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnDateOk.Location = new System.Drawing.Point(373, 100);
            this.btnDateOk.Margin = new System.Windows.Forms.Padding(4);
            this.btnDateOk.Name = "btnDateOk";
            this.btnDateOk.Size = new System.Drawing.Size(103, 28);
            this.btnDateOk.TabIndex = 3;
            this.btnDateOk.Text = "OK";
            this.btnDateOk.UseVisualStyleBackColor = true;
            this.btnDateOk.Click += new System.EventHandler(this.btnDateOk_Click);
            // 
            // txtDealerSearch
            // 
            this.txtDealerSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(86)))));
            this.txtDealerSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDealerSearch.Location = new System.Drawing.Point(24, 185);
            this.txtDealerSearch.Margin = new System.Windows.Forms.Padding(4);
            this.txtDealerSearch.Name = "txtDealerSearch";
            this.txtDealerSearch.Size = new System.Drawing.Size(390, 27);
            this.txtDealerSearch.TabIndex = 6;
            // 
            // dtpInvoiceDate
            // 
            this.dtpInvoiceDate.Location = new System.Drawing.Point(24, 101);
            this.dtpInvoiceDate.Margin = new System.Windows.Forms.Padding(4);
            this.dtpInvoiceDate.Name = "dtpInvoiceDate";
            this.dtpInvoiceDate.Size = new System.Drawing.Size(334, 27);
            this.dtpInvoiceDate.TabIndex = 7;
            // 
            // dgvDealerList
            // 
            this.dgvDealerList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDealerList.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(86)))));
            this.dgvDealerList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDealerList.Location = new System.Drawing.Point(24, 262);
            this.dgvDealerList.Margin = new System.Windows.Forms.Padding(4);
            this.dgvDealerList.Name = "dgvDealerList";
            this.dgvDealerList.RowHeadersVisible = false;
            this.dgvDealerList.RowHeadersWidth = 51;
            this.dgvDealerList.RowTemplate.Height = 24;
            this.dgvDealerList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDealerList.Size = new System.Drawing.Size(573, 554);
            this.dgvDealerList.TabIndex = 8;
            this.dgvDealerList.Click += new System.EventHandler(this.dgvDealerList_SelectionChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label4.Location = new System.Drawing.Point(19, 68);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 29);
            this.label4.TabIndex = 10;
            this.label4.Text = "Date";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label3.Location = new System.Drawing.Point(19, 229);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 29);
            this.label3.TabIndex = 11;
            this.label3.Text = "Dealer List";
            // 
            // lblSelectedDealerName
            // 
            this.lblSelectedDealerName.AutoSize = true;
            this.lblSelectedDealerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectedDealerName.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblSelectedDealerName.Location = new System.Drawing.Point(798, 12);
            this.lblSelectedDealerName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSelectedDealerName.Name = "lblSelectedDealerName";
            this.lblSelectedDealerName.Size = new System.Drawing.Size(185, 29);
            this.lblSelectedDealerName.TabIndex = 12;
            this.lblSelectedDealerName.Text = "Dealer\'s Name";
            // 
            // dgvSalesDetails
            // 
            this.dgvSalesDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSalesDetails.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(86)))));
            this.dgvSalesDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSalesDetails.Location = new System.Drawing.Point(625, 46);
            this.dgvSalesDetails.Margin = new System.Windows.Forms.Padding(4);
            this.dgvSalesDetails.Name = "dgvSalesDetails";
            this.dgvSalesDetails.RowHeadersVisible = false;
            this.dgvSalesDetails.RowHeadersWidth = 51;
            this.dgvSalesDetails.RowTemplate.Height = 24;
            this.dgvSalesDetails.Size = new System.Drawing.Size(521, 770);
            this.dgvSalesDetails.TabIndex = 13;
            // 
            // btnCancel
            // 
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(83)))), ((int)(((byte)(255)))));
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(83)))), ((int)(((byte)(255)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnCancel.Location = new System.Drawing.Point(1044, 824);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(103, 28);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnPrint
            // 
            this.btnPrint.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(83)))), ((int)(((byte)(255)))));
            this.btnPrint.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(83)))), ((int)(((byte)(255)))));
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnPrint.Location = new System.Drawing.Point(928, 824);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(4);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(103, 28);
            this.btnPrint.TabIndex = 15;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = true;
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // btnDateChange
            // 
            this.btnDateChange.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(83)))), ((int)(((byte)(255)))));
            this.btnDateChange.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(83)))), ((int)(((byte)(255)))));
            this.btnDateChange.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDateChange.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnDateChange.Location = new System.Drawing.Point(495, 100);
            this.btnDateChange.Margin = new System.Windows.Forms.Padding(4);
            this.btnDateChange.Name = "btnDateChange";
            this.btnDateChange.Size = new System.Drawing.Size(103, 28);
            this.btnDateChange.TabIndex = 16;
            this.btnDateChange.Text = "Change";
            this.btnDateChange.UseVisualStyleBackColor = true;
            this.btnDateChange.Click += new System.EventHandler(this.btnDateChange_Click);
            // 
            // btnDealerOk
            // 
            this.btnDealerOk.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(83)))), ((int)(((byte)(255)))));
            this.btnDealerOk.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(83)))), ((int)(((byte)(255)))));
            this.btnDealerOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDealerOk.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnDealerOk.Location = new System.Drawing.Point(380, 824);
            this.btnDealerOk.Margin = new System.Windows.Forms.Padding(4);
            this.btnDealerOk.Name = "btnDealerOk";
            this.btnDealerOk.Size = new System.Drawing.Size(103, 28);
            this.btnDealerOk.TabIndex = 17;
            this.btnDealerOk.Text = "OK";
            this.btnDealerOk.UseVisualStyleBackColor = true;
            this.btnDealerOk.Click += new System.EventHandler(this.btnDealerOk_Click);
            // 
            // btnDealerChange
            // 
            this.btnDealerChange.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(83)))), ((int)(((byte)(255)))));
            this.btnDealerChange.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(83)))), ((int)(((byte)(255)))));
            this.btnDealerChange.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDealerChange.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnDealerChange.Location = new System.Drawing.Point(494, 824);
            this.btnDealerChange.Margin = new System.Windows.Forms.Padding(4);
            this.btnDealerChange.Name = "btnDealerChange";
            this.btnDealerChange.Size = new System.Drawing.Size(103, 28);
            this.btnDealerChange.TabIndex = 18;
            this.btnDealerChange.Text = "Change";
            this.btnDealerChange.UseVisualStyleBackColor = true;
            this.btnDealerChange.Click += new System.EventHandler(this.btnDealerChange_Click);
            // 
            // InvoiceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(28)))), ((int)(((byte)(63)))));
            this.ClientSize = new System.Drawing.Size(1160, 858);
            this.Controls.Add(this.btnDealerChange);
            this.Controls.Add(this.btnDealerOk);
            this.Controls.Add(this.btnDateChange);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.dgvSalesDetails);
            this.Controls.Add(this.lblSelectedDealerName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dgvDealerList);
            this.Controls.Add(this.dtpInvoiceDate);
            this.Controls.Add(this.txtDealerSearch);
            this.Controls.Add(this.btnDateOk);
            this.Controls.Add(this.btnDealerSearch);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Arial Rounded MT Bold", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "InvoiceForm";
            this.Text = "InvoiceForm";
            ((System.ComponentModel.ISupportInitialize)(this.dgvDealerList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSalesDetails)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnDealerSearch;
        private System.Windows.Forms.Button btnDateOk;
        private System.Windows.Forms.TextBox txtDealerSearch;
        private System.Windows.Forms.DateTimePicker dtpInvoiceDate;
        private System.Windows.Forms.DataGridView dgvDealerList;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblSelectedDealerName;
        private System.Windows.Forms.DataGridView dgvSalesDetails;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnPrint;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Windows.Forms.Button btnDateChange;
        private System.Windows.Forms.Button btnDealerOk;
        private System.Windows.Forms.Button btnDealerChange;
    }
}
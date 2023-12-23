namespace QuanLyDoanhNghiepMililap
{
    partial class fDoanhThuTheoNgay
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
            this.txtLable = new System.Windows.Forms.Label();
            this.button_search = new System.Windows.Forms.Button();
            this.cbDate = new System.Windows.Forms.DateTimePicker();
            this.grvNgay = new System.Windows.Forms.DataGridView();
            this.btnExportExcel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grvNgay)).BeginInit();
            this.SuspendLayout();
            // 
            // txtLable
            // 
            this.txtLable.AutoSize = true;
            this.txtLable.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.txtLable.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLable.Location = new System.Drawing.Point(195, 229);
            this.txtLable.Name = "txtLable";
            this.txtLable.Size = new System.Drawing.Size(417, 27);
            this.txtLable.TabIndex = 7;
            this.txtLable.Text = "Bảng thống kê doanh thu theo ngày ";
            // 
            // button_search
            // 
            this.button_search.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_search.Location = new System.Drawing.Point(639, 147);
            this.button_search.Name = "button_search";
            this.button_search.Size = new System.Drawing.Size(114, 31);
            this.button_search.TabIndex = 8;
            this.button_search.Text = "Tìm kiếm";
            this.button_search.UseVisualStyleBackColor = true;
            this.button_search.Click += new System.EventHandler(this.button_search_Click);
            // 
            // cbDate
            // 
            this.cbDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDate.Location = new System.Drawing.Point(257, 148);
            this.cbDate.Name = "cbDate";
            this.cbDate.Size = new System.Drawing.Size(376, 30);
            this.cbDate.TabIndex = 9;
            // 
            // grvNgay
            // 
            this.grvNgay.AllowUserToAddRows = false;
            this.grvNgay.AllowUserToDeleteRows = false;
            this.grvNgay.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grvNgay.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grvNgay.Location = new System.Drawing.Point(0, 402);
            this.grvNgay.Name = "grvNgay";
            this.grvNgay.ReadOnly = true;
            this.grvNgay.RowHeadersWidth = 51;
            this.grvNgay.RowTemplate.Height = 24;
            this.grvNgay.Size = new System.Drawing.Size(1066, 150);
            this.grvNgay.TabIndex = 10;
            this.grvNgay.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.grvNgay_CellFormatting);
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.Location = new System.Drawing.Point(824, 33);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(149, 45);
            this.btnExportExcel.TabIndex = 11;
            this.btnExportExcel.Text = "Xuất Excel";
            this.btnExportExcel.UseVisualStyleBackColor = true;
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
            // 
            // fDoanhThuTheoNgay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(1066, 552);
            this.Controls.Add(this.btnExportExcel);
            this.Controls.Add(this.grvNgay);
            this.Controls.Add(this.cbDate);
            this.Controls.Add(this.button_search);
            this.Controls.Add(this.txtLable);
            this.Name = "fDoanhThuTheoNgay";
            this.Text = "fThongKeTheoNgay";
            this.Load += new System.EventHandler(this.fDoanhThuTheoNgay_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grvNgay)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label txtLable;
        private System.Windows.Forms.Button button_search;
        private System.Windows.Forms.DateTimePicker cbDate;
        private System.Windows.Forms.DataGridView grvNgay;
        private System.Windows.Forms.Button btnExportExcel;
    }
}
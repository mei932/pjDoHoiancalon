namespace QuanLyDoanhNghiepMililap
{
    partial class fThongKeVatTuTrongKho
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
            this.txtLabel = new System.Windows.Forms.Label();
            this.dtgrVatTuTrongKho = new System.Windows.Forms.DataGridView();
            this.btnExportExcel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dtgrVatTuTrongKho)).BeginInit();
            this.SuspendLayout();
            // 
            // txtLabel
            // 
            this.txtLabel.AutoSize = true;
            this.txtLabel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.txtLabel.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLabel.Location = new System.Drawing.Point(299, 103);
            this.txtLabel.Name = "txtLabel";
            this.txtLabel.Size = new System.Drawing.Size(458, 35);
            this.txtLabel.TabIndex = 10;
            this.txtLabel.Text = "Bảng thống kê vật tư trong kho";
            // 
            // dtgrVatTuTrongKho
            // 
            this.dtgrVatTuTrongKho.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgrVatTuTrongKho.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dtgrVatTuTrongKho.Location = new System.Drawing.Point(0, 402);
            this.dtgrVatTuTrongKho.Name = "dtgrVatTuTrongKho";
            this.dtgrVatTuTrongKho.RowHeadersWidth = 51;
            this.dtgrVatTuTrongKho.RowTemplate.Height = 24;
            this.dtgrVatTuTrongKho.Size = new System.Drawing.Size(1066, 150);
            this.dtgrVatTuTrongKho.TabIndex = 11;
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.Location = new System.Drawing.Point(811, 32);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(155, 49);
            this.btnExportExcel.TabIndex = 12;
            this.btnExportExcel.Text = "Xuất Excel";
            this.btnExportExcel.UseVisualStyleBackColor = true;
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
            // 
            // fThongKeVatTuTrongKho
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(1066, 552);
            this.Controls.Add(this.btnExportExcel);
            this.Controls.Add(this.dtgrVatTuTrongKho);
            this.Controls.Add(this.txtLabel);
            this.Name = "fThongKeVatTuTrongKho";
            this.Text = "fThongKeVatTuTrongKho";
            this.Load += new System.EventHandler(this.fThongKeVatTuTrongKho_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgrVatTuTrongKho)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label txtLabel;
        private System.Windows.Forms.DataGridView dtgrVatTuTrongKho;
        private System.Windows.Forms.Button btnExportExcel;
    }
}
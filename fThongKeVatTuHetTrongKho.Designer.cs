namespace QuanLyDoanhNghiepMililap
{
    partial class fThongKeVatTuHetTrongKho
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
            this.dtgrThongKeVatTuHet = new System.Windows.Forms.DataGridView();
            this.btnExportExcel = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dtgrThongKeVatTuHet)).BeginInit();
            this.SuspendLayout();
            // 
            // txtLabel
            // 
            this.txtLabel.AutoSize = true;
            this.txtLabel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.txtLabel.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLabel.Location = new System.Drawing.Point(275, 116);
            this.txtLabel.Name = "txtLabel";
            this.txtLabel.Size = new System.Drawing.Size(511, 35);
            this.txtLabel.TabIndex = 11;
            this.txtLabel.Text = "Bảng thống kê vật tư hết trong kho";
            // 
            // dtgrThongKeVatTuHet
            // 
            this.dtgrThongKeVatTuHet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgrThongKeVatTuHet.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dtgrThongKeVatTuHet.Location = new System.Drawing.Point(0, 402);
            this.dtgrThongKeVatTuHet.Name = "dtgrThongKeVatTuHet";
            this.dtgrThongKeVatTuHet.RowHeadersWidth = 51;
            this.dtgrThongKeVatTuHet.RowTemplate.Height = 24;
            this.dtgrThongKeVatTuHet.Size = new System.Drawing.Size(1066, 150);
            this.dtgrThongKeVatTuHet.TabIndex = 12;
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.Location = new System.Drawing.Point(839, 31);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(112, 40);
            this.btnExportExcel.TabIndex = 13;
            this.btnExportExcel.Text = "Xuất Excel";
            this.btnExportExcel.UseCompatibleTextRendering = true;
            this.btnExportExcel.UseVisualStyleBackColor = true;
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(104, 31);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(104, 40);
            this.btnPrint.TabIndex = 14;
            this.btnPrint.Text = "In";
            this.btnPrint.UseVisualStyleBackColor = true;
            // 
            // fThongKeVatTuHetTrongKho
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(1066, 552);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnExportExcel);
            this.Controls.Add(this.dtgrThongKeVatTuHet);
            this.Controls.Add(this.txtLabel);
            this.Name = "fThongKeVatTuHetTrongKho";
            this.Text = "fThongKeVatTuHetTrongKho";
            this.Load += new System.EventHandler(this.fThongKeVatTuHetTrongKho_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgrThongKeVatTuHet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label txtLabel;
        private System.Windows.Forms.DataGridView dtgrThongKeVatTuHet;
        private System.Windows.Forms.Button btnExportExcel;
        private System.Windows.Forms.Button btnPrint;
    }
}
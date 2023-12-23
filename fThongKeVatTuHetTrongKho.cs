using QuanLyDoanhNghiepMililap.Modify;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyDoanhNghiepMililap
{
    public partial class fThongKeVatTuHetTrongKho : Form
    {
       
        public fThongKeVatTuHetTrongKho()
        {
            InitializeComponent();
        }

        private void fThongKeVatTuHetTrongKho_Load(object sender, EventArgs e)
        {
            SqlConnection con = connection.GetSqlConnection();
            con.Open();
            // Load dữ liệu từ SQL Server vào DataTable
            LoadData();
        }
        private void LoadData()
        {
            try
            {
                {
                    string query = @"SELECT *
                        FROM VatTu 
                        where VatTu.SoLuongTonKho <= 5
                        ORDER BY SoLuongTonKho ASC";

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            LoadData();
           /* Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
            app.Visible = true;
            worksheet = workbook.Sheets["Sheet1"];
            worksheet = workbook.ActiveSheet;
            worksheet.Name = "Exported from gridview";
            for (int i = 0; i < dtgrThongKeVatTuHet.Columns.Count; i++)
            {
                worksheet.Cells[1, i + 1] = dtgrThongKeVatTuHet.Columns[i].HeaderText;
            }
            for (int i = 0; i < dtgrThongKeVatTuHet.Rows.Count-1; i++)
            {
                for (int j = 0; j < dtgrThongKeVatTuHet.Columns.Count; j++)
                {
                    worksheet.Cells[i + 2, j + 1] = dtgrThongKeVatTuHet.Rows[i].Cells[j].Value.ToString();
                }
            }
            try
            {
                workbook.SaveAs(".\\Thongkevattuhettrongkho" + DateTime.Now.ToString("yyyyMMddHHmmssffff") + ".xlsx", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            }
            catch
            {
                MessageBox.Show("File đã tồn tại trong thư mục");
            }
            app.Quit();*/
        }
    }
}

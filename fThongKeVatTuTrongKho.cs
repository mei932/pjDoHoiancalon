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
    public partial class fThongKeVatTuTrongKho : Form
    {
        string connectString = @"Data Source=DELL\SQLEXPRESS;Initial Catalog=DoAn1Nhom3;Integrated Security=True";
        SqlConnection con;
        SqlCommand cmd; //thực hiện câu lệnh 
        SqlDataAdapter adt;
        DataTable dt; //đổ dữ liệu vào 
        public fThongKeVatTuTrongKho()
        {
            InitializeComponent();
        }

        private void fThongKeVatTuTrongKho_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(connectString);
            // mở kết nối 
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
                        where VatTu.SoLuongTonKho >= 30
                        ORDER BY SoLuongTonKho DESC";


                    cmd = new SqlCommand(query, con);
                    // Tạo đối tượng SqlDataAdapter để đổ dữ liệu vào DataTable
                    adt = new SqlDataAdapter(cmd);
                    dt = new DataTable();

                    // Đổ dữ liệu vào DataTable
                    adt.Fill(dt);

                    // Đặt DataTable làm nguồn dữ liệu cho DataGridView
                    dtgrVatTuTrongKho.DataSource = dt;
                    dtgrVatTuTrongKho.Columns["MaVatTu"].HeaderText = "Mã vật tư";
                    dtgrVatTuTrongKho.Columns["TenVatTu"].HeaderText = "Tên khách hàng";
                    dtgrVatTuTrongKho.Columns["MaLoaiVatTu"].HeaderText = "Mã loại vật tư";
                    dtgrVatTuTrongKho.Columns["GiaNhap"].HeaderText = "Giá nhập";
                    dtgrVatTuTrongKho.Columns["GiaBan"].HeaderText = "Giá bán";
                    dtgrVatTuTrongKho.Columns["SoLuongTonKho"].HeaderText = "Số lượng tồn kho";

                    dtgrVatTuTrongKho.Columns["MaVatTu"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    dtgrVatTuTrongKho.Columns["TenVatTu"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    dtgrVatTuTrongKho.Columns["MaLoaiVatTu"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    dtgrVatTuTrongKho.Columns["GiaNhap"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    dtgrVatTuTrongKho.Columns["GiaBan"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    dtgrVatTuTrongKho.Columns["SoLuongTonKho"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    con.Close();

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
            /*Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
            app.Visible = true;
            worksheet = workbook.Sheets["Sheet1"];
            worksheet = workbook.ActiveSheet;
            worksheet.Name = "Exported from gridview";
            for (int i = 0; i < dtgrVatTuTrongKho.Columns.Count; i++)
            {
                worksheet.Cells[1, i + 1] = dtgrVatTuTrongKho.Columns[i].HeaderText;
            }
            for (int i = 0; i < dtgrVatTuTrongKho.Rows.Count - 1; i++)
            {
                for (int j = 0; j < dtgrVatTuTrongKho.Columns.Count; j++)
                {
                    worksheet.Cells[i + 2, j + 1] = dtgrVatTuTrongKho.Rows[i].Cells[j].Value.ToString();
                }
            }
            try
            {
                workbook.SaveAs(".\\Thongkevattutrongkho" + DateTime.Now.ToString("yyyyMMddHHmmssffff") + ".xlsx", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            }
            catch
            {
                MessageBox.Show("File đã tồn tại trong thư mục");
            }
            app.Quit();*/
        }
    }
}

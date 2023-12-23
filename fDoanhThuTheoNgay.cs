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
    public partial class fDoanhThuTheoNgay : Form
    {
        String connectString = @"Data Source=DELL\SQLEXPRESS;Initial Catalog=DoAn1Nhom3;Integrated Security=True";
        SqlConnection con;
        SqlCommand cmd; //thực hiện câu lệnh 
        SqlDataAdapter adt;
        DataTable dt; //đổ dữ liệu vào 
        public fDoanhThuTheoNgay()
        { 
            InitializeComponent();
        }
        private void fDoanhThuTheoNgay_Load(object sender, EventArgs e)
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
                if (cbDate.Value.ToString() != "")
                {

                    string query = @"SELECT KhachHang.MaKhachHang,KhachHang.TenKhachHang,HoaDon.NgayBan,SUM(HoaDon.TongTien) as TongTien
                                FROM HoaDon 
                                JOIN KhachHang on KhachHang.MaKhachHang = HoaDon.MaKhachHang
                                where HoaDon.NgayBan = '"+cbDate.Value.ToString()+"' group by KhachHang.MaKhachHang,KhachHang.TenKhachHang,HoaDon.NgayBan";
                    // Tạo đối tượng SqlCommand
                    cmd = new SqlCommand(query, con);
                    // Tạo đối tượng SqlDataAdapter để đổ dữ liệu vào DataTable
                    adt = new SqlDataAdapter(cmd);
                    dt = new DataTable();

                    // Đổ dữ liệu vào DataTable
                    adt.Fill(dt);

                    //Đặt DataTable làm nguồn dữ liệu cho DataGridView
                    grvNgay.DataSource = dt;
                    grvNgay.Columns["makhachhang"].HeaderText = "Mã khách hàng";
                    grvNgay.Columns["tenkhachhang"].HeaderText = "Tên khách hàng";
                    grvNgay.Columns["ngayBan"].HeaderText = "Ngày bán";
                    grvNgay.Columns["tongtien"].HeaderText = "Tổng tiền";

                    grvNgay.Columns["makhachhang"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    grvNgay.Columns["tenkhachhang"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    grvNgay.Columns["ngayBan"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    grvNgay.Columns["tongtien"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    con.Close();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button_search_Click(object sender, EventArgs e)
        {
            LoadData();
            txtLable.Text= "Bảng thống kê doanh thu ngày " + cbDate.Text;
        }

        private void grvNgay_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 3 && e.Value != null)
            {
                try
                {
                    decimal cellValue = decimal.Parse(e.Value.ToString());
                    e.Value = string.Format("{0:C0} đ", cellValue);


                    e.FormattingApplied = true;
                }
                catch (FormatException)
                {

                }
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
            for (int i = 0; i < grvNgay.Columns.Count; i++)
            {
                worksheet.Cells[1, i + 1] = grvNgay.Columns[i].HeaderText;
            }
            for (int i = 0; i < grvNgay.Rows.Count; i++)
            {
                for (int j = 0; j < grvNgay.Columns.Count; j++)
                {
                    worksheet.Cells[i + 2, j + 1] = grvNgay.Rows[i].Cells[j].Value.ToString();
                }
            }
            try
            {
                workbook.SaveAs(".\\Doanhthutheongay" + DateTime.Now.ToString("yyyyMMddHHmmssffff") + ".xlsx", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            }
            catch
            {
                MessageBox.Show("File đã tồn tại trong thư mục");
            }
            app.Quit();*/
        }
    }
}

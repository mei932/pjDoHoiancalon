using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyDoanhNghiepMililap
{
    public partial class fDoanhThuTheoThang : Form
    {
        String connectString = @"Data Source=DELL\SQLEXPRESS;Initial Catalog=DoAn1Nhom3;Integrated Security=True";
        SqlConnection con;
        SqlCommand cmd; //thực hiện câu lệnh 
        SqlDataAdapter adt;
        DataTable dt; //đổ dữ liệu vào 
        public fDoanhThuTheoThang()
        {
            InitializeComponent();
        }

        private void fDoanhThuTheoThang_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(connectString);
            // mở kết nối 
            con.Open();
            // Load dữ liệu từ SQL Server vào DataTable
            LoadData();

            var monthnames = new string[] { "Tháng 1", "Tháng 2", "Tháng 3", "04", "05", "06", "07", "08", "09", "10", "11", "12", "" };
            List<int> thang = new List<int> { 1,2,3,4,5,6,7,8,9,10,11,12};
            List<int> nam = new List<int>();
            for(int i = 2015; i<= DateTime.Now.Year; i++)
            {
                nam.Add(i);
            }
            foreach(var item in nam)
            {
                cbNam.Items.Add(item);
            }
            foreach(var item in thang)
            {
                cbThang.Items.Add(item);
            }
         
        }
        private void LoadData()
        {
            try
            {
                if (cbThang.Text.Length > 0 && cbNam.Text.Length > 0)
                {
                   
                    string query = @"select khachhang.makhachhang,khachhang.tenkhachhang,month(hoadon.ngayban) as thang,sum(hoadon.tongtien) as tongtien
                                    from hoadon 
                                    join khachhang on khachhang.makhachhang = hoadon.makhachhang
                                    where month(hoadon.ngayban) = " + (int)cbThang.SelectedItem + @"and year(hoadon.ngayban) = " + (int)cbNam.SelectedItem + @"
                                    group by khachhang.makhachhang,khachhang.tenkhachhang,month(hoadon.ngayban)";

                  
                    cmd = new SqlCommand(query, con);
                // Tạo đối tượng SqlDataAdapter để đổ dữ liệu vào DataTable
                adt = new SqlDataAdapter(cmd);
                dt = new DataTable();

                // Đổ dữ liệu vào DataTable
                adt.Fill(dt);

                // Đặt DataTable làm nguồn dữ liệu cho DataGridView
                dgvThang.DataSource = dt;
                    dgvThang.Columns["makhachhang"].HeaderText = "Mã khách hàng";
                    dgvThang.Columns["tenkhachhang"].HeaderText = "Tên khách hàng";
                    dgvThang.Columns["thang"].HeaderText = "Tháng";
                    dgvThang.Columns["tongtien"].HeaderText = "Tổng tiền";

                    dgvThang.Columns["makhachhang"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    dgvThang.Columns["tenkhachhang"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    dgvThang.Columns["thang"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    dgvThang.Columns["tongtien"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    con.Close();
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            txtLabel.Text= "Bảng thống kê doanh thu tháng " + cbThang.Text + " năm " + cbNam.Text;
            LoadData();
        }

        private void dgvThang_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 3 && e.Value != null)
            {
                try
                {
                    decimal cellValue = decimal.Parse(e.Value.ToString());
                    e.Value = string.Format("{0:N0} vnđ", Convert.ToDecimal(cellValue));
                    e.FormattingApplied = true;
                }
                catch (FormatException)
                {
                   
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadData();
            /*Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
            app.Visible = true;
            worksheet = workbook.Sheets["Sheet1"];
            worksheet = workbook.ActiveSheet;
            worksheet.Name = "Exported from gridview";
            for (int i = 0; i < dgvThang.Columns.Count; i++)
            {
                worksheet.Cells[1, i+1] = dgvThang.Columns[i].HeaderText;
            }
            for (int i = 0; i < dgvThang.Rows.Count; i++)
            {
                for (int j = 0; j < dgvThang.Columns.Count; j++)
                {
                    worksheet.Cells[i + 2, j + 1] = dgvThang.Rows[i].Cells[j].Value.ToString();
                }
            }
            try
            {
                workbook.SaveAs(".\\Doanhthutheothang" + DateTime.Now.ToString("yyyyMMddHHmmssffff") + ".xlsx", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            }
            catch
            {
                MessageBox.Show("File đã tồn tại trong thư mục");
            }
            app.Quit();*/
        }

    }
}

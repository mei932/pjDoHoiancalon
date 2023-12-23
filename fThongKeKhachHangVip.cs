using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyDoanhNghiepMililap
{
    public partial class fThongKeKhachHangVip : Form
    {
        string connectString = @"Data Source=DESKTOP-ESFTV9I\SQLEXPRESS;Initial Catalog=DoAn1Nhom3;Integrated Security=True";
        SqlConnection con;
        SqlCommand cmd; //thực hiện câu lệnh 
        SqlDataAdapter adt;
        DataTable dt; //đổ dữ liệu vào 
        public fThongKeKhachHangVip()
        {
            InitializeComponent();
        }

        private void fThongKeKhachHangVip_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(connectString);
            // mở kết nối 
            con.Open();
            // Load dữ liệu từ SQL Server vào DataTable
            LoadData();

            var monthnames = new string[] { "Tháng 1", "Tháng 2", "Tháng 3", "04", "05", "06", "07", "08", "09", "10", "11", "12", "" };
            List<int> thang = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            List<int> nam = new List<int>();
            for (int i = 2015; i <= DateTime.Now.Year; i++)
            {
                nam.Add(i);
            }
            foreach (var item in nam)
            {
                cbNam.Items.Add(item);
            }
            foreach (var item in thang)
            {
                cbThang.Items.Add(item);
            }
        }
        private void LoadData()
        {
            try
            {
                if (cbThang.SelectedItem != null && cbNam.SelectedItem != null)
                {
                    using (SqlConnection con = new SqlConnection(connectString))
                    {
                        con.Open();

                        string query = @"SELECT TOP 1 KhachHang.MaKhachHang, KhachHang.TenKhachHang, COUNT(HoaDon.MaHoaDon) AS SoLanMua, SUM(HoaDon.TongTien) AS TongTienMua
                                     FROM HoaDon
                                    JOIN KhachHang ON HoaDon.MaKhachHang = KhachHang.MaKhachHang
                                    WHERE YEAR(HoaDon.NgayBan) = @Nam AND MONTH(HoaDon.NgayBan) = @Thang
                                    GROUP BY KhachHang.MaKhachHang, KhachHang.TenKhachHang
                                    ORDER BY COUNT(HoaDon.MaHoaDon) DESC";

                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@Thang", (int)cbThang.SelectedItem);
                        cmd.Parameters.AddWithValue("@Nam", (int)cbNam.SelectedItem);

                        SqlDataAdapter adt = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();

                        adt.Fill(dt);

                        dtgrKhachHangVip.DataSource = dt;
                        dtgrKhachHangVip.DataSource = dt;
                        dtgrKhachHangVip.Columns["MaKhachHang"].HeaderText = "Mã Khách Hàng";
                        dtgrKhachHangVip.Columns["TenKhachHang"].HeaderText = "Tên Khách Hàng";
                        dtgrKhachHangVip.Columns["SoLanMua"].HeaderText = "Số Lần Mua";
                        dtgrKhachHangVip.Columns["TongTienMua"].HeaderText = "Tổng Tiền Mua";
                        con.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn Tháng và Năm để thực hiện thống kê!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }


        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            txtLabel.Text = "Bảng thống kê khách hàng vip tháng " + cbThang.Text + " năm " + cbNam.Text;
            LoadData();
        }

        private void dtgrKhachHangVip_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "Excel Files|*.xlsx";
                    sfd.FileName = "ThongKe_KhachHang_VIP_" + cbThang.Text + "_" + cbNam.Text + ".xlsx";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        FileInfo file = new FileInfo(sfd.FileName);
                        using (ExcelPackage package = new ExcelPackage(file))
                        {
                            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("ThongKeKhachHang");

                            // Đổ dữ liệu từ DataGridView vào Excel
                            worksheet.Cells[1, 1].Value = "Tháng:";
                            worksheet.Cells[1, 2].Value = cbThang.Text;
                            worksheet.Cells[2, 1].Value = "Năm:";
                            worksheet.Cells[2, 2].Value = cbNam.Text;

                            for (int i = 1; i <= dtgrKhachHangVip.Columns.Count; i++)
                            {
                                worksheet.Cells[4, i].Value = dtgrKhachHangVip.Columns[i - 1].HeaderText;
                                worksheet.Column(i).AutoFit(); // Điều chỉnh độ rộng của cột
                            }

                            for (int i = 0; i < dtgrKhachHangVip.Rows.Count; i++)
                            {
                                for (int j = 0; j < dtgrKhachHangVip.Columns.Count; j++)
                                {
                                    worksheet.Cells[i + 5, j + 1].Value = dtgrKhachHangVip.Rows[i].Cells[j].Value;
                                }
                            }

                            package.Save();
                        }

                        MessageBox.Show("Xuất Excel thành công!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
    }
}

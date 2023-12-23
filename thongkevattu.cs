using OfficeOpenXml;
using QuanLyDoanhNghiepMililap.Modify;
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
    public partial class thongkevattu : Form
    {
        public thongkevattu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Kết nối đến cơ sở dữ liệu
            using (SqlConnection con = connection.GetSqlConnection())
            {
                // Mở kết nối
                con.Open();

                // Chuẩn bị truy vấn SQL
                string query = "SELECT vt.TenVatTu, vt.SoLuongTonKho AS SoLuongConLai, " +
                               "SUM(ct.SoLuong) AS SoLuongBanDuoc, " +
                               "SUM(ct.SoLuong * vt.GiaBan) AS TongTienBanDuoc " +
                               "FROM VatTu vt " +
                               "INNER JOIN ChiTiet_DonDatHang ct ON vt.MaVatTu = ct.MaVatTu " +
                               "WHERE vt.TenVatTu = @TenVatTu " +
                               "GROUP BY vt.TenVatTu, vt.SoLuongTonKho";

                // Tạo đối tượng Command và thực hiện truy vấn
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    command.Parameters.AddWithValue("@TenVatTu", txtTim.Text.Trim());

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Hiển thị kết quả tìm được vào các TextBox tương ứng
                            txtCl.Text = reader["SoLuongConLai"].ToString();
                            txtBanDc.Text = reader["SoLuongBanDuoc"].ToString();
                            txtTong.Text = reader["TongTienBanDuoc"].ToString();
                        }
                        else
                        {
                            // Nếu không tìm thấy thông tin vật tư, thông báo hoặc xử lý phù hợp
                            MessageBox.Show("Không tìm thấy thông tin vật tư!");
                            // Xóa dữ liệu trong các TextBox
                            txtCl.Text = "";
                            txtBanDc.Text = "";
                            txtTong.Text = "";
                        }
                    }
                }
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnXuatFile_Click(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "Excel Files|*.xlsx";
                    sfd.FileName = "ThongKe_KhachHang_VIP_" + txtTim.Text  + ".xlsx";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        FileInfo file = new FileInfo(sfd.FileName);
                        using (ExcelPackage package = new ExcelPackage(file))
                        {
                            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("ThongKeKhachHang");

                            // Đổ dữ liệu từ DataGridView vào Excel
                            worksheet.Cells[1, 1].Value = "tên vật liệu được tìm :";
                            worksheet.Cells[1, 2].Value = txtTim.Text;
                            worksheet.Cells[2, 1].Value = "Số lượng còn lại:";
                            worksheet.Cells[2, 2].Value = txtCl.Text;

                            worksheet.Cells[3, 1].Value = "Số lượng bán được:";
                            worksheet.Cells[3, 2].Value = txtBanDc.Text;

                            worksheet.Cells[4, 1].Value = "Tổng tiền bán được:";
                            worksheet.Cells[4, 2].Value = txtTong.Text;
                            // Thiết lập độ rộng của cột
                            worksheet.Column(1).Width = 30; // Cột 1 có độ rộng 20
                            worksheet.Column(2).AutoFit(); // Cột 2 tự động điều chỉnh độ rộng


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

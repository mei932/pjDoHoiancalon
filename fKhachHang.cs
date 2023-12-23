using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace QuanLyDoanhNghiepMililap
{
    public partial class fKhachHang : Form
    {
        // lưu lại chuôi kết nối 
        String connectString = @"Data Source=DELL\SQLEXPRESS;Initial Catalog=DoAn1Nhom3;Integrated Security=True";
        SqlConnection con;
        SqlCommand cmd; //thực hiện câu lệnh 
        SqlDataAdapter adt;
        DataTable dt; //đổ dữ liệu vào 

        public fKhachHang()
        {
            InitializeComponent();
        }


        private void fKhachHang_Load(object sender, EventArgs e)
        {
            // khởi tạo đối tượng 
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
                // Tạo câu lệnh SQL để lấy dữ liệu từ bảng NCC
                string query = "SELECT * FROM KhachHang";

                // Tạo đối tượng SqlCommand
                cmd = new SqlCommand(query, con);

                // Tạo đối tượng SqlDataAdapter để đổ dữ liệu vào DataTable
                adt = new SqlDataAdapter(cmd);
                dt = new DataTable();

                // Đổ dữ liệu vào DataTable
                adt.Fill(dt);

                // Đặt DataTable làm nguồn dữ liệu cho DataGridView
                dataGridViewKH.DataSource = dt;
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private bool KiemTraThongTin()
        {
            if (txtMaKhachHang.Text == "")
            {
                MessageBox.Show("Vui lòng điền mã khach hàng  ", "Thông báo");
                txtMaKhachHang.Focus();
                return false;
            }
            if (txtTenKhachHang.Text == "")
            {
                MessageBox.Show("Vui lòng điền tên Nhà cung cấp ", "Thông báo");
                txtTenKhachHang.Focus();
                return false;
            }
            if (txtDiaChiKhachHang.Text == "")
            {
                MessageBox.Show("Vui lòng điền địa chỉ nhà cung cấp ", "Thông báo");
                txtDiaChiKhachHang.Focus();
                return false;
            }
            if (txtSdtKhachHang.Text == "")
            {
                MessageBox.Show("Vui lòng điền số điện thoại nhà cung cấp ", "Thông báo");
                txtSdtKhachHang.Focus();
                return false;
            }
            return true;
        }

        private void ThemKhachHang_Click(object sender, EventArgs e)
        {
            if (KiemTraThongTin())
            {
                try
                {
                    // Tạo kết nối và command
                    using (SqlConnection conn = new SqlConnection(connectString))
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "SP_ThemKhachHang"; // Giả sử tên stored procedure là SP_ThemNhaCungCap

                        // Thêm tham số
                        cmd.Parameters.Add("@MaKhachHang", SqlDbType.NVarChar).Value = txtMaKhachHang.Text;
                        cmd.Parameters.Add("@TenKhachHang", SqlDbType.NVarChar).Value = txtTenKhachHang.Text;
                        cmd.Parameters.Add("@DiaChi", SqlDbType.NVarChar).Value = txtDiaChiKhachHang.Text;
                        cmd.Parameters.Add("@SoDienThoai", SqlDbType.NVarChar).Value = txtSdtKhachHang.Text;

                        // Mở kết nối và thực hiện stored procedure
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }

                    // Hiển thị thông báo và làm mới danh sách
                    MessageBox.Show("Thêm mới khách hàng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void SuaKhachHang_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaKhachHang.Text) || txtMaKhachHang.Text == "Thêm mới không cần ID")
            {
                MessageBox.Show("Vui lòng điền ma khách hàng .", "Thông báo");
                txtMaKhachHang.Focus();
                txtMaKhachHang.SelectAll();
            }
            else if (KiemTraThongTin())
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectString))
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "SP_SuaKhachHang";

                        // Thêm tham số
                        cmd.Parameters.Add("@MaKhachHang", SqlDbType.NVarChar).Value = txtMaKhachHang.Text;
                        cmd.Parameters.Add("@TenKhachHang", SqlDbType.NVarChar).Value = txtTenKhachHang.Text;
                        cmd.Parameters.Add("@DiaChi", SqlDbType.NVarChar).Value = txtDiaChiKhachHang.Text;
                        cmd.Parameters.Add("@SoDienThoai", SqlDbType.NVarChar).Value = txtSdtKhachHang.Text;

                        // Thực hiện kiểm tra thông tin trước khi mở kết nối
                        if (KiemTraThongTin())
                        {
                            // Mở kết nối và thực hiện stored procedure
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();
                            LoadData();
                            MessageBox.Show("Sửa khách hàng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void XoaKhachHang_Click(object sender, EventArgs e)
        {

            if (txtMaKhachHang.Text == "Thêm mới không cần ma" || string.IsNullOrEmpty(txtMaKhachHang.Text))
            {
                MessageBox.Show("Vui lòng điền mã khách hàng cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaKhachHang.Focus();
            }
            else
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectString))
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "SP_XoaKhachHang";
                        // Thêm tham số cho stored procedure
                        cmd.Parameters.Add("@MaKhachHang", SqlDbType.NVarChar).Value = txtMaKhachHang.Text;

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        LoadData();
                        MessageBox.Show("Xóa khách hàng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void TimKiemKhachHang_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectString))
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TimKiemMaKhachHang";

                    // Truyền giá trị tìm kiếm tổng quát từ người dùng
                    cmd.Parameters.Add("@MaKhachHang", SqlDbType.NVarChar).Value = txtMaKhachHang.Text;
                    conn.Open();

                    SqlDataAdapter adt = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adt.Fill(dt);

                    dataGridViewKH.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ThoatKhachHang_Click(object sender, EventArgs e)
        {
            DialogResult dg = MessageBox.Show("Bạn có chắc muốn thoát?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dg == DialogResult.OK)
            {
                // Ẩn form fNCC
                this.Hide();

                // Hiển thị form fChinh
                fChinh formChinh = new fChinh();
                formChinh.Show();
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}

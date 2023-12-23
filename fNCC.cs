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
    public partial class fNCC : Form
    {
        // lưu lại chuôi kết nối 
        String connectString = @"Data Source=DELL\SQLEXPRESS;Initial Catalog=DoAn1Nhom3;Integrated Security=True";
        SqlConnection con;
        SqlCommand cmd; //thực hiện câu lệnh 
        SqlDataAdapter adt;
        DataTable dt; //đổ dữ liệu vào 

        public fNCC()
        {
            InitializeComponent();
        }

        private void fNCC_Load(object sender, EventArgs e)
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
                string query = "SELECT * FROM NhaCungCap";

                // Tạo đối tượng SqlCommand
                cmd = new SqlCommand(query, con);

                // Tạo đối tượng SqlDataAdapter để đổ dữ liệu vào DataTable
                adt = new SqlDataAdapter(cmd);
                dt = new DataTable();

                // Đổ dữ liệu vào DataTable
                adt.Fill(dt);

                // Đặt DataTable làm nguồn dữ liệu cho DataGridView
                dataGridViewNCC.DataSource = dt;
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private bool KiemTraThongTin()
        {
            if (txtMaNCC.Text == "")
            {
                MessageBox.Show("Vui lòng điền mã nhà cung cấp ", "Thông báo");
                txtMaNCC.Focus();
                return false;
            }
            if (txtTenNCC.Text == "")
            {
                MessageBox.Show("Vui lòng điền tên Nhà cung cấp ", "Thông báo");
                txtTenNCC.Focus();
                return false;
            }
            if (txtDiaChiNCC.Text == "")
            {
                MessageBox.Show("Vui lòng điền địa chỉ nhà cung cấp ", "Thông báo");
                txtDiaChiNCC.Focus();
                return false;
            }
            if (txtSdtNCC.Text == "")
            {
                MessageBox.Show("Vui lòng điền số điện thoại nhà cung cấp ", "Thông báo");
                txtSdtNCC.Focus();
                return false;
            }
            return true;
        }

        private void ThemNCC_Click(object sender, EventArgs e)
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
                        cmd.CommandText = "SP_ThemNhaCungCap"; // Giả sử tên stored procedure là SP_ThemNhaCungCap

                        // Thêm tham số
                        cmd.Parameters.Add("@MaNCC", SqlDbType.NVarChar).Value = txtMaNCC.Text;
                        cmd.Parameters.Add("@TenNCC", SqlDbType.NVarChar).Value = txtTenNCC.Text;
                        cmd.Parameters.Add("@DiaChi", SqlDbType.NVarChar).Value = txtDiaChiNCC.Text;
                        cmd.Parameters.Add("@SoDienThoai", SqlDbType.NVarChar).Value = txtSdtNCC.Text;

                        // Mở kết nối và thực hiện stored procedure
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }

                    // Hiển thị thông báo và làm mới danh sách
                    MessageBox.Show("Thêm mới nhà cung cấp thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void SuaNCC_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaNCC.Text) || txtMaNCC.Text == "Thêm mới không cần ID")
            {
                MessageBox.Show("Vui lòng điền ID nhà cung cấp.", "Thông báo");
                txtMaNCC.Focus();
                txtMaNCC.SelectAll();
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
                        cmd.CommandText = "SP_SuaNhaCungCap";

                        cmd.Parameters.Add("@MaNCC", SqlDbType.NVarChar).Value = txtMaNCC.Text;
                        cmd.Parameters.Add("@TenNCC", SqlDbType.NVarChar).Value = txtTenNCC.Text;
                        cmd.Parameters.Add("@DiaChi", SqlDbType.NVarChar).Value = txtDiaChiNCC.Text;
                        cmd.Parameters.Add("@SoDienThoai", SqlDbType.NVarChar).Value = txtSdtNCC.Text;

                        // Thực hiện kiểm tra thông tin trước khi mở kết nối
                        if (KiemTraThongTin())
                        {
                            // Mở kết nối và thực hiện stored procedure
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();
                            LoadData();
                            MessageBox.Show("Sửa nhà cung cấp thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void XoaNCC_Click(object sender, EventArgs e)
        {
            if (txtMaNCC.Text == "Thêm mới không cần ID" || string.IsNullOrEmpty(txtMaNCC.Text))
            {
                MessageBox.Show("Vui lòng điền mã nhà cung cấp cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaNCC.Focus();
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
                        cmd.CommandText = "SP_XoaNhaCungCap";

                        // Thêm tham số cho stored procedure
                        cmd.Parameters.Add("@MaNCC", SqlDbType.NVarChar).Value = txtMaNCC.Text;

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        LoadData();
                        MessageBox.Show("Xóa nhà cung cấp thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }


        }

        private void TimKiemNCC_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectString))
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TimKiemMaNCC";

                    // Truyền giá trị tìm kiếm tổng quát từ người dùng
                    cmd.Parameters.Add("@MaNCC", SqlDbType.NVarChar).Value = txtMaNCC.Text;
                    conn.Open();

                    SqlDataAdapter adt = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adt.Fill(dt);

                    dataGridViewNCC.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ThoatNCC_Click(object sender, EventArgs e)
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
    }
}

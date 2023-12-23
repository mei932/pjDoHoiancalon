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
    public partial class fLoaiVT : Form
    {
        // lưu lại chuôi kết nối 
        String connectString = @"Data Source=DELL\SQLEXPRESS;Initial Catalog=DoAn1Nhom3;Integrated Security=True";
        SqlConnection con;
        SqlCommand cmd; //thực hiện câu lệnh 
        SqlDataAdapter adt;
        DataTable dt; //đổ dữ liệu vào 

        public fLoaiVT()
        {
            InitializeComponent();
        }

        private void fLoaiVT_Load(object sender, EventArgs e)
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
                string query = "SELECT * FROM LoaiVatTu";

                // Tạo đối tượng SqlCommand
                cmd = new SqlCommand(query, con);

                // Tạo đối tượng SqlDataAdapter để đổ dữ liệu vào DataTable
                adt = new SqlDataAdapter(cmd);
                dt = new DataTable();

                // Đổ dữ liệu vào DataTable
                adt.Fill(dt);


                // Đặt DataTable làm nguồn dữ liệu cho DataGridView
                dataGridViewLoaiVT.DataSource = dt;
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private bool KiemTraThongTin()
        {
            if (txtMaLoaiVatTu.Text == "")
            {
                MessageBox.Show("Vui lòng điền mã loại vật tư ", "Thông báo");
                txtMaLoaiVatTu.Focus();
                return false;
            }
            if (txtTenLoaiVatTu.Text == "")
            {
                MessageBox.Show("Vui lòng điền tên loại vật tư  ", "Thông báo");
                txtTenLoaiVatTu.Focus();
                return false;
            }
            return true;
        }

        private void ThemLoaiVT_Click(object sender, EventArgs e)
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
                        cmd.CommandText = "SP_ThemLoaiVatTu"; // Giả sử tên stored procedure là SP_ThemNhaCungCap

                        // Thêm tham số
                        cmd.Parameters.Add("@MaLoaiVatTu", SqlDbType.NVarChar).Value = txtMaLoaiVatTu.Text;
                        cmd.Parameters.Add("@TenLoai", SqlDbType.NVarChar).Value = txtTenLoaiVatTu.Text;

                        // Mở kết nối và thực hiện stored procedure
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }

                    // Hiển thị thông báo và làm mới danh sách
                    MessageBox.Show("Thêm mới loại vật tư thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void SuaLoaiVT_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaLoaiVatTu.Text) || txtMaLoaiVatTu.Text == "Thêm mới không cần ID")
            {
                MessageBox.Show("Vui lòng điền ID loai vat tu .", "Thông báo");
                txtMaLoaiVatTu.Focus();
                txtTenLoaiVatTu.SelectAll();
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
                        cmd.CommandText = "SP_SuaLoaiVatTu";

                        cmd.Parameters.Add("@MaLoaiVatTu", SqlDbType.NVarChar).Value = txtMaLoaiVatTu.Text;
                        cmd.Parameters.Add("@TenLoai", SqlDbType.NVarChar).Value = txtTenLoaiVatTu.Text;
                        
                        // Thực hiện kiểm tra thông tin trước khi mở kết nối
                        if (KiemTraThongTin())
                        {
                            // Mở kết nối và thực hiện stored procedure
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();
                            LoadData();
                            MessageBox.Show("Sửa loại vật tư thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void XoaLoaiVT_Click(object sender, EventArgs e)
        {
            if (txtMaLoaiVatTu.Text == "Thêm mới không cần ID" || string.IsNullOrEmpty(txtMaLoaiVatTu.Text))
            {
                MessageBox.Show("Vui lòng điền mã loại vật tư cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaLoaiVatTu.Focus();
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
                        cmd.CommandText = "SP_XoaLoaiVatTu";

                        // Thêm tham số cho stored procedure
                        cmd.Parameters.Add("@MaLoaiVatTu", SqlDbType.NVarChar).Value = txtMaLoaiVatTu.Text;

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        LoadData();
                        MessageBox.Show("Xóa loại vật tư thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void TimKiemLoaiVT_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectString))
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TimKiemMaLoaiVatTu";

                    // Truyền giá trị tìm kiếm tổng quát từ người dùng
                    cmd.Parameters.Add("@MaLoaiVatTu", SqlDbType.NVarChar).Value = txtMaLoaiVatTu.Text;
                    conn.Open();

                    SqlDataAdapter adt = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adt.Fill(dt);

                    dataGridViewLoaiVT.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ThoaiLoaiVT_Click(object sender, EventArgs e)
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

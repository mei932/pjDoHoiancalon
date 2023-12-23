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
    public partial class fDichVu : Form
    {
        // lưu lại chuôi kết nối 
        String connectString = @"Data Source=DELL\SQLEXPRESS;Initial Catalog=DoAn1Nhom3;Integrated Security=True";
        SqlConnection con;
        SqlCommand cmd; //thực hiện câu lệnh 
        SqlDataAdapter adt;
        DataTable dt; //đổ dữ liệu vào 
        public fDichVu()
        {
            InitializeComponent();
        }

        private void fDichVu_Load(object sender, EventArgs e)
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
                string query = "SELECT * FROM DichVu";

                // Tạo đối tượng SqlCommand
                cmd = new SqlCommand(query, con);

                // Tạo đối tượng SqlDataAdapter để đổ dữ liệu vào DataTable
                adt = new SqlDataAdapter(cmd);
                dt = new DataTable();

                // Đổ dữ liệu vào DataTable
                adt.Fill(dt);


                // Đặt DataTable làm nguồn dữ liệu cho DataGridView
                dataGridViewDV.DataSource = dt;
                con.Close();
                this.LoadMaLoaiDichVuToComboBox();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private bool KiemTraThongTin()
        {
            if (txtMaDV.Text == "")
            {
                MessageBox.Show("Vui lòng điền dich vu  ", "Thông báo");
                txtMaDV.Focus();
                return false;
            }
            if (txtTenDV.Text == "")
            {
                MessageBox.Show("Vui lòng điền tên dịch vụ   ", "Thông báo");
                txtTenDV.Focus();
                return false;
            }
            if (txtGiaBan.Text == "")
            {
                MessageBox.Show("Vui lòng điền giá dịch vụ  ", "Thông báo");
                txtGiaBan.Focus();
                return false;
            }
            return true;
        }
        public void LoadMaLoaiDichVuToComboBox()
        {
            try
            {
                con.Open();
                string query = "SELECT MaLoai, TenLoai FROM LoaiDichVu"; // Truy vấn lấy danh sách mã và tên loại vật tư từ bảng LoaiVatTu
                cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();

                List<KeyValuePair<string, string>> dataList = new List<KeyValuePair<string, string>>();
                while (reader.Read())
                {
                    // Lấy thông tin mã loại vật tư và tên vật tư
                    string maLoai = reader.GetString(0);
                    string tenLoai = reader.GetString(1);

                    // Đưa mã loại vật tư và tên vật tư vào ComboBox LoaiVatTu trong bảng VatTu
                    // Sử dụng KeyValuePair để lưu trữ cả mã và tên
                    dataList.Add(new KeyValuePair<string, string>(maLoai, tenLoai));
                }
                comboBoxMaLoaiDV.DataSource = dataList;
                // Đặt DisplayMember và ValueMember cho ComboBox
                comboBoxMaLoaiDV.DisplayMember = "Value"; // Hiển thị tên loại vật tư
                comboBoxMaLoaiDV.ValueMember = "Key"; // Lưu trữ mã loại vật tư

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void ThemDV_Click(object sender, EventArgs e)
        {
            if (KiemTraThongTin())
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectString))
                    using (SqlCommand cmd = new SqlCommand("SP_ThemDichVu", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@MaDichVu", SqlDbType.NVarChar).Value = txtMaDV.Text;
                        cmd.Parameters.Add("@TenDichVu", SqlDbType.NVarChar).Value = txtTenDV.Text;
                        cmd.Parameters.Add("@MaLoai", SqlDbType.NVarChar).Value = comboBoxMaLoaiDV.SelectedValue;
                        // Thay comboBoxMaLoaiVT.SelectedValue bằng giá trị thích hợp để truyền MaLoaiVatTu
                        cmd.Parameters.Add("@GiaDichVu", SqlDbType.Decimal).Value = decimal.Parse(txtGiaBan.Text);
                        

                        conn.Open();
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Thêm mới dịch vụ thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData(); // Assuming LoadData() is a method to refresh the data after insertion
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void SuaDV_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtMaDV.Text) || txtMaDV.Text == "Thêm mới không cần ID")
            {
                MessageBox.Show("Vui lòng điền ID vật tư.", "Thông báo");
                txtMaDV.Focus();
                txtMaDV.SelectAll();
                return; // Dừng thực hiện phương thức nếu thiếu thông tin
            }

            if (KiemTraThongTin())
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectString))
                    using (SqlCommand cmd = new SqlCommand("SP_SuaDichVu", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        conn.Open();
                        cmd.Parameters.Add("@MaDichVu", SqlDbType.NVarChar).Value = txtMaDV.Text;
                        cmd.Parameters.Add("@TenDichVu", SqlDbType.NVarChar).Value = txtTenDV.Text;
                        cmd.Parameters.Add("@MaLoai", SqlDbType.NVarChar).Value = comboBoxMaLoaiDV.SelectedValue;
                        // Thay comboBoxMaLoaiVT.SelectedValue bằng giá trị thích hợp để truyền MaLoaiVatTu
                        cmd.Parameters.Add("@GiaDichVu", SqlDbType.Decimal).Value = decimal.Parse(txtGiaBan.Text);

                        cmd.ExecuteNonQuery();
                        conn.Close();

                        LoadData(); // Cập nhật dữ liệu sau khi sửa
                        MessageBox.Show("Sửa dịch vụ  thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void XoaDV_Click(object sender, EventArgs e)
        {
            if (txtMaDV.Text == "Thêm mới không cần ID" || string.IsNullOrEmpty(txtMaDV.Text))
            {
                MessageBox.Show("Vui lòng điền mã vật tư cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaDV.Focus();
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
                        cmd.CommandText = "SP_XoaDichVu";

                        // Thêm tham số cho stored procedure
                        cmd.Parameters.Add("@MaDichVu", SqlDbType.NVarChar).Value = txtMaDV.Text;

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        LoadData();
                        MessageBox.Show("Xóa dich vu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }


        }

        private void TimKiemDV_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectString))
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TimKiemDichVu";

                    // Truyền giá trị tìm kiếm tổng quát từ người dùng
                    cmd.Parameters.Add("@MaDichVu", SqlDbType.NVarChar).Value = txtMaDV.Text;
                    conn.Open();

                    SqlDataAdapter adt = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adt.Fill(dt);

                    dataGridViewDV.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ThoatDV_Click(object sender, EventArgs e)
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

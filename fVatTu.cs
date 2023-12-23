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
    public partial class fVatTu : Form
    {
        // lưu lại chuôi kết nối 
        String connectString = @"Data Source=DELL\SQLEXPRESS;Initial Catalog=DoAn1Nhom3;Integrated Security=True";
        SqlConnection con;
        SqlCommand cmd; //thực hiện câu lệnh 
        SqlDataAdapter adt;
        DataTable dt; //đổ dữ liệu vào 
        public fVatTu()
        {
            InitializeComponent();
        }

        private void fVatTu_Load(object sender, EventArgs e)
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
                string query = "SELECT * FROM VatTu";

                // Tạo đối tượng SqlCommand
                cmd = new SqlCommand(query, con);

                // Tạo đối tượng SqlDataAdapter để đổ dữ liệu vào DataTable
                adt = new SqlDataAdapter(cmd);
                dt = new DataTable();
                
                // Đổ dữ liệu vào DataTable
                adt.Fill(dt);


                // Đặt DataTable làm nguồn dữ liệu cho DataGridView
                dataGridViewVT.DataSource = dt;
                con.Close();
                this.LoadMaLoaiVatTuToComboBox();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private bool KiemTraThongTin()
        {
            if (txtMaVT.Text == "")
            {
                MessageBox.Show("Vui lòng điền vật tư ", "Thông báo");
                txtMaVT.Focus();
                return false;
            }
            if (txtTenVT.Text == "")
            {
                MessageBox.Show("Vui lòng điền tên vật tư  ", "Thông báo");
                txtTenVT.Focus();
                return false;
            }
            if (txtGiaNhap.Text == "")
            {
                MessageBox.Show("Vui lòng điền giá nhập vật tư ", "Thông báo");
                txtMaVT.Focus();
                return false;
            }
            if (txtGiaBan.Text == "")
            {
                MessageBox.Show("Vui lòng điền giá bán vật tư  ", "Thông báo");
                txtGiaBan.Focus();
                return false;
            }
            if (txtSoLuongTonKHo.Text == "")
            {
                MessageBox.Show("Vui lòng điền số lượng tồn kho vật tư  ", "Thông báo");
                txtSoLuongTonKHo.Focus();
                return false;
            }
            return true;
        }

        public void LoadMaLoaiVatTuToComboBox()
        {
            try
            {
                con.Open();
                string query = "SELECT MaLoaiVatTu, TenLoai FROM LoaiVatTu"; // Truy vấn lấy danh sách mã và tên loại vật tư từ bảng LoaiVatTu
                cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();

                List<KeyValuePair<string, string>> dataList = new List<KeyValuePair<string, string>>();
                while (reader.Read())
                {
                    // Lấy thông tin mã loại vật tư và tên vật tư
                    string maLoaiVatTu = reader.GetString(0);
                    string tenLoaiVatTu = reader.GetString(1);

                    // Đưa mã loại vật tư và tên vật tư vào ComboBox LoaiVatTu trong bảng VatTu
                    // Sử dụng KeyValuePair để lưu trữ cả mã và tên
                    dataList.Add(new KeyValuePair<string, string>(maLoaiVatTu, tenLoaiVatTu));
                }
                comboBoxMaLoaiVT.DataSource = dataList;
                // Đặt DisplayMember và ValueMember cho ComboBox
                comboBoxMaLoaiVT.DisplayMember = "Value"; // Hiển thị tên loại vật tư
                comboBoxMaLoaiVT.ValueMember = "Key"; // Lưu trữ mã loại vật tư

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void ThemVT_Click(object sender, EventArgs e)
        {
            if (KiemTraThongTin())
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectString))
                    using (SqlCommand cmd = new SqlCommand("SP_ThemVatTu", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@MaVatTu", SqlDbType.NVarChar).Value = txtMaVT.Text;
                        cmd.Parameters.Add("@TenVatTu", SqlDbType.NVarChar).Value = txtTenVT.Text;
                        cmd.Parameters.Add("@MaLoaiVatTu", SqlDbType.NVarChar).Value = comboBoxMaLoaiVT.SelectedValue;
                        // Thay comboBoxMaLoaiVT.SelectedValue bằng giá trị thích hợp để truyền MaLoaiVatTu
                        cmd.Parameters.Add("@GiaNhap", SqlDbType.Decimal).Value = decimal.Parse(txtGiaNhap.Text);
                        cmd.Parameters.Add("@GiaBan", SqlDbType.Decimal).Value = decimal.Parse(txtGiaBan.Text);
                        cmd.Parameters.Add("@SoLuongTonKho", SqlDbType.Int).Value = int.Parse(txtSoLuongTonKHo.Text);

                        conn.Open();
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Thêm mới vật tư thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData(); // Assuming LoadData() is a method to refresh the data after insertion
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void SuaVT_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaVT.Text) || txtMaVT.Text == "Thêm mới không cần ID")
            {
                MessageBox.Show("Vui lòng điền ID vật tư.", "Thông báo");
                txtMaVT.Focus();
                txtMaVT.SelectAll();
                return; // Dừng thực hiện phương thức nếu thiếu thông tin
            }

            if (KiemTraThongTin())
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectString))
                    using (SqlCommand cmd = new SqlCommand("SP_SuaVatTu", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        conn.Open();
                        cmd.Parameters.Add("@MaVatTu", SqlDbType.NVarChar).Value = txtMaVT.Text;
                        cmd.Parameters.Add("@TenVatTu", SqlDbType.NVarChar).Value = txtTenVT.Text;
                        cmd.Parameters.Add("@MaLoaiVatTu", SqlDbType.NVarChar).Value = comboBoxMaLoaiVT.SelectedValue;
                        cmd.Parameters.Add("@GiaNhap", SqlDbType.Decimal).Value = decimal.Parse(txtGiaNhap.Text);
                        cmd.Parameters.Add("@GiaBan", SqlDbType.Decimal).Value = decimal.Parse(txtGiaBan.Text);
                        cmd.Parameters.Add("@SoLuongTonKho", SqlDbType.Int).Value = int.Parse(txtSoLuongTonKHo.Text);

                        cmd.ExecuteNonQuery();
                        conn.Close();

                        LoadData(); // Cập nhật dữ liệu sau khi sửa
                        MessageBox.Show("Sửa vật tư thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void XoaVT_Click(object sender, EventArgs e)
        {
            if (txtMaVT.Text == "Thêm mới không cần ID" || string.IsNullOrEmpty(txtMaVT.Text))
            {
                MessageBox.Show("Vui lòng điền mã vật tư cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaVT.Focus();
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
                        cmd.CommandText = "SP_XoaVatTu";

                        // Thêm tham số cho stored procedure
                        cmd.Parameters.Add("@MaVatTu", SqlDbType.NVarChar).Value = txtMaVT.Text;

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        LoadData();
                        MessageBox.Show("Xóa vật tư thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }


        }

        private void TimKiemVT_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectString))
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TimKiemVatTu";

                    // Truyền giá trị tìm kiếm tổng quát từ người dùng
                    cmd.Parameters.Add("@MaVatTu", SqlDbType.NVarChar).Value = txtMaVT.Text;
                    conn.Open();

                    SqlDataAdapter adt = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adt.Fill(dt);

                    dataGridViewVT.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ThoatVT_Click(object sender, EventArgs e)
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

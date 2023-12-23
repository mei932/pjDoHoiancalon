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
using System.Data.SqlClient;
namespace QuanLyDoanhNghiepMililap
{
    public partial class fPhieuChi : Form
    {
        // lưu lại chuôi kết nối 
        String connectString = @"Data Source=DESKTOP-7FKFR8T;Initial Catalog=DoAn1Nhom3;Integrated Security=True";
        SqlConnection con;
        SqlCommand cmd; //thực hiện câu lệnh 
        SqlDataAdapter adt;
        DataTable dt; //đổ dữ liệu vào 
        public fPhieuChi()
        {
            InitializeComponent();
        }

        private void fPhieuChi_Load(object sender, EventArgs e)
        {
            // khởi tạo đối tượng 
            con = new SqlConnection(connectString);
            // mở kết nối 
            con.Open();
            // Load dữ liệu từ SQL Server vào DataTable
            LoadData();
            con.Close();
        }

        private void LoadData()
        {
            try
            {
                // Load data for ComboBox MaPhieuChi
                string queryPhieuChi = "SELECT MaPhieuChi FROM PhieuChi";
                using (SqlCommand cmd = new SqlCommand(queryPhieuChi, con))
                {
                    using (SqlDataAdapter adt = new SqlDataAdapter(cmd))
                    {
                        DataTable dtPhieuChi = new DataTable();
                        adt.Fill(dtPhieuChi);
                        comboBoxMaDDHPC.DataSource = dtPhieuChi;
                        comboBoxMaDDHPC.DisplayMember = "MaPhieuChi";
                        comboBoxMaDDHPC.ValueMember = "MaPhieuChi";
                    }
                }

                // Load data for ComboBox MaNCC
                string queryNhaCungCap = "SELECT MaNCC, TenNCC FROM NhaCungCap";
                using (SqlCommand cmd = new SqlCommand(queryNhaCungCap, con))
                {
                    using (SqlDataAdapter adt = new SqlDataAdapter(cmd))
                    {
                        DataTable dtNhaCungCap = new DataTable();
                        adt.Fill(dtNhaCungCap);
                        comboBoxMaNCCPC.DataSource = dtNhaCungCap;
                        comboBoxMaNCCPC.DisplayMember = "TenNCC"; // Display supplier names
                        comboBoxMaNCCPC.ValueMember = "MaNCC"; // Store supplier IDs
                    }
                }

                // Load other data (if needed)...
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }
        public void LoadMaNhaCungCapToComboBox()
        {
            try
            {
                con.Open();
                string query = "SELECT MaNCC, TenNCC FROM NhaCungCap";
                cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();

                List<KeyValuePair<string, string>> dataList = new List<KeyValuePair<string, string>>();
                while (reader.Read())
                {
                    string maNCC = reader.GetString(0);
                    string tenNCC = reader.GetString(1);

                    dataList.Add(new KeyValuePair<string, string>(maNCC, tenNCC));
                }
                comboBoxMaNCCPC.DataSource = dataList;
                comboBoxMaNCCPC.DisplayMember = "Value"; // Display supplier names
                comboBoxMaNCCPC.ValueMember = "Key"; // Store supplier IDs
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        private void comboBoxMaNCCPC_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                con.Open();

                // Lấy dữ liệu từ ComboBox
                KeyValuePair<string, string> selectedNCC = (KeyValuePair<string, string>)comboBoxMaNCCPC.SelectedItem;
                string maNCC = selectedNCC.Key;

                // Truy vấn SQL để lấy Địa chỉ và Số điện thoại từ Mã Nhà cung cấp
                string query = "SELECT DiaChi, SoDienThoai FROM NhaCungCap WHERE MaNCC = @MaNCC";
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    command.Parameters.AddWithValue("@MaNCC", maNCC);
                    SqlDataReader reader = command.ExecuteReader();

                    // Hiển thị thông tin Địa chỉ và Số điện thoại
                    if (reader.Read())
                    {
                        string diaChi = reader.GetString(0);
                        string sdt = reader.GetString(1);

                        // Hiển thị thông tin địa chỉ và số điện thoại tương ứng vào các TextBox
                        txtDiaChiNCCPC.Text = diaChi;
                        txtSDTNCCPC.Text = sdt; // Chỉnh sửa tên control của TextBox số điện thoại
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }
        public void LoadMaPhieuChiToComboBox()
        {
            try
            {
                con.Open();
                string query = "SELECT MaPhieuChi FROM PhieuChi"; // Truy vấn lấy danh sách mã đơn đặt hàng từ bảng DonDatHang
                using (SqlCommand cmdPhieuChi = new SqlCommand(query, con))
                {
                    SqlDataReader reader = cmdPhieuChi.ExecuteReader();

                    List<KeyValuePair<string, string>> dataList = new List<KeyValuePair<string, string>>();
                    while (reader.Read())
                    {
                        // Lấy thông tin mã đơn đặt hàng
                        string maDDH = reader.GetString(0);

                        // Đưa mã đơn đặt hàng vào ComboBox MaDDH trong bảng DonDatHang
                        // Sử dụng KeyValuePair để lưu trữ cả mã và tên
                        dataList.Add(new KeyValuePair<string, string>(maDDH, maDDH));
                    }
                    comboBoxMaDDHPC.DataSource = dataList;
                    // Đặt DisplayMember và ValueMember cho ComboBox
                    comboBoxMaDDHPC.DisplayMember = "Value"; // Hiển thị mã đơn đặt hàng
                    comboBoxMaDDHPC.ValueMember = "Key"; // Lưu trữ mã đơn đặt hàng

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        private void comboBoxMaDDHPC_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                string maPhieuChi = comboBoxMaDDHPC.SelectedValue.ToString();

                // Truy vấn để lấy dữ liệu cho DataGridView
                string queryDataGridView = "SELECT ChiTiet_PhieuChi.MaVatTu, VatTu.TenVatTu, ChiTiet_PhieuChi.SoLuong " +
                                           "FROM ChiTiet_PhieuChi " +
                                           "INNER JOIN VatTu ON ChiTiet_PhieuChi.MaVatTu = VatTu.MaVatTu " +
                                           "WHERE ChiTiet_PhieuChi.MaPhieuChi = @MaPhieuChi";

                using (SqlCommand cmdDataGridView = new SqlCommand(queryDataGridView, con))
                {
                    cmdDataGridView.Parameters.AddWithValue("@MaPhieuChi", maPhieuChi);
                    using (SqlDataAdapter adtDataGridView = new SqlDataAdapter(cmdDataGridView))
                    {
                        DataTable dt = new DataTable();
                        adtDataGridView.Fill(dt);

                        dataGridViewPC.DataSource = dt;
                    }
                }

                // Truy vấn để lấy thông tin từ Mã đơn đặt hàng
                string queryDonDatHang = "SELECT PhieuChi.MaNCC, NhaCungCap.DiaChi, NhaCungCap.SoDienThoai, PhieuChi.NgayNhap " +
                                            "FROM PhieuChi " +
                                            "INNER JOIN DonDatHang ON PhieuChi.MaDonDatHang = DonDatHang.MaDonDatHang " +
                                            "INNER JOIN NhaCungCap ON DonDatHang.MaNCC = NhaCungCap.MaNCC " +
                                            "WHERE PhieuChi.MaPhieuChi = @MaPhieuChi";

                using (SqlCommand cmdDonDatHang = new SqlCommand(queryDonDatHang, con))
                {
                    cmdDonDatHang.Parameters.AddWithValue("@MaPhieuChi", maPhieuChi);
                    SqlDataReader reader = cmdDonDatHang.ExecuteReader();

                    if (reader.Read())
                    {
                        string maNCC = reader["MaNCC"].ToString();
                        string diaChi = reader["DiaChi"].ToString();
                        string soDienThoai = reader["SoDienThoai"].ToString();
                        string ngayNhap = reader["NgayNhap"].ToString();

                        comboBoxMaDDHPC.SelectedValue = maNCC; // Có thể xảy ra xung đột gán giá trị ở đây
                        txtDiaChiNCCPC.Text = diaChi;
                        txtSDTNCCPC.Text = soDienThoai;
                        txtNgayNhapPC.Text = ngayNhap;
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        public void LoadMaVatTuPCComboBox()
        {
            try
            {
                con.Open();
                string query = "SELECT MaVatTu FROM VatTu";
                using (SqlCommand cmdMaVatTu = new SqlCommand(query, con))
                {
                    SqlDataReader reader = cmdMaVatTu.ExecuteReader();

                    List<KeyValuePair<string, string>> dataList = new List<KeyValuePair<string, string>>();
                    while (reader.Read())
                    {
                        string maVT = reader.GetString(0);
                        dataList.Add(new KeyValuePair<string, string>(maVT, maVT));
                    }

                    comboBoxMaVTPC.DataSource = dataList;
                    comboBoxMaVTPC.DisplayMember = "Value";
                    comboBoxMaVTPC.ValueMember = "Key";

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        

    }
}

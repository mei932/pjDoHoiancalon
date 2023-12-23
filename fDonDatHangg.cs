using OfficeOpenXml;
using QuanLyDoanhNghiepMililap.Modify;
using QuanLyDoanhNghiepMililap.Object;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;


namespace QuanLyDoanhNghiepMililap
{
    public partial class fDonDatHangg : Form
    {
        // lưu lại chuôi kết nối 
        modifiall modifiall = new modifiall();
        chitiet ct;
        dondathang ddh;
        public fDonDatHangg()
        {
            InitializeComponent();
        }
       

        private void fDonDatHangg_Load(object sender, EventArgs e)
        {
            LoadData();
            ToTal();
        }
        private void LoadData()
        {
            SqlConnection conn = connection.GetSqlConnection();
            try
            {
                // Lấy dữ liệu cho ComboBox MaDonDatHang
             
                string queryDonDatHang = "SELECT MaDonDatHang FROM DonDatHang";
                using (SqlCommand cmd = new SqlCommand(queryDonDatHang, conn))
                {
                    using (SqlDataAdapter adt = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adt.Fill(dt);
                        comboBoxMaDDH.DataSource = dt;
                        comboBoxMaDDH.DisplayMember = "MaDonDatHang";
                        comboBoxMaDDH.ValueMember = "MaDonDatHang";
                    }
                }
                // Gán dữ liệu cho ComboBox MaNhaCungCap
                string queryNhaCungCap = "SELECT MaNCC FROM NhaCungCap";
                using (SqlCommand cmd = new SqlCommand(queryNhaCungCap, conn))
                {
                    using (SqlDataAdapter adt = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adt.Fill(dt);
                        cbMNCC.DataSource = dt;
                        cbMNCC.DisplayMember = "MaNCC";
                        cbMNCC.ValueMember = "MaNCC";
                    }
                }
                

                this.LoadMaNhaCungCapToComboBox();
                conn.Close();
                this.LoadMaDonDatHangToComboBox();
                conn.Close();
                this.LoadMaVatTuToComboBox();
                conn.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            conn.Close();
            
        }

        /*   private void ToTal()
           {
               string maDonDatHang = comboBoxMaDDH.SelectedValue.ToString();

               SqlConnection conn = connection.GetSqlConnection();
               string query = "SELECT SUM((CH.SoLuong * VT.GiaBan) + DV.GiaDichVu) AS TongThanhTien FROM ChiTiet_HoaDon CH INNER JOIN VatTu VT ON CH.MaVatTu = VT.MaVatTu INNER JOIN DichVu DV ON CH.MaDichVu = DV.MaDichVu INNER JOIN HoaDon HD ON CH.MaHoaDon = HD.MaHoaDon INNER JOIN KhachHang KH ON HD.MaKhachHang = KH.MaKhachHang WHERE CH.MaHoaDon = @MaHD GROUP BY CH.MaHoaDon";
               SqlCommand cmd = new SqlCommand(query, conn);
               cmd.Parameters.AddWithValue("@MaHD", maDonDatHang);

               conn.Open();
               SqlDataReader reader = cmd.ExecuteReader();

               if (reader.Read())
               {
                   if (!reader.IsDBNull(reader.GetOrdinal("TongThanhTien")))
                   {
                       txtToTal.Text = reader["TongThanhTien"].ToString();
                   }
                   else
                   {
                       // Xử lý trường hợp giá trị là NULL (nếu cần)
                       txtToTal.Text = "0"; // hoặc có thể gán giá trị mặc định khác
                   }
               }
               else
               {
                   // Xử lý trường hợp không có dữ liệu trả về (nếu cần)
               }

               conn.Close();


           }*/

        private void ToTal()
        {
            string maDonDatHang = comboBoxMaDDH.SelectedValue.ToString();

            using (SqlConnection conn = connection.GetSqlConnection())
            {
                string query = "SELECT SUM(CT.SoLuong * VT.GiaNhap) AS TongThanhTien FROM ChiTiet_DonDatHang CT INNER JOIN VatTu VT ON CT.MaVatTu = VT.MaVatTu INNER JOIN DonDatHang DDH ON CT.MaDonDatHang = DDH.MaDonDatHang WHERE DDH.MaDonDatHang = @MaDDH";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaDDH", maDonDatHang);
                    conn.Open();

                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        /* txtToTal.Text = result.ToString();*/
                        decimal tongThanhTien;
                        if (decimal.TryParse(result.ToString(), out tongThanhTien))
                        {
                            txtToTal.Text = tongThanhTien.ToString("#,##0 VND");
                        }
                        else
                        {
                            // Xử lý trường hợp không chuyển đổi được sang decimal
                            txtToTal.Text = "0 VND"; // hoặc giá trị mặc định khác
                        }
                    }
                    else
                    {
                        // Xử lý trường hợp không có dữ liệu trả về (nếu cần)
                        txtToTal.Text = "0"; // hoặc giá trị mặc định khác
                    }
                }
            }
        }


        private bool KiemTraThongTin()
        {
            if (txtNgayDat.Text == "")
            {
                MessageBox.Show("Vui lòng điền ngày đặt hàng  ", "Thông báo");
                txtNgayDat.Focus();
                return false;
            }
            if (txtSLNhap.Text == "")
            {
                MessageBox.Show("Vui lòng điền số lương vật tư   ", "Thông báo");
                txtSLNhap.Focus();
                return false;
            }
            
            return true;
        }
        public void LoadMaNhaCungCapToComboBox()
        {
            SqlConnection conn = connection.GetSqlConnection();
            try
            {
                conn.Open();
                string query = "SELECT MaNCC, TenNCC FROM NhaCungCap"; // Truy vấn lấy danh sách mã và tên loại vật tư từ bảng LoaiVatTu
                SqlCommand cmd  = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                List<KeyValuePair<string, string>> dataList = new List<KeyValuePair<string, string>>();
                while (reader.Read())
                {
                    // Lấy thông tin mã loại vật tư và tên vật tư
                    string maNCC = reader.GetString(0);
                    string tenNCC = reader.GetString(1);

                    // Đưa mã loại vật tư và tên vật tư vào ComboBox LoaiVatTu trong bảng VatTu
                    // Sử dụng KeyValuePair để lưu trữ cả mã và tên
                    dataList.Add(new KeyValuePair<string, string>(maNCC, tenNCC));
                }
                cbMNCC.DataSource = dataList;
                // Đặt DisplayMember và ValueMember cho ComboBox
                cbMNCC.DisplayMember = "Value"; // Hiển thị tên loại vật tư
                cbMNCC.ValueMember = "Key"; // Lưu trữ mã loại vật tư
                reader.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        private void comboBoxMaNCCDDH_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection conn = connection.GetSqlConnection();
            try
            {
                conn.Open();

                // Lấy dữ liệu từ ComboBox
                KeyValuePair<string, string> selectedNCC = (KeyValuePair<string, string>)cbMNCC.SelectedItem;
                string maNCC = selectedNCC.Key;

                // Truy vấn SQL để lấy Địa chỉ và Số điện thoại từ Mã Nhà cung cấp
                string query = "SELECT DiaChi, SoDienThoai FROM NhaCungCap WHERE MaNCC = @MaNCC";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaNCC", maNCC);
                SqlDataReader reader = cmd.ExecuteReader();

                // Hiển thị thông tin Địa chỉ và Số điện thoại
                if (reader.Read())
                {
                    string diaChi = reader.GetString(0);
                    string sdt = reader.GetString(1);

                    // Hiển thị thông tin địa chỉ và số điện thoại tương ứng vào các TextBox
                    txtDiaChiNCCDDH.Text = diaChi;
                    txtSDTNCCDDH.Text = sdt;
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            ToTal();
        }

        public void LoadMaDonDatHangToComboBox()
        {
            SqlConnection conn = connection.GetSqlConnection();
            try
            {
                conn.Open();
                string query = "SELECT MaDonDatHang FROM DonDatHang"; // Truy vấn lấy danh sách mã đơn đặt hàng từ bảng DonDatHang
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                List<KeyValuePair<string, string>> dataList = new List<KeyValuePair<string, string>>();
                while (reader.Read())
                {
                    // Lấy thông tin mã đơn đặt hàng
                    string maDDH = reader.GetString(0);

                    // Đưa mã đơn đặt hàng vào ComboBox MaDDH trong bảng DonDatHang
                    // Sử dụng KeyValuePair để lưu trữ cả mã và tên
                    dataList.Add(new KeyValuePair<string, string>(maDDH, maDDH));
                }
                comboBoxMaDDH.DataSource = dataList;
                // Đặt DisplayMember và ValueMember cho ComboBox
                comboBoxMaDDH.DisplayMember = "Value"; // Hiển thị mã đơn đặt hàng
                comboBoxMaDDH.ValueMember = "Key"; // Lưu trữ mã đơn đặt hàng

                reader.Close();
            }
            
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }
        private void comboBoxMaDDH_SelectedIndexChanged(object sender, EventArgs e)
        {
            string maDonDatHang = comboBoxMaDDH.SelectedValue.ToString();

            SqlConnection conn = connection.GetSqlConnection();
            try
            {
                conn.Open();

                string query = @"SELECT CT.MaVatTu, VT.TenVatTu, CT.SoLuong, CT.SoLuong * VT.GiaNhap AS ThanhTien FROM ChiTiet_DonDatHang CT INNER JOIN VatTu VT ON CT.MaVatTu = VT.MaVatTu INNER JOIN DonDatHang DDH ON CT.MaDonDatHang = DDH.MaDonDatHang WHERE DDH.MaDonDatHang =@MaDonDatHang ";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaDonDatHang", maDonDatHang);

                SqlDataReader reader = cmd.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(reader);

                dataGridViewDDH.DataSource = dt;

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            ToTal();
        }
        public void LoadMaVatTuToComboBox()
        {
            SqlConnection conn = connection.GetSqlConnection();
            try
            {
                conn.Open();
                string query = "SELECT MaVatTu FROM VatTu"; // Truy vấn lấy danh sách mã đơn đặt hàng từ bảng DonDatHang
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                List<KeyValuePair<string, string>> dataList = new List<KeyValuePair<string, string>>();
                while (reader.Read())
                {
                    // Lấy thông tin mã đơn đặt hàng
                    string maVT = reader.GetString(0);

                    // Đưa mã đơn đặt hàng vào ComboBox MaDDH trong bảng DonDatHang
                    // Sử dụng KeyValuePair để lưu trữ cả mã và tên
                    dataList.Add(new KeyValuePair<string, string>(maVT, maVT));
                }
                cbMMVT.DataSource = dataList;
                // Đặt DisplayMember và ValueMember cho ComboBox
                cbMMVT.DisplayMember = "Value"; // Hiển thị mã đơn đặt hàng
                cbMMVT.ValueMember = "Key"; // Lưu trữ mã đơn đặt hàng
                reader.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        

        private void XoaDDH_Click(object sender, EventArgs e)
        {
            if (dataGridViewDDH.SelectedRows.Count > 0) // Kiểm tra xem có hàng nào được chọn không
            {
                string choose = dataGridViewDDH.SelectedRows[0].Cells[0].Value.ToString();// Thay "TenCotMaDonDatHangHoacMaVatTu" bằng tên cột tương ứng trong DataGridView

                string query = "DELETE FROM ChiTiet_DonDatHang WHERE MaDonDatHang = @choose OR MaVatTu = @choose";

                try
                {
                    if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        using (SqlConnection con = connection.GetSqlConnection())
                        {
                            using (SqlCommand cmd = new SqlCommand(query, con))
                            {
                                cmd.Parameters.AddWithValue("@choose", choose);

                                con.Open();
                                int rowsAffected = cmd.ExecuteNonQuery();
                                con.Close();

                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Bạn đã xóa thành công!");
                                    LoadData();
                                }
                                else
                                {
                                    MessageBox.Show("Không có bản ghi nào được xóa!");
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn hàng để xóa!");
            }
        }

        private void comboBoxMaVTDDH_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection conn = connection.GetSqlConnection();
            try
            {
                conn.Open();

                // Lấy giá trị MaVatTu từ ComboBox
                string maVatTu = cbMMVT.SelectedValue.ToString();

                // Truy vấn SQL để lấy giá nhập từ MaVatTu trong bảng VatTu
                string query = "SELECT GiaNhap FROM VatTu WHERE MaVatTu = @MaVatTu";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaVatTu", maVatTu);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    // Hiển thị giá nhập lên TextBox
                    txtGiaNhap.Text = reader["GiaNhap"].ToString();
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }
        public void Getvaluetextbox()
        {
           
            string MADH = comboBoxMaDDH.Text;
            string MVT = cbMMVT.Text;
            int SL = int.Parse(txtSLNhap.Text);
            ct = new chitiet( MADH, MVT, SL);

            string ID = comboBoxMaDDH.Text;
            string NCC = cbMNCC.Text;
            string day = txtNgayDat.Text;
            ddh = new dondathang(ID, NCC, day);
        }

        public bool CheckValue()
        {
            if (string.IsNullOrWhiteSpace(comboBoxMaDDH.Text) || string.IsNullOrWhiteSpace(cbMNCC.Text) ||
                string.IsNullOrWhiteSpace(txtNgayDat.Text) ||
                string.IsNullOrWhiteSpace(cbMNCC.Text) ||
                string.IsNullOrWhiteSpace(txtSLNhap.Text)
                )

            {
                MessageBox.Show("Mời bạn nhập đầy đủ thông tin!");
                return false;
            }

            return true;
        }

        private void ThemDDH_Click(object sender, EventArgs e)
        {
            if (CheckValue()) // Kiểm tra dữ liệu đã nhập
            {
                SqlConnection conn = connection.GetSqlConnection();

                /*string sqlCheck = "SELECT COUNT(*) FROM DonDatHang WHERE MaDonDatHang = @ID";
                SqlCommand cmdCheck = new SqlCommand(sqlCheck, conn);
                cmdCheck.Parameters.AddWithValue("@ID", comboBoxMaDDH.Text);

                try
                {
                    conn.Open();
                    int count = (int)cmdCheck.ExecuteScalar();
                    if (count > 0)
                    {
                        MessageBox.Show("Mã đơn đặt đã tồn tại!");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kiểm tra mã : " + ex.Message);
                    return;
                }
                finally
                {
                    conn.Close();
                }*/

                Getvaluetextbox();

              /*  string query = "INSERT INTO DonDatHang (MaDonDatHang, MaNCC, NgayDat) " +
                               "VALUES (@ID,@NCC, @day)";
                SqlCommand insert = new SqlCommand(query, conn);
                insert.Parameters.AddWithValue("@ID", ddh.MADDHProperty);
                insert.Parameters.AddWithValue("@NCC", ddh.MaNCCProperty);
                insert.Parameters.AddWithValue("@day", ddh.NgayDatProperty);*/


                string query1 = "INSERT  INTO ChiTiet_DonDatHang ( MaDonDatHang, MaVatTu, SoLuong) " +
                              "VALUES (@I,@M, @SL)";
                SqlCommand insert1 = new SqlCommand(query1, conn);
                insert1.Parameters.AddWithValue("@I", ct.MADDHProperty);
                insert1.Parameters.AddWithValue("@M", ct.MaVTProperty);
                insert1.Parameters.AddWithValue("@SL", ct.soLuongProperty);

                try
                {
                    conn.Open();
                    insert1.ExecuteNonQuery();
                    MessageBox.Show("Thêm thành công!");

                    // 1. Cập nhật DataGridView sau khi thêm mới
                    LoadData(); // Hoặc hàm tương tự để tải lại dữ liệu

                    // 2. Cập nhật lại tổng số sau khi thêm mới
                    ToTal(); // Gọi hàm để cập nhật tổng số

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi thêm: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }

        }
       
        private void btnXuatFile_Click(object sender, EventArgs e)
        {
           

        }

        private void butSuaDDH_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBoxMaDDH.Text )|| string.IsNullOrEmpty(cbMMVT.Text))
            {
                MessageBox.Show("Vui lòng nhập mã hoa don, hoặc mã vật tư để sửa");
                return;
            }
            if (CheckValue())
            {
                SqlConnection con = connection.GetSqlConnection();
                string sql = "SELECT count(*) FROM ChiTiet_DonDatHang WHERE MaDonDatHang = @DDH OR MaVatTu = @MVT";
                SqlCommand sqlCmd = new SqlCommand(sql, con);
                sqlCmd.Parameters.AddWithValue("@DDH", comboBoxMaDDH.Text);
                sqlCmd.Parameters.AddWithValue("@MVT", cbMMVT.Text);

                con.Open();
                int count = (int)sqlCmd.ExecuteScalar();
                if (count == 0)
                {
                    MessageBox.Show("Mã hóa đơn bán không tồn tại!");
                    return;
                }

                Getvaluetextbox();
                string update = "UPDATE ChiTiet_DonDatHang SET  SoLuong= @SL WHERE MaDonDatHang = @DDH OR MaVatTu = @MVT ";
                SqlCommand udt = new SqlCommand(update, con);
                //udt.Parameters.AddWithValue("@ID", CTBH.MaHDBProperty);
                udt.Parameters.AddWithValue("@DDH", ct.MADDHProperty);
                udt.Parameters.AddWithValue("@MVT", ct.MaVTProperty);
                udt.Parameters.AddWithValue("@SL", ct.soLuongProperty);


                try
                {
                    if (MessageBox.Show("Bạn có muốn sửa lại dữ liệu không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        udt.ExecuteNonQuery();
                        MessageBox.Show("Bạn đã sửa thông tin thành công!");
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi sửa: " + ex.Message);
                }

            }
        }

        private void dataGridViewDDH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewDDH.Rows.Count >= 0)
            {
                cbMMVT.Text = dataGridViewDDH.SelectedRows[0].Cells[0].Value.ToString();
                txtSLNhap.Text = dataGridViewDDH.SelectedRows[0].Cells[2].Value.ToString();
                
            }
        }

        private void TimKiemDDH_Click(object sender, EventArgs e)
        {

        }

        private void btnXuatFILE_Click_1(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "Excel Files|*.xlsx";
                    sfd.FileName = "ThongKe_DonDatHang_" + comboBoxMaDDH.Text + ".xlsx";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        FileInfo file = new FileInfo(sfd.FileName);
                        using (ExcelPackage package = new ExcelPackage(file))
                        {
                            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("ChiTietDonDatHang");
                            worksheet.Cells["D1"].Value = "DANH SÁCH HÓA ĐƠN NHẬP HÀNG";
                            worksheet.Cells["D1"].Style.Font.Bold = true;
                            worksheet.Cells["D1"].Style.Font.Size = 16;
                            worksheet.Cells["D1"].Style.Font.Color.SetColor(System.Drawing.Color.Red);

                            worksheet.Cells["C1:C7"].Style.Font.Bold = true;
                            worksheet.Cells["C3"].Value = "Mã đơn đặt hàng:";
                            worksheet.Cells["D3"].Value = comboBoxMaDDH.Text;

                            worksheet.Cells["C4"].Value = "Nhà cung cấp:";
                            worksheet.Cells["D4"].Value = cbMNCC.Text;

                            worksheet.Cells["C5"].Value = "Địa chỉ:";
                            worksheet.Cells["D5"].Value = txtDiaChiNCCDDH.Text;

                            worksheet.Cells["C6"].Value = "Số điện thoại:";
                            worksheet.Cells["D6"].Value = txtSDTNCCDDH.Text;

                            worksheet.Cells["C7"].Value = "Total:";
                            worksheet.Cells["D7"].Value = txtToTal.Text;

                            worksheet.Column(3).Width = 30;
                            worksheet.Column(4).Width = 30;
                            worksheet.Cells["C1:C7"].AutoFitColumns();

                            
                            // Ghi dữ liệu từ DataGridView vào Excel
                            for (int i = 1; i <= dataGridViewDDH.Columns.Count; i++)
                            {
                                worksheet.Cells[7, i + 2].Value = dataGridViewDDH.Columns[i - 1].HeaderText;
                            }

                            for (int i = 0; i < dataGridViewDDH.Rows.Count; i++)
                            {
                                for (int j = 0; j < dataGridViewDDH.Columns.Count; j++)
                                {
                                    worksheet.Cells[i + 8, j + 3].Value = dataGridViewDDH.Rows[i].Cells[j].Value?.ToString();
                                }
                            }

                            // Thiết lập định dạng cho Excel
                            using (ExcelRange range = worksheet.Cells[9, 3, dataGridViewDDH.Rows.Count + 9, dataGridViewDDH.Columns.Count + 3])
                            {
                                range.Style.Font.Bold = true;
                                range.AutoFitColumns();
                                range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
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

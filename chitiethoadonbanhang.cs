using QuanLyDoanhNghiepMililap.Modify;
using QuanLyDoanhNghiepMililap.Object;
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

namespace QuanLyDoanhNghiepMililap
{
    public partial class chitiethoadonbanhang : Form
    {
        public chitiethoadonbanhang()
        {
            InitializeComponent();
        }
        modifiall modifyall = new modifiall();
        banhang_chitietdondathang CTBH;
        public void load_data()
        {
            dgvKhachhang.DataSource = modifyall.Table("select  * from ChiTiet_DonDatHang");
            DataTable dataTable = modifyall.Table("SELECT MaDonDatHang FROM DonDatHang");

            // Đặt nguồn dữ liệu cho ComboBox
            cb_MDH.DisplayMember = "MaDonDatHang";
            cb_MDH.ValueMember = "MaDonDatHang";
            cb_MDH.DataSource = dataTable;

            DataTable dataTable1 = modifyall.Table("select  * from VatTu");

            // Đặt nguồn dữ liệu cho ComboBox
            cb_MVT.DisplayMember = "MaVatTu";
            cb_MVT.ValueMember = "MaVatTu";
            cb_MVT.DataSource = dataTable1;


        }
        public bool CheckValue()
        {
            if (string.IsNullOrWhiteSpace(cb_MDH.Text) || string.IsNullOrWhiteSpace(cb_MVT.Text) ||
                string.IsNullOrWhiteSpace(txtSL.Text))

            {
                MessageBox.Show("Mời bạn nhập đầy đủ thông tin!");
                return false;
            }

            return true;
        }
        public void Getvaluetextbox()
        {
            string MHDB = txtMaHDB.Text; 
            string MDH = cb_MDH.Text;
            string MVT = cb_MVT.Text;
            int SL = int.Parse(txtSL.Text);
            CTBH = new banhang_chitietdondathang(MHDB,MDH, MVT, SL);
        }
        private void chitiethoadonbanhang_Load(object sender, EventArgs e)
        {
            load_data();
        }

        private void dgvKhachhang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvKhachhang.Rows.Count >= 0)
            {
                txtMaHDB.Text = dgvKhachhang.SelectedRows[0].Cells[0].Value.ToString();
                cb_MDH.Text = dgvKhachhang.SelectedRows[0].Cells[1].Value.ToString();
                cb_MVT.Text = dgvKhachhang.SelectedRows[0].Cells[2].Value.ToString();
                txtSL.Text = dgvKhachhang.SelectedRows[0].Cells[3].Value.ToString();
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (CheckValue()) // Kiểm tra dữ liệu đã nhập

            {
                SqlConnection conn = connection.GetSqlConnection();
                string sqlCheck = "SELECT COUNT(*) FROM ChiTiet_DonDatHang WHERE MaHoaDonBan = @ID";
                SqlCommand cmdCheck = new SqlCommand(sqlCheck, conn);
                cmdCheck.Parameters.AddWithValue("@ID", txtMaHDB.Text);

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
                }
                Getvaluetextbox();

                string query = "INSERT  INTO ChiTiet_DonDatHang (MaHoaDonBan, MaDonDatHang, MaVatTu, SoLuong) " +
                               "VALUES (@A,@I,@M, @SL)";
                SqlCommand insert = new SqlCommand(query, conn);
                insert.Parameters.AddWithValue("@a", CTBH.MaHDBProperty);
                insert.Parameters.AddWithValue("@I", CTBH.MADDHProperty);
                insert.Parameters.AddWithValue("@M", CTBH.MaVTProperty);
                insert.Parameters.AddWithValue("@SL", CTBH.soLuongProperty);


                try
                {
                    conn.Open();
                    insert.ExecuteNonQuery();
                    MessageBox.Show("Thêm  thành công!");
                    load_data(); // Tải lại dữ liệu trong DataGridView
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi thêm : " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cb_MDH.Text))
            {
                MessageBox.Show("Vui lòng nhập mã hoa don!");
                return;
            }
            if (CheckValue())
            {
                SqlConnection con = connection.GetSqlConnection();
                string sql = "SELECT count(*) FROM ChiTiet_DonDatHang WHERE MaHoaDonBan = @ID";
                SqlCommand sqlCmd = new SqlCommand(sql, con);
                sqlCmd.Parameters.AddWithValue("@ID", txtMaHDB.Text);
                con.Open();
                int count = (int)sqlCmd.ExecuteScalar();
                if (count == 0)
                {
                    MessageBox.Show("Mã hóa đơn bán không tồn tại!");
                    return;
                }

                Getvaluetextbox();
                string update = "UPDATE ChiTiet_DonDatHang SET  MaDonDatHang = @DDH, MaVatTu = @MVT,SoLuong= @SL WHERE MaHoaDonBan = @ID ";
                SqlCommand udt = new SqlCommand(update, con);
                udt.Parameters.AddWithValue("@ID", CTBH.MaHDBProperty);
                udt.Parameters.AddWithValue("@DDH", CTBH.MADDHProperty);
                udt.Parameters.AddWithValue("@MVT", CTBH.MaVTProperty);
                udt.Parameters.AddWithValue("@SL", CTBH.soLuongProperty);


                try
                {
                    if (MessageBox.Show("Bạn có muốn sửa lại dữ liệu không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        udt.ExecuteNonQuery();
                        MessageBox.Show("Bạn đã sửa thông tin thành công!");
                        load_data();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi sửa: " + ex.Message);
                }

            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvKhachhang.Rows.Count > 1)
            {
                string choose = dgvKhachhang.SelectedRows[0].Cells[0].Value.ToString();
                string query = "DELETE ChiTiet_DonDatHang ";
                query += " WHERE MaHoaDonBan = '" + choose + "'";
                try
                {
                    if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        modifyall.Command(query);
                        MessageBox.Show("Bạn đã xóa thành công!");
                        load_data();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa: " + ex.Message);
                }
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            string name = txtTim.Text.Trim();
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Vui lòng nhập mã  để tìm!");
                return;
            }
            else
            {
                string query = "SELECT * FROM ChiTiet_DonDatHang WHERE MaHoaDonBan =  @sMaNV";

                using (SqlConnection conn = connection.GetSqlConnection())
                {
                    DataTable resultTable = new DataTable();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@sMaNV", name);
                        conn.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(resultTable);
                    }

                    dgvKhachhang.DataSource = resultTable;
                }
            }
        }
    }
}

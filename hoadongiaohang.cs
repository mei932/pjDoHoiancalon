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
    public partial class hoadongiaohang : Form
    {
        public hoadongiaohang()
        {
            InitializeComponent();
        }

        modifiall modifyall = new modifiall();
        giaohang gh;
        public void load_data()
        {
            dgvKhachhang.DataSource = modifyall.Table("select  * from HoaDon");
        }
        public bool CheckValue()
        {
            if (string.IsNullOrWhiteSpace(txtMHD.Text) || string.IsNullOrWhiteSpace(txtMKH.Text) ||
                string.IsNullOrWhiteSpace(txtNgaylap.Text) || string.IsNullOrWhiteSpace(txtTongTien.Text))

            {
                MessageBox.Show("Mời bạn nhập đầy đủ thông tin!");
                return false;
            }

            return true;
        }
        public void Getvaluetextbox()
        {
            string ID = txtMHD.Text;
            string mkh = txtMKH.Text;
          DateTime NgayNhap  = DateTime.Parse(txtNgaylap.Text);
            float TT = float.Parse(txtTongTien.Text);
            gh = new giaohang(ID, mkh, NgayNhap, TT);
        }

        private void hoadongiaohang_Load(object sender, EventArgs e)
        {
            load_data();
        }

        private void dgvKhachhang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvKhachhang.Rows.Count >= 0)
            {
                txtMHD.Text = dgvKhachhang.SelectedRows[0].Cells[0].Value.ToString();
                txtMKH.Text = dgvKhachhang.SelectedRows[0].Cells[1].Value.ToString();
                txtNgaylap.Text = dgvKhachhang.SelectedRows[0].Cells[2].Value.ToString();
                txtTongTien.Text = dgvKhachhang.SelectedRows[0].Cells[3].Value.ToString();

            }

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (CheckValue()) // Kiểm tra dữ liệu đã nhập
            {
                SqlConnection conn = connection.GetSqlConnection();

                string sqlCheck = "SELECT COUNT(*) FROM HoaDon WHERE MaHoaDon = @ID";
                SqlCommand cmdCheck = new SqlCommand(sqlCheck, conn);
                cmdCheck.Parameters.AddWithValue("@ID", txtMHD.Text);

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

                string query = "INSERT  INTO HoaDon (MaHoaDon, MaKhachHang, NgayBan,TongTien) " +
                               "VALUES (@ID,@MKH, @day, @TT)";
                SqlCommand insert = new SqlCommand(query, conn);
                insert.Parameters.AddWithValue("@ID", gh.MaHDproperty);
                insert.Parameters.AddWithValue("@MKH", gh.MaKHproperty);
                insert.Parameters.AddWithValue("@day", gh.NgayBanproperty);
                insert.Parameters.AddWithValue("@TT", gh.TongTienproperty);




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
            if (string.IsNullOrEmpty(txtMHD.Text))
            {
                MessageBox.Show("Vui lòng nhập mã hóa đơn!");
                return;
            }
            if (CheckValue())
            {
                SqlConnection con = connection.GetSqlConnection();
                string sql = "SELECT count(*) FROM HoaDon WHERE MaHoaDon = @ID";
                SqlCommand sqlCmd = new SqlCommand(sql, con);
                sqlCmd.Parameters.AddWithValue("@ID", txtMHD.Text);
                con.Open();
                int count = (int)sqlCmd.ExecuteScalar();
                if (count == 0)
                {
                    MessageBox.Show("Mã hóa đơn giao hàng không tồn tại!");
                    return;
                }

                Getvaluetextbox();
                string update = "UPDATE HoaDon SET MaKhachHang = @MKH, NgayBan = @day, TongTien = @TT " +
               "WHERE MaHoaDon = @ID";

                SqlCommand udt = new SqlCommand(update, con);
                udt.Parameters.AddWithValue("@ID", gh.MaHDproperty);
                udt.Parameters.AddWithValue("@MKH", gh.MaKHproperty);
                udt.Parameters.AddWithValue("@day", gh.NgayBanproperty);
                udt.Parameters.AddWithValue("@TT", gh.TongTienproperty);


                try
                {
                    if (MessageBox.Show("Bạn có muốn sửa lại dữ liệu không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        udt.ExecuteNonQuery();
                        MessageBox.Show("Bạn đã sửa thông tin nhân viên thành công!");
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
                string query = "DELETE HoaDon ";
                query += " WHERE MaHoaDon = '" + choose + "'";
                try
                {
                    if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        modifyall.Command(query);
                        MessageBox.Show("Bạn đã xóa 1 dịch vụ thành công!");
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
                string query = "SELECT * FROM HoaDon WHERE MaHoaDon =  @sMaNV";

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

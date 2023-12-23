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
    public partial class fDonDatHang : Form
    {
        modifiall modifyall = new modifiall();
        dondathang ddh;
        public void load_data()
        {
            dgvKhachhang.DataSource = modifyall.Table("select  * from DonDatHang");
            DataTable dataTable = modifyall.Table("SELECT MaNCC FROM Nhacungcap");

            // Đặt nguồn dữ liệu cho ComboBox
            cb_NCC.DisplayMember = "MaNCC";
            cb_NCC.ValueMember = "MaNCC";
            cb_NCC.DataSource = dataTable;
        }
        public bool CheckValue()
        {
            if (string.IsNullOrWhiteSpace(txtMaDonDat.Text) || string.IsNullOrWhiteSpace(cb_NCC.Text) ||
                string.IsNullOrWhiteSpace(txtNgayDat.Text) )

            {
                MessageBox.Show("Mời bạn nhập đầy đủ thông tin!");
                return false;
            }

            return true;
        }
        public void Getvaluetextbox()
        {
            string ID = txtMaDonDat.Text;
            string NCC = cb_NCC.Text;
            string day = txtNgayDat.Text;
            ddh = new dondathang(ID, NCC, day);
        }
        public fDonDatHang()
        {
            InitializeComponent();
        }

        private void fDonDatHang_Load(object sender, EventArgs e)
        {
            load_data();

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (CheckValue()) // Kiểm tra dữ liệu đã nhập
            {
                SqlConnection conn = connection.GetSqlConnection();

                string sqlCheck = "SELECT COUNT(*) FROM DonDatHang WHERE MaDonDatHang = @ID";
                SqlCommand cmdCheck = new SqlCommand(sqlCheck, conn);
                cmdCheck.Parameters.AddWithValue("@ID", txtMaDonDat.Text);

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

                string query = "INSERT INTO DonDatHang (MaDonDatHang, MaNCC, NgayDat) " +
                               "VALUES (@ID,@NCC, @day)";
                SqlCommand insert = new SqlCommand(query, conn);
                insert.Parameters.AddWithValue("@ID", ddh.MADDHProperty);
                insert.Parameters.AddWithValue("@NCC", ddh.MaNCCProperty);
                insert.Parameters.AddWithValue("@day", ddh.NgayDatProperty);
               

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
            if (string.IsNullOrEmpty(txtMaDonDat.Text))
            {
                MessageBox.Show("Vui lòng nhập mã dịch vụ!");
                return;
            }
            if (CheckValue())
            {
                SqlConnection con = connection.GetSqlConnection();
                string sql = "SELECT count(*) FROM DonDatHang WHERE MaDonDatHang = @ID";
                SqlCommand sqlCmd = new SqlCommand(sql, con);
                sqlCmd.Parameters.AddWithValue("@ID", txtMaDonDat.Text);
                con.Open();
                int count = (int)sqlCmd.ExecuteScalar();
                if (count == 0)
                {
                    MessageBox.Show("Mã hóa đơn bán không tồn tại!");
                    return;
                }

                Getvaluetextbox();
                string update = "UPDATE DonDatHang SET  MaNCC = @NCC, NgayDat= @day WHERE MaDonDatHang = @ID ";
                SqlCommand udt = new SqlCommand(update, con);
                udt.Parameters.AddWithValue("@ID", ddh.MADDHProperty);
                udt.Parameters.AddWithValue("@NCC", ddh.MaNCCProperty);
                udt.Parameters.AddWithValue("@day", ddh.NgayDatProperty);

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
                string query = "DELETE DonDatHang ";
                query += " WHERE MaDonDatHang = '" + choose + "'";
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
                string query = "SELECT * FROM DonDatHang WHERE MaDonDatHang =  @sMaNV";

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

        private void dgvKhachhang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvKhachhang.Rows.Count >= 0)
            {
                txtMaDonDat.Text = dgvKhachhang.SelectedRows[0].Cells[0].Value.ToString();
                cb_NCC.Text = dgvKhachhang.SelectedRows[0].Cells[1].Value.ToString();
                txtNgayDat.Text = dgvKhachhang.SelectedRows[0].Cells[2].Value.ToString();
            }
        }
    }
}

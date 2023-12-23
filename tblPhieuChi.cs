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
    public partial class tblPhieuChi : Form
    {
        public tblPhieuChi()
        {
            InitializeComponent();
        }
        modifiall modifyall = new modifiall();
        phieuchi pc;
        public void load_data()
        {
            dgvKhachhang.DataSource = modifyall.Table("select  * from PhieuChi");
            DataTable dataTable = modifyall.Table("SELECT MaDonDatHang FROM DonDatHang");

            // Đặt nguồn dữ liệu cho ComboBox
            cb_MDDH.DisplayMember = "MaDonDatHang";
            cb_MDDH.ValueMember = "MaDonDatHang";
            cb_MDDH.DataSource = dataTable;
        }
        public bool CheckValue()
        {
            if (string.IsNullOrWhiteSpace(txtmaPC.Text) || string.IsNullOrWhiteSpace(txtNgayNhap.Text) ||
                string.IsNullOrWhiteSpace(txtTT.Text)|| string.IsNullOrWhiteSpace(cb_MDDH.Text))

            {
                MessageBox.Show("Mời bạn nhập đầy đủ thông tin!");
                return false;
            }

            return true;
        }
        public void Getvaluetextbox()
        {
            string ID = txtmaPC.Text;
            string DDH = cb_MDDH.Text;
            float TT = float.Parse(txtTT.Text);
            string day = txtNgayNhap.Text;
            pc = new phieuchi(ID, DDH, TT,day);
        }
        private void tblPhieuChi_Load(object sender, EventArgs e)
        {
            load_data();
        }

        private void dgvKhachhang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvKhachhang.Rows.Count >= 0)
            {
                txtmaPC.Text = dgvKhachhang.SelectedRows[0].Cells[0].Value.ToString();
                cb_MDDH.Text = dgvKhachhang.SelectedRows[0].Cells[1].Value.ToString();
                txtTT.Text = dgvKhachhang.SelectedRows[0].Cells[2].Value.ToString();
                txtNgayNhap.Text = dgvKhachhang.SelectedRows[0].Cells[3].Value.ToString();

            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (CheckValue()) // Kiểm tra dữ liệu đã nhập
            {
                SqlConnection conn = connection.GetSqlConnection();

                string sqlCheck = "SELECT COUNT(*) FROM PhieuChi WHERE MaPhieuChi = @ID";
                SqlCommand cmdCheck = new SqlCommand(sqlCheck, conn);
                cmdCheck.Parameters.AddWithValue("@ID", txtmaPC.Text);

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

                string query = "INSERT  INTO PhieuChi (MaPhieuChi, MaDonDathang, TongTien,NgayNhap) " +
                               "VALUES (@ID,@MDDH, @TT, @day)";
                SqlCommand insert = new SqlCommand(query, conn);
                insert.Parameters.AddWithValue("@ID", pc.maPCProperty);
                insert.Parameters.AddWithValue("@MDDH", pc.MDDHProperty);
                insert.Parameters.AddWithValue("@TT", pc.tongtienProprty);
                insert.Parameters.AddWithValue("@day", pc.NgayNhapProperty);


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
            if (string.IsNullOrEmpty(txtmaPC.Text))
            {
                MessageBox.Show("Vui lòng nhập mã phiếu chi!");
                return;
            }
            if (CheckValue())
            {
                SqlConnection con = connection.GetSqlConnection();
                string sql = "SELECT count(*) FROM PhieuChi WHERE MaPhieuChi = @ID";
                SqlCommand sqlCmd = new SqlCommand(sql, con);
                sqlCmd.Parameters.AddWithValue("@ID", txtmaPC.Text);
                con.Open();
                int count = (int)sqlCmd.ExecuteScalar();
                if (count == 0)
                {
                    MessageBox.Show("Mã hóa đơn bán không tồn tại!");
                    return;
                }

                Getvaluetextbox();
                string update = "UPDATE PhieuChi SET  MaDonDatHang = @hhd,TongTien= @TT, NgayNhap= @day WHERE MaPhieuChi = @ID ";
                SqlCommand udt = new SqlCommand(update, con);
                udt.Parameters.AddWithValue("@ID", pc.maPCProperty);
                udt.Parameters.AddWithValue("@hhd", pc.MDDHProperty);
                udt.Parameters.AddWithValue("@TT", pc.tongtienProprty);
                udt.Parameters.AddWithValue("@day", pc.NgayNhapProperty);


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
                string query = "DELETE PhieuChi ";
                query += " WHERE MaPhieuChi = '" + choose + "'";
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
                string query = "SELECT * FROM PhieuChi WHERE MaPhieuChi =  @sMaNV";

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

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
    public partial class tblkho : Form
    {
        public tblkho()
        {
            InitializeComponent();
        }
        modifiall modifyall = new modifiall();
        kho kho;
        public void load_data()
        {
            dgvKhachhang.DataSource = modifyall.Table("select  * from VatTu");
          
        }
        public bool CheckValue()
        {
            if (string.IsNullOrWhiteSpace(txtMVT.Text) || string.IsNullOrWhiteSpace(txtTenVT.Text) ||
                string.IsNullOrWhiteSpace(txtMaLoai.Text) || string.IsNullOrWhiteSpace(txtGiaNhap.Text) || string.IsNullOrWhiteSpace(txtGiaBan.Text) || string.IsNullOrWhiteSpace(txtSLTK.Text))

            {
                MessageBox.Show("Mời bạn nhập đầy đủ thông tin!");
                return false;
            }

            return true;
        }
        public void Getvaluetextbox()
        {
            string ID = txtMVT.Text;
            string tenVt = txtTenVT.Text;
            string  LoaiVT = txtMaLoai.Text;
            float giaNhap = float.Parse(txtGiaNhap.Text);
            float giaBan = float.Parse(txtGiaBan.Text);

            int SLTK = int.Parse(txtSLTK.Text);

            kho = new kho(ID, tenVt, LoaiVT, giaNhap,giaBan,SLTK);
        }

        private void tblkho_Load(object sender, EventArgs e)
        {
            load_data();
        }

        private void dgvKhachhang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvKhachhang.Rows.Count >= 0)
            {
                txtMVT.Text = dgvKhachhang.SelectedRows[0].Cells[0].Value.ToString();
                txtTenVT.Text = dgvKhachhang.SelectedRows[0].Cells[1].Value.ToString();
                txtMaLoai.Text = dgvKhachhang.SelectedRows[0].Cells[2].Value.ToString();
                txtGiaNhap.Text = dgvKhachhang.SelectedRows[0].Cells[3].Value.ToString();
                txtGiaBan.Text = dgvKhachhang.SelectedRows[0].Cells[4].Value.ToString();
                txtSLTK.Text = dgvKhachhang.SelectedRows[0].Cells[5].Value.ToString();
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (CheckValue()) // Kiểm tra dữ liệu đã nhập
            {
                SqlConnection conn = connection.GetSqlConnection();

                string sqlCheck = "SELECT COUNT(*) FROM VatTu WHERE MaVatTu = @ID";
                SqlCommand cmdCheck = new SqlCommand(sqlCheck, conn);
                cmdCheck.Parameters.AddWithValue("@ID", txtMVT.Text);

                try
                {
                    conn.Open();
                    int count = (int)cmdCheck.ExecuteScalar();
                    if (count > 0)
                    {
                        MessageBox.Show("Mã vật tư đã tồn tại!");
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

                string query = "INSERT  INTO VatTu (MaVatTu, TenVatTu, MaLoaiVatTu,GiaNhap,GiaBan,SoLuongTonKho) " +
                               "VALUES (@ID,@TenVT, @MaLoai, @giaNhap,@GiaBan,@SLTK)";
                SqlCommand insert = new SqlCommand(query, conn);
                insert.Parameters.AddWithValue("@ID", kho.MaVTproperty);
                insert.Parameters.AddWithValue("@TenVT", kho.TenVTProperty);
                insert.Parameters.AddWithValue("@MaLoai", kho.MaLoaiVTProperty);
                insert.Parameters.AddWithValue("@GiaNhap", kho.GiaNhapProperty);
                insert.Parameters.AddWithValue("@GiaBan", kho.GiaBanProperty);
                insert.Parameters.AddWithValue("@SLTK", kho.SLTonKhoproperty);



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

            if (string.IsNullOrEmpty(txtMVT.Text))
            {
                MessageBox.Show("Vui lòng nhập mã phiếu chi!");
                return;
            }
            if (CheckValue())
            {
                SqlConnection con = connection.GetSqlConnection();
                string sql = "SELECT count(*) FROM VatTu WHERE maVatTu = @ID";
                SqlCommand sqlCmd = new SqlCommand(sql, con);
                sqlCmd.Parameters.AddWithValue("@ID", txtMVT.Text);
                con.Open();
                int count = (int)sqlCmd.ExecuteScalar();
                if (count == 0)
                {
                    MessageBox.Show("Mã vật tư không tồn tại!");
                    return;
                }

                Getvaluetextbox();
                string update = "UPDATE VatTu SET TenVatTu = @TenVT, MaLoaiVatTu = @MaLoai, GiaNhap = @giaNhap, GiaBan = @GiaBan, SoLuongTonKho = @SLTK " +
                "WHERE MaVatTu = @ID";
                SqlCommand udt = new SqlCommand(update, con);
                udt.Parameters.AddWithValue("@ID", kho.MaVTproperty);
                udt.Parameters.AddWithValue("@TenVT", kho.TenVTProperty);
                udt.Parameters.AddWithValue("@MaLoai", kho.MaLoaiVTProperty);
                udt.Parameters.AddWithValue("@GiaNhap", kho.GiaNhapProperty);
                udt.Parameters.AddWithValue("@GiaBan", kho.GiaBanProperty);
                udt.Parameters.AddWithValue("@SLTK", kho.SLTonKhoproperty);


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
                string query = "DELETE VatTu ";
                query += " WHERE MaVatTu = '" + choose + "'";
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
                MessageBox.Show("Vui lòng nhập tên vật tư để tìm!");
                return;
            }
            else
            {
                string query = "SELECT * FROM VatTu WHERE TenVatTu LIKE @tenVatTu";

                using (SqlConnection conn = connection.GetSqlConnection())
                {
                    DataTable resultTable = new DataTable();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@tenVatTu", "%" + name + "%"); // Sử dụng % để tìm kiếm một phần của tên vật tư
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

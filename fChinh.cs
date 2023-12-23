using System;
using System.Windows.Forms;

namespace QuanLyDoanhNghiepMililap
{
    public partial class fChinh : Form
    {
        public fChinh()
        {
            InitializeComponent();
        }

        private void MenuNCC_Click(object sender, EventArgs e)
        {
            fNCC fncc = new fNCC();
            fncc.Show();
        }

        private void MenuDichVu_Click(object sender, EventArgs e)
        {
            fDichVu fdichvu = new fDichVu();
            fdichvu.Show();
        }

        private void MenuLapDonHang_Click(object sender, EventArgs e)
        {
            fDonDatHangg fdondathang = new fDonDatHangg();
            fdondathang.Show();
        }

        private void MenuPhieuChi_Click(object sender, EventArgs e)
        {
            tblPhieuChi pc  = new tblPhieuChi();
            pc.Show();
        }

        private void MenuKhachhang_Click(object sender, EventArgs e)
        {
            fKhachHang fkhachhang = new fKhachHang();
            fkhachhang.Show();
        }

        private void MenuHoaDon_Click(object sender, EventArgs e)
        {
            /*fHoaDon fhoadon = new fHoaDon();
            fhoadon.Show();*/
        }

        private void MenuLoaiVT_Click(object sender, EventArgs e)
        {
            fLoaiVT fLoaiVT = new fLoaiVT();
            fLoaiVT.Show();
        }

        private void MenuLoaiDV_Click(object sender, EventArgs e)
        {
            fLoaiDV floaidv = new fLoaiDV();
            floaidv.Show();
        }

        private void MenuVatTu_Click_1(object sender, EventArgs e)
        {
            /*fVatTu fvattu = new fVatTu();
            fvattu.Show();*/

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        

        private void MenuThongKeKhachHang_Click(object sender, EventArgs e)
        {
            fThongKeKhachHangVip fKhachHang = new fThongKeKhachHangVip();
            fKhachHang.Show();
        }

        private void MenuThongKeVatTu_Click(object sender, EventArgs e)
        {
            thongkevattu vt = new thongkevattu();
            vt.Show();
        }

        private void MenuDoanhThuTheoThang_Click(object sender, EventArgs e)
        {
            fThongKeVatTuTrongKho fVattu = new fThongKeVatTuTrongKho();
            fVattu.Show();
        }

        private void MenuDoanhThuTheoNgay_Click(object sender, EventArgs e)
        {
           /* fDoanhThuTheoNgay fdoanhthungay = new fDoanhThuTheoNgay();
            fdoanhthungay.Show();*/
        }

        private void MenuDoanhThuTheoVatTu_Click(object sender, EventArgs e)
        {
            fThongKeVatTuHetTrongKho fvattuhet = new fThongKeVatTuHetTrongKho();
            fvattuhet.Show();
        }

        private void quảnLýĐơnĐặtHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void MenuGiaoHang_Click(object sender, EventArgs e)
        {
            hoadongiaohang tc = new hoadongiaohang();
            tc.Show();
        }

        private void hóaĐơnĐặtHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fDonDatHang ddh = new fDonDatHang();

            ddh.Show();
        }

        private void hóaĐơnBánHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chitiethoadonbanhang tc = new chitiethoadonbanhang();
            tc.Show();
        }

        private void MenuNhapKho_Click(object sender, EventArgs e)
        {
            tblkho kho = new tblkho();
            kho.Show();
        }
    }
}

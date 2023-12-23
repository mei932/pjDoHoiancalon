using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDoanhNghiepMililap.Object
{
    class banhang_chitietdondathang
    {
        private string MaHDB;
        private string MaDDH;
        private string MAVT;
        private int soLuong;


        public banhang_chitietdondathang(string MaHDB, string MaDDH, string MAVT, int soLuong)
        {
            this.MaHDB = MaHDB;
            this.MaDDH = MaDDH;
            this.MAVT = MAVT;
            this.soLuong = soLuong;
        }
        public string MaHDBProperty
        {
            get { return MaHDB; }
            set { MaHDB = value; }
        }

        public string MADDHProperty
        {
            get { return MaDDH; }
            set { MaDDH = value; }
        }

        public string MaVTProperty
        {
            get { return MAVT; }
            set { MAVT = value; }
        }

        public int soLuongProperty
        {
            get { return soLuong; }
            set { soLuong = value; }
        }

    }
}

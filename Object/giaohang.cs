using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDoanhNghiepMililap.Object
{
    class giaohang
    {
        private string MaHD;
        private string maKH;
        private DateTime NgayBan;
        private float TongTien;

        public giaohang(string MaHD, string maKH, DateTime NgayBan, float TongTien)
        {
            this.MaHD = MaHD;
            this.maKH = maKH;
            this.NgayBan = NgayBan;
            this.TongTien = TongTien;
        }
        public string MaHDproperty
        {
            get { return MaHD; }
            set { MaHD = value; }
        }

        public string MaKHproperty
        {
            get { return maKH; }
            set { maKH = value; }
        }

        public DateTime NgayBanproperty
        {
            get { return NgayBan; }
            set { NgayBan = value; }
        }

        public float TongTienproperty
        {
            get { return TongTien; }
            set { TongTien = value; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDoanhNghiepMililap.Object
{
    class kho
    {
        private string MaVT;
        private string tenVT;
        private string MaLoaiVT;
        private float GiaNhap;
        private float GiaBan;
        private int SLTonKho;

        public kho(string MaVT, string TenVT, string MaLoaiVT, float GiaNhap, float GiaBan, int SLTonKho)
        {
            this.MaVT = MaVT;
            this.tenVT = TenVT;
            this.MaLoaiVT = MaLoaiVT;
            this.GiaNhap = GiaNhap;
            this.GiaBan = GiaBan;
            this.SLTonKho = SLTonKho;
        }
        public string MaVTproperty
        {
            get { return MaVT; }
            set { MaVT = value; }
        }

        public string TenVTProperty
        {
            get { return tenVT; }
            set { tenVT = value; }
        }

        public string MaLoaiVTProperty
        {
            get { return MaLoaiVT; }
            set { MaLoaiVT = value; }
        }

        public float GiaNhapProperty
        {
            get { return GiaNhap; }
            set { GiaNhap = value; }
        }

        public float GiaBanProperty
        {
            get { return GiaBan; }
            set { GiaBan = value; }
        }

        public int SLTonKhoproperty
        {
            get { return SLTonKho; }
            set { SLTonKho = value; }
        }
    }
}

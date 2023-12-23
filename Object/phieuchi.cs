using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDoanhNghiepMililap.Object
{
    class phieuchi
    {
        private string maPC;
        private string MDDH;
        private float tongtien;
        private string NgayNhap;


        public phieuchi(string maPC, string MDDH,float tongtien, string NgayNhap)
        {
            this.maPC = maPC;
            this.MDDH = MDDH;
            this.tongtien = tongtien;
            this.NgayNhap = NgayNhap;
        }
        public string maPCProperty
        {
            get { return maPC; }
            set { maPC = value; }
        }
        public string MDDHProperty
        {
            get { return MDDH; }
            set { MDDH = value; }
        }

        public float tongtienProprty
        {
            get { return tongtien; }
            set { tongtien = value; }
        }


        public string NgayNhapProperty
        {
            get { return NgayNhap; }
            set {  NgayNhap = value; }
        }


    }
}

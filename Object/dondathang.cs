using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDoanhNghiepMililap.Object
{
    class dondathang
    {
        private string MaDDH;
        private string MaNCC;
        private string NgayDat;
        

        public dondathang(string MaDDH, string MaNCC, string NgayDat)
        {
            this.MaDDH = MaDDH;
            this.MaNCC = MaNCC;
            this.NgayDat = NgayDat;
        }
        public string MADDHProperty
        {
            get { return MaDDH;     }
            set { MaDDH = value; }
        }

        public string MaNCCProperty
        {
            get { return MaNCC; }
            set { MaNCC = value; }
        }

        public string NgayDatProperty
        {
            get { return NgayDat; }
            set { NgayDat = value; }
        }

        
    }
}

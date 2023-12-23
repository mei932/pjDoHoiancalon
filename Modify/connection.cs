using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDoanhNghiepMililap.Modify
{
    class connection
    {
        private static string stringConnection = @"Data Source=DESKTOP-ESFTV9I\SQLEXPRESS;Initial Catalog=DoAn1Nhom3;Integrated Security=True";
        public static SqlConnection GetSqlConnection()
        {
            return new SqlConnection(stringConnection);
        }
    }
}

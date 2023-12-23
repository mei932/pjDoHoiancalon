using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDoanhNghiepMililap.Modify
{
    class modifiall
    {
        SqlCommand sqlCommand; // Dùng để truy vấn các lệnh insert, update,..
        SqlDataAdapter dataAdapter;

        public void Command(string query)  // Dùng để thêm sửa xóa
        {
            // Mở đường kết nối
            using (SqlConnection sqlConnection = connection.GetSqlConnection())
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.ExecuteNonQuery(); //thực hiện truy vấn
                sqlConnection.Close();
            }
        }


        // Tạo dataTable để đổ dữ liệu vào datagridview
        public DataTable Table(string query)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection sqlConnection = connection.GetSqlConnection())
            {
                sqlConnection.Open();
                dataAdapter = new SqlDataAdapter(query, sqlConnection);
                dataAdapter.Fill(dataTable);
                sqlConnection.Close();
            }
            return dataTable;
        }
    }
}

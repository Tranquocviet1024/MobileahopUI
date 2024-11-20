using System.Data;
using Microsoft.Data.SqlClient;

namespace MobileShopUI
{
    public class KetNoi
    {
        // Chuỗi kết nối đến SQL Server
       private string conStr = @"Data Source=LAPTOP-8NL4P7LM;Initial Catalog=MobileShop;Integrated Security=True;TrustServerCertificate=True";

        private SqlConnection connection;

        // Constructor
        public KetNoi()
        {
            connection = new SqlConnection(conStr);

        }

        // Phương thức lấy dữ liệu và trả về DataSet
        public DataSet? LayDuLieu(string truyvan)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(truyvan, connection);
                da.Fill(ds); // Lấy dữ liệu vào DataSet
                return ds;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy dữ liệu: " + ex.Message);
                return null;
            }
        }

        // Phương thức thực thi câu lệnh (INSERT, UPDATE, DELETE)
        public bool ThucThi(string truyvan)
        {
            try
            {
                connection.Open(); // Mở kết nối
                SqlCommand cmd = new SqlCommand(truyvan, connection);
                int rowsAffected = cmd.ExecuteNonQuery(); // Thực thi truy vấn
                return rowsAffected > 0; // Trả về true nếu có ít nhất 1 dòng bị ảnh hưởng
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi thực thi: " + ex.Message);
                return false;
            }
            finally
            {
                connection.Close(); // Đảm bảo đóng kết nối
            }
        }
        public DataSet? LayDuLieuCoThamSo(string truyvan, Dictionary<string, object> thamSo)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(conStr))
                {
                    using (SqlCommand cmd = new SqlCommand(truyvan, connection))
                    {
                        foreach (var param in thamSo)
                        {
                            cmd.Parameters.AddWithValue(param.Key, param.Value);
                        }

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataSet ds = new DataSet();
                            da.Fill(ds);
                            return ds;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy dữ liệu: " + ex.Message);
                return null;
            }
        }
        public bool ThucThiCoThamSo(string truyvan, Dictionary<string, object> thamSo)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(conStr))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(truyvan, connection))
                    {
                        foreach (var param in thamSo)
                        {
                            cmd.Parameters.AddWithValue(param.Key, param.Value);
                        }

                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi thực thi: " + ex.Message);
                return false;
            }
        }

    }
}

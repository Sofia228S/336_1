using System;
using System.Data;
using System.Data.SqlClient;

namespace lab1
{
    // Цей клас виступає середньою ланкою між формами та базою даних
    public class DataManager
    {
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=lb1;Integrated Security=True;";

        // Метод для отримання даних з будь-якої таблиці
        public DataTable GetTableData(string tableName)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = $"SELECT * FROM [dbo].[{tableName}]";
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                {
                    adapter.Fill(dt);
                }
            }
            return dt;
        }

        // Метод для збереження змін у будь-яку таблицю
        public void SaveTableData(DataTable modifiedData, string tableName)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = $"SELECT * FROM [dbo].[{tableName}]";
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                {
                    SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                    adapter.Update(modifiedData);
                }
            }
        }
    }
}

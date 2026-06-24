using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace KamoChatBotGUI
{
    public class DatabaseHelper
    {
        private string connectionString;

        public DatabaseHelper()
        {
            string server = "mysql-prog6221.alwaysdata.net";
            string port = "3306";
            string database = "prog6221_chatbot_db";
            string username = "prog6221_app_user";
            string password = "kamo@always";

            connectionString = $"Server={server};Port={port};Database={database};Uid={username};Pwd={password};SslMode=None;";
        }

        public bool TestConnection()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    return true;
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Connection Error: " + ex.Message);
                return false;
            }
        }

        public void CreateTables()
        {
            string sql = @"
                CREATE TABLE IF NOT EXISTS tasks (
                    id INT AUTO_INCREMENT PRIMARY KEY,
                    title VARCHAR(200) NOT NULL,
                    description TEXT,
                    reminder_date DATETIME,
                    status VARCHAR(20) DEFAULT 'pending',
                    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
                );

                CREATE TABLE IF NOT EXISTS activity_log (
                    id INT AUTO_INCREMENT PRIMARY KEY,
                    action_type VARCHAR(50) NOT NULL,
                    description TEXT NOT NULL,
                    timestamp TIMESTAMP DEFAULT CURRENT_TIMESTAMP
                );

                CREATE TABLE IF NOT EXISTS quiz_scores (
                    id INT AUTO_INCREMENT PRIMARY KEY,
                    player_name VARCHAR(100) NOT NULL,
                    score INT NOT NULL,
                    total_questions INT NOT NULL,
                    date_played TIMESTAMP DEFAULT CURRENT_TIMESTAMP
                );
            ";

            ExecuteNonQuery(sql);
        }

        public int ExecuteNonQuery(string sql)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ExecuteNonQuery Error: " + ex.Message);
                return -1;
            }
        }

        public DataTable ExecuteQuery(string sql)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ExecuteQuery Error: " + ex.Message);
                return new DataTable();
            }
        }

        public object ExecuteScalar(string sql)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    return cmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ExecuteScalar Error: " + ex.Message);
                return null;
            }
        }
    }
}
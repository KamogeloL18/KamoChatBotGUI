using System;
using System.Collections.Generic;
using System.Data;


namespace KamoChatBotGUI
{
    public class ActivityLog
    {
        private DatabaseHelper db;

        public ActivityLog(DatabaseHelper databaseHelper)
        {
            db = databaseHelper;
        }

        public bool AddLog(string actionType, string description)
        {
            string sql = $"INSERT INTO activity_log (action_type, description) VALUES ('{actionType.Replace("'", "''")}', '{description.Replace("'", "''")}')";
            int result = db.ExecuteNonQuery(sql);
            return result > 0;
        }

        public List<string> GetRecentLogs(int count = 10)
        {
            List<string> logs = new List<string>();
            string sql = $"SELECT action_type, description, timestamp FROM activity_log ORDER BY timestamp DESC LIMIT {count}";
            DataTable dt = db.ExecuteQuery(sql);

            foreach (DataRow row in dt.Rows)
            {
                string log = $"{row["timestamp"]} - {row["action_type"]}: {row["description"]}";
                logs.Add(log);
            }

            return logs;
        }

        public DataTable GetAllLogs()
        {
            string sql = "SELECT * FROM activity_log ORDER BY timestamp DESC";
            return db.ExecuteQuery(sql);
        }

        public int GetLogCount()
        {
            string sql = "SELECT COUNT(*) FROM activity_log";
            object result = db.ExecuteScalar(sql);
            return result != null ? Convert.ToInt32(result) : 0;
        }

        public bool ClearLogs()
        {
            string sql = "DELETE FROM activity_log";
            int result = db.ExecuteNonQuery(sql);
            return result > 0;
        }
    }
}
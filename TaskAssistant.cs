using System;
using System.Collections.Generic;
using System.Data;


namespace KamoChatBotGUI
{
    public class TaskAssistant
    {
        private DatabaseHelper db;

        public TaskAssistant(DatabaseHelper databaseHelper)
        {
            db = databaseHelper;
        }

        public bool AddTask(string title, string description, DateTime reminderDate)
        {
            string sql = $"INSERT INTO tasks (title, description, reminder_date, status) VALUES ('{title.Replace("'", "''")}', '{description.Replace("'", "''")}', ";

            if (reminderDate != DateTime.MinValue)
            {
                sql += $"'{reminderDate.ToString("yyyy-MM-dd HH:mm:ss")}'";
            }
            else
            {
                sql += "NULL";
            }
            sql += ", 'pending')";

            int result = db.ExecuteNonQuery(sql);
            return result > 0;
        }

        public List<TaskItem> GetAllTasks()
        {
            List<TaskItem> tasks = new List<TaskItem>();
            string sql = "SELECT * FROM tasks ORDER BY created_at DESC";
            DataTable dt = db.ExecuteQuery(sql);

            foreach (DataRow row in dt.Rows)
            {
                TaskItem task = new TaskItem();
                task.Id = Convert.ToInt32(row["id"]);
                task.Title = row["title"].ToString();
                task.Description = row["description"]?.ToString() ?? "";

                if (row["reminder_date"] != DBNull.Value)
                    task.ReminderDate = Convert.ToDateTime(row["reminder_date"]);
                else
                    task.ReminderDate = DateTime.MinValue;

                task.Status = row["status"].ToString();
                task.CreatedAt = Convert.ToDateTime(row["created_at"]);
                tasks.Add(task);
            }

            return tasks;
        }

        public List<TaskItem> GetPendingTasks()
        {
            List<TaskItem> tasks = new List<TaskItem>();
            string sql = "SELECT * FROM tasks WHERE status = 'pending' ORDER BY created_at DESC";
            DataTable dt = db.ExecuteQuery(sql);

            foreach (DataRow row in dt.Rows)
            {
                TaskItem task = new TaskItem();
                task.Id = Convert.ToInt32(row["id"]);
                task.Title = row["title"].ToString();
                task.Description = row["description"]?.ToString() ?? "";

                if (row["reminder_date"] != DBNull.Value)
                    task.ReminderDate = Convert.ToDateTime(row["reminder_date"]);
                else
                    task.ReminderDate = DateTime.MinValue;

                task.Status = row["status"].ToString();
                task.CreatedAt = Convert.ToDateTime(row["created_at"]);
                tasks.Add(task);
            }

            return tasks;
        }

        public bool CompleteTask(string title)
        {
            string sql = $"UPDATE tasks SET status = 'completed' WHERE LOWER(title) LIKE '%{title.ToLower().Replace("'", "''")}%' AND status = 'pending'";
            int result = db.ExecuteNonQuery(sql);
            return result > 0;
        }

        public bool CompleteTaskById(int id)
        {
            string sql = $"UPDATE tasks SET status = 'completed' WHERE id = {id}";
            int result = db.ExecuteNonQuery(sql);
            return result > 0;
        }

        public bool DeleteTask(string title)
        {
            string sql = $"DELETE FROM tasks WHERE LOWER(title) LIKE '%{title.ToLower().Replace("'", "''")}%'";
            int result = db.ExecuteNonQuery(sql);
            return result > 0;
        }

        public bool DeleteTaskById(int id)
        {
            string sql = $"DELETE FROM tasks WHERE id = {id}";
            int result = db.ExecuteNonQuery(sql);
            return result > 0;
        }

        public int GetPendingCount()
        {
            string sql = "SELECT COUNT(*) FROM tasks WHERE status = 'pending'";
            object result = db.ExecuteScalar(sql);
            return result != null ? Convert.ToInt32(result) : 0;
        }

        public int GetTotalCount()
        {
            string sql = "SELECT COUNT(*) FROM tasks";
            object result = db.ExecuteScalar(sql);
            return result != null ? Convert.ToInt32(result) : 0;
        }
    }
}
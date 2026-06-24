using System;

namespace KamoChatBotGUI
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ReminderDate { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }

        public TaskItem()
        {
            Title = "";
            Description = "";
            Status = "pending";
            ReminderDate = DateTime.MinValue;
        }

        public TaskItem(int id, string title, string desc, DateTime reminder, string status, DateTime created)
        {
            Id = id;
            Title = title;
            Description = desc;
            ReminderDate = reminder;
            Status = status;
            CreatedAt = created;
        }
    }
}
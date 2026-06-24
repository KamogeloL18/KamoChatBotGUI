using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace KamoChatBotGUI
{
    public partial class ViewTasksForm : Form
    {
        private TaskAssistant taskAssistant;

        public ViewTasksForm(TaskAssistant assistant)
        {
            InitializeComponent();
            taskAssistant = assistant;
            LoadTasks();
        }

        private void InitializeComponent()
        {
            this.lblTitle = new Label();
            this.lstTasks = new ListBox();
            this.btnRefresh = new Button();
            this.btnComplete = new Button();
            this.btnDelete = new Button();
            this.btnClose = new Button();
            this.SuspendLayout();

            // Form
            this.Text = "My Tasks";
            this.Size = new Size(500, 400);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(240, 240, 240);

            // lblTitle
            this.lblTitle.Text = "Your Cybersecurity Tasks";
            this.lblTitle.Location = new Point(20, 15);
            this.lblTitle.Size = new Size(460, 30);
            this.lblTitle.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            this.lblTitle.TextAlign = ContentAlignment.MiddleCenter;

            // lstTasks
            this.lstTasks.Location = new Point(20, 55);
            this.lstTasks.Size = new Size(460, 250);
            this.lstTasks.Font = new Font("Segoe UI", 10);
            this.lstTasks.BackColor = Color.White;

            // btnRefresh
            this.btnRefresh.Text = " Refresh";
            this.btnRefresh.Location = new Point(20, 315);
            this.btnRefresh.Size = new Size(100, 35);
            this.btnRefresh.BackColor = Color.FromArgb(0, 132, 255);
            this.btnRefresh.ForeColor = Color.White;
            this.btnRefresh.FlatStyle = FlatStyle.Flat;
            this.btnRefresh.Click += BtnRefresh_Click;

            // btnComplete
            this.btnComplete.Text = "Complete";
            this.btnComplete.Location = new Point(130, 315);
            this.btnComplete.Size = new Size(100, 35);
            this.btnComplete.BackColor = Color.FromArgb(0, 200, 0);
            this.btnComplete.ForeColor = Color.White;
            this.btnComplete.FlatStyle = FlatStyle.Flat;
            this.btnComplete.Click += BtnComplete_Click;

            // btnDelete
            this.btnDelete.Text = "Delete";
            this.btnDelete.Location = new Point(240, 315);
            this.btnDelete.Size = new Size(100, 35);
            this.btnDelete.BackColor = Color.FromArgb(255, 80, 80);
            this.btnDelete.ForeColor = Color.White;
            this.btnDelete.FlatStyle = FlatStyle.Flat;
            this.btnDelete.Click += BtnDelete_Click;

            // btnClose
            this.btnClose.Text = "Close";
            this.btnClose.Location = new Point(380, 315);
            this.btnClose.Size = new Size(100, 35);
            this.btnClose.BackColor = Color.Gray;
            this.btnClose.ForeColor = Color.White;
            this.btnClose.FlatStyle = FlatStyle.Flat;
            this.btnClose.Click += BtnClose_Click;

            // Add controls
            this.Controls.Add(lblTitle);
            this.Controls.Add(lstTasks);
            this.Controls.Add(btnRefresh);
            this.Controls.Add(btnComplete);
            this.Controls.Add(btnDelete);
            this.Controls.Add(btnClose);

            this.ResumeLayout(false);
        }

        private Label lblTitle;
        private ListBox lstTasks;
        private Button btnRefresh;
        private Button btnComplete;
        private Button btnDelete;
        private Button btnClose;

        private void LoadTasks()
        {
            lstTasks.Items.Clear();
            List<TaskItem> tasks = taskAssistant.GetAllTasks();

            if (tasks.Count == 0)
            {
                lstTasks.Items.Add("No tasks found. Add one!");
                return;
            }

            foreach (TaskItem task in tasks)
            {
                string status = task.Status == "completed" ? "[✓ DONE]" : "[ ] PENDING";
                string reminder = task.ReminderDate != DateTime.MinValue ? " ⏰ " + task.ReminderDate.ToShortDateString() : "";
                string display = status + " " + task.Title + reminder;
                lstTasks.Items.Add(display);
                lstTasks.Items[lstTasks.Items.Count - 1] = display;
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadTasks();
        }

        private void BtnComplete_Click(object sender, EventArgs e)
        {
            if (lstTasks.SelectedIndex == -1)
            {
                MessageBox.Show("Select a task to complete.");
                return;
            }

            string selected = lstTasks.SelectedItem.ToString();
            if (selected.Contains("[✓ DONE]"))
            {
                MessageBox.Show("This task is already completed.");
                return;
            }

            string title = ExtractTitle(selected);
            bool completed = taskAssistant.CompleteTask(title);

            if (completed)
            {
                MessageBox.Show("Task completed!");
                LoadTasks();
            }
            else
            {
                MessageBox.Show("Could not complete task.");
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (lstTasks.SelectedIndex == -1)
            {
                MessageBox.Show("Select a task to delete.");
                return;
            }

            string selected = lstTasks.SelectedItem.ToString();
            string title = ExtractTitle(selected);

            DialogResult result = MessageBox.Show("Delete '" + title + "'?", "Confirm", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                bool deleted = taskAssistant.DeleteTask(title);
                if (deleted)
                {
                    MessageBox.Show("Task deleted!");
                    LoadTasks();
                }
                else
                {
                    MessageBox.Show("Could not delete task.");
                }
            }
        }

        private string ExtractTitle(string display)
        {
            // Remove status prefix and reminder suffix
            string title = display;
            if (title.Contains("[ DONE]"))
                title = title.Replace("[DONE]", "").Trim();
            else if (title.Contains("[ ] PENDING"))
                title = title.Replace("[ ] PENDING", "").Trim();

            // Remove reminder part if exists
            int reminderIndex = title.IndexOf("");
            if (reminderIndex > 0)
                title = title.Substring(0, reminderIndex).Trim();

            return title;
        }
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace KamoChatBotGUI
{
    public partial class ActivityLogForm : Form
    {
        private ActivityLog activityLog;

        public ActivityLogForm(ActivityLog log)
        {
            InitializeComponent();
            activityLog = log;
            LoadLogs();
            this.StartPosition = FormStartPosition.CenterParent;
        }

        private void InitializeComponent()
        {
            this.lblTitle = new Label();
            this.lstLogs = new ListBox();
            this.btnRefresh = new Button();
            this.btnClear = new Button();
            this.btnClose = new Button();
            this.SuspendLayout();

            // Form
            this.Text = "Activity Log";
            this.Size = new Size(600, 450);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(240, 240, 240);

            // lblTitle
            this.lblTitle.Text = "Recent Activity";
            this.lblTitle.Location = new Point(20, 15);
            this.lblTitle.Size = new Size(560, 30);
            this.lblTitle.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            this.lblTitle.TextAlign = ContentAlignment.MiddleCenter;

            // lstLogs
            this.lstLogs.Location = new Point(20, 55);
            this.lstLogs.Size = new Size(560, 300);
            this.lstLogs.Font = new Font("Segoe UI", 9);
            this.lstLogs.BackColor = Color.White;

            // btnRefresh
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Location = new Point(20, 365);
            this.btnRefresh.Size = new Size(120, 35);
            this.btnRefresh.BackColor = Color.FromArgb(0, 132, 255);
            this.btnRefresh.ForeColor = Color.White;
            this.btnRefresh.FlatStyle = FlatStyle.Flat;
            this.btnRefresh.Click += new EventHandler(BtnRefresh_Click);

            // btnClear
            this.btnClear.Text = "Clear All";
            this.btnClear.Location = new Point(150, 365);
            this.btnClear.Size = new Size(120, 35);
            this.btnClear.BackColor = Color.FromArgb(255, 80, 80);
            this.btnClear.ForeColor = Color.White;
            this.btnClear.FlatStyle = FlatStyle.Flat;
            this.btnClear.Click += new EventHandler(BtnClear_Click);

            // btnClose
            this.btnClose.Text = "Close";
            this.btnClose.Location = new Point(480, 365);
            this.btnClose.Size = new Size(100, 35);
            this.btnClose.BackColor = Color.Gray;
            this.btnClose.ForeColor = Color.White;
            this.btnClose.FlatStyle = FlatStyle.Flat;
            this.btnClose.Click += BtnClose_Click;

            // Add controls
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lstLogs);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnClose);

            this.ResumeLayout(false);
        }

        private Label lblTitle;
        private ListBox lstLogs;
        private Button btnRefresh;
        private Button btnClear;
        private Button btnClose;

        private void LoadLogs()
        {
            lstLogs.Items.Clear();
            List<string> logs = activityLog.GetRecentLogs(50);

            if (logs.Count == 0)
            {
                lstLogs.Items.Add("No activity recorded yet.");
                return;
            }

            foreach (string log in logs)
            {
                lstLogs.Items.Add(log);
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadLogs();
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Clear all activity logs?", "Confirm", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                activityLog.ClearLogs();
                LoadLogs();
                MessageBox.Show("Logs cleared!");
            }
        }
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
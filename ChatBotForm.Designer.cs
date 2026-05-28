using System.Drawing;
using System.Windows.Forms;

namespace KamoChatBotGUI
{
    partial class ChatBotForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            txtAsciiArt = new TextBox();
            pnlChatContainer = new Panel();
            flowChat = new FlowLayoutPanel();
            txtUserInput = new TextBox();
            btnSend = new Button();
            pnlChatContainer.SuspendLayout();
            SuspendLayout();
            // 
            // txtAsciiArt
            // 
            txtAsciiArt.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtAsciiArt.BackColor = Color.Black;
            txtAsciiArt.Font = new Font("Consolas", 8F);
            txtAsciiArt.ForeColor = Color.Magenta;
            txtAsciiArt.Location = new Point(12, 3);
            txtAsciiArt.Multiline = true;
            txtAsciiArt.Name = "txtAsciiArt";
            txtAsciiArt.ReadOnly = true;
            txtAsciiArt.Size = new Size(760, 103);
            txtAsciiArt.TabIndex = 0;
            // 
            // pnlChatContainer
            // 
            pnlChatContainer.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlChatContainer.AutoScroll = true;
            pnlChatContainer.BackColor = Color.FromArgb(240, 240, 240);
            pnlChatContainer.Controls.Add(flowChat);
            pnlChatContainer.Location = new Point(12, 112);
            pnlChatContainer.Name = "pnlChatContainer";
            pnlChatContainer.Size = new Size(760, 248);
            pnlChatContainer.TabIndex = 1;
            // 
            // flowChat
            // 
            flowChat.AutoSize = true;
            flowChat.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowChat.BackColor = Color.FromArgb(240, 240, 240);
            flowChat.FlowDirection = FlowDirection.TopDown;
            flowChat.Location = new Point(0, 0);
            flowChat.Name = "flowChat";
            flowChat.Size = new Size(0, 0);
            flowChat.TabIndex = 0;
            flowChat.WrapContents = false;
            // 
            // txtUserInput
            // 
            txtUserInput.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtUserInput.Font = new Font("Segoe UI", 10F);
            txtUserInput.Location = new Point(12, 370);
            txtUserInput.Name = "txtUserInput";
            txtUserInput.Size = new Size(630, 25);
            txtUserInput.TabIndex = 2;
            // 
            // btnSend
            // 
            btnSend.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnSend.BackColor = Color.FromArgb(0, 132, 255);
            btnSend.FlatStyle = FlatStyle.Flat;
            btnSend.ForeColor = Color.White;
            btnSend.Location = new Point(650, 368);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(122, 30);
            btnSend.TabIndex = 3;
            btnSend.Text = "Send";
            btnSend.UseVisualStyleBackColor = false;
            btnSend.Click += btnSend_Click;
            // 
            // ChatBotForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 240, 240);
            ClientSize = new Size(800, 420);
            Controls.Add(btnSend);
            Controls.Add(txtUserInput);
            Controls.Add(pnlChatContainer);
            Controls.Add(txtAsciiArt);
            Name = "ChatBotForm";
            Text = "Cybersecurity Awareness Bot";
            pnlChatContainer.ResumeLayout(false);
            pnlChatContainer.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private TextBox txtAsciiArt;
        private Panel pnlChatContainer;
        private FlowLayoutPanel flowChat;
        private TextBox txtUserInput;
        private Button btnSend;
    }
}
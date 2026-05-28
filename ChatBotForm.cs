using System;
using System.Collections.Generic;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace KamoChatBotGUI
{
    public partial class ChatBotForm : Form
    {
        private Greeting greeting;
        private ResponseEngine responseEngine;
        private Random random;
        private Dictionary<string, string> userMemory;
        private string currentTopic;
        private string userName;
        private Image botImage;
        private Image userImage;

        public ChatBotForm()
        {
            InitializeComponent();
            LoadImages();
            InitializeChatBot();
        }

        private void LoadImages()
        {
            string botPath = Application.StartupPath + "\\bot.jpeg";
            string userPath = Application.StartupPath + "\\user.jpeg";

            try
            {
                if (System.IO.File.Exists(botPath))
                    botImage = Image.FromFile(botPath);
                else
                    botImage = null;
            }
            catch { botImage = null; }

            try
            {
                if (System.IO.File.Exists(userPath))
                    userImage = Image.FromFile(userPath);
                else
                    userImage = null;
            }
            catch { userImage = null; }
        }

        private void InitializeChatBot()
        {
            greeting = new Greeting();
            responseEngine = new ResponseEngine();
            random = new Random();
            userMemory = new Dictionary<string, string>();
            currentTopic = "";
            userName = "";

            PlayVoiceGreeting();
            DisplayAsciiArt();
            AppendBotMessage("Welcome to the Cybersecurity Awareness Bot.");
            AppendBotMessage("What is your name?");

            txtUserInput.KeyPress += txtUserInput_KeyPress;
        }

        private void PlayVoiceGreeting()
        {
            try
            {
                VoiceGreeting.AudioHelper("Kamo.wav");
            }
            catch { }
        }

        private void DisplayAsciiArt()
        {
            txtAsciiArt.Text = AsciiArt.GetLogo();
            txtAsciiArt.Font = new Font("Consolas", 8);
        }

        private string GetTimestamp()
        {
            return DateTime.Now.ToString("HH:mm");
        }

        private void AppendBotMessage(string message)
        {
            Panel msgPanel = new Panel();
            msgPanel.AutoSize = true;
            msgPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            msgPanel.Margin = new Padding(5, 5, 5, 5);
            msgPanel.BackColor = Color.FromArgb(240, 240, 240);
            msgPanel.Width = flowChat.Width - 20;

            PictureBox avatar = new PictureBox();
            avatar.Size = new Size(35, 35);
            avatar.SizeMode = PictureBoxSizeMode.StretchImage;
            if (botImage != null) avatar.Image = botImage;
            avatar.Location = new Point(5, 5);

            Panel bubblePanel = new Panel();
            bubblePanel.AutoSize = true;
            bubblePanel.BackColor = Color.FromArgb(225, 225, 225);
            bubblePanel.Location = new Point(45, 0);

            Label lblTimestamp = new Label();
            lblTimestamp.Text = "KamoBot " + GetTimestamp();
            lblTimestamp.Font = new Font("Segoe UI", 7, FontStyle.Italic);
            lblTimestamp.ForeColor = Color.Gray;
            lblTimestamp.AutoSize = true;
            lblTimestamp.Location = new Point(10, 5);

            Label lblMessage = new Label();
            lblMessage.Text = message;
            lblMessage.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            lblMessage.ForeColor = Color.Black;
            lblMessage.BackColor = Color.FromArgb(225, 225, 225);
            lblMessage.AutoSize = true;
            lblMessage.MaximumSize = new Size(500, 0);
            lblMessage.Location = new Point(10, 25);

            bubblePanel.Controls.Add(lblTimestamp);
            bubblePanel.Controls.Add(lblMessage);
            bubblePanel.Height = lblMessage.Bottom + 10;
            bubblePanel.Width = Math.Max(lblTimestamp.Width, lblMessage.Width) + 20;

            msgPanel.Controls.Add(avatar);
            msgPanel.Controls.Add(bubblePanel);

            flowChat.Controls.Add(msgPanel);
            flowChat.ScrollControlIntoView(msgPanel);
        }

        private void AppendUserMessage(string message)
        {
            Panel msgPanel = new Panel();
            msgPanel.AutoSize = true;
            msgPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            msgPanel.Margin = new Padding(5, 5, 5, 5);
            msgPanel.BackColor = Color.FromArgb(240, 240, 240);
            msgPanel.Width = flowChat.Width - 20;

            Panel bubblePanel = new Panel();
            bubblePanel.AutoSize = true;
            bubblePanel.BackColor = Color.FromArgb(0, 132, 255);

            Label lblTimestamp = new Label();
            lblTimestamp.Text = GetTimestamp();
            lblTimestamp.Font = new Font("Segoe UI", 7, FontStyle.Italic);
            lblTimestamp.ForeColor = Color.LightGray;
            lblTimestamp.AutoSize = true;
            lblTimestamp.Location = new Point(10, 5);

            Label lblMessage = new Label();
            lblMessage.Text = message;
            lblMessage.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            lblMessage.ForeColor = Color.White;
            lblMessage.BackColor = Color.FromArgb(0, 132, 255);
            lblMessage.AutoSize = true;
            lblMessage.MaximumSize = new Size(500, 0);
            lblMessage.Location = new Point(10, 25);

            bubblePanel.Controls.Add(lblTimestamp);
            bubblePanel.Controls.Add(lblMessage);
            bubblePanel.Height = lblMessage.Bottom + 10;
            bubblePanel.Width = Math.Max(lblTimestamp.Width, lblMessage.Width) + 20;
            bubblePanel.Location = new Point(msgPanel.Width - bubblePanel.Width - 45, 0);

            PictureBox avatar = new PictureBox();
            avatar.Size = new Size(35, 35);
            avatar.SizeMode = PictureBoxSizeMode.StretchImage;
            if (userImage != null) avatar.Image = userImage;
            avatar.Location = new Point(msgPanel.Width - 40, 5);

            msgPanel.Controls.Add(bubblePanel);
            msgPanel.Controls.Add(avatar);

            flowChat.Controls.Add(msgPanel);
            flowChat.ScrollControlIntoView(msgPanel);
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string userInput = txtUserInput.Text.Trim();

            if (string.IsNullOrEmpty(userInput))
            {
                AppendBotMessage("Please type something.");
                return;
            }

            AppendUserMessage(userInput);
            ProcessUserInput(userInput);
            txtUserInput.Clear();
            txtUserInput.Focus();
        }

        private void ProcessUserInput(string input)
        {
            string lowerInput = input.ToLower();

            if (string.IsNullOrEmpty(userName))
            {
                if (greeting.SetUserName(input))
                {
                    userName = greeting.UserName;
                    userMemory["Name"] = userName;
                    AppendBotMessage(greeting.GetWelcomeMessage());
                }
                else
                {
                    AppendBotMessage("Please enter a valid name.");
                }
                return;
            }

            if (lowerInput == "bye" || lowerInput == "exit")
            {
                AppendBotMessage("Goodbye! Stay safe online.");
                return;
            }

            if (lowerInput == "help" || lowerInput == "what can i ask")
            {
                AppendBotMessage(responseEngine.GetHelpMessage(userName));
                return;
            }

            if (lowerInput.Contains("remember") || lowerInput.Contains("what do you know"))
            {
                RecallUserInfo();
                return;
            }

            if (IsFollowUpRequest(lowerInput))
            {
                HandleFollowUp();
                return;
            }

            string sentiment = DetectSentiment(lowerInput);
            string response = GetKeywordResponse(lowerInput, sentiment);

            if (response == null)
            {
                response = responseEngine.GetResponse(input, userName);
            }

            AppendBotMessage(response);
            StoreCurrentTopic(lowerInput);
        }

        private string DetectSentiment(string input)
        {
            if (input.Contains("worried") || input.Contains("scared") || input.Contains("nervous"))
                return "worried";

            if (input.Contains("frustrated") || input.Contains("confused"))
                return "frustrated";

            if (input.Contains("curious") || input.Contains("interested"))
                return "curious";

            return "neutral";
        }

        private string GetKeywordResponse(string input, string sentiment)
        {
            if (input.Contains("password"))
            {
                string[] tips = {
                    "Use at least 12 characters with uppercase, lowercase, numbers and symbols.",
                    "Never reuse passwords across different accounts.",
                    "Consider using a password manager to store strong passwords.",
                    "Enable two-factor authentication whenever possible."
                };
                string tip = tips[random.Next(tips.Length)];

                if (sentiment == "worried")
                    return "Dont worry. " + tip;
                else if (sentiment == "curious")
                    return "Good question. " + tip;

                return tip;
            }

            if (input.Contains("scam") || input.Contains("phish"))
            {
                string[] tips = {
                    "Never click links in unexpected emails. Check the senders email address carefully.",
                    "Scammers create urgency. If someone demands immediate action, its likely a scam.",
                    "Legitimate companies never ask for your password via email.",
                    "Look for spelling errors and generic greetings like Dear Customer."
                };
                string tip = tips[random.Next(tips.Length)];

                if (sentiment == "worried")
                    return "Its normal to be concerned. " + tip;

                return tip;
            }

            if (input.Contains("privacy"))
            {
                string[] tips = {
                    "Review your social media privacy settings monthly.",
                    "Use a VPN on public WiFi to encrypt your data.",
                    "Regularly check which apps have access to your accounts.",
                    "Cover your webcam when not in use."
                };
                string tip = tips[random.Next(tips.Length)];

                userMemory["Interest"] = "privacy";
                return tip + " I will remember you like privacy.";
            }

            if (input.Contains("2fa") || input.Contains("two factor"))
            {
                return "Two-factor authentication adds an extra security layer. Always enable it when available.";
            }

            return null;
        }

        private bool IsFollowUpRequest(string input)
        {
            string[] triggers = { "another tip", "tell me more", "explain more", "more", "another" };
            foreach (string trigger in triggers)
            {
                if (input.Contains(trigger))
                    return true;
            }
            return false;
        }

        private void HandleFollowUp()
        {
            if (currentTopic == "password")
            {
                string[] tips = {
                    "Use a passphrase with random words.",
                    "Change passwords immediately if you suspect a breach.",
                    "Dont store passwords in plain text files."
                };
                AppendBotMessage(tips[random.Next(tips.Length)]);
            }
            else if (currentTopic == "scam")
            {
                string[] tips = {
                    "Report phishing emails to your email provider.",
                    "Dont call phone numbers from suspicious emails.",
                    "Scammers often pretend to be from tech support."
                };
                AppendBotMessage(tips[random.Next(tips.Length)]);
            }
            else if (currentTopic == "privacy")
            {
                string[] tips = {
                    "Use different email addresses for different services.",
                    "Turn off location tracking for apps.",
                    "Regularly clear your browser cookies."
                };
                AppendBotMessage(tips[random.Next(tips.Length)]);
            }
            else
            {
                AppendBotMessage("What topic would you like more tips on? Try password, scam, or privacy.");
            }
        }

        private void StoreCurrentTopic(string input)
        {
            if (input.Contains("password"))
                currentTopic = "password";
            else if (input.Contains("scam") || input.Contains("phish"))
                currentTopic = "scam";
            else if (input.Contains("privacy"))
                currentTopic = "privacy";
        }

        private void RecallUserInfo()
        {
            if (userMemory.Count == 0)
            {
                AppendBotMessage("I dont know much about you yet. Tell me your interests.");
                return;
            }

            string message = "Here is what I remember about you:";
            foreach (var item in userMemory)
            {
                message = message + Environment.NewLine + "- " + item.Key + ": " + item.Value;
            }

            if (userMemory.ContainsKey("Interest"))
            {
                message = message + Environment.NewLine + "Since you like " + userMemory["Interest"] + ", focus on privacy settings.";
            }

            AppendBotMessage(message);
        }

        private void txtUserInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnSend_Click(sender, e);
                e.Handled = true;
            }
        }
    }
}
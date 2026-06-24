using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace KamoChatBotGUI
{
    public partial class QuizForm : Form
    {
        private List<QuizQuestion> questions;
        private int currentQuestionIndex = 0;
        private int score = 0;
        private string playerName;
        private int totalQuestions;
        private List<RadioButton> optionButtons = new List<RadioButton>();

        public int Score { get { return score; } }
        public int TotalQuestions { get { return totalQuestions; } }

        public QuizForm(List<QuizQuestion> quizQuestions, string name)
        {
            InitializeComponent();
            questions = quizQuestions;
            playerName = name;
            totalQuestions = questions.Count;
            ShowQuestion();
        }

        private void InitializeComponent()
        {
            this.lblQuestion = new Label();
            this.panelOptions = new Panel();
            this.btnSubmit = new Button();
            this.lblScore = new Label();
            this.lblProgress = new Label();
            this.SuspendLayout();

            // Form
            this.Text = "Cybersecurity Quiz";
            this.Size = new Size(600, 450);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(240, 240, 240);

            // lblProgress
            this.lblProgress.Text = "Question 1 of 10";
            this.lblProgress.Location = new Point(20, 20);
            this.lblProgress.Size = new Size(200, 25);
            this.lblProgress.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            // lblScore
            this.lblScore.Text = "Score: 0";
            this.lblScore.Location = new Point(450, 20);
            this.lblScore.Size = new Size(120, 25);
            this.lblScore.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            this.lblScore.TextAlign = ContentAlignment.MiddleRight;

            // lblQuestion
            this.lblQuestion.Text = "";
            this.lblQuestion.Location = new Point(20, 60);
            this.lblQuestion.Size = new Size(540, 60);
            this.lblQuestion.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            this.lblQuestion.AutoSize = false;
            this.lblQuestion.TextAlign = ContentAlignment.MiddleLeft;

            // panelOptions
            this.panelOptions.Location = new Point(20, 130);
            this.panelOptions.Size = new Size(540, 180);
            this.panelOptions.AutoScroll = true;

            // btnSubmit
            this.btnSubmit.Text = "Submit Answer";
            this.btnSubmit.Location = new Point(200, 330);
            this.btnSubmit.Size = new Size(150, 40);
            this.btnSubmit.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            this.btnSubmit.BackColor = Color.FromArgb(0, 132, 255);
            this.btnSubmit.ForeColor = Color.White;
            this.btnSubmit.FlatStyle = FlatStyle.Flat;
            this.btnSubmit.Click += BtnSubmit_Click;

            // Add controls
            this.Controls.Add(lblProgress);
            this.Controls.Add(lblScore);
            this.Controls.Add(lblQuestion);
            this.Controls.Add(panelOptions);
            this.Controls.Add(btnSubmit);

            this.ResumeLayout(false);
        }

        private Label lblQuestion;
        private Panel panelOptions;
        private Button btnSubmit;
        private Label lblScore;
        private Label lblProgress;

        private void ShowQuestion()
        {
            if (currentQuestionIndex >= questions.Count)
            {
                FinishQuiz();
                return;
            }

            QuizQuestion q = questions[currentQuestionIndex];
            lblQuestion.Text = (currentQuestionIndex + 1) + ". " + q.Question;
            lblProgress.Text = $"Question {currentQuestionIndex + 1} of {questions.Count}";
            lblScore.Text = $"Score: {score}";

            // Clear previous options
            panelOptions.Controls.Clear();
            optionButtons.Clear();

            int y = 10;
            for (int i = 0; i < q.Options.Count; i++)
            {
                RadioButton rb = new RadioButton();
                rb.Text = q.Options[i];
                rb.Location = new Point(10, y);
                rb.Size = new Size(500, 30);
                rb.Font = new Font("Segoe UI", 10);
                rb.Tag = i;
                panelOptions.Controls.Add(rb);
                optionButtons.Add(rb);
                y += 35;
            }

            btnSubmit.Enabled = true;
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            int selectedIndex = -1;
            for (int i = 0; i < optionButtons.Count; i++)
            {
                if (optionButtons[i].Checked)
                {
                    selectedIndex = i;
                    break;
                }
            }

            if (selectedIndex == -1)
            {
                MessageBox.Show("Please select an answer.");
                return;
            }

            QuizQuestion q = questions[currentQuestionIndex];

            if (selectedIndex == q.CorrectAnswerIndex)
            {
                score++;
                MessageBox.Show("Correct! " + q.Explanation, "Good Job!");
            }
            else
            {
                MessageBox.Show("Incorrect. " + q.Explanation, "Keep Learning!");
            }

            currentQuestionIndex++;
            btnSubmit.Enabled = false;
            ShowQuestion();
        }

        private void FinishQuiz()
        {
            lblProgress.Text = "Quiz Complete!";
            lblScore.Text = $"Final Score: {score}";
            lblQuestion.Text = $"You scored {score} out of {totalQuestions}!";
            panelOptions.Controls.Clear();
            btnSubmit.Enabled = false;

            string feedback = score >= 8 ? "Excellent! You are a cybersecurity pro!" :
                              score >= 6 ? "Good job! Keep learning!" :
                              "Keep practicing to stay safe online!";

            Label lblFeedback = new Label();
            lblFeedback.Text = feedback;
            lblFeedback.Location = new Point(20, 100);
            lblFeedback.Size = new Size(500, 50);
            lblFeedback.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblFeedback.TextAlign = ContentAlignment.MiddleCenter;
            panelOptions.Controls.Add(lblFeedback);

            Button btnClose = new Button();
            btnClose.Text = "Close";
            btnClose.Location = new Point(220, 170);
            btnClose.Size = new Size(100, 35);
            btnClose.Click += (s, ev) => { this.Close(); };
            panelOptions.Controls.Add(btnClose);
        }
    }
}
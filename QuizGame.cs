using System;
using System.Collections.Generic;
using System.Data;


namespace KamoChatBotGUI
{
    public class QuizGame
    {
        private DatabaseHelper db;

        public QuizGame(DatabaseHelper databaseHelper)
        {
            db = databaseHelper;
        }

        public List<QuizQuestion> GetQuestions()
        {
            List<QuizQuestion> questions = new List<QuizQuestion>();

            questions.Add(new QuizQuestion(
                "What should you do if you receive an email asking for your password?",
                new List<string> { "Reply with your password", "Delete the email", "Report it as phishing", "Ignore it" },
                2,
                "Reporting phishing emails helps prevent scams."
            ));

            questions.Add(new QuizQuestion(
                "What is a strong password?",
                new List<string> { "Your birthday", "password123", "A mix of uppercase, lowercase, numbers and symbols", "Your pet's name" },
                2,
                "A strong password uses a mix of characters and is at least 12 characters long."
            ));

            questions.Add(new QuizQuestion(
                "What is Two-Factor Authentication (2FA)?",
                new List<string> { "A password manager", "A second layer of security", "A type of malware", "A social media login" },
                1,
                "2FA adds an extra layer of security by requiring a second form of verification."
            ));

            questions.Add(new QuizQuestion(
                "What is phishing?",
                new List<string> { "A type of fish", "A hacking method", "A scam to steal personal information", "A computer virus" },
                2,
                "Phishing is a scam where attackers trick you into revealing personal information."
            ));

            questions.Add(new QuizQuestion(
                "Why should you use a VPN on public WiFi?",
                new List<string> { "To make internet faster", "To encrypt your data", "To watch Netflix", "To install software" },
                1,
                "A VPN encrypts your data, protecting it from hackers on public networks."
            ));

            questions.Add(new QuizQuestion(
                "What is ransomware?",
                new List<string> { "Software that locks your files", "A type of antivirus", "A government program", "A social media app" },
                0,
                "Ransomware is malware that encrypts your files and demands payment for release."
            ));

            questions.Add(new QuizQuestion(
                "What should you do if you suspect a data breach?",
                new List<string> { "Ignore it", "Change your passwords immediately", "Wait for notification", "Delete your accounts" },
                1,
                "Changing passwords immediately helps prevent unauthorized access."
            ));

            questions.Add(new QuizQuestion(
                "What is social engineering?",
                new List<string> { "Engineering for society", "Manipulating people to reveal confidential info", "Building social networks", "Software development" },
                1,
                "Social engineering uses psychological manipulation to get confidential information."
            ));

            questions.Add(new QuizQuestion(
                "What is malware?",
                new List<string> { "Software that helps you", "Software designed to harm your computer", "A type of hardware", "A game" },
                1,
                "Malware is malicious software designed to harm or exploit your computer."
            ));

            questions.Add(new QuizQuestion(
                "What should you do with suspicious links?",
                new List<string> { "Click them to see what happens", "Forward them to friends", "Do not click", "Save them for later" },
                2,
                "Never click suspicious links. They may lead to phishing sites or malware."
            ));

            questions.Add(new QuizQuestion(
                "What is the purpose of a firewall?",
                new List<string> { "To block all internet access", "To monitor and control network traffic", "To speed up the internet", "To install software" },
                1,
                "A firewall monitors and controls incoming and outgoing network traffic based on security rules."
            ));

            return questions;
        }

        public bool SaveScore(string playerName, int score, int totalQuestions)
        {
            string sql = $"INSERT INTO quiz_scores (player_name, score, total_questions) VALUES ('{playerName.Replace("'", "''")}', {score}, {totalQuestions})";
            int result = db.ExecuteNonQuery(sql);
            return result > 0;
        }

        public DataTable GetTopScores(int limit = 5)
        {
            string sql = $"SELECT player_name, score, total_questions, date_played FROM quiz_scores ORDER BY score DESC LIMIT {limit}";
            return db.ExecuteQuery(sql);
        }

        public DataTable GetAllScores()
        {
            string sql = "SELECT player_name, score, total_questions, date_played FROM quiz_scores ORDER BY date_played DESC";
            return db.ExecuteQuery(sql);
        }

        public double GetAverageScore()
        {
            string sql = "SELECT AVG(score) FROM quiz_scores";
            object result = db.ExecuteScalar(sql);
            return result != null ? Convert.ToDouble(result) : 0;
        }
    }
}
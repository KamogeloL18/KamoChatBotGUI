using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;  // Keep this for potential future use, but we won't use Console

namespace KamoChatBotGUI  // Change namespace to match your GUI project
{
    public class ResponseEngine
    {
        // Dictionary to store cyber questions and answers
        private Dictionary<string, string> responses;

        // Constructor that initializes the dictionary
        public ResponseEngine()
        {
            responses = new Dictionary<string, string>();

            responses.Add("hello", "Hello! How can I help you stay safe online?");
            responses.Add("hi", "Hi! How can I help you stay safe online?");
            responses.Add("how are you?", "I'm doing good, thanks for asking!");
            responses.Add("purpose", "My purpose is to guide you on how to stay safe online!");
            responses.Add("who are you?", "I'm KamoChatBot, your cybersecurity assistant. You can ask me questions that relate to anything with cyber");
            responses.Add("password", "You should use at least 8 characters with uppercase, lowercase, numbers and symbols when you create a password.");
            responses.Add("phishing", "Phishing is when an individual is manipulated to reveal their legitimate information. Always check email senders carefully!");
            responses.Add("social engineering", "Social engineering manipulates people into revealing confidential information. Never share passwords or sensitive data with strangers!");
            responses.Add("two-factor authentication", "Two-factor authentication adds an extra layer of security by requiring a second form of verification in addition to your password.");
            responses.Add("malware", "Malware is malicious software designed to harm or exploit your computer. Always keep your antivirus software updated.");
            responses.Add("ransomware", "Ransomware is a type of malware that encrypts your files and demands payment for their release. Never pay the ransom!");
            responses.Add("vpn", "A VPN (Virtual Private Network) encrypts your internet connection and hides your IP address for increased privacy and security.");
            responses.Add("firewall", "A firewall is a network security system that monitors and controls incoming and outgoing network traffic based on predetermined security rules.");
            responses.Add("data breach", "A data breach occurs when unauthorized individuals access sensitive data. Always use strong passwords and enable two-factor authentication.");
            responses.Add("encryption", "Encryption converts your data into a code that only authorized parties can decode. It's essential for protecting sensitive information.");
            responses.Add("identity theft", "Identity theft is when someone uses your personal information to commit fraud. Monitor your accounts and credit reports regularly.");
            responses.Add("secure browsing", "Use HTTPS websites, avoid public Wi-Fi for sensitive transactions, and keep your browser updated to browse securely.");
            responses.Add("antivirus", "Antivirus software detects and removes malicious programs from your computer. Install reputable antivirus software and keep it updated.");
            responses.Add("bye", "Goodbye! Stay safe online!");
            responses.Add("exit", "Goodbye! Stay safe online!");
        }

        // Method that accepts user input and returns personalized responses (GUI version)
        public string GetResponse(string userInput, string userName)
        {
            // String manipulation
            string input = userInput.Trim().ToLower();

            // Check if user entered any input
            if (input.Length == 0)
            {
                return $"{userName}, you didn't enter anything. Please ask me about cybersecurity!";
            }

            // Check for exit commands
            if (input == "bye" || input == "exit")
            {
                return "Goodbye! Stay safe online! ";
            }

            // Search the dictionary for a matching keyword
            foreach (KeyValuePair<string, string> entry in responses)
            {
                if (input.Contains(entry.Key))
                {
                    return entry.Value.Replace("{name}", userName);
                }
            }

            // If input doesn't match any dictionary keywords
            return $"I'm not sure about that, {userName}. Ask me questions relating to cybersecurity (passwords, phishing, malware, ransomware, etc.)";
        }

        // NEW: Get response with random variation (for Part 2 random responses feature)
        public string GetRandomResponse(string keyword, string userName)
        {
            Random random = new Random();

            switch (keyword.ToLower())
            {
                case "password":
                    string[] passwordTips = {
                        $"Use strong, unique passwords for each account, {userName}! At least 12 characters with numbers and symbols.",
                        $"Avoid using personal info like birthdays or pet names in your passwords, {userName}.",
                        $"Consider using a password manager like Bitwarden or LastPass to store complex passwords securely, {userName}!",
                        $"Enable 2FA whenever possible - it's like a second lock on your digital door, {userName}!"
                    };
                    return passwordTips[random.Next(passwordTips.Length)];

                case "phishing":
                    string[] phishingTips = {
                        $"Never click links in unsolicited emails, {userName}. Hover over links first to see the real URL!",
                        $"Scammers create urgency, {userName}. If someone demands immediate action, it's likely a scam.",
                        $"Legitimate companies never ask for your password via email or phone, {userName}.",
                        $"Look for spelling errors and generic greetings like 'Dear Customer' - these are scam red flags, {userName}!"
                    };
                    return phishingTips[random.Next(phishingTips.Length)];

                default:
                    return GetResponse(keyword, userName);
            }
        }

        // NEW: Get a random default response when no keyword matches
        public string GetRandomDefaultResponse(string userName)
        {
            Random random = new Random();
            string[] defaults = {
                $"I didn't quite understand that, {userName}. Could you rephrase?",
                $"Hmm, I'm not sure about that, {userName}. Can you ask about passwords, phishing, or malware?",
                $"Try asking me about 'password safety' or 'phishing scams', {userName}!",
                $"I'm still learning, {userName}! Want to ask me about cybersecurity topics like ransomware or two-factor authentication?"
            };
            return defaults[random.Next(defaults.Length)];
        }

        // NEW: Get list of all topics the bot can answer about
        public string GetHelpMessage(string userName)
        {
            string topics = "I can help you with:\n";
            foreach (string key in responses.Keys)
            {
                if (key != "bye" && key != "exit" && key != "hello" && key != "hi")
                {
                    topics += $"• {char.ToUpper(key[0]) + key.Substring(1)}\n";
                }
            }
            return $"Here's what I can help you with, {userName}:\n{topics}\nJust type any of these topics!";
        }

        // OLD: TypeText method for Console - KEPT but marked obsolete (won't be used in GUI)
        // You can delete this method if you want, but keeping it won't hurt since GUI won't call it
        [Obsolete("This method is for Console only. GUI uses direct string returns.")]
        public void TypeText(string text, int delayMilliseconds = 30)
        {
            foreach (char character in text)
            {
                Console.Write(character);
                Thread.Sleep(delayMilliseconds);
            }
            Console.WriteLine();
            Console.ResetColor();
        }
    }
}

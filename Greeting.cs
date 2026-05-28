using KamoChatBotGUI;
using System;

namespace KamoChatBotGUI  // Change namespace to match your GUI project
{
    public class Greeting
    {
        // Properties (keeping your shorthand auto-properties)
        public string ChatBotName { get; set; }
        public string UserName { get; set; }

        // Response engine reference
        private ResponseEngine responseEngine;

        // Constructor
        public Greeting()
        {
            ChatBotName = "KamoChatBot";
            UserName = "";
            responseEngine = new ResponseEngine();
        }

        // NEW: Validate and set username (returns true if valid, false if empty)
        public bool SetUserName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return false;  // Invalid - name is empty
            }

            UserName = name.Trim();
            return true;  // Valid name
        }

        // NEW: Get welcome message as string (not Console output)
        public string GetWelcomeMessage()
        {
            return $"Hello {UserName}! I am {ChatBotName}, your cybersecurity assistant.\n\n" +
                   "You can ask me about:\n" +
                   "• Phishing scams\n" +
                   "• Password safety\n" +
                   "• Privacy protection\n" +
                   "• Social engineering\n" +
                   "• Malware & Ransomware\n" +
                   "• DDoS attacks\n\n" +
                   "Type 'help' to see what I can do, or 'bye' to exit.";
        }

        // NEW: Get initial greeting (when app starts, before name is entered)
        public string GetInitialGreeting()
        {
            return $"Hi! I'm {ChatBotName}, your cybersecurity awareness chatbot.\n\n" +
                   "I'm here to help you stay safe online! What's your name?";
        }

        // NEW: Process user input and get response (GUI-friendly)
        public string ProcessChat(string userInput)
        {
            if (string.IsNullOrWhiteSpace(userInput))
            {
                return "Please type something. I'd love to help you learn about cybersecurity!";
            }

            string lowerInput = userInput.ToLower();

            // Check for exit command
            if (lowerInput == "bye" || lowerInput == "exit")
            {
                return "Goodbye! Stay safe online!";
            }

            // Get response from ResponseEngine
            string response = responseEngine.GetResponse(userInput, UserName);
            return response;
        }

        // NEW: Check if conversation should exit
        public bool ShouldExit(string input)
        {
            string lowerInput = input?.ToLower() ?? "";
            return lowerInput == "bye" || lowerInput == "exit";
        }

        // Optional: Keep your original methods but mark as obsolete for GUI
        // (These won't be used in GUI but kept for reference)
        [Obsolete("This method is for Console only. Use GUI version instead.")]
        public void getUserName()
        {
            // Original Console version - don't use in GUI
        }

        [Obsolete("This method is for Console only. Use GUI version instead.")]
        public void MainChat()
        {
            // Original Console version - don't use in GUI
        }
    }
}
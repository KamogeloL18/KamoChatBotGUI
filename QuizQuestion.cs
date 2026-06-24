using System.Collections.Generic;

namespace KamoChatBotGUI
{
    public class QuizQuestion
    {
        public string Question { get; set; }
        public List<string> Options { get; set; }
        public int CorrectAnswerIndex { get; set; }
        public string Explanation { get; set; }

        public QuizQuestion()
        {
            Question = "";
            Options = new List<string>();
            CorrectAnswerIndex = 0;
            Explanation = "";
        }

        public QuizQuestion(string question, List<string> options, int correctIndex, string explanation)
        {
            Question = question;
            Options = options;
            CorrectAnswerIndex = correctIndex;
            Explanation = explanation;
        }
    }
}
using System.Collections.Generic;

namespace FuraiQ
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class Quiz : IQuiz
    {
        public string Question { get; }

        public List<QuizOption> Options { get; }

        public bool IsCorrect(List<int> selectedIndexes)
        {
            foreach (var index in selectedIndexes)
            {
                if (!Options[index].isCorrect)
                {
                    return false;
                }
            }
            return true;
        }

        public Quiz(string message, List<QuizOption> options)
        {
            Question = message;
            Options = options;
        }
    }
}

using UnityEngine;

namespace FuraiQ
{
    /// <summary>
    /// クイズビルダー
    /// </summary>
    public abstract class QuizBuilder : ScriptableObject, IQuizBuilder
    {
        public abstract IQuiz Build();
    }
}

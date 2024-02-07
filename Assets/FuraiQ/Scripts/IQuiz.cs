using System.Collections.Generic;

namespace FuraiQ
{
    /// <summary>
    /// クイズ
    /// </summary>
    public interface IQuiz
    {
        /// <summary>
        /// 問題文
        /// </summary>
        string Message { get; }

        /// <summary>
        /// 選択肢
        /// </summary>
        List<QuizOption> Options { get; }

        /// <summary>
        /// <paramref name="selectedIndexes"/>が全て正解か返す
        /// </summary>
        bool IsCorrect(List<int> selectedIndexes);
    }
}

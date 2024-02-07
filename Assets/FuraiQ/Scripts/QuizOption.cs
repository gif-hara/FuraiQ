using System;

namespace FuraiQ
{
    /// <summary>
    /// クイズの選択肢
    /// </summary>
    [Serializable]
    public sealed class QuizOption
    {
        /// <summary>
        /// 正解か
        /// </summary>
        public bool isCorrect;

        /// <summary>
        /// 選択肢
        /// </summary>
        public string message;
    }
}

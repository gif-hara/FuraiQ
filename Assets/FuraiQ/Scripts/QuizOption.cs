using System;
using UnityEngine;

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

        /// <summary>
        /// ボタンの色
        /// </summary>
        public Color buttonColor = new(188 / 255.0f, 188 / 255.0f, 188 / 255.0f, 1.0f);
    }
}

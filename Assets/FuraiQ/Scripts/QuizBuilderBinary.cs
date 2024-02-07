using System.Collections.Generic;
using UnityEngine;

namespace FuraiQ
{
    /// <summary>
    /// 2択のクイズビルダー
    /// </summary>
    [CreateAssetMenu(menuName = "FuraiQ/QuizBuilderBinary")]
    public sealed class QuizBuilderBinary : QuizBuilder
    {
        [SerializeField]
        private string question;

        [SerializeField]
        private QuizOption leftOption;

        [SerializeField]
        private QuizOption rightOption;

        public override IQuiz Build()
        {
            return new Quiz(question, new List<QuizOption> { leftOption, rightOption });
        }
    }
}

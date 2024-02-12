using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FuraiQ
{
    /// <summary>
    /// クイズビルダー
    /// </summary>
    [CreateAssetMenu(menuName = "FuraiQ/QuizBuilderBasic")]
    public sealed class QuizBuilderBasic : QuizBuilder
    {
        [SerializeField]
        private string question;

        [SerializeField]
        private List<string> correctOptions;

        [SerializeField]
        private List<string> incorrectOptions;

        [SerializeField]
        private int optionNumber;

        public override IQuiz Build()
        {
            var options = new List<QuizOption>
            {
                new() { message = correctOptions.OrderBy(_ => Guid.NewGuid()).First(), isCorrect = true }
            };
            var incorrectIndexies = Enumerable.Range(0, incorrectOptions.Count).OrderBy(_ => Guid.NewGuid()).ToList();
            for (int i = 0; i < optionNumber - 1; i++)
            {
                if (i > incorrectIndexies.Count - 1)
                {
                    break;
                }
                options.Add(new QuizOption { message = incorrectOptions[incorrectIndexies[i]], isCorrect = false });
            }
            return new Quiz(question, options.OrderBy(i => Guid.NewGuid()).ToList());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FuraiQ
{
    /// <summary>
    /// 2択のクイズビルダー
    /// </summary>
    [CreateAssetMenu(menuName = "FuraiQ/BasicQuizBuilder")]
    public sealed class BasicQuizBuilder : QuizBuilder
    {
        [SerializeField]
        private string question;

        [SerializeField]
        private string correctOption;

        [SerializeField]
        private List<string> incorrectOptions;

        [SerializeField]
        private int optionNumber;

        public override IQuiz Build()
        {
            var options = new List<QuizOption>
            {
                new() { message = correctOption, isCorrect = true }
            };
            var incorrectIndexies = Enumerable.Range(0, incorrectOptions.Count).OrderBy(i => Guid.NewGuid()).ToList();
            for (int i = 0; i < optionNumber - 1; i++)
            {
                options.Add(new QuizOption { message = incorrectOptions[incorrectIndexies[i]], isCorrect = false });
            }
            return new Quiz(question, options.OrderBy(i => Guid.NewGuid()).ToList());
        }
    }
}

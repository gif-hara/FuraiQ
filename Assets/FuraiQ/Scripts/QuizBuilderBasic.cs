using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var options = new List<(string option, bool isCorrect)>
            {
                new()
                {
                    option = correctOptions.OrderBy(_ => Guid.NewGuid()).First(),
                    isCorrect = true
                }
            };
            var incorrectIndexies = Enumerable.Range(0, incorrectOptions.Count).OrderBy(_ => Guid.NewGuid()).ToList();
            for (int i = 0; i < optionNumber - 1; i++)
            {
                if (i > incorrectIndexies.Count - 1)
                {
                    break;
                }
                options.Add
                (
                    new()
                    {
                        option = incorrectOptions[incorrectIndexies[i]],
                        isCorrect = false
                    }
                );
            }
            options = options.OrderBy(_ => Guid.NewGuid()).ToList();
            var sb = new StringBuilder();
            sb.AppendLine(question);
            sb.AppendLine();
            for (var i = 0; i < options.Count; i++)
            {
                sb.AppendLine($"{i + 1}. {options[i].option}");
            }
            return new Quiz(sb.ToString(), options.Select((x, index) => new QuizOption { message = (index + 1).ToString(), isCorrect = x.isCorrect }).ToList());
        }
    }
}

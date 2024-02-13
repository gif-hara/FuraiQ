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

        [SerializeField]
        private List<Color> colors;

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
            var quizOptions = new List<QuizOption>();
            var sb = new StringBuilder();
            sb.AppendLine(question);
            sb.AppendLine();
            for (var i = 0; i < options.Count; i++)
            {
                var color = $"#{ColorUtility.ToHtmlStringRGB(colors[i])}";
                sb.AppendLine($"<color={color}>{i + 1}.</color> <u color={color}>{options[i].option}</u>");
                quizOptions.Add(new QuizOption
                {
                    message = $"<size=60>{(i + 1).ToString()}</size>",
                    isCorrect = options[i].isCorrect,
                    buttonColor = colors[i]
                });
            }
            return new Quiz(sb.ToString(), quizOptions);
        }
    }
}

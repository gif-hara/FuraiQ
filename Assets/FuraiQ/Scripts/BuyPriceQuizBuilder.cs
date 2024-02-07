using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FuraiQ
{
    /// <summary>
    /// 2択のクイズビルダー
    /// </summary>
    [CreateAssetMenu(menuName = "FuraiQ/BuyPriceQuizBuilder")]
    public sealed class BuyPriceQuizBuilder : QuizBuilder
    {
        [SerializeField]
        private string questionFormat;

        [SerializeField]
        private ItemMasterData itemMasterData;

        [SerializeField]
        private int optionNumber;

        public override IQuiz Build()
        {
            var shuffledItems = itemMasterData.Items
                .Where(x => !x.alwaysIdentified)
                .OrderBy(i => Guid.NewGuid())
                .ToList();
            var targetItem = shuffledItems[0];
            shuffledItems.RemoveAt(0);
            var targetBuyPrice = targetItem.BuyPrices[UnityEngine.Random.Range(0, targetItem.BuyPrices.Length)];
            var question = string.Format(questionFormat, targetBuyPrice);
            var options = new List<QuizOption>
            {
                new() { message = targetItem.name, isCorrect = true }
            };
            while (options.Count < optionNumber && shuffledItems.Count > 0)
            {
                targetItem = shuffledItems[0];
                shuffledItems.RemoveAt(0);
                if (!targetItem.BuyPrices.Any(price => price == targetBuyPrice))
                {
                    options.Add(new QuizOption { message = targetItem.name, isCorrect = false });
                }
            }

            return new Quiz(question, options.OrderBy(i => Guid.NewGuid()).ToList());
        }
    }
}

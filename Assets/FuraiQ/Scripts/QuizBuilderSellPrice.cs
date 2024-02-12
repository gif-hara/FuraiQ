using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FuraiQ
{
    /// <summary>
    /// 売値クイズビルダー
    /// </summary>
    [CreateAssetMenu(menuName = "FuraiQ/QuizBuilderSellPrice")]
    public sealed class QuizBuilderSellPrice : QuizBuilder
    {
        [SerializeField]
        private string questionFormat;

        [SerializeField]
        private ItemMasterData itemMasterData;

        [SerializeField]
        private int optionNumber;

        [SerializeField]
        private int weightStandardPrice;

        [SerializeField]
        private int weightBlessedPrice;

        [SerializeField]
        private int weightCursedPrice;

        public override IQuiz Build()
        {
            var shuffledItems = itemMasterData.Items
                .Where(x => !x.alwaysIdentified)
                .OrderBy(i => Guid.NewGuid())
                .ToList();
            var targetItem = shuffledItems[0];
            shuffledItems.RemoveAt(0);
            var sellPrices = targetItem.GetSellPriceWeights(weightStandardPrice, weightBlessedPrice, weightCursedPrice);
            var targetBuyPrice = sellPrices.Lottery(x => x.weight).price;
            var question = string.Format(questionFormat, targetBuyPrice);
            var options = new List<QuizOption>
            {
                new() { message = targetItem.name, isCorrect = true }
            };
            while (options.Count < optionNumber && shuffledItems.Count > 0)
            {
                targetItem = shuffledItems[0];
                shuffledItems.RemoveAt(0);
                if (!targetItem.SellPrices.Any(price => price == targetBuyPrice))
                {
                    options.Add(new QuizOption { message = targetItem.name, isCorrect = false });
                }
            }

            return new Quiz(question, options.OrderBy(i => Guid.NewGuid()).ToList());
        }
    }
}

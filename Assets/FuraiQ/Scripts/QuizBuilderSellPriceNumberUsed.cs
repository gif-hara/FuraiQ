using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FuraiQ
{
    /// <summary>
    /// 使用回数付きの売値のクイズビルダー
    /// </summary>
    [CreateAssetMenu(menuName = "FuraiQ/QuizBuilderSellPriceNumberUsed")]
    public sealed class QuizBuilderSellPriceNumberUsed : QuizBuilder
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
            var sellNumberUsedPrices = targetItem.SellNumberUsedPrices;
            var targetBuyPrice = sellNumberUsedPrices[UnityEngine.Random.Range(0, sellNumberUsedPrices.Length)];
            var question = string.Format(questionFormat, targetBuyPrice);
            var options = new List<QuizOption>
            {
                new() { message = targetItem.name, isCorrect = true }
            };
            while (options.Count < optionNumber && shuffledItems.Count > 0)
            {
                targetItem = shuffledItems[0];
                shuffledItems.RemoveAt(0);
                if (!targetItem.SellNumberUsedPrices.Any(price => price == targetBuyPrice))
                {
                    options.Add(new QuizOption { message = targetItem.name, isCorrect = false });
                }
            }

            return new Quiz(question, options.OrderBy(i => Guid.NewGuid()).ToList());
        }
    }
}

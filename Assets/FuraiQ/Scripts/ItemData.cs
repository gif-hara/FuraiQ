using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace FuraiQ
{
    /// <summary>
    /// アイテムデータ
    /// </summary>
    [Serializable]
    public sealed class ItemData
    {
        /// <summary>
        /// アイテム名
        /// </summary>
        public string name;

        /// <summary>
        /// アイテムの買値
        /// </summary>
        public int buyPrice;

        /// <summary>
        /// アイテムの売値
        /// </summary>
        public int sellPrice;

        /// <summary>
        /// 常に識別されているか
        /// </summary>
        public bool alwaysIdentified;

        /// <summary>
        /// 祝福可能か
        /// </summary>
        public bool blessable = true;

        /// <summary>
        /// 呪われるか
        /// </summary>
        public bool curseable = true;

        /// <summary>
        /// 祝福されている場合の買値
        /// </summary>
        public int buyPriceBlessing => buyPrice * 2;

        /// <summary>
        /// 祝福されている場合の売値
        /// </summary>
        public int sellPriceBlessing => sellPrice * 2;

        /// <summary>
        /// アイテムの特殊な属性リスト
        /// </summary>
        public ItemAttribute[] attributes;

        /// <summary>
        /// 呪われている場合の買値
        /// </summary>
        public int buyPriceCurse => Mathf.FloorToInt(buyPrice * 0.87f);

        /// <summary>
        /// 呪われている場合の売値
        /// </summary>
        public int sellPriceCurse => Mathf.FloorToInt(sellPrice * 0.87f);

        /// <summary>
        /// 通常・祝福・呪いの買値
        /// </summary>
        public int[] BuyPrices
        {
            get
            {
                var result = new List<int>
                {
                    buyPrice,
                };
                if (blessable)
                {
                    result.Add(buyPriceBlessing);
                }
                if (curseable)
                {
                    result.Add(buyPriceCurse);
                }
                return result.ToArray();
            }
        }

        /// <summary>
        /// 通常・祝福・呪いの売値
        /// </summary>
        public int[] SellPrices
        {
            get
            {
                var result = new List<int>
                {
                    sellPrice,
                };
                if (blessable)
                {
                    result.Add(sellPriceBlessing);
                }
                if (curseable)
                {
                    result.Add(sellPriceCurse);
                }
                return result.ToArray();
            }
        }

        public int[] BuyNumberUsedPrices
        {
            get
            {
                var numberUsedMin = Array.Find(attributes, x => x.key == "NumberUsedMin");
                Assert.IsNotNull(numberUsedMin, $"numberUsedMin != null, {name}");
                var numberUsedMax = Array.Find(attributes, x => x.key == "NumberUsedMax");
                Assert.IsNotNull(numberUsedMax, $"numberUsedMax != null, {name}");
                var addBuyPrice = Array.Find(attributes, x => x.key == "AddBuyPrice");
                Assert.IsNotNull(addBuyPrice, $"addBuyPrice != null, {name}");
                var numberUserMinInt = numberUsedMin.ParseToInt();
                var numberUserMaxInt = numberUsedMax.ParseToInt();
                var addBuyPriceInt = addBuyPrice.ParseToInt();
                var result = new List<int>();
                for (var i = numberUserMinInt; i <= numberUserMaxInt; i++)
                {
                    var price = buyPrice + i * addBuyPriceInt;
                    result.Add(price);
                    result.Add(Mathf.FloorToInt(price * 0.87f));
                }

                return result.ToArray();
            }
        }

        public int[] SellNumberUsedPrices
        {
            get
            {
                var numberUsedMin = Array.Find(attributes, x => x.key == "NumberUsedMin");
                Assert.IsNotNull(numberUsedMin, $"numberUsedMin != null, {name}");
                var numberUsedMax = Array.Find(attributes, x => x.key == "NumberUsedMax");
                Assert.IsNotNull(numberUsedMax, $"numberUsedMax != null, {name}");
                var addSellPrice = Array.Find(attributes, x => x.key == "AddSellPrice");
                Assert.IsNotNull(addSellPrice, $"addSellPrice != null, {name}");
                var numberUserMinInt = numberUsedMin.ParseToInt();
                var numberUserMaxInt = numberUsedMax.ParseToInt();
                var addSellPriceInt = addSellPrice.ParseToInt();
                var result = new List<int>();
                for (var i = numberUserMinInt; i <= numberUserMaxInt; i++)
                {
                    var price = sellPrice + i * addSellPriceInt;
                    result.Add(price);
                    result.Add(Mathf.FloorToInt(price * 0.87f));
                }

                return result.ToArray();
            }
        }
    }
}

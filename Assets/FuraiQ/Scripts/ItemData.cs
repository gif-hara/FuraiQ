using System;
using UnityEngine;

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

        public int[] BuyPrices => new int[] { buyPrice, buyPriceBlessing, buyPriceCurse };

        public int[] SellPrices => new int[] { sellPrice, sellPriceBlessing, sellPriceCurse };
    }
}

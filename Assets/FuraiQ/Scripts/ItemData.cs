using System;

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
    }
}

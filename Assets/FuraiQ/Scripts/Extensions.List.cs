using System;
using System.Collections.Generic;
using UnityEngine.Assertions;
using Random = UnityEngine.Random;

namespace FuraiQ
{
    public static partial class Extensions
    {
        /// <summary>
        /// 抽選を行う
        /// </summary>
        public static T Lottery<T>(this IList<T> self, Func<T, int> weightSelector)
        {
            return Lottery(self, weightSelector, max => Random.Range(0, max));
        }

        /// <summary>
        /// 抽選を行う
        /// </summary>
        public static int LotteryIndex<T>(this IList<T> self, Func<T, int> weightSelector)
        {
            return LotteryIndex(self, weightSelector, max => Random.Range(0, max));
        }

        /// <summary>
        /// 抽選を行う
        /// </summary>
        /// <remarks>
        /// 乱数を独自で実装したい場合に利用します
        /// </remarks>
        public static T Lottery<T>(this IList<T> self, Func<T, int> weightSelector, Func<int, int> randomSelector)
        {
            var max = 0;
            foreach (var i in self)
            {
                max += weightSelector(i);
            }

            var current = 0;
            var random = randomSelector(max);
            foreach (var i in self)
            {
                var w = weightSelector(i);
                if (random >= current && random < current + w)
                {
                    return i;
                }

                current += w;
            }

            Assert.IsTrue(false, "未定義の動作です");
            return default;
        }

        /// <summary>
        /// 抽選を行う
        /// </summary>
        /// <remarks>
        /// 乱数を独自で実装したい場合に利用します
        /// </remarks>
        public static int LotteryIndex<T>(this IList<T> self, Func<T, int> weightSelector, Func<int, int> randomSelector)
        {
            var max = 0;
            foreach (var i in self)
            {
                max += weightSelector(i);
            }

            var current = 0;
            var random = randomSelector(max);
            for (var i = 0; i < self.Count; i++)
            {
                var element = self[i];
                var w = weightSelector(element);
                if (random >= current && random < current + w)
                {
                    return i;
                }

                current += w;
            }

            Assert.IsTrue(false, "未定義の動作です");
            return -1;
        }
    }
}

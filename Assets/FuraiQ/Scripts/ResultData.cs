using System;
using UnityEngine;

namespace FuraiQ
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public sealed class ResultData
    {
        [SerializeField]
        private int correctNumber;

        [SerializeField]
        private int totalNumber;

        public ResultData(int correctNumber, int totalNumber)
        {
            this.correctNumber = correctNumber;
            this.totalNumber = totalNumber;
        }

        public int CorrectNumber => correctNumber;

        public int TotalNumber => totalNumber;

        public string GetRatingName()
        {
            if (correctNumber == totalNumber)
            {
                return "シレンマスター";
            }
            else if (correctNumber >= totalNumber * 0.8f)
            {
                return "上級シレンジャー";
            }
            else if (correctNumber >= totalNumber * 0.6f)
            {
                return "中級シレンジャー";
            }
            else if (correctNumber >= totalNumber * 0.4f)
            {
                return "初級シレンジャー";
            }
            else
            {
                return "おにぎりシレン";
            }
        }
    }
}
